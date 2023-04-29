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
    public class Node
    {
        [XmlElement("NodePosition")]
        public Point nodePosition;

        [XmlAttribute("F")]
        public int F;

        [XmlAttribute("G")]
        public int G;

        [XmlAttribute("H")]
        public int H;

        [XmlIgnore]
        public Node parentNode;

    }

}
