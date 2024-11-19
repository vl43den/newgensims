// Models/RegisterModel.cs
namespace UserApi.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public string Name { get; set; }  // Include Name if it's required
    }
}
