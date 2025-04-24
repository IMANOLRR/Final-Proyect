using LifeCall.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LifeCall.Infrastructure;
using System.Net.Http.Json;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System;

namespace LifeCall.Web.Controllers
{
    public class UnitController : Controller
    {
        [HttpGet]
        public IActionResult AssignUnitView()
        {
            // Simulamos datos de ejemplo para la vista.
            ViewBag.Units = new List<SelectListItem>
    {
        new SelectListItem { Value = "1", Text = "Unidad 1" },
        new SelectListItem { Value = "2", Text = "Unidad 2" },
        new SelectListItem { Value = "3", Text = "Unidad 3" }
    };

            var assignment = new UnitAssignment { EmergencyReportId = 123 }; // ID ficticio.
            return View("AssignUnitView", assignment);
        }


        //    private readonly HttpClient _httpClient;
        //    private readonly string _apiUrl = "https://localhost:44380/api/UnitApiController";

        //    public UnitController(HttpClient httpClient)
        //    {
        //        _httpClient = httpClient;
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> Index()
        //    {
        //        var units = await _httpClient.GetFromJsonAsync<List<Unit>>($"{_apiUrl}/units");
        //        return View(units);
        //    }

        //    [HttpPost]
        //    [ValidateAntiForgeryToken]
        //    public async Task<IActionResult> Assign(UnitAssignment assignment)
        //    {
        //        if (!ModelState.IsValid)
        //        {
        //            return View(assignment);
        //        }

        //        var response = await _httpClient.PostAsJsonAsync($"{_apiUrl}/assignments", assignment);

        //        if (response.IsSuccessStatusCode)
        //        {
        //            return RedirectToAction("Details", "EmergencyReport", new { id = assignment.EmergencyReportId });
        //        }

        //        return View(assignment);
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> Assignments(int reportId)
        //    {
        //        var assignments = await _httpClient.GetFromJsonAsync<List<UnitAssignment>>($"{_apiUrl}/assignments/{reportId}");
        //        return View(assignments);
        //    }

        //    [HttpGet]
        //    public async Task<IActionResult> AssignUnit(int id)
        //    {
        //        var report = await _httpClient.GetFromJsonAsync<EmergencyReport>($"{_apiUrl}/{id}");
        //        if (report == null)
        //        {
        //            return NotFound();
        //        }

        //        var units = await _httpClient.GetFromJsonAsync<List<Unit>>($"{_apiUrl}/units");

        //        if (units == null || !units.Any())
        //        {
        //            ViewBag.Units = new List<SelectListItem>();
        //        }
        //        else
        //        {
        //            ViewBag.Units = units.Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
        //                                 .ToList();
        //        }

        //        var assignment = new UnitAssignment { EmergencyReportId = id };

        //        return View(assignment);
        //    }
    }
}
