using BooksWebAPI.Data;
using BooksWebAPI.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BooksWebAPI.Application.Services
{
    public class JWTService
    {

        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public JWTService(IConfiguration configuration, AppDbContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GenerateToken(string username, string password)
        {
            // Find user by username
            var user = await _context.AspNetUsers
                .Include(u => u.Roles) // load roles
                .FirstOrDefaultAsync(u => u.UserName == username);

            if (user == null)
                return null;

            // Verify password
            
            if (user.PasswordHash != password)//assuming it is not encrypted
                return null;


            //Create claims
            var claims = new List<Claim>();

            claims.Add(new Claim(ClaimTypes.Name, user.UserName));

            foreach (AspNetRole role in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role.Name));
            }


            //JWT 
            var jwtSettings = _configuration.GetSection("JwtSettings");
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

           

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
