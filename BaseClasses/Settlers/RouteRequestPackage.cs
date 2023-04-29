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
    public class RouteRequestPackage
    {
        [XmlElement("RouteCorePointList")]
        public List<Point> RouteCorePointList { get; set; }

        [XmlElement("NodeList_Open")]
        public List<Node> NodeList_Open { get; set; }

        [XmlElement("NodeList_Closed")]
        public List<Node> NodeList_Closed { get; set; }


        public RouteRequestPackage()
        {
            RouteCorePointList = new List<Point>();
            NodeList_Open = new List<Node>();
            NodeList_Closed = new List<Node>();
        }


    }

}
