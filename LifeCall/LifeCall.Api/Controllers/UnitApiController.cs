using LifeCall.Domain;
using LifeCall.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LifeCall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UnitApiController : ControllerBase
    {
        private readonly LifeCallDbContext _context;

        public UnitApiController(LifeCallDbContext context)
        {
            _context = context;
        }

        [HttpGet("units")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            return await _context.Units.ToListAsync();
        }

        [HttpPost("assignments")]
        public async Task<IActionResult> AssignUnit([FromBody] UnitAssignment assignment)
        {
            if (assignment == null)
            {
                return BadRequest("Assignment data is missing.");
            }

            _context.UnitAssignments.Add(assignment);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(AssignUnit), new { id = assignment.Id }, assignment);
        }

        [HttpGet("assignments/{reportId}")]
        public async Task<ActionResult<IEnumerable<UnitAssignment>>> GetAssignments(int reportId)
        {
            var assignments = await _context.UnitAssignments
                .Where(a => a.EmergencyReportId == reportId)
                .Include(a => a.Unit)
                .ToListAsync();

            if (assignments == null || !assignments.Any())
            {
                return NotFound($"No assignments found for report ID {reportId}.");
            }

            return assignments;

        }
    }
}
