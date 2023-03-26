using Microsoft.AspNetCore.Mvc;
using Prometheus;

namespace StatelessService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IndexController : ControllerBase
    {
        //#private static Meter _meter = new Meter("hs_ready", "1.0.0");
        private static readonly Counter _httpRequests = Metrics.CreateCounter("http_requests", "number of http requests");

        public IndexController()
        {
        }   

        [HttpGet()]
        public async Task<IActionResult> Get()
        {
            await Task.CompletedTask;
            _httpRequests.Inc(1);
            return new JsonResult($"Serving home page at {DateTime.UtcNow}");
        }
    }
}