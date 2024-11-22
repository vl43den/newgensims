
using Microsoft.EntityFrameworkCore;
using System.Linq;
using UserApi.Data;
using UserApi.Models;

namespace UserApi.Services
{
    public class IncidentService : IIncidentService
    {
        private readonly IncidentDbContext _context;

        public IncidentService(IncidentDbContext context)
        {
            _context = context;
        }

        public (bool Success, string Message) CreateIncident(Incident incident)
        {
            _context.Incidents.Add(incident);
            _context.SaveChanges(); // Save to database
            return (true, "Incident created successfully.");
        }

        public Incident? GetIncidentById(int id)
        {
            return _context.Incidents.FirstOrDefault(i => i.Id == id); // Fetch incident by ID
        }

        public (bool Success, string Message) UpdateIncident(int id, Incident incident)
        {
            var existingIncident = _context.Incidents.Find(id);
            if (existingIncident == null)
            {
                return (false, "Incident not found.");
            }

            existingIncident.Update(incident.Title, incident.Description, incident.Severity, incident.Status, incident.AssignedUser);
            _context.SaveChanges(); // Save updates to the database
            return (true, "Incident updated successfully.");
        }

        public (bool Success, string Message) DeleteIncident(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident == null)
            {
                return (false, "Incident not found.");
            }

            _context.Incidents.Remove(incident);
            _context.SaveChanges(); // Delete the incident
            return (true, "Incident deleted successfully.");
        }

        public (bool Success, string Message) EscalateIncident(int id)
        {
            var incident = _context.Incidents.Find(id);
            if (incident == null)
            {
                return (false, "Incident not found.");
            }

            incident.Status = "Escalated"; // Update the status to "Escalated"
            _context.SaveChanges(); // Save the escalation
            return (true, "Incident escalated successfully.");
        }
    }
}
