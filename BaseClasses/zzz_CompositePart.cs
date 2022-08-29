using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;


namespace BaseClasses
{
    //[Serializable]
    //public class zzz_CompositePart
    //{
    //    [XmlAttribute("ParentLDrawRef")]
    //    public string ParentLDrawRef { get; set; }

    //    [XmlAttribute("LDrawRef")]
    //    public string LDrawRef { get; set; }

    //    [XmlAttribute("LDrawDescription")]
    //    public string LDrawDescription { get; set; }

    //    [XmlAttribute("LDrawColourID")]
    //    public int LDrawColourID { get; set; }

    //    [XmlAttribute("PosX")]
    //    public float PosX { get; set; }

    //    [XmlAttribute("PosY")]
    //    public float PosY { get; set; }

    //    [XmlAttribute("PosZ")]
    //    public float PosZ { get; set; }

    //    [XmlAttribute("RotX")]
    //    public float RotX { get; set; }

    //    [XmlAttribute("RotY")]
    //    public float RotY { get; set; }

    //    [XmlAttribute("RotZ")]
    //    public float RotZ { get; set; }

    //    public string SerializeToString(bool omitDeclaration)
    //    {
    //        XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
    //        using (StringWriter textWriter = new Utf8StringWriter())
    //        {
    //            xmlSerializer.Serialize(textWriter, this);
    //            if (omitDeclaration)
    //            {
    //                return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
    //            }
    //            else
    //            {
    //                return textWriter.ToString();
    //            }
    //        }
    //    }

    //    public class Utf8StringWriter : StringWriter
    //    {
    //        // Use UTF8 encoding but write no BOM to the wire
    //        public override Encoding Encoding
    //        {
    //            get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
    //        }
    //    }

    //    public zzz_CompositePart DeserialiseFromXMLString(String XMLString)
    //    {
    //        // ** IMPROVED METHOD **           
    //        var serializer = new XmlSerializer(this.GetType());
    //        using (TextReader reader = new StringReader(XMLString))
    //        {
    //            return (zzz_CompositePart)serializer.Deserialize(reader);
    //        }
    //    }

    //}

}
