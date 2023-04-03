using Microsoft.AspNetCore.Mvc;
using Prometheus;
using StatelessService.Constans;

namespace StatelessService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthStatusController : ControllerBase
{
    private Gauge _readinessState = Metrics.CreateGauge("hs_ready", "Readiness state");

    [HttpGet("SetReady")]
    public async Task<IActionResult> AddConnection()
    {
        await Task.CompletedTask;
        _readinessState.Inc();
        return new JsonResult($"{Constants.AppId}: AddConnection: connection count = {_readinessState.Value}");
    }

    [HttpGet("SetNotReady")]
    public async Task<IActionResult> RemoveConnection()
    {
        await Task.CompletedTask;
        _readinessState.Dec();
        return new JsonResult($"{Constants.AppId}: RemoveConnection: connection count = {_readinessState.Value}");
    }

    [HttpGet("GetReadinessState")]
    public async Task<IActionResult> GetReadinessState()
    {
        await Task.CompletedTask;
        _readinessState.Inc();
        return new JsonResult($"{Constants.AppId}: Readiness state = {_readinessState.Value}");
    }
}
