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
    public class Silo
    {
        [XmlAttribute("BuildingRef")]
        public string buildingRef;                                              // The parent building where the silo is located.

        [XmlAttribute("SiloIndex")]
        public int siloIndex;                                                   // The index of the Silo so that the DropZoneLocation can be inferred.

        [XmlAttribute("CommodityType")]
        public string commodityType;                                            // The type of Commodity that can be stored by the Silo.

        [XmlAttribute("MaxCount")]
        public int maxCount;                                                    // The maximum number of objects that can be stored in the Silo.

        [XmlAttribute("Orientation")]
        public string orientation;                                              // The orientation of the Commodity that can be stored by the Silo.

        [XmlElement("StoredCommodityRef")]
        public List<string> storedCommodityRefList = new List<string>();        // List of all the objects currently be stored in the Silo (this + below should not be more than maxCount)

        [XmlElement("ReservedCommodityRef")]
        public List<string> reservedCommodityRefList = new List<string>();      // List of all the objects currently due to be stored in the Silo (this + above should not be more than maxCount)

        

        // Orientation = X_MAJOR, Y_MAJOR

               


    }

}
