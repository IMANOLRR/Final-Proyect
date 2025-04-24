using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeCall.Domain;
using LifeCall.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LifeCall.Web.Controllers
{
    public class EmergencyReportController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "https://localhost:44380/api/EmergencyReport";
        private readonly LifeCallDbContext _context;


        public EmergencyReportController(HttpClient httpClient, LifeCallDbContext context)
        {
            _httpClient = httpClient;
            _context = context;
        }


        public async Task<IActionResult> Reports()
        {
            var response = await _httpClient.GetAsync(_apiUrl);
            if (!response.IsSuccessStatusCode)
            {
                return View(new List<EmergencyReport>());
            }
            var reports = await response.Content.ReadFromJsonAsync<List<EmergencyReport>>();
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
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            var response = await _httpClient.PostAsJsonAsync(_apiUrl, report);

            if (response.IsSuccessStatusCode)
            {
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
        public async Task<IActionResult> Edit(EmergencyReport report)
        {
            if (!ModelState.IsValid)
            {
                return View(report);
            }

            _context.Entry(report).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.EmergencyReports.Any(e => e.Id == report.Id))
                    return NotFound();

                throw;
            }

            return RedirectToAction(nameof(Reports));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                TempData["SuccessMessage"] = "El reporte fue eliminado exitosamente.";
                return RedirectToAction(nameof(Reports));
            }

            TempData["ErrorMessage"] = "Ocurrió un error al intentar eliminar el reporte.";
            return RedirectToAction(nameof(Reports));
        }

    }
}
