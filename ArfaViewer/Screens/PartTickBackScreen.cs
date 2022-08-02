using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Diagnostics;
using System.Threading;
using ScintillaNET;
using iTextSharp.text.pdf;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using BaseClasses;





namespace Generator
{
    public partial class PartTickBackScreen : Form
    {
        // ** Variables **
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private XmlDocument currentSetXml;
        private Scintilla TextArea;
        private string SelectedNodeTag = "";       
        private Dictionary<string, int> ScrollingRowIndexDict = new Dictionary<string, int>();
        private Dictionary<string, int> ScrollingPnlIndexDict = new Dictionary<string, int>();



        public PartTickBackScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Part Tick Back";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                //log.Info(".......................................................................GENERATOR SCREEN STARTED.......................................................................");

                #region ** FORMAT SUMMARIES **
                string[] DGnames = new string[] { "dgObjectPartListSummary", "dgSetPartListSummary" };
                foreach (string dgName in DGnames)
                {
                    DataGridView dgv = (DataGridView)(this.Controls.Find(dgName, true)[0]);
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToOrderColumns = true;
                    dgv.MultiSelect = true;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    dgv.EnableHeadersVisualStyles = false;
                    //dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dgv.ColumnHeadersHeight = 30;
                }
                lblSetPartListCount.Text = "";
                lblObjectPartListCount.Text = "";
                lblStatus.Text = "";
                #endregion

                // ** CREATE ENTRIES FOR SCROLLING INDEX DICTIONARY **
                ScrollingRowIndexDict.Add("dgObjectPartListSummary", -1);
                ScrollingRowIndexDict.Add("dgSetPartListSummary", -1);

