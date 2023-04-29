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
    public class RequestCollection
    {
        [XmlElement("Request")]
        public List<Request> Items { get; set; }

        public RequestCollection()
        {
            Items = new List<Request>();
        }




       

        


    }

}
