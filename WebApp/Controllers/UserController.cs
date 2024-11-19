using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using WebApp.Models; // Passe diesen Namespace ggf. an dein Projekt an
using WebApp.Data;  // Passe diesen Namespace an

namespace WebApp.Controllers
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

        /// <summary>
        /// Registers a new user.
        /// </summary>
        /// <param name="model">The registration request model.</param>
        /// <returns>Success message or error details.</returns>
        [HttpPost("register")]
        public IActionResult Register([FromBody] RegisterRequest model)
        {
            if (model == null || string.IsNullOrWhiteSpace(model.Username) || string.IsNullOrWhiteSpace(model.Password))
            {
                return BadRequest(new { message = "Invalid user data." });
            }

            var user = new User
            {
                UserName = model.Username,
                Name = model.Name,
                Email = model.Email,
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
