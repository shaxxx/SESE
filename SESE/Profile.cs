// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using Krkadoni.SESE.Properties;

namespace Krkadoni.SESE
{

    // ReSharper disable once LocalizableElement
    [SerializableAttribute, DesignerCategoryAttribute("code"),
        XmlTypeAttribute(AnonymousType = true, TypeName = "Profile")]
    public class Profile : INotifyPropertyChanged, IEditableObject, IDataErrorInfo
    {

        #region "INotifyPropertyChanged"

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #region "IEditable"

        private bool _isEditing;
        private string _mName;
        private int _mEnigma;
        private string _mUsername;
        private string _mPassword;
        private string _mAddress;
        private Int32 _mPort;
        private Int32 _mSshPort;
        private Int32 _mFtpPort;
        private bool _mPreferred;
        private string _mServicesFolder;
        private string _mSatellitesFolder;

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mName = _name;
            _mEnigma = _enigma;
            _mUsername = _username;
            _mPassword = _password;
            _mAddress = _address;
            _mPort = _port;
            _mSshPort = _sshPort;
            _mFtpPort = _ftpPort;
            _mPreferred = _preferred;
            _mServicesFolder = _servicesFolder;
            _mSatellitesFolder = _satellitesFolder;
            _isEditing = true;
        }

        public void EndEdit()
        {
           if (Error.Length > 0) 
               CancelEdit();
            _isEditing = false;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;
            Name = _mName;
            Enigma = _mEnigma;
            Username = _mUsername;
            Password = _mPassword;
            Address = _mAddress;
            Port = _mPort;
            SSHPort = _mSshPort;
            FTPPort = _mFtpPort;
            Preferred = _mPreferred;
            ServicesFolder = _mServicesFolder;
            SatellitesFolder = _mSatellitesFolder;
            _isEditing = false;
        }

        #endregion

        #region "IDataErrorInfo"

        private readonly Dictionary<string, List<string>> _errors = new Dictionary<string, List<string>>();
        private readonly string _invalidValue = Resources.Profile_invalidValue_Invalid_value_;

        [XmlIgnore]
        public string this[string columnName]
        {
            get
            {
                if (_errors.ContainsKey(columnName))
                {
                    return string.Join("\n", _errors[columnName].ToArray()).Trim();
                }
                return string.Empty;
            }
        }

        [XmlIgnore]
        public string Error
        {
            get
            {
                if (ValidateAll()) return string.Empty;
                var errorText = string.Empty;
                foreach (string prop in _errors.Keys)
                {
                    errorText += "\n" + this[prop];
                }
                return errorText.Trim();
            }
        }

        private void AddError(string propertyName, string error)
        {
            if (!_errors.ContainsKey(propertyName))
            {
                _errors.Add(propertyName, new List<string>());
            }
            if (!_errors[propertyName].Contains(error))
            {
                _errors[propertyName].Insert(0, error);
            }
        }

        private void RemoveError(string propertyName, string error)
        {
            if (_errors.ContainsKey(propertyName) && _errors[propertyName].Contains(error))
            {
                _errors[propertyName].Remove(error);
                if (_errors[propertyName].Count == 0)
                    _errors.Remove(propertyName);
            }
        }

        #region "Validators"

        private bool IsAddressValid(string value)
        {
            const string validIpAddressRegex = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            const string validHostnameRegex = @"^(([a-zA-Z0-9]|[a-zA-Z0-9][a-zA-Z0-9\-]*[a-zA-Z0-9])\.)*([A-Za-z0-9]|[A-Za-z0-9][A-Za-z0-9\-]*[A-Za-z0-9])$";
            if (Regex.IsMatch(value, validIpAddressRegex) || Regex.IsMatch(value, validHostnameRegex))
            {
                RemoveError("Address", _invalidValue);
                return true;
            }
            AddError("Address", _invalidValue);
            return false;
        }

