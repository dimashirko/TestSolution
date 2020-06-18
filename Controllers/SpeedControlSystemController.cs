using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace TestSolution.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SpeedControlSystemController : ControllerBase
    {
        private readonly ILogger<SpeedControlSystemController> _logger;

        public SpeedControlSystemController(ILogger<SpeedControlSystemController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {

        }
    }
}
