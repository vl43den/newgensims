using System;

namespace newgensims.Models
{
    public class Incident
    {
        public int Id { get; set; } // Eindeutige ID des Vorfalls
        public string Title { get; set; } // Titel des Vorfalls
        public string Description { get; set; } // Beschreibung des Vorfalls
        public string Severity { get; set; } // Schweregrad (z.B. "Low", "Medium", "High")
        public string Status { get; set; } // Status (z.B. "Open", "In Progress", "Closed")
        public string AssignedUser { get; set; } // Name des Bearbeiters
        public DateTime CreatedAt { get; set; } // Zeitstempel der Erstellung
        public DateTime? UpdatedAt { get; set; } // Letzte Aktualisierung (optional)

        // Beispiel: Konstruktor
        public Incident()
        {
            CreatedAt = DateTime.UtcNow; // Standard-Zeitstempel
        }
    }
}
