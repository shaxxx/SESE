// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System;
using System.ComponentModel;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Krkadoni.SESE
{

    [System.SerializableAttribute,
        // ReSharper disable once LocalizableElement
        DesignerCategoryAttribute("code"),
        XmlTypeAttribute(AnonymousType = true),
        XmlRootAttribute(IsNullable = false, ElementName = "Profiles")]

    public class ProfileSerialize
    {

        private static BindingList<Profile> _items = new BindingList<Profile>();
        private static XmlSerializer _sSerializer;
        private static ProfileSerialize _defInstance;

        [XmlIgnore]
        public static ProfileSerialize DefInstance
        {
            get { return _defInstance ?? (_defInstance = new ProfileSerialize()); }
            set { _defInstance = value; }
        }

        [XmlElementAttribute("Profile", Order = 0)]
        public BindingList<Profile> Items
        {
            get { return _items; }
            set
            {
                _items = value;
            }
        }

        private static XmlSerializer Serializer
        {
            get { return _sSerializer ?? (_sSerializer = new XmlSerializer(typeof(ProfileSerialize))); }
        }

        #region "Serialize/Deserialize"

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

        public static ProfileSerialize Deserialize(string xml)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(xml);
                return (ProfileSerialize)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
            }
            finally
            {
                if (stringReader != null)
                {
                    stringReader.Dispose();
                }
            }
        }

        public static void Save(string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                AppSettings.Log.DebugFormat("Saving profiles to file {0}", fileName);
                string xmlString = Serialize();
                AppSettings.Log.DebugFormat("Profiles content serialized to xmlString");
                var xmlFile = new FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
                AppSettings.Log.DebugFormat("Profiles saved to {0}", fileName);
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
            Save(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "profiles.xml"));
        }

        public static ProfileSerialize Load(string fileName)
        {
            AppSettings.Log.DebugFormat("Loading profiles from file {0}", fileName);
            if (System.IO.File.Exists(fileName))
            {
                AppSettings.Log.DebugFormat("Profiles file {0} exists", fileName);
                FileStream file = null;
                StreamReader sr = null;
                try
                {
                    AppSettings.Log.DebugFormat("Reading profiles file {0}", fileName);
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    var xmlString = sr.ReadToEnd();
                    AppSettings.Log.DebugFormat("Profiles file {0} read to xmlString", fileName);
                    sr.Close();
                    file.Close();
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
                AppSettings.Log.DebugFormat("Profiles file {0} does not exists", fileName);
                return ProfileSerialize.DefInstance;
            }
        }

        public static ProfileSerialize Load()
        {
           return Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "profiles.xml"));
        }

        #endregion

    }
}
