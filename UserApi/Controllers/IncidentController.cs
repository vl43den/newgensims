using Microsoft.AspNetCore.Mvc;
using YourNamespace.DTOs;
using YourNamespace.Services;

//Version 0.1

namespace YourNamespace.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentService _incidentService;

        public IncidentController(IIncidentService incidentService)
        {
            _incidentService = incidentService;
        }

        // POST: api/Incident
        [HttpPost]
        public IActionResult CreateIncident(IncidentDto incidentDto)
        {
            var result = _incidentService.CreateIncident(incidentDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // GET: api/Incident/{id}
        [HttpGet("{id}")]
        public IActionResult GetIncident(int id)
        {
            var incident = _incidentService.GetIncidentById(id);
            if (incident == null)
                return NotFound();

            return Ok(incident);
        }

        // PUT: api/Incident/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateIncident(int id, IncidentDto incidentDto)
        {
            var result = _incidentService.UpdateIncident(id, incidentDto);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // DELETE: api/Incident/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteIncident(int id)
        {
            var result = _incidentService.DeleteIncident(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }

        // POST: api/Incident/escalate/{id}
        [HttpPost("escalate/{id}")]
        public IActionResult EscalateIncident(int id)
        {
            var result = _incidentService.EscalateIncident(id);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok(result);
        }
    }
}
