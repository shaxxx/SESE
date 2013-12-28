// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;
using Krkadoni.SESE.Properties;


namespace Krkadoni.SESE
{
    public partial class ProfilesOptionsPage : OptionsPage
    {
        private readonly ErrorProvider _erp;
        private readonly BindingSource _bsProfiles;

        public ProfilesOptionsPage()
        {
            InitializeComponent();
            _bsProfiles = new BindingSource(AppSettings.DefInstance.Profiles, "");
            _bsProfiles.DataSource = AppSettings.DefInstance.Profiles;
            _bsProfiles.CurrentChanged += _bsProfiles_CurrentChanged;
            _erp = new ErrorProvider(this);
            _erp.ContainerControl = this;
            lbProfiles.DataSource = _bsProfiles;
            txtProfileName.DataBindings.Add(new Binding("Text", _bsProfiles, "Name", true, DataSourceUpdateMode.OnPropertyChanged));
            txtUsername.DataBindings.Add(new Binding("Text", _bsProfiles, "Username", true, DataSourceUpdateMode.OnPropertyChanged));
            txtPassword.DataBindings.Add(new Binding("Text", _bsProfiles, "PasswordDecrypted", true, DataSourceUpdateMode.OnPropertyChanged));
            cbEnigma.DataBindings.Add(new Binding("SelectedItem", _bsProfiles, "Enigma", true, DataSourceUpdateMode.OnPropertyChanged));
            txtSatellites.DataBindings.Add(new Binding("Text", _bsProfiles, "SatellitesFolder", true, DataSourceUpdateMode.OnPropertyChanged));
            txtServices.DataBindings.Add(new Binding("Text", _bsProfiles, "ServicesFolder", true, DataSourceUpdateMode.OnPropertyChanged));
            txtHttpPort.DataBindings.Add(new Binding("Text", _bsProfiles, "Port", false, DataSourceUpdateMode.OnPropertyChanged));
            txtSshPort.DataBindings.Add(new Binding("Text", _bsProfiles, "SSHPort", false, DataSourceUpdateMode.OnPropertyChanged));
            txtFtpPort.DataBindings.Add(new Binding("Text", _bsProfiles, "FTPPort", false, DataSourceUpdateMode.OnPropertyChanged));
            txtAddress.DataBindings.Add(new Binding("Text", _bsProfiles, "Address", true, DataSourceUpdateMode.OnPropertyChanged));
            ceDefault.DataBindings.Add(new Binding("Checked", _bsProfiles, "Preferred", true, DataSourceUpdateMode.OnPropertyChanged));
            panelProfile.Validating += panelProfile_Validating;
            txtProfileName.Validating += txtProfileName_Validating;
            txtUsername.Validating += txtUsername_Validating;
            txtPassword.Validating += txtPassword_Validating;
            txtSatellites.Validating += txtSatellites_Validating;
            txtServices.Validating += txtServices_Validating;
            txtHttpPort.Validating += txtHttpPort_Validating;
            txtSshPort.Validating += txtSshPort_Validating;
            txtFtpPort.Validating += txtFtpPort_Validating;
            txtAddress.Validating += txtAddress_Validating;
            txtHttpPort.DataBindings[0].Parse += Port_Parse;
            txtSshPort.DataBindings[0].Parse += Port_Parse;
            txtFtpPort.DataBindings[0].Parse += Port_Parse;
            cbEnigma.DataBindings[0].Parse += Enigma_Parse;
            cbEnigma.DataBindings[0].Format += Enigma_Format;
            btnDelete.Enabled = (lbProfiles.SelectedItem != null);
            panelProfile.Enabled = (lbProfiles.SelectedItem != null);
        }

        private void _bsProfiles_CurrentChanged(object sender, EventArgs e)
        {
            var currentProfile = (Profile)lbProfiles.SelectedItem;
            cbEnigma.SelectedIndex = (currentProfile != null && currentProfile.Enigma == 1) ? 0 : 1;
            btnDelete.Enabled = (lbProfiles.SelectedItem != null);
            panelProfile.Enabled = (lbProfiles.SelectedItem != null);
        }

        private static void Enigma_Format(object sender, ConvertEventArgs e)
        {
            if (e.Value == null || sbyte.Parse(e.Value.ToString()) == 0)
                e.Value = "Enigma 2"; //if enigma type is not set default to Enigma 2
            else if (sbyte.Parse(e.Value.ToString()) == 1)
                e.Value = "Enigma 1"; //Enigma 1 is on SelectedIndex 0
            else if (sbyte.Parse(e.Value.ToString()) == 2)
                e.Value = "Enigma 2"; //Enigma 2 is on SelectedIndex 1
        }

