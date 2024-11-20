using UserApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<User> _userManager;

        public UserService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task<User> GetUserByIdAsync(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<bool> CreateUserAsync(User user, string password)
        {
            var result = await _userManager.CreateAsync(user, password);
            return result.Succeeded;
        }

        public async Task<bool> ValidateUserCredentialsAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null) return false;
            
            var result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }

        public async Task<User> Authenticate(string email, string password) // Implement this method
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await ValidateUserCredentialsAsync(email, password))
            {
                return user;
            }
            return null;
        }
    }
}
