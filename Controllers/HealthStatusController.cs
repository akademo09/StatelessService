using Microsoft.AspNetCore.Mvc;
using Prometheus;
using StatelessService.Constans;
using Microsoft.Extensions.Hosting;

namespace StatelessService.Controllers;

[ApiController]
[Route("[controller]")]
public class HealthStatusController : ControllerBase
{
    private IHostApplicationLifetime _applicationLifetime;
    private bool _ready = true;

    public HealthStatusController(IHostApplicationLifetime applicationLifrtime)
    {
        _applicationLifetime = applicationLifrtime;
    }

    private Gauge _readinessState = Metrics.CreateGauge("hs_ready", "Readiness state");

    [HttpGet("SetReady")]
    public async Task<IActionResult> AddConnection()
    {
        await Task.CompletedTask;
        _readinessState.Inc();
        _ready = true;
        return new JsonResult($"{Constants.AppId}: AddConnection: connection count = {_readinessState.Value}");
    }

    [HttpGet("SetNotReady")]
    public async Task<IActionResult> RemoveConnection()
    {
        await Task.CompletedTask;
        _readinessState.Dec();
        _ready = false;
        return new JsonResult($"{Constants.AppId}: RemoveConnection: connection count = {_readinessState.Value}");
    }

    [HttpGet("GetReadinessState")]
    public async Task<IActionResult> GetReadinessState()
    {
        await Task.CompletedTask;
        _readinessState.Inc();
        return new JsonResult($"{Constants.AppId}: Readiness state = {_readinessState.Value}");
    }

    [HttpGet("Complete")]
    public async Task<IActionResult> Complete()
    {
        await Task.CompletedTask;
        _readinessState.Dec();
        _applicationLifetime.StopApplication();
        return new JsonResult($"{Constants.AppId}: Readiness state = {_readinessState.Value}");
    }

    [HttpGet("Ready")]
    public async Task<IActionResult> Ready()
    {
        await Task.CompletedTask;
        if(_ready)
        {
            return new OkResult();
        }
        else
        {
            return NotFound();
        }
    }
}
