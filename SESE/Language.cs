// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System.ComponentModel;
using System.Globalization;


namespace Krkadoni.SESE
{
    // ReSharper disable once LocalizableElement
    [System.SerializableAttribute, DesignerCategoryAttribute("code"),
        System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, TypeName = "Language")]
    public class Language :INotifyPropertyChanged, IEditableObject
    {
        private string _name;
        private string _culture;
        private string _author;

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
        private string _mCulture;
        private string _mAuthor;

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mName = _name;
            _mCulture = _culture;
            _mAuthor = _author;
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
            Culture = _mCulture;
            Author = _mAuthor;
            _isEditing = false;
        }

        #endregion

        public Language()
        {

        }

        public Language(string name, string culture, string author)
        {
            Name = name;
            Culture = culture;
            Author = author;
        }

        [System.Xml.Serialization.XmlElementAttribute]
        public string Name
        {
            get { return _name; }
            set
            {
                if (Equals(_name, value)) 
                    return;
                _name = value;
                OnPropertyChanged("Name");
            }
        }

        [System.Xml.Serialization.XmlElementAttribute]
        public string Culture
        {
            get { return _culture; }
            set
            {
                if (Equals(_culture, value))
                    return;
                _culture = value;
                OnPropertyChanged("Culture");
            }
        }

        [System.Xml.Serialization.XmlElementAttribute]
        public string Author
        {
            get { return _author; }
            set
            {
                if (Equals(_author, value))
                    return;
                _author = value;
                OnPropertyChanged("Author");
            }
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
