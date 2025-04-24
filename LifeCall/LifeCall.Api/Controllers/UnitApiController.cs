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
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        [Route("")]
        public async Task<ActionResult<IEnumerable<Unit>>> GetUnits()
        {
            try
            {
                var units = await _context.Units.ToListAsync();
                return Ok(units);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching units: {ex.Message}");
            }
        }

        [HttpPost]
        [Route("assignments")]
        public async Task<IActionResult> AssignUnit([FromBody] UnitAssignment assignment)
        {
            if (!IsValidAssignment(assignment))
            {
                return BadRequest("Assignment data is invalid. Ensure all required fields are provided.");
            }

            try
            {
                _context.UnitAssignments.Add(assignment);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAssignments), new { reportId = assignment.EmergencyReportId }, assignment);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Database update error: {ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Unexpected server error: {ex.Message}");
            }
        }

        [HttpGet]
        [Route("{reportId:int}/assignments")]
        public async Task<ActionResult<IEnumerable<UnitAssignmentDto>>> GetAssignments(int reportId)
        {
            try
            {
                var assignments = await _context.UnitAssignments
                    .Where(a => a.EmergencyReportId == reportId)
                    .Include(a => a.Unit)
                    .Select(a => new UnitAssignmentDto
                    {
                        Id = a.Id,
                        EmergencyReportId = a.EmergencyReportId,
                        UnitName = a.Unit.Name
                    })
                    .ToListAsync();

                if (!assignments.Any())
                {
                    return NotFound($"No assignments found for report ID {reportId}.");
                }

                return Ok(assignments);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error fetching assignments: {ex.Message}");
            }
        }

        private bool IsValidAssignment(UnitAssignment assignment)
        {
            return assignment != null && assignment.EmergencyReportId > 0 && assignment.UnitId > 0;
        }
    }

    public class UnitAssignmentDto
    {
        public int Id { get; set; }
        public int EmergencyReportId { get; set; }
        public string UnitName { get; set; }
    }
}
