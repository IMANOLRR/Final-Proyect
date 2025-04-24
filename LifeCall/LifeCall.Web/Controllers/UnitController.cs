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
                _context.UnitAssignments.Add(assignment);
                await _context.SaveChangesAsync();

                TempData["SuccessMessage"] = "Unidad asignada exitosamente.";
                return RedirectToAction("Reports", "EmergencyReport");
            }

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
            ViewBag.Units = _context.Units.Select(u => new SelectListItem
            {
                Value = u.Id.ToString(),
                Text = u.Name
            }).ToList();

            var assignment = new UnitAssignment { EmergencyReportId = id };
            return View("AssignUnitView", assignment);
        }
    }
}
