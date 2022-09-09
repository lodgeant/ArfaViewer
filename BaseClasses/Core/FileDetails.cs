using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class FileDetails
    {
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Size")]
        public long Size { get; set; }

        [XmlAttribute("CreatedTS")]
        public DateTime CreatedTS { get; set; }

        [XmlAttribute("LastUpdatedTS")]
        public DateTime LastUpdatedTS { get; set; }

        [XmlAttribute("Data")]
        public string Data { get; set; }

    }

}
