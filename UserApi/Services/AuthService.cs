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

        // Consolidated authentication logic
        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            try
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
                    return null;
                }

                return user;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred during authentication for email: {Email}", email);
                return null;
            }
        }

        // Get user by Id with null check
        public async Task<User?> GetUserByIdAsync(string id)
        {
            try
            {
                return await _context.Users.FindAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while fetching user by ID: {Id}", id);
                return null;
            }
        }

        // Create user with better logging for errors
        public async Task<bool> CreateUserAsync(User user, string password)
        {
            try
            {
                var result = await _userManager.CreateAsync(user, password);
                if (!result.Succeeded)
                {
                    _logger.LogWarning("Failed to create user: {UserName}. Errors: {Errors}",
                        user.UserName, string.Join(", ", result.Errors.Select(e => e.Description)));
                    return false;
                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while creating user: {UserName}", user.UserName);
                return false;
            }
        }

        // Validating user credentials (for login or other purposes)
        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return false;
                }
                return await _userManager.CheckPasswordAsync(user, password);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while validating credentials for email: {Email}", email);
                return false;
            }
        }
    }
}
