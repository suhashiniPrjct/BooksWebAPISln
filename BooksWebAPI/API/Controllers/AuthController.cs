using BooksWebAPI.Application.DTOs;
using BooksWebAPI.Application.Services;
using BooksWebAPI.Data;
using BooksWebAPI.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace BooksWebAPI.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {

        private readonly JWTService _jwtService;

        //    // Hardcoded users for demo; replace with DB in production
        //    private List<AspNetUser> _users = new List<AspNetUser>
        //{
        //    new AspNetUser 
        //    { 
        //        UserName = "admin", 
        //        PasswordHash = "admin123", 
        //        Roles = new List<AspNetRole>
        //        {
        //           new AspNetRole { Name = "admin" },
        //           new AspNetRole{ Name = "Cleint1" },
        //        }

        //        },
        //   new AspNetUser
        //    {
        //     UserName = "user",
        //        PasswordHash = "user111",
        //        Roles = new List<AspNetRole>
        //        {
        //           new AspNetRole { Name = "user" },
        //           new AspNetRole{ Name = "Manager" },
        //        }
        //   }
        //};

        public AuthController(JWTService jwtService) => _jwtService = jwtService;

        [HttpPost("login")]
        public IActionResult Login(LoginDTO dto)
        {

            var token = _jwtService.GenerateToken(dto.Username, dto.Password);
            if (token == null)
                return Unauthorized();
            return Ok(new { token });
        }
    }
}
