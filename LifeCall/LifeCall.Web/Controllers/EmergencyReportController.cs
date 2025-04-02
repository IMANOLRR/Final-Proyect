using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LifeCall.Domain;
using LifeCall.Infrastructure;

namespace LifeCall.Web.Controllers
{
    public class EmergencyReportController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiUrl = "http://localhost:5043/api/EmergencyReportApi";

        public EmergencyReportController(HttpClient httpClient)
        {
            _httpClient = httpClient;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmergencyReport report)
        {
            if (id != report.Id || !ModelState.IsValid)
            {
                return View(report);
            }

            var response = await _httpClient.PutAsJsonAsync($"{_apiUrl}/{id}", report);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Reports));
            }

            return View(report);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _httpClient.DeleteAsync($"{_apiUrl}/{id}");

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Reports));
            }

            return View("Error");
        }
    }
}
