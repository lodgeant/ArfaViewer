using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class PartList
    {
        [XmlElement("PartListPart")]
        public List<PartListPart> partList = new List<PartListPart>();

        public string SerializeToString(bool omitDeclaration)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                if (omitDeclaration)
                {
                    return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
                }
                else
                {
                    return textWriter.ToString();
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            // Use UTF8 encoding but write no BOM to the wire
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
            }
        }

        public PartList DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (PartList)serializer.Deserialize(reader);
            }
        }



        // ** USEFUL FUNCTIONS **

        public static PartList GetPartList_ForModel(XmlDocument ModelXML)
        {
            try
            {
                // ** Generate Part Count Dictionary **                
                Dictionary<string, int> partCountDict = GetPartCountDictionary_UsingModelXML(ModelXML);

                // ** Generate PartList object **
                PartList pl = new PartList();
                foreach (string partKey in partCountDict.Keys)
                {
                    // ** Get variables **           
                    string LDrawRef = partKey.Split('|')[0];
                    int LDrawColourID = int.Parse(partKey.Split('|')[1]);

                    // ** Generate PartListPart **
                    PartListPart plp = new PartListPart() { LDrawRef = LDrawRef, LDrawColourID = LDrawColourID };                    
                    plp.Qty = partCountDict[partKey];
                    string XMLString = "//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                    plp.QtyFound = ModelXML.SelectNodes(XMLString).Count;
                    pl.partList.Add(plp);
                }
                return pl;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static Dictionary<string, int> GetPartCountDictionary_UsingModelXML(XmlDocument ModelXML)
        {
            Dictionary<string, int> partCountdDict = new Dictionary<string, int>();
            XmlNodeList ModelPartNodeList = ModelXML.SelectNodes("//Part[@IsSubPart='false']");
            foreach (XmlNode partNode in ModelPartNodeList)
            {
                // ** Get variables **
                string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                string LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
                string partKey = LDrawRef + "|" + LDrawColourID;

                // ** Get Part Count details **
                if (partCountdDict.ContainsKey(partKey) == false) partCountdDict.Add(partKey, 0);
                partCountdDict[partKey] += 1;
            }
            return partCountdDict;
        }








    }

}
