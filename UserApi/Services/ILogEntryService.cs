using UserApi.Models;  // Correct namespace for the LogEntry model
using UserApi.Data;    // Correct namespace for your DbContext

namespace UserApi.Services
{
    public interface ILogEntryService
    {
        (bool Success, string Message) CreateLogEntry(LogEntry logEntry);
        LogEntry? GetLogEntryById(int id);
        (bool Success, string Message) UpdateLogEntry(int id, LogEntry logEntry);
        (bool Success, string Message) DeleteLogEntry(int id);
    }
}
