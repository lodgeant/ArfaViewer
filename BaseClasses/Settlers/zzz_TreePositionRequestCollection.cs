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
    public class zzz_TreePositionRequestCollection
    {
        [XmlElement("TreePositionRequest")]
        public List<zzz_TreePositionRequest> Items { get; set; }

        public zzz_TreePositionRequestCollection()
        {
            Items = new List<zzz_TreePositionRequest>();
        }


              

        


    }

}
