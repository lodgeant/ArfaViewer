using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace BaseClasses
{
    [Serializable]
    public class SubPartMappingCollection
    {
        [XmlElement("SubPartMapping")]
        public List<SubPartMapping> SubPartMappingList { get; set; }

        public SubPartMappingCollection()
        {
            SubPartMappingList = new List<SubPartMapping>();
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

        public SubPartMappingCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (SubPartMappingCollection)serializer.Deserialize(reader);
            }
        }


        public static SubPartMappingCollection GetSubPartMappingCollectionFromDataTable(DataTable table)
        {
            SubPartMappingCollection coll = new SubPartMappingCollection();
            foreach (DataRow row in table.Rows)
            {
                SubPartMapping item = SubPartMapping.GetSubPartMappingFromDBDataRow(row);
                coll.SubPartMappingList.Add(item);
            }
            return coll;
        }



        public static DataTable GetDatatableFromSubPartMappingCollection(SubPartMappingCollection coll)
        {
            DataTable table = new DataTable("", "");
            table.Columns.Add("Parent LDraw Ref", typeof(string));
            table.Columns.Add("Sub Part LDraw Ref", typeof(string));
            table.Columns.Add("LDraw Colour ID", typeof(int));
            table.Columns.Add("PosX", typeof(float));
            table.Columns.Add("PosY", typeof(float));
            table.Columns.Add("PosZ", typeof(float));
            table.Columns.Add("RotX", typeof(float));
            table.Columns.Add("RotY", typeof(float));
            table.Columns.Add("RotZ", typeof(float));
            foreach (SubPartMapping item in coll.SubPartMappingList)
            {
                DataRow newRow = table.NewRow();
                newRow["Parent LDraw Ref"] = item.ParentLDrawRef;
                newRow["Sub Part LDraw Ref"] = item.SubPartLDrawRef;
                newRow["LDraw Colour ID"] = item.LDrawColourID;
                newRow["PosX"] = item.PosX;
                newRow["PosY"] = item.PosY;
                newRow["PosZ"] = item.PosZ;
                newRow["RotX"] = item.RotX;
                newRow["RotY"] = item.RotY;
                newRow["RotZ"] = item.RotZ;
                table.Rows.Add(newRow);
            }
            return table;
        }





    }


}