                #region ** ADD MAIN HEADER LINE TOOLSTRIP ITEMS **
                toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                btnExit,
                                toolStripSeparator1,                                                               
                                lblTickBackRef,
                                fldTickBackRef,
                                btnLoadTickBack,
                                btnSaveTickBack,
                                btnDeleteTickBack,
                                toolStripSeparator2,
                                lblSetRef,
                                fldSetRef,
                                btnLoadFromSet,
                                //new ToolStripControlHost(chkShowBig),  
                                });
                #endregion

                toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                                
                                new ToolStripControlHost(chkSelectedObjectShowBig),
                                new ToolStripControlHost(chkSelectedObjectShowMissingOnly)
                                });
                toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                new ToolStripControlHost(chkWholeSetShowBig),
                                });


                toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                new ToolStripControlHost(chkWholeSetShowMissingOnly),
                                });



                // ** Set up Scintilla **
                SetupScintillaPanel1();

                fldTickBackRef.Text = "621-1";
                fldSetRef.Text = "621-1";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #region ** SCINTILLA FUNCTIONS **

        private const int NUMBER_MARGIN = 1;
        private const int FOLDING_MARGIN = 3;
        private const bool CODEFOLDING_CURCULAR = false;

        private void SetupScintillaPanel1()
        {
            try
            {
                // ** Initialise Scintilla Control Surface **
                TextArea = new ScintillaNET.Scintilla();
                panel1.Controls.Add(TextArea);
                TextArea.Dock = System.Windows.Forms.DockStyle.Fill;
                TextArea.WrapMode = WrapMode.None;
                TextArea.IndentationGuides = IndentView.LookBoth;

                // Configuring the default style with properties **
                TextArea.StyleResetDefault();
                TextArea.Styles[Style.Default].Font = "Consolas";
                TextArea.Styles[Style.Default].Size = 8;
                TextArea.Styles[Style.Default].BackColor = Color.Black;
                TextArea.Styles[Style.Default].ForeColor = Color.White;

                // ** Number Margin **
                TextArea.Styles[Style.LineNumber].ForeColor = Color.White;
                TextArea.Styles[Style.LineNumber].BackColor = Color.Black;
                TextArea.Styles[Style.IndentGuide].ForeColor = Color.White;
                TextArea.Styles[Style.IndentGuide].BackColor = Color.Black;
                var nums = TextArea.Margins[NUMBER_MARGIN];
                nums.Width = 30;
                nums.Type = MarginType.Number;
                nums.Sensitive = true;
                nums.Mask = 0;

                // ** Code Folding **
                //TextArea.SetFoldMarginColor(true, Color.Black);
                //TextArea.SetFoldMarginHighlightColor(true, Color.Black);
                //TextArea.SetProperty("fold", "1");
                //TextArea.SetProperty("fold.compact", "1");

                //TextArea.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
                //TextArea.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
                //TextArea.Margins[FOLDING_MARGIN].Sensitive = true;
                //TextArea.Margins[FOLDING_MARGIN].Width = 20;
                //for (int i = 25; i <= 31; i++)
                //{
                //    TextArea.Markers[i].SetForeColor(Color.Black);
                //    TextArea.Markers[i].SetBackColor(Color.White);
                //}

                //TextArea.Markers[Marker.Folder].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
                //TextArea.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
                //TextArea.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
                //TextArea.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
                //TextArea.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
                //TextArea.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
                //TextArea.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

                //TextArea.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);



                // ** UPDATE LEXER TO XML **
                TextArea.Lexer = Lexer.Xml;
                TextArea.StyleClearAll();

                // ** Configure the CPP Lexer styles **
                TextArea.Styles[Style.Xml.Comment].ForeColor = Color.Gray;
                TextArea.Styles[Style.Xml.Tag].ForeColor = Color.White;
                TextArea.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
                TextArea.Styles[Style.Xml.DoubleString].ForeColor = Color.Yellow;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnSetStructureRefresh_Click(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        private void btnLoadFromSet_Click(object sender, EventArgs e)
        {
            LoadFromSet();
        }

        private void btnSaveTickBack_Click(object sender, EventArgs e)
        {
            SaveTickBack();
        }

        private void btnLoadTickBack_Click(object sender, EventArgs e)
        {
            LoadTickBack();
        }

        private void btnDeleteTickBack_Click(object sender, EventArgs e)
        {
            DeleteTickBackk();
        }

        private void chkShowBig_CheckedChanged(object sender, EventArgs e)
        {            
            //if (dgObjectPartListSummary.Rows.Count > 0) AdjustPartListSummaryRowFormatting(dgObjectPartListSummary);
            //if (dgSetPartListSummary.Rows.Count > 0) AdjustPartListSummaryRowFormatting(dgSetPartListSummary);
        }

        private void chkSelectedObjectShowBig_CheckedChanged(object sender, EventArgs e)
        {
            if (dgObjectPartListSummary.Rows.Count > 0) AdjustPartListSummaryRowFormatting(dgObjectPartListSummary);
        }

        private void chkWholeSetShowBig_CheckedChanged(object sender, EventArgs e)
        {
            if (dgSetPartListSummary.Rows.Count > 0) AdjustPartListSummaryRowFormatting(dgSetPartListSummary);
        }



        #endregion

        #region ** TICKBACK FUNCTIONS **

        private void LoadTickBack()
        {
            try
            {
                // ** Validation Checks **
                if (fldTickBackRef.Text.Equals(""))
                {
                    throw new Exception("No TickBack Ref entered...");
                }
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "tickback-xmls").GetBlobClient(fldTickBackRef.Text + ".xml");
                if (blob.Exists() == false)
                {
                    throw new Exception("TickBack " + fldTickBackRef.Text + " not found...");
                }

                // ** LOAD Set XML into Object **   
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(xmlString);

                // ** Clear fields **
                //ClearAllFields();

                // ** Generate Treeview then Refresh screen **
                GenerateTreeview();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveTickBack()
        {
            try
            {
                // ** Validation Checks **
                if (fldTickBackRef.Text.Equals(""))
                {
                    throw new Exception("No TickBack Ref entered...");
                }
                string tickBackRef = fldTickBackRef.Text;

                // ** SAVE TICKBACK **
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "tickback-xmls").GetBlobClient(tickBackRef + ".xml");
                //string flushedXML = new Set().DeserialiseFromXMLString(currentSetXml.OuterXml).SerializeToString(true);
                //byte[] bytes = Encoding.UTF8.GetBytes(flushedXML);
                byte[] bytes = Encoding.UTF8.GetBytes(currentSetXml.OuterXml);
                using (var ms = new MemoryStream(bytes))
                {
                    blob.Upload(ms, true);
                }
                MessageBox.Show("TickBack " + tickBackRef + " saved...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteTickBackk()
        {
            try
            {
                // ** Validation Checks **
                if (fldTickBackRef.Text.Equals(""))
                {
                    throw new Exception("No TickBack Ref entered...");
                }
                string tickBackRef = fldTickBackRef.Text;
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "tickback-xmls").GetBlobClient(tickBackRef + ".xml");
                if (blob.Exists() == false)
                {
                    throw new Exception("TickBack " + tickBackRef + " not found...");
                }

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // ** Delete Set **
                    blob.Delete();

                    // ** Clear all fields **                    
                    currentSetXml = null;
                    GenerateTreeview();
                    RefreshScreen();

                    // ** Show confirm **
                    MessageBox.Show("TickBack " + tickBackRef + " deleted...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadFromSet()
        {
            try
            {
                // ** Validation Checks **
                if (fldSetRef.Text.Equals(""))
                {
                    throw new Exception("No Set Ref entered...");
                }
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(fldSetRef.Text + ".xml");
                if (blob.Exists() == false)
                {
                    throw new Exception("Set " + fldSetRef.Text + " not found...");
                }

                // ** LOAD Set XML into Object **   
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(xmlString);

                #region ** MERGE STANDALONE MINIFIG XML's INTO SET XML ** 
                XmlNodeList MiniFigNodeList = currentSetXml.SelectNodes("//SubModel[@SubModelLevel='1' and @LDrawModelType='MINIFIG']");
                List<string> MiniFigSetList = MiniFigNodeList.Cast<XmlNode>()
                                               .Select(x => x.SelectSingleNode("@Description").InnerXml.Split('_')[0])
                                               .OrderBy(x => x).ToList();
                Dictionary<string, XmlDocument> MiniFigXMLDict = new Dictionary<string, XmlDocument>();
                foreach (string MiniFigRef in MiniFigSetList)
                {
                    // ** Get the Set XML doc for the MiniFig **                   
                    blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(MiniFigRef + ".xml");
                    if (blob.Exists())
                    {
                        // ** Get MiniFig XML **
                        XmlDocument MiniFigXmlDoc = new XmlDocument();
                        fileContent = new byte[blob.GetProperties().Value.ContentLength];
                        using (var ms = new MemoryStream(fileContent))
                        {
                            blob.DownloadTo(ms);
                        }
                        xmlString = Encoding.UTF8.GetString(fileContent);
                        MiniFigXmlDoc.LoadXml(xmlString);
                        if (MiniFigXMLDict.ContainsKey(MiniFigRef) == false)
                        {
                            MiniFigXMLDict.Add(MiniFigRef, MiniFigXmlDoc);
                        }
                    }
                }
                if (MiniFigXMLDict.Count > 0)
                {
                    currentSetXml = Set.MergeMiniFigsIntoSetXML(currentSetXml, MiniFigXMLDict);
                }
                #endregion

                // ** Clear fields **
                //ClearAllFields();
                SelectedNodeTag = "";
                fldTickBackRef.Text = "";

                // ** Generate Treeview then Refresh screen **
                GenerateTreeview();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** COLUMN HEADER CLICK FUNCTIONS **

        private void dgSetPartListSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustPartListSummaryRowFormatting(dgSetPartListSummary);
        }

        private void dgObjectPartListSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustPartListSummaryRowFormatting(dgObjectPartListSummary);
        }

        #endregion

        //private TreeNode lastSelectedTreeNode;
        //private string lastSelectedNodeFullPath = "";

        private void tvSetSummary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                SelectedNodeTag = tvSetSummary.SelectedNode.Tag.ToString();
                RefreshScreen();


                //fldSelectedNode.Text = tvSetSummary.SelectedNode.FullPath;
                //lastSelectedNodeFullPath = tvSetSummary.SelectedNode.FullPath;

                //if (lastSelectedTreeNode != null)
                //{
                //    lastSelectedTreeNode.BackColor = Color.White;
                //    lastSelectedTreeNode.ForeColor = Color.Black;
                //}
                //e.Node.BackColor = Color.CornflowerBlue;
                //e.Node.ForeColor = Color.White;
                //lastSelectedTreeNode = e.Node;

                //RefreshScreen();

                //lastSelectedNode = tvSetSummary.SelectedNode;
                //lastSelectedNodeFullPath = tvSetSummary.SelectedNode.FullPath;
                //if (tvSetSummary.SelectedNode.Tag.ToString() != SelectedNodeTag)
                //{
                //    SelectedNodeTag = tvSetSummary.SelectedNode.Tag.ToString();
                //    RefreshScreen();
                //}

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ** GENERATE TREEVIEW FUNCTIONS **

        private void GenerateTreeview()
        {
            try
            {
                tvSetSummary.Nodes.Clear();

                #region ** UPDATE TEEVIEW **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Processing refresh...");
                if (currentSetXml != null)
                {
                    List<string> nodeList = new List<string>();

                    #region ** POPULATE Set DETAILS **                    
                    string SetRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;
                    string SetDescription = currentSetXml.SelectSingleNode("//Set/@Description").InnerXml;
                    TreeNode SetTN = new TreeNode(SetRef + "|" + SetDescription);
                    SetTN.Tag = "SET|" + SetRef;
                    SetTN.ImageIndex = 0;
                    SetTN.SelectedImageIndex = 0;
                    nodeList.Add("SET|" + SetRef + "|" + SetDescription);
                    #endregion

                    #region ** POPULATE ALL SubSet DETAILS **                    
                    if (currentSetXml.SelectNodes("//SubSet") != null)
                    {
                        XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                        foreach (XmlNode SubSetNode in SubSetNodeList)
                        {
                            // ** GET VARIABLES **
                            string SubSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;
                            string SubSetDescription = SubSetNode.SelectSingleNode("@Description").InnerXml;
                            TreeNode SubSetTN = new TreeNode(SubSetRef + "|" + SubSetDescription);
                            SubSetTN.Tag = "SUBSET|" + SubSetRef;
                            SubSetTN.ImageIndex = 1;
                            SubSetTN.SelectedImageIndex = 1;
                            nodeList.Add("SUBSET|" + SubSetRef + "|" + SubSetDescription);
                            SetTN.Nodes.Add(SubSetTN);

                            #region ** POPULATE ALL MODEL DETAILS **
                            if (currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']") != null)
                            {
                                XmlNodeList ModelNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']");
                                foreach (XmlNode ModelNode in ModelNodeList)
                                {
                                    string ModelRef = ModelNode.SelectSingleNode("@Ref").InnerXml;
                                    string ModelDescription = ModelDescription = ModelNode.SelectSingleNode("@Description").InnerXml;
                                    string ModelType = ModelNode.SelectSingleNode("@LDrawModelType").InnerXml;
                                    TreeNode modelTN = new TreeNode(ModelRef + "|" + ModelDescription);
                                    modelTN.Tag = "MODEL|" + SubSetRef + "|" + ModelRef;
                                    int imageIndex = 0;
                                    if (ModelType.Equals("MINIFIG"))
                                    {
                                        imageIndex = 7;
                                    }
                                    else
                                    {
                                        imageIndex = 2;
                                    }
                                    modelTN.ImageIndex = imageIndex;
                                    modelTN.SelectedImageIndex = imageIndex;
                                    nodeList.Add("MODEL|" + ModelRef + "|" + ModelDescription);
                                    SubSetTN.Nodes.Add(modelTN);

                                    // ** POPULATE ALL SUBMODEL & STEP DETAILS **
                                    //List<TreeNode> treeNodeList = GenerateNodes(ModelNode.ChildNodes);
                                    //modelTN.Nodes.AddRange(treeNodeList.ToArray());
                                }
                            }
                            #endregion

                        }
                    }
                    Delegates.TreeView_AddNodes(this, tvSetSummary, SetTN);
                    #endregion

                    // ** Update Scintilla XML areas **                    
                    Delegates.Scintilla_SetText(this, TextArea, XDocument.Parse(currentSetXml.OuterXml).ToString());
                }
                #endregion

                if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** REFRESH FUNCTIONS **

        private BackgroundWorker bw_RefreshScreen;

        private void EnableControls_RefreshScreen(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshScreen(value)));
            }
            else
            {
                btnExit.Enabled = value;
                fldTickBackRef.Enabled = value;
                btnLoadTickBack.Enabled = value;
                btnSaveTickBack.Enabled = value;
                btnDeleteTickBack.Enabled = value;                
                gpSetStructure.Enabled = value;
                gpSelectedObject.Enabled = value;
                gpWholeSet.Enabled = value;
            }
        }

        private void RefreshScreen()
        {
            try
            {
                EnableControls_RefreshScreen(false);

                // ** Store Treeview node positions **
                //savedExpansionState = tvSetSummary.Nodes.GetExpansionState();

                // ** CLEAR FIELDS ** 
                //tvSetSummary.Nodes.Clear();
                TextArea.Text = "";
                dgObjectPartListSummary.DataSource = null;
                dgSetPartListSummary.DataSource = null;
                lblObjectPartListCount.Text = "";
                lblSetPartListCount.Text = "";                
                pnlButtonsWholeSet.Controls.Clear();

                // ** Run background to process functions **
                bw_RefreshScreen = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshScreen.DoWork += new DoWorkEventHandler(bw_RefreshScreen_DoWork);
                bw_RefreshScreen.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshScreen_RunWorkerCompleted);
                //bw_RefreshScreen.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshScreen_ProgressChanged);
                bw_RefreshScreen.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                EnableControls_RefreshScreen(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshScreen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {                
                EnableControls_RefreshScreen(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");

                // ** Update Treeview selections **
                //tvSetSummary.Nodes.SetExpansionState(savedExpansionState);
                //if (lastSelectedNode != null)
                //{
                    //tvSetSummary.SelectedNode = tvSetSummary.Nodes.Descendants().Where(n => n.FullPath.Equals(lastSelectedNodeFullPath)).FirstOrDefault();
                    //if (tvSetSummary.SelectedNode != null)
                    //{
                    //    tvSetSummary.SelectedNode.BackColor = SystemColors.HighlightText;
                    //    tvSetSummary.Focus();
                    //}
                //}
                //if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void bw_RefreshScreen_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                #region ** UPDATE TEEVIEW **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Processing refresh...");
                //if (currentSetXml != null)
                //{
                //    List<string> nodeList = new List<string>();

                //    #region ** POPULATE Set DETAILS **                    
                //    string SetRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;
                //    string SetDescription = currentSetXml.SelectSingleNode("//Set/@Description").InnerXml;
                //    TreeNode SetTN = new TreeNode(SetRef + "|" + SetDescription);
                //    SetTN.Tag = "SET|" + SetRef;
                //    SetTN.ImageIndex = 0;
                //    SetTN.SelectedImageIndex = 0;
                //    nodeList.Add("SET|" + SetRef + "|" + SetDescription);
                //    #endregion

                //    #region ** POPULATE ALL SubSet DETAILS **                    
                //    if (currentSetXml.SelectNodes("//SubSet") != null)
                //    {
                //        XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                //        foreach (XmlNode SubSetNode in SubSetNodeList)
                //        {
                //            // ** GET VARIABLES **
                //            string SubSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;
                //            string SubSetDescription = SubSetNode.SelectSingleNode("@Description").InnerXml;
                //            TreeNode SubSetTN = new TreeNode(SubSetRef + "|" + SubSetDescription);
                //            SubSetTN.Tag = "SUBSET|" + SubSetRef;
                //            SubSetTN.ImageIndex = 1;
                //            SubSetTN.SelectedImageIndex = 1;
                //            nodeList.Add("SUBSET|" + SubSetRef + "|" + SubSetDescription);
                //            SetTN.Nodes.Add(SubSetTN);

                //            #region ** POPULATE ALL MODEL DETAILS **
                //            if (currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']") != null)
                //            {
                //                XmlNodeList ModelNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@SubModelLevel='1']");
                //                foreach (XmlNode ModelNode in ModelNodeList)
                //                {
                //                    string ModelRef = ModelNode.SelectSingleNode("@Ref").InnerXml;
                //                    string ModelDescription = ModelDescription = ModelNode.SelectSingleNode("@Description").InnerXml;
                //                    string ModelType = ModelNode.SelectSingleNode("@LDrawModelType").InnerXml;
                //                    TreeNode modelTN = new TreeNode(ModelRef + "|" + ModelDescription);
                //                    modelTN.Tag = "MODEL|" + SubSetRef + "|" + ModelRef;
                //                    int imageIndex = 0;
                //                    if (ModelType.Equals("MINIFIG"))
                //                    {
                //                        imageIndex = 7;
                //                    }
                //                    else
                //                    {
                //                        imageIndex = 2;
                //                    }
                //                    modelTN.ImageIndex = imageIndex;
                //                    modelTN.SelectedImageIndex = imageIndex;
                //                    nodeList.Add("MODEL|" + ModelRef + "|" + ModelDescription);
                //                    SubSetTN.Nodes.Add(modelTN);

                //                    // ** POPULATE ALL SUBMODEL & STEP DETAILS **
                //                    //List<TreeNode> treeNodeList = GenerateNodes(ModelNode.ChildNodes);
                //                    //modelTN.Nodes.AddRange(treeNodeList.ToArray());
                //                }
                //            }
                //            #endregion

                //        }
                //    }
                //    Delegates.TreeView_AddNodes(this, tvSetSummary, SetTN);
                //    #endregion

                //    // ** Update Scintilla XML areas **                    
                //    Delegates.Scintilla_SetText(this, TextArea, XDocument.Parse(currentSetXml.OuterXml).ToString());
                //}
                #endregion

                // ** Update Scintilla XML areas **
                if (currentSetXml != null)
                {
                    Delegates.Scintilla_SetText(this, TextArea, XDocument.Parse(currentSetXml.OuterXml).ToString());
                }


                #region ** UPDATE SET PART LIST SUMMARY (on both tabs) **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Updating Set parts list...");               
                if (currentSetXml != null)
                {
                    // ** Get Part dictionary ACROSS ALL SubSets **                   
                    Dictionary<string, int> masterPartCount = new Dictionary<string, int>();
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        #region ** Generate Part Count Dictionary **
                        Dictionary<string, int> partCountdDict = new Dictionary<string, int>();
                        XmlNodeList BuildPartsNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + subSetRef + "']//Part[@IsSubPart='false']");
                        foreach (XmlNode partNode in BuildPartsNodeList)
                        {
                            // ** Get variables **
                            string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                            string LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
                            string partKey = LDrawRef + "|" + LDrawColourID;

                            // ** Get Part Count details **
                            if (partCountdDict.ContainsKey(partKey) == false)
                            {
                                partCountdDict.Add(partKey, 0);
                            }
                            partCountdDict[partKey] += 1;
                        }
                        #endregion

                        #region ** Compare part count with master part count **
                        foreach (string partKey in partCountdDict.Keys)
                        {
                            if (masterPartCount.ContainsKey(partKey) == false)
                            {
                                masterPartCount.Add(partKey, partCountdDict[partKey]);
                            }
                            else
                            {
                                int partQty = partCountdDict[partKey];
                                int masterQty = masterPartCount[partKey];
                                if (partQty > masterQty)
                                {
                                    masterPartCount[partKey] = partQty;
                                }
                            }
                        }
                        #endregion
                    }

                    #region ** Generate Master PartList **
                    PartList pl = new PartList();
                    foreach (string partKey in masterPartCount.Keys)
                    {
                        // ** Get variables **           
                        string LDrawRef = partKey.Split('|')[0];
                        int LDrawColourID = int.Parse(partKey.Split('|')[1]);

                        // ** Generate part **
                        PartListPart plp = new PartListPart();
                        plp.LDrawRef = LDrawRef;
                        plp.LDrawColourID = LDrawColourID;
                        plp.Qty = masterPartCount[partKey];

                        int QtyFound = 0;
                        foreach (string subSetRef in SubSetList)
                        {
                            string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                            XmlNodeList SpecificPartNodeList = currentSetXml.SelectNodes(XMLString);
                            if (SpecificPartNodeList.Count > QtyFound) QtyFound = SpecificPartNodeList.Count;

                        }
                        plp.QtyFound = QtyFound;
                        pl.partList.Add(plp);
                    }
                    #endregion

                    // ** Get Node List **
                    XmlDocument PartListXML = new XmlDocument();
                    string PartListXMLString = pl.SerializeToString(true);
                    PartListXML.LoadXml(PartListXMLString);
                    XmlNodeList partListNodeList = PartListXML.SelectNodes("//PartListPart");

                    // ** UPDATE SUMMARY **
                    DataTable partListTable = GeneratePartListTable(partListNodeList);
                    partListTable.DefaultView.Sort = "LDraw Colour Name";
                    partListTable = partListTable.DefaultView.ToTable();
                    Delegates.DataGridView_SetDataSource(this, dgSetPartListSummary, partListTable);
                    AdjustPartListSummaryRowFormatting(dgSetPartListSummary);



                    // ** UPDATE CONTROL BUTTON SUMMARY **                    
                    Delegates.Panel_AddControlRange(this, pnlButtonsWholeSet, GeneratePartButtonControls(partListNodeList));

                    // ** RESTORE SCROLLING INDEX (IF REQUIRED) ** 
                    if (ScrollingPnlIndexDict.ContainsKey("pnlButtonsWholeSet"))
                    {
                        //pnlButtonsWholeSet.AutoScrollPosition = ScrollingPnlIndexDict["pnlButtonsWholeSet"];
                        //pnlButtonsWholeSet.VerticalScroll.Value = ScrollingPnlIndexDict["pnlButtonsWholeSet"];
                        //pnlButtonsWholeSet.AutoScrollPosition = new Point(0, 500);
                        //pnlButtonsWholeSet.VerticalScroll.Value = 500;
                    }


                    


                    // ** Get tickedBack count **
                    int tickedBackPartCount = 0;
                    foreach (string subSetRef in SubSetList)
                    {
                        string tickback_XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@TickedBack='true']";
                        int SubSet_tickedBackPartCount = currentSetXml.SelectNodes(tickback_XMLString).Count;
                        if (SubSet_tickedBackPartCount > tickedBackPartCount) tickedBackPartCount = SubSet_tickedBackPartCount;
                    }

                    // ** UPDATE SUMMARY LABEL **
                    int elementCount = partListTable.Rows.Count;
                    int partCount = (from r in partListTable.AsEnumerable()
                                 select r.Field<int>("Qty")).ToList().Sum();
                    int colourCount = (from r in partListTable.AsEnumerable()
                                   group r by r.Field<string>("LDraw Colour Name") into g
                                   select new { ColourName = g.Key }).Count();
                    int lDrawPartCount = (from r in partListTable.AsEnumerable()
                                      group r by r.Field<string>("LDraw Ref") into g
                                      select new { ColourName = g.Key }).Count();
                    double pc = 0;
                    if (partCount > 0) pc = (((double)tickedBackPartCount / (double)partCount) * 100);
                    Delegates.ToolStripLabel_SetText(this, lblSetPartListCount, partCount.ToString("#,##0") + " Part(s), " + elementCount.ToString("#,##0") + " Element(s), " + lDrawPartCount.ToString("#,##0") + " LDraw Part(s), " + colourCount.ToString("#,##0") + " Colour(s) | " + tickedBackPartCount + " of " + partCount + " part(s) found [" + pc.ToString("#,##0") + "%]");
                }

                #endregion


                #region ** UPDATE OBJECT SUMMARY **                 
                string Type = SelectedNodeTag.Split('|')[0];
                if (Type.Equals("MODEL"))
                {
                    // ** Get variables **                    
                    string SubSetRef = SelectedNodeTag.Split('|')[1];
                    string ModelRef = SelectedNodeTag.Split('|')[2];

                    #region ** Generate Part Count Dictionary **
                    Dictionary<string, int> partCountdDict = new Dictionary<string, int>();
                    XmlNodeList BuildPartsNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@IsSubPart='false']");
                    foreach (XmlNode partNode in BuildPartsNodeList)
                    {
                        // ** Get variables **
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        string LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
                        string partKey = LDrawRef + "|" + LDrawColourID;

                        // ** Get Part Count details **
                        if (partCountdDict.ContainsKey(partKey) == false)
                        {
                            partCountdDict.Add(partKey, 0);
                        }
                        partCountdDict[partKey] += 1;
                    }
                    #endregion

                    #region ** Generate Master PartList **
                    PartList pl = new PartList();
                    foreach (string partKey in partCountdDict.Keys)
                    {
                        // ** Get variables **           
                        string LDrawRef = partKey.Split('|')[0];
                        int LDrawColourID = int.Parse(partKey.Split('|')[1]);

                        // ** Generate part **
                        PartListPart plp = new PartListPart();
                        plp.LDrawRef = LDrawRef;
                        plp.LDrawColourID = LDrawColourID;
                        plp.Qty = partCountdDict[partKey];

                        string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                        XmlNodeList SpecificPartNodeList = currentSetXml.SelectNodes(XMLString);
                        int QtyFound = SpecificPartNodeList.Count;
                                                
                        plp.QtyFound = QtyFound;
                        pl.partList.Add(plp);
                    }
                    #endregion

                    // ** Get Node List **
                    XmlDocument PartListXML = new XmlDocument();
                    string PartListXMLString = pl.SerializeToString(true);
                    PartListXML.LoadXml(PartListXMLString);
                    XmlNodeList partListNodeList = PartListXML.SelectNodes("//PartListPart");

                    // ** UPDATE SUMMARY **
                    DataTable partListTable = GeneratePartListTable(partListNodeList);
                    partListTable.DefaultView.Sort = "LDraw Colour Name";
                    partListTable = partListTable.DefaultView.ToTable();
                    Delegates.DataGridView_SetDataSource(this, dgObjectPartListSummary, partListTable);
                    AdjustPartListSummaryRowFormatting(dgObjectPartListSummary);

                    // ** Get tickedBack count **                    
                    string tickback_XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@TickedBack='true']";
                    int tickedBackPartCount = currentSetXml.SelectNodes(tickback_XMLString).Count;

                    // ** UPDATE SUMMARY LABEL **
                    int elementCount = partListTable.Rows.Count;
                    int partCount = (from r in partListTable.AsEnumerable()
                                 select r.Field<int>("Qty")).ToList().Sum();
                    int colourCount = (from r in partListTable.AsEnumerable()
                                   group r by r.Field<string>("LDraw Colour Name") into g
                                   select new { ColourName = g.Key }).Count();
                    int lDrawPartCount = (from r in partListTable.AsEnumerable()
                                      group r by r.Field<string>("LDraw Ref") into g
                                      select new { ColourName = g.Key }).Count();
                    double pc = 0;
                    if (partCount > 0) pc = (((double)tickedBackPartCount / (double)partCount) * 100);
                    Delegates.ToolStripLabel_SetText(this, lblObjectPartListCount, partCount.ToString("#,##0") + " Part(s), " + elementCount.ToString("#,##0") + " Element(s), " + lDrawPartCount.ToString("#,##0") + " LDraw Part(s), " + colourCount.ToString("#,##0") + " Colour(s) | " + tickedBackPartCount + " of " + partCount + " part(s) found [" + pc.ToString("#,##0") + "%]");
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable GeneratePartListTable(XmlNodeList partListNodeList)
        {
            try
            {
                // ** GENERATE COLUMNS **
                DataTable partListTable = new DataTable("partListTable", "partListTable");
                partListTable.Columns.Add("Part Image", typeof(Bitmap));
                partListTable.Columns.Add("LDraw Ref", typeof(string));
                partListTable.Columns.Add("LDraw Description", typeof(string));
                //partListTable.Columns.Add("LDraw Colour ID", typeof(int));
                partListTable.Columns.Add("LDraw Colour Name", typeof(string));
                //partListTable.Columns.Add("Colour Image", typeof(Bitmap));
                partListTable.Columns.Add("Qty", typeof(int));
                partListTable.Columns.Add("Qty Found", typeof(int));

                // ** CYCLE THROUGH PART NODES AND GENERATE PART ROWS **  
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, partListNodeList.Count);
                int nodeIndex = 1;
                foreach (XmlNode partNode in partListNodeList)
                {
                    // ** GET LDRAW VARIABLES **
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    //String LDrawColourName = (from r in Global_Variables.pcc.PartColourList
                    //                          where r.LDrawColourID == LDrawColourID
                    //                          select r.LDrawColourName).FirstOrDefault();
                    //string LDrawColourName = Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourID='" + LDrawColourID + "']/@LDrawColourName").InnerXml;
                    string LDrawColourName = StaticData.GetLDrawColourName(LDrawColourID);
                    string LDrawDescription = Global_Variables.BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawDescription").InnerXml;
                    int Qty = int.Parse(partNode.SelectSingleNode("@Qty").InnerXml);
                    int QtyFound = int.Parse(partNode.SelectSingleNode("@QtyFound").InnerXml);

                    // ** Update progress **
                    Delegates.ToolStripProgressBar_SetValue(this, pbStatus, nodeIndex);
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Updating parts list | Processing " + LDrawRef + "|" + LDrawColourID + " (" + nodeIndex + " of " + partListNodeList.Count + ")...");

                    // ** Get element & Partcolour images **
                    //Bitmap elementImage = Generator.GetElementImage(LDrawRef, LDrawColourID);
                    Bitmap elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    //Bitmap partColourImage = Generator.GetPartColourImage(LDrawColourID);
                    Bitmap partColourImage = ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });

                    // ** Build row **
                    object[] row = new object[partListTable.Columns.Count];
                    //row[0] = elementImage;
                    //row[1] = LDrawRef;
                    //row[2] = LDrawDescription;
                    //row[3] = LDrawColourID;
                    //row[4] = LDrawColourName;
                    //row[5] = partColourImage;
                    //row[6] = Qty;
                    //row[7] = QtyFound;
                    row[0] = elementImage;
                    row[1] = LDrawRef;
                    row[2] = LDrawDescription;
                    //row[2] = LDrawColourID;
                    row[3] = LDrawColourName;                    
                    row[4] = Qty;
                    row[5] = QtyFound;
                    partListTable.Rows.Add(row);

                    // ** Update Node Index **
                    nodeIndex += 1;
                }
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                return partListTable;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
                return null;
            }
        }

        private void AdjustPartListSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustPartListSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                //dg.Columns["Colour Image"].HeaderText = "";
                //((DataGridViewImageColumn)dg.Columns["Colour Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if ((int)row.Cells["Qty Found"].Value == (int)row.Cells["Qty"].Value)
                    {
                        row.DefaultCellStyle.Font = new System.Drawing.Font(this.Font, FontStyle.Strikeout);                        
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if ((int)row.Cells["Qty Found"].Value > 0)
                    {
                        if ((int)row.Cells["Qty Found"].Value < (int)row.Cells["Qty"].Value)
                        {                            
                            row.DefaultCellStyle.BackColor = Color.Orange;
                        }
                    }
                }
                dg.Columns["LDraw Description"].Width = 150;


                // ** Adjust part images to be small or big **
                bool showBig = false;
                if (dg.Name.Equals("dgObjectPartListSummary") && chkSelectedObjectShowBig.Checked)
                {
                    showBig = true;
                }
                if (dg.Name.Equals("dgSetPartListSummary") && chkWholeSetShowBig.Checked)
                {
                    showBig = true;
                }
                if (showBig)
                {
                    dg.Columns["Part Image"].Width = 50;
                    foreach (DataGridViewRow row in dg.Rows) row.Height = 50;
                }
                else
                {
                    foreach (DataGridViewRow row in dg.Rows) row.Height = 22;
                    dg.AutoResizeColumns();
                }

                // ** RESTORE SCROLLING INDEX (IF REQUIRED) **                
                if (ScrollingRowIndexDict[dg.Name] != -1)
                {
                    dg.FirstDisplayedScrollingRowIndex = ScrollingRowIndexDict[dg.Name];
                }


            }
        }

        #endregion

        #region ** MOUSE DOUBLE CLICK FUNCTIONS **

        private void dgSetPartListSummary_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // ** Check if any row has been selected **
                if (dgSetPartListSummary.SelectedRows.Count <= 0) return;

                // ** Select current row if RMB pressed **
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {
                    dgSetPartListSummary.ClearSelection();
                    dgSetPartListSummary.Rows[e.RowIndex].Selected = true;
                }
                ScrollingRowIndexDict["dgSetPartListSummary"] = dgSetPartListSummary.FirstDisplayedScrollingRowIndex;

                // ** GET VARIABLES **                  
                string LDrawRef = (string)dgSetPartListSummary.SelectedRows[0].Cells["LDraw Ref"].Value;
                //int LDrawColourID = (int)dgSetPartListSummary.SelectedRows[0].Cells["LDraw Colour ID"].Value;                
                string LDrawColourName = (string)dgSetPartListSummary.SelectedRows[0].Cells["LDraw Colour Name"].Value;
                //int LDrawColourID = -1;
                //if (Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourName='" + LDrawColourName + "']/@LDrawColourID") != null)
                //{
                //    LDrawColourID = int.Parse(Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourName='" + LDrawColourName + "']/@LDrawColourID").InnerXml);
                //}
                int LDrawColourID = StaticData.GetLDrawColourID(LDrawColourName);
                int Qty = (int)dgSetPartListSummary.SelectedRows[0].Cells["Qty"].Value;
                int QtyFound = (int)dgSetPartListSummary.SelectedRows[0].Cells["Qty Found"].Value;

                #region ** DETERMINE MOUSE AND KEY PRESS COMBO ** 
                if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (Qty == QtyFound) return;

                    // FIND ALL PARTS IN ALL SUBSETS AND MARK AS TickedBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        foreach (XmlNode partNode in partNodeList)
                        {
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                        }                        
                    }
                }
                else if (e.Button == MouseButtons.Right && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (QtyFound == 0) return;

                    // FIND ALL PARTS IN ALL SUBSETS AND MARK AS TickedBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        foreach (XmlNode partNode in partNodeList)
                        {
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                        }
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (Qty == QtyFound) return;

                    // find the 1st part in all the SubSets and mark as TickBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        if (partNodeList.Count > 0)
                        {
                            XmlNode partNode = partNodeList[0];
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                        }
                    }
                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (QtyFound == 0) return;

                    // find the last part in all the SubSets and mark as TickBack = false
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        if (partNodeList.Count > 0)
                        {
                            XmlNode partNode = partNodeList[partNodeList.Count - 1];
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                        }
                    }
                }
                #endregion

                // ** REFRESH SCREEN **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgObjectPartListSummary_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                // ** Check if any row has been selected **
                if (dgObjectPartListSummary.SelectedRows.Count < 0) return;

                // ** Select current row if RMB pressed **
                if (e.Button == MouseButtons.Right && e.RowIndex != -1)
                {                    
                    dgObjectPartListSummary.ClearSelection();
                    dgObjectPartListSummary.Rows[e.RowIndex].Selected = true;                    
                }
                //ObjectPartListSummary_RowIndex = dgObjectPartListSummary.FirstDisplayedScrollingRowIndex;
                ScrollingRowIndexDict["dgObjectPartListSummary"] = dgObjectPartListSummary.FirstDisplayedScrollingRowIndex;

                // ** GET VARIABLES **                  
                string LDrawRef = (string)dgObjectPartListSummary.SelectedRows[0].Cells["LDraw Ref"].Value;
                //int LDrawColourID = (int)dgObjectPartListSummary.SelectedRows[0].Cells["LDraw Colour ID"].Value;
                string LDrawColourName = (string)dgObjectPartListSummary.SelectedRows[0].Cells["LDraw Colour Name"].Value;
                //int LDrawColourID = -1;
                //if (Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourName='" + LDrawColourName + "']/@LDrawColourID") != null)
                //{
                //    LDrawColourID = int.Parse(Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourName='" + LDrawColourName + "']/@LDrawColourID").InnerXml);
                //}
                int LDrawColourID = StaticData.GetLDrawColourID(LDrawColourName);
                int Qty = (int)dgObjectPartListSummary.SelectedRows[0].Cells["Qty"].Value;
                int QtyFound = (int)dgObjectPartListSummary.SelectedRows[0].Cells["Qty Found"].Value;                              
                string SubSetRef = SelectedNodeTag.Split('|')[1];
                string ModelRef = SelectedNodeTag.Split('|')[2];

                #region ** DETERMINE MOUSE AND KEY PRESS COMBO ** 
                if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (Qty == QtyFound) return;

                    // FIND ALL PARTS IN MODEL AND MARK AS TickedBack = true                    
                    string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false']";
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    foreach (XmlNode partNode in partNodeList)
                    {
                        partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                    }
                }
                else if (e.Button == MouseButtons.Right && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    if (QtyFound == 0) return;

                    // FIND ALL PARTS IN MODEL AND MARK AS TickedBack = false                    
                    string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false']";
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    foreach (XmlNode partNode in partNodeList)
                    {
                        partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                    }
                }
                else if (e.Button == MouseButtons.Left)
                {                   
                    if (Qty == QtyFound) return;

                    // mark as TickBack = true
                    string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    XmlNode partNode = partNodeList[0];
                    partNode.SelectSingleNode("@TickedBack").InnerXml = "true";

                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (QtyFound == 0) return;

                    // mark as TickBack = false
                    string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    XmlNode partNode = partNodeList[partNodeList.Count - 1];
                    partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                }
                #endregion

                // ** REFRESH SCREEN **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion




        private void Handle_TickBack_Button_Click(object sender, EventArgs e)
        {
            try
            {
                // ** GET DETAILS OF SENDING CONTROL **
                string[] nameDetails = ((Button)sender).Name.Split('_');
                string LDrawRef = nameDetails[3];
                string LDrawColourID = nameDetails[4];
                ScrollingPnlIndexDict["pnlButtonsWholeSet"] = pnlButtonsWholeSet.VerticalScroll.Value;

                #region ** ELEMENT IMAGE CLICKED **
                if (((Button)sender).Name.StartsWith("btn_ElementImage"))
                {
                    // FIND ALL PARTS IN ALL SUBSETS AND MARK AS TickedBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        foreach (XmlNode partNode in partNodeList)
                        {
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                        }
                    }

                    // ** REFRESH SCREEN **
                    RefreshScreen();
                }
                #endregion

                #region ** BTN MINUS CLICKED **
                if (((Button)sender).Name.StartsWith("btn_Minus"))
                {
                    // find the last part in all the SubSets and mark as TickBack = false
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='true']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        if (partNodeList.Count > 0)
                        {
                            XmlNode partNode = partNodeList[partNodeList.Count - 1];
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                        }
                    }

                    // ** REFRESH SCREEN **
                    RefreshScreen();
                }
                #endregion

                #region ** BTN PLUS CLICKED **
                if (((Button)sender).Name.StartsWith("btn_Plus"))
                {
                    // find the 1st part in all the SubSets and mark as TickBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)
                                                       //.OrderBy(x => x)
                                                       .ToList();
                    foreach (string subSetRef in SubSetList)
                    {
                        string XMLString = "//SubSet[@Ref='" + subSetRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                        XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                        if (partNodeList.Count > 0)
                        {
                            XmlNode partNode = partNodeList[0];
                            partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                        }
                    }

                    // ** REFRESH SCREEN **
                    RefreshScreen();
                }
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private List<Control> GeneratePartButtonControls(XmlNodeList partListNodeList)
        {
            List<Control> controlList = new List<Control>();
            try
            {
                // ** GET VARIABLES **
                int xBound;                
                if (int.TryParse(fldButtonWidth.Text, out xBound) == false)
                {
                    xBound = 5;
                }
                int index = 0;
                int xIndex = 0;
                int yIndex = 0;

                // ** CYCLE THROUGH EACH ELEMENT AND GENERATE CONTROLS **
                IEnumerable<XmlNode> rowList = partListNodeList.Cast<XmlNode>().OrderBy(r => r.Attributes["LDrawColourID"].Value);
                foreach (XmlNode partNode in rowList)                
                {
                    // ** GET LDRAW VARIABLES **
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    int Qty = int.Parse(partNode.SelectSingleNode("@Qty").InnerXml);
                    int QtyFound = int.Parse(partNode.SelectSingleNode("@QtyFound").InnerXml);
                    //if (QtyFound < Qty)
                    //{
                    //}   

                    #region ** CREATE GROUPBOX CONTROL FOR PART **
                        GroupBox gb = new GroupBox();
                        gb.SuspendLayout();
                        gb.Name = "gp_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                        gb.Text = LDrawRef + " | " + LDrawColourID;
                        gb.Size = new Size(175, 185);
                        gb.TabStop = false;
                        // ** Update Location **
                        int locationX = xIndex * 175;
                        int locationY = yIndex * 185;
                        gb.Location = new System.Drawing.Point(locationX, locationY);
                        gb.BackColor = Color.White;
                        if (QtyFound == Qty) gb.BackColor = Color.LightGreen;
                        if (QtyFound > 0 && QtyFound < Qty) gb.BackColor = Color.Orange;
                        controlList.Add(gb);
                        #endregion

                    #region ** ADD ELEMENT IMAGE BUTTON **
                    Button btn = new Button();
                    btn.Location = new System.Drawing.Point(6, 14);
                    btn.Name = "btn_ElementImage_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                    btn.Size = new Size(120, 120);
                    btn.TabIndex = 0;
                    btn.UseVisualStyleBackColor = true;
                    //btn.BackgroundImage = Generator.GetElementImage(LDrawRef, LDrawColourID);
                    btn.BackgroundImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    btn.Click += new System.EventHandler(Handle_TickBack_Button_Click);
                    gb.Controls.Add(btn);
                    #endregion

                    #region ** ADD PARTCOLOUR IMAGE BUTTON **
                    btn = new Button();
                    btn.Enabled = false;
                    btn.Location = new System.Drawing.Point(127, 94);
                    btn.Name = "btn_PartColourImage_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                    btn.Size = new Size(40, 40);
                    btn.TabIndex = 0;
                    //btn.Text = LDrawColourID.ToString();
                    btn.UseVisualStyleBackColor = true;
                    btn.BackgroundImage = ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });                    
                    btn.BackgroundImageLayout = ImageLayout.Zoom;
                    gb.Controls.Add(btn);
                    #endregion

                    #region ** ADD QTY BUTTON **
                    btn = new Button();
                    btn.Enabled = false;
                    btn.Location = new System.Drawing.Point(127, 14);
                    btn.Name = "btn_Qty_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                    btn.Size = new Size(40, 40);
                    btn.TabIndex = 0;
                    btn.Text = Qty.ToString();
                    btn.UseVisualStyleBackColor = true;
                    btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    gb.Controls.Add(btn);
                    #endregion

                    #region ** ADD QTY FOUND BUTTON **
                    btn = new Button();
                    btn.Enabled = false;
                    btn.Location = new System.Drawing.Point(46, 135);
                    btn.Name = "btn_QtyFound_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                    btn.Size = new Size(40, 40);
                    btn.TabIndex = 0;
                    btn.Text = QtyFound.ToString();                        
                    btn.UseVisualStyleBackColor = true;
                    btn.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
                    btn.ForeColor = Color.Blue;
                    gb.Controls.Add(btn);
                    #endregion

                    #region ** ADD [-] BUTTON **
                    if (QtyFound > 0)
                    {
                        btn = new Button();
                        //if (QtyFound == 0) btn.Enabled = false;
                        btn.Location = new System.Drawing.Point(6, 135);
                        btn.Name = "btn_Minus_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                        btn.Size = new Size(40, 40);
                        btn.TabIndex = 0;
                        btn.Text = "-";
                        btn.UseVisualStyleBackColor = true;
                        btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = Color.Red;
                        btn.Click += new System.EventHandler(Handle_TickBack_Button_Click);
                        gb.Controls.Add(btn);
                    }
                    #endregion

                    #region ** ADD [+] BUTTON **
                    if (QtyFound < Qty)
                    {
                        btn = new Button();
                        //if (QtyFound == Qty) btn.Enabled = false;
                        btn.Location = new System.Drawing.Point(86, 135);
                        btn.Name = "btn_Plus_" + index + "_" + LDrawRef + "_" + LDrawColourID;
                        btn.Size = new Size(40, 40);
                        btn.TabIndex = 0;
                        btn.Text = "+";
                        btn.UseVisualStyleBackColor = true;
                        btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                        btn.ForeColor = Color.Green;
                        btn.Click += new System.EventHandler(Handle_TickBack_Button_Click);
                        gb.Controls.Add(btn);
                    }
                    #endregion

                    // ** Update indexes **
                    index += 1;
                    xIndex += 1;
                    if (xIndex == xBound)
                    {
                        xIndex = 0;
                        yIndex += 1;
                    }
                    
                }
                return controlList;
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }



        private void btnButtonWidthMinus_Click(object sender, EventArgs e)
        {
            try
            {
                int oldValue = int.Parse(fldButtonWidth.Text);
                if (oldValue == 1) return;
                int newvalue = oldValue - 1;
                fldButtonWidth.Text = newvalue.ToString();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnButtonWidthPlus_Click(object sender, EventArgs e)
        {
            try
            {
                int oldValue = int.Parse(fldButtonWidth.Text);                
                int newvalue = oldValue + 1;
                fldButtonWidth.Text = newvalue.ToString();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    
    
    
    }
}
