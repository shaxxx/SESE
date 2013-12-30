// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;
using Krkadoni.EnigmaSettings;
using log4net;

namespace Krkadoni.SESE
{
    [System.SerializableAttribute,
        // ReSharper disable once LocalizableElement
     DesignerCategory("code"),
     XmlType(AnonymousType = true),
     XmlRoot(IsNullable = false, ElementName = "Settings")]
    public class AppSettings : INotifyPropertyChanged
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
        private static Language _mCurrentLanguage;
        private static bool _mDonated;
        private static bool _mCheckUpdates;
        private static BindingList<string> _mDelimiters;
        private static BindingList<Task> _mTtasks = new BindingList<Task>();
        private static BindingList<Profile> _mProfiles = new BindingList<Profile>();
        private static BindingList<PositionTransform> _mMoveList = new BindingList<PositionTransform>();
        private static BindingList<PositionTransform> _mCopyList = new BindingList<PositionTransform>();
        private static BindingList<Language> _mLanguages = new BindingList<Language>();

        public void BeginEdit()
        {
            if (_isEditing) return;
            _mDonated = _donated;
            _mCheckUpdates = _checkUpdates;
            _mCurrentLanguage = _currentLanguage;
            _mDelimiters = _delimiters;
            _mProfiles.Clear();
            _mMoveList.Clear();
            _mCopyList.Clear();
            _mLanguages.Clear();
            foreach (Task task in Tasks)
            {
                _mTtasks.Add(task);
            }
            foreach (Profile profile in Profiles)
            {
                _mProfiles.Add(profile);
            }
            foreach (PositionTransform positionTransform in MoveList)
            {
                _mMoveList.Add(positionTransform);
            }
            foreach (PositionTransform positionTransform in CopyList)
            {
                _mCopyList.Add(positionTransform);
            }
            foreach (Language language in Languages)
            {
                _mLanguages.Add(language);
            }
            _isEditing = true;
        }

        public void EndEdit()
        {

            _mProfiles.Clear();
            _mMoveList.Clear();
            _mCopyList.Clear();
            _mLanguages.Clear();

            if (!_isEditing) return;
            foreach (Task task in Tasks)
            {
                task.EndEdit();
            }

            foreach (Profile profile in Profiles)
            {
                profile.EndEdit();
            }

            foreach (PositionTransform positionTransform in MoveList)
            {
                positionTransform.EndEdit();
            }

            foreach (PositionTransform positionTransform in CopyList)
            {
                positionTransform.EndEdit();
            }

            foreach (Language language in Languages)
            {
                language.EndEdit();
            }
            _isEditing = false;
        }

        public void CancelEdit()
        {
            if (!_isEditing) return;
            Donated = _mDonated;
            CheckUpdates = _mCheckUpdates;
            CurrentLanguage = _mCurrentLanguage;
            Delimiters = _mDelimiters;
            Tasks.Clear();
            foreach (Task task in _mTtasks)
            {
                Tasks.Add(task);
                task.CancelEdit();
            }

            Profiles.Clear();
            foreach (Profile profile in _mProfiles)
            {
                Profiles.Add(profile);
                profile.CancelEdit();
            }

            MoveList.Clear();
            foreach (PositionTransform positionTransform in _mMoveList)
            {
                MoveList.Add(positionTransform);
                positionTransform.CancelEdit();
            }

            CopyList.Clear();
            foreach (PositionTransform positionTransform in _mCopyList)
            {
                CopyList.Add(positionTransform);
                positionTransform.CancelEdit();
            }

            Languages.Clear();
            foreach (Language language in _mLanguages)
            {
                Languages.Add(language);
                language.CancelEdit();
            }

            _mProfiles.Clear();
            _mMoveList.Clear();
            _mCopyList.Clear();
            _mLanguages.Clear();
            _mTtasks.Clear();

            _isEditing = false;
        }

        #endregion

        private static Language _currentLanguage;
        private static bool _donated;
        private static bool _checkUpdates = true;
        private static ILog _log;

        /// <summary>
        ///     Log4Net instance used for logging
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        public static ILog Log
        {
            get { return _log ?? (_log = LogManager.GetLogger("SESE")); }
            set
            {
                if (value == null) return;
                _log = value;
            }
        }

        [XmlIgnore]
        public const string ReleaseName = "Streamer";

        [XmlIgnore]
        public const string BarTekst = "http://www.krkadoni.com";

