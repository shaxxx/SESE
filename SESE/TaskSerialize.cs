// Copyright (c) 2013 Krkadoni.com - Released under The MIT License.
// Full license text can be found at http://opensource.org/licenses/MIT
     
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

namespace Krkadoni.SESE
{

    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "4.0.30319.33440"),
        System.SerializableAttribute,
        // ReSharper disable once LocalizableElement
        DesignerCategoryAttribute("code"),
        XmlTypeAttribute(AnonymousType = true),
        XmlRootAttribute(IsNullable = false, ElementName = "Tasks")]

    public class TaskSerialize
    {

        private BindingList<Task> _items;

        private static XmlSerializer _sSerializer;
        public TaskSerialize()
        {
            _items = new BindingList<Task>();
        }

        [XmlElementAttribute("task", Order = 0)]
        public BindingList<Task> Items
        {
            get { return _items; }
            set
            {
                if (_items != null && _items.Equals(value)) return;
                _items = value;
            }
        }

        private static XmlSerializer Serializer
        {
            get { return _sSerializer ?? (_sSerializer = new XmlSerializer(typeof (TaskSerialize))); }
        }

        #region "Serialize/Deserialize"

        protected virtual string Serialize()
        {
            StreamReader streamReader = null;
           MemoryStream memoryStream = null;
            try
            {
                memoryStream = new MemoryStream();
                Serializer.Serialize(memoryStream, this);
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

        public static  TaskSerialize Deserialize(string xml)
        {
            StringReader stringReader = null;
            try
            {
                stringReader = new StringReader(xml);
                return (TaskSerialize)Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader));
            }
            finally
            {
                if (stringReader != null)
                {
                    stringReader.Dispose();
                }
            }
        }

        public virtual void SaveToFile(string fileName)
        {
            StreamWriter streamWriter = null;
            try
            {
                string xmlString = Serialize();
                var xmlFile = new FileInfo(fileName);
                streamWriter = xmlFile.CreateText();
                streamWriter.WriteLine(xmlString);
                streamWriter.Close();
            }
            finally
            {
                if (streamWriter != null)
                {
                    streamWriter.Dispose();
                }
            }
        }

        public static TaskSerialize LoadFromFile(string fileName)
        {
            FileStream file = null;
            StreamReader sr = null;
            try
            {
                file = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                sr = new StreamReader(file);
                var xmlString = sr.ReadToEnd();
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

        #endregion

    }
}
