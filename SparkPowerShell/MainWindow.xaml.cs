using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SparkPowerShell
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<Pi> Pis;

        public MainWindow()
        {
            InitializeComponent();

            //Pis = new List<Pi>();

            Pis = new ObservableCollection<Pi>()
            {
            new Pi { AssetNumber = "135", HostName = "m135spark", IPAddress = "10.0.205.12" },
            new Pi { AssetNumber = "702", HostName = "m702spark", IPAddress = "10.0.110.27" },
            new Pi { AssetNumber = "1005", HostName = "m1005spark", IPAddress = "10.0.205.14" },
            new Pi { AssetNumber = "1090", HostName = "m1090spark", IPAddress = "10.0.205.10" },
            new Pi { AssetNumber = "155", HostName = "m155spark2", IPAddress = "" },
            new Pi { AssetNumber = "944", HostName = "m944spark", IPAddress = "" },
            new Pi { AssetNumber = "1081", HostName = "m1081spark", IPAddress = "" },
            new Pi { AssetNumber = "1087", HostName = "m1087spark", IPAddress = "" },
            new Pi { AssetNumber = "605", HostName = "m605spark", IPAddress = "10.0.110.25" },
            new Pi { AssetNumber = "701", HostName = "m701spark", IPAddress = "10.0.110.22" },
            new Pi { AssetNumber = "804", HostName = "m804spark", IPAddress = "10.0.110.24" },
            new Pi { AssetNumber = "483", HostName = "", IPAddress = "" }
        };
             timerUppdateTime = new Timer(UpdatePis, null, 5000, 600000);
            this.DataContext = Pis;
            lstNames.ItemsSource = Pis;
            
    }

        private async void UpdatePis(object state)
        {
            int intSuccess = 0;
            int intFail = 0;

            foreach (Pi p in Pis)
            {
                p.Note = "Attempting: " + DateTime.Now.ToLocalTime();

                string strMessages = p.HostName;
                strMessages += " ";

                var cts = new CancellationTokenSource();
                CancellationToken token = cts.Token;
                cts.CancelAfter(60000);

                //var token = new CancellationTokenSource(1000).Token;
                // var token = tokenSource.Token;

                System.Timers.Timer timerTaskTimeLimit = new System.Timers.Timer();
                timerTaskTimeLimit.Elapsed += TimerTaskTimeLimit_Elapsed;



                var task = Task.Factory.StartNew(() =>
                {

                    try
                    {

                        string domainAndUserName;
                        if (p.IPAddress != null && p.IPAddress.Length > 5)
                        {
                            domainAndUserName = p.IPAddress + @"\Administrator";
                        }
                        else
                        {
                            domainAndUserName = p.HostName + @"\Administrator";
                        }

                        strMessages += domainAndUserName;
                        strMessages += " - ";
                        //char[] pw = "p@ssw0rd".ToCharArray();
                        //System.Security.SecureString securePassword = new System.Security.SecureString(,10);

                        //Create a SecureString object
                        SecureString securePassword;

                        // Define the string value to assign to a new secure string.
                        //char[] chars = { 'p', '@', 's', 's', 'w', '0', 'r', 'd' };
                        char[] chars = { 's', 'p', 'a', 'r', 'k', 'p', 'i' };

                        unsafe
                        {
                            fixed (char* pChars = chars)
                            {
                                securePassword = new SecureString(pChars, chars.Length);
                            }
                        }


                        PSCredential remoteMachineCredentials = new PSCredential(domainAndUserName, securePassword);


                        WSManConnectionInfo connectionInfo;
                        if (p.IPAddress != null && p.IPAddress.Length > 5)
                        {
                            connectionInfo = new WSManConnectionInfo(false, p.IPAddress, 5985, "/wsman", @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell", remoteMachineCredentials);
                        }
                        else
                        {
                            connectionInfo = new WSManConnectionInfo(false, p.HostName, 5985, "/wsman", @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell", remoteMachineCredentials);
                        }
                        Runspace rspace = RunspaceFactory.CreateRunspace();
                        rspace.Open();
                        Pipeline pl;
                        if (p.IPAddress != null && p.IPAddress.Length > 5)
                        {
                            pl = rspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value " + p.IPAddress + " -Force");
                        }
                        else
                        {
                            pl = rspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value " + p.HostName + " -Force");
                        }


                        //rspace.Open();
                        var _result = pl.Invoke();
                        Debug.WriteLine("R1: " + _result.ToString());

                        using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                        {

                            runspace.Open();
                            //string command = CommandBuilder.Reboot();
                            string command = CommandBuilder.SetTime();
                            Pipeline pipeline = runspace.CreatePipeline(command);
                            //Pipeline pipeline = runspace.CreatePipeline("set-date \"Thursday, February 11, 2016 1:03:00 PM\"");
                            // Pipeline pipeline = runspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value 10.0.99.97");

                            var results = pipeline.Invoke();

                             pipeline = runspace.CreatePipeline("get-date");
                             var respGetDate = pipeline.Invoke();
                            p.CurrentDateTime = respGetDate[0].ToString();
                            //txtResult.Text = results.Count.ToString();
                            intSuccess += 1;
                            p.TimeUpdatedSuccessfully = true;
                        }

                        securePassword.Dispose();
                        token.ThrowIfCancellationRequested();
                    }
                    catch (Exception ex)
                    {
                        p.TimeUpdatedSuccessfully = false;
                        strMessages += " ";
                        strMessages += ex.Message;
                        intFail += 1;
                        //txtResult.Text = "ERROR: " + ex.Message;
                    }
                }, token);

                try
                {
                    task.Wait();
                }
                catch (AggregateException ex)
                {
                    p.TimeUpdatedSuccessfully = false;
                    strMessages += ex.Message;
                }
                MessageBox.Show("Success " + intSuccess.ToString() + " Fail " + intFail.ToString() + strMessages);
            }
        }

        private async void btnSetTime_Click(object sender, RoutedEventArgs e)
        {
           
        }

        private void TimerTaskTimeLimit_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            throw new NotImplementedException();
        }

        System.Threading.Timer timerUppdateTime;

    }
}
