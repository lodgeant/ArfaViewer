using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class Step
    {
        [XmlAttribute("PureStepNo")]
        public int PureStepNo;

        [XmlAttribute("StepLevel")]
        public int StepLevel;

        [XmlAttribute("ModelRotationX")]
        public float ModelRotationX;

        [XmlAttribute("ModelRotationY")]
        public float ModelRotationY;

        [XmlAttribute("ModelRotationZ")]
        public float ModelRotationZ;

        [XmlAttribute("StepBook")]
        public int StepBook;

        [XmlAttribute("StepPage")]
        public int StepPage;




        [XmlElement("Part")]
        public List<Part> partList = new List<Part>();

        [XmlElement("SubModel")]
        public List<SubModel> subModelList = new List<SubModel>();

        // ### NEW ###
        //[XmlAttribute("DefaultCameraPosX")]
        //public float DefaultCameraPosX;

        //[XmlAttribute("DefaultCameraPosY")]
        //public float DefaultCameraPosY;

        //[XmlAttribute("DefaultCameraPosZ")]
        //public float DefaultCameraPosZ;

        //[XmlAttribute("DefaultCameraRotX")]
        //public float DefaultCameraRotX;

        //[XmlAttribute("DefaultCameraRotY")]
        //public float DefaultCameraRotY;

        //[XmlAttribute("DefaultCameraRotZ")]
        //public float DefaultCameraRotZ;

        [XmlAttribute("State")]
        public StepState state;

        [Serializable]
        public enum StepState
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

        public Step DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (Step)serializer.Deserialize(reader);
            }
        }


    }

}
