﻿using System;
using System.Collections.Generic;
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
        public List<Pi> Pis;

        private Timer _timer;
        
      
        public MainWindow()
        {
            InitializeComponent();
                     

            Pis = new List<Pi>();

            Pis.Add(new Pi { AssetNumber = "135", HostName = "m135spark", IPAddress = "10.0.205.12" });
            Pis.Add(new Pi { AssetNumber = "702", HostName = "m702spark", IPAddress = "10.0.110.27" });
            Pis.Add(new Pi { AssetNumber = "1005", HostName = "m1005spark", IPAddress = "10.0.205.14" });
            Pis.Add(new Pi { AssetNumber = "1090", HostName = "m1090spark", IPAddress = "10.0.205.10" });
            Pis.Add(new Pi { AssetNumber = "155", HostName = "m155spark2", IPAddress = "" });
            Pis.Add(new Pi { AssetNumber = "944", HostName = "m944spark", IPAddress = "" });
            Pis.Add(new Pi { AssetNumber = "1081", HostName = "m1081spark", IPAddress = "" });
            Pis.Add(new Pi { AssetNumber = "1087", HostName = "m1087spark", IPAddress = "" });
            Pis.Add(new Pi { AssetNumber = "605", HostName = "m605spark", IPAddress = "10.0.110.25" });
            Pis.Add(new Pi { AssetNumber = "701", HostName = "m701spark", IPAddress = "10.0.110.22" });
          
            Pis.Add(new Pi { AssetNumber = "804", HostName = "m804spark", IPAddress = "10.0.110.24" });
            // Pis.Add(new Pi { AssetNumber = "483", HostName = "", IPAddress = "" });

            timerUppdateTime = new Timer(UpdatePis, null, 5000, 900000);
    }

        private void UpdateUI(object state)
        {
            Dispatcher.Invoke(() =>
            {
                txtResult.Text = DateTime.Now.ToLongDateString();

                foreach(Pi _p in Pis.ToList())
                {
                    if(_p.TimeUpdatedSuccessfully == false)
                    {
                      //   _p.ControlColor = new SolidColorBrush { Color = Color.FromRgb(100, 100, 100) };
                    }
                }
            });
        }

        private void UpdatePis(object state)
        {
            int intSuccess = 0;
            int intFail = 0;

            _timer = new Timer(UpdateUI, null, 5000, 5000);
                        
            foreach (Pi p in Pis)
            {
                string strMessages = p.HostName;
                strMessages += " ";

                var tokenSource = new CancellationTokenSource();
                var token = tokenSource.Token;

                var task = Task.Run(() =>
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
                    }
                    catch (Exception ex)
                    {
                        strMessages += " ";
                        strMessages += ex.Message;
                        intFail += 1;
                        p.TimeUpdatedSuccessfully = false;

                        //txtResult.Text = "ERROR: " + ex.Message;
                    }
                }, token);

                try
                {
                    task.Wait(60000);
                }
                catch(AggregateException _ex)
                {
                    p.TimeUpdatedSuccessfully = false;
                    
                }

               // txtResult.Text = "Success " + intSuccess.ToString() + " Fail " + intFail.ToString() + strMessages;
                //MessageBox.Show("Success " + intSuccess.ToString() + " Fail " + intFail.ToString() + strMessages);
            }
        }

        private async void btnSetTime_Click(object sender, RoutedEventArgs e)
        {
            
        }
        
        System.Threading.Timer timerUppdateTime;

    }
}
