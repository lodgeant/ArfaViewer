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


    [XmlInclude(typeof(RouteRequest))]
    [Serializable]
    public class Request
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("RequestingObject")]
        public string RequestingObject { get; set; }

        [XmlElement("Priority")]
        public string Priority { get; set; }

        [XmlAttribute("TimeTakenMS")]
        public double TimeTakenMS { get; set; }

        [XmlElement("CreatedTS")]
        public DateTime CreatedTS { get; set; }

        [XmlElement("GameTS")]
        public float GameTS { get; set; }




    }

}
