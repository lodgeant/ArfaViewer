using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class FBXDetails
    {
        [XmlAttribute("LDrawRef")]
        public string LDrawRef { get; set; }

        [XmlAttribute("FBXCount")]
        public int FBXCount { get; set; }

        [XmlAttribute("FBXSize")]
        public long FBXSize { get; set; }

        [XmlAttribute("AllFBXExist")]
        public bool AllFBXExist { get; set; }

    }

}
