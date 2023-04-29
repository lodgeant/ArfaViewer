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
    // Status:      OPEN, IN_PROGRESS, CLOSED
    // Priority:    LOW, HIGH


    [Serializable]
    public class CommodityOrder
    {
        [XmlAttribute("ID")]
        public int ID { get; set; }

        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("CommodityType")]
        public string CommodityType { get; set; }

        [XmlAttribute("Priority")]
        public string Priority { get; set; }

        [XmlAttribute("RelatedSettlerRef")]
        public string RelatedSettlerRef { get; set; }

        [XmlAttribute("CommodityRef")]
        public string CommodityRef { get; set; }

        [XmlAttribute("SourceBuildingRef")]
        public string SourceBuildingRef { get; set; }

        [XmlAttribute("DestinationBuildingRef")]
        public string DestinationBuildingRef { get; set; }






        [XmlElement("CreatedTS")]
        public DateTime CreatedTS { get; set; }

        [XmlElement("CreatedGameTS")]
        public float CreatedGameTS { get; set; }

        [XmlElement("PickedUpTS")]
        public DateTime PickedUpTS { get; set; }

        [XmlElement("PickedUpGameTS")]
        public float PickedUpGameTS { get; set; }

        [XmlElement("DeliveredTS")]
        public DateTime DeliveredTS { get; set; }

        [XmlElement("DeliveredGameTS")]
        public float DeliveredGameTS { get; set; }




        [XmlElement("SourcePosition")]
        public Vect SourcePosition { get; set; }

        [XmlElement("DestinationPosition")]
        public Vect DestinationPosition { get; set; }


    }

}
