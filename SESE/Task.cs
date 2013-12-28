// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Krkadoni.SESE.Properties;

namespace Krkadoni.SESE
{

    [System.SerializableAttribute,
        // ReSharper disable once LocalizableElement
    DesignerCategoryAttribute("code"),
    XmlTypeAttribute(AnonymousType = true, TypeName = "Task")]
    public class Task : INotifyPropertyChanged, IEditableObject
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
        private string[] _mPositions;
        private string _mDirectory;
        private string _mFileName;
        private bool _mZip;
        private bool _mDvbt;
        private bool _mDvbc;
        private bool _mStreams;

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mName = _name;
            _mPositions = _positions;
            _mDirectory = _directory;
            _mFileName = _fileName;
            _mZip = _zip;
            _mDvbt = _dvbt;
            _mDvbc = _dvbc;
            _mStreams = _streams;
            _isEditing = true;
        }

        public void EndEdit()
        {
            _isEditing = false;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;
            Name = _mName;
            Positions = _mPositions;
            Directory = _mDirectory;
            FileName = _mFileName;
            Zip = _mZip;
            DVBT = _mDvbt;
            DVBC = _mDvbc;
            Streams = _mStreams;
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

        private bool IsNameValid(string value)
        {

            RemoveError("Address", Resources.WARNING_INVALID_TASKNAME);
            RemoveError("Address", Resources.WARNING_TASKNAME_EXISTS);

            if (string.IsNullOrEmpty(value))
            {
                AddError("Address", Resources.WARNING_INVALID_TASKNAME);
                return false;
            }

            if (AppSettings.DefInstance.Tasks.Any(x =>
                string.Equals(x.Name, Name, StringComparison.InvariantCultureIgnoreCase)
                    && x.GetHashCode() != this.GetHashCode()))
            {
                AddError("Address", Resources.WARNING_TASKNAME_EXISTS);
                return false;
            }

            return true;
        }

        private bool IsPositionsValid(string[] value)
        {

            RemoveError("Positions", Resources.WARNING_SAME_SATELLITES);
            RemoveError("Positions", Resources.WARNING_NO_SAT_SELECTED);

            if (value == null || value.Length == 0)
            {
                AddError("Positions", Resources.WARNING_NO_SAT_SELECTED);
                return false;
            }

            var existingSat = AppSettings.DefInstance.Tasks.FirstOrDefault(x =>
                string.Equals(string.Join(",", x.Positions), string.Join(",", Positions), StringComparison.InvariantCultureIgnoreCase)
                && x.GetHashCode() != this.GetHashCode());

            if (existingSat != null)
            {
                AddError("Positions", String.Format(Resources.WARNING_SAME_SATELLITES,existingSat.Name));
                return false;
            }

            return true;
        }

        private bool IsDirectoryValid(string value)
        {

            string sameDirectory = String.Format(Resources.WARNING_SAME_OUTFOLDER1 + "{0}" + Resources.WARNING_SAME_OUTFOLDER2, "\n");
            RemoveError("Directory", sameDirectory);
            RemoveError("Directory", Resources.WARNING_OUTDIR_NOTEXIST);

            if (string.IsNullOrEmpty(value))
            {
                AddError("Directory", Resources.WARNING_OUTDIR_NOTEXIST);
                return false;
            }

            try
            {
                //make sure it's not some invalid string
                System.IO.Path.GetDirectoryName(value);
            }
            catch (Exception)
            {
                AddError("Directory", Resources.WARNING_OUTDIR_NOTEXIST);
                return false;
            }

            if (!System.IO.Directory.Exists(value))
            {
                AddError("Directory", Resources.WARNING_OUTDIR_NOTEXIST);
                return false;
            }

            if (Zip) return true;

            if (AppSettings.DefInstance.Tasks.Any(x =>
                string.Equals(x.Directory, Directory, StringComparison.InvariantCultureIgnoreCase)
                && x.GetHashCode() != this.GetHashCode()))
            {
                AddError("Directory", sameDirectory);
                return false;
            }

            return true;
        }

        private bool IsFileNameValid(string value)
        {

            RemoveError("FileName", Resources.WARNING_INVALID_FILENAME);

            if (!Zip) return true;

            if (!string.IsNullOrEmpty(value) && value.IndexOfAny(Path.GetInvalidFileNameChars()) > -1)
            {
                AddError("FileName", Resources.WARNING_INVALID_FILENAME);
                return false;
            }
            
            if (AppSettings.DefInstance.Tasks.Any(x =>
                string.Equals(x.FileName, FileName, StringComparison.InvariantCultureIgnoreCase)
                && x.Zip
                && x.GetHashCode() != this.GetHashCode()))
            {
                AddError("FileName", Resources.WARNING_INVALID_FILENAME);
                return false;
            }

            return true;
        }

        private bool ValidateAll()
        {
            bool isValid = true;

            if (!IsNameValid(Name))
                isValid = false;

            if (!IsPositionsValid(Positions))
                isValid = false;

            if (!IsDirectoryValid(Directory))
                isValid = false;

            if (!IsFileNameValid(FileName))
                isValid = false;

            return isValid;
        }

        #endregion

        #endregion

        private string _name;
        private string[] _positions;
        private string _directory;
        private string _fileName;
        private bool _zip;
        private bool _dvbt;
        private bool _dvbc;
        private bool _streams;

        [XmlElement]
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
        public string[] Positions
        {
            get { return _positions; }
            set
            {
                if (Equals(_positions, value)) return;
                _positions = value;
                IsPositionsValid(value);
                OnPropertyChanged("Positions");
            }
        }

        [XmlElementAttribute]
        public string Directory
        {
            get { return _directory; }
            set
            {
                if (Equals(_directory, value)) return;
                _directory = value;
                IsDirectoryValid(value);
                OnPropertyChanged("Directory");
            }
        }

        [XmlElementAttribute]
        public string FileName
        {
            get { return _fileName; }
            set
            {
                if (Equals(_fileName, value)) return;
                var extension = Path.GetExtension(value);
                if (extension != null && extension.ToLower() != ".zip")
                {
                    value = value.TrimEnd('.') + ".zip";
                }
                _fileName = value;
                IsFileNameValid(value);
                OnPropertyChanged("FileName");
            }
        }

        [XmlElementAttribute]
        public bool Zip
        {
            get { return _zip; }
            set
            {
                if (_zip == value) return;
                _zip = value;
                IsFileNameValid(FileName);
                OnPropertyChanged("Zip");
            }
        }

        [XmlElementAttribute]
        public bool DVBT
        {
            get { return _dvbt; }
            set
            {
                if (_dvbt == value) return;
                _dvbt = value;
                OnPropertyChanged("DVBT");
            }
        }

        [XmlElementAttribute]
        public bool DVBC
        {
            get { return _dvbc; }
            set
            {
                if (_dvbc == value) return;
                _dvbc = value;
                OnPropertyChanged("DVBC");
            }
        }

        [XmlElementAttribute]
        public bool Streams
        {
            get { return _streams; }
            set
            {
                _streams = value;
                if (_streams == value) return;
                _streams = value;
                OnPropertyChanged("Streams");
            }
        }
    }
}
