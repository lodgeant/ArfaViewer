﻿using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Serialization;


namespace BaseClasses
{
    [Serializable]
    public class PartListPart
    {
        [XmlAttribute("LDrawRef")]
        public string LDrawRef { get; set; }

        [XmlAttribute("LDrawColourID")]
        public int LDrawColourID { get; set; }

        [XmlAttribute("Qty")]
        public int Qty { get; set; }

        [XmlAttribute("QtyFound")]
        public int QtyFound { get; set; }

        [XmlAttribute("LDrawDescription")]
        public string LDrawDescription { get; set; }



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

        public PartListPart DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (PartListPart)serializer.Deserialize(reader);
            }
        }




        


    }
}
