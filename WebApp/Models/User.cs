using Microsoft.AspNetCore.Identity;

namespace WebApp.Models
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }
        public bool IsActive { get; set; }

        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
        {
            var verificationResult = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }

    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
