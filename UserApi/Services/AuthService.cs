using UserApi.Models;
using Microsoft.AspNetCore.Identity;

namespace UserApi.Services
{
    public class AuthService
    {
        private readonly UserApi.Data.ApplicationDbContext _context;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AuthService(UserApi.Data.ApplicationDbContext context, IPasswordHasher<User> passwordHasher)
        {
            _context = context;
            _passwordHasher = passwordHasher;
        }

        public User? GetUserByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.UserName == username);
        }

        public bool VerifyPassword(User user, string password)
        {
            return _passwordHasher.VerifyHashedPassword(user, user.PasswordHash, password) == PasswordVerificationResult.Success;
        }

        public UserRole ConvertRoleFromString(string roleString)
        {
            return Enum.TryParse<UserRole>(roleString, true, out var role) ? role : UserRole.User;  // Default to User if parsing fails
        }
    }
}
