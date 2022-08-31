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
    public class BasePartCollection
    {
        [XmlElement("BasePart")]
        public List<BasePart> BasePartList { get; set; }


        public BasePartCollection()
        {
            BasePartList = new List<BasePart>();
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

        public BasePartCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (BasePartCollection)serializer.Deserialize(reader);
            }
        }


        public static BasePartCollection GetBasePartCollectionFromDataTable(DataTable table)
        {
            BasePartCollection coll = new BasePartCollection();
            foreach (DataRow row in table.Rows)
            {
                BasePart item = BasePart.GetBasePartFromDBDataRow(row);
                coll.BasePartList.Add(item);
            }
            return coll;
        }

        public static string ConvertBasePartCollectionToDBInsertValuesString(BasePartCollection coll)
        {           
            StringBuilder sb = new StringBuilder();
            int index = 1;
            foreach (BaseClasses.BasePart p in coll.BasePartList)
            {
                //if (index == 5) break;
                sb.Append("(");
                sb.Append(index.ToString() + ",");                 // ID int
                sb.Append("'" + p.LDrawRef + "',");                // LDRAW_REF varchar(25)
                sb.Append("'" + p.LDrawDescription + "',");        // LDRAW_DESCRIPTION varchar(100)
                sb.Append("'" + p.LDrawCategory + "',");           // LDRAW_CATEGORY varchar(25)
                sb.Append(p.LDrawSize + ",");                      // LDRAW_SIZE int
                sb.Append(p.OffsetX + ",");                        // OFFSET_X decimal
                sb.Append(p.OffsetY + ",");                        // OFFSET_Y decimal
                sb.Append(p.OffsetZ + ",");                        // OFFSET_Z decimal
                sb.Append("'" + p.IsSubPart + "',");               // IS_SUB_PART varchar(5)
                sb.Append("'" + p.IsSticker + "',");               // IS_STICKER varchar(5)
                sb.Append("'" + p.IsLargeModel + "',");            // IS_LARGE_MODEL varchar(5)
                sb.Append("'" + p.partType + "',");                // PART_TYPE varchar(25)
                sb.Append("'" + p.lDrawPartType + "'");            // LDRAW_PART_TYPE varchar(25)
                sb.Append(")," + Environment.NewLine);
                index += 1;
            }
            return sb.ToString();
        }


        public static DataTable GetDatatableFromBasePartCollection(BasePartCollection coll)
        {
            DataTable table = new DataTable("","");
            table.Columns.Add("LDraw Ref", typeof(string));
            table.Columns.Add("LDraw Description", typeof(string));
            table.Columns.Add("LDraw Size", typeof(int));
            table.Columns.Add("Part Type", typeof(string));
            table.Columns.Add("LDraw Part Type", typeof(string));
            table.Columns.Add("Is Sub Part", typeof(bool));
            table.Columns.Add("Is Sticker", typeof(bool));
            table.Columns.Add("Is Large Model", typeof(bool));
            table.Columns.Add("OffsetX", typeof(float));
            table.Columns.Add("OffsetY", typeof(float));
            table.Columns.Add("OffsetZ", typeof(float));
            table.Columns.Add("Sub Part Count", typeof(int));
            foreach (BasePart bp in coll.BasePartList)
            {
                DataRow newRow = table.NewRow();
                newRow["LDraw Ref"] = bp.LDrawRef;
                newRow["LDraw Description"] = bp.LDrawDescription;
                newRow["LDraw Size"] = bp.LDrawSize;
                newRow["Part Type"] = bp.partType;
                newRow["LDraw Part Type"] = bp.lDrawPartType;
                newRow["Is Sub Part"] = bp.IsSubPart;
                newRow["Is Sticker"] = bp.IsSticker;
                newRow["Is Large Model"] = bp.IsLargeModel;
                newRow["OffsetX"] = bp.OffsetX;
                newRow["OffsetY"] = bp.OffsetY;
                newRow["OffsetZ"] = bp.OffsetZ;
                newRow["Sub Part Count"] = bp.SubPartCount;
                table.Rows.Add(newRow);
            }
            return table;
        }



    }
}
