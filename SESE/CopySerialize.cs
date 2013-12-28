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
        XmlRootAttribute(IsNullable = false, ElementName = "Copy")]

    public class CopySerialize
    {

        private static BindingList<PositionTransform> _items = new BindingList<PositionTransform>();
        private static XmlSerializer _sSerializer;
        private static CopySerialize _defInstance;

        [XmlIgnore]
        public static CopySerialize DefInstance
        {
            get { return _defInstance ?? (_defInstance = new CopySerialize()); }
            set { _defInstance = value; }
        }

        [XmlElementAttribute("PositionTransform", Order = 0)]
        public BindingList<PositionTransform> Items
        {
            get { return _items; }
            set
            {
                _items = value;
            }
        }

        private static XmlSerializer Serializer
        {
            get { return _sSerializer ?? (_sSerializer = new XmlSerializer(typeof(CopySerialize))); }
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

        public static CopySerialize Deserialize(string xml)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(xml);
                return (CopySerialize)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
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
            AppSettings.Log.DebugFormat("Saving copy list to file {0}", fileName);
            StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                AppSettings.Log.DebugFormat("Copy list content serialized as:{0}{1}", Environment.NewLine, xmlString);
                var xmlFile = new FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
                AppSettings.Log.DebugFormat("Copy list saved to {0}", fileName);
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
            Save(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "copy.xml"));
        }

        public static CopySerialize Load(string fileName)
        {
            AppSettings.Log.Debug("Loading copy list");
            if (System.IO.File.Exists(fileName))
            {
                AppSettings.Log.DebugFormat("Copy list file {0} exists", fileName);
                FileStream file = null;
                StreamReader sr = null;
                try
                {
                    AppSettings.Log.DebugFormat("Trying to read copy list file {0}", fileName);
                    file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    sr = new StreamReader(file);
                    var xmlString = sr.ReadToEnd();
                    AppSettings.Log.DebugFormat("Copy list content read:{0}{1}", Environment.NewLine, xmlString);
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
                return CopySerialize.DefInstance;
            }
        }

        public static CopySerialize Load()
        {
            return Load(Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "copy.xml"));
        }

        #endregion

    }
}
