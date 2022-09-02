using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Linq;
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

        public static Dictionary<string, int> GetPartCountDictionary_UsingPartNodeList(XmlNodeList PartNodeList)
        {
            Dictionary<string, int> partCountdDict = new Dictionary<string, int>();
            foreach (XmlNode partNode in PartNodeList)
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
            catch (Exception)
            {
                return null;
            }
        }

        public static PartList GetPartList_FromPartNodeList(XmlNodeList PartNodeList)
        {
            try
            {       
                // ** Generate Part Count Dictionary **                
                Dictionary<string, int> partCountDict = GetPartCountDictionary_UsingPartNodeList(PartNodeList);

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
                    //string XMLString = "//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                    //plp.QtyFound = ModelXML.SelectNodes(XMLString).Count;
                    pl.partList.Add(plp);
                }
                return pl;
            }
            catch (Exception)
            {
                return null;
            }
        }





        public static PartList GetPartList_FromSetInstructionsCollection(SetInstructionsCollection siColl)
        {
            try
            {
                PartList pl = new PartList();
                foreach (SetInstructions si in siColl.SetInstructionsList)
                {
                    XmlDocument MFSetXML = new XmlDocument();
                    MFSetXML.LoadXml(si.Data);
                    XmlNodeList MFpartNodeList = MFSetXML.SelectNodes("//PartListPart");
                    foreach (XmlNode node in MFpartNodeList)
                    {
                        PartListPart plp = new PartListPart();
                        plp.LDrawRef = node.SelectSingleNode("@LDrawRef").InnerXml;
                        plp.LDrawColourID = int.Parse(node.SelectSingleNode("@LDrawColourID").InnerXml);
                        plp.Qty = int.Parse(node.SelectSingleNode("@Qty").InnerXml);
                        pl.partList.Add(plp);
                    }
                }
                return pl;
            }
            catch (Exception)
            {
                return null;
            }
        }



        //public static DataTable GeneratePartListTable_FromRebrickableJSON(string JSONString)
        //{
        //    DataTable partListTable = new DataTable("partListTable", "partListTable");
        //    //string LDrawRef_debug = "";
        //    try
        //    {
        //        // ** GENERATE COLUMNS **
        //        partListTable.Columns.Add("Part Image", typeof(Bitmap));
        //        partListTable.Columns.Add("LDraw Ref", typeof(string));
        //        partListTable.Columns.Add("LDraw Ref List", typeof(string));
        //        partListTable.Columns.Add("LDraw Description", typeof(string));
        //        partListTable.Columns.Add("LDraw Colour ID", typeof(int));
        //        partListTable.Columns.Add("LDraw Colour Name", typeof(string));
        //        partListTable.Columns.Add("Colour Image", typeof(Bitmap));
        //        partListTable.Columns.Add("Qty", typeof(int));

        //        // ** Load JSON string to XML **                
        //        XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(JSONString), new XmlDictionaryReaderQuotas()));
        //        string XMLString = xml.ToString();
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(XMLString);
        //        XmlNodeList partItemList = doc.SelectNodes("//item[@type='object' and is_spare='false']");

        //        // ** Cycle through nodes and generate table rows **               
        //        foreach (XmlNode partNode in partItemList)
        //        {
        //            // ** GET LDRAW VARIABLES **                    
        //            string LDrawRef = "";
        //            XmlNodeList nodeList = partNode.SelectNodes("part/external_ids/LDraw/item");
        //            foreach (XmlNode node in nodeList)
        //            {
        //                LDrawRef += node.InnerText + "|";
        //            }
        //            //LDrawRef_debug = LDrawRef;
        //            int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/external_ids/LDraw/ext_ids/item[1]").InnerXml);
        //            int Qty = int.Parse(partNode.SelectSingleNode("quantity").InnerXml);

        //            // ** Build row and add to table **                     
        //            DataRow newRow = partListTable.NewRow();
        //            newRow["LDraw Ref List"] = LDrawRef;
        //            newRow["LDraw Colour ID"] = LDrawColourID;
        //            newRow["Qty"] = Qty;
        //            partListTable.Rows.Add(newRow);
        //        }
        //        return partListTable;
        //    }
        //    catch (Exception)
        //    {
        //        return partListTable;
        //    }
        //}




    }

}
