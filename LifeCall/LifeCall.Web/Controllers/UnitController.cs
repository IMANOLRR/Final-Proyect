using LifeCall.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LifeCall.Infrastructure;

namespace LifeCall.Web.Controllers
{
    public class UnitController : Controller
    {
        private readonly LifeCallDbContext _context;

        public UnitController(LifeCallDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Assign(UnitAssignment assignment)
        {
            if (ModelState.IsValid)
            {
                // Agregar la asignación a la base de datos
                _context.UnitAssignments.Add(assignment);
                await _context.SaveChangesAsync();

                // Redirigir a la vista de reportes con un mensaje de éxito
                TempData["SuccessMessage"] = "Unidad asignada exitosamente.";
                return RedirectToAction("Reports", "EmergencyReport");
            }

            // Si el modelo no es válido, vuelve a mostrar la vista con los datos ingresados
            ViewBag.Units = await _context.Units.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToListAsync();
            return View("AssignUnitView", assignment);
        }

        [HttpGet]
        public IActionResult AssignUnitView(int id)
        {
            // Cargar las unidades disponibles desde la base de datos
            ViewBag.Units = _context.Units.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            // Crear un nuevo objeto UnitAssignment con el ID del reporte de emergencia
            var assignment = new UnitAssignment { EmergencyReportId = id };
            return View("AssignUnitView", assignment);
        }
    }
}
