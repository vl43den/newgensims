


namespace newgensims.Models
{
    public class LoginRequest
    {
        public required string Username { get; set; }
        public required string Password { get; set; }
    }

    public class User
    {
        public int Id { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; } // Store securely (e.g., hashed)
        public required string Role { get; set; } // Optional: Admin/User roles
    }

    public class LoginResponse
    {
        public required string Username { get; set; }
        public required string Token { get; set; } // Optional: JWT or session token
    }

}