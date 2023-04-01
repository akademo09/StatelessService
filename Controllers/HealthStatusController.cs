using Microsoft.AspNetCore.Mvc;
using Prometheus;
using StatelessService.Constans;

namespace StatelessService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthStatusController : ControllerBase
{
    private Gauge _connectionCount = Metrics.CreateGauge("hs_connections", "number of connections");

    [HttpGet("AddConnection")]
    public async Task<IActionResult> AddConnection()
    {
        await Task.CompletedTask;
        _connectionCount.Inc();
        return new JsonResult($"{Constants.AppId}: AddConnection: connection count = {_connectionCount.Value}");
    }

    [HttpGet("RemoveConnection")]
    public async Task<IActionResult> RemoveConnection()
    {
        await Task.CompletedTask;
        _connectionCount.Dec();
        return new JsonResult($"{Constants.AppId}: RemoveConnection: connection count = {_connectionCount.Value}");
    }
}
