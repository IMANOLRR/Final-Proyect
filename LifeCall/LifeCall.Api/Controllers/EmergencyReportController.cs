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
        public async Task<IActionResult> Create([FromBody] EmergencyReport report)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            try
            {
                _context.EmergencyReports.Add(report);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error en el servidor: {ex.Message}");
            }
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, EmergencyReport report)
        {
            if (id != report.Id)
            {
                return BadRequest(new { message = "El ID proporcionado no coincide con el ID del reporte." });
            }

            var existingReport = await _context.EmergencyReports.FindAsync(id);
            if (existingReport == null)
            {
                return NotFound(new { message = "El reporte no fue encontrado." });
            }

            existingReport.CallerName = report.CallerName;
            existingReport.Description = report.Description;
            existingReport.DateReport = report.DateReport;
            existingReport.Status = report.Status;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Hubo un conflicto al intentar guardar los cambios. Inténtalo nuevamente." });
            }

            return Ok(new { message = "El reporte fue actualizado exitosamente." });
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null)
            {
                return NotFound(new { message = "El reporte no existe o ya fue eliminado." });
            }

            _context.EmergencyReports.Remove(report);
            await _context.SaveChangesAsync();

            return Ok(new { message = "El reporte fue eliminado exitosamente." });
        }



    }
}
