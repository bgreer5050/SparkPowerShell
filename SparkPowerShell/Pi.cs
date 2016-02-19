using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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


    public class Pi : INotifyPropertyChanged
    {

        private string ipAddress;
        private string hostName;
        private string assetNumber;
        private string note;
        private string timeUpdatedSuccessfullyAt;
        private bool timeUpdatedSuccessfully;
        private int timeFailedCounter;
        private string currentDateTime;

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string status)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(status));
            }
        }

        public string IPAddress
        {
            get { return ipAddress; }

            set
            {
                ipAddress = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IPAddress"));
            }

        }
        public string HostName
        {
            get
            {
                return hostName;
            }
            set
            {
                hostName = value;
                PropertyChanged(this, new PropertyChangedEventArgs("HostName"));
            }
        }
        public string AssetNumber
        {
            get
            {
                return assetNumber;
            }

            set
            {
                assetNumber = value;
                PropertyChanged(this, new PropertyChangedEventArgs("AssetNumber"));
            }
        }

        public string Note
        {
            get
            {
                return note;
            }

            set
            {
                note = value;
                PropertyChanged(this, new PropertyChangedEventArgs("Note"));
            }
        }

        public string TimeUpdatedSuccessfullyAt
        {
            get
            {
                return timeUpdatedSuccessfullyAt;
            }

            set
            {
                timeUpdatedSuccessfullyAt = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeUpdatedSuccessfullyAt"));
            }
        }

        public bool TimeUpdatedSuccessfully
        {
            get
            {
                return timeUpdatedSuccessfully;
            }

            set
            {
                timeUpdatedSuccessfully = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeUpdatedSuccessfully"));
            }
        }

        public int TimeFailedCounter
        {
            get
            {
                return timeFailedCounter;
            }

            set
            {
                timeFailedCounter = value;
                PropertyChanged(this, new PropertyChangedEventArgs("TimeFailedCounter"));
            }
        }
        public string CurrentDateTime
        {
            get
            {
                return currentDateTime;
            }

            set
            {
                currentDateTime = value;
                PropertyChanged(this, new PropertyChangedEventArgs("CurrentDateTime"));
            }
        }
    }
}
