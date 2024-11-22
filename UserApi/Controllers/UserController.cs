using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UserApi.Models;
using UserApi.Data;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        // Constructor to inject ApplicationDbContext and IPasswordHasher
        public UserController(ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password) || string.IsNullOrEmpty(model.Email))
            {
                return BadRequest(new { message = "Username, password, and email are required." });
            }

            // Check if the user already exists
            var existingUser = _context.Users.FirstOrDefault(u => u.UserName == model.Username || u.Email == model.Email);
            if (existingUser != null)
            {
                return Conflict(new { message = "Username or email already exists." });
            }

            var user = new User
            {
                UserName = model.Username, // Use UserName instead of Username
                Name = model.Name,
                Email = model.Email,
                IsActive = true,
                Role = UserRole.User // Default role
            };

            // Hash and set the user's password
            user.SetPassword(model.Password, _passwordHasher);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new
            {
                message = "User registered successfully!",
                user = new
                {
                    user.UserName,
                    user.Name,
                    user.Email,
                    user.Role
                }
            });
        }
    }
}
