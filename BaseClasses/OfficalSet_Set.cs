using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class OfficalSet_Set
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

    [Serializable]
    public enum OfficalSet_State
    {
        NOT_COMPLETED,
        COMPLETED
    }

}
