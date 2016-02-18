using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPowerShell
{
    public class Pi
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public string AssetNumber { get; set; }

        public string TimeUpdatedSuccessfullyAt { get; set; }
        public bool TimeUpdatedSuccessfully { get; set; }
        public int TimeFailedCounter { get; set; }

    }
}
