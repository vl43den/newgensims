// Services/RedisSessionService.cs
using StackExchange.Redis;
using System.Text.Json;
using UserApi.Models;
using UserApi.Services;

public class RedisSessionService : ISessionService
{
    private readonly IConnectionMultiplexer _redis;
    private readonly ILogger<RedisSessionService> _logger;
    private const int SESSION_TIMEOUT = 3600; // 1 hour in seconds

    public RedisSessionService(IConnectionMultiplexer redis, ILogger<RedisSessionService> logger)
    {
        _redis = redis;
        _logger = logger;
    }

    public async Task<string> CreateSessionAsync(User user)
    {
        try
        {
            var sessionId = Guid.NewGuid().ToString();
            var db = _redis.GetDatabase();
            
            var userSession = JsonSerializer.Serialize(new {
                UserId = user.Id,
                user.UserName,
                user.Email,
                user.Role,
                CreatedAt = DateTime.UtcNow
            });

            await db.StringSetAsync(
                $"session:{sessionId}",
                userSession,
                TimeSpan.FromSeconds(SESSION_TIMEOUT)
            );

            return sessionId;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error creating session for user {UserId}", user.Id);
            throw;
        }
    }

    public async Task<User?> ValidateSessionAsync(string sessionId)
    {
        try
        {
            var db = _redis.GetDatabase();
            var session = await db.StringGetAsync($"session:{sessionId}");
            
            if (!session.HasValue)
                return null;

            // Extend session timeout
            await db.KeyExpireAsync($"session:{sessionId}", TimeSpan.FromSeconds(SESSION_TIMEOUT));
            
            return JsonSerializer.Deserialize<User>(session!);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error validating session {SessionId}", sessionId);
            return null;
        }
    }

    public async Task RemoveSessionAsync(string sessionId)
    {
        try
        {
            var db = _redis.GetDatabase();
            await db.KeyDeleteAsync($"session:{sessionId}");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error removing session {SessionId}", sessionId);
            throw;
        }
    }
}
