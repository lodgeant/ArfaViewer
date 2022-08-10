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
    public class LDrawDetailsCollection
{
        [XmlElement("LDrawDetails")]
        public List<LDrawDetails> LDrawDetailsList = new List<LDrawDetails>();

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

        public LDrawDetailsCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (LDrawDetailsCollection)serializer.Deserialize(reader);
            }
        }

        public static LDrawDetailsCollection GetLDrawDetailsCollectionFromDataTable(DataTable table)
        {
            LDrawDetailsCollection coll = new LDrawDetailsCollection();
            foreach (DataRow row in table.Rows)
            {
                LDrawDetails item = LDrawDetails.GetLDrawDetailsFromDBDataRow(row);
                coll.LDrawDetailsList.Add(item);
            }
            return coll;
        }

        public static string ConvertLDrawDetailsCollectionToDBInsertValuesString(LDrawDetailsCollection coll)
        {
            StringBuilder sb = new StringBuilder();
            int index = 1;
            foreach (BaseClasses.LDrawDetails p in coll.LDrawDetailsList)
            {
                //if (index == 5) break;
                sb.Append("(");
                sb.Append(index.ToString() + ",");                 // ID int
                sb.Append("'" + p.LDrawRef + "',");                // LDRAW_REF varchar(25)
                sb.Append("'" + p.LDrawDescription + "',");        // LDRAW_DESCRIPTION varchar(100)
                //sb.Append("'" + p.LDrawCategory + "',");           // LDRAW_CATEGORY varchar(25)
                //sb.Append(p.LDrawSize + ",");                      // LDRAW_SIZE int
                //sb.Append(p.OffsetX + ",");                        // OFFSET_X decimal
                //sb.Append(p.OffsetY + ",");                        // OFFSET_Y decimal
                //sb.Append(p.OffsetZ + ",");                        // OFFSET_Z decimal
                //sb.Append("'" + p.IsSubPart + "',");               // IS_SUB_PART varchar(5)
                //sb.Append("'" + p.IsSticker + "',");               // IS_STICKER varchar(5)
                //sb.Append("'" + p.IsLargeModel + "',");            // IS_LARGE_MODEL varchar(5)
                //sb.Append("'" + p.partType + "',");                // PART_TYPE varchar(25)
                //sb.Append("'" + p.lDrawPartType + "'");            // LDRAW_PART_TYPE varchar(25)
                sb.Append(")," + Environment.NewLine);
                index += 1;
            }
            return sb.ToString();
        }

    }


}
