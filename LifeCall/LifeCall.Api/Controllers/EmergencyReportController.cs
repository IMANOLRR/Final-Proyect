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

        // Obtener todos los reportes
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var reports = await _context.EmergencyReports.ToListAsync();
                return Ok(reports);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al obtener los reportes.", error = ex.Message });
            }
        }

        // Obtener reporte por ID
        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var report = await _context.EmergencyReports.FindAsync(id);
                if (report == null)
                {
                    return NotFound(new { message = $"Reporte con ID {id} no encontrado." });
                }
                return Ok(report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al buscar el reporte.", error = ex.Message });
            }
        }

        // Crear un nuevo reporte
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EmergencyReport report)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Datos inválidos.", errors = ModelState.Values });
            }

            try
            {
                await _context.EmergencyReports.AddAsync(report);
                await _context.SaveChangesAsync();
                return CreatedAtAction(nameof(GetById), new { id = report.Id }, report);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al crear el reporte.", error = ex.Message });
            }
        }

        // Actualizar un reporte existente
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, [FromBody] EmergencyReport report)
        {
            if (id != report.Id)
            {
                return BadRequest(new { message = "El ID proporcionado no coincide con el ID del reporte." });
            }

            try
            {
                var existingReport = await _context.EmergencyReports.FindAsync(id);
                if (existingReport == null)
                {
                    return NotFound(new { message = $"Reporte con ID {id} no encontrado." });
                }

                // Actualizar campos
                existingReport.CallerName = report.CallerName;
                existingReport.Description = report.Description;
                existingReport.DateReport = report.DateReport;
                existingReport.Status = report.Status;

                await _context.SaveChangesAsync();
                return Ok(new { message = "Reporte actualizado exitosamente.", report = existingReport });
            }
            catch (DbUpdateConcurrencyException)
            {
                return Conflict(new { message = "Conflicto al guardar los cambios. Intenta nuevamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al actualizar el reporte.", error = ex.Message });
            }
        }

        // Eliminar un reporte
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var report = await _context.EmergencyReports.FindAsync(id);
                if (report == null)
                {
                    return NotFound(new { message = $"Reporte con ID {id} no encontrado o ya fue eliminado." });
                }

                _context.EmergencyReports.Remove(report);
                await _context.SaveChangesAsync();

                return Ok(new { message = "Reporte eliminado exitosamente." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Error al eliminar el reporte.", error = ex.Message });
            }
        }
    }
}
