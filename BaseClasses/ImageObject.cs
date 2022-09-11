using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class ImageObject
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Data")]
        public byte[] Data { get; set; }

    }


}
