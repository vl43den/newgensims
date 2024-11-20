using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;
using System.Threading.Tasks;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            if (string.IsNullOrEmpty(model.Username) || string.IsNullOrEmpty(model.Password))
            {
                return BadRequest(new { message = "Username and password are required." });
            }

            var user = await _userService.AuthenticateAsync(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials." });
            }

            return Ok(new
            {
                message = "Login successful",
                user = new
                {
                    user.Id,
                    user.UserName,
                    user.Email,
                    user.Name,
                    user.Role,
                    user.IsActive
                }
            });
        }
    }
}
