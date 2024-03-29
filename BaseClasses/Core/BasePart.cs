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
    public class BasePart
    {
        [XmlAttribute("LDrawRef")]
        public string LDrawRef { get; set; }

        [XmlAttribute("LDrawDescription")]
        public string LDrawDescription { get; set; }

        [XmlAttribute("LDrawCategory")]
        public string LDrawCategory { get; set; }

        [XmlAttribute("LDrawSize")]
        public int LDrawSize { get; set; }

        [XmlAttribute("OffsetX")]
        public float OffsetX { get; set; }

        [XmlAttribute("OffsetY")]
        public float OffsetY { get; set; }

        [XmlAttribute("OffsetZ")]
        public float OffsetZ { get; set; }

        [XmlAttribute("IsSubPart")]
        public bool IsSubPart { get; set; }

        [XmlAttribute("IsSticker")]
        public bool IsSticker { get; set; }

        [XmlAttribute("PartType")]
        public PartType partType { get; set; }
        
        [XmlAttribute("IsLargeModel")]
        public bool IsLargeModel { get; set; }

        [XmlAttribute("LDrawPartType")]
        public LDrawPartType lDrawPartType { get; set; }
        
        [XmlAttribute("SubPartCount")]
        public int SubPartCount { get; set; }



        public BasePart()
        {
            partType = PartType.NONE;
            lDrawPartType = LDrawPartType.UNKNOWN;
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

        public BasePart DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (BasePart)serializer.Deserialize(reader);
            }
        }


        public static BasePart GetBasePartFromDBDataRow(DataRow row)
        {
            BasePart item = new BasePart();
            item.LDrawRef = (string)row["LDRAW_REF"];
            item.LDrawDescription = (string)row["LDRAW_DESCRIPTION"];
            item.LDrawCategory = (string)row["LDRAW_CATEGORY"];
            item.LDrawSize = (int)row["LDRAW_SIZE"];
            item.OffsetX = float.Parse(row["OFFSET_X"].ToString());
            item.OffsetY = float.Parse(row["OFFSET_Y"].ToString());
            item.OffsetZ = float.Parse(row["OFFSET_Z"].ToString());
            item.IsSubPart = bool.Parse(row["IS_SUB_PART"].ToString());
            item.IsSticker = bool.Parse(row["IS_STICKER"].ToString());
            item.IsLargeModel = bool.Parse(row["IS_LARGE_MODEL"].ToString());
            item.partType = (BasePart.PartType)Enum.Parse(typeof(BasePart.PartType), (string)row["PART_TYPE"], true);
            item.lDrawPartType = (BasePart.LDrawPartType)Enum.Parse(typeof(BasePart.LDrawPartType), (string)row["LDRAW_PART_TYPE"], true);
            item.SubPartCount = (int)row["SUB_PART_COUNT"];
            return item;
        }

        




        public enum PartType
        {
            NONE,
            BASIC,
            COMPOSITE,
            //STICKER,
            //BASIC_STICKER_COMPOSITE,      //for use by BASIC parts that currently have a sticker attached
        }

        public enum LDrawPartType
        {
            UNKNOWN,
            OFFICIAL,
            UNOFFICIAL
        }

    }

    

}
