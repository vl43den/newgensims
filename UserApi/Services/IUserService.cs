using UserApi.Models;
using UserApi.Services; // Add this line if missing


namespace UserApi.Services
{
    public interface IUserService
    {
        User? Authenticate(string username, string password);
    }
}
