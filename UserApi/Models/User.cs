using Microsoft.AspNetCore.Identity;

namespace UserApi.Models
{
    public class User : IdentityUser
    {
        public new string Email { get; set; } = string.Empty; // Using 'new' keyword to hide inherited member
        public string Name { get; set; } = string.Empty;
        public UserRole Role { get; set; } = UserRole.User;
        public bool IsActive { get; set; } = true;

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