        private static AppSettings _defInstance;
        private static BindingList<Task> _tasks;
        private static BindingList<Profile> _profiles;
        private static BindingList<string> _delimiters;
        private static BindingList<PositionTransform> _moveList;
        private static BindingList<PositionTransform> _copyList;
        private static BindingList<Language> _languages;

        public AppSettings()
        {
            if (_tasks == null)
                Tasks = new BindingList<Task>();
            if (_profiles == null)
                Profiles = new BindingList<Profile>();
            if (_delimiters == null)
                Delimiters = new BindingList<string>();
            if (_moveList == null)
                MoveList = new BindingList<PositionTransform>();
            if (_copyList == null)
                CopyList = new BindingList<PositionTransform>();
            if (_languages == null)
                _languages = new BindingList<Language>();
           _log = LogManager.GetLogger("SESE");
        }

        #region "Properties"

        [XmlIgnore]
        public static AppSettings DefInstance
        {
            get { return _defInstance ?? (_defInstance = new AppSettings()); }
            set { _defInstance = value; }
        }

        [XmlElementAttribute]
        public Language CurrentLanguage
        {
            get
            {
                if (_currentLanguage != null) return _currentLanguage;
                _currentLanguage = DetectLanguage();
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(_currentLanguage.Culture);
                return _currentLanguage;
            }
            set
            {
                if (Equals(_currentLanguage, value))
                    return;
                _currentLanguage = value;
                Thread.CurrentThread.CurrentUICulture = new CultureInfo(CurrentLanguage.Culture);
                OnPropertyChanged("CurrentLanguage");
            }
        }

        [XmlElementAttribute]
        public bool Donated
        {
            get { return _donated; }
            set
            {
                if (_donated == value)
                    return;
                _donated = value;
                OnPropertyChanged("Donated");
            }
        }

        [XmlElementAttribute]
        public bool CheckUpdates
        {
            get { return _checkUpdates; }
            set
            {
                if (_checkUpdates == value)
                    return;
                _checkUpdates = value;
                OnPropertyChanged("CheckUpdates");
            }
        }

        [XmlIgnore]
        public BindingList<Profile> Profiles
        {
            get { return _profiles; }
            set
            {
                if (Equals(_profiles, value))
                    return;
                _profiles = value;
                OnPropertyChanged("Profiles");
            }
        }

        [XmlArray]
        public BindingList<Task> Tasks
        {
            get { return _tasks; }
            set
            {
                if (Equals(_tasks, value))
                    return;
                _tasks = value;
                OnPropertyChanged("Tasks");
            }
        }

        [XmlArray]
        public BindingList<string> Delimiters
        {
            get { return _delimiters; }
            set
            {
                if (Equals(_delimiters, value))
                    return;
                _delimiters = value;
                OnPropertyChanged("Delimiters");
            }
        }

        [XmlIgnore]
        public BindingList<PositionTransform> MoveList
        {
            get { return _moveList; }
            set
            {
                if (Equals(_moveList, value))
                    return;
                _moveList = value;
                OnPropertyChanged("MoveList");
            }
        }

        [XmlIgnore]
        public BindingList<PositionTransform> CopyList
        {
            get { return _copyList; }
            set
            {
                if (Equals(_copyList, value))
                    return;
                _copyList = value;
                OnPropertyChanged("CopyList");
            }
        }

        [XmlIgnore]
        public BindingList<Language> Languages
        {
            get
            {
                if (_languages.Any()) return _languages;
                _languages = DefaultLanguages();
                return _languages;
            }
        }

        /// <summary>
        /// Application.ExecutablePath
        /// </summary>
        /// <value></value>
        /// <returns></returns>
        /// <remarks></remarks>
        [XmlIgnore]
        public static string CurrentDir
        {
            get { return Path.GetDirectoryName(Application.ExecutablePath); }
        }

        [XmlIgnore]
        public static string AppDataDir
        {
            get { return CurrentDir; }
        }

        public static bool IsRunningOnMono()
        {
            return Type.GetType("Mono.Runtime") != null;
        }

        #endregion

        #region "Serialize/Deserialize"

        private static XmlSerializer _sSerializer;

        private static XmlSerializer Serializer
        {
            get { return _sSerializer ?? (_sSerializer = new XmlSerializer(typeof(AppSettings))); }
        }

