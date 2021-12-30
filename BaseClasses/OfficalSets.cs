using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class OfficalSets
    {
        [XmlElement("User")]
        public User user;

        [XmlElement("Set")]
        public List<OfficalSet_Set> setList = new List<OfficalSet_Set>();

        [XmlElement("MiniFig")]
        public List<OfficalSet_MiniFig> minifigList = new List<OfficalSet_MiniFig>();



        public string SerializeToString(bool omitDeclaration)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                if (omitDeclaration)
                {
                    return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
                }
                else
                {
                    return textWriter.ToString();
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            // Use UTF8 encoding but write no BOM to the wire
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
            }
        }

        public OfficalSets DeserialiseFromXMLString(string XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (OfficalSets)serializer.Deserialize(reader);
            }
        }

    }

}
