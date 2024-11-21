using UserApi.Models;

namespace UserApi.Services
{
    public interface IIncidentService
    {
        (bool Success, string Message) CreateIncident(Incident incident);
        Incident GetIncidentById(int id);
        (bool Success, string Message) UpdateIncident(int id, Incident incident);
        (bool Success, string Message) DeleteIncident(int id);
        (bool Success, string Message) EscalateIncident(int id);
    }
}
