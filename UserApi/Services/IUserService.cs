using UserApi.Models;
using System.Threading.Tasks;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(string id);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<User> Authenticate(string email, string password); // Add this method
    }
}
