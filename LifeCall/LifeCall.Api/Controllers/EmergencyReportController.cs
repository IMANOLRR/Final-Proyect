using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeCall.Domain;
using LifeCall.Infrastructure;

namespace LifeCall.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmergencyReportController : ControllerBase
    {
        private readonly LifeCallDbContext _context;

        public EmergencyReportController(LifeCallDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _context.EmergencyReports.ToListAsync();
            return Ok(reports);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null) return NotFound();
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Create(EmergencyReport report)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            _context.EmergencyReports.Add(report);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmergencyReport report)
        {
            if (id != report.Id) return BadRequest();

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EmergencyReports.Any(e => e.Id == id))
                    return NotFound();

                throw;
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null) return NotFound();

            _context.EmergencyReports.Remove(report);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
