using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SetUpSpark.Desktop
{
    public partial class Form1 : Form
    {
        SparkPi pi;

        public string Message
        {
            get { return lblMessage.Text; }
            set { lblMessage.Text = value; }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
          
            pi.Connect();
        }

        private void btnSetPassword_Click(object sender, EventArgs e)
        {
           
        }

        private void btnSetTimeZone_Click(object sender, EventArgs e)
        {
          
            pi.SetTimeZone();
        }

        private void btnCopyTaskFile_Click(object sender, EventArgs e)
        {

            MessageBox.Show("NOT IMPLEMENTED");

            //System.IO.File.Copy(@"c:\Temp\SyncTime.ps1", @"\\192.168.1.109\c$\SyncTime2.ps1");
            
            
            //if (pi == null)
            //{
            //    Form1 frm = (Form1)this.FindForm();
            //    pi = new SparkPi("192.168.1.109", "192.168.1.109", frm);
            //}
            //pi.CopyTimeTaskToPi();


        }

        private void btnScheduleTasks_Click(object sender, EventArgs e)
        {
            
            pi.ScheduleTasks();
            MessageBox.Show("Make Sure SyncTime.ps1 exists in the root of the device.");
        }

        private void btnSetDNSServer_Click(object sender, EventArgs e)
        {
            
            pi.SetDNSEthernet();
        }

        private void btnSetDNSWiFi_Click(object sender, EventArgs e)
        {
          
            pi.SetDNSWiFi();
        }

        private void cmbPlant_ValueMemberChanged(object sender, EventArgs e)
        {
            Debug.WriteLine("kdhbfvkhdf");
        }

        private void cmbPlant_SelectedValueChanged(object sender, EventArgs e)
        {
            switch(this.cmbPlant.SelectedItem.ToString())
            {
                case "Minneapolis":
                    MessageBox.Show("1");

                    break;

                case "Bedford":
                    MessageBox.Show("2");

                    break;

                case "Middletown":
                    MessageBox.Show("3");

                    break;

                case "Humboldt":
                    MessageBox.Show("4");

                    break;

                default:
                    MessageBox.Show("ERROR");

                    break;
            }

            MessageBox.Show(this.cmbPlant.SelectedItem.ToString());
        }

        private void btnNewName_Click(object sender, EventArgs e)
        {
            Form1 frm = (Form1)this.FindForm();
            pi.ChangeName(txtNewName.Text);
         
        }

        private void textBox1_Leave(object sender, EventArgs e)
        {
            this.pi.IPAddress = textBox1.Text.Trim();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            pi = new SparkPi("0.0.0.0", "0.0.0.0", this);
            textBox1.Text = "0.0.0.0";
        }
    }



    public class SparkPi
    {
        private string ipAddress;

        private string _domainName;

        public string DomainName
        {
            get {
                return _domainName;
            }
            set { _domainName = value; }
        }


        private SecureString _securePassword;

        public SecureString SecurePassword
        {
            get {
                if(_securePassword == null)
                {
                    // Define the string value to assign to a new secure string.
                    //char[] chars = { 'p', '@', 's', 's', 'w', '0', 'r', 'd' };
                    char[] chars = { 's', 'p', 'a', 'r', 'k', 'p', 'i' };
                    unsafe
                    {
                        fixed (char* pChars = chars)
                        {
                            _securePassword = new SecureString(pChars, chars.Length);
                         }
                    }
                }
                return _securePassword;
            }
            set { _securePassword = value; }
        }

        private WSManConnectionInfo _connectionInfo;

        public WSManConnectionInfo connectionInfo
        {
            get {

                if(_connectionInfo == null)
                {

                    connectionInfo = new WSManConnectionInfo(false, IPAddress, 5985, "/wsman", @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell", RemoteMachineCredentials);

                }

                return _connectionInfo;
            }
            set { _connectionInfo = value; }
        }

        private PSCredential _remoteMachineCredentials;

        public PSCredential RemoteMachineCredentials
        {
            get {
                if(_remoteMachineCredentials == null)
                {
                    string strDomainAndUserName = ipAddress;
                    strDomainAndUserName += @"\Administrator";
                    _remoteMachineCredentials = new PSCredential(strDomainAndUserName, SecurePassword);
                }

                return _remoteMachineCredentials;
            }
            set { _remoteMachineCredentials = value; }
        }






        public string IPAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                ipAddress = value;
            }
        }

        private Form1 form;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="strIPAddress">The IP Address of the pi</param>
        /// <param name="frmMain"></param>
        public SparkPi(string strIPAddress,string strDomainName, Form1 frmMain)
        {
            form = frmMain;
            this.IPAddress = strIPAddress;
            this.DomainName = strDomainName;
        }

        public bool Connect()
        {
            AddToTrustedHostsLists(this.IPAddress);

            // TODO Should not be hardset
            return false;

        }

        public  void AddToTrustedHostsLists(string _ip)
        {
           // string domainAndUserName = _ip + @"\Administrator";
         
            Runspace rspace = RunspaceFactory.CreateRunspace();
            rspace.Open();

            Pipeline pl;
            pl = rspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value " + _ip + " -Force");

            var _result = pl.Invoke();
            form.Message = _result.ToString();

            pl.Dispose();
            


            //Try running a command

            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                runspace.Open();
                //string command = CommandBuilder.Reboot();
                // string command = CommandBuilder.SetTime();
                Pipeline pipeline = runspace.CreatePipeline("get-date");
                //Pipeline pipeline = runspace.CreatePipeline("set-date \"Thursday, February 11, 2016 1:03:00 PM\"");
                // Pipeline pipeline = runspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value 10.0.99.97");

                var results = pipeline.Invoke();

                pipeline = runspace.CreatePipeline("get-date");
                var respGetDate = pipeline.Invoke();
                form.Message = respGetDate[0].ToString();
               // p.CurrentDateTime = respGetDate[0].ToString();
                //txtResult.Text = results.Count.ToString();
                //intSuccess += 1;
               // p.TimeUpdatedSuccessfully = true;
                //p.TimeUpdatedSuccessfullyAt = DateTime.Now.ToLongTimeString();
                //p.TimeUpdatedLastAtTicks = DateTime.Now.Ticks;

            }


        }

        internal void SetTimeZone()
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                runspace.Open();
                Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");
              
                var results = pipeline.Invoke();
                if (results.Count > 0)
                {

                    form.Message = results[0].ToString();
                }
                else
                {
                    form.Message = "Timezone Updated!";
                }
            }
        }

        //internal void CopyTimeTaskToPi()
        //{
        //    System.Net.NetworkCredential nc = new System.Net.NetworkCredential("Administrator", "p@ssw0rd", "192.168.1.109");


        //    using (new  NetworkConnection(@"\\server2\write", writeCredentials))
        //    {
        //        File.Copy(@"\\server\read\file", @"\\server2\write\file");
        //    }


        //    System.IO.File.Create(@"\\192.168.1.109\c$\SyncTime.ps1");

        //    string strCommand = @"use K: \\192.168.1.109\c$ /USER:192.168.1.109\Administrator p@ssw0rd";

        //    System.Text.StringBuilder sb = new StringBuilder();
        //    sb.Append("use K: ");
        //    sb.Append(@"\\192.168.1.109");
        //    sb.Append(@"\c$");
        //    sb.Append(" /USER:192.168.1.109");
        //    sb.Append(@"\Administrator ");
        //    //p @ssw0rd");

        //    System.Diagnostics.Process p = new System.Diagnostics.Process();
        //    System.Diagnostics.ProcessStartInfo info = new System.Diagnostics.ProcessStartInfo(@"c:\Windows\System32\net.exe", sb.ToString());

        //    p.StartInfo = info;

        //    p.Exited += P_Exited;
        //    p.Start();

        //    p.ErrorDataReceived += P_ErrorDataReceived;
        //    p.OutputDataReceived += P_OutputDataReceived;





        //    //System.Diagnostics.Process.Start("net.exe", sb.ToString());
        //    //System.Diagnostics.Process p = System.Diagnostics.Process.Start(psi);

        //    //using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
        //    //{

        //    //    runspace.Open();
        //    //    Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");

        //    //    var results = pipeline.Invoke();
        //    //    if (results.Count > 0)
        //    //    {

        //    //        form.Message = results[0].ToString();
        //    //    }
        //    //    else
        //    //    {
        //    //        form.Message = "Timezone Updated!";
        //    //    }
        //    //}
        //}

        private void P_OutputDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void P_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            throw new NotImplementedException();
        }

        private void P_Exited(object sender, EventArgs e)
        {

            throw new NotImplementedException();
        }

        internal void ScheduleTasks()
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                runspace.Open();
                //Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");
                Pipeline  pipeline = runspace.CreatePipeline(@"schtasks /Create /SC ONSTART /TN TimeSync /TR c:\SyncTime.ps1");

                var results = pipeline.Invoke();
                if (results.Count > 0)
                {

                    form.Message = results[0].ToString();
                }
                else
                {
                    form.Message = "Timezone Updated!";
                }
            }
        }

        internal void SetDNSEthernet()
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                

                runspace.Open();
                //Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");
                Pipeline pipeline = runspace.CreatePipeline(@"netsh interface ipv4 add dnsserver ""Ethernet"" address=10.0.0.1 index=1");

                var results = pipeline.Invoke();
                if (results.Count > 0)
                {

                    form.Message = results[0].ToString();
                }
                else
                {
                    form.Message = "Nothing Returned";
                }
            }
        }

        internal void SetDNSWiFi()
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                runspace.Open();
                //Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");
                Pipeline pipeline = runspace.CreatePipeline(@"netsh interface ipv4 add dnsserver ""Wi-Fi"" address=10.0.0.1 index=1");

                var results = pipeline.Invoke();
                if (results.Count > 0)
                {

                    form.Message = results[0].ToString();
                }
                else
                {
                    form.Message = "Nothing Returned";
                }

                form.Message = "Confirm The WiFi Module Is Plugged In and Test!";
            }
        }

        internal void ChangeName(string text)
        {
            using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
            {

                runspace.Open();
                //Pipeline pipeline = runspace.CreatePipeline(@"tzutil /s ""Central Standard Time""");
                Pipeline pipeline = runspace.CreatePipeline(@"setcomputername " + text);

                var results = pipeline.Invoke();
                if (results.Count > 0)
                {

                    form.Message = results[0].ToString();
                }
                else
                {
                    form.Message = "Nothing Returned";
                }

            }
        }


        //public void SetPassword()
        //{
        //    using (Runspace runspace = RunspaceFactory.CreateRunspace(connectionInfo))
        //    {

        //        runspace.Open();
        //        //string command = CommandBuilder.Reboot();
        //        // string command = CommandBuilder.SetTime();
        //        Pipeline pipeline = runspace.CreatePipeline("net user Administrator sparkpi");
        //        //Pipeline pipeline = runspace.CreatePipeline("set-date \"Thursday, February 11, 2016 1:03:00 PM\"");
        //        // Pipeline pipeline = runspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value 10.0.99.97");

        //        var results = pipeline.Invoke();

        //        pipeline = runspace.CreatePipeline("get-date");
        //        var respGetDate = pipeline.Invoke();
        //        form.Message = respGetDate[0].ToString();
        //        // p.CurrentDateTime = respGetDate[0].ToString();
        //        //txtResult.Text = results.Count.ToString();
        //        //intSuccess += 1;
        //        // p.TimeUpdatedSuccessfully = true;
        //        //p.TimeUpdatedSuccessfullyAt = DateTime.Now.ToLongTimeString();
        //        //p.TimeUpdatedLastAtTicks = DateTime.Now.Ticks;

        //    }
        //}
    }
}
