using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class User
    {
        [XmlAttribute("BrickBalance")]
        public int BrickBalance;

    }

}
