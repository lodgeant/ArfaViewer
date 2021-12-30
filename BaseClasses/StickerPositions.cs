using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class StickerPositions
    {
        [XmlElement("Sticker")]
        public List<Sticker> stickerList = new List<Sticker>();
    }
}
