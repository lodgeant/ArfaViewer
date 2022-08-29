using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;

namespace BaseClasses
{
    [Serializable]
    public class PartColourCollection
    {
        [XmlElement("PartColour")]
        public List<PartColour> PartColourList { get; set; }


        public PartColourCollection()
        {
            PartColourList = new List<PartColour>();
        }
            





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

        public PartColourCollection DeserialiseFromXMLString(String XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (PartColourCollection)serializer.Deserialize(reader);
            }
        }


        public static PartColourCollection GetPartColourCollectionFromDataTable(DataTable table)
        {
            PartColourCollection coll = new PartColourCollection();
            foreach (DataRow row in table.Rows)
            {
                PartColour item = PartColour.GetPartColourFromDBDataRow(row);
                coll.PartColourList.Add(item);
            }
            return coll;
        }

    }
}
