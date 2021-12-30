using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

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

    }

}
