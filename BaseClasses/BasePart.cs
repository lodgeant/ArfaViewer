using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class BasePart
    {
        [XmlAttribute("LDrawRef")]
        public string LDrawRef;

        [XmlAttribute("LDrawDescription")]
        public string LDrawDescription;

        [XmlAttribute("LDrawCategory")]
        public string LDrawCategory;

        [XmlAttribute("LDrawSize")]
        public int LDrawSize;

        [XmlAttribute("OffsetX")]
        public float OffsetX;

        [XmlAttribute("OffsetY")]
        public float OffsetY;

        [XmlAttribute("OffsetZ")]
        public float OffsetZ;

        [XmlAttribute("IsSubPart")]
        public bool IsSubPart;

        [XmlAttribute("IsSticker")]
        public bool IsSticker;

        [XmlAttribute("PartType")]
        public PartType partType = PartType.NONE;
        public enum PartType
        {
            NONE,
            BASIC,
            COMPOSITE,
            //STICKER,
            //BASIC_STICKER_COMPOSITE,      //for use by BASIC parts that currently have a sticker attached
        }

        [XmlAttribute("IsLargeModel")]
        public bool IsLargeModel;

        [XmlAttribute("LDrawPartType")]
        public LDrawPartType lDrawPartType = LDrawPartType.UNKNOWN;
        public enum LDrawPartType
        {
            UNKNOWN,
            OFFICIAL,
            UNOFFICIAL
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

    }

}
