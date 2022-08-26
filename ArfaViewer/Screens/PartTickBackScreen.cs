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
        //private TreeNode lastSelectedTreeNode;
        //private string lastSelectedNodeFullPath = "";



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
                                fldTickBackName,
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

                #region ** ADD OTHER TOOLSTRIP ITEMS **
                tsSelectedObject.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {                                
                                new ToolStripControlHost(chkSelectedObjectShowBig),
                                new ToolStripControlHost(chkSelectedObjectShowMissingOnly)
                                });
                toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                new ToolStripControlHost(chkWholeSetShowBig),
                                });
                tsWholeSet.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                new ToolStripControlHost(chkWholeSetShowMissingOnly),
                                });
                #endregion

                // ** Set up Scintilla **
                SetupScintillaPanel1();

                fldTickBackName.Text = "4742-1";
                fldSetRef.Text = "4742-1";
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

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].ExpandAll();
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].Collapse(false);
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

        #region ** TREEVIEW FUNCTIONS **

        private void RefreshSetSummaryTreeview()
        {
            try
            {
                // ** UPDATE TEEVIEW **
                tvSetSummary.Nodes.Clear();
                tvSetSummary.Nodes.Add(Set.GetSetTreeViewFromSetXML(currentSetXml, false, false, false, false));
                if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].ExpandAll();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        #endregion

        #region ** PART BUTTON FUNCTIONS **

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
                    // ** Update Overall PartListPart section **
                    int Qty = int.Parse(currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@Qty").InnerXml);
                    currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound").InnerXml = Qty.ToString();

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
                        foreach (XmlNode partNode in partNodeList) partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                    }

                    // ** REFRESH SCREEN **
                    RefreshScreen();
                }
                #endregion

                #region ** BTN MINUS CLICKED **
                if (((Button)sender).Name.StartsWith("btn_Minus"))
                {
                    // ** Update PartList Qty Found **
                    int QtyFound = int.Parse(currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound").InnerXml);
                    QtyFound -= 1;
                    currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound").InnerXml = QtyFound.ToString();

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
                    // ** Update PartList Qty Found **
                    int QtyFound = int.Parse(currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound").InnerXml);
                    QtyFound += 1;
                    currentSetXml.SelectSingleNode("//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound").InnerXml = QtyFound.ToString();

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
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
                fldTickBackName.Enabled = value;
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
                #region ** UPDATE SET PART LIST SUMMARY (on both tabs) **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Updating Set parts list...");
                if (currentSetXml != null)
                {
                    // ** Update Scintilla XML areas **
                    Delegates.Scintilla_SetText(this, TextArea, XDocument.Parse(currentSetXml.OuterXml).ToString());

                    // ** UPDATE SUMMARY **
                    XmlNodeList partListNodeList = currentSetXml.SelectNodes("//PartListPart");
                    DataTable partListTable = GeneratePartListTable(partListNodeList);
                    partListTable.DefaultView.Sort = "LDraw Colour Name";
                    partListTable = partListTable.DefaultView.ToTable();
                    Delegates.DataGridView_SetDataSource(this, dgSetPartListSummary, partListTable);
                    AdjustPartListSummaryRowFormatting(dgSetPartListSummary);

                    // ** UPDATE CONTROL BUTTON SUMMARY **                    
                    Delegates.Panel_AddControlRange(this, pnlButtonsWholeSet, GeneratePartButtonControls(partListNodeList));

                    // ** RESTORE SCROLLING INDEX (IF REQUIRED) ** 
                    //if (ScrollingPnlIndexDict.ContainsKey("pnlButtonsWholeSet"))
                    //{
                    //    pnlButtonsWholeSet.AutoScrollPosition = ScrollingPnlIndexDict["pnlButtonsWholeSet"];
                    //    pnlButtonsWholeSet.VerticalScroll.Value = ScrollingPnlIndexDict["pnlButtonsWholeSet"];
                    //    pnlButtonsWholeSet.AutoScrollPosition = new Point(0, 500);
                    //    pnlButtonsWholeSet.VerticalScroll.Value = 500;
                    //}

                    // ** Get tickedBack count **
                    int tickedBackPartCount = 0;
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)                                                      
                                                       .ToList();
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

                    // ** Get Partlist for the Model within the SubSet **
                    string ModelXMLString = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']").OuterXml;
                    XmlDocument ModelXML = new XmlDocument();
                    ModelXML.LoadXml(ModelXMLString);
                    PartList pl = PartList.GetPartList_ForModel(ModelXML);
                    
                    // ** Get PartList node list which will be used to render the part data **
                    string PartListXMLString = pl.SerializeToString(true);                   
                    XmlDocument PartListXML = new XmlDocument();                    
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

        //TODO: This is duplicate code that is also used in the Generator screen.
        private DataTable GeneratePartListTable(XmlNodeList partListNodeList)
        {
            try
            {
                #region ** GET DATA UPFRONT **
                // Get a list of LDrawColourIDs & LDrawRefs
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Getting data...");
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, partListNodeList.Count);
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                int index = 0;
                List<int> LDrawColourIDList = new List<int>();
                List<string> LDrawRefList = new List<string>();
                foreach (XmlNode partNode in partListNodeList)
                {
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
                    if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });                    
                    if (chkShowPartcolourImages.Checked) ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });
                    Delegates.ToolStripProgressBar_SetValue(this, pbStatus, index);
                    index += 1;
                }
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                PartColourCollection PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
                BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                #endregion

                // ** GENERATE COLUMNS **
                DataTable partListTable = new DataTable("partListTable", "partListTable");
                partListTable.Columns.Add("Part Image", typeof(Bitmap));
                partListTable.Columns.Add("LDraw Ref", typeof(string));
                partListTable.Columns.Add("LDraw Description", typeof(string));
                partListTable.Columns.Add("LDraw Colour ID", typeof(int));
                partListTable.Columns.Add("LDraw Colour Name", typeof(string));
                partListTable.Columns.Add("Colour Image", typeof(Bitmap));
                partListTable.Columns.Add("Qty", typeof(int));
                partListTable.Columns.Add("Qty Found", typeof(int));

                // ** CYCLE THROUGH PART NODES AND GENERATE PART ROWS **  
                foreach (XmlNode partNode in partListNodeList)
                {
                    // ** GET LDRAW VARIABLES **
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    int Qty = int.Parse(partNode.SelectSingleNode("@Qty").InnerXml);
                    int QtyFound = int.Parse(partNode.SelectSingleNode("@QtyFound").InnerXml);
                    string LDrawColourName = (from r in PartColourCollection.PartColourList
                                              where r.LDrawColourID == LDrawColourID
                                              select r.LDrawColourName).FirstOrDefault();
                    string LDrawDescription = (from r in BasePartCollection.BasePartList
                                               where r.LDrawRef.Equals(LDrawRef)
                                               select r.LDrawDescription).FirstOrDefault();

                    // ** Get element & Partcolour images **
                    Bitmap elementImage = null;
                    Bitmap partColourImage = null;
                    if (chkShowElementImages.Checked) elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    if (chkShowPartcolourImages.Checked) partColourImage = ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });

                    // ** Build row and add to table **
                    DataRow newRow = partListTable.NewRow();
                    newRow["Part Image"] = elementImage;
                    newRow["LDraw Ref"] = LDrawRef;
                    newRow["LDraw Description"] = LDrawDescription;
                    newRow["LDraw Colour ID"] = LDrawColourID;
                    newRow["LDraw Colour Name"] = LDrawColourName;
                    newRow["Colour Image"] = partColourImage;
                    newRow["Qty"] = Qty;
                    newRow["Qty Found"] = QtyFound;
                    partListTable.Rows.Add(newRow);
                }
                
                return partListTable;
            }
            catch (Exception ex)
            {
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
                dg.Columns["Colour Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Colour Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();
                foreach (DataGridViewRow row in dg.Rows)
                {
                    int qtyFound = (int)row.Cells["Qty Found"].Value;
                    int qty = (int)row.Cells["Qty"].Value;
                    if (qtyFound == qty)
                    {
                        row.DefaultCellStyle.Font = new System.Drawing.Font(this.Font, FontStyle.Strikeout);                        
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (qtyFound > 0 && qtyFound < qty)
                    {
                        //if (qtyFound < qty)
                        //{                            
                            row.DefaultCellStyle.BackColor = Color.Orange;
                        //}
                    }
                }
                dg.Columns["LDraw Description"].Width = 150;

                // ** Adjust part images to be small or big **
                bool showBig = false;
                if (dg.Name.Equals("dgObjectPartListSummary") && chkSelectedObjectShowBig.Checked) showBig = true;               
                if (dg.Name.Equals("dgSetPartListSummary") && chkWholeSetShowBig.Checked) showBig = true;               
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

        #region ** TICKBACK FUNCTIONS **

        private void LoadFromSet()
        {
            try
            {
                // ** Validation **
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldSetRef.Text;
                //SetDetails setDetails = StaticData.GetSetDetails(SetRef);
                //if (setDetails == null) throw new Exception("Set " + SetRef + " not found...");
                SetInstructions si = StaticData.GetSetInstructions(SetRef);
                if (si == null) throw new Exception("Set Instructions for " + SetRef + " not found...");

                // ** LOAD Set XML into Object **
                //string xmlString = setDetails.Instructions;
                string xmlString = si.Data;
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(xmlString);

                // ** MERGE STANDALONE MINIFIG XML's INTO SET XML **   
                Dictionary<string, XmlDocument> MiniFigXMLDict = StaticData.GetMiniFigXMLDict(currentSetXml);
                if (MiniFigXMLDict.Count > 0) currentSetXml = Set.MergeMiniFigsIntoSetXML(currentSetXml, MiniFigXMLDict);

                // ** Clear fields **
                SelectedNodeTag = "";
                fldTickBackName.Text = "";

                // ** Generate Treeview then Refresh screen **
                RefreshSetSummaryTreeview();
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
                if (fldTickBackName.Text.Equals("")) throw new Exception("No TickBack Ref entered...");
                string TickBackName = fldTickBackName.Text;
                if(currentSetXml == null) throw new Exception("No Set XML loaded...");

                // Check if Set already exists - if so update it, if not, add it.
                string action = "UPDATE";
                TickBack tickBack = StaticData.GetTickBack(TickBackName);
                if (tickBack == null)
                {
                    action = "ADD";
                    tickBack = new TickBack();                    
                }
                tickBack.Name = TickBackName;
                tickBack.Data = currentSetXml.OuterXml;
                
                // ** Determine what action to take **
                if (action.Equals("ADD")) StaticData.AddTickBack(tickBack);
                else if (action.Equals("UPDATE")) StaticData.UpdateTickBack(tickBack);

                // ** Tidy Up **
                //ClearAllSetDetailsFields();
                //RefreshScreen();
                //RefreshSetDetailsSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LoadTickBack()
        {
            try
            {
                // ** Validation Checks **
                if (fldTickBackName.Text.Equals("")) throw new Exception("No TickBack Ref entered...");
                string tickBackName = fldTickBackName.Text;
                
                // ** Get Set details from API **
                TickBack tickBack = StaticData.GetTickBack(tickBackName);
                if (tickBack == null) throw new Exception("No details found for TickBack: " + tickBackName);
                string tickBackXML = tickBack.Data;
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(tickBackXML);

                // ** Clear fields **
                SelectedNodeTag = "";
                
                // ** Generate Treeview then Refresh screen **
                RefreshSetSummaryTreeview();
                RefreshScreen();
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
                if (fldTickBackName.Text.Equals("")) throw new Exception("No TickBack Ref entered...");                
                string tickBackName = fldTickBackName.Text;
                TickBack tickBack = StaticData.GetTickBack(tickBackName);
                if (tickBack == null) throw new Exception("No details found for TickBack: " + tickBackName);

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // ** Delete Set Details **
                    StaticData.DeleteTickBack(tickBackName);

                    // ** Clear all fields **                    
                    currentSetXml = null;
                    RefreshSetSummaryTreeview();
                    RefreshScreen();

                    // ** Show confirm **
                    MessageBox.Show("TickBack " + tickBackName + " deleted...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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
                string LDrawColourName = (string)dgSetPartListSummary.SelectedRows[0].Cells["LDraw Colour Name"].Value;
                int LDrawColourID = -1;                    
                int.TryParse(StaticData.GetLDrawColourID(LDrawColourName), out LDrawColourID);
                int Qty = (int)dgSetPartListSummary.SelectedRows[0].Cells["Qty"].Value;
                int QtyFound = (int)dgSetPartListSummary.SelectedRows[0].Cells["Qty Found"].Value;





                #region ** DETERMINE MOUSE AND KEY PRESS COMBO ** 
                if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    // This marks all the parts as ticked back
                    if (Qty == QtyFound) return;

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = Qty.ToString();

                    // FIND ALL PARTS IN ALL SUBSETS AND MARK AS TickedBack = true
                    XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
                    List<string> SubSetList = SubSetNodeList.Cast<XmlNode>()
                                                       .Select(x => x.SelectSingleNode("@Ref").InnerXml)                                                      
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
                    // This marks all the parts as NOT ticked back
                    if (QtyFound == 0) return;

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = "0";

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

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    int currentQtyFound = int.Parse(currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml);
                    currentQtyFound += 1;
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = currentQtyFound.ToString();

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

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    int currentQtyFound = int.Parse(currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml);
                    currentQtyFound -= 1;
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = currentQtyFound.ToString();

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
                string LDrawColourName = (string)dgObjectPartListSummary.SelectedRows[0].Cells["LDraw Colour Name"].Value;
                //int LDrawColourID = StaticData.GetLDrawColourID(LDrawColourName);
                int LDrawColourID = -1;
                int.TryParse(StaticData.GetLDrawColourID(LDrawColourName), out LDrawColourID);
                int Qty = (int)dgObjectPartListSummary.SelectedRows[0].Cells["Qty"].Value;
                int QtyFound = (int)dgObjectPartListSummary.SelectedRows[0].Cells["Qty Found"].Value;
                string SubSetRef = SelectedNodeTag.Split('|')[1];
                string ModelRef = SelectedNodeTag.Split('|')[2];

                #region ** DETERMINE MOUSE AND KEY PRESS COMBO ** 
                if (e.Button == MouseButtons.Left && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    //if (Qty == QtyFound) return;

                    //// FIND ALL PARTS IN MODEL AND MARK AS TickedBack = true                    
                    //string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false']";
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    //foreach (XmlNode partNode in partNodeList)
                    //{
                    //    partNode.SelectSingleNode("@TickedBack").InnerXml = "true";
                    //}
                }
                else if (e.Button == MouseButtons.Right && (ModifierKeys & Keys.Alt) == Keys.Alt)
                {
                    //if (QtyFound == 0) return;

                    //// FIND ALL PARTS IN MODEL AND MARK AS TickedBack = false                    
                    //string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false']";
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    //foreach (XmlNode partNode in partNodeList)
                    //{
                    //    partNode.SelectSingleNode("@TickedBack").InnerXml = "false";
                    //}
                }
                else if (e.Button == MouseButtons.Left)
                {
                    if (Qty == QtyFound) return;

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    int currentQtyFound = int.Parse(currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml);
                    currentQtyFound += 1;
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = currentQtyFound.ToString();

                    // mark as TickBack = true
                    string XMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "' and @IsSubPart='false' and @TickedBack='false']";
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(XMLString);
                    XmlNode partNode = partNodeList[0];
                    partNode.SelectSingleNode("@TickedBack").InnerXml = "true";

                }
                else if (e.Button == MouseButtons.Right)
                {
                    if (QtyFound == 0) return;

                    // ** Update Overall PartListPart section **
                    string PartListPart_XMLString = "//PartListPart[@LDrawRef='" + LDrawRef + "' and @LDrawColourID='" + LDrawColourID + "']/@QtyFound";
                    int currentQtyFound = int.Parse(currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml);
                    currentQtyFound -= 1;
                    currentSetXml.SelectSingleNode(PartListPart_XMLString).InnerXml = currentQtyFound.ToString();

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










    }
}
