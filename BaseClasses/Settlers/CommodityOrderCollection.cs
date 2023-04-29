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
    public class CommodityOrderCollection
    {
        [XmlElement("CommodityOrder")]
        public List<CommodityOrder> Items { get; set; }

        public CommodityOrderCollection()
        {
            Items = new List<CommodityOrder>();
        }




       

        


    }

}
