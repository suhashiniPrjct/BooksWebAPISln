using BooksWebAPI.API.Middleware;
using BooksWebAPI.Application.Interfaces;
using BooksWebAPI.Application.Services;
using BooksWebAPI.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//when both frontend backend api are in same domain
//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
//                                   .AddCookie(options => { options.LoginPath = "/account/login"; });// redirect unauthenticated users here
//JWTSettings                                                                                                  //when its a multi domain ,SPA and mobile 
var jwtsettings = builder.Configuration.GetSection("JwtSettings");

builder.Services.AddDbContext<AppDbContext>(options =>
                                            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                                            .LogTo(Console.WriteLine, LogLevel.Information).EnableSensitiveDataLogging());

builder.Services.AddScoped<JWTService, JWTService>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtsettings["Issuer"],
        ValidAudience = jwtsettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtsettings["SecretKey"]))
    };
});

builder.Services.AddAuthorization();



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//to enable aws cloudwatch logging
//builder.Logging.ClearProviders();//remove inbuild ilogger 
//builder.Logging.AddConsole(); //add console logger
//builder.Logging.AddDebug();//added debugger

var app = builder.Build();
app.UseMiddleware<ExceptionMiddleware>();//custom middleware

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
else
{
    app.UseExceptionHandler("/error?");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