        private bool IsEnigmaValid(int value)
        {
            if (0 < value && value < 3)
            {
                RemoveError("Enigma", _invalidValue);
                return true;
            }
            AddError("Enigma", _invalidValue);
            return false;
        }

        private bool IsFTPPortValid(Int32 value)
        {

            if (value < 1 || value > 65536)
            {
                AddError("FTPPort", _invalidValue);
                return false;
            }

            if (value == SSHPort)
            {
                AddError("FTPPort", _invalidValue);
                return false;
            }

            if (value == Port)
            {
                AddError("FTPPort", _invalidValue);
                return false;
            }

            RemoveError("FTPPort", _invalidValue);
            return true;
        }

        private bool IsNameValid(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                RemoveError("Name", _invalidValue);
                return true;
            }
            AddError("Name", _invalidValue);
            return false;
        }

        private bool IsPasswordValid(string value)
        {
            if (value != null)
            {
                RemoveError("Password", _invalidValue);
                return true;
            }
            AddError("Password", _invalidValue);
            return false;
        }

        private bool IsPortValid(Int32 value)
        {
            if (value < 1 || value > 65536)
            {
                AddError("Port", _invalidValue);
                return false;
            }

            if (value == SSHPort)
            {
                AddError("Port", _invalidValue);
                return false;
            }

            if (value == FTPPort)
            {
                AddError("Port", _invalidValue);
                return false;
            }

            RemoveError("Port", _invalidValue);
            return true;

        }

        private bool IsSatellitesFolderValid(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                RemoveError("SatellitesFolder", _invalidValue);
                return true;
            }
            AddError("SatellitesFolder", _invalidValue);
            return false;
        }

        private bool IsServicesFolderValid(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                RemoveError("ServicesFolder", _invalidValue);
                return true;
            }
            AddError("ServicesFolder", _invalidValue);
            return false;
        }

        private bool IsSshPortValid(Int32 value)
        {
            if (value < 1 || value > 65536)
            {
                AddError("SSHPort", _invalidValue);
                return false;
            }

            if (value == FTPPort)
            {
                AddError("SSHPort", _invalidValue);
                return false;
            }

            if (value == Port)
            {
                AddError("SSHPort", _invalidValue);
                return false;
            }

            RemoveError("SSHPort", _invalidValue);
            return true;
        }

        private bool IsUsernameValid(string value)
        {
            if (!String.IsNullOrEmpty(value))
            {
                RemoveError("Username", _invalidValue);
                return true;
            }
            AddError("Username", _invalidValue);
            return false;
        }

        private bool ValidateAll()
        {
            bool isValid = true;
            if (!IsAddressValid(Address))
                isValid = false;
            if (!IsEnigmaValid(Enigma))
                isValid = false;
            if (!IsFTPPortValid(FTPPort))
                isValid = false;
            if (!IsNameValid(Name))
                isValid = false;
            if (!IsPasswordValid(Password))
                isValid = false;
            if (!IsPortValid(Port))
                isValid = false;
            if (!IsSatellitesFolderValid(SatellitesFolder))
                isValid = false;
            if (!IsServicesFolderValid(ServicesFolder))
                isValid = false;
            if (!IsSshPortValid(SSHPort))
                isValid = false;
            if (!IsUsernameValid(Username))
                isValid = false;
            return isValid;
        }

        #endregion

        #endregion

        private string _name = string.Empty;
        private int _enigma = 2;
        private string _username = "root";
        private string _password = "EAAAAEA2+ubugRDT3OhHH6cdz2UmqA5R1w85inm0HYrwKK9X";
        private string _address = "192.168.1.1";
        private Int32 _port = 80;
        private Int32 _sshPort = 22;
        private Int32 _ftpPort = 21;
        private bool _preferred;
        private string _servicesFolder = "/etc/enigma2/";
        private string _satellitesFolder = "/etc/tuxbox/";
        private const string SharedSecret = "SESE!";

        [XmlElementAttribute]
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value))
                    return;
                _name = value;
                IsNameValid(value);
                OnPropertyChanged("Name");
            }
        }

        [XmlElementAttribute]
        public int Enigma
        {
            get { return _enigma; }
            set
            {
                if (Equals(_enigma, value))
                    return;
                _enigma = value;
                IsEnigmaValid(value);
                OnPropertyChanged("Enigma");
                if (_enigma == 1)
                {
                    ServicesFolder = "/var/tuxbox/config/enigma/";
                    SatellitesFolder = "/var/etc/";
                }
                else
                {
                    ServicesFolder = "/etc/enigma2/";
                    SatellitesFolder = "/etc/tuxbox/";
                }
            }
        }

        [XmlElementAttribute]
        public string Username
        {
            get { return _username; }
            set
            {
                if (Equals(_username, value))
                    return;
                _username = value;
                IsUsernameValid(value);
                OnPropertyChanged("Username");
            }
        }

        [XmlElementAttribute]
        public string Password
        {
            get { return _password; }
            set
            {
                if (Equals(_password, value))
                    return;
                _password = value;
                IsPasswordValid(PasswordDecrypted);
                OnPropertyChanged("Password");
                OnPropertyChanged("PasswordDecrypted");
            }
        }

        [XmlIgnore]
        public string PasswordDecrypted
        {
            get
            {
                return string.IsNullOrEmpty(_password) ? String.Empty : Crypto.DecryptStringAES(_password, SharedSecret);
            }
            set
            {
                if (value == null)
                    value = string.Empty;
                String passwordEncrypted = string.Empty;
                if (value.Length != 0)
                passwordEncrypted = Crypto.EncryptStringAES(value, SharedSecret);
                if (Equals(passwordEncrypted, _password))
                    return;
                _password = passwordEncrypted;
                IsPasswordValid(PasswordDecrypted);
                OnPropertyChanged("Password");
                OnPropertyChanged("PasswordDecrypted");
            }
        }

        [XmlElementAttribute]
        public string Address
        {
            get { return _address; }
            set
            {
                if (Equals(_address, value))
                    return;
                _address = value;
                IsAddressValid(value);
                OnPropertyChanged("Address");
            }
        }

        [XmlElementAttribute]
        public Int32 Port
        {
            get { return _port; }
            set
            {
                if (Equals(_port, value))
                    return;
                _port = value;
                IsPortValid(value);
                OnPropertyChanged("Port");
            }
        }

        [XmlElementAttribute]
        public Int32 SSHPort
        {
            get { return _sshPort; }
            set
            {
                if (Equals(_sshPort, value))
                    return;
                _sshPort = value;
                IsSshPortValid(value);
                OnPropertyChanged("SSHPort");
            }
        }

        [XmlElementAttribute]
        public Int32 FTPPort
        {
            get { return _ftpPort; }
            set
            {
                if (Equals(_ftpPort, value))
                    return;
                _ftpPort = value;
                IsFTPPortValid(value);
                OnPropertyChanged("FTPPort");
            }
        }

        [XmlElementAttribute]
        public bool Preferred
        {
            get { return _preferred; }
            set
            {
                if (_preferred == value)
                    return;
                _preferred = value;
                OnPropertyChanged("Preferred");
            }
        }

        [XmlElementAttribute]
        public string ServicesFolder
        {
            get { return _servicesFolder; }
            set
            {
                if (Equals(_servicesFolder, value))
                    return;
                _servicesFolder = value;
                IsServicesFolderValid(value);
                OnPropertyChanged("ServicesFolder");
            }
        }

        [XmlElementAttribute]
        public string SatellitesFolder
        {
            get { return _satellitesFolder; }
            set
            {
                if (Equals(_satellitesFolder, value))
                    return;
                _satellitesFolder = value;
                IsSatellitesFolderValid(value);
                OnPropertyChanged("SatellitesFolder");
            }
        }

    }
}
