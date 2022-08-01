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
        public int LDrawColourID;

        [XmlAttribute("LDrawColourName")]
        public string LDrawColourName;

        [XmlAttribute("LDrawColourHex")]
        public string LDrawColourHex;

        [XmlAttribute("LDrawColourAlpha")]
        public int LDrawColourAlpha;
                
        //public int LegoColourID;
        //public string LegoColourName;
        //public int BrickLinkColourID;
        //public string BrickLinkColourName;
        //public string PeeronColourName;



        public static PartColour GetPartColourFromDBDataRow(DataRow row)
        {
            PartColour pc = new PartColour();
            pc.LDrawColourID = (int)row["LDRAWCOLOUR_ID"];
            pc.LDrawColourName = (string)row["LDRAWCOLOUR_NAME"];
            pc.LDrawColourHex = (string)row["LDRAWCOLOUR_HEX"];
            pc.LDrawColourAlpha = (int)row["LDRAWCOLOUR_ALPHA"];
            return pc;
        }


    }





}
