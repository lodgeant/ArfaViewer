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
    public class FileDetailsCollection
    {
        [XmlElement("FileDetails")]
        public List<FileDetails> FileDetailsList { get; set; }

        public FileDetailsCollection()
        {
            FileDetailsList = new List<FileDetails>();
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

        public FileDetailsCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (FileDetailsCollection)serializer.Deserialize(reader);
            }
        }



        public static DataTable GetDatatableFromFileDetailsCollection(FileDetailsCollection coll)
        {
            DataTable table = new DataTable("", "");
            table.Columns.Add("Name", typeof(string));
            table.Columns.Add("Size", typeof(long));
            table.Columns.Add("Created TS", typeof(DateTime));
            table.Columns.Add("Last Updated TS", typeof(DateTime));
            foreach (FileDetails fd in coll.FileDetailsList)
            {
                DataRow newRow = table.NewRow();
                newRow["Name"] = fd.Name;
                newRow["Size"] = fd.Size;
                newRow["Created TS"] = fd.CreatedTS;
                newRow["Last Updated TS"] = fd.LastUpdatedTS;
                table.Rows.Add(newRow);
            }
            return table;
        }

    }
}
