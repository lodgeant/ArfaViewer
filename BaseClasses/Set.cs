using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Windows.Forms;
using System.Linq;




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
       


        public static Set GenerateBaseSet(string setRef, string description, string type)
        {
            Set set = new Set() { Ref = setRef, Description = description };
            SubSet ss = new SubSet() { Ref = setRef + "_1", Description = description, SubSetType = type };
            SubSetList ssl = new SubSetList();
            ssl.subSetList.Add(ss);
            set.subSetList = ssl;

            BuildInstructions bi = new BuildInstructions();
            ss.buildInstructions = bi;

            SubModel fm = new SubModel(){ Ref = "S1", Description = description, lDrawModelType = SubModel.LDrawModelType.FINAL_MODEL, SubModelLevel = 0};
            bi.SubModel = fm;

            SubModel m = new SubModel() { Ref = "S2", Description = "Model 1", lDrawModelType = SubModel.LDrawModelType.MODEL, SubModelLevel = 1 };
            fm.subModelList.Add(m);

            Step s = new Step() { PureStepNo = 1, StepLevel = 1 };
            m.stepList.Add(s);

            return set;
        }

        public static TreeNode GetSetTreeViewFromSetXML(XmlDocument setXML, bool showPages, bool showSteps, bool showParts, bool showPlacementMovements)
        {
            TreeNode SetTN = new TreeNode();
            List<string> nodeList = new List<string>();   // ## Debug
            if (setXML != null)
            {
                // ** POPULATE Set DETAILS **                    
                string SetRef = setXML.SelectSingleNode("//Set/@Ref").InnerXml;
                string SetDescription = setXML.SelectSingleNode("//Set/@Description").InnerXml;
                SetTN = new TreeNode() { Text = SetRef + "|" + SetDescription, Tag = "SET|" + SetRef, ImageIndex = 0, SelectedImageIndex = 0 };
                nodeList.Add("SET|" + SetRef + "|" + SetDescription);   // ## Debug

                // ** POPULATE ALL SubSet DETAILS **                    
                if (setXML.SelectNodes("//SubSet") != null)
                {
                    XmlNodeList SubSetNodeList = setXML.SelectNodes("//SubSet");
                    foreach (XmlNode SubSetNode in SubSetNodeList)
                    {
                        // ** GET VARIABLES **
                        string SubSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;
                        string SubSetDescription = SubSetNode.SelectSingleNode("@Description").InnerXml;
                        TreeNode SubSetTN = new TreeNode() { Text = SubSetRef + "|" + SubSetDescription, Tag = "SUBSET|" + SubSetRef, ImageIndex = 1, SelectedImageIndex = 1 };
                        SetTN.Nodes.Add(SubSetTN);
                        nodeList.Add("SUBSET|" + SubSetRef + "|" + SubSetDescription);   // ## Debug

                        // ** POPULATE ALL MODEL DETAILS **
                        if (setXML.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']") != null)
                        {
                            XmlNodeList ModelNodeList = setXML.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']");
                            foreach (XmlNode ModelNode in ModelNodeList)
                            {
                                string ModelRef = ModelNode.SelectSingleNode("@Ref").InnerXml;
                                string ModelDescription = ModelDescription = ModelNode.SelectSingleNode("@Description").InnerXml;
                                string ModelType = ModelNode.SelectSingleNode("@LDrawModelType").InnerXml;
                                TreeNode modelTN = new TreeNode(ModelRef + "|" + ModelDescription);
                                modelTN.Tag = "MODEL|" + SubSetRef + "|" + ModelRef;
                                int imageIndex = 2;
                                if (ModelType.Equals("MINIFIG")) imageIndex = 7;                                
                                modelTN.ImageIndex = imageIndex;
                                modelTN.SelectedImageIndex = imageIndex;
                                SubSetTN.Nodes.Add(modelTN);
                                nodeList.Add("MODEL|" + ModelRef + "|" + ModelDescription);   // ## Debug

                                // ** POPULATE ALL SUBMODEL & STEP DETAILS **                                
                                List<TreeNode> treeNodeList = GenerateTreeNodeList_UsingXmlNodeList(ModelNode.ChildNodes, showPages, showSteps, showParts, showPlacementMovements);
                                modelTN.Nodes.AddRange(treeNodeList.ToArray());                                                             
                            }
                        }
                    }
                }
            }
            return SetTN;
        }

        private static List<TreeNode> GenerateTreeNodeList_UsingXmlNodeList(XmlNodeList childNodeList, bool showPages, bool showSteps, bool showParts, bool showPlacementMovements)
        {
            // ** Define which node types to ignore **
            HashSet<String> NodeTypesToIgnore = new HashSet<string>() { "#COMMENT" };

            // ** Cycle through all nodes **
            List<TreeNode> treeNodeList = new List<TreeNode>();
            int nodeStepIndex = 0;
            int nodePlacementIndex = 0;
            foreach (XmlNode xmlNode in childNodeList)
            {
                String nodeType = xmlNode.LocalName.ToUpper();
                if (NodeTypesToIgnore.Contains(nodeType) == false)
                {
                    TreeNode treeNode = new TreeNode();
                    if (nodeType.Equals("SUBMODEL"))
                    {
                        string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                        string SubModelRef = xmlNode.SelectSingleNode("@Ref").InnerXml;
                        string SubModelDescription = xmlNode.SelectSingleNode("@Description").InnerXml;                        
                        treeNode = new TreeNode() {Text = SubModelRef + "|" + SubModelDescription, Tag = nodeType + "|" + parentSubSetRef + "|" + SubModelRef + "|", ImageIndex = 3, SelectedImageIndex = 3 };
                        treeNodeList.Add(treeNode);
                    }
                    else if (nodeType.Equals("STEP"))
                    {
                        if(showSteps)
                        {
                            string PureStepNo = xmlNode.SelectSingleNode("@PureStepNo").InnerXml;
                            string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                            string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                            //string parentSubModelRef = xmlNode.SelectSingleNode("parent::SubModel/@Ref").InnerXml;

                            String StepBook = "";
                            String StepPage = "";
                            String extraString = "";
                            if (showPages)
                            {
                                if (xmlNode.SelectSingleNode("@StepBook") != null)
                                {
                                    StepBook = xmlNode.SelectSingleNode("@StepBook").InnerXml;
                                    StepPage = xmlNode.SelectSingleNode("@StepPage").InnerXml;
                                    if (StepBook != "0" && StepPage != "0")
                                    {
                                        extraString = " [b" + StepBook + ".p" + StepPage + "]";
                                    }
                                }
                            }                            
                            treeNode = new TreeNode() { Text = PureStepNo + extraString, Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + PureStepNo, ImageIndex = 4, SelectedImageIndex = 4 };
                            treeNodeList.Add(treeNode);
                            nodeStepIndex = 0;
                            // ** Update Colour of Step (if required) **
                        }
                    }
                    else if (nodeType.Equals("PART"))
                    {
                        if (showParts)
                        {
                            string LDrawRef = xmlNode.SelectSingleNode("@LDrawRef").InnerXml;
                            String LDrawColourID = xmlNode.SelectSingleNode("@LDrawColourID").InnerXml;
                            string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                            string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                            string parentPureStepNo = xmlNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml;
                            treeNode = new TreeNode() { Text = LDrawRef + "|" + LDrawColourID, Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + parentPureStepNo + "|" + LDrawRef + "|" + LDrawColourID + "|" + nodeStepIndex, ImageIndex = 5, SelectedImageIndex = 5 };
                            if (LDrawRef.Contains("stk"))
                            {
                                treeNode.ImageIndex = 9;
                                treeNode.SelectedImageIndex = 9;
                            }
                            treeNodeList.Add(treeNode);
                            nodeStepIndex += 1;
                            nodePlacementIndex = 0;
                        }                           
                    }
                    else if (nodeType.Equals("PLACEMENTMOVEMENT"))
                    {
                        if (showPlacementMovements)
                        {
                            string Axis = xmlNode.SelectSingleNode("@Axis").InnerXml;
                            String Value = xmlNode.SelectSingleNode("@Value").InnerXml;
                            string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                            string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                            string parentPureStepNo = xmlNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml;
                            string LDrawRef = xmlNode.SelectSingleNode("ancestor::Part/@LDrawRef").InnerXml;
                            String LDrawColourID = xmlNode.SelectSingleNode("ancestor::Part/@LDrawColourID").InnerXml;
                            treeNode = new TreeNode() { Text = Axis + "=" + Value, Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + parentPureStepNo + "|" + LDrawRef + "|" + LDrawColourID + "|" + nodePlacementIndex, ImageIndex = 8, SelectedImageIndex = 8 };
                            treeNodeList.Add(treeNode);
                            nodePlacementIndex += 1;
                        }
                    }
                    if (xmlNode.HasChildNodes) treeNode.Nodes.AddRange(GenerateTreeNodeList_UsingXmlNodeList(xmlNode.ChildNodes, showPages, showSteps, showParts, showPlacementMovements).ToArray());
                }
            }
            return treeNodeList;
        }

        //public static XmlDocument MergeMiniFigsIntoSetXML(XmlDocument SetXml, Dictionary<string, XmlDocument> MiniFigXMLDict)
        //{
        //    try
        //    {
        //        XmlNode ParentPartListNode = SetXml.SelectSingleNode("//PartList");
        //        XmlNodeList MiniFigNodeList = SetXml.SelectNodes("//SubModel[@SubModelLevel='1' and @LDrawModelType='MINIFIG']");
        //        foreach (XmlNode ModelNode in MiniFigNodeList)
        //        {
        //            // ** Get variables **
        //            string ModelDescription = ModelNode.SelectSingleNode("@Description").InnerXml;
        //            string MiniFigRef = ModelDescription.Split('_')[0];
        //            XmlNode parentStepNode = SetXml.SelectSingleNode("//SubModel[@Description=" + "\"" + ModelDescription + "\"" + " and @SubModelLevel='1' and @LDrawModelType='MINIFIG']//Step[1]");
        //            XmlNode PartNodeToInsertBefore = parentStepNode.ChildNodes[0];

        //            // ** Get Part nodes to add **
        //            if (MiniFigXMLDict.ContainsKey(MiniFigRef))
        //            {
        //                XmlDocument MiniFigXmlDoc = MiniFigXMLDict[MiniFigRef];
        //                XmlNodeList partNodeList = MiniFigXmlDoc.SelectNodes("//Part[@IsSubPart='false']");
        //                foreach (XmlNode PartNode in partNodeList)
        //                {
        //                    // ** Get variables **
        //                    string mfPart_LDrawRef = PartNode.SelectSingleNode("@LDrawRef").InnerXml;
        //                    string mfPart_LDrawColourID = PartNode.SelectSingleNode("@LDrawColourID").InnerXml;
        //                    string xmlNodeString = PartNode.OuterXml;

        //                    // ** ADD PART NODES TO THE RELEVANT STEP IN THE ORIGINAL SET **                       
        //                    XmlDocument doc = new XmlDocument();
        //                    doc.LoadXml(xmlNodeString);
        //                    XmlNode newNode = doc.DocumentElement;
        //                    XmlNode importNode = parentStepNode.OwnerDocument.ImportNode(newNode, true);
        //                    parentStepNode.InsertBefore(importNode, PartNodeToInsertBefore);

        //                    // ** UPDATE THE ORIGINAL SET'S PARTLIST **
        //                    XmlNode PartListNode = SetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + mfPart_LDrawRef + "' and @LDrawColourID='" + mfPart_LDrawColourID + "']");
        //                    if (PartListNode != null)
        //                    {
        //                        // ** Amend EXISTING PartListPart Node **
        //                        int origQty = int.Parse(PartListNode.SelectSingleNode("@Qty").InnerXml);
        //                        PartListNode.SelectSingleNode("@Qty").InnerXml = (origQty + 1).ToString();
        //                    }
        //                    else
        //                    {
        //                        // ** Create NEW PartListPart node **
        //                        PartListPart plp = new PartListPart() { LDrawRef = mfPart_LDrawRef, LDrawColourID = int.Parse(mfPart_LDrawColourID), Qty = 1 };

        //                        // add node to partlist
        //                        string PartList_xmlNodeString = HelperFunctions.RemoveAllNamespaces(plp.SerializeToString(true));
        //                        XmlDocument pldoc = new XmlDocument();
        //                        pldoc.LoadXml(PartList_xmlNodeString);
        //                        XmlNode newPLNode = pldoc.DocumentElement;
        //                        XmlNode importPLNode = ParentPartListNode.OwnerDocument.ImportNode(newPLNode, true);
        //                        ParentPartListNode.AppendChild(importPLNode);
        //                    }
        //                }
        //            }
        //        }
        //        return SetXml;
        //    }
        //    catch (Exception)
        //    {
        //        return null;
        //    }
        //}

        public static List<string> GetMinFigSetRefsFromSetXML(XmlDocument setXML)
        {
            XmlNodeList MiniFigNodeList = setXML.SelectNodes("//SubModel[@SubModelLevel='1' and @LDrawModelType='MINIFIG']");
            List<string> MiniFigSetList = MiniFigNodeList.Cast<XmlNode>()
                                           .Select(x => x.SelectSingleNode("@Description").InnerXml.Split('_')[0])
                                           .OrderBy(x => x).ToList();
            return MiniFigSetList;
        }

        public static XmlDocument MergeMiniFigsIntoSetXML(XmlDocument SetXml, SetInstructionsCollection MiniFigSetInstructions)
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
                    SetInstructions si = (from r in MiniFigSetInstructions.SetInstructionsList
                                          where r.Ref.Equals(MiniFigRef)
                                          select r).FirstOrDefault();
                    if (si != null)
                    {
                        XmlDocument MiniFigXmlDoc = new XmlDocument();
                        MiniFigXmlDoc.LoadXml(si.Data);
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
