using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Model;
using System.Security.Cryptography;
using System.Security;
using Project.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Project.Api.Dto;
using Project.Api.Interfaces;

namespace Project.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        private readonly jwtDbcontext _context;
        private readonly IGenerateToken _TokenService;
        public HomeController(jwtDbcontext context, IGenerateToken TokenService)
        {
            _context = context;
            _TokenService = TokenService;
        }
        [HttpGet("GetCategory")]
        public async Task<IActionResult> GetCategoriesAsync()
        {
            var cats = await _context.Categories.ToListAsync();
            return  Ok(cats);
        }
        [Authorize(Roles ="Admin")]
        [HttpGet("userlist")]
        public async Task<List<User>> userlist()
        {
            var users = await _context.Users.ToListAsync();
     
            return users;
        }

       [HttpPost("Register")]

        public async Task<IActionResult> Register(RegisterDto Rdto)
        {
            User userobj = new User
            {
                UserName = Rdto.UserName,
                FullName = Rdto.FullName,
                HashPassword = PasswordHelper.HashPassword(Rdto.Password),
                Role = "user",
                Uid = Guid.NewGuid()
              
            };
             _context.Users.Add(userobj);
             await _context.SaveChangesAsync();
            return Ok(new {message="Registered Successfully"});
        }
        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginDto Ldto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u=>u.UserName == Ldto.UserName);
            if (user == null) 
            { 
                return NotFound();
            }
            if (!PasswordHelper.VerifyPassword(Ldto.Password, user.HashPassword))
            {
                return Unauthorized(new { message = "Invalid Credentials" });
            }
            var token = _TokenService.GetToken(user);
            return Ok(new { token=token,message="Welcome"});

        }

    }
}
