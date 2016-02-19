using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SparkPowerShell
{

    //public class PiList : ObservableCollection<Pi>
    //{
    //    public PiList():base()
    //    {
    //        Add(new Pi { AssetNumber = "135", HostName = "m135spark", IPAddress = "10.0.205.12" });
    //        Add(new Pi { AssetNumber = "702", HostName = "m702spark", IPAddress = "10.0.110.27" });
    //        Add(new Pi { AssetNumber = "1005", HostName = "m1005spark", IPAddress = "10.0.205.14" });
    //        Add(new Pi { AssetNumber = "1090", HostName = "m1090spark", IPAddress = "10.0.205.10" });
    //        Add(new Pi { AssetNumber = "155", HostName = "m155spark2", IPAddress = "" });
    //        Add(new Pi { AssetNumber = "944", HostName = "m944spark", IPAddress = "" });
    //        Add(new Pi { AssetNumber = "1081", HostName = "m1081spark", IPAddress = "" });
    //        Add(new Pi { AssetNumber = "1087", HostName = "m1087spark", IPAddress = "" });
    //        Add(new Pi { AssetNumber = "605", HostName = "m605spark", IPAddress = "10.0.110.25" });
    //        Add(new Pi { AssetNumber = "701", HostName = "m701spark", IPAddress = "10.0.110.22" });
    //        Add(new Pi { AssetNumber = "804", HostName = "m804spark", IPAddress = "10.0.110.24" });

    //    }
        //}


    public class Pi
    {
        public string IPAddress { get; set; }
        public string HostName { get; set; }
        public string AssetNumber { get; set; }

        public string Note { get; set; }

        public string TimeUpdatedSuccessfullyAt { get; set; }
        public bool TimeUpdatedSuccessfully { get; set; }
        public int TimeFailedCounter { get; set; }
        public string CurrentDateTime { get; set; }
    }
}
