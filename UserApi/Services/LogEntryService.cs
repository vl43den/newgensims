using System.Linq;
using UserApi.Models;  // Correct namespace for the LogEntry model
using UserApi.Data;    // Correct namespace for your DbContext

namespace UserApi.Services
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

        public LogEntry? GetLogEntryById(int id)
        {
            return _context.LogEntries.FirstOrDefault(le => le.Id == id); // Fetch log entry by ID
        }

        public (bool Success, string Message) UpdateLogEntry(int id, LogEntry logEntry)
        {
            var existingLogEntry = _context.LogEntries.Find(id);
            if (existingLogEntry == null)
            {
                return (false, "Log entry not found.");
            }

            existingLogEntry.Timestamp = logEntry.Timestamp;
            existingLogEntry.LogLevel = logEntry.LogLevel;
            existingLogEntry.Message = logEntry.Message;
            existingLogEntry.User = logEntry.User;
            _context.SaveChanges(); // Save updates to the database
            return (true, "Log entry updated successfully.");
        }

        public (bool Success, string Message) DeleteLogEntry(int id)
        {
            var logEntry = _context.LogEntries.Find(id);
            if (logEntry == null)
            {
                return (false, "Log entry not found.");
            }

            _context.LogEntries.Remove(logEntry);
            _context.SaveChanges(); // Delete the log entry
            return (true, "Log entry deleted successfully.");
        }
    }
}
