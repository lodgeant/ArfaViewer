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
    public class Forester : SettlerObject
    {
        [XmlElement("CurrentTreeType")]
        public string CurrentTreeType { get; set; }

        [XmlElement("TreePlantStartTime")]
        public float TreePlantStartTime { get; set; }

        [XmlElement("TreePlantDuration")]
        public float TreePlantDuration { get; set; }



        



    }

}
