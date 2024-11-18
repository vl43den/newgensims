using Microsoft.AspNetCore.Identity;

namespace UserApi.Models
{
    public class User : IdentityUser
    {
        // Remove the custom Password property, as IdentityUser already has PasswordHash
        // public string Password { get; set; } 

        public string Name { get; set; }
        public string? Email { get; set; }
        public UserRole Role { get; set; }  // UserRole is an enum

        public bool IsActive { get; set; }

        // The SetPassword method will use the existing PasswordHash from IdentityUser
        public void SetPassword(string password, IPasswordHasher<User> passwordHasher)
        {
            // IdentityUser already has PasswordHash, so we use that to hash the password
            PasswordHash = passwordHasher.HashPassword(this, password);
        }

        // The ValidatePassword method uses the existing PasswordHash from IdentityUser
        public bool ValidatePassword(string password, IPasswordHasher<User> passwordHasher)
        {
            var verificationResult = passwordHasher.VerifyHashedPassword(this, PasswordHash, password);
            return verificationResult == PasswordVerificationResult.Success;
        }
    }

    // Enum to define roles
    public enum UserRole
    {
        Admin = 1,
        User = 2
    }
}
