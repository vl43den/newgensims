using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserApi.Models;  // Ensure you are importing the correct namespace for your models

namespace UserApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly UserDbContext _context;

        // Inject UserDbContext into the controller
        public UserController(UserDbContext context)
        {
            _context = context;
        }

        // GET: api/users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            // Get all users from the database
            var users = await _context.Users.ToListAsync();  // Ensure we get all users from the database
            return Ok(users);
        }

        // GET: api/users/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserById(int id)
        {
            // Try to find the user by ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound(); // Return 404 if not found
            }
            return Ok(user);
        }

        // POST: api/users
        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(); // Return a 400 Bad Request if the user object is null
            }

            // Add the new user to the database
            _context.Users.Add(user);
            await _context.SaveChangesAsync();  // Save the changes to the database

            // Return the newly created user, including the ID, and a 201 status
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        // DELETE: api/users/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            // Try to find the user by ID
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();  // Return 404 if the user is not found
            }

            // Remove the user from the database
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            // Return 204 No Content to indicate success
            return NoContent();
        }
    }
}
