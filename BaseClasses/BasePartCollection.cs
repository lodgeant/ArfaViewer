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
        public List<BasePart> BasePartList = new List<BasePart>();

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

        public static string ConvertBasePartCollectionToDBInsertValuesString(BasePartCollection bpc)
        {           
            StringBuilder sb = new StringBuilder();
            int index = 1;
            foreach (BaseClasses.BasePart bp in bpc.BasePartList)
            {
                //if (index == 5) break;
                sb.Append("(");
                sb.Append(index.ToString() + ",");                  // ID int
                sb.Append("'" + bp.LDrawRef + "',");                // LDRAW_REF varchar(25)
                sb.Append("'" + bp.LDrawDescription + "',");        // LDRAW_DESCRIPTION varchar(100)
                sb.Append("'" + bp.LDrawCategory + "',");           // LDRAW_CATEGORY varchar(25)
                sb.Append(bp.LDrawSize + ",");                      // LDRAW_SIZE int
                sb.Append(bp.OffsetX + ",");                        // OFFSET_X decimal
                sb.Append(bp.OffsetY + ",");                        // OFFSET_Y decimal
                sb.Append(bp.OffsetZ + ",");                        // OFFSET_Z decimal
                sb.Append("'" + bp.IsSubPart + "',");               // IS_SUB_PART varchar(5)
                sb.Append("'" + bp.IsSticker + "',");               // IS_STICKER varchar(5)
                sb.Append("'" + bp.IsLargeModel + "',");            // IS_LARGE_MODEL varchar(5)
                sb.Append("'" + bp.partType + "',");                // PART_TYPE varchar(25)
                sb.Append("'" + bp.lDrawPartType + "'");            // LDRAW_PART_TYPE varchar(25)
                sb.Append(")," + Environment.NewLine);
                index += 1;
            }
            return sb.ToString();
        }

    }
}
