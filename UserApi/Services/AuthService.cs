using Microsoft.AspNetCore.Identity;  // For UserManager<T> and related classes
using UserApi.Models;   // Replace with the actual namespace where your 'User' model is defined
using UserApi.Services; // Replace with the actual namespace where 'IUserService' is defined
using UserApi.Data; // Ensure the correct namespace is used



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

    // Authenticate user by username and password
    public async Task<User?> AuthenticateAsync(string username, string password)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username); // Search by Username
            if (user == null)
            {
                _logger.LogWarning("User not found: {Username}", username);
                return null;
            }

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            if (!passwordValid)
            {
                _logger.LogWarning("Invalid password for user: {Username}", username);
                return null;
            }

            return user;  // Return the user directly (no token generation)
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred during authentication for username: {Username}", username);
            return null;
        }
    }

    // Get user by ID
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

    // Create user
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

    // Validate user credentials
    public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
    {
        try
        {
            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return false;
            }
            return await _userManager.CheckPasswordAsync(user, password);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error occurred while validating credentials for username: {Username}", username);
            return false;
        }
    }
}
