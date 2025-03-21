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

        public async Task<IActionResult> Index()
        {
           var reports = await _context.EmergencyReports.ToListAsync();
            return View(reports);
        }

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create(EmergencyReport report)
        {
            if (ModelState.IsValid)
            {
                _context.Add(report);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(report);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
