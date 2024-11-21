using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using UserApi.Models;

namespace UserApi.Services
{
    public class AuthService : IUserService
    {
        private readonly UserApi.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly ILogger<AuthService> _logger;

        public AuthService(UserApi.Data.ApplicationDbContext context, UserManager<User> userManager, ILogger<AuthService> logger)
        {
            _context = context;
            _userManager = userManager;
            _logger = logger;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                _logger.LogWarning("User not found: {Email}", email);
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                _logger.LogWarning("Invalid password for user: {Email}", email);
            }

            return passwordValid ? user : null;
        }

        public Task<User> GetUserByIdAsync(string id)
        {
            return _context.Users.FindAsync(id).AsTask();
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            return user != null && await _userManager.CheckPasswordAsync(user, password);
        }
    }
}
