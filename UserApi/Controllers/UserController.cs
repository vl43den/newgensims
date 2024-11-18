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
            var user = new User
            {
                UserName = model.Username, // Use UserName instead of Username
                Name = model.Name,
                Email = model.Email, // Assuming Email is properly initialized
                IsActive = true,
                Role = UserRole.User // Default role
            };

            // Hash and set the user's password
            user.SetPassword(model.Password, _passwordHasher);

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(new { message = "User registered successfully!" });
        }
    }
}
