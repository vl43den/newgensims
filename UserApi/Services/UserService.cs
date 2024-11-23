using UserApi.Models;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User?> AuthenticateAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return null;

            var passwordValid = await _userManager.CheckPasswordAsync(user, password);
            return passwordValid ? user : null;
        }

        public Task<User?> GetUserByIdAsync(string id)
        {
            return _userManager.FindByIdAsync(id);
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
