using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
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

        public MainWindow()
        {
            InitializeComponent();

        Pis = new List<Pi>();

        Pi m135spark = new Pi { AssetNumber = "135", HostName = "m135spark", IPAddress = "10.0.205.12" };
        Pi m1005spark = new Pi { AssetNumber = "1005", HostName = "m1005spark", IPAddress = "10.0.205.14" };
        Pi m1090spark = new Pi { AssetNumber = "1090", HostName = "m1090spark", IPAddress = "10.0.205.10" };

        Pi m155spark = new Pi { AssetNumber = "155", HostName = "m155spark", IPAddress = "" };
        Pi m944spark = new Pi { AssetNumber = "944", HostName = "m944spark", IPAddress = "" };
        Pi m1081spark = new Pi { AssetNumber = "1081", HostName = "m1081spark", IPAddress = "" };
        Pi m1087spark = new Pi { AssetNumber = "1087", HostName = "m1087spark", IPAddress = "" };

        Pi m605spark = new Pi { AssetNumber = "605", HostName = "m605spark", IPAddress = "10.0.110.25" };
        Pi m701spark = new Pi { AssetNumber = "701", HostName = "m701spark", IPAddress = "10.0.110.22" };
        Pi m702spark = new Pi { AssetNumber = "702", HostName = "m702spark", IPAddress = "10.0.110.23" };
        Pi m804spark = new Pi { AssetNumber = "804", HostName = "m804spark", IPAddress = "10.0.110.24" };

        Pi m483spark1 = new Pi { AssetNumber = "483", HostName = "", IPAddress = "" };

       

    }

        private void btnSetTime_Click(object sender, RoutedEventArgs e)
        {

            foreach (Pi p in Pis)
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


                    WSManConnectionInfo connectionInfo = new WSManConnectionInfo(false, @"10.0.110.27", 5985, "/wsman", @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell", remoteMachineCredentials);

                    Runspace rspace = RunspaceFactory.CreateRunspace();
                    rspace.Open();
                    Pipeline pl = rspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value 10.0.110.27 -Force");
                    var res = pl.Invoke();
                   
                    using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
                    {

                        runspace.Open();
                        //string command = CommandBuilder.Reboot();
                        string command = CommandBuilder.SetTime();
                        Pipeline pipeline = runspace.CreatePipeline(command);
                        //Pipeline pipeline = runspace.CreatePipeline("set-date \"Thursday, February 11, 2016 1:03:00 PM\"");
                        // Pipeline pipeline = runspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value 10.0.99.97");

                        var results = pipeline.Invoke();

                        txtResult.Text = results.Count.ToString();

                    }

                    securePassword.Dispose();
                }
                catch (Exception ex)
                {

                    txtResult.Text = "ERROR: " + ex.Message;
                }
            }
        }
    }
}