        protected static string Serialize()
        {
            StreamReader streamReader = null;
            MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                Serializer.Serialize(memoryStream, DefInstance);
                memoryStream.Seek(0, SeekOrigin.Begin);
                streamReader = new StreamReader(memoryStream);
                return streamReader.ReadToEnd();
            }
            finally
            {
                if (streamReader != null)
                {
                    streamReader.Dispose();
                }
                if (memoryStream != null)
                {
                    memoryStream.Dispose();
                }
            }
        }

        protected static AppSettings Deserialize(string xml)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(xml);
                var instance = (AppSettings)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
                _defInstance = instance;
                return instance;
            }
            finally
            {
                if (stringReader != null)
                {
                    stringReader.Dispose();
                }
            }
        }

        private static void Save(string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                Log.DebugFormat("Saving AppSettings to file {0}",fileName);
                string xmlString = Serialize();
                Log.DebugFormat("AppSettings content serialized as:{0}{1}", Environment.NewLine, xmlString);
                var xmlFile = new FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
                Log.DebugFormat("AppSettings saved to {0}", fileName);
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Dispose();
                }
            }
        }

        public static void Save()
        {
            Log.Debug("Saving AppSettings data");
            ProfileSerialize.DefInstance.Items = _profiles;
            ProfileSerialize.Save();
            MoveSerialize.DefInstance.Items = _moveList;
            MoveSerialize.Save();
            CopySerialize.DefInstance.Items = _copyList;
            CopySerialize.Save();
            Save(Path.Combine(AppDataDir, "settings.xml"));
            Log.Debug("All AppSettings data sucessfully saved.");
        }

        private static AppSettings Load(string fileName)
        {
            Log.Debug("Loading AppSettings");
            if (System.IO.File.Exists(fileName))
            {
                Log.DebugFormat("AppSettings file {0} exists",fileName);
                FileStream file = null;
                StreamReader sr = null;
                try
                {
                    Log.DebugFormat("Trying to read AppSettings file {0}", fileName);
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    var xmlString = sr.ReadToEnd();
                    sr.Close();
                    file.Close();
                    Log.DebugFormat("AppSettings content read:{0}{1}", Environment.NewLine, xmlString);
                    return Deserialize(xmlString);
                }
                finally
                {
                    if (file != null)
                    {
                        file.Dispose();
                    }
                    if (sr != null)
                    {
                        sr.Dispose();
                    }
                }
            }
            else
            {
                Log.DebugFormat("AppSettings file {0} does not exists!", fileName);
                return AppSettings.DefInstance;
            }
        }

        public static AppSettings Load()
        {
            var appSettings = Load(Path.Combine(AppDataDir, "settings.xml"));
            appSettings.Profiles = ProfileSerialize.Load().Items;
            appSettings.MoveList = MoveSerialize.Load().Items;
            appSettings.CopyList = CopySerialize.Load().Items;
            Log.Debug("All AppSettings data sucessfully loaded.");
            Log.DebugFormat("{0} profiles loaded", AppSettings.DefInstance.Profiles.Count);
            Log.DebugFormat("Selected language: {0}", AppSettings.DefInstance.CurrentLanguage.Name);
            return appSettings;
        }

        #endregion

        #region "Methods"

        private static BindingList<Language> DefaultLanguages()
        {
            var lngs = new BindingList<Language>
                {
                    new Language("Deutsch", "de-DE", "muzoss"),
                    new Language("English", "en-US", "Krkadoni"),
                    new Language("Français", "fr-FR", "Krkadoni"),
                    new Language("Hrvatski", "hr-HR", "Krkadoni"),
                    new Language("Italiano", "it-IT", "Milena Gerboni"),
                    new Language("Polski", "pl-PL", "kosmacz")
                };
            return lngs;
        }

        private Language DetectLanguage()
        {
            switch (Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName)
            {
                case "hr":
                case "sr":
                case "bs":
                    {
                        return Languages.SingleOrDefault(x => x.Name == "Hrvatski");
                    }
                case "de":
                    {
                        return Languages.SingleOrDefault(x => x.Name == "Deutsch");
                    }
                case "fr":
                    {
                        return Languages.SingleOrDefault(x => x.Name == "Français");
                    }
                case "it":
                    {
                        return Languages.SingleOrDefault(x => x.Name == "Italiano");
                    }
                case "pl":
                    {
                        return Languages.SingleOrDefault(x => x.Name == "Polski");
                    }
                default:
                    {
                        return Languages.SingleOrDefault(x => x.Name == "English");
                    }

            }

        }

        #endregion

    }
}
