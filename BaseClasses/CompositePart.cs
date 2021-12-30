using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;



namespace BaseClasses
{
    [Serializable]
    public class CompositePart
    {
        [XmlAttribute("ParentLDrawRef")]
        public string ParentLDrawRef;

        [XmlAttribute("LDrawRef")]
        public string LDrawRef;

        [XmlAttribute("LDrawDescription")]
        public string LDrawDescription;

        [XmlAttribute("LDrawColourID")]
        public int LDrawColourID;

        [XmlAttribute("PosX")]
        public float PosX;

        [XmlAttribute("PosY")]
        public float PosY;

        [XmlAttribute("PosZ")]
        public float PosZ;

        [XmlAttribute("RotX")]
        public float RotX;

        [XmlAttribute("RotY")]
        public float RotY;

        [XmlAttribute("RotZ")]
        public float RotZ;

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

        public CompositePart DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (CompositePart)serializer.Deserialize(reader);
            }
        }

    }

}
