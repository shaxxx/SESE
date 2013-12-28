// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;
using Krkadoni.SESE.Properties;


namespace Krkadoni.SESE
{
    // ReSharper disable once LocalizableElement
    [XmlTypeAttribute(AnonymousType = true, TypeName = "PositionTransform")]
    public class PositionTransform : INotifyPropertyChanged, IEditableObject
    {
        private int _originalPosition;
        private int _destination;

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
        private int _mOriginalPosition;
        private int _mDestination;

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mOriginalPosition = _originalPosition;
            _mDestination = _destination;
            _isEditing = true;
        }

        public void EndEdit()
        {
            _isEditing = false;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;
            OriginalPosition = _mOriginalPosition;
            Destination = _mDestination;
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
                return errorText;
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

        private bool IsOriginalPositionValid(int value)
        {
            if (!ValidPosition(value) || value == Destination)
            {
                AddError("OriginalPosition", _invalidValue);
                return false;
            }
            RemoveError("OriginalPosition", _invalidValue);
            return true;
        }

        private bool IsDestinationValid(int value)
        {
            if (!ValidPosition(value) || value == OriginalPosition)
            {
                AddError("Destination", _invalidValue);
                return false;
            }
            RemoveError("Destination", _invalidValue);
            return true;
        }

        private bool ValidateAll()
        {
            bool isValid = true;
            if (!IsOriginalPositionValid(OriginalPosition))
                isValid = false;
            if (!IsDestinationValid(Destination))
                isValid = false;
            return isValid;
        }

        #endregion

        #endregion

        public PositionTransform(int originalPosition, int destination)
        {
            OriginalPosition = originalPosition;
            Destination = destination;
        }

        public PositionTransform()
        {

        }

        [XmlElementAttribute]
        public int OriginalPosition
        {
            get { return _originalPosition; }
            set
            {
                _originalPosition = value;
                IsOriginalPositionValid(value);
                OnPropertyChanged("OriginalPosition");
                OnPropertyChanged("Display");
            }
        }

        [XmlElementAttribute]
        public int Destination
        {
            get { return _destination; }
            set
            {
                _destination = value;
                IsDestinationValid(value);
                OnPropertyChanged("Destination");
                OnPropertyChanged("Display");
            }
        }

        [XmlIgnore]
        public string Display
        {
            get
            {
                return string.Join(" --> ", new [] {PositionString(OriginalPosition), PositionString(Destination)});
            }
        }

        private bool ValidPosition(int value)
        {
            if (value < 0 && value > -1800)
                return true;
            if (value > 0 && value < 1800)
                return true;
            return false;
        }

        private string PositionString(int position)
        {
            var pos = Math.Abs(position).ToString(CultureInfo.InvariantCulture);
            if (pos.EndsWith("0"))
            {
                pos = pos.Substring(0, pos.Length - 1);
            }
            else
            {
                pos = pos.Substring(0, pos.Length - 1) + "." + pos.Substring(pos.Length - 1);
                if (pos.StartsWith("."))
                    pos = "0" + pos;
            }
            if (position < 0)
            {
                return pos + "° W";
            }
            return pos + "° E";
        }

    }
}

