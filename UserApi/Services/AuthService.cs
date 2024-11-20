using Microsoft.AspNetCore.Identity;
using UserApi.Models;

namespace UserApi.Services
{
    public class AuthService : IUserService
    {
        private readonly UserApi.Data.ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AuthService(UserApi.Data.ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
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
