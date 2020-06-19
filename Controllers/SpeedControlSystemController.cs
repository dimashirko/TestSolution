using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace SpeedControlSystemService.Controllers
{
    /// <summary>
    /// Base spedd control system controller
    /// </summary>
    [ApiController]
    [Route("speedcontrolsystem")]
    public class SpeedControlSystemController : ControllerBase
    {
        private readonly ILogger<SpeedControlSystemController> _logger;
        private readonly AppConfig _appConfig;
        private readonly SpeedControlSystemCore _systemCore;

        public SpeedControlSystemController(ILogger<SpeedControlSystemController> logger, IOptionsMonitor<AppConfig> configuration)
        {
            _logger = logger;
            _appConfig = configuration.CurrentValue;
            _systemCore = new SpeedControlSystemCore(_appConfig.DataBasePath);
        }

        /// <summary>
        /// Get max and min fixed speed for selected date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Max and min fixed speed for selected date</returns>
        [HttpGet]
        [Route("speedextremums/{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}")]
        [Route("speedextremums/{date:datetime:regex(\\d{{2}}-\\d{{2}}-\\d{{4}})}")]
        public string GetSpeedExtremums(DateTime date)
        {
            if (CheckTime())
            {
                return _systemCore.GetSpeedExtremums(date);
            }
            else
                throw GetTimeError();
        }

        /// <summary>
        /// Get all speed excesses for selected date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="speed">Speed limit</param>
        /// <returns>All speed excesses for selected date</returns>
        [HttpGet]
        [Route("speedexcesses/{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}})}/{speed:float:regex(\\d{{1,3}}.\\d)}")]
        [Route("speedexcesses/{date:datetime:regex(\\d{{2}}-\\d{{2}}-\\d{{4}})}/{speed:float:regex(\\d{{1,3}}.\\d)}")]
        public string GetSpeedExcesses(DateTime date, float speed)
        {
            if (CheckTime())
            {
                return _systemCore.GetSpeedExcesses(date, speed);
            }
            else
                throw GetTimeError();
        }

        /// <summary>
        /// Check system availability hours
        /// </summary>
        /// <returns></returns>
        private bool CheckTime()
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            // see if start comes before end
            if (_appConfig.StartWorkingTime < _appConfig.EndWorkingTime)
                return _appConfig.StartWorkingTime <= now && now <= _appConfig.EndWorkingTime;
            // start is after end, so do the inverse comparison
            return !(_appConfig.EndWorkingTime <= now && now <= _appConfig.StartWorkingTime);
        }

        /// <summary>
        /// Create exception object for time availability erros
        /// </summary>
        private InvalidOperationException GetTimeError()
        {
            var errorText = FormatTimeErrorText();
            _logger.LogError(errorText);
            return new InvalidOperationException(errorText);
        }

        /// <summary>
        /// Create text for time availability erros
        /// </summary>
        private string FormatTimeErrorText()
        {
            return $"System currently not available. Try again in system availability hours ({_appConfig.StartWorkingTime:hh\\:mm} - {_appConfig.EndWorkingTime:hh\\:mm})";
        }

        /// <summary>
        /// Add new fixed speed to base
        /// </summary>
        /// <param name="date">Fixation date</param>
        /// <param name="number">License plate</param>
        /// <param name="speed">Fixed speed</param>
        /// https://localhost:5001/speedcontrolsystem/fixspeed/12-12-2020%2010:03:30/3324%20KB-1/122.2
        [HttpGet]
        [Route("fixspeed/{date:datetime:regex(\\d{{4}}-\\d{{2}}-\\d{{2}} \\d{{2}}:\\d{{2}}:\\d{{2}})}/{licensePlate:length(9):regex(\\d{{4}} [[a-zA-Z]]{{2}}-\\d)}/{speed:float:regex(\\d{{1,3}}.\\d)}")]
        [Route("fixspeed/{date:datetime:regex(\\d{{2}}-\\d{{2}}-\\d{{4}} \\d{{2}}:\\d{{2}}:\\d{{2}})}/{licensePlate:length(9):regex(\\d{{4}} [[a-zA-Z]]{{2}}-\\d)}/{speed:float:regex(\\d{{1,3}}.\\d)}")]
        public void FixSpeed(DateTime date, string licensePlate, float speed)
        {
            _systemCore.FixSpeed(date, licensePlate, speed);
        }
    }
}
