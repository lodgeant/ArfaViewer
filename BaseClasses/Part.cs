using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class Part
    {
        [XmlAttribute("SubSetRef")]
        public string SubSetRef = "";

        //[XmlAttribute("UnityRef")]
        //public string UnityRef;

        [XmlAttribute("LDrawRef")]
        public string LDrawRef;

        [XmlAttribute("LDrawColourID")]
        public int LDrawColourID;

        [XmlAttribute("IsSubPart")]
        public bool IsSubPart;

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

        [XmlElement("Part")]
        public List<Part> SubPartList = new List<Part>();

        [XmlElement("PlacementMovement")]
        public List<PlacementMovement> placementMovementList = new List<PlacementMovement>();

        [XmlAttribute("TickedBack")]
        public bool TickedBack;



        //[XmlAttribute("BrickBuddyTrayID")]
        //public int BrickBuddyTrayID;

        //[XmlAttribute("StepPlacementIndex")]
        //public int StepPlacementIndex;

        //[XmlAttribute("ModelPlacementIndex")]
        //public int ModelPlacementIndex;

        //[XmlAttribute("RelatedParentUnityRef")]
        //public String RelatedParentUnityRef;

        //[XmlAttribute("BuildBagID")]
        //public String BuildBagID;

        // ### NEW ###
        //[XmlAttribute("IsParentPart")]
        //public bool IsParentPart;



        //[XmlAttribute("IsSticker")]
        //public bool IsSticker;

        //[XmlAttribute("HasStickerAttached")]
        //public bool HasStickerAttached;




        [XmlAttribute("State")]
        public PartState state;

        [Serializable]
        public enum PartState
        {
            NOT_COMPLETED,
            COMPLETED
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


    }

}
