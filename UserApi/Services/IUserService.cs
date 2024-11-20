using System.Threading.Tasks;
using UserApi.Models;

namespace UserApi.Services
{
    public interface IUserService
    {
        Task<User?> GetUserByIdAsync(string id);
        Task<bool> CreateUserAsync(User user, string password);
        Task<bool> ValidateUserCredentialsAsync(string email, string password);
        Task<User?> AuthenticateAsync(string email, string password); 
    }
}
