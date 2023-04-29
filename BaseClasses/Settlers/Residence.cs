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
    public class Residence : SettlerObject
    {
        [XmlAttribute("CarriersGeneratedCount")]
        public int CarriersGeneratedCount { get; set; }

        [XmlAttribute("MaxCarriersAllowed")]
        public int MaxCarriersAllowed { get; set; }



    }

}
