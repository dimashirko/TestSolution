using Microsoft.AspNetCore.Mvc.Infrastructure;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

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
        public string GetSpeedExtremums(DateTime date)
        {
            var dateFolderPath = _systemDatabasePath + date.ToString("dd.MM.yyyy"); var dirInfo = new DirectoryInfo(dateFolderPath);
            if (!dirInfo.Exists)
            {
                return string.Empty;
            }
            else
            {
                var result = new StringBuilder();
                var files = dirInfo.GetFiles().OrderBy(x => float.Parse(x.Name, CultureInfo.InvariantCulture));
                var minSpeed = files.FirstOrDefault();
                var maxSpeed = files.LastOrDefault();
                if (minSpeed != null)
                {
                    result.AppendLine(File.ReadLines(minSpeed.FullName).FirstOrDefault());
                    result.AppendLine(File.ReadLines(maxSpeed.FullName).FirstOrDefault());
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// Get all speed excesses for selected date
        /// </summary>
        /// <param name="date">Date</param>
        /// <param name="speed">Speed limit</param>
        /// <returns>All speed excesses for selected date</returns>
        public string GetSpeedExcesses(DateTime date, float speed)
        {
            var dateFolderPath = _systemDatabasePath + date.ToString("dd.MM.yyyy"); var dirInfo = new DirectoryInfo(dateFolderPath);
            if (!dirInfo.Exists)
            {
                return string.Empty;
            }
            else
            {
                var result = new StringBuilder();
                var files = dirInfo.GetFiles().Where(x => float.Parse(x.Name, CultureInfo.InvariantCulture) > speed);
                foreach(var file in files)
                {
                    result.AppendLine(File.ReadAllText(file.FullName));
                }
                return result.ToString();
            }
        }

        /// <summary>
        /// Add new fixed speed to base
        /// </summary>
        /// <param name="date">Fixation date</param>
        /// <param name="number">License plate</param>
        /// <param name="speed">Fixed speed</param>
        public void FixSpeed(DateTime date, string licensePlate, float speed)
        {
            var dateFolderPath = _systemDatabasePath + date.ToString("dd.MM.yyyy");
            var fileName = speed.ToString("N1", CultureInfo.InvariantCulture);
            var filePath = dateFolderPath + '\\' + fileName; 
            var dirInfo = new DirectoryInfo(dateFolderPath);
            if (!dirInfo.Exists)
            {
                dirInfo.Create();
            }

            File.AppendAllLines(filePath, new[] { date.ToString("dd.MM.yyyy HH:mm:ss") + ' ' + licensePlate + ' ' + fileName});
        }
    }
}
