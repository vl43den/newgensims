using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using UserApi.Models;
using UserApi.Services;

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISessionService _sessionService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(
            IUserService userService,
            ISessionService sessionService,
            ILogger<AuthController> logger)
        {
            _userService = userService;
            _sessionService = sessionService;
            _logger = logger;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest model)
        {
            try
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

                // Create session in Redis
                var sessionId = await _sessionService.CreateSessionAsync(user);

                // Set session cookie
                Response.Cookies.Append("SessionId", sessionId, new CookieOptions
                {
                    HttpOnly = true,
                    Secure = true,
                    SameSite = SameSiteMode.Strict,
                    MaxAge = TimeSpan.FromHours(1) // Session expires in 1 hour
                });

                return Ok(new LoginResponse
                {
                    UserId = user.Id,
                    Username = user.UserName,
                    Role = user.Role.ToString(), // Convert UserRole to string
                    Token = sessionId // Optional
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for user {Username}", model.Username);
                return StatusCode(500, new { message = "An error occurred during login." });
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                var sessionId = Request.Cookies["SessionId"];
                if (!string.IsNullOrEmpty(sessionId))
                {
                    // Remove session from Redis
                    await _sessionService.RemoveSessionAsync(sessionId);

                    // Delete session cookie
                    Response.Cookies.Delete("SessionId");
                }
                return Ok(new { message = "Logged out successfully" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return StatusCode(500, new { message = "An error occurred during logout." });
            }
        }
    }
}
