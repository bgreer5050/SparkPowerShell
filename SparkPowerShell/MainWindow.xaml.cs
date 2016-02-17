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
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnSetTime_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string domainAndUserName = @"10.0.99.97\Administrator";

                //char[] pw = "p@ssw0rd".ToCharArray();


                //System.Security.SecureString securePassword = new System.Security.SecureString(,10);

                //Create a SecureString object
                SecureString securePassword;

                // Define the string value to assign to a new secure string.
                //char[] chars = { 'p', '@', 's', 's', 'w', '0', 'r', 'd' };
                char[] chars = { 's', 'p', 'a', 'r', 'k', 'p', 'i'};

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
                txtResult.Text = "test "; 
                System.Threading.Thread.Sleep(2000);


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
