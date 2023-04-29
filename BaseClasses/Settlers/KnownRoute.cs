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
    public class KnownRoute
    {
        [XmlAttribute("Name")]                         
        public string Name { get; set; }

        [XmlElement("RouteList")]
        public List<Vect> RouteList { get; set; }


        public KnownRoute()
        {
            RouteList = new List<Vect>();            
        }





    }

}
