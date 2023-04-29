using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{
        
    [Serializable]
    public class zzz_TreePositionRequest : Request
    {
        
        [XmlElement("Center")]
        public Vect Center;

        [XmlAttribute("Radius")]
        public float Radius;

        [XmlAttribute("TreeType")]
        public string TreeType;

        [XmlElement("PlantingPosition")]
        public Vect PlantingPosition;

        [XmlAttribute("PositionFound")]
        public bool PositionFound;





        
    }

}
