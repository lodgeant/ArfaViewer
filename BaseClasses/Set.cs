using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;


namespace BaseClasses
{
    [Serializable]
    public class Set
    {
        [XmlAttribute("Ref")]
        public string Ref;

        [XmlAttribute("Description")]
        public string Description;

        [XmlElement("LargePartPositions")]
        public LargePartPositions largePartPositions = new LargePartPositions();

        [XmlElement("StickerPositions")]
        public StickerPositions stickerPositions = new StickerPositions();

        [XmlElement("PartList")]
        public PartList partList = new PartList();

        [XmlElement("SubSetList")]
        public SubSetList subSetList = new SubSetList();


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

        public Set DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (Set)serializer.Deserialize(reader);
            }
        }

        public static XmlDocument MergeMiniFigsIntoSetXML(XmlDocument SetXml, Dictionary<string, XmlDocument> MiniFigXMLDict)
        {
            try
            {
                XmlNode ParentPartListNode = SetXml.SelectSingleNode("//PartList");
                XmlNodeList MiniFigNodeList = SetXml.SelectNodes("//SubModel[@SubModelLevel='1' and @LDrawModelType='MINIFIG']");
                foreach (XmlNode ModelNode in MiniFigNodeList)
                {
                    // ** Get variables **
                    string ModelDescription = ModelNode.SelectSingleNode("@Description").InnerXml;
                    string MiniFigRef = ModelDescription.Split('_')[0];                   
                    XmlNode parentStepNode = SetXml.SelectSingleNode("//SubModel[@Description=" + "\"" + ModelDescription + "\"" + " and @SubModelLevel='1' and @LDrawModelType='MINIFIG']//Step[1]");
                    XmlNode PartNodeToInsertBefore = parentStepNode.ChildNodes[0];

                    // ** Get Part nodes to add **
                    if (MiniFigXMLDict.ContainsKey(MiniFigRef))
                    {
                        XmlDocument MiniFigXmlDoc = MiniFigXMLDict[MiniFigRef];
                        XmlNodeList partNodeList = MiniFigXmlDoc.SelectNodes("//Part[@IsSubPart='false']");
                        foreach (XmlNode PartNode in partNodeList)
                        {
                            // ** Get variables **
                            string mfPart_LDrawRef = PartNode.SelectSingleNode("@LDrawRef").InnerXml;
                            string mfPart_LDrawColourID = PartNode.SelectSingleNode("@LDrawColourID").InnerXml;
                            string xmlNodeString = PartNode.OuterXml;

                            // ** ADD PART NODES TO THE RELEVANT STEP IN THE ORIGINAL SET **                       
                            XmlDocument doc = new XmlDocument();
                            doc.LoadXml(xmlNodeString);
                            XmlNode newNode = doc.DocumentElement;
                            XmlNode importNode = parentStepNode.OwnerDocument.ImportNode(newNode, true);
                            parentStepNode.InsertBefore(importNode, PartNodeToInsertBefore);

                            // ** UPDATE THE ORIGINAL SET'S PARTLIST **
                            XmlNode PartListNode = SetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + mfPart_LDrawRef + "' and @LDrawColourID='" + mfPart_LDrawColourID + "']");
                            if (PartListNode != null)
                            {
                                // ** Amend EXISTING PartListPart Node **
                                int origQty = int.Parse(PartListNode.SelectSingleNode("@Qty").InnerXml);
                                PartListNode.SelectSingleNode("@Qty").InnerXml = (origQty + 1).ToString();
                            }
                            else
                            {
                                // ** Create NEW PartListPart node **
                                PartListPart plp = new PartListPart() { LDrawRef = mfPart_LDrawRef, LDrawColourID = int.Parse(mfPart_LDrawColourID), Qty = 1 };

                                // add node to partlist
                                string PartList_xmlNodeString = HelperFunctions.RemoveAllNamespaces(plp.SerializeToString(true));
                                XmlDocument pldoc = new XmlDocument();
                                pldoc.LoadXml(PartList_xmlNodeString);
                                XmlNode newPLNode = pldoc.DocumentElement;
                                XmlNode importPLNode = ParentPartListNode.OwnerDocument.ImportNode(newPLNode, true);
                                ParentPartListNode.AppendChild(importPLNode);
                            }
                        }
                    }
                }
                return SetXml;
            }
            catch (Exception)
            {                
                return null;
            }
        }

             
        


    }
}
