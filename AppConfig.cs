using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SpeedControlSystemService
{
    public class AppConfig
    {
        public string DataBasePath { get; set; }
        public TimeSpan StartWorkingTime { get; set; }
        public TimeSpan EndWorkingTime { get; set; }
    }
}
