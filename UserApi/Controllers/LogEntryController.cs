using Microsoft.AspNetCore.Mvc;
using UserApi.Models;  // Correct namespace for the LogEntry model
using UserApi.Services;

namespace UserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LogEntryController : ControllerBase
    {
        private readonly ILogEntryService _logEntryService;

        public LogEntryController(ILogEntryService logEntryService)
        {
            _logEntryService = logEntryService;
        }

        [HttpPost]
        public IActionResult CreateLogEntry([FromBody] LogEntry logEntry)
        {
            var result = _logEntryService.CreateLogEntry(logEntry);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        // Add other necessary endpoints like GetLogEntry, UpdateLogEntry, etc.
    }
}
