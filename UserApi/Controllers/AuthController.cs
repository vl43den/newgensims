using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;

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
        public IActionResult Login([FromBody] LoginRequest model)
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
            {
                return Unauthorized(new { message = "Invalid credentials" });
            }

            return Ok(user);
        }
    }

    public interface IUserService
    {
        object Authenticate(string username, string password);
    }
}
