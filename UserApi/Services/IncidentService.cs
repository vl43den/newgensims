using newgensims.Models;

namespace newgensims.Services
{
    public class IncidentService : IIncidentService
    {
        public (bool Success, string Message) CreateIncident(Incident incident)
        {
            // Simulate creation logic
            return (true, "Incident created successfully.");
        }

        public Incident GetIncidentById(int id)
        {
            // Simulate fetching an incident
            return new Incident { Id = id, Title = "Sample Incident", Description = "Sample Description", Status = "New" };
        }

        public (bool Success, string Message) UpdateIncident(int id, Incident incident)
        {
            // Simulate update logic
            return (true, "Incident updated successfully.");
        }

        public (bool Success, string Message) DeleteIncident(int id)
        {
            // Simulate deletion logic
            return (true, "Incident deleted successfully.");
        }

        public (bool Success, string Message) EscalateIncident(int id)
        {
            // Simulate escalation logic
            return (true, "Incident escalated successfully.");
        }
    }
}
