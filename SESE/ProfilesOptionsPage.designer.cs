using System.Windows.Forms;

namespace Krkadoni.SESE
{
    partial class ProfilesOptionsPage
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProfilesOptionsPage));
            this.lbProfiles = new System.Windows.Forms.ListBox();
            this.lbProfileName = new System.Windows.Forms.Label();
            this.txtProfileName = new System.Windows.Forms.TextBox();
            this.lbUsername = new System.Windows.Forms.Label();
            this.txtUsername = new System.Windows.Forms.TextBox();
            this.lbPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cbEnigma = new System.Windows.Forms.ComboBox();
            this.lbEnigma = new System.Windows.Forms.Label();
            this.lbSatellites = new System.Windows.Forms.Label();
            this.txtSatellites = new System.Windows.Forms.TextBox();
            this.lbServices = new System.Windows.Forms.Label();
            this.txtServices = new System.Windows.Forms.TextBox();
            this.lbHttpPort = new System.Windows.Forms.Label();
            this.txtHttpPort = new Krkadoni.SESE.PortTextBox();
            this.lbSshPort = new System.Windows.Forms.Label();
            this.txtSshPort = new Krkadoni.SESE.PortTextBox();
            this.lbFtpPort = new System.Windows.Forms.Label();
            this.txtFtpPort = new Krkadoni.SESE.PortTextBox();
            this.lbAddress = new System.Windows.Forms.Label();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnDelete = new System.Windows.Forms.Button();
            this.ceDefault = new System.Windows.Forms.CheckBox();
            this.panelProfile = new System.Windows.Forms.Panel();
            this.panelProfile.SuspendLayout();
            this.SuspendLayout();
            // 
            // lbProfiles
            // 
            resources.ApplyResources(this.lbProfiles, "lbProfiles");
            this.lbProfiles.DisplayMember = "Name";
            this.lbProfiles.FormattingEnabled = true;
            this.lbProfiles.Name = "lbProfiles";
            this.lbProfiles.ValueMember = "Name";
            // 
            // lbProfileName
            // 
            resources.ApplyResources(this.lbProfileName, "lbProfileName");
            this.lbProfileName.Name = "lbProfileName";
            // 
            // txtProfileName
            // 
            resources.ApplyResources(this.txtProfileName, "txtProfileName");
            this.txtProfileName.Name = "txtProfileName";
            // 
            // lbUsername
            // 
            resources.ApplyResources(this.lbUsername, "lbUsername");
            this.lbUsername.Name = "lbUsername";
            // 
            // txtUsername
            // 
            resources.ApplyResources(this.txtUsername, "txtUsername");
            this.txtUsername.Name = "txtUsername";
            // 
            // lbPassword
            // 
            resources.ApplyResources(this.lbPassword, "lbPassword");
            this.lbPassword.Name = "lbPassword";
            // 
            // txtPassword
            // 
            resources.ApplyResources(this.txtPassword, "txtPassword");
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // cbEnigma
            // 
            resources.ApplyResources(this.cbEnigma, "cbEnigma");
            this.cbEnigma.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbEnigma.FormattingEnabled = true;
            this.cbEnigma.Items.AddRange(new object[] {
            resources.GetString("cbEnigma.Items"),
            resources.GetString("cbEnigma.Items1")});
            this.cbEnigma.Name = "cbEnigma";
            this.cbEnigma.SelectedIndexChanged += new System.EventHandler(this.cbEnigma_SelectedIndexChanged);
            // 
            // lbEnigma
            // 
            resources.ApplyResources(this.lbEnigma, "lbEnigma");
            this.lbEnigma.Name = "lbEnigma";
            // 
            // lbSatellites
            // 
            resources.ApplyResources(this.lbSatellites, "lbSatellites");
            this.lbSatellites.Name = "lbSatellites";
            // 
            // txtSatellites
            // 
            resources.ApplyResources(this.txtSatellites, "txtSatellites");
            this.txtSatellites.Name = "txtSatellites";
            // 
            // lbServices
            // 
            resources.ApplyResources(this.lbServices, "lbServices");
            this.lbServices.Name = "lbServices";
            // 
            // txtServices
            // 
            resources.ApplyResources(this.txtServices, "txtServices");
            this.txtServices.Name = "txtServices";
            // 
            // lbHttpPort
            // 
            resources.ApplyResources(this.lbHttpPort, "lbHttpPort");
            this.lbHttpPort.Name = "lbHttpPort";
            // 
            // txtHttpPort
            // 
            resources.ApplyResources(this.txtHttpPort, "txtHttpPort");
            this.txtHttpPort.Name = "txtHttpPort";
            // 
            // lbSshPort
            // 
            resources.ApplyResources(this.lbSshPort, "lbSshPort");
            this.lbSshPort.Name = "lbSshPort";
            // 
            // txtSshPort
            // 
            resources.ApplyResources(this.txtSshPort, "txtSshPort");
            this.txtSshPort.Name = "txtSshPort";
            // 
            // lbFtpPort
            // 
            resources.ApplyResources(this.lbFtpPort, "lbFtpPort");
            this.lbFtpPort.Name = "lbFtpPort";
            // 
            // txtFtpPort
            // 
            resources.ApplyResources(this.txtFtpPort, "txtFtpPort");
            this.txtFtpPort.Name = "txtFtpPort";
            // 
            // lbAddress
            // 
            resources.ApplyResources(this.lbAddress, "lbAddress");
            this.lbAddress.Name = "lbAddress";
            // 
            // txtAddress
            // 
            resources.ApplyResources(this.txtAddress, "txtAddress");
            this.txtAddress.Name = "txtAddress";
            // 
            // btnAdd
            // 
            resources.ApplyResources(this.btnAdd, "btnAdd");
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnDelete
            // 
            resources.ApplyResources(this.btnDelete, "btnDelete");
            this.btnDelete.Name = "btnDelete";
            this.btnDelete.UseVisualStyleBackColor = true;
            this.btnDelete.Click += new System.EventHandler(this.btnDelete_Click);
            // 
            // ceDefault
            // 
            resources.ApplyResources(this.ceDefault, "ceDefault");
            this.ceDefault.Name = "ceDefault";
            this.ceDefault.UseVisualStyleBackColor = true;
            this.ceDefault.CheckedChanged += new System.EventHandler(this.ceDefault_CheckedChanged);
            // 
            // panelProfile
            // 
            resources.ApplyResources(this.panelProfile, "panelProfile");
            this.panelProfile.Controls.Add(this.txtProfileName);
            this.panelProfile.Controls.Add(this.ceDefault);
            this.panelProfile.Controls.Add(this.lbProfileName);
            this.panelProfile.Controls.Add(this.txtHttpPort);
            this.panelProfile.Controls.Add(this.lbHttpPort);
            this.panelProfile.Controls.Add(this.lbSshPort);
            this.panelProfile.Controls.Add(this.txtServices);
            this.panelProfile.Controls.Add(this.lbUsername);
            this.panelProfile.Controls.Add(this.txtSshPort);
            this.panelProfile.Controls.Add(this.lbServices);
            this.panelProfile.Controls.Add(this.txtUsername);
            this.panelProfile.Controls.Add(this.lbFtpPort);
            this.panelProfile.Controls.Add(this.lbPassword);
            this.panelProfile.Controls.Add(this.txtSatellites);
            this.panelProfile.Controls.Add(this.txtFtpPort);
            this.panelProfile.Controls.Add(this.txtPassword);
            this.panelProfile.Controls.Add(this.lbAddress);
            this.panelProfile.Controls.Add(this.lbSatellites);
            this.panelProfile.Controls.Add(this.lbEnigma);
            this.panelProfile.Controls.Add(this.cbEnigma);
            this.panelProfile.Controls.Add(this.txtAddress);
            this.panelProfile.Name = "panelProfile";
            // 
            // ProfilesOptionsPage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panelProfile);
            this.Controls.Add(this.btnDelete);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.lbProfiles);
            this.Image = ((System.Drawing.Image)(resources.GetObject("$this.Image")));
            this.Name = "ProfilesOptionsPage";
            this.panelProfile.ResumeLayout(false);
            this.panelProfile.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lbProfiles;
        private System.Windows.Forms.Label lbProfileName;
        private System.Windows.Forms.TextBox txtProfileName;
        private System.Windows.Forms.Label lbUsername;
        private System.Windows.Forms.TextBox txtUsername;
        private System.Windows.Forms.Label lbPassword;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.ComboBox cbEnigma;
        private System.Windows.Forms.Label lbEnigma;
        private System.Windows.Forms.Label lbSatellites;
        private System.Windows.Forms.TextBox txtSatellites;
        private System.Windows.Forms.Label lbServices;
        private System.Windows.Forms.TextBox txtServices;
        private System.Windows.Forms.Label lbHttpPort;
        private PortTextBox txtHttpPort;
        private System.Windows.Forms.Label lbSshPort;
        private PortTextBox txtSshPort;
        private System.Windows.Forms.Label lbFtpPort;
        private PortTextBox txtFtpPort;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnDelete;
        private System.Windows.Forms.Label lbAddress;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.CheckBox ceDefault;
        private Panel panelProfile;
    }
}
