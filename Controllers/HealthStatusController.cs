using Microsoft.AspNetCore.Mvc;

namespace StatelessService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthStatusController : ControllerBase
{


    private readonly ILogger<WeatherForecastController> _logger;

    public HealthStatusController(ILogger<WeatherForecastController> logger)
    {
        _logger = logger;
    }

    [HttpGet(Name = "GetRediness")]
    public async Task<IActionResult> Get()
    {
        await Task.CompletedTask;
        return new JsonResult("GetReadiness");
    }
}
