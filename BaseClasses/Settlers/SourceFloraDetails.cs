using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{

    [Serializable]
    public class SourceFloraDetails
    {
        [XmlAttribute("Ref")]
        public string Ref;

        [XmlAttribute("PositionX")]
        public float PositionX;
        [XmlAttribute("PositionY")]
        public float PositionY;
        [XmlAttribute("PositionZ")]
        public float PositionZ;

        //[XmlElement("Position")]
        //public Vect Position;

        [XmlAttribute("DistanceFromHut")]
        public float DistanceFromHut;

        [XmlAttribute("ObjectSubType")]
        public string ObjectSubType;



    }

}
