using Microsoft.AspNetCore.Mvc;
using newgensims.Models;
using newgensims.Services;


namespace newgensims.AuthController
{

    [ApiController]
    [Route("api/[controller]")]

    public class AuthController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;  //Replace IUserService with the actual service handling user logic

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest model)  //and LoginRequest should represent the payload (e.g., { Username, Password }).
        {
            var user = _userService.Authenticate(model.Username, model.Password);
            if (user == null)
                return Unauthorized(new { message = "Invalid credentials" });

            return Ok(user);
        }
    }

    public interface IUserService
    {
        object Authenticate(string username, string password);
    }
}
