using LifeCall.Application.Services;
using LifeCall.Domain;
using LifeCall.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

[Route("api/emergency")]
[ApiController]
public class EmergencyApiController : ControllerBase
{
    private readonly EmergencyService _service;

    public EmergencyApiController(EmergencyService service)
    {
        _service = service;
    }

    [HttpGet]
    public IEnumerable<EmergencyReport> Get()
    {
        return _service.GetAllReports();
    }

    [HttpPost]
    public IActionResult Post([FromBody] EmergencyReport report)
    {
        _service.CreateReport(report);
        return Ok();
    }
}
