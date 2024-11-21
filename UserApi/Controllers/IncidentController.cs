using Microsoft.AspNetCore.Mvc;
using UserApi.Models;
using UserApi.Services;

namespace newgensims.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        [HttpPost]
        public IActionResult CreateIncident([FromBody] Incident incident)
        {
            var result = _incidentService.CreateIncident(incident);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpGet("{id}")]
        public IActionResult GetIncident(int id)
        {
            var incident = _incidentService.GetIncidentById(id);
            if (incident == null)
            {
                return NotFound("Incident not found.");
            }
            return Ok(incident);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateIncident(int id, [FromBody] Incident incident)
        {
            var result = _incidentService.UpdateIncident(id, incident);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteIncident(int id)
        {
            var result = _incidentService.DeleteIncident(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        [HttpPatch("{id}/escalate")]
        public IActionResult EscalateIncident(int id)
        {
            var result = _incidentService.EscalateIncident(id);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
