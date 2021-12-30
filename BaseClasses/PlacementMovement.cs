using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class PlacementMovement
    {
        [XmlAttribute("Axis")]
        public string Axis;

        [XmlAttribute("Value")]
        public float Value;

        //[XmlAttribute("State")]
        //public State state;

        //[Serializable]
        //public enum State
        //{
        //    NotPlaced,
        //    Placed
        //}


        public string SerializeToString(bool omitDeclaration)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                //xmlSerializer.Serialize(textWriter, this);

                XmlSerializerNamespaces ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                xmlSerializer.Serialize(textWriter, this, ns);

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

        public PlacementMovement DeserialiseFromXMLString(String XMLString)
        {           
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (PlacementMovement)serializer.Deserialize(reader);
            }
        }


    }

}
