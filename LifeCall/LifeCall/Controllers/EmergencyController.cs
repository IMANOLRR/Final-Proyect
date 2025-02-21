using Microsoft.AspNetCore.Mvc;
using LifeCall.Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace LifeCall.Web.Controllers
{
    public class EmergencyController : Controller
    {
        private readonly EmergencyService _service;

        public EmergencyController(EmergencyService service)
        {
            _service = service;
        }

        public IActionResult Index()
        {
            var reports = _service.GetAllReports();
            return View(reports);
        }
    }
}
