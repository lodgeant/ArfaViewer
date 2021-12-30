using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class Sticker
    {
        [XmlAttribute("ParentPartRef")]
        public string ParentPartRef;

        [XmlAttribute("ParentLDrawColourID")]
        public int ParentLDrawColourID;

        [XmlAttribute("StickerRef")]
        public string StickerRef;

        [XmlAttribute("LDrawRef")]
        public string LDrawRef;

        [XmlAttribute("PosX")]
        public float PosX;

        [XmlAttribute("PosY")]
        public float PosY;

        [XmlAttribute("PosZ")]
        public float PosZ;

        [XmlAttribute("RotX")]
        public float RotX;

        [XmlAttribute("RotY")]
        public float RotY;

        [XmlAttribute("RotZ")]
        public float RotZ;

        [XmlAttribute("SheetPosX")]
        public float SheetPosX;

        [XmlAttribute("SheetPosY")]
        public float SheetPosY;

        [XmlAttribute("SheetPosZ")]
        public float SheetPosZ;

        [XmlAttribute("SheetRotX")]
        public float SheetRotX;

        [XmlAttribute("SheetRotY")]
        public float SheetRotY;

        [XmlAttribute("SheetRotZ")]
        public float SheetRotZ;

    }

}
