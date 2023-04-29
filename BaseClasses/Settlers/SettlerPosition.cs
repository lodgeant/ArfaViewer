using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{


        
    [Serializable]
    public class SettlerPosition
    {
        [XmlAttribute("Name")]
        public string Name;

        [XmlElement("Position")]
        public Vect Position { get; set; }





        


        //public string SerializeToString(bool omitDeclaration)
        //{
        //    XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
        //    using (StringWriter textWriter = new Utf8StringWriter())
        //    {
        //        xmlSerializer.Serialize(textWriter, this);
        //        if (omitDeclaration)
        //        {
        //            return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
        //        }
        //        else
        //        {
        //            return textWriter.ToString();
        //        }
        //    }
        //}

        //public class Utf8StringWriter : StringWriter
        //{
        //    // Use UTF8 encoding but write no BOM to the wire
        //    public override Encoding Encoding
        //    {
        //        get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
        //    }
        //}

        //public Tree DeserialiseFromXMLString(String XMLString)
        //{
        //    // ** IMPROVED METHOD **           
        //    var serializer = new XmlSerializer(this.GetType());
        //    using (TextReader reader = new StringReader(XMLString))
        //    {
        //        return (Tree)serializer.Deserialize(reader);
        //    }
        //}

        //public static GlobalMapData GetGlobalMapDataFromDBDataRow(DataRow row)
        //{
        //    GlobalMapData item = new GlobalMapData();
        //    item.Ref = (string)row["REF"];
        //    if (row.Table.Columns.Contains("DATA")) item.Data = (string)row["DATA"];
        //    return item;
        //}

    }

}
