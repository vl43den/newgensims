// Services/ISessionService.cs
using UserApi.Models;

public interface ISessionService
{
    Task<string> CreateSessionAsync(User user);
    Task<User?> ValidateSessionAsync(string sessionId);
    Task RemoveSessionAsync(string sessionId);
}
