using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
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
            Form1 frm = (Form1)this.FindForm();
            SparkPi pi = new SparkPi("10.0.0.82", frm);
            pi.Connect("10.0.0.82");
        }
    }



    public class SparkPi
    {
        private string ipAddress;

        public string IPAddress
        {
            get { return ipAddress; }
            set { ipAddress = value; }
        }

        private Form1 form;
        public SparkPi(string strIPAddress, Form1 frmMain)
        {
            form = frmMain;
        }

        public bool Connect(string _ip)
        {
            AddToTrustedHostsLists(_ip);

            // TODO Should not be hardset
            return false;

        }

        public  void AddToTrustedHostsLists(string _ip)
        {
            string domainAndUserName = _ip + @"\Administrator";

            // Define the string value to assign to a new secure string.
            //char[] chars = { 'p', '@', 's', 's', 'w', '0', 'r', 'd' };
            char[] chars = { 's', 'p', 'a', 'r', 'k', 'p', 'i' };
            SecureString securePassword;

            unsafe
            {
                fixed (char* pChars = chars)
                {
                    securePassword = new SecureString(pChars, chars.Length);
                }
            }

            PSCredential remoteMachineCredentials = new PSCredential(domainAndUserName, securePassword);

            WSManConnectionInfo connectionInfo;
            connectionInfo = new WSManConnectionInfo(false, _ip, 5985, "/wsman", @"http://schemas.microsoft.com/powershell/Microsoft.PowerShell", remoteMachineCredentials);

            Runspace rspace = RunspaceFactory.CreateRunspace();
            rspace.Open();

            Pipeline pl;
            pl = rspace.CreatePipeline("Set-Item WSMan:\\localhost\\Client\\TrustedHosts -Value " + _ip + " -Force");

            var _result = pl.Invoke();
            form.Message = _result.ToString();

            pl.Dispose();
             
        }
    }
}
