using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class SubModel
    {
        [XmlAttribute("Ref")]
        public string Ref;

        [XmlAttribute("Description")]
        public string Description;

        //[XmlAttribute("LDrawModelType")]
        //public string LDrawModelType;

        [XmlAttribute("LDrawModelType")]
        public LDrawModelType lDrawModelType;   

        [XmlAttribute("SubModelLevel")]
        public int SubModelLevel;

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

        [XmlElement("Step")]
        public List<Step> stepList = new List<Step>();

        [XmlElement("SubModel")]
        public List<SubModel> subModelList = new List<SubModel>();

        [XmlElement("PlacementMovement")]
        public List<PlacementMovement> placementMovementList = new List<PlacementMovement>();
                
        [XmlAttribute("State")]
        public SubModelState state;

        [Serializable]
        public enum SubModelState
        {
            NOT_COMPLETED,
            COMPLETED
        }

        [Serializable]
        public enum LDrawModelType
        {
            FINAL_MODEL,
            MODEL,
            MINIFIG,
            SUB_MODEL            
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
