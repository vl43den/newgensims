using System;
using System.ComponentModel.DataAnnotations;

namespace newgensims.Models
{
    public class Incident
    {
        public int Id { get; set; } // Eindeutige ID des Vorfalls

        [Required]
        [MaxLength(100)] // Ensuring that the title does not exceed 100 characters
        public string Title { get; set; } // Titel des Vorfalls

        [Required]
        [MaxLength(500)] // Adding a max length for the description field
        public string Description { get; set; } // Beschreibung des Vorfalls

        [Required]
        [MaxLength(50)] // Limiting severity length (e.g. "Low", "Medium", "High")
        public string Severity { get; set; } // Schweregrad (z.B. "Low", "Medium", "High")

        [Required]
        [MaxLength(50)] // Limiting the status field length
        public string Status { get; set; } // Status (z.B. "Open", "In Progress")

        [Required]
        [MaxLength(100)] // Limiting length of assigned user
        public string AssignedUser { get; set; } // Name des Bearbeiters

        public DateTime CreatedAt { get; set; } // Zeitstempel der Erstellung

        public DateTime? UpdatedAt { get; set; } // Letzte Aktualisierung (optional)

        // Konstruktor
        public Incident()
        {
            CreatedAt = DateTime.UtcNow; // Standard-Zeitstempel
            UpdatedAt = null; // Initializing as null
        }

        // You may want to implement a method to update the Incident, e.g.:
        public void Update(string title, string description, string severity, string status, string assignedUser)
        {
            Title = title;
            Description = description;
            Severity = severity;
            Status = status;
            AssignedUser = assignedUser;
            UpdatedAt = DateTime.UtcNow; // Updating the timestamp when the incident is modified
        }
    }
}