        private static void Enigma_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value == null)
                e.Value = 2;
            else if (e.Value.ToString() == "Enigma 1")
                e.Value = 1;
            else
                e.Value = 2;
        }

        private static void Port_Parse(object sender, ConvertEventArgs e)
        {
            if (e.Value == null || e.Value.ToString().Length == 0)
                e.Value = 0;
        }

        private void panelProfile_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                e.Cancel = ((Profile)_bsProfiles.Current).Error.Length > 0;
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            Profile newProfile = new Profile();
            var newProfileName = "Profile (";
            var existingIndex = 0;
            var count = AppSettings.DefInstance.Profiles.Count(x => x.Name.ToLower().StartsWith(newProfileName.ToLower()));
            newProfile.Name = "Profile (" + (count + 1 + existingIndex).ToString(CultureInfo.InvariantCulture) + ")";
            while (AppSettings.DefInstance.Profiles.Any(x => x.Name.ToLower() == newProfile.Name.ToLower().ToString(CultureInfo.InvariantCulture)))
            {
                existingIndex += 1;
                newProfile.Name = "Profile (" + (count + 1 + existingIndex).ToString(CultureInfo.InvariantCulture) + ")";
            }

            AppSettings.DefInstance.Profiles.Add(newProfile);
            btnDelete.Enabled = (lbProfiles.SelectedItem != null);
            panelProfile.Enabled = (lbProfiles.SelectedItem != null);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            AppSettings.DefInstance.Profiles.Remove((Profile)lbProfiles.SelectedItem);
            btnDelete.Enabled = (lbProfiles.SelectedItem != null);
            panelProfile.Enabled = (lbProfiles.SelectedItem != null);
        }

        private void ceDefault_CheckedChanged(object sender, EventArgs e)
        {
            if (ceDefault.Checked)
                AppSettings.DefInstance.Profiles.Where(x => !x.Equals(lbProfiles.SelectedItem)).ToList().ForEach(x => x.Preferred = false);
        }

        private void cbEnigma_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lbProfiles.SelectedItem == null)
                return;
            var currentProfile = (Profile)lbProfiles.SelectedItem;
            currentProfile.Enigma = cbEnigma.SelectedIndex == 0 ? sbyte.Parse("1") : sbyte.Parse("2");
        }

        #region "Validation Handlers"


        void txtAddress_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["Address"];
                _erp.SetError(txtAddress, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtAddress, string.Empty);
            }
        }

        void txtFtpPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["FTPPort"];
                _erp.SetError(txtFtpPort, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtFtpPort, string.Empty);
            }
        }

        void txtSshPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["SSHPort"];
                _erp.SetError(txtSshPort, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtSshPort, string.Empty);
            }
        }

        void txtHttpPort_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["Port"];
                _erp.SetError(txtHttpPort, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtHttpPort, string.Empty);
            }
        }

        void txtServices_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["ServicesFolder"];
                _erp.SetError(txtServices, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtServices, string.Empty);
            }
        }

        void txtSatellites_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["SatellitesFolder"];
                _erp.SetError(txtSatellites, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtSatellites, string.Empty);
            }
        }

        void txtPassword_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["PasswordDecrypted"];
                _erp.SetError(txtPassword, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtPassword, string.Empty);
            }
        }

        void txtUsername_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["Username"];
                _erp.SetError(txtUsername, err);
                e.Cancel = err.Length > 0;
            }
            else
            {
                _erp.SetError(txtUsername, string.Empty);
            }
        }

        void txtProfileName_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (_bsProfiles.Current != null)
            {
                var err = ((Profile)_bsProfiles.Current)["Name"];
                _erp.SetError(txtProfileName, err);
                if (err.Length > 0)
                    e.Cancel = true;
                if (AppSettings.DefInstance.Profiles.Count(x => x.Name.ToLower() == txtProfileName.Text.ToLower()) <= 1) return;
                e.Cancel = true;
                _erp.SetError(txtProfileName, Resources.Profile_invalidValue_Invalid_value_);
            }
            else
            {
                _erp.SetError(txtProfileName, string.Empty);
            }
        }

        #endregion

    }
}
