using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class OfficalSet_MiniFig
    {
        [XmlAttribute("Ref")]
        public string Ref;

        [XmlAttribute("Description")]
        public string Description;

        [XmlAttribute("Theme")]
        public string Theme;

        [XmlAttribute("SubTheme")]
        public string SubTheme;

        [XmlAttribute("Year")]
        public string Year;

        [XmlAttribute("State")]
        public OfficalSet_State state;

    }

}
