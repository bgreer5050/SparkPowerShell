namespace SetUpSpark.Desktop
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.btnConnect = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtDNSServer = new System.Windows.Forms.TextBox();
            this.btnSetDNSServer = new System.Windows.Forms.Button();
            this.btnSetPassword = new System.Windows.Forms.Button();
            this.btnSetTimeZone = new System.Windows.Forms.Button();
            this.btnCopyTaskFile = new System.Windows.Forms.Button();
            this.btnScheduleTasks = new System.Windows.Forms.Button();
            this.btnSetDNSWiFi = new System.Windows.Forms.Button();
            this.cmbPlant = new System.Windows.Forms.ComboBox();
            this.txtNewName = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnNewName = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(41, 49);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.Leave += new System.EventHandler(this.textBox1_Leave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(41, 30);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "Spark IP Address";
            // 
            // btnConnect
            // 
            this.btnConnect.Location = new System.Drawing.Point(47, 75);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 2;
            this.btnConnect.Text = "Connect ";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(293, 373);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 3;
            // 
            // txtDNSServer
            // 
            this.txtDNSServer.Location = new System.Drawing.Point(46, 185);
            this.txtDNSServer.Name = "txtDNSServer";
            this.txtDNSServer.Size = new System.Drawing.Size(100, 20);
            this.txtDNSServer.TabIndex = 4;
            // 
            // btnSetDNSServer
            // 
            this.btnSetDNSServer.Location = new System.Drawing.Point(55, 211);
            this.btnSetDNSServer.Name = "btnSetDNSServer";
            this.btnSetDNSServer.Size = new System.Drawing.Size(75, 23);
            this.btnSetDNSServer.TabIndex = 5;
            this.btnSetDNSServer.Text = "Set DNS";
            this.btnSetDNSServer.UseVisualStyleBackColor = true;
            this.btnSetDNSServer.Click += new System.EventHandler(this.btnSetDNSServer_Click);
            // 
            // btnSetPassword
            // 
            this.btnSetPassword.Location = new System.Drawing.Point(41, 124);
            this.btnSetPassword.Name = "btnSetPassword";
            this.btnSetPassword.Size = new System.Drawing.Size(102, 23);
            this.btnSetPassword.TabIndex = 6;
            this.btnSetPassword.Text = "Set Password";
            this.btnSetPassword.UseVisualStyleBackColor = true;
            this.btnSetPassword.Click += new System.EventHandler(this.btnSetPassword_Click);
            // 
            // btnSetTimeZone
            // 
            this.btnSetTimeZone.Location = new System.Drawing.Point(55, 261);
            this.btnSetTimeZone.Name = "btnSetTimeZone";
            this.btnSetTimeZone.Size = new System.Drawing.Size(141, 23);
            this.btnSetTimeZone.TabIndex = 7;
            this.btnSetTimeZone.Text = "Set Time Zone";
            this.btnSetTimeZone.UseVisualStyleBackColor = true;
            this.btnSetTimeZone.Click += new System.EventHandler(this.btnSetTimeZone_Click);
            // 
            // btnCopyTaskFile
            // 
            this.btnCopyTaskFile.Location = new System.Drawing.Point(55, 312);
            this.btnCopyTaskFile.Name = "btnCopyTaskFile";
            this.btnCopyTaskFile.Size = new System.Drawing.Size(173, 23);
            this.btnCopyTaskFile.TabIndex = 8;
            this.btnCopyTaskFile.Text = "Copy Task File";
            this.btnCopyTaskFile.UseVisualStyleBackColor = true;
            this.btnCopyTaskFile.Click += new System.EventHandler(this.btnCopyTaskFile_Click);
            // 
            // btnScheduleTasks
            // 
            this.btnScheduleTasks.Location = new System.Drawing.Point(296, 49);
            this.btnScheduleTasks.Name = "btnScheduleTasks";
            this.btnScheduleTasks.Size = new System.Drawing.Size(177, 23);
            this.btnScheduleTasks.TabIndex = 9;
            this.btnScheduleTasks.Text = "Schedule Tasks";
            this.btnScheduleTasks.UseVisualStyleBackColor = true;
            this.btnScheduleTasks.Click += new System.EventHandler(this.btnScheduleTasks_Click);
            // 
            // btnSetDNSWiFi
            // 
            this.btnSetDNSWiFi.Location = new System.Drawing.Point(159, 210);
            this.btnSetDNSWiFi.Name = "btnSetDNSWiFi";
            this.btnSetDNSWiFi.Size = new System.Drawing.Size(185, 23);
            this.btnSetDNSWiFi.TabIndex = 10;
            this.btnSetDNSWiFi.Text = "Set DNS WiFi";
            this.btnSetDNSWiFi.UseVisualStyleBackColor = true;
            this.btnSetDNSWiFi.Click += new System.EventHandler(this.btnSetDNSWiFi_Click);
            // 
            // cmbPlant
            // 
            this.cmbPlant.FormattingEnabled = true;
            this.cmbPlant.Items.AddRange(new object[] {
            "Minneapolis",
            "Bedford",
            "Middletown",
            "Humboldt"});
            this.cmbPlant.Location = new System.Drawing.Point(375, 277);
            this.cmbPlant.Name = "cmbPlant";
            this.cmbPlant.Size = new System.Drawing.Size(121, 21);
            this.cmbPlant.TabIndex = 11;
            this.cmbPlant.ValueMemberChanged += new System.EventHandler(this.cmbPlant_ValueMemberChanged);
            this.cmbPlant.SelectedValueChanged += new System.EventHandler(this.cmbPlant_SelectedValueChanged);
            // 
            // txtNewName
            // 
            this.txtNewName.Location = new System.Drawing.Point(373, 168);
            this.txtNewName.Name = "txtNewName";
            this.txtNewName.Size = new System.Drawing.Size(100, 20);
            this.txtNewName.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(375, 149);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(88, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Enter New Name";
            // 
            // btnNewName
            // 
            this.btnNewName.Location = new System.Drawing.Point(378, 195);
            this.btnNewName.Name = "btnNewName";
            this.btnNewName.Size = new System.Drawing.Size(75, 23);
            this.btnNewName.TabIndex = 14;
            this.btnNewName.Text = "Set Name";
            this.btnNewName.UseVisualStyleBackColor = true;
            this.btnNewName.Click += new System.EventHandler(this.btnNewName_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(561, 395);
            this.Controls.Add(this.btnNewName);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtNewName);
            this.Controls.Add(this.cmbPlant);
            this.Controls.Add(this.btnSetDNSWiFi);
            this.Controls.Add(this.btnScheduleTasks);
            this.Controls.Add(this.btnCopyTaskFile);
            this.Controls.Add(this.btnSetTimeZone);
            this.Controls.Add(this.btnSetPassword);
            this.Controls.Add(this.btnSetDNSServer);
            this.Controls.Add(this.txtDNSServer);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Name = "Form1";
            this.Text = "Spark Setup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.TextBox txtDNSServer;
        private System.Windows.Forms.Button btnSetDNSServer;
        private System.Windows.Forms.Button btnSetPassword;
        private System.Windows.Forms.Button btnSetTimeZone;
        private System.Windows.Forms.Button btnCopyTaskFile;
        private System.Windows.Forms.Button btnScheduleTasks;
        private System.Windows.Forms.Button btnSetDNSWiFi;
        private System.Windows.Forms.ComboBox cmbPlant;
        private System.Windows.Forms.TextBox txtNewName;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnNewName;
    }
}

