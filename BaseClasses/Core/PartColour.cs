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
    public class PartColour
    {
        [XmlAttribute("LDrawColourID")]
        public int LDrawColourID { get; set; }

        [XmlAttribute("LDrawColourName")]
        public string LDrawColourName { get; set; }

        [XmlAttribute("LDrawColourHex")]
        public string LDrawColourHex { get; set; }

        [XmlAttribute("LDrawColourAlpha")]
        public int LDrawColourAlpha { get; set; }

        //public int LegoColourID;
        //public string LegoColourName;
        //public int BrickLinkColourID;
        //public string BrickLinkColourName;
        //public string PeeronColourName;



        public static PartColour GetPartColourFromDBDataRow(DataRow row)
        {
            PartColour item = new PartColour();
            item.LDrawColourID = (int)row["LDRAW_COLOUR_ID"];
            item.LDrawColourName = (string)row["LDRAW_COLOUR_NAME"];
            item.LDrawColourHex = (string)row["LDRAW_COLOUR_HEX"];
            item.LDrawColourAlpha = (int)row["LDRAW_COLOUR_ALPHA"];
            return item;
        }

        



    }





}
