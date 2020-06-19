using System;
using System.Collections.Generic;

namespace SpeedControlSystemService
{
    /// <summary>
    /// Base system functionality
    /// </summary>
    public class SpeedControlSystemCore
    {
        private readonly string _systemDatabasePath;

        /// <summary>
        /// .ctor
        /// </summary>
        public SpeedControlSystemCore(string systemPath)
        {
            _systemDatabasePath = systemPath;
        }

        /// <summary>
        /// Get max and min fixed speed for selected date
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Max and min fixed speed for selected date</returns>
        public IEnumerable<SpeedInfo> GetSpeedExtremums(DateTime date)
        {
            return new List<SpeedInfo>() { new SpeedInfo() { Date = DateTime.Now, LicensePlate = "Extremums", Speed = 67.7f } };
        }

        /// <summary>
        /// Get all speed excesses for selected date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="speed">Speed limit</param>
        /// <returns>All speed excesses for selected date</returns>
        public IEnumerable<SpeedInfo> GetSpeedExcesses(DateTime date, float speed)
        {
            return new List<SpeedInfo>() { new SpeedInfo() { Date = DateTime.Now, LicensePlate = "Excesses", Speed = 67.7f } };
        }

        /// <summary>
        /// Add new fixed speed to base
        /// </summary>
        /// <param name="date">Fixation date</param>
        /// <param name="number">License plate</param>
        /// <param name="speed">Fixed speed</param>
        public void FixSpeed(DateTime date, string licensePlate, float speed)
        {
            var dateFolder = _systemDatabasePath + date.ToString("dd.MM.yyyy");
            var fileName = speed.ToString("N1");
        }
    }
}
