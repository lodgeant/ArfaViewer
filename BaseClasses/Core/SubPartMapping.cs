﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;


namespace BaseClasses
{
    [Serializable]
    public class SubPartMapping
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("ParentLDrawRef")]
        public string ParentLDrawRef { get; set; }

        [XmlAttribute("SubPartLDrawRef")]
        public string SubPartLDrawRef { get; set; }

        [XmlAttribute("LDrawColourID")]
        public int LDrawColourID { get; set; }

        [XmlAttribute("PosX")]
        public float PosX { get; set; }

        [XmlAttribute("PosY")]
        public float PosY { get; set; }

        [XmlAttribute("PosZ")]
        public float PosZ { get; set; }

        [XmlAttribute("RotX")]
        public float RotX { get; set; }

        [XmlAttribute("RotY")]
        public float RotY { get; set; }

        [XmlAttribute("RotZ")]
        public float RotZ { get; set; }







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

        public SubPartMapping DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (SubPartMapping)serializer.Deserialize(reader);
            }
        }

        public static SubPartMapping GetSubPartMappingFromDBDataRow(DataRow row)
        {
            SubPartMapping item = new SubPartMapping();
            item.ID = (int)row["ID"];
            item.SubPartLDrawRef = (string)row["SUB_PART_LDRAW_REF"];
            item.ParentLDrawRef = (string)row["PARENT_LDRAW_REF"];
            item.LDrawColourID = (int)row["LDRAW_COLOUR_ID"];
            item.PosX = float.Parse(row["POS_X"].ToString());
            item.PosY = float.Parse(row["POS_Y"].ToString());
            item.PosZ = float.Parse(row["POS_Z"].ToString());
            item.RotX = float.Parse(row["ROT_X"].ToString());
            item.RotY = float.Parse(row["ROT_Y"].ToString());
            item.RotZ = float.Parse(row["ROT_Z"].ToString());
            return item;
        }




    }

}
