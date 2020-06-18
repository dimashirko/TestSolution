using System;

namespace SpeedControlSystemService
{
    /// <summary>
    /// Describes fixed speed info
    /// </summary>
    public class SpeedInfo
    {
        /// <summary>
        /// Fixing dates
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Fixed auto license plate
        /// </summary>
        public string LicensePlate { get; set; }

        /// <summary>
        /// Fixed speed
        /// </summary>
        public float Speed { get; set; }
    }
}
