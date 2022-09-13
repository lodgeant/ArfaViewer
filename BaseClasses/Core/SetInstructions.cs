using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;




namespace BaseClasses
{
    [Serializable]
    public class SetInstructions
    {
        [XmlAttribute("Ref")]
        public string Ref { get; set; }

        [XmlAttribute("Data")]
        public string Data { get; set; }



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

        public SetDetails DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (SetDetails)serializer.Deserialize(reader);
            }
        }

        public static SetInstructions GetSetInstructionsFromDBDataRow(DataRow row)
        {
            SetInstructions item = new SetInstructions();
            item.Ref = (string)row["REF"];
            if(row.Table.Columns.Contains("DATA")) item.Data = (string)row["DATA"];                            
            return item;
        }

    }
}
