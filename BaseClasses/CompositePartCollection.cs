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
    public class CompositePartCollection
    {
        [XmlElement("CompositePart")]
        public List<CompositePart> CompositePartList { get; set; }

        public CompositePartCollection()
        {
            CompositePartList = new List<CompositePart>();
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

        public CompositePartCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (CompositePartCollection)serializer.Deserialize(reader);
            }
        }


        public static CompositePartCollection GetCompositePartCollectionFromDataTable(DataTable table)
        {
            CompositePartCollection coll = new CompositePartCollection();
            foreach (DataRow row in table.Rows)
            {
                CompositePart item = CompositePart.GetCompositePartFromDBDataRow(row);
                coll.CompositePartList.Add(item);
            }
            return coll;
        }




        public static string ConvertCompositePartCollectionToDBInsertValuesString(CompositePartCollection coll)
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;
            foreach (CompositePart p in coll.CompositePartList)
            {
                //if (index == 5) break;
                sb.Append("(");
                sb.Append(index.ToString() + ",");                  // ID int
                sb.Append("'" + p.LDrawRef + "',");                 // LDRAW_REF varchar(25)
                sb.Append("'" + p.LDrawDescription + "',");         // LDRAW_DESCRIPTION varchar(100)
                sb.Append("'" + p.ParentLDrawRef + "',");           // PARENT_LDRAW_REF varchar(25)
                sb.Append(p.LDrawColourID + ",");                   // LDRAW_COLOUR_ID int
                sb.Append(p.PosX + ",");                            // POX_X float
                sb.Append(p.PosY + ",");                            // POX_Y float
                sb.Append(p.PosZ + ",");                            // POX_Z float
                sb.Append(p.RotX + ",");                            // ROT_X float
                sb.Append(p.RotY + ",");                            // ROT_Y float
                sb.Append(p.RotZ);                                  // ROT_Z float
                sb.Append(")," + Environment.NewLine);
                index += 1;
            }
            return sb.ToString();
        }


    }

}
