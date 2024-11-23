using newgensims.Data;
using newgensims.Models;

namespace newgensims.Services
{
    public class LogEntryService : ILogEntryService
    {
        private readonly ApplicationDbContext _context;

        public LogEntryService(ApplicationDbContext context)
        {
            _context = context;
        }

        public (bool Success, string Message) CreateLogEntry(LogEntry logEntry)
        {
            _context.LogEntries.Add(logEntry);
            _context.SaveChanges(); // Save to database
            return (true, "Log entry created successfully.");
        }

        // Add other necessary methods like GetLogEntryById, UpdateLogEntry, etc.
    }
}
