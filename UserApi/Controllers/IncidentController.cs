using Microsoft.AspNetCore.Mvc;
using newgensims.Services;
using newgensims.Models; // Assuming the Incident model is in this namespace

//Version 0.1

namespace newgensims.Controllers
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
        public IActionResult CreateIncident(Incident incident)
        {
            var result = _incidentService.CreateIncident(incident);
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
        public IActionResult UpdateIncident(int id, Incident incident)
        {
            var result = _incidentService.UpdateIncident(id, incident);
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
