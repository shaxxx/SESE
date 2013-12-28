// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System.ComponentModel;

namespace Krkadoni.SESE
{
    class VmXmlSatellite : IEditableObject, INotifyPropertyChanged

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
        private bool _mSelected;

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mSelected = _selected;
            _isEditing = true;
        }

        public void EndEdit()
        {
            _isEditing = false;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;
            _selected = _mSelected;
            _isEditing = false;
        }

        #endregion

        private bool _selected;
       
        private readonly EnigmaSettings.Interfaces.IXmlSatellite _xmlSatellite;

        /// <summary>
        /// View Model to display list of satellites in grid
        /// </summary>
        /// <param name="xmlSatellite"></param>
        public VmXmlSatellite(EnigmaSettings.Interfaces.IXmlSatellite xmlSatellite)
        {
            _xmlSatellite = xmlSatellite;
        }

        /// <summary>
        ///  Determines if satellite is selected for processing
        /// </summary>
        /// <value></value>
        /// <returns>Should this satellite be selected for processing</returns>
        /// <remarks></remarks>
        public bool Selected
        {
            get
            {
                return _selected;
            }
            set
            {
                if (value != _selected)
                {
                    _selected = value;
                    OnPropertyChanged("Selected");
                }
            }
        }

        /// <summary>
        /// Name of the satellite displayed in the grid
        /// </summary>
        public string Name
        {
            get
            {
                return _xmlSatellite.Name;
            }
        }

        /// <summary>
        /// Positionstring of the satellite displayed in the grid
        /// </summary>
        public string PositionString
        {
            get
            {
                return _xmlSatellite.PositionString;
            }
        }

        /// <summary>
        /// Original XmlSatellite object
        /// </summary>
        public EnigmaSettings.Interfaces.IXmlSatellite XmlSatellite
        {
            get
            {
                return _xmlSatellite;
            }
        }

    }

}
