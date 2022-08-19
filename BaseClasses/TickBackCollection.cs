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
    public class TickBackCollection
    {
        [XmlElement("TickBack")]
        public List<TickBack> TickBackList = new List<TickBack>();

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

        public TickBackCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (TickBackCollection)serializer.Deserialize(reader);
            }
        }

        public static TickBackCollection GetTickBackCollectionFromDataTable(DataTable table)
        {
            TickBackCollection coll = new TickBackCollection();
            foreach (DataRow row in table.Rows)
            {
                TickBack item = TickBack.GetTickBackFromDBDataRow(row);
                coll.TickBackList.Add(item);
            }
            return coll;
        }


    }
}
