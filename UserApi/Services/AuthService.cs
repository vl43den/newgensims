using newgensims.Models;
using Microsoft.EntityFrameworkCore;
using UserApi.Models;
using UserApi.Data;

namespace newgensims.Services
{

   public class UserService
{
    private readonly ApplicationDbContext _context;

    public UserService(ApplicationDbContext context)
    {
        _context = context;
    }

    public UserApi.Models.User? Authenticate(string username, string password)
    {
        var user = _context.Users.FirstOrDefault(u => u.Username == username && u.Password == password);
        if (user == null) return null;

        // Optionally: Generate a JWT token
        return user;
    }
}



}