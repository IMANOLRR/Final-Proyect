using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeCall.Domain;
using LifeCall.Infrastructure;

namespace LifeCall.Web.Controllers
{
    public class EmergencyReportController : Controller
    {
        private readonly LifeCallDbContext _context;

        public EmergencyReportController(LifeCallDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IActionResult> Reports()
        {
            var reports = await _context.EmergencyReports.ToListAsync();
            return View(reports);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmergencyReport report)
        {
            if (ModelState.IsValid)
            {
                _context.EmergencyReports.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Reports));
            }
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmergencyReport report)
        {
            if (id != report.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(report);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Reports));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.EmergencyReports.Any(e => e.Id == report.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            return View(report);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            return View(report);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var report = await _context.EmergencyReports.FindAsync(id);
            if (report == null)
            {
                return NotFound();
            }

            _context.EmergencyReports.Remove(report);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Reports));
        }

    }
}
