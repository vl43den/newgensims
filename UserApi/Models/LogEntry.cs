using System;

namespace UserApi.Models  // Correct namespace for the model
{
    public class LogEntry
    {
        public int Id { get; set; } // Eindeutige ID des Logs
        public DateTime Timestamp { get; set; } // Zeitstempel des Log-Eintrags
        public string LogLevel { get; set; } // Log-Level (z.B. "Info", "Warning", "Error")
        public string Message { get; set; } // Nachricht des Log-Eintrags
        public string User { get; set; } // Optional: Benutzer, der die Aktion ausgelöst hat

        // Beispiel: Konstruktor
        public LogEntry()
        {
            Timestamp = DateTime.UtcNow; // Standard-Zeitstempel
        }
    }
}
