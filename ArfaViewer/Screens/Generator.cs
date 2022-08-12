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
using System.Runtime.Serialization.Json;
using System.Net.Http;


namespace Generator
{
    public partial class Generator : Form
    {
        // ** Variables **
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private Scintilla TextArea;
        private Scintilla TextArea2;
        private XmlDocument currentSetXml;
        private XmlDocument fullSetXml;
        private DataTable dgPartSummaryTable_Orig;
        private TreeNode lastSelectedNode;
        private string lastSelectedNodeFullPath = "";



        public Generator()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Generator";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                log.Info(".......................................................................GENERATOR SCREEN STARTED.......................................................................");

                #region ** FORMAT SUMMARIES **
                String[] DGnames = new string[] { "dgPartSummary", "dgPartListSummary", "dgPartListWithMFsSummary" };
                foreach (String dgName in DGnames)
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
                lblStatus.Text = "";
                lblPartCount.Text = "";                
                lblPartSummaryItemFilteredCount.Text = "";
                lblPartListCount.Text = "";
                lblPartListWithMFsCount.Text = "";
                #endregion

                #region ** ADD MAIN HEADER LINE TOOLSTRIP ITEMS **
                toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                btnExit,
                                toolStripSeparator1,

                                btnRefreshStaticData,
                                toolStripSeparator2,

                                lblSetRef,
                                fldCurrentSetRef,
                                btnLoadSet,
                                btnSaveSet,
                                btnDeleteSet,                                                               
                                new ToolStripControlHost(chkShowSubParts),
                                new ToolStripControlHost(chkShowPages),
                                toolStripSeparator4,

                                btnOpenSetURLs,
                                btnOpenSetInstructions,
                                toolStripSeparator22,
                                new ToolStripControlHost(chkShowPartcolourImages),
                                new ToolStripControlHost(chkShowElementImages),

                                });
                #endregion

                #region ** ADD PART DETAILS TOOLSTRIP ITEMS **
                tsPartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                toolStripLabel1,
                                fldLDrawRef,
                                fldLDrawImage,
                                //lblLDrawDescription,
                                toolStripLabel2,
                                fldLDrawColourID,
                                toolStripLabel4,
                                fldLDrawColourName,
                                lblQty,
                                fldQty,
                                new ToolStripControlHost(chkBasePartCollection),
                                btnAddPartToBasePartCollection2,
                                toolStripLabel3,
                                fldPlacementMovements,
                                btnPartClear,
                                toolStripSeparator3,
                                btnPartAdd,
                                btnPartSave,
                                btnPartDelete,
                                toolStripSeparator24,
                                //btnAddToPartDAT,
                                btnGenerateDatFile
                                });
                #endregion

                #region ** ADD PART SUMMARY TOOLSTRIP ITEMS **
                tsPartSummary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                btnPartSummaryCopyToClipboard,
                                toolStripSeparator7,
                                lblLDrawRefAc,
                                new ToolStripControlHost(chkLDrawRefAcEquals),
                                fldLDrawRefAc,

                                lblLDrawColourNameAc,
                                new ToolStripControlHost(chkLDrawColourNameAcEquals),
                                fldLDrawColourNameAc,

                                new ToolStripControlHost(chkFBXMissingAc),

                                });
                #endregion

                #region ** ADD BASEPART COLLECTION HEADER LINE TOOLSTRIP ITEMS **
                tsBasePartCollection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                    lblPartType,
                                    fldPartType,
                                    lblLDrawSize,
                                    fldLDrawSize,
                                    new ToolStripControlHost(chkIsSubPart),
                                    new ToolStripControlHost(chkIsSticker),
                                    new ToolStripControlHost(chkIsLargeModel),
                                    btnAddPartToBasePartCollection
                                    });
                #endregion

                // ** Set up Scintilla **
                SetupScintillaPanel1();
                SetupScintillaPanel2();
                log.Info("Set up Scintilla");

                // ** REFRESH STATIC DATA **    
                //RefreshStaticData();
                //StaticData.RefreshStaticData_All();
                RefreshLDrawColourNameDropdown();

                // ** UPDATE LABELS **                
                //fldCurrentSetRef.Text = "621-1";
                //fldCurrentSetRef.Text = "7327-1";
                //fldCurrentSetRef.Text = "621-2";
                //.Text = "41621-1";
                fldCurrentSetRef.Text = "TEST-1";
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        private void Test()
        {

            // ** DOWNLOAD FROM BLOB **
            //BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");            
            //byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
            //using (var ms = new MemoryStream(fileContent))
            //{
            //    blob.DownloadTo(ms);
            //}
            //string xmlString = Encoding.UTF8.GetString(fileContent);

            // ** UPLOAD TO BLOB **            
            //BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
            //using (var ms = new MemoryStream(fileContent))
            //{
            //    blob.Upload(ms, true);                
            //}


            // ** DOWNLOAD FROM SHARE **            
            //ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient("1.dat"); 
            //byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
            //Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
            //using (var ms = new MemoryStream(fileContent))
            //{
            //    download.Content.CopyTo(ms);
            //}
            //string fileString = Encoding.UTF8.GetString(fileContent);
            ////string localFilePath = Path.Combine(Path.GetTempPath(), fileName);
            ////using (FileStream stream = File.OpenWrite(localFilePath))
            ////{
            ////    download.Content.CopyTo(stream);
            ////}

            // ** UPLOAD TO SHARE **                  
            //ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient("files-instructions").GetFileClient("test.pdf");
            //share.Create(fileContent.Length);
            //using (var ms = new MemoryStream(fileContent))
            //{
            //    share.Upload(ms);
            //    //share.UploadRange(new Azure.HttpRange(0, ms.Length), ms);
            //}
            ////string targetFilePath = "";
            ////using (FileStream stream = File.OpenRead(targetFilePath))
            ////{
            ////    share.Upload(stream);
            ////}

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

        private void SetupScintillaPanel2()
        {
            try
            {
                // ** Initialise Scintilla Control Surface **
                TextArea2 = new ScintillaNET.Scintilla();
                panel2.Controls.Add(TextArea2);
                TextArea2.Dock = System.Windows.Forms.DockStyle.Fill;
                TextArea2.WrapMode = WrapMode.None;
                TextArea2.IndentationGuides = IndentView.LookBoth;

                // Configuring the default style with properties **
                TextArea2.StyleResetDefault();
                TextArea2.Styles[Style.Default].Font = "Consolas";
                TextArea2.Styles[Style.Default].Size = 8;
                TextArea2.Styles[Style.Default].BackColor = Color.Black;
                TextArea2.Styles[Style.Default].ForeColor = Color.White;

                // ** Number Margin **
                TextArea2.Styles[Style.LineNumber].ForeColor = Color.White;
                TextArea2.Styles[Style.LineNumber].BackColor = Color.Black;
                TextArea2.Styles[Style.IndentGuide].ForeColor = Color.White;
                TextArea2.Styles[Style.IndentGuide].BackColor = Color.Black;
                var nums = TextArea2.Margins[NUMBER_MARGIN];
                nums.Width = 30;
                nums.Type = MarginType.Number;
                nums.Sensitive = true;
                nums.Mask = 0;

                // ** Code Folding **
                //TextArea2.SetFoldMarginColor(true, Color.Black);
                //TextArea2.SetFoldMarginHighlightColor(true, Color.Black);
                //TextArea2.SetProperty("fold", "1");
                //TextArea2.SetProperty("fold.compact", "1");

                //TextArea2.Margins[FOLDING_MARGIN].Type = MarginType.Symbol;
                //TextArea2.Margins[FOLDING_MARGIN].Mask = Marker.MaskFolders;
                //TextArea2.Margins[FOLDING_MARGIN].Sensitive = true;
                //TextArea2.Margins[FOLDING_MARGIN].Width = 20;
                //for (int i = 25; i <= 31; i++)
                //{
                //    TextArea2.Markers[i].SetForeColor(Color.Black);
                //    TextArea2.Markers[i].SetBackColor(Color.White);
                //}

                //TextArea2.Markers[Marker.Folder].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CirclePlus : MarkerSymbol.BoxPlus;
                //TextArea2.Markers[Marker.FolderOpen].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CircleMinus : MarkerSymbol.BoxMinus;
                //TextArea2.Markers[Marker.FolderEnd].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CirclePlusConnected : MarkerSymbol.BoxPlusConnected;
                //TextArea2.Markers[Marker.FolderMidTail].Symbol = MarkerSymbol.TCorner;
                //TextArea2.Markers[Marker.FolderOpenMid].Symbol = CODEFOLDING_CURCULAR ? MarkerSymbol.CircleMinusConnected : MarkerSymbol.BoxMinusConnected;
                //TextArea2.Markers[Marker.FolderSub].Symbol = MarkerSymbol.VLine;
                //TextArea2.Markers[Marker.FolderTail].Symbol = MarkerSymbol.LCorner;

                //TextArea2.AutomaticFold = (AutomaticFold.Show | AutomaticFold.Click | AutomaticFold.Change);



                // ** UPDATE LEXER TO XML **
                TextArea2.Lexer = Lexer.Xml;
                TextArea2.StyleClearAll();

                // ** Configure the CPP Lexer styles **
                TextArea2.Styles[Style.Xml.Comment].ForeColor = Color.Gray;
                TextArea2.Styles[Style.Xml.Tag].ForeColor = Color.White;
                TextArea2.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
                TextArea2.Styles[Style.Xml.DoubleString].ForeColor = Color.Yellow;

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

        private void btnRefreshStaticData_Click(object sender, EventArgs e)
        {
            //RefreshStaticData();
        }
        
        private void btnStepDelete_Click(object sender, EventArgs e)
        {
            DeleteStep();
        }

        private void btnStepSave_Click(object sender, EventArgs e)
        {
            SaveStep();
        }
        
        private void btnAddPartToBasePartCollection2_Click(object sender, EventArgs e)
        {
            AddPartToBasePartCollection();
        }
        
        private void tsSubModelImportPartPosRot_Click(object sender, EventArgs e)
        {
            SubModelImportPartPosRot();
        }

        private void chkShowPages_CheckedChanged(object sender, EventArgs e)
        {
            RefreshScreen();
        }
        
        private void btnOpenSetURLs_Click(object sender, EventArgs e)
        {
            OpenSetURLs();
        }

        private void btnOpenSetInstructions_Click(object sender, EventArgs e)
        {
            OpenSetInstructions();
        }

        private void btnLoadSet_Click(object sender, EventArgs e)
        {
            LoadSet();
        }

        private void btnSaveSet_Click(object sender, EventArgs e)
        {
            SaveSet();
        }

        private void btnDeleteSet_Click(object sender, EventArgs e)
        {
            DeleteSet();
        }

        private void btnSetAdd_Click(object sender, EventArgs e)
        {
            AddSet();
        }

        private void btnSetSaveNode_Click(object sender, EventArgs e)
        {
            SetSaveNode();
        }

        private void btnSetClear_Click(object sender, EventArgs e)
        {
            ClearSet();
        }

        private void btnConvertToLDR_Click(object sender, EventArgs e)
        {
            ConvertSetXMLToLDR();
        }

        private void btnAdjustModelStepNos_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation Checks **
                if (fldModelCurrentRef.Text.Equals(""))
                {
                    throw new Exception("No Model selected");
                }
                String modelRef = fldModelCurrentRef.Text;
                String subSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + modelRef + "']/ancestor::SubSet/@Ref").InnerXml;

                // ** Adjust step numbers **
                AdjustSubSetStepNumbers(subSetRef, modelRef);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void btnRecalculatePartList_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set loaded");
                }

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** Refresh Screen **
                RefreshScreen();

                // ** Show confirmation **
                MessageBox.Show("Part List recalculated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnAddPartToBasePartCollection_Click(object sender, EventArgs e)
        {
            AddPartToBasePartCollection();
        }

        private void tsShowMiniFigSet_Click(object sender, EventArgs e)
        {
            ShowMiniFigSet();
        }

        private void btnOpenInNotePadPlus_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals(""))
                {
                    throw new Exception("No Set Ref entered...");
                }

                // ** OLD **
                //string setfileLocation = Global_Variables.SetSaveLocation + "\\" + fldCurrentSetRef.Text + ".xml";
                //if (File.Exists(setfileLocation) == false)
                //{
                //    throw new Exception("Set not found...");
                //}
                //Process.Start("notepad++.exe", "\"" + setfileLocation + "\"");

                // ** NEW **
                throw new Exception("Function needs updating...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void tsAddStepToEnd_Click(object sender, EventArgs e)
        {                  
            InsertStep(1, "END");
        }

        private void tsAdd5StepsToEnd_Click(object sender, EventArgs e)
        {            
            InsertStep(5, "END");
        }

        private void tsInsertStepBefore_Click(object sender, EventArgs e)
        {
            InsertStep(1, "BEFORE");
        }

        private void tsInsertStepAfter_Click(object sender, EventArgs e)
        {
            InsertStep(1, "AFTER");
        }

        private void tsAddSubModelAtEnd_Click(object sender, EventArgs e)
        {
            InsertSubModel(1, "END");
        }

        private void tsInsertSubModelBefore_Click(object sender, EventArgs e)
        {
            InsertSubModel(1, "BEFORE");
        }

        private void tsInsertSubModelAfter_Click(object sender, EventArgs e)
        {
            InsertSubModel(1, "AFTER");
        }

        private void btnSubModelDelete_Click(object sender, EventArgs e)
        {
            DeleteSubModel();
        }

        private void btnSubModelSave_Click(object sender, EventArgs e)
        {
            SaveSubModel();
        }

        private void tsSubModelDuplicateToEnd_Click(object sender, EventArgs e)
        {
            DuplicateSubModel("END");
        }

        private void tsSubModelDuplicateToBefore_Click(object sender, EventArgs e)
        {
            DuplicateSubModel("BEFORE");
        }

        private void tsSubModelDuplicateToAfter_Click(object sender, EventArgs e)
        {
            DuplicateSubModel("AFTER");
        }

        private void btnPartClear_Click(object sender, EventArgs e)
        {
            ClearPart();
        }

        private void btnPartAdd_Click(object sender, EventArgs e)
        {
            AddPart();
        }

        private void btnPartSave_Click(object sender, EventArgs e)
        {
            SavePart();
        }

        private void btnPartDelete_Click(object sender, EventArgs e)
        {
            DeletePart();
        }

        private void btnSetStructureRefresh_Click(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        private void fldLDrawImage_Click(object sender, EventArgs e)
        {
            Handle_fldLDrawImage_Click();
        }

        private void dgPartSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Handle_dgPartSummary_CellClick(sender, e);
        }

        private void btnGenerateDatFile_Click(object sender, EventArgs e)
        {
            GenerateDATFile();
        }

        private void btnSubSetAdd_Click(object sender, EventArgs e)
        {
            AddSubSet();
        }

        private void btnSubSetSave_Click(object sender, EventArgs e)
        {
            SaveSubSet();
        }

        private void btnSubSetDelete_Click(object sender, EventArgs e)
        {
            DeleteSubSet();
        }

        private void btnModelAdd_Click(object sender, EventArgs e)
        {
            AddModel();
        }

        private void btnModelSave_Click(object sender, EventArgs e)
        {
            SaveModel();
        }

        private void btnModelDelete_Click(object sender, EventArgs e)
        {
            DeleteModel();
        }

        private void tsAddSet_Click(object sender, EventArgs e)
        {
            AddSet();
        }

        private void tsAddSubSet_Click(object sender, EventArgs e)
        {
            AddSubSet();
        }

        private void tsAddModel_Click(object sender, EventArgs e)
        {
            AddModel();
        }

        private void tsRecalulatePartList_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set loaded");
                }

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** Refresh Screen **
                RefreshScreen();

                // ** Show confirmation **
                MessageBox.Show("Part List recalculated.");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void chkShowSubParts_CheckedChanged(object sender, EventArgs e)
        {
            RefreshScreen();            
        }

        private void btnPartSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgPartSummary.Rows.Count == 0)
                {
                    throw new Exception("No data to copy from " + dgPartSummary.Name + "...");
                }
                StringBuilder sb = BaseClasses.HelperFunctions.GenerateClipboardStringFromDataTable(dgPartSummary);
                Clipboard.SetText(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnPartListBasicCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopyToClipboard(dgPartListSummary);
        }

        private void btnPartListWithMFCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopyToClipboard(dgPartListWithMFsSummary);
        }

        private void tsRecalulateSubSetRefs_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null) throw new Exception("No Set loaded");
               
                // ** Recalculate Unity refs for all SubSets **
                int totalParts;
                int totalSubSets;
                RecalculateSubSetRefs(currentSetXml, out totalParts, out totalSubSets);

                // ** Refresh Screen **
                RefreshScreen();

                // ** Show confirmation **
                MessageBox.Show(totalParts + " Part(s) across " + totalSubSets + " SubSet(s) recalculated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnCompareWithRebrickable_Click(object sender, EventArgs e)
        {
            CompareSetPartsWithRebrickable();
        }

        private void tsStepDuplicateToEnd_Click(object sender, EventArgs e)
        {
            DuplicateStep("END");
        }

        private void tsStepDuplicateToBefore_Click(object sender, EventArgs e)
        {
            DuplicateStep("BEFORE");
        }

        private void tsStepDuplicateToAfter_Click(object sender, EventArgs e)
        {
            DuplicateStep("AFTER");
        }

        #endregion

        #region ** REFRESH STATIC DATA FUNCTIONS **

        //private void EnableControls_RefreshStaticData(bool value)
        //{
        //    if (this.InvokeRequired)
        //    {
        //        this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshStaticData(value)));
        //    }
        //    else
        //    {
        //        btnExit.Enabled = value;
        //        fldCurrentSetRef.Enabled = value;
        //        btnLoadSet.Enabled = value;
        //        btnSaveSet.Enabled = value;
        //        btnDeleteSet.Enabled = value;
        //        //btnRecalculatePartList.Enabled = value;
        //        //btnRecalculateUnityRefs.Enabled = value;
        //        btnOpenSetInstructions.Enabled = value;
        //        btnOpenSetURLs.Enabled = value;
        //        //btnSaveToOfficialSetsXML.Enabled = value;
        //        chkShowSubParts.Enabled = value;
        //        chkShowPages.Enabled = value;
        //        tabControl1.Enabled = value;
        //        //fldInstructionsSetRef.Enabled = value;
        //        //fldSetInstructions.Enabled = value;
        //        //btnUploadInstructionsFromWeb.Enabled = value;
        //    }
        //}

        //private void RefreshStaticData()
        //{
        //    try
        //    {
        //        EnableControls_All(false);

        //        //lblStatus.Text = "Downloading BasePartCollection.xml from Azure...";
        //        //string xmlString = Global_Variables.APIProxy.GetStaticData(StaticData.Filename.BasePartCollection.ToString());
        //        //Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //        //System.Threading.Thread.Sleep(2000);

        //        //lblStatus.Text = "Downloading CompositePartCollection.xml from Azure...";
        //        //xmlString = Global_Variables.APIProxy.GetStaticData(StaticData.Filename.CompositePartCollection.ToString());
        //        //Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
        //        //System.Threading.Thread.Sleep(2000);

        //        //lblStatus.Text = "Downloading PartColourCollection.xml from Azure...";
        //        //xmlString = Global_Variables.APIProxy.GetStaticData(StaticData.Filename.PartColourCollection.ToString());
        //        //Global_Variables.PartColourCollectionXML.LoadXml(xmlString);
        //        //System.Threading.Thread.Sleep(2000);

        //        //StaticData.RefreshStaticData_All();
        //        //RefreshLDrawColourNameDropdown();
        //        //System.Threading.Thread.Sleep(5000);

        //        // ** Update status **               
        //        lblStatus.Text = "Static data last updated on " + DateTime.Now.ToString("dd-MMM-yyyy") + " @" + DateTime.Now.ToString("HH:mm:ss");

        //        EnableControls_All(true);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        #endregion

        private void RefreshLDrawColourNameDropdown()
        {
            //List<string> partColourNameList =   (from r in Global_Variables.pcc.PartColourList
            //                                    select r.LDrawColourName).OrderBy(x => x).ToList();                
            //XmlNodeList LDrawColourNameNodeList = Global_Variables.PartColourCollectionXML.SelectNodes("//PartColour/@LDrawColourName");
            //List<string> partColourNameList = LDrawColourNameNodeList.Cast<XmlNode>()
            //                                   .Select(x => x.InnerText)
            //                                   .OrderBy(x => x).ToList();
            List<string> partColourNameList = StaticData.GetAllLDrawColourNames();
            fldLDrawColourName.Items.Clear();
            fldLDrawColourName.Items.AddRange(partColourNameList.ToArray());
        }


        #region ** SET FUNCTIONS **

        private void LoadSet()
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldCurrentSetRef.Text;

                // ** Get Set details from API - OLD **
                //Set set = StaticData.GetSet(SetRef);
                //if (set == null) throw new Exception("Set " + SetRef + " not found...");
                //string setXML = set.SerializeToString(true);
                //currentSetXml = new XmlDocument();
                //currentSetXml.LoadXml(setXML);

                // ** Get Set details from API **
                SetDetails setDetails = StaticData.GetSetDetails(SetRef);
                if (setDetails == null) throw new Exception("Set " + SetRef + " not found...");
                string setXML = setDetails.Instructions;
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(setXML);

                // ** Tidy Up **
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        private void SaveSet()
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                if (currentSetXml == null) throw new Exception("No Set currently loaded...");

                // ** Update Set details - OLD **
                //Set set = new Set().DeserialiseFromXMLString(currentSetXml.OuterXml);
                //StaticData.UpdateSet(set);

                // ** Update Set details **
                StaticData.UpdateSetDetailsInstructions_UsingSetRef(fldCurrentSetRef.Text, currentSetXml.OuterXml);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        private void DeleteSet()
        {
            try
            {
                // ** Validation Checks **
                string setRef = fldCurrentSetRef.Text;
                if (setRef.Equals("")) throw new Exception("No Set Ref entered...");
                if (currentSetXml == null) throw new Exception("No Set currently loaded...");
                //if (StaticData.CheckIfSetExists(setRef) == false) throw new Exception("Set " + setRef + " not found...");

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // ** Delete Set using API **
                    StaticData.DeleteSet(setRef);

                    // ** Tidy up & refresh screen **
                    currentSetXml = null;
                    fullSetXml = null;
                    ClearAllFields();
                    RefreshScreen();

                    // ** Show confirm **
                    MessageBox.Show("Set " + setRef + " successfully deleted...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void AddSet()
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml != null) throw new Exception("Set already present...");

                // ** Add Step to selected node **
                Set newSet = new Set() { Ref = "NEW SET", Description = "NEW SET" };

                // ** Update Set Xml object **
                string setXmlString = newSet.SerializeToString(true);
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(setXmlString);

                // ** Tidy Up **
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        private void SetSaveNode()
        {
            try
            {
                // ** Validation Checks ** 
                if (fldSetCurrentRef.Text.Equals("")) throw new Exception("No Set selected...");

                // ** UPDATE XML DOC **
                string oldSetRef = fldSetCurrentRef.Text;
                string newSetRef = fldSetNewRef.Text;
                currentSetXml.SelectSingleNode("//Set[@Ref='" + oldSetRef + "']/@Description").InnerXml = fldSetDescription.Text;
                if (fldSetNewRef.Text != "") // UPDATE REF LAST SO REFS ABOVE WORK CORRECTLY
                {
                    currentSetXml.SelectSingleNode("//Set[@Ref='" + oldSetRef + "']/@Ref").InnerXml = newSetRef;
                }

                // ** Tidy Up **
                RefreshScreen();
                ClearAllFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
                
        private void ClearSet()
        {
            try
            {
                currentSetXml = null;
                fullSetXml = null;
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** OPEN SET URLS & INSTRUCTION FUNCTIONS

        private void OpenSetURLs()
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string setRef = fldCurrentSetRef.Text;

                // ** Open urls **                
                Process.Start("https://brickset.com/sets/" + setRef);
                Process.Start("https://rebrickable.com/sets/" + setRef);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void OpenSetInstructions()
        {
            try
            {
                // ** VALIDATION **                 
                //if (currentSetXml == null) throw new Exception("No Set loaded...");
                //string setRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;
                if(fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string setRef = fldCurrentSetRef.Text;

                // ********** FS **********
                string FSPath = Path.Combine(@"\\lodgeaccount.file.core.windows.net\lodgeant-fs\static-data\files-instructions", setRef + ".pdf");
                if (File.Exists(FSPath) == false) throw new Exception("Cannot find Azure FS Instructions for " + setRef + ".pdf");
                Process.Start(FSPath);

                // ********** BLOB - NOT CURRENTLY USED **********
                // ** Check if Instructions exist **                 
                ////CloudBlockBlob blob = blobClient.GetContainerReference("files-instructions").GetBlockBlobReference(setRef + ".pdf");
                //BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "files-instructions").GetBlobClient(setRef + ".pdf");
                //if (blob.Exists() == false)
                //{
                //    throw new Exception("Cannot find Azure BLOB Instruction for " + setRef + ".pdf");
                //}

                //// ** Download & Open Instructions - NOT CURRENTLY USED **                
                //// check if file is already in TEMP folder                    
                //string insUrl = Path.Combine(Path.GetTempPath(), setRef + ".pdf");
                //if (File.Exists(insUrl) == false)                    
                //{
                //    // ** Download file to TEMP & open **
                //    string webUrl = "https://lodgeaccount.blob.core.windows.net/files-instructions/" + setRef + ".pdf";                        
                //    using (WebClient webClient = new WebClient())
                //    {
                //        webClient.DownloadProgressChanged += (s, e1) =>
                //        {
                //            pbStatus.Value = e1.ProgressPercentage;                                
                //            lblStatus.Text = "Open Set Instructions PDF | Downloading " + setRef + " from Azure | Downloaded " + e1.ProgressPercentage + "%";
                //        };
                //        webClient.DownloadFileCompleted += (s, e1) =>
                //        {
                //            pbStatus.Value = 0;
                //            lblStatus.Text = "";
                //        };
                //        Task downloadTask = webClient.DownloadFileTaskAsync(new Uri(webUrl), insUrl);
                //        await downloadTask;                            
                //    }
                //}
                //Process.Start(insUrl);                 
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** REFRESH FUNCTIONS **

        List<string> savedExpansionState = new List<string>();
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
                fldCurrentSetRef.Enabled = value;
                btnLoadSet.Enabled = value;
                btnSaveSet.Enabled = value;
                btnDeleteSet.Enabled = value;
                //btnRecalculatePartList.Enabled = value;
                //btnRecalculateUnityRefs.Enabled = value;
                btnOpenSetInstructions.Enabled = value;
                btnOpenSetURLs.Enabled = value;
                //btnSaveToOfficialSetsXML.Enabled = value;
                chkShowSubParts.Enabled = value;
                chkShowPages.Enabled = value;
                tabControl1.Enabled = value;
                //fldInstructionsSetRef.Enabled = value;
                //fldSetInstructions.Enabled = value;
                //btnUploadInstructionsFromWeb.Enabled = value;
                btnRefreshStaticData.Enabled = value;
                chkShowPartcolourImages.Enabled = value;
                chkShowElementImages.Enabled = value;
                pnlSetImage.Enabled = value;
            }
        }

        private void RefreshScreen()
        {
            try
            {
                EnableControls_RefreshScreen(false);

                // ** Store Treeview node positions **
                savedExpansionState = tvSetSummary.Nodes.GetExpansionState();

                // ** CLEAR FIELDS ** 
                tvSetSummary.Nodes.Clear();
                TextArea.Text = "";
                TextArea2.Text = "";
                dgPartListSummary.DataSource = null;
                lblPartListCount.Text = "";
                dgPartListWithMFsSummary.DataSource = null;
                lblPartListWithMFsCount.Text = "";

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
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_RefreshScreen(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");

                // ** Update Treeview selections **
                tvSetSummary.Nodes.SetExpansionState(savedExpansionState);
                if (lastSelectedNode != null)
                {
                    tvSetSummary.SelectedNode = tvSetSummary.Nodes.Descendants().Where(n => n.FullPath.Equals(lastSelectedNodeFullPath)).FirstOrDefault();                    
                    if (tvSetSummary.SelectedNode != null)
                    {                        
                        tvSetSummary.SelectedNode.BackColor = SystemColors.HighlightText;
                        tvSetSummary.Focus();
                    }
                }
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
                #region ** Process refresh only if a SET has been loaded **
                if (currentSetXml != null)
                {
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Generating Treeview...");

                    // ** MERGE STANDALONE MINIFIG XML's INTO SET XML **   
                    fullSetXml = new XmlDocument();
                    fullSetXml.LoadXml(currentSetXml.OuterXml);
                    Dictionary<string, XmlDocument> MiniFigXMLDict = GetMiniFigXMLDict(currentSetXml);
                    if (MiniFigXMLDict.Count > 0) fullSetXml = Set.MergeMiniFigsIntoSetXML(fullSetXml, MiniFigXMLDict);
                    
                    // ** GENERATE SET SUMMARY TREEVIEW **
                    // ** Add SET details to Treeview node **    
                    List<string> nodeList = new List<string>();
                    string SetRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;
                    string SetDescription = currentSetXml.SelectSingleNode("//Set/@Description").InnerXml;
                    TreeNode SetTN = new TreeNode(SetRef + "|" + SetDescription);
                    SetTN.Tag = "SET|" + SetRef;
                    SetTN.ImageIndex = 0;
                    SetTN.SelectedImageIndex = 0;
                    nodeList.Add("SET|" + SetRef + "|" + SetDescription);
                    
                    #region ** Add all SUBSET details to Treeview node **                    
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

                            // ** POPULATE ALL MODEL DETAILS **
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
                                    int imageIndex = 2;
                                    if (ModelType.Equals("MINIFIG")) imageIndex = 7;                                    
                                    modelTN.ImageIndex = imageIndex;
                                    modelTN.SelectedImageIndex = imageIndex;
                                    nodeList.Add("MODEL|" + ModelRef + "|" + ModelDescription);
                                    SubSetTN.Nodes.Add(modelTN);

                                    // ** POPULATE ALL SUBMODEL & STEP DETAILS **
                                    List<TreeNode> treeNodeList = GenerateNodes(ModelNode.ChildNodes);
                                    modelTN.Nodes.AddRange(treeNodeList.ToArray());
                                }
                            }
                        }
                    }
                    #endregion

                    // ** Populate Summary Treeview with data **
                    Delegates.TreeView_AddNodes(this, tvSetSummary, SetTN);

                    // ** Update Scintilla XML areas **                    
                    Delegates.Scintilla_SetText(this, TextArea, XDocument.Parse(currentSetXml.OuterXml).ToString());
                    Delegates.Scintilla_SetText(this, TextArea2, XDocument.Parse(fullSetXml.OuterXml).ToString());

                    // ** Update Set Image **
                    pnlSetImage.BackgroundImage = ArfaImage.GetImage(ImageType.SET, new string[] { SetRef });
                }
                #endregion

                //System.Threading.Thread.Sleep(2000);

                #region ** UPDATE PART LIST SUMMARIES - Current Set XML **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Updating Current Set parts list...");                               
                if (currentSetXml != null)
                {
                    XmlNodeList partListNodeList = currentSetXml.SelectNodes("//PartListPart");
                    Stopwatch watch = new Stopwatch(); watch.Start();
                    DataTable partListTable = GeneratePartListTable(partListNodeList);
                    watch.Stop(); long msecs = watch.ElapsedMilliseconds;
                    partListTable.DefaultView.Sort = "LDraw Colour Name";
                    partListTable = partListTable.DefaultView.ToTable();                    
                    Delegates.DataGridView_SetDataSource(this, dgPartListSummary, partListTable);
                    AdjustPartListSummaryRowFormatting(dgPartListSummary);

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
                    Delegates.ToolStripLabel_SetText(this, lblPartListCount, partCount.ToString("#,##0") + " Part(s), " + elementCount.ToString("#,##0") + " Element(s), " + lDrawPartCount.ToString("#,##0") + " LDraw Part(s), " + colourCount.ToString("#,##0") + " Colour(s)");
                }
                #endregion

                //System.Threading.Thread.Sleep(2000);

                #region ** UPDATE PART LIST SUMMARIES - Full Set XML **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Updating Full Set parts list...");
                if (fullSetXml != null)
                {
                    XmlNodeList partListNodeList = fullSetXml.SelectNodes("//PartListPart");
                    DataTable partListTable = GeneratePartListTable(partListNodeList);
                    partListTable.DefaultView.Sort = "LDraw Colour Name";
                    partListTable = partListTable.DefaultView.ToTable();                   
                    Delegates.DataGridView_SetDataSource(this, dgPartListWithMFsSummary, partListTable);
                    AdjustPartListSummaryRowFormatting(dgPartListWithMFsSummary);

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
                    Delegates.ToolStripLabel_SetText(this, lblPartListWithMFsCount, partCount.ToString("#,##0") + " Part(s), " + elementCount.ToString("#,##0") + " Element(s), " + lDrawPartCount.ToString("#,##0") + " LDraw Part(s), " + colourCount.ToString("#,##0") + " Colour(s)");
                }
                #endregion

                //System.Threading.Thread.Sleep(2000);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private Dictionary<string, XmlDocument> GetMiniFigXMLDict(XmlDocument currentSetxmlDoc)
        {
            Dictionary<string, XmlDocument> MiniFigXMLDict = new Dictionary<string, XmlDocument>();

            XmlNodeList MiniFigNodeList = currentSetxmlDoc.SelectNodes("//SubModel[@SubModelLevel='1' and @LDrawModelType='MINIFIG']");
            List<string> MiniFigSetList = MiniFigNodeList.Cast<XmlNode>()
                                           .Select(x => x.SelectSingleNode("@Description").InnerXml.Split('_')[0])
                                           .OrderBy(x => x).ToList();
            foreach (string MiniFigRef in MiniFigSetList)
            {
                // ** Get the Set XML doc for the MiniFig **                   
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(MiniFigRef + ".xml");
                if (blob.Exists())
                {
                    // ** Get MiniFig XML **
                    XmlDocument MiniFigXmlDoc = new XmlDocument();
                    byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                    using (var ms = new MemoryStream(fileContent))
                    {
                        blob.DownloadTo(ms);
                    }
                    string xmlString = Encoding.UTF8.GetString(fileContent);
                    MiniFigXmlDoc.LoadXml(xmlString);
                    if (MiniFigXMLDict.ContainsKey(MiniFigRef) == false)
                    {
                        MiniFigXMLDict.Add(MiniFigRef, MiniFigXmlDoc);
                    }
                }
            }
            return MiniFigXMLDict;
        }

        private void ClearAllFields()
        {
            try
            {
                // ** Set **
                fldSetCurrentRef.Text = "";
                fldSetNewRef.Text = "";
                fldSetDescription.Text = "";               
                pnlSetImage.BackgroundImage = null;

                // ** SubSet **
                fldSubSetCurrentRef.Text = "";
                fldSubSetNewRef.Text = "";
                fldSubSetDescription.Text = "";
                fldSubSetType.Text = "";

                // ** Model **
                fldModelCurrentRef.Text = "";
                fldModelNewRef.Text = "";
                fldModelDescription.Text = "";
                fldModelType.Text = "";

                // ** SubModel **
                fldSubModelCurrentRef.Text = "";
                fldSubModelNewRef.Text = "";
                fldSubModelDescription.Text = "";
                fldSubModelPosX.Text = "";
                fldSubModelPosY.Text = "";
                fldSubModelPosZ.Text = "";
                fldSubModelRotX.Text = "";
                fldSubModelRotY.Text = "";
                fldSubModelRotZ.Text = "";

                // ** Step **
                fldStepParentSubSetRef.Text = "";
                fldStepParentModelRef.Text = "";
                fldPureStepNo.Text = "";
                fldStepLevel.Text = "";
                fldStepBook.Text = "";
                fldStepPage.Text = "";
                fldModelRotX.Text = "";
                fldModelRotY.Text = "";
                fldModelRotZ.Text = "";

                // ** Parts **
                dgPartSummary.DataSource = null;
                lblPartCount.Text = "";
                fldLDrawRef.Text = "";
                fldLDrawImage.Image = null;
                //lblLDrawDescription.Text = "";
                fldLDrawColourName.Text = "";
                fldLDrawColourID.Text = "";
                chkBasePartCollection.Checked = false;
                fldPartType.Text = "BASIC";
                chkIsSubPart.Checked = false;
                chkIsSticker.Checked = false;
                chkIsLargeModel.Checked = false;
                fldQty.Text = "1";
                gbPartDetails.Text = "Part Details";

                tsPartDetails.Enabled = true;
                tsBasePartCollection.Enabled = true;

                fldPlacementMovements.Text = "Y=-5";

                fldPartPosX.Text = "";
                fldPartPosY.Text = "";
                fldPartPosZ.Text = "";
                fldPartRotX.Text = "";
                fldPartRotY.Text = "";
                fldPartRotZ.Text = "";

                fldLDrawSize.Text = "";


                //TextArea.Text = "";
                //TextArea2.Text = "";
                //dgPartListSummary.DataSource = null;
                //lblPartListCount.Text = "";                
                //dgPartListWithMFsSummary.DataSource = null;
                //lblPartListWithMFsCount.Text = "";


            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private List<TreeNode> GenerateNodes(XmlNodeList childNodeList)
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
                        treeNode.Text = SubModelRef + "|" + SubModelDescription;
                        treeNode.Tag = nodeType + "|" + parentSubSetRef + "|" + SubModelRef + "|";
                        treeNode.ImageIndex = 3;
                        treeNode.SelectedImageIndex = 3;
                    }
                    else if (nodeType.Equals("STEP"))
                    {
                        string PureStepNo = xmlNode.SelectSingleNode("@PureStepNo").InnerXml;
                        string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                        string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                        //string parentSubModelRef = xmlNode.SelectSingleNode("parent::SubModel/@Ref").InnerXml;

                        String StepBook = "";
                        String StepPage = "";
                        String extraString = "";
                        if (chkShowPages.Checked)
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
                        treeNode.Text = PureStepNo + extraString;                        
                        treeNode.Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + PureStepNo;
                        treeNode.ImageIndex = 4;
                        treeNode.SelectedImageIndex = 4;
                        nodeStepIndex = 0;
                        // ** Update Colour of Step (if required) **
                    }
                    else if (nodeType.Equals("PART"))
                    {
                        string LDrawRef = xmlNode.SelectSingleNode("@LDrawRef").InnerXml;
                        String LDrawColourID = xmlNode.SelectSingleNode("@LDrawColourID").InnerXml;
                        string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                        string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                        string parentPureStepNo = xmlNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml;
                        treeNode.Text = LDrawRef + "|" + LDrawColourID;
                        treeNode.Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + parentPureStepNo + "|" + LDrawRef + "|" + LDrawColourID + "|" + nodeStepIndex;
                        if (LDrawRef.Contains("stk"))
                        {
                            treeNode.ImageIndex = 9;
                            treeNode.SelectedImageIndex = 9;
                        }
                        else
                        {
                            treeNode.ImageIndex = 5;
                            treeNode.SelectedImageIndex = 5;
                        }
                        nodeStepIndex += 1;
                        nodePlacementIndex = 0;
                    }
                    else if (nodeType.Equals("PLACEMENTMOVEMENT"))
                    {
                        string Axis = xmlNode.SelectSingleNode("@Axis").InnerXml;
                        String Value = xmlNode.SelectSingleNode("@Value").InnerXml;
                        string parentSubSetRef = xmlNode.SelectSingleNode("ancestor::SubSet/@Ref").InnerXml;
                        string parentModelRef = xmlNode.SelectSingleNode("ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                        string parentPureStepNo = xmlNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml;
                        string LDrawRef = xmlNode.SelectSingleNode("ancestor::Part/@LDrawRef").InnerXml;
                        String LDrawColourID = xmlNode.SelectSingleNode("ancestor::Part/@LDrawColourID").InnerXml;
                        treeNode.Text = Axis + "=" + Value;
                        treeNode.Tag = nodeType + "|" + parentSubSetRef + "|" + parentModelRef + "|" + parentPureStepNo + "|" + LDrawRef + "|" + LDrawColourID + "|" + nodePlacementIndex;
                        treeNode.ImageIndex = 8;
                        treeNode.SelectedImageIndex = 8;
                        nodePlacementIndex += 1;
                    }
                    if (xmlNode.HasChildNodes)
                    {
                        treeNode.Nodes.AddRange(GenerateNodes(xmlNode.ChildNodes).ToArray());
                    }
                    treeNodeList.Add(treeNode);
                }
            }
            return treeNodeList;
        }
   
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
                    if (chkShowElementImages.Checked)
                    {
                        Bitmap elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    }
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

                // ** CYCLE THROUGH PART NODES AND GENERATE PART ROWS **
                foreach (XmlNode partNode in partListNodeList)
                {
                    // ** GET VARIABLES **
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    int Qty = int.Parse(partNode.SelectSingleNode("@Qty").InnerXml);                   
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
            }
        }

        #endregion

        #region ** TREENODE FUNCTIONS **

        private void tvSetSummary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // ** Clear all fields & disable groupboxes **
                ClearAllFields();
                gpSet.Enabled = false;
                gpSubSet.Enabled = false;
                gpSubModel.Enabled = false;
                gpModel.Enabled = false;
                gpStep.Enabled = false;
                gbPartDetails.Enabled = false;

                // ** Update treeview node highlighting **
                if (lastSelectedNode != null)
                {
                    lastSelectedNode.BackColor = Color.White;
                    lastSelectedNode.ForeColor = Color.Black;
                }
                tvSetSummary.SelectedNode.BackColor = SystemColors.Highlight;
                tvSetSummary.SelectedNode.ForeColor = SystemColors.HighlightText;
                
                #region ** DETERMINE TYPE **
                lastSelectedNode = tvSetSummary.SelectedNode;
                lastSelectedNodeFullPath = tvSetSummary.SelectedNode.FullPath;                
                string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                if (Type.Equals("SET"))
                {
                    // ** Get variables **
                    string SetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string Description = currentSetXml.SelectSingleNode("//Set[@Ref='" + SetRef + "']/@Description").InnerXml;
                    
                    // ** Post data **
                    fldSetCurrentRef.Text = SetRef;
                    fldSetDescription.Text = Description;                    
                    gpSet.Enabled = true;
                }
                else if (Type.Equals("SUBSET"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string Description = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/@Description").InnerXml;
                    string SubSetType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/@SubSetType").InnerXml;

                    // ** Post data **                    
                    fldSubSetCurrentRef.Text = SubSetRef;
                    fldSubSetDescription.Text = Description;
                    fldSubSetType.Text = SubSetType;
                    gpSubSet.Enabled = true;
                }
                else if (Type.Equals("MODEL"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];                    
                    //string SubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + ModelRef + "']/ancestor::SubSet/@Ref").InnerXml;
                    string ModelDescription = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@Description").InnerXml;
                    string ModelType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@LDrawModelType").InnerXml;

                    // ** Post data **                    
                    fldModelCurrentRef.Text = ModelRef;
                    fldModelDescription.Text = ModelDescription;
                    fldModelType.Text = ModelType;
                    gpModel.Enabled = true;

                    // ** POST PART DATA FOR STEP **
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part[@IsSubPart='false']");
                    string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part";
                    if (chkShowSubParts.Checked == false)
                    {
                        xmlString += "[@IsSubPart='false']";
                    }
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes(xmlString);
                    XmlNodeList partNodeList = fullSetXml.SelectNodes(xmlString);
                    dgPartSummaryTable_Orig = GenerateStepPartTable(partNodeList);
                    dgPartSummary.DataSource = dgPartSummaryTable_Orig;
                    AdjustPartSummaryRowFormatting(dgPartSummary);
                    ProcessPartSummaryFilter();
                    lblPartCount.Text = dgPartSummaryTable_Orig.Rows.Count.ToString("#,##0") + " Part(s)";
                }
                else if (Type.Equals("SUBMODEL"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];                    
                    //string SubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + SubModelRef + "']/ancestor::SubSet/@Ref").InnerXml;
                    string Description = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@Description").InnerXml;
                    string ModelType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@LDrawModelType").InnerXml;
                    string PosX = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@PosX").InnerXml;
                    string PosY = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@PosY").InnerXml;
                    string PosZ = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@PosZ").InnerXml;
                    string RotX = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@RotX").InnerXml;
                    string RotY = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@RotY").InnerXml;
                    string RotZ = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/@RotZ").InnerXml;

                    // ** Post data **                    
                    fldSubModelCurrentRef.Text = SubModelRef;
                    fldSubModelDescription.Text = Description;
                    fldSubModelPosX.Text = PosX;
                    fldSubModelPosY.Text = PosY;
                    fldSubModelPosZ.Text = PosZ;
                    fldSubModelRotX.Text = RotX;
                    fldSubModelRotY.Text = RotY;
                    fldSubModelRotZ.Text = RotZ;
                    gpSubModel.Enabled = true;

                    // ** POST PART DATA FOR STEP **
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part[@IsSubPart='false']");
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part");                    
                    String xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Part";
                    if (chkShowSubParts.Checked == false)
                    {
                        xmlString += "[@IsSubPart='false']";
                    }
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(xmlString);
                    //DataTable partTable = GenerateStepPartTable(partNodeList);
                    //dgPartSummary.DataSource = partTable;
                    dgPartSummaryTable_Orig = GenerateStepPartTable(partNodeList);
                    dgPartSummary.DataSource = dgPartSummaryTable_Orig;
                    AdjustPartSummaryRowFormatting(dgPartSummary);
                    ProcessPartSummaryFilter();
                    lblPartCount.Text = dgPartSummaryTable_Orig.Rows.Count.ToString("#,##0") + " Part(s)";
                }
                else if (Type.Equals("STEP"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    string PureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
                    //String SubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + ModelRef + "']/ancestor::SubSet/@Ref").InnerXml;                    
                    string StepLevel = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepLevel").InnerXml;
                    string StepBook = "";
                    string StepPage = "";
                    if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepBook") != null)
                    {
                        StepBook = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepBook").InnerXml;
                        if (StepBook.Equals("0")) StepBook = "";                        
                        StepPage = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepPage").InnerXml;
                        if (StepPage.Equals("0")) StepPage = "";                        
                    }
                    string ModelRotX = "";
                    if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationX") != null)
                    {
                        ModelRotX = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationX").InnerXml;
                    }
                    string ModelRotY = "";
                    if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationY") != null)
                    {
                        ModelRotY = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationY").InnerXml;
                    }
                    string ModelRotZ = "";
                    if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationZ") != null)
                    {
                        ModelRotZ = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationZ").InnerXml;
                    }

                    // ** Post data **  
                    fldStepParentSubSetRef.Text = SubSetRef;
                    fldStepParentModelRef.Text = ModelRef;
                    fldPureStepNo.Text = PureStepNo;
                    fldStepLevel.Text = StepLevel;
                    fldStepBook.Text = StepBook.ToString();
                    fldStepPage.Text = StepPage.ToString();
                    fldModelRotX.Text = ModelRotX;
                    fldModelRotY.Text = ModelRotY;
                    fldModelRotZ.Text = ModelRotZ;
                    gpStep.Enabled = true;
                    gbPartDetails.Enabled = true;

                    // ** POST PART DATA FOR STEP **
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']//Part[@IsSubPart='false']");
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']//Part");                    
                    String xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']//Part";
                    if (chkShowSubParts.Checked == false) xmlString += "[@IsSubPart='false']";                                   
                    XmlNodeList partNodeList = fullSetXml.SelectNodes(xmlString);                    
                    dgPartSummaryTable_Orig = GenerateStepPartTable(partNodeList);
                    dgPartSummary.DataSource = dgPartSummaryTable_Orig;
                    AdjustPartSummaryRowFormatting(dgPartSummary);
                    ProcessPartSummaryFilter();
                    lblPartCount.Text = dgPartSummaryTable_Orig.Rows.Count.ToString("#,##0") + " Part(s)";
                }
                #endregion
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable GenerateStepPartTable(XmlNodeList partNodeList)
        {
            try
            {
                #region ** GET DATA UPFRONT **
                // Get a list of LDrawColourIDs & LDrawRefs
                List<int> LDrawColourIDList = new List<int>();
                List<string> LDrawRefList = new List<string>();
                foreach (XmlNode partNode in partNodeList)
                {
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
                }
                // ** Get a Collections for this data **
                PartColourCollection PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
                BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                CompositePartCollection CompositePartCollection = StaticData.GetCompositePartData_UsingLDrawRefList(LDrawRefList);

                // ** Get Element images **
                if (chkShowElementImages.Checked)
                {
                    //Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Screen - Getting images...");
                    //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, partListNodeList.Count);
                    //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                    int index = 0;
                    foreach (XmlNode partNode in partNodeList)
                    {
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                        Bitmap elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                        //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, index);
                        index += 1;
                    }
                    //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                }
                #endregion
                
                #region ** GENERATE COLUMNS **
                DataTable partTable = new DataTable("partTable", "partTable");                
                partTable.Columns.Add("Part Image", typeof(Bitmap));
                partTable.Columns.Add("LDraw Ref", typeof(string));
                partTable.Columns.Add("LDraw Colour ID", typeof(int));
                partTable.Columns.Add("LDraw Colour Name", typeof(string));
                partTable.Columns.Add("Colour Image", typeof(Bitmap));
                partTable.Columns.Add("Is Official", typeof(bool));
                partTable.Columns.Add("Unity FBX", typeof(bool));
                partTable.Columns.Add("Base Part Collection", typeof(bool));
                partTable.Columns.Add("Part Type", typeof(string));
                partTable.Columns.Add("Is SubPart", typeof(bool));
                partTable.Columns.Add("Step No", typeof(int));
                partTable.Columns.Add("Step Node Index", typeof(int));                
                partTable.Columns.Add("Placement Movements", typeof(string));
                partTable.Columns.Add("SubSet Ref", typeof(string));
                partTable.Columns.Add("PosX", typeof(string));
                partTable.Columns.Add("PosY", typeof(string));
                partTable.Columns.Add("PosZ", typeof(string));
                partTable.Columns.Add("RotX", typeof(string));
                partTable.Columns.Add("RotY", typeof(string));
                partTable.Columns.Add("RotZ", typeof(string));
                partTable.Columns.Add("Sub Part Count", typeof(int));
                partTable.Columns.Add("FBX Count", typeof(int));
                partTable.Columns.Add("FBX Size", typeof(long));
                partTable.Columns.Add("LDraw Part Type", typeof(string));
                partTable.Columns.Add("LDraw Description", typeof(string));
                partTable.Columns.Add("Unity Ref", typeof(string));
                #endregion
                   
                #region ** CYCLE THROUGH PART NODES AND GENERATE PART ROWS **  
                int stepNodeIndex = 0;
                int previousStepNo = 1;
                foreach (XmlNode partNode in partNodeList)
                {
                    #region ** GET LDRAW VARIABLES ** 
                    string SubSetRef = "";
                    if (partNode.SelectSingleNode("@SubSetRef") != null)
                    {
                        SubSetRef = partNode.SelectSingleNode("@SubSetRef").InnerXml;
                    }
                    string UnityRef = partNode.SelectSingleNode("@UnityRef").InnerXml;
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    bool IsSubPart = bool.Parse(partNode.SelectSingleNode("@IsSubPart").InnerXml);
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);  
                    string LDrawColourName = (from r in PartColourCollection.PartColourList
                                              where r.LDrawColourID == LDrawColourID
                                              select r.LDrawColourName).FirstOrDefault();                    
                    string partType = "";
                    string LDrawDescription = "";
                    string LDrawPartType = "";
                    if (IsSubPart == false)
                    {
                        partType = (from r in BasePartCollection.BasePartList
                                    where r.LDrawRef.Equals(LDrawRef)
                                    select r.partType.ToString()).FirstOrDefault();
                        LDrawPartType = (from r in BasePartCollection.BasePartList
                                         where r.LDrawRef.Equals(LDrawRef)
                                         select r.lDrawPartType.ToString()).FirstOrDefault();
                        LDrawDescription = (from r in BasePartCollection.BasePartList
                                            where r.LDrawRef.Equals(LDrawRef)
                                            select r.LDrawDescription).FirstOrDefault();
                    }
                    else
                    {                       
                        LDrawDescription = (from r in CompositePartCollection.CompositePartList
                                           where r.LDrawRef.Equals(LDrawRef)
                                           select r.LDrawDescription).FirstOrDefault();
                        // ** Infer other part details from parent **   
                        string parentLDrawRef = (from r in CompositePartCollection.CompositePartList
                                                where r.LDrawRef.Equals(LDrawRef)
                                                select r.ParentLDrawRef).FirstOrDefault();
                        partType = (from r in BasePartCollection.BasePartList
                                    where r.LDrawRef.Equals(parentLDrawRef)
                                    select r.partType.ToString()).FirstOrDefault();
                        LDrawPartType = (from r in BasePartCollection.BasePartList
                                         where r.LDrawRef.Equals(parentLDrawRef)
                                         select r.lDrawPartType.ToString()).FirstOrDefault();
                    }
                    #endregion

                    // ** Check for official/unoffical part **                    
                    bool IsOfficial = false;
                    if (LDrawPartType.Equals("OFFICIAL")) IsOfficial = true;

                    // Get LDrawDetails for part **
                    LDrawDetailsCollection ldd_coll = StaticData.GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
                    LDrawDetails LDrawDetails = ldd_coll.LDrawDetailsList[0];
                    
                    // ** GET FBX DETAILS FOR PART MODEL **
                    //TODO_H: The below is too slow - needs speeding up!
                    FBXDetails fbxDetails = StaticData.GetFBXDetails(LDrawRef, partType);

                    // ** Check BasePart Collection **
                    bool basePartCollection = true;                   
                    var BasePart = (from r in BasePartCollection.BasePartList
                                    where r.LDrawRef.Equals(LDrawRef)
                                    select r).FirstOrDefault();
                    if(BasePart == null) basePartCollection = false;
                   
                    // ** GET ELEMENT & PARTCOLOUR IMAGES **
                    Bitmap elementImage = null;
                    Bitmap partColourImage = null;
                    if (chkShowElementImages.Checked) elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });                    
                    if (chkShowPartcolourImages.Checked) partColourImage = ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });

                    // ** Get Placement Movements **
                    string placementMovementString = "";
                    XmlNodeList pmNodeList = partNode.SelectNodes("PlacementMovement");
                    for (int a = 0; a < pmNodeList.Count; a++)
                    {
                        string Axis = pmNodeList[a].SelectSingleNode("@Axis").InnerXml;
                        string Value = pmNodeList[a].SelectSingleNode("@Value").InnerXml;
                        if (a == 0) placementMovementString += Axis + "=" + Value;
                        else placementMovementString += "," + Axis + "=" + Value;
                    }

                    // ** Get Pos & Rot values **
                    string PosX = partNode.SelectSingleNode("@PosX").InnerXml;
                    string PosY = partNode.SelectSingleNode("@PosY").InnerXml;
                    string PosZ = partNode.SelectSingleNode("@PosZ").InnerXml;
                    string RotX = partNode.SelectSingleNode("@RotX").InnerXml;
                    string RotY = partNode.SelectSingleNode("@RotY").InnerXml;
                    string RotZ = partNode.SelectSingleNode("@RotZ").InnerXml;

                    // ** Get Pure Step No **
                    //string StepRef = partNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml;
                    int StepRef = int.Parse(partNode.SelectSingleNode("ancestor::Step/@PureStepNo").InnerXml);
                    if (StepRef != previousStepNo)
                    {
                        stepNodeIndex = 1;
                        previousStepNo = StepRef;
                    }
                    else
                    {
                        stepNodeIndex += 1;
                    }

                    // ** Build row and add to table **                    
                    DataRow newRow = partTable.NewRow();
                    newRow["Part Image"] = elementImage;
                    newRow["LDraw Ref"] = LDrawRef;
                    newRow["LDraw Colour ID"] = LDrawColourID;
                    newRow["LDraw Colour Name"] = LDrawColourName;
                    newRow["Colour Image"] = partColourImage;
                    newRow["Is Official"] = IsOfficial;
                    newRow["Unity FBX"] = fbxDetails.allFBXExist;
                    newRow["Base Part Collection"] = basePartCollection;
                    newRow["Part Type"] = partType;
                    newRow["Is SubPart"] = IsSubPart;
                    newRow["Step No"] = StepRef;
                    newRow["Step Node Index"] = stepNodeIndex;
                    newRow["Placement Movements"] = placementMovementString;
                    newRow["SubSet Ref"] = SubSetRef;
                    newRow["PosX"] = PosX;
                    newRow["PosY"] = PosY;
                    newRow["PosZ"] = PosZ;
                    newRow["RotX"] = RotX;
                    newRow["RotY"] = RotY;
                    newRow["RotZ"] = RotZ;
                    newRow["Sub Part Count"] = LDrawDetails.SubPartCount;
                    newRow["FBX Size"] = fbxDetails.fbxSize;
                    newRow["FBX Count"] = fbxDetails.fbxCount;
                    newRow["LDraw Part Type"] = LDrawPartType;
                    newRow["LDraw Description"] = LDrawDescription;
                    newRow["Unity Ref"] = UnityRef;
                    partTable.Rows.Add(newRow);
                }
                #endregion

                return partTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
                return null;
            }
        }

        private void AdjustPartSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustPartSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.Columns["Colour Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Colour Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();
                dg.Columns["Step Node Index"].DefaultCellStyle.Format = "#,###";
                dg.Columns["FBX Size"].DefaultCellStyle.Format = "#,##0";

                // ** Change colours of rows **
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.Cells["Is SubPart"].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        row.DefaultCellStyle.Font = new System.Drawing.Font(this.Font, FontStyle.Italic);
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }                                    
                    if (row.Cells["Unity FBX"].Value.ToString().ToUpper().Equals("FALSE"))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                    if (row.Cells["Is SubPart"].Value.ToString().ToUpper().Equals("FALSE"))
                    {
                        if (row.Cells["Base Part Collection"].Value.ToString().ToUpper().Equals("FALSE"))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
                    }                    
                }
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].ExpandAll();            
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            if (tvSetSummary.Nodes.Count > 0) tvSetSummary.Nodes[0].Collapse(false);
        }

        #endregion

        #region ** REBRICKABLE MATCHING FUNCTIONS **

        private async void CompareSetPartsWithRebrickable()
        {
            try
            {
                // ** Validation Checks **               
                if (fullSetXml == null) throw new Exception("No Set Ref loaded...");
                string SetRef = fldCurrentSetRef.Text;

                // ** GET SOURCE TABLE FROM FULL SET XML **
                XmlNodeList partListNodeList = fullSetXml.SelectNodes("//PartListPart");
                DataTable sourceTable = GeneratePartListTable(partListNodeList);
                sourceTable.Columns.Add("Matched", typeof(string));
                for (int a = 0; a < sourceTable.Rows.Count; a++) sourceTable.Rows[a]["Matched"] = "False";

                // ** GET TARGET TABLE FROM REBRICKABLE **                
                string url = "https://rebrickable.com/api/v3/lego/sets/" + SetRef + "/parts/";
                string JSONString = "";
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "application/json");
                        request.Headers.TryAddWithoutValidation("Authorization", "key " + Global_Variables.RebrickableKey);
                        var response = await httpClient.SendAsync(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            JSONString = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
                DataTable targetTable = GeneratePartListTableFromRebrickable(JSONString);
                targetTable.Columns.Add("Matched", typeof(string));
                for (int a = 0; a < targetTable.Rows.Count; a++) targetTable.Rows[a]["Matched"] = "False";
                
                #region ** RUN MATCHING PROCESS **
                bool overallMatch = true;
                for (int a = 0; a < sourceTable.Rows.Count; a++)
                {
                    string Set_LDrawRef = sourceTable.Rows[a]["LDraw Ref"].ToString();
                    int Set_LDrawColourID = int.Parse(sourceTable.Rows[a]["LDraw Colour ID"].ToString());
                    int Set_Qty = int.Parse(sourceTable.Rows[a]["Qty"].ToString());

                    // ** Find match **
                    var targetPart = (from r in targetTable.AsEnumerable()
                                      where r.Field<string>("Matched").Equals("False")
                                      && r.Field<string>("LDraw Ref").Equals(Set_LDrawRef)
                                      && r.Field<int>("LDraw Colour ID") == Set_LDrawColourID
                                      && r.Field<int>("Qty") == Set_Qty
                                      select r).FirstOrDefault();
                    if (targetPart != null)
                    {
                        sourceTable.Rows[a]["Matched"] = true;
                        targetPart["Matched"] = true;
                    }
                }
                int SourceUnmatchedCount = (from r in sourceTable.AsEnumerable()
                                            where r.Field<string>("Matched").Equals("False")
                                            select r).Count();
                int TargetUnmatchedCount = (from r in targetTable.AsEnumerable()
                                            where r.Field<string>("Matched").Equals("False")
                                            select r).Count();
                if (SourceUnmatchedCount > 0 || TargetUnmatchedCount > 0)
                {
                    overallMatch = false;
                }
                #endregion

                // ** IF OVERALL MATCH = FALSE, SHOW Matching Screen **
                if (overallMatch == false)
                {
                    MatchingScreen form = new MatchingScreen() { sourceTable = sourceTable, targetTable = targetTable };                    
                    form.Refresh_Screen();
                    form.Visible = true;
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable GeneratePartListTableFromRebrickable(string JSONString)
        {            
            try
            {
                // ** Load JSON string to XML **
                var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(JSONString), new XmlDictionaryReaderQuotas()));
                string XMLString = xml.ToString();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList partItemList = doc.SelectNodes("//item[@type='object' and is_spare='false']");

                #region ** GET DATA UPFRONT **
                // Get a list of LDrawColourIDs & LDrawRefs
                List<int> LDrawColourIDList = new List<int>();
                List<string> LDrawRefList = new List<string>();
                foreach (XmlNode partNode in partItemList)
                {
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/id").InnerXml);
                    if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                    string LDrawRef = partNode.SelectSingleNode("part/part_num").InnerXml;
                    if (partNode.SelectSingleNode("part/external_ids/LDraw") != null)
                    {
                        LDrawRef = partNode.SelectSingleNode("part/external_ids/LDraw/item").InnerXml;
                    }
                    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);

                    // ** Get Element images **                
                    if (chkShowElementImages.Checked)
                    {
                        Bitmap elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    }
                }
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

                // ** Cycle through nodes and generate table rows **               
                foreach (XmlNode partNode in partItemList)
                {
                    // ** GET LDRAW VARIABLES **                    
                    string LDrawRef = partNode.SelectSingleNode("part/part_num").InnerXml;
                    if (partNode.SelectSingleNode("part/external_ids/LDraw") != null)
                    {
                        LDrawRef = partNode.SelectSingleNode("part/external_ids/LDraw/item").InnerXml;
                    }
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/id").InnerXml);
                    int Qty = int.Parse(partNode.SelectSingleNode("quantity").InnerXml); 
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

                    // ** Build row **
                    object[] row = new object[partListTable.Columns.Count];
                    row[0] = elementImage;
                    row[1] = LDrawRef;
                    row[2] = LDrawDescription;
                    row[3] = LDrawColourID;
                    row[4] = LDrawColourName;
                    row[5] = partColourImage;
                    row[6] = Qty;
                    partListTable.Rows.Add(row);
                }
                return partListTable;
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                //MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
                return null;                
            }
        }

        #endregion









        #region ** SUBSET FUNCTIONS **

        private void AddSubSet()
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No Set node selected...");
                }
                string parentType = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                if (parentType != "SET")
                {
                    throw new Exception("Cannot add a SubSet to this node...");
                }

                // IDENTIFY THE PARENT NODE                
                XmlNode parentNode = currentSetXml.SelectSingleNode("//SubSetList");

                // ** Add SubSet to selected node **
                SubSet newSubSet = new SubSet();
                newSubSet.Ref = "NEW SUBSET";
                newSubSet.Description = "NEW SUBSET";
                newSubSet.SubSetType = "OFFICIAL";

                // ** Add BuildInstructions **
                BuildInstructions bi = new BuildInstructions();
                newSubSet.buildInstructions = bi;

                // ** Add FinalModel **
                SubModel FinalModel = new SubModel();
                bi.SubModel = FinalModel;
                FinalModel.Ref = "S1";
                FinalModel.Description = fldSubSetDescription.Text;
                //FinalModel.LDrawModelType = "FINAL_MODEL";
                FinalModel.lDrawModelType = SubModel.LDrawModelType.FINAL_MODEL;
                FinalModel.SubModelLevel = 0;

                // ** Add SubModel to XML node ** 
                string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newSubSet.SerializeToString(true));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode newNode = doc.DocumentElement;
                XmlNode importNode = parentNode.OwnerDocument.ImportNode(newNode, true);
                parentNode.AppendChild(importNode);

                // ** Tidy Up **
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveSubSet()
        {
            try
            {
                // ** UPDATE XML DOC **
                string oldSubSetRef = fldSubSetCurrentRef.Text;
                string newSubSetRef = fldSubSetNewRef.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + oldSubSetRef + "']/@Description").InnerXml = fldSubSetDescription.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + oldSubSetRef + "']/@SubSetType").InnerXml = fldSubSetType.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + oldSubSetRef + "']/BuildInstructions/SubModel/@Description").InnerXml = fldSubSetDescription.Text;
                if (fldSubSetNewRef.Text != "") // UPDATE REF LAST SO REFS ABOVE WORK CORRECTLY
                {
                    currentSetXml.SelectSingleNode("//SubSet[@Ref='" + oldSubSetRef + "']/@Ref").InnerXml = newSubSetRef;
                    //currentSetXml.SelectSingleNode("//SubSet[@Ref='" + newSubSetRef + "']/BuildInstructions/SubModel/@Ref").InnerXml = newSubSetRef;
                }

                // ** Tidy up **
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteSubSet()
        {
            try
            {
                // ** Remove node from XML **
                string subSetRef = fldSubSetCurrentRef.Text;
                XmlNode removalNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + subSetRef + "']");
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);

                // ** Tidy Up **
                ClearAllFields();
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** MODEL FUNCTIONS **

        private void AddModel()
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No SubSet node selected...");
                }
                string parentType = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                if (parentType != "SUBSET")
                {
                    throw new Exception("Cannot add a Model to this node...");
                }

                // Identify parent SubSet
                //String parentSubSetRef = tvSetSummary.SelectedNode.Text.Split('|')[0].Trim();
                string parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1].Trim();
                XmlNode parentNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']/BuildInstructions/SubModel");

                // ** Add Model SubModel to selected node **
                SubModel newModel = new SubModel() { Ref = "NEW MODEL", Description = "NEW MODEL", lDrawModelType = SubModel.LDrawModelType.MODEL, SubModelLevel = 1 };
                //newModel.Ref = "NEW MODEL";
                //newModel.Description = "NEW MODEL";
                //newModel.LDrawModelType = "MODEL";
                //newModel.SubModelLevel = 1;
                string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newModel.SerializeToString(true));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode newNode = doc.DocumentElement;
                XmlNode SubModelNode = parentNode.OwnerDocument.ImportNode(newNode, true);
                parentNode.AppendChild(SubModelNode);

                // ** ADD first STEP TO MODEL **                
                Step newStep = new Step() { PureStepNo = 1, StepLevel = 1, state = Step.StepState.NOT_COMPLETED, StepBook = 0, StepPage = 0 };
                //newStep.PureStepNo = 1;
                //newStep.StepLevel = 1;
                //newStep.state = Step.StepState.NOT_COMPLETED;
                //newStep.StepBook = 0;
                //newStep.StepPage = 0;
                xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newStep.SerializeToString(true));
                doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                newNode = doc.DocumentElement;
                XmlNode StepNode = SubModelNode.OwnerDocument.ImportNode(newNode, true);
                SubModelNode.AppendChild(StepNode);
               
                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveModel()
        {
            try
            {
                // ** UPDATE XML DOC **
                String oldModelRef = fldModelCurrentRef.Text;
                String ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1].Trim();
                //String ParentSubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + oldModelRef + "']/ancestor::SubSet/@Ref").InnerXml;
                String newModelRef = fldModelNewRef.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldModelRef + "']/@Description").InnerXml = fldModelDescription.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldModelRef + "']/@LDrawModelType").InnerXml = fldModelType.Text;
                if (fldModelNewRef.Text != "") // UPDATE REF LAST SO REFS ABOVE WORK CORRECTLY
                {
                    currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldModelRef + "']/@Ref").InnerXml = newModelRef;
                }

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteModel()
        {
            try
            {
                // ** Get SubSet & Model refs **
                string modelRef = fldModelCurrentRef.Text;
                string ParentSubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + modelRef + "']/ancestor::SubSet/@Ref").InnerXml;

                // ** Remove node from XML **                
                XmlNode removalNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + modelRef + "']");
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** SUBMODEL FUNCTIONS **
        
        private void InsertSubModel(int StepCount, string insertType)
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No node selected...");
                }                
                string elementType = lastSelectedNode.Tag.ToString().Split('|')[0];               
                if (elementType != "STEP")
                {
                    throw new Exception("No Step selected...");
                }
                
                // ** GET MAIN VARIABLES FROM LAST SELECTED NODE (which will be Step) **              
                string parentSubSetRef = lastSelectedNode.Tag.ToString().Split('|')[1];
                string parentModelRef = lastSelectedNode.Tag.ToString().Split('|')[2];
                int StepNo = int.Parse(lastSelectedNode.Tag.ToString().Split('|')[3]);

                // ** DERIVE VARIABLES FROM XML **                
                XmlNode stepNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + StepNo + "']");
                int StepLevel = int.Parse(stepNode.SelectSingleNode("@StepLevel").InnerXml);
                string parentSubModelRef = stepNode.SelectSingleNode("parent::SubModel/@Ref").InnerXml;
                XmlNode parentSubModelNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']");
                
                // ** Generate the relevant number of steps **
                for (int a = 0; a < StepCount; a++)
                {
                    // ** Add SubModel to selected node **
                    SubModel newSubModel = new SubModel();
                    newSubModel.Ref = "NEW SUBMODEL";
                    newSubModel.Description = "NEW SUBMODEL";
                    newSubModel.lDrawModelType = SubModel.LDrawModelType.SUB_MODEL;
                    newSubModel.SubModelLevel = StepLevel + 1;

                    // ** Add SubModel to XML node ** 
                    string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newSubModel.SerializeToString(true));
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlNodeString);
                    XmlNode newNode = doc.DocumentElement;
                    XmlNode importNode = parentSubModelNode.OwnerDocument.ImportNode(newNode, true);                    
                    if (insertType.Equals("BEFORE"))
                    {
                        parentSubModelNode.InsertBefore(importNode, stepNode);
                    }
                    else if (insertType.Equals("AFTER"))
                    {
                        parentSubModelNode.InsertAfter(importNode, stepNode);
                    }
                    else if (insertType.Equals("END"))
                    {
                        parentSubModelNode.AppendChild(importNode);
                    }
                }
                
                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void DeleteSubModel()
        {
            try
            {
                // ** Get SubSet & Model refs **
                string SubModelRef = fldSubModelCurrentRef.Text;
                string ParentModelRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + SubModelRef + "']/ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                string ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];

                // ** Remove node from XML **                
                XmlNode removalNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']");
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);

                // ** Adjust SuBSet PureStep Numbers **
                AdjustSubSetStepNumbers(ParentSubSetRef, ParentModelRef);

                // ** Update PartList **
                //RecalculatePartList();
                RecalculatePartList(currentSetXml);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        private void SaveSubModel()
        {
            try
            {
                // ** UPDATE XML DOC **
                string ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                string oldSubModelRef = fldSubModelCurrentRef.Text;
                string newSubModelRef = fldSubModelNewRef.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@Description").InnerXml = fldSubModelDescription.Text;
                if (fldSubModelNewRef.Text != "") // UPDATE REF LAST SO REFS ABOVE WORK CORRECTLY
                {
                    currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@Ref").InnerXml = newSubModelRef;
                    oldSubModelRef = newSubModelRef;
                }

                // ** UPDATE POS & ROT **
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@PosX").InnerXml = fldSubModelPosX.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@PosY").InnerXml = fldSubModelPosY.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@PosZ").InnerXml = fldSubModelPosZ.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@RotX").InnerXml = fldSubModelRotX.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@RotY").InnerXml = fldSubModelRotY.Text;
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + oldSubModelRef + "']/@RotZ").InnerXml = fldSubModelRotZ.Text;

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DuplicateSubModel(string insertType)
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No node selected...");
                }
                string elementType = lastSelectedNode.Tag.ToString().Split('|')[0];
                if (elementType != "SUBMODEL")
                {
                    throw new Exception("No SubModel selected...");
                }

                // ** Get SubSet & Model refs **
                string SubModelRef = fldSubModelCurrentRef.Text;
                string ParentModelRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + SubModelRef + "']/ancestor::SubModel[@SubModelLevel=1]/@Ref").InnerXml;
                string ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];

                // ** Get old & new node destails **                
                XmlNode oldNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']");
                XmlNode parentNode = oldNode.ParentNode;

                // ** Add SubModel to XML node ** 
                string xmlNodeString = oldNode.OuterXml;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode newNode = doc.DocumentElement;
                XmlNode importNode = parentNode.OwnerDocument.ImportNode(newNode, true);
                importNode.SelectSingleNode("@Ref").InnerXml += "_" + DateTime.Now.ToString("hhmmss");
                if (insertType.Equals("BEFORE"))
                {
                    parentNode.InsertBefore(importNode, oldNode);
                }
                else if (insertType.Equals("AFTER"))
                {
                    parentNode.InsertAfter(importNode, oldNode);
                }
                else if (insertType.Equals("END"))
                {
                    parentNode.AppendChild(importNode);
                }

                // ** Adjust SuBSet PureStep Numbers **
                AdjustSubSetStepNumbers(ParentSubSetRef, ParentModelRef);

                // ** Recalculate SubSet Refs **
                int totalParts;
                int totalSubSets;
                RecalculateSubSetRefs(currentSetXml, out totalParts, out totalSubSets);

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** STEP FUNCTIONS **

        private void InsertStep(int StepCount, string insertType)
        {
            try
            {
                // ** Validation checks **               
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (lastSelectedNode == null)
                {
                    throw new Exception("No node selected...");
                }
                string elementType = lastSelectedNode.Tag.ToString().Split('|')[0];
                
                // ** GET VARIABLES **
                string parentSubSetRef = "";
                string parentModelRef = "";
                string parentSubModelRef = "";
                int StepLevel = 0;
                XmlNode stepNode = null;
                if (elementType.Equals("MODEL"))
                {
                    parentSubSetRef = lastSelectedNode.Tag.ToString().Split('|')[1];
                    parentModelRef = lastSelectedNode.Tag.ToString().Split('|')[2];
                    parentSubModelRef = parentModelRef;
                    StepLevel = int.Parse(currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']/@SubModelLevel").InnerXml);
                }
                else if (elementType.Equals("SUBMODEL"))
                {
                    parentSubSetRef = lastSelectedNode.Tag.ToString().Split('|')[1];
                    parentSubModelRef = lastSelectedNode.Tag.ToString().Split('|')[2];                    
                    parentModelRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']/parent::SubModel/@Ref").InnerXml;
                    StepLevel = int.Parse(currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']/@SubModelLevel").InnerXml);
                }
                else if (elementType.Equals("STEP"))
                {
                    parentSubSetRef = lastSelectedNode.Tag.ToString().Split('|')[1];
                    parentModelRef = lastSelectedNode.Tag.ToString().Split('|')[2];
                    int StepNo = int.Parse(lastSelectedNode.Tag.ToString().Split('|')[3]);
                    stepNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + StepNo + "']");
                    StepLevel = int.Parse(stepNode.SelectSingleNode("@StepLevel").InnerXml);
                    parentSubModelRef = stepNode.SelectSingleNode("parent::SubModel/@Ref").InnerXml;                    
                }
                XmlNode parentSubModelNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']");
                
                // ** Generate the relevant number of steps **
                for (int a = 0; a < StepCount; a++)
                {
                    // ** Add Step to selected node **
                    Step newStep = new Step();
                    newStep.PureStepNo = 0;
                    newStep.StepLevel = StepLevel;
                    newStep.state = Step.StepState.NOT_COMPLETED;
                    newStep.StepBook = 0;
                    newStep.StepPage = 0;

                    // ** Add SubModel to XML node ** 
                    string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newStep.SerializeToString(true));
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlNodeString);
                    XmlNode newNode = doc.DocumentElement;
                    XmlNode importNode = parentSubModelNode.OwnerDocument.ImportNode(newNode, true);
                    if (insertType.Equals("BEFORE"))
                    {
                        parentSubModelNode.InsertBefore(importNode, stepNode);
                    }
                    else if (insertType.Equals("AFTER"))
                    {
                        parentSubModelNode.InsertAfter(importNode, stepNode);
                    }
                    else if (insertType.Equals("END"))
                    {
                        parentSubModelNode.AppendChild(importNode);
                    }
                }

                // ** Adjust all Pure Step nodes in the SubSet **                
                AdjustSubSetStepNumbers(parentSubSetRef, parentModelRef);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteStep()
        {
            try
            {
                // ** Get SubSet & Model refs **
                string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                string ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                //String ParentSubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + SubModelRef + "']/ancestor::SubSet/@Ref").InnerXml;
                string PureStepNo = fldPureStepNo.Text;
                string ParentModelRef = fldStepParentModelRef.Text;

                // ** Remove node from XML **                
                XmlNode removalNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']");
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);

                // ** Adjust SuBSet PureStep Numbers **
                AdjustSubSetStepNumbers(ParentSubSetRef, ParentModelRef);

                // ** Update PartList **
                //RecalculatePartList();
                RecalculatePartList(currentSetXml);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SaveStep()
        {
            try
            {
                // ** Get variables **
                string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                string ParentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                string PureStepNo = fldPureStepNo.Text;
                int StepBook = 0;
                int.TryParse(fldStepBook.Text, out StepBook);
                int StepPage = 0;
                int.TryParse(fldStepPage.Text, out StepPage);

                float ModelRotX = 0;
                float.TryParse(fldModelRotX.Text, out ModelRotX);
                float ModelRotY = 0;
                float.TryParse(fldModelRotY.Text, out ModelRotY);
                float ModelRotZ = 0;
                float.TryParse(fldModelRotZ.Text, out ModelRotZ);
                
                // ** Update XML **
                if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepBook") == null)
                {
                    XmlNode parentNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']");
                    XmlDocument doc = parentNode.OwnerDocument;
                    XmlAttribute newAttribute = doc.CreateAttribute("StepBook");
                    parentNode.Attributes.SetNamedItem(newAttribute);
                }
                if (currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepPage") == null)
                {
                    XmlNode parentNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']");
                    XmlDocument doc = parentNode.OwnerDocument;
                    XmlAttribute newAttribute = doc.CreateAttribute("StepPage");
                    parentNode.Attributes.SetNamedItem(newAttribute);
                }
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepBook").InnerXml = StepBook.ToString();
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@StepPage").InnerXml = StepPage.ToString();
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationX").InnerXml = ModelRotX.ToString();
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationY").InnerXml = ModelRotY.ToString();
                currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Step[@PureStepNo='" + PureStepNo + "']/@ModelRotationZ").InnerXml = ModelRotZ.ToString();
                
                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DuplicateStep(string insertType)
        {
            try
            {
                // ** Validation checks **               
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (lastSelectedNode == null)
                {
                    throw new Exception("No node selected...");
                }
                string elementType = lastSelectedNode.Tag.ToString().Split('|')[0];
                if(elementType != "STEP")
                {
                    throw new Exception("Step not selected...");
                }
                    
                // ** GET VARIABLES **               
                string ParentSubSetRef = lastSelectedNode.Tag.ToString().Split('|')[1];
                string ParentModelRef = lastSelectedNode.Tag.ToString().Split('|')[2];
                int StepNo = int.Parse(lastSelectedNode.Tag.ToString().Split('|')[3]);
                XmlNode oldNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + ParentSubSetRef + "']//SubModel[@Ref='" + ParentModelRef + "']//Step[@PureStepNo='" + StepNo + "']");
                XmlNode parentNode = oldNode.ParentNode;
                string parentSubModelRef = oldNode.SelectSingleNode("parent::SubModel/@Ref").InnerXml;

                // ** Add Step to XML node ** 
                string xmlNodeString = oldNode.OuterXml;
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode newNode = doc.DocumentElement;
                XmlNode importNode = parentNode.OwnerDocument.ImportNode(newNode, true);
                if (insertType.Equals("BEFORE"))
                {
                    parentNode.InsertBefore(importNode, oldNode);
                }
                else if (insertType.Equals("AFTER"))
                {
                    parentNode.InsertAfter(importNode, oldNode);
                }
                else if (insertType.Equals("END"))
                {
                    parentNode.AppendChild(importNode);
                }

                // ** Adjust SuBSet PureStep Numbers **
                AdjustSubSetStepNumbers(ParentSubSetRef, ParentModelRef);

                // ** Recalculate SubSet Refs **
                int totalParts;
                int totalSubSets;
                RecalculateSubSetRefs(currentSetXml, out totalParts, out totalSubSets);

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** FIELD LEAVE FUNCTIONS **

        private void fldLDrawColourName_Leave(object sender, EventArgs e)
        {
            ProcessLDrawColourName_Leave();
        }

        private void fldLDrawColourID_Leave(object sender, EventArgs e)
        {
            ProcessLDrawColourID_Leave();
        }

        private void fldLDrawRef_Leave(object sender, EventArgs e)
        {
            ProcessLDrawRef_Leave();
        }

        private void fldLDrawColourName_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProcessLDrawColourName_Leave();
        }

        private void ProcessLDrawColourName_Leave()
        {
            try
            {
                // ** Lookup LDraw Colour ID **
                if(fldLDrawColourName.Text != "")
                {                    
                    int LDrawColourID = StaticData.GetLDrawColourID(fldLDrawColourName.Text);
                    fldLDrawColourID.Text = LDrawColourID.ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessLDrawColourID_Leave()
        {
            try
            {
                // ** Lookup LDraw Colour ID **
                if (fldLDrawColourID.Text != "")
                {
                    string LDrawColourName = StaticData.GetLDrawColourName(int.Parse(fldLDrawColourID.Text));
                    fldLDrawColourName.Text = LDrawColourName;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessLDrawRef_Leave()
        {
            try
            {
                // ** GET VARIABLES **
                string LDrawRef = fldLDrawRef.Text;
                
                // ** GET LDRAW IMAGE **                
                fldLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });

                // ** POST LDRAW DETAILS **
                gbPartDetails.Text = "Part Details | ???";
                BasePartCollection coll = StaticData.GetBasePartData_UsingLDrawRefList(new List<string>() { LDrawRef });
                if (coll.BasePartList.Count > 0)                
                {
                    // ** Post data **
                    string description = coll.BasePartList[0].LDrawDescription;
                    if(description != "") gbPartDetails.Text = "Part Details | " + description;
                    fldPartType.Text = coll.BasePartList[0].partType.ToString();
                    fldLDrawSize.Text = "";
                    if(coll.BasePartList[0].LDrawSize > 0) fldLDrawSize.Text = coll.BasePartList[0].LDrawSize.ToString();
                    chkIsSubPart.Checked = coll.BasePartList[0].IsSubPart;
                    chkIsSticker.Checked = coll.BasePartList[0].IsSticker;
                    chkIsLargeModel.Checked = coll.BasePartList[0].IsLargeModel;
                }
                else
                {                    
                    // check if LDrawDetails exist, if yes, use the value, if not save the ldrawdetails                    
                    LDrawDetailsCollection ldd_coll = StaticData.GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
                    if(ldd_coll.LDrawDetailsList.Count > 0)
                    {
                        fldPartType.Text = ldd_coll.LDrawDetailsList[0].PartType;
                        gbPartDetails.Text = "Part Details | " + ldd_coll.LDrawDetailsList[0].LDrawDescription;
                        fldPartType.Text = ldd_coll.LDrawDetailsList[0].PartType.ToString();
                    }                                     
                }

                // ** UPDATE BASE PART COLLECTION BOOLEAN - CHECK IF PART IS IN BASE PART COLLECTION **
                if (coll.BasePartList.Count > 0)
                {
                    chkBasePartCollection.Checked = true;
                    btnAddPartToBasePartCollection2.Enabled = false;
                    btnAddPartToBasePartCollection2.BackColor = Color.Transparent;
                    tsBasePartCollection.Enabled = false;
                }
                else
                {
                    chkBasePartCollection.Checked = false;
                    btnAddPartToBasePartCollection2.Enabled = true;
                    btnAddPartToBasePartCollection2.BackColor = Color.Red;
                    tsBasePartCollection.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void UpdateBasePartCollectionBoolean()
        //{
        //    try
        //    {
        //        // ** UPDATE BASE PART COLLECTION BOOLEAN - CHECK IF PART IS IN BASE PART COLLECTION **
        //        string LDrawRef = fldLDrawRef.Text;
        //        if (Global_Variables.BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + fldLDrawRef.Text + "']") != null)
        //        {
        //            chkBasePartCollection.Checked = true;
        //            btnAddPartToBasePartCollection2.Enabled = false;
        //            btnAddPartToBasePartCollection2.BackColor = Color.Transparent;
        //            tsBasePartCollection.Enabled = false;
        //        }
        //        else
        //        {
        //            chkBasePartCollection.Checked = false;
        //            btnAddPartToBasePartCollection2.Enabled = true;
        //            btnAddPartToBasePartCollection2.BackColor = Color.Red;
        //            tsBasePartCollection.Enabled = true;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        #endregion

        #region ** RECALCULATE FUNCTIONS **

        //private void RecalculatePartList_OLD()
        //{
        //    try
        //    {
        //        // ** Cycle through all SubSets **
        //        Dictionary<String, int> masterPartCount = new Dictionary<string, int>();
        //        XmlNodeList SubSetNodeList = currentSetXml.SelectNodes("//SubSet");
        //        foreach (XmlNode SubSetNode in SubSetNodeList)
        //        {
        //            // ** Get variables **
        //            String subSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;

        //            // ** Generate Part Count Dictionary **
        //            Dictionary<String, int> partCount = new Dictionary<string, int>();
        //            //XmlNodeList BuildPartsNodeList = SubSetNode.SelectNodes("BuildInstructions/*//Part[@IsSubPart='false']");
        //            XmlNodeList BuildPartsNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + subSetRef + "']//Part[@IsSubPart='false']");
        //            foreach (XmlNode partNode in BuildPartsNodeList)
        //            {
        //                // ** Get variables **
        //                String LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
        //                String LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
        //                String partKey = LDrawRef + "|" + LDrawColourID;

        //                // ** Get Part Count details **
        //                if (partCount.ContainsKey(partKey) == false)
        //                {
        //                    partCount.Add(partKey, 0);
        //                }
        //                partCount[partKey] += 1;
        //            }

        //            // ** Compare part count with master part count **
        //            foreach (String partKey in partCount.Keys)
        //            {
        //                if (masterPartCount.ContainsKey(partKey) == false)
        //                {
        //                    masterPartCount.Add(partKey, partCount[partKey]);
        //                }
        //                else
        //                {
        //                    int partQty = partCount[partKey];
        //                    int masterQty = masterPartCount[partKey];
        //                    if (partQty > masterQty)
        //                    {
        //                        masterPartCount[partKey] = partQty;
        //                    }
        //                }
        //            }
        //        }

        //        // ** Generate Master PartList **
        //        PartList pl = new PartList();
        //        foreach (String partKey in masterPartCount.Keys)
        //        {
        //            // ** Get variables **           
        //            String LDrawRef = partKey.Split('|')[0];
        //            int LDrawColourID = int.Parse(partKey.Split('|')[1]);

        //            // ** Generate part **
        //            PartListPart plp = new PartListPart();
        //            plp.LDrawRef = LDrawRef;
        //            plp.LDrawColourID = LDrawColourID;
        //            plp.Qty = masterPartCount[partKey];
        //            pl.partList.Add(plp);
        //        }

        //        // ** Update PartList Node **
        //        XmlNode parentNode = currentSetXml.SelectSingleNode("//PartList");
        //        parentNode.RemoveAll();
        //        String xmlNodeString = HelperFunctions.RemoveAllNamespaces(pl.SerializeToString(true));
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(xmlNodeString);
        //        XmlNode newNode = doc.DocumentElement;
        //        foreach (XmlNode node in newNode.ChildNodes)
        //        {
        //            XmlNode importNode = parentNode.OwnerDocument.ImportNode(node, true);
        //            parentNode.AppendChild(importNode);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void RecalculatePartList(XmlDocument setXML)
        {
            try
            {
                // ** Cycle through all SubSets **
                Dictionary<string, int> masterPartCount = new Dictionary<string, int>();
                XmlNodeList SubSetNodeList = setXML.SelectNodes("//SubSet");
                foreach (XmlNode SubSetNode in SubSetNodeList)
                {
                    // ** Get variables **
                    string subSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;

                    // ** Generate Part Count Dictionary **
                    Dictionary<string, int> partCount = new Dictionary<string, int>();
                    //XmlNodeList BuildPartsNodeList = SubSetNode.SelectNodes("BuildInstructions/*//Part[@IsSubPart='false']");
                    XmlNodeList BuildPartsNodeList = setXML.SelectNodes("//SubSet[@Ref='" + subSetRef + "']//Part[@IsSubPart='false']");
                    foreach (XmlNode partNode in BuildPartsNodeList)
                    {
                        // ** Get variables **
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        string LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
                        string partKey = LDrawRef + "|" + LDrawColourID;

                        // ** Get Part Count details **
                        if (partCount.ContainsKey(partKey) == false)
                        {
                            partCount.Add(partKey, 0);
                        }
                        partCount[partKey] += 1;
                    }

                    // ** Compare part count with master part count **
                    foreach (string partKey in partCount.Keys)
                    {
                        if (masterPartCount.ContainsKey(partKey) == false)
                        {
                            masterPartCount.Add(partKey, partCount[partKey]);
                        }
                        else
                        {
                            int partQty = partCount[partKey];
                            int masterQty = masterPartCount[partKey];
                            if (partQty > masterQty)
                            {
                                masterPartCount[partKey] = partQty;
                            }
                        }
                    }
                }

                // ** Generate Master PartList **
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
                    pl.partList.Add(plp);
                }

                // ** Update PartList Node **
                XmlNode parentNode = setXML.SelectSingleNode("//PartList");
                parentNode.RemoveAll();
                string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(pl.SerializeToString(true));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode newNode = doc.DocumentElement;
                foreach (XmlNode node in newNode.ChildNodes)
                {
                    XmlNode importNode = parentNode.OwnerDocument.ImportNode(node, true);
                    parentNode.AppendChild(importNode);
                }
            }
            catch (Exception ex)
            {
                //return null;
                MessageBox.Show(ex.Message);
            }
        }

        //private void RecalculateUnityRefs_OLD(XmlDocument setXML)
        //{
        //    try
        //    {
        //        Dictionary<string, int> UnityRefList = new Dictionary<string, int>();
        //        XmlNodeList SubSetNodeList = setXML.SelectNodes("//SubSet");
        //        foreach (XmlNode SubSetNode in SubSetNodeList)
        //        {
        //            // ** Get variables **
        //            string subSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;

        //            // ** Generate Unity Ref part count Dictionary **    
        //            int index = 1;
        //            XmlNodeList PartsNodeList = setXML.SelectNodes("//SubSet[@Ref='" + subSetRef + "']//Part");
        //            foreach (XmlNode partNode in PartsNodeList)
        //            {
        //                // ** Get variables **
        //                string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
        //                int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);

        //                // ** Get Unity ref **
        //                string LDrawKey = subSetRef + "|" + LDrawRef + "|" + LDrawColourID;
        //                if (UnityRefList.ContainsKey(LDrawKey) == false) UnityRefList.Add(LDrawKey, 0);
        //                UnityRefList[LDrawKey] += 1;                       
        //                string unityRef = LDrawRef + "|" + LDrawColourID + "." + UnityRefList[LDrawKey];

        //                // ** Update Set Xml **                        
        //                string xpath = "(//SubSet[@Ref='" + subSetRef + "']//Part)[" + index + "]/@UnityRef";
        //                setXML.SelectSingleNode(xpath).InnerXml = unityRef;

        //                // ** Update Index **
        //                index += 1;
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void RecalculateSubSetRefs_OLD(XmlDocument setXML)
        //{
        //    try
        //    {
        //        // ** Validation checks **
        //        if (currentSetXml == null)
        //        {
        //            throw new Exception("No Set loaded");
        //        }

        //        // ** Recalculate Unity refs for all SubSets **
        //        int totalParts = 0;
        //        XmlNodeList SubSetNodeList = setXML.SelectNodes("//SubSet");
        //        foreach (XmlNode SubSetNode in SubSetNodeList)
        //        {
        //            // ** Get variables **
        //            string subSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;

        //            // ** Update XML Node **
        //            int index = 1;
        //            string xmlString = "//SubSet[@Ref='" + subSetRef + "']//SubModel[@LDrawModelType='MODEL']//Part";
        //            XmlNodeList PartsNodeList = setXML.SelectNodes(xmlString);
        //            foreach (XmlNode partNode in PartsNodeList)
        //            {
        //                // ** Update Set Xml **
        //                string SubSetRef = subSetRef + "|" + index;
        //                partNode.SelectSingleNode("@SubSetRef").InnerXml = SubSetRef;
        //                //partNode.SelectSingleNode("@UnityRef").InnerXml = "";

        //                // ** Update Index **
        //                index += 1;
        //            }
        //            totalParts += PartsNodeList.Count;
        //        }

        //        // ** Refresh Screen **
        //        RefreshScreen();

        //        // ** Show confirmation **
        //        MessageBox.Show(totalParts + " Part(s) across " + SubSetNodeList.Count + " SubSet(s) recalculated successfully");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void RecalculateSubSetRefs(XmlDocument setXML, out int totalParts, out int totalSubSets)
        {
            totalParts = 0;
            totalSubSets = 0;
            try
            {
                // ** Validation checks **
                //if (currentSetXml == null)
                //{
                //    throw new Exception("No Set loaded");
                //}

                // ** Recalculate Unity refs for all SubSets **
                //int totalParts = 0;
                XmlNodeList SubSetNodeList = setXML.SelectNodes("//SubSet");
                foreach (XmlNode SubSetNode in SubSetNodeList)
                {
                    // ** Get variables **
                    string subSetRef = SubSetNode.SelectSingleNode("@Ref").InnerXml;

                    // ** Update XML Node **
                    int index = 1;
                    string xmlString = "//SubSet[@Ref='" + subSetRef + "']//SubModel[@LDrawModelType='MODEL']//Part";
                    XmlNodeList PartsNodeList = setXML.SelectNodes(xmlString);
                    foreach (XmlNode partNode in PartsNodeList)
                    {
                        // ** Update Set Xml **
                        string SubSetRef = subSetRef + "|" + index;
                        partNode.SelectSingleNode("@SubSetRef").InnerXml = SubSetRef;
                        //partNode.SelectSingleNode("@UnityRef").InnerXml = "";

                        // ** Update Index **
                        index += 1;
                    }
                    totalParts += PartsNodeList.Count;
                }
                totalSubSets = SubSetNodeList.Count;

                // ** Refresh Screen **
                //RefreshScreen();

                // ** Show confirmation **
                //MessageBox.Show(totalParts + " Part(s) across " + SubSetNodeList.Count + " SubSet(s) recalculated successfully");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private int GetNextSubSetIndex(XmlDocument xmlDoc, string SubSetRef)
        {
            int SuBSetIndex = -1;
            try
            {
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//Part/@UnityRef";
                string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@LDrawModelType='MODEL']//Part/@SubSetRef";
                XmlNodeList PartNodeList = xmlDoc.SelectNodes(xmlString);
                List<string> partSubSetRefList = PartNodeList.Cast<XmlNode>()
                                                    .Select(x => x.InnerText)
                                                    .OrderBy(x => x).ToList();
                List<int> partSubSetIndexList = PartNodeList.Cast<XmlNode>()
                                                    .Select(x => (int.Parse(x.InnerText.Split('|')[1])))
                                                    .OrderBy(x => x).ToList();
                SuBSetIndex = 1;
                if (partSubSetIndexList.Count > 0)
                {
                    SuBSetIndex = partSubSetIndexList.Max() + 1;
                }

                return SuBSetIndex;
            }
            catch (Exception)
            {
                return SuBSetIndex;
            }
        }

        private void AdjustSubSetStepNumbers(string SubSetRef, string ModelRef)
        {
            try
            {
                // ** Adjust all Pure Step nodes in the SubSet **
                int index = 1;
                XmlNodeList nodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Step");
                foreach (XmlNode node in nodeList)
                {
                    node.SelectSingleNode("@PureStepNo").InnerXml = index.ToString();
                    index += 1;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** PART SUMMARY FUNCTIONS **

        private void Handle_dgPartSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                {
                    Bitmap image = (Bitmap)dgPartSummary.SelectedRows[0].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
                else
                {
                    // ** Get variables **
                    string pureStepNo = fldPureStepNo.Text;
                    string parentSubSetRef = fldStepParentSubSetRef.Text;
                    string parentSubModelRef = fldStepParentModelRef.Text;

                    // ** Get Part Xml node **                
                    string xmlString = "";
                    if (chkShowSubParts.Checked)
                    {
                        xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + (e.RowIndex + 1) + "]";
                    }
                    else
                    {
                        xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + (e.RowIndex + 1) + "]";
                    }
                    XmlNode partNode = fullSetXml.SelectSingleNode(xmlString);
                    if (partNode != null)
                    {
                        // ** Update UI based on whether part is Sub Part or not **
                        //bool IsSubPart = bool.Parse(partNode.SelectSingleNode("@IsSubPart").InnerXml);
                        bool IsSubPart = false;
                        if (partNode.SelectSingleNode("@IsSubPart") != null)
                        {
                            IsSubPart = bool.Parse(partNode.SelectSingleNode("@IsSubPart").InnerXml);
                        }
                        bool IsMiniFig = false;
                        if (partNode.SelectSingleNode("@IsMiniFig") != null) IsMiniFig = bool.Parse(partNode.SelectSingleNode("@IsMiniFig").InnerXml);
                        if (IsSubPart || IsMiniFig)
                        {
                            tsPartDetails.Enabled = false;
                            tsBasePartCollection.Enabled = false;
                            tsPartPosDetails.Enabled = false;
                        }
                        else
                        {
                            tsPartDetails.Enabled = true;
                            tsBasePartCollection.Enabled = true;
                            tsPartPosDetails.Enabled = true;
                        }

                        // ** Post variables **
                        fldLDrawRef.Text = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        fldLDrawRef_Leave(null, null);
                        fldLDrawColourID.Text = partNode.SelectSingleNode("@LDrawColourID").InnerXml;

                        // ** Get Placement Movements **
                        string placementMovementString = "";
                        XmlNodeList pmNodeList = partNode.SelectNodes("PlacementMovement");
                        for (int a = 0; a < pmNodeList.Count; a++)
                        {
                            string Axis = pmNodeList[a].SelectSingleNode("@Axis").InnerXml;
                            string Value = pmNodeList[a].SelectSingleNode("@Value").InnerXml;
                            if (a == 0) placementMovementString += Axis + "=" + Value;
                            else placementMovementString += "," + Axis + "=" + Value;
                        }
                        fldPlacementMovements.Text = placementMovementString;

                        // ** trigger LDraw Colour ID field lookup **
                        ProcessLDrawColourID_Leave();

                        // ** Post Part Positions & Rotations **
                        fldPartPosX.Text = partNode.SelectSingleNode("@PosX").InnerXml;
                        fldPartPosY.Text = partNode.SelectSingleNode("@PosY").InnerXml;
                        fldPartPosZ.Text = partNode.SelectSingleNode("@PosZ").InnerXml;
                        fldPartRotX.Text = partNode.SelectSingleNode("@RotX").InnerXml;
                        fldPartRotY.Text = partNode.SelectSingleNode("@RotY").InnerXml;
                        fldPartRotZ.Text = partNode.SelectSingleNode("@RotZ").InnerXml;

                        // ** Post LDraw Size **
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        //string LDrawSize = Global_Variables.BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawSize").InnerXml;
                        //if (LDrawSize.Equals("0")) LDrawSize = "";
                        //fldLDrawSize.Text = LDrawSize;
                        int LDrawSize = StaticData.GetLDrawSize(LDrawRef);                        
                        if (LDrawSize > 0) fldLDrawSize.Text = LDrawSize.ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }




        private void Handle_fldLDrawImage_Click()
        {
            try
            {
                string LDrawRef = fldLDrawRef.Text;
                if (LDrawRef != "")
                {
                    //Bitmap LDrawImage = GetLDrawImage(LDrawRef);
                    Bitmap LDrawImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                    if (LDrawImage == null)
                    {
                        throw new Exception("LDraw image for " + LDrawRef + " not found in Azure...");
                    }
                    PartViewer.image = LDrawImage;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** COPY TO CLIPBOARD FUNCTIONS **

        private void btnCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopyToClipboard(dgPartListSummary);
        }

        private void CopyToClipboard(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count == 0) throw new Exception("No data populated in " + dg.Name);               
                StringBuilder sb = BaseClasses.HelperFunctions.GenerateClipboardStringFromDataTable(dg);
                Clipboard.SetText(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** MOVE NODE FUNCTIONS **

        private void btnMoveDown_Click(object sender, EventArgs e)
        {
            MoveNode(1);
        }

        private void btnMoveUp_Click(object sender, EventArgs e)
        {
            MoveNode(-1);
        }

        private void btnMoveDownBy5_Click(object sender, EventArgs e)
        {
            //MoveNode(5);
            int BulkValue = 5;
            if (fldBulkValue.Text != "") BulkValue = int.Parse(fldBulkValue.Text);
            MoveNode(BulkValue);
        }

        private void btnMoveUpBy5_Click(object sender, EventArgs e)
        {
            //MoveNode(-5);
            int BulkValue = -5;
            if (fldBulkValue.Text != "") BulkValue = -(int.Parse(fldBulkValue.Text));
            MoveNode(BulkValue);
        }

        private void MoveNode(int directionAdj)
        {
            try
            {
                // ** Get selected node details **               
                string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                //String Ref = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];

                // ** Get node details **
                XmlNode selectedNode = null;
                XmlNodeList parentNodeList = null;
                string parentSubSetRef = "";
                string parentModelRef = "";
                if (Type.Equals("SUBSET"))
                {
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']");
                    parentNodeList = currentSetXml.SelectNodes("//SubSet");
                }
                else if (Type.Equals("MODEL"))
                {
                    string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + ModelRef + "']");
                    parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@SubModelLevel=1]");
                }
                else if (Type.Equals("SUBMODEL"))
                {
                    // ** Get variables **
                    string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string parentSubModelRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//parent::SubModel/@Ref").InnerXml;
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']");
                    parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']/SubModel");
                }
                else if (Type.Equals("STEP"))
                {
                    // ** Get variables **                   
                    parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    string pureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']");
                    parentNodeList = selectedNode.ParentNode.ChildNodes;
                }
                else if (Type.Equals("PART"))
                {
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    string parentPureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
                    string LDrawRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[4];
                    string LDrawColourID = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[5];
                    int nodeIndex = int.Parse(tvSetSummary.SelectedNode.Tag.ToString().Split('|')[6]);
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + parentPureStepNo + "']//Part[" + (nodeIndex + 1) + "]");
                    parentNodeList = selectedNode.ParentNode.ChildNodes;
                }
                else
                {
                    throw new Exception("Cannot move selected node");
                }

                // ** Work out new node index **
                int selectedNodeIndex = 0;
                for (int a = 0; a < parentNodeList.Count; a++)
                {
                    if (parentNodeList[a] == selectedNode)
                    {
                        selectedNodeIndex = a;
                        break;
                    }
                }
                int newNodeIndex = selectedNodeIndex + directionAdj;
                if (newNodeIndex < 0) newNodeIndex = 0;


                // ** Add the nodes back into the parent except the SelectedNode **
                XmlNode parentNode = selectedNode.ParentNode;
                List<XmlNode> nodeList = new List<XmlNode>();
                foreach (XmlNode childNode in parentNode.ChildNodes)
                {
                    if (childNode != selectedNode) nodeList.Add(childNode);
                }
                nodeList.Insert(newNodeIndex, selectedNode);

                // ** Post nodes back into parent **                
                foreach (XmlNode childNode in parentNode.ChildNodes) parentNode.RemoveChild(childNode);
                foreach (XmlNode node in nodeList) parentNode.AppendChild(node);

                // ** Adjust SubSet PureStep Numbers **
                if (Type.Equals("SUBMODEL") || Type.Equals("STEP"))
                {
                    AdjustSubSetStepNumbers(parentSubSetRef, parentModelRef);
                }

                // ** Clear fields **
                ClearAllFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** GET LDRAW DATA FUNCTIONS - DEMISED **

        //public static string GetLDrawFileDetails(string LDrawRef)
        //{
        //      
        //    string value = "";
        //    try
        //    {
        //        //if (Global_Variables.LDrawDATDetails_Dict.ContainsKey(LDrawRef))
        //        //{
        //        //    value = Global_Variables.LDrawDATDetails_Dict[LDrawRef];
        //        //}
        //        //else
        //        //{
        //            ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //            if (share.Exists() == false)
        //            {
        //                share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //                if (share.Exists() == false)
        //                {
        //                    share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
        //                }
        //            }
        //            if (share.Exists())
        //            {

        //                //DateTime lastUpdatedUTC = share.GetProperties().Value.LastModified.UtcDateTime;

        //                byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
        //                Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
        //                using (var ms = new MemoryStream(fileContent))
        //                {
        //                    download.Content.CopyTo(ms);
        //                }
        //                value = Encoding.UTF8.GetString(fileContent);
        //                //Global_Variables.LDrawDATDetails_Dict.Add(LDrawRef, value);
        //            }
        //        //}
        //        return value;
        //    }
        //    catch (Exception)
        //    {
        //        return value;
        //    }
        //}
                
        //public static string GetLDrawDescription_FromLDrawFile(string LDrawRef)
        //{
        //    string value = "";
        //    try
        //    {
        //        string LDrawFileText = GetLDrawFileDetails(LDrawRef);
        //        if(LDrawFileText != "")
        //        {
        //            string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //            value = lines[0].Replace("0 ", "");
        //        }
        //        return value;
        //    }
        //    catch (Exception)
        //    {
        //        return value;
        //    }
        //}

        //public static string GetPartType_FromLDrawFile(string LDrawRef)
        //{
        //    string value = "BASIC";
        //    try
        //    {
        //        // ** Get LDraw details for part **
        //        string LDrawFileText = StaticData.GetLDrawFileDetails(LDrawRef);
        //        string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //        foreach (string fileLine in lines)
        //        {
        //            // ** Check if part contains references to any other parts - if it does then the part is COMPOSITE
        //            if (fileLine.StartsWith("1"))
        //            {
        //                string[] DatLine = fileLine.Split(' ');
        //                string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "").Replace("s\\", "");
        //                string SubPart_LDrawFileText = StaticData.GetLDrawFileDetails(SubPart_LDrawRef);
        //                if (SubPart_LDrawFileText != "")
        //                {
        //                    value = "COMPOSITE";
        //                    break;
        //                }
        //            }
        //        }
        //        return value;
        //    }
        //    catch (Exception)
        //    {
        //        return value;
        //    }
        //}

        //public static string GetLDrawPartType(string LDrawRef)
        //{
        //    string value = BasePart.LDrawPartType.UNKNOWN.ToString();
        //    try
        //    {               
        //        // ** CHECK IF PART EXISTS IN OFFICIAL/UNOFFIAL LDRAW PARTS **            
        //        ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //        if (share.Exists())
        //        {
        //            value = BasePart.LDrawPartType.OFFICIAL.ToString();
        //        }
        //        else
        //        {
        //            share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //            if (share.Exists())
        //            {
        //                value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //            }
        //            else
        //            {
        //                share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
        //                if (share.Exists())
        //                {
        //                    value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //                }
        //            }
        //        }
        //        return value;
        //    }
        //    catch (Exception)
        //    {                
        //        return value;
        //    }
        //}

        #endregion

        #region ** PART FUNCTIONS **

        private void AddPart()
        {
            try
            {
                #region ** VALIDATION CHECKS **               
                if (currentSetXml == null) throw new Exception("No Set active...");                
                if (fldPureStepNo.Text.Equals("")) throw new Exception("No Step selected");               
                if (fldLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");               
                if (fldLDrawColourID.Text.Equals("")) throw new Exception("No LDraw Colour ID entered...");               
                if (fldQty.Text.Equals("")) throw new Exception("No Qty entered...");               
                if (fldPlacementMovements.Text.Equals("")) throw new Exception("No Placement Movement(s) entered...");                
                string LDrawRef = fldLDrawRef.Text;
                int LDrawColourID = int.Parse(fldLDrawColourID.Text);

                // ** CHECK IF PART IS IN BASE PART COLLECTION **
                if(StaticData.CheckIfBasePartExists(LDrawRef) == false) throw new Exception("Part not found in BasePart Collection");
                
                // ** IF PART IS COMPOSITE, CHECK WHETHER SUB PARTS HAVE BEEN SET UP **
                string partType = StaticData.GetPartType(LDrawRef);
                if (partType.Equals("COMPOSITE") && StaticData.CheckIfCompositePartsExist(LDrawRef) == false)
                {                   
                    throw new Exception("SubParts for Part not found in Composite Part Collection");                   
                }
                #endregion

                // ** IDENTIFY THE PARENT STEP **
                string pureStepNo = fldPureStepNo.Text;
                string parentSubSetRef = fldStepParentSubSetRef.Text;
                string parentSubModelRef = fldStepParentModelRef.Text;
                XmlNode parentNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']");

                // ** GET NEXT SUBSET INDEX **                
                int SubSetIndex = GetNextSubSetIndex(currentSetXml, parentSubSetRef);

                // ** ADD NEW PART AND SUB PART(s) IN REQUIRED QTS **
                //int SubSetIndex = NextSubSetIndex;
                int qty = int.Parse(fldQty.Text);
                for (int a = 0; a < qty; a++)
                {
                    #region ** GENERATE NEW PART **
                    Part newPart = new Part();
                    newPart.LDrawRef = LDrawRef;
                    newPart.LDrawColourID = LDrawColourID;
                    newPart.SubSetRef = parentSubSetRef + "|" + SubSetIndex;
                    newPart.UnityRef = "";
                    newPart.state = Part.PartState.NOT_COMPLETED;

                    // ** Add PlacementMovements to Part **
                    List<PlacementMovement> pmList = new List<PlacementMovement>();
                    foreach (string pmString in fldPlacementMovements.Text.Split(','))
                    {
                        PlacementMovement pm = new PlacementMovement();
                        pm.Axis = pmString.Split('=')[0].ToUpper();
                        pm.Value = float.Parse(pmString.Split('=')[1]);
                        pmList.Add(pm);
                    }
                    newPart.placementMovementList = pmList;
                    SubSetIndex += 1;
                    #endregion

                    #region ** ADD SUB PARTS TO NEW PART **
                    CompositePartCollection coll = StaticData.GetCompositePartData_UsingParentLDrawRefList(LDrawRef);
                    foreach(CompositePart compPart in coll.CompositePartList)
                    {
                        // ** Generate subPart and add to SubPartList **
                        Part subPart = new Part();
                        subPart.LDrawRef = compPart.LDrawRef;
                        subPart.LDrawColourID = compPart.LDrawColourID;
                        subPart.SubSetRef = parentSubSetRef + "|" + SubSetIndex;    //TODO: Check whether this data is even used? This is data is used but the value doesn't seem to be populated correctly for Sub Parts.
                        subPart.UnityRef = "";
                        subPart.state = Part.PartState.NOT_COMPLETED;
                        subPart.IsSubPart = true;
                        subPart.PosX = compPart.PosX;
                        subPart.PosY = compPart.PosY;
                        subPart.PosZ = compPart.PosZ;
                        subPart.RotX = compPart.RotX;
                        subPart.RotY = compPart.RotY;
                        subPart.RotZ = compPart.RotZ;
                        newPart.SubPartList.Add(subPart);
                        SubSetIndex += 1;
                    }
                    #endregion

                    #region ** ADD NEW PART XML NODE **    
                    string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newPart.SerializeToString(true));
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlNodeString);
                    XmlNode newNode = doc.DocumentElement;
                    XmlNode importNode = parentNode.OwnerDocument.ImportNode(newNode, true);                    
                    parentNode.AppendChild(importNode);
                    #endregion

                }

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** CLEAR ALL Part Summary FIELDS **
                ClearPartSummaryFields();

                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SavePart()
        {
            try
            {
                #region ** VALIDATION CHECKS **               
                if (currentSetXml == null) throw new Exception("No Set active...");               
                if (fldPureStepNo.Text.Equals("")) throw new Exception("No Step selected");                
                if (fldLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");               
                if (fldLDrawColourID.Text.Equals("")) throw new Exception("No LDraw Colour ID entered...");               
                if (fldPlacementMovements.Text.Equals("")) throw new Exception("No Placement Movement(s) entered...");
                string LDrawRef = fldLDrawRef.Text;
                int LDrawColourID = int.Parse(fldLDrawColourID.Text);

                // ** CHECK IF PART IS IN BASE PART COLLECTION **                
                if (StaticData.CheckIfBasePartExists(LDrawRef) == false) throw new Exception("Part not found in BasePart Collection");
                
                // ** IF PART IS COMPOSITE, CHECK WHETHER SUB PARTS HAVE BEEN SET UP **
                string partType = StaticData.GetPartType(fldLDrawRef.Text);                
                if (partType.Equals("COMPOSITE") && StaticData.CheckIfCompositePartsExist(LDrawRef) == false)
                {
                    throw new Exception("SubParts for Part not found in Composite Part Collection");
                }
                #endregion

                // ** GET VARIABLES **
                string pureStepNo = fldPureStepNo.Text;
                string parentSubSetRef = fldStepParentSubSetRef.Text;
                string parentSubModelRef = fldStepParentModelRef.Text;               
                int nodeIndex = (int)dgPartSummary.SelectedRows[0].Cells["Step Node Index"].Value;
                float posX = 0; if (fldPartPosX.Text != "") posX = float.Parse(fldPartPosX.Text);
                float posY = 0; if (fldPartPosY.Text != "") posY = float.Parse(fldPartPosY.Text);
                float posZ = 0; if (fldPartPosZ.Text != "") posZ = float.Parse(fldPartPosZ.Text);
                float rotX = 0; if (fldPartRotX.Text != "") rotX = float.Parse(fldPartRotX.Text);
                float rotY = 0; if (fldPartRotY.Text != "") rotY = float.Parse(fldPartRotY.Text);
                float rotZ = 0; if (fldPartRotZ.Text != "") rotZ = float.Parse(fldPartRotZ.Text);

                // ** GET OLD PART NODE **                
                string xmlString = "";
                if (chkShowSubParts.Checked)
                {
                    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + nodeIndex + "]";
                }
                else
                {
                    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + nodeIndex + "]";
                }
                XmlNode oldNode = currentSetXml.SelectSingleNode(xmlString);
                //string oldUnityRef = oldNode.SelectSingleNode("@UnityRef").InnerXml;
                string oldSubSetRef = oldNode.SelectSingleNode("@SubSetRef").InnerXml;
                
                #region ** GENERATE NEW PART **
                Part newPart = new Part();
                newPart.LDrawRef = LDrawRef;
                newPart.LDrawColourID = LDrawColourID;
                newPart.UnityRef = "";
                newPart.SubSetRef = oldSubSetRef;
                newPart.state = Part.PartState.NOT_COMPLETED;
                newPart.PosX = posX;
                newPart.PosY = posY;
                newPart.PosZ = posZ;
                newPart.RotX = rotX;
                newPart.RotY = rotY;
                newPart.RotZ = rotZ;

                // ** Add PlacementMovements to Part **
                List<PlacementMovement> pmList = new List<PlacementMovement>();
                foreach (string pmString in fldPlacementMovements.Text.Split(','))
                {
                    PlacementMovement pm = new PlacementMovement();
                    pm.Axis = pmString.Split('=')[0].ToUpper();
                    pm.Value = float.Parse(pmString.Split('=')[1]);
                    pmList.Add(pm);
                }
                newPart.placementMovementList = pmList;
                #endregion

                #region ** ADD SUB PARTS TO NEW PART **
                CompositePartCollection coll = StaticData.GetCompositePartData_UsingParentLDrawRefList(LDrawRef);
                foreach (CompositePart compPart in coll.CompositePartList)
                {
                    // ** Generate subPart and add to SubPartList **
                    Part subPart = new Part();
                    subPart.LDrawRef = compPart.LDrawRef;
                    subPart.LDrawColourID = compPart.LDrawColourID;
                    subPart.UnityRef = "";
                    subPart.state = Part.PartState.NOT_COMPLETED;
                    subPart.IsSubPart = true;
                    subPart.PosX = compPart.PosX;
                    subPart.PosY = compPart.PosY;
                    subPart.PosZ = compPart.PosZ;
                    subPart.RotX = compPart.RotX;
                    subPart.RotY = compPart.RotY;
                    subPart.RotZ = compPart.RotZ;
                    newPart.SubPartList.Add(subPart);                    
                }
                #endregion

                // ** REPLACE PART XML NODE **
                string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newPart.SerializeToString(true));
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(xmlNodeString);
                XmlNode parentNode = oldNode.ParentNode;
                XmlNode importNode = parentNode.OwnerDocument.ImportNode(doc.DocumentElement, true);
                parentNode.ReplaceChild(importNode, oldNode);                
                
                // ** UPDATE PartList **                
                RecalculatePartList(currentSetXml);
                
                // ** CLEAR ALL Part Summary FIELDS **
                ClearPartSummaryFields();
                
                // ** Refresh screen **
                RefreshScreen();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeletePart()
        {
            try
            {
                // ** GET VARIABLES **
                string pureStepNo = fldPureStepNo.Text;
                string parentSubSetRef = fldStepParentSubSetRef.Text;
                string parentSubModelRef = fldStepParentModelRef.Text;                
                int nodeIndex = (int)dgPartSummary.SelectedRows[0].Cells["Step Node Index"].Value;

                #region ** REMOVE DELETED NODE FROM XML **                
                string xmlString = "";
                if (chkShowSubParts.Checked)
                {                    
                    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + nodeIndex + "]";
                }
                else
                {                    
                    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + nodeIndex + "]";
                }
                XmlNode removalNode = currentSetXml.SelectSingleNode(xmlString);
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);
                #endregion

                // ** UPDATE PartList & UnityRefs **                
                RecalculatePartList(currentSetXml);
                
                // ** CLEAR ALL Part Summary FIELDS **
                ClearPartSummaryFields();

                // ** Refresh screen **
                RefreshScreen();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearPart()
        {
            ClearPartSummaryFields();
        }

        private void ClearPartSummaryFields()
        {
            try
            {
                lblPartCount.Text = "";
                fldLDrawRef.Text = "";
                fldLDrawImage.Image = null;
                //lblLDrawDescription.Text = "";
                fldLDrawColourName.Text = "";
                fldLDrawColourID.Text = "";
                chkBasePartCollection.Checked = false;
                fldPartType.Text = "BASIC";
                chkIsSubPart.Checked = false;
                chkIsSticker.Checked = false;
                chkIsLargeModel.Checked = false;
                fldQty.Text = "1";
                fldPlacementMovements.Text = "Y=-5";
                fldPartPosX.Text = "";
                fldPartPosY.Text = "";
                fldPartPosZ.Text = "";
                fldPartRotX.Text = "";
                fldPartRotY.Text = "";
                fldPartRotZ.Text = "";
                fldLDrawSize.Text = "";
                gbPartDetails.Text = "Part Details";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private void AddToPartDAT()
        //{
        //    try
        //    {
        //        // ** Validation checks **
        //        if (fldLDrawRef.Text.Equals(""))
        //        {
        //            throw new Exception("No LDraw Ref entered...");
        //        }

        //        // ** Update part.dat file **
        //        string line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + fldLDrawRef.Text + ".dat" + Environment.NewLine;                
        //        byte[] bytes = Encoding.UTF8.GetBytes(line);
        //        ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient("static-data").GetFileClient("part.dat");
        //        share.Create(bytes.Length);
        //        using (MemoryStream ms = new MemoryStream(bytes))
        //        {
        //            share.Upload(ms);                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void AddPartToBasePartCollection()
        {            
            try
            {
                #region ** VALIDATION **
                if (fldLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");                               
                if (fldPartType.Text.Equals("")) throw new Exception("No Part Type selected...");                
                if (fldLDrawRef.Text.Contains("c") && fldPartType.Text.Equals("BASIC"))
                {
                    // ** Make sure user wants to create strange part **
                    DialogResult res = MessageBox.Show("The part contains 'c' - do you really want to create a BASIC part and not a COMPOSITE one?", "BASIC Part Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No) return;                    
                }
                string LDrawRef = fldLDrawRef.Text;
                BasePart.PartType partType = (BasePart.PartType)Enum.Parse(typeof(BasePart.PartType), fldPartType.Text, true);
                int LDrawSize = 0;
                if (fldLDrawSize.Text != "") LDrawSize = int.Parse(fldLDrawSize.Text);

                // ** Check if LDrawDetails are available **
                LDrawDetails lDrawDetails = null;
                LDrawDetailsCollection ldd_coll = StaticData.GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
                if (ldd_coll.LDrawDetailsList.Count > 0) lDrawDetails = ldd_coll.LDrawDetailsList[0];
                if (lDrawDetails == null) throw new Exception("Unable to find LDraw details for " + LDrawRef);
                BasePart.LDrawPartType lDrawPartType = (BasePart.LDrawPartType)Enum.Parse(typeof(BasePart.LDrawPartType), lDrawDetails.LDrawPartType, true);
                
                // ** Check if LDraw Ref already exists in BasePart **
                if (StaticData.CheckIfBasePartExists(LDrawRef) == true) throw new Exception("LDraw Ref already exists...");

                // ** Check if CHECK IF PART EXISTS IN OFFICIAL/UNOFFIAL LDRAW PARTS ** 
                //if (StaticData.CheckIfLDrawFileDetailsExist(LDrawRef) == false) throw new Exception("Unable to find " + LDrawRef + " in official or unofficial LDraw Parts data...");
                
                // ** Check if LDraw Refs already exist in CompositePartCollection XML **               
                if (partType == BasePart.PartType.COMPOSITE && StaticData.CheckIfCompositePartsExist(LDrawRef) == true) throw new Exception("Parent LDraw Ref already exists...");
                #endregion

                #region ** GENERATE NEW BasePart & ADD TO STATIC DATA **                
                BasePart newBasePart = new BasePart()
                {
                    LDrawRef = LDrawRef,
                    LDrawDescription = new System.Xml.Linq.XText(lDrawDetails.LDrawDescription).ToString(),                   
                    lDrawPartType = lDrawPartType,
                    LDrawCategory = "",
                    partType = partType,
                    IsSubPart = chkIsSubPart.Checked,
                    IsSticker = chkIsSticker.Checked,
                    IsLargeModel = chkIsLargeModel.Checked,
                    SubPartCount = lDrawDetails.SubPartCount
                };
                if (newBasePart.partType == BasePart.PartType.BASIC) newBasePart.OffsetX = -1;               
                newBasePart.LDrawSize = LDrawSize;                
                StaticData.AddBasePart(newBasePart);
                #endregion

                #region ** ADD ALL SUB PARTS FROM LDRAW .DAT FILE (IF PART = COMPOSITE) **
                if (newBasePart.partType == BasePart.PartType.COMPOSITE)
                {   
                    CompositePartCollection SubPartCollection = StaticData.GetAllCompositeSubParts_FromLDrawDetails(LDrawRef);                    
                    foreach (CompositePart cp in SubPartCollection.CompositePartList)
                    {
                        cp.ParentLDrawRef = LDrawRef;
                        cp.PosX = -1;
                        StaticData.AddCompositePart(cp);
                    }
                }
                #endregion

                // ** Refresh Part Details ** 
                ProcessLDrawRef_Leave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private List<string> GetAllSubPartsForLDrawRef(string LDrawRef, int LDrawColourID)
        //{
        //    List<string> SubPartList = new List<string>();
        //    try
        //    {
        //        string ParentLDrawFileText = StaticData.GetLDrawFileDetails(LDrawRef);
        //        string[] lines = ParentLDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //        foreach (string fileLine in lines)
        //        {
        //            if (fileLine.StartsWith("1"))
        //            {
        //                string[] DatLine = fileLine.Split(' ');
        //                string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "");
        //                int SubPart_LDrawColourID = int.Parse(DatLine[1]);
        //                //if (SubPart_LDrawRef.Contains("c0"))
        //                //{
        //                //    // LINE HAS REFERENCE TO ANOTHER PART WITH HAS SUB PARTS **
        //                //    List<string> SubPartList2 = GetAllSubPartsForLDrawRef(SubPart_LDrawRef, SubPart_LDrawColourID);
        //                //    SubPartList.AddRange(SubPartList2);
        //                //}
        //                //else
        //                //{
        //                    // LINE ONLY HAS REFERENCE TO ANOTHER SINGLE PART                            
        //                    if (SubPart_LDrawColourID == 16)
        //                    {
        //                        //SubPart_LDrawColourID = -1;
        //                        SubPart_LDrawColourID = LDrawColourID;
        //                    }
        //                    SubPartList.Add(SubPart_LDrawRef + "|" + SubPart_LDrawColourID);
        //                //}
        //            }
        //        }
        //        return SubPartList;
        //    }
        //    catch (Exception)
        //    {
        //        return new List<string>();
        //    }
        //}

        //private void SubModelImportPartPosRot_OLD1()
        //{
        //    try
        //    {
        //        #region ** VALIDATION **
        //        if (tvSetSummary.SelectedNode == null)
        //        {
        //            throw new Exception("No Model or SubModel node selected...");
        //        }
        //        string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
        //        if (Type != "MODEL" & Type != "SUBMODEL")
        //        {
        //            throw new Exception("Can only import data on Model or SubModel nodes");
        //        }
        //        #endregion

        //        // ** Get current Model or SubModel **              
        //        string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //        string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //        string SetRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/ancestor::Set/@Ref").InnerXml;

        //        // ** GET SET PARTS FOR SUB MODEL **                         
        //        string SetPartXMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/*/Part";
        //        XmlNodeList setPartNodeList = currentSetXml.SelectNodes(SetPartXMLString);

        //        #region ** GET SUBMODEL PARTS **
        //        BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "submodel-xmls").GetBlobClient(SubSetRef + "." + SubModelRef + ".xml");
        //        if (blob.Exists() == false)
        //        {
        //            throw new Exception("SubModel XML file " + SubSetRef + "." + SubModelRef + ".xml not found...");
        //        }
        //        byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
        //        using (var ms = new MemoryStream(fileContent))
        //        {
        //            blob.DownloadTo(ms);
        //        }
        //        string xmlString = Encoding.UTF8.GetString(fileContent);
        //        XmlDocument SubModelXMLDoc = new XmlDocument();
        //        SubModelXMLDoc.LoadXml(xmlString);
        //        //string SubModelPartXMLString = "//Part";
        //        string SubModelPartXMLString = "//Part[@IsSubPart='false']";
        //        XmlNodeList subModelPartNodeList = SubModelXMLDoc.SelectNodes(SubModelPartXMLString);
        //        #endregion

        //        #region ** CHECK IF PARTS ARE IN CORRECT ORDER AND MATCH **               
        //        bool match = true;
        //        for (int a = 0; a < subModelPartNodeList.Count; a++)
        //        {
        //            string Set_LDrawRef = subModelPartNodeList[a].SelectSingleNode("@LDrawRef").InnerXml; //debug_LDrawRef = Set_LDrawRef;
        //            string Set_LDrawColourID = subModelPartNodeList[a].SelectSingleNode("@LDrawColourID").InnerXml;
        //            string SubModel_LDrawRef = setPartNodeList[a].SelectSingleNode("@LDrawRef").InnerXml;
        //            string SubModel_LDrawColourID = setPartNodeList[a].SelectSingleNode("@LDrawColourID").InnerXml;
        //            if (Set_LDrawRef != SubModel_LDrawRef)
        //            {
        //                match = false;
        //            }
        //            if (Set_LDrawColourID != SubModel_LDrawColourID)
        //            {
        //                match = false;
        //            }
        //        }
        //        if (match == false)
        //        {
        //            // ** GET SOURCE TABLE AND REARRANGE COLUMNS **
        //            DataTable sourceTable = GenerateStepPartTable(setPartNodeList);
        //            string[] columnNames = new string[] { "Step No", "Part Image", "LDraw Ref", "LDraw Colour ID", "LDraw Colour Name", "Colour Image", "Unity FBX", "Base Part Collection", "Part Type", "Is SubPart", "Step Node Index", "Placement Movements", "SuBSet Ref", "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ" };
        //            var colIndex = 0;
        //            foreach (var colName in columnNames)
        //            {
        //                sourceTable.Columns[colName].SetOrdinal(colIndex++);
        //            }

        //            // ** SHOW MATCHING SCREEN **
        //            MatchingScreen form = new MatchingScreen();
        //            form.sourceTable = sourceTable;
        //            form.targetTable = GenerateStepPartTable(subModelPartNodeList);
        //            form.Refresh_Screen();
        //            form.Visible = true;
        //        }
        //        else
        //        {
        //            // ** UPDATE SET XML **                
        //            for (int a = 0; a < subModelPartNodeList.Count; a++)
        //            {
        //                string Set_LDrawRef = subModelPartNodeList[a].SelectSingleNode("@LDrawRef").InnerXml;
        //                string Set_LDrawColourID = subModelPartNodeList[a].SelectSingleNode("@LDrawColourID").InnerXml;
        //                string PosX = subModelPartNodeList[a].SelectSingleNode("@PosX").InnerXml;
        //                string PosY = subModelPartNodeList[a].SelectSingleNode("@PosY").InnerXml;
        //                string PosZ = subModelPartNodeList[a].SelectSingleNode("@PosZ").InnerXml;
        //                string RotX = subModelPartNodeList[a].SelectSingleNode("@RotX").InnerXml;
        //                string RotY = subModelPartNodeList[a].SelectSingleNode("@RotY").InnerXml;
        //                string RotZ = subModelPartNodeList[a].SelectSingleNode("@RotZ").InnerXml;
        //                setPartNodeList[a].SelectSingleNode("@PosX").InnerXml = PosX;
        //                setPartNodeList[a].SelectSingleNode("@PosY").InnerXml = PosY;
        //                setPartNodeList[a].SelectSingleNode("@PosZ").InnerXml = PosZ;
        //                setPartNodeList[a].SelectSingleNode("@RotX").InnerXml = RotX;
        //                setPartNodeList[a].SelectSingleNode("@RotY").InnerXml = RotY;
        //                setPartNodeList[a].SelectSingleNode("@RotZ").InnerXml = RotZ;
        //            }

        //            // ** Refresh screen **
        //            RefreshScreen();

        //            // ** SHOW CONFIRMATION **
        //            MessageBox.Show("Successfully updated " + subModelPartNodeList.Count + " Part(s)...");
        //        }
        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void SubModelImportPartPosRot_OLD()
        //{
        //    
        //    try
        //    {
        //        #region ** VALIDATION **
        //        if (tvSetSummary.SelectedNode == null)
        //        {
        //            throw new Exception("No Model or SubModel node selected...");
        //        }
        //        string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
        //        if (Type != "MODEL" & Type != "SUBMODEL")
        //        {
        //            throw new Exception("Can only import data on Model or SubModel nodes");
        //        }
        //        #endregion

        //        // ** Get current Model or SubModel **              
        //        string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //        string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //        string SetRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/ancestor::Set/@Ref").InnerXml;

        //        // ** GET SET PARTS FOR SUB MODEL **                         
        //        string SetPartXMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/*/Part";
        //        //string SetPartXMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Part";
        //        XmlNodeList setPartNodeList = currentSetXml.SelectNodes(SetPartXMLString);

        //        #region ** GET SUBMODEL PARTS **
        //        BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "submodel-xmls").GetBlobClient(SubSetRef + "." + SubModelRef + ".xml");
        //        if (blob.Exists() == false)
        //        {
        //            throw new Exception("SubModel XML file " + SubSetRef + "." + SubModelRef + ".xml not found...");
        //        }
        //        byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
        //        using (var ms = new MemoryStream(fileContent))
        //        {
        //            blob.DownloadTo(ms);
        //        }
        //        string xmlString = Encoding.UTF8.GetString(fileContent);
        //        XmlDocument SubModelXMLDoc = new XmlDocument();
        //        SubModelXMLDoc.LoadXml(xmlString);
        //        string SubModelPartXMLString = "//Part";
        //        //string SubModelPartXMLString = "//Part[@IsSubPart='false']";
        //        XmlNodeList subModelPartNodeList = SubModelXMLDoc.SelectNodes(SubModelPartXMLString);
        //        #endregion

        //        #region ** GENERATE SOURCE AND TARGET TABLES **
        //        DataTable sourceTable = GenerateStepPartTable(setPartNodeList);
        //        sourceTable.Columns.Add("Matched", typeof(string));
        //        DataTable targetTable = GenerateStepPartTable(subModelPartNodeList);
        //        targetTable.Columns.Add("Matched", typeof(string));
        //        for (int a = 0; a < sourceTable.Rows.Count; a++) sourceTable.Rows[a]["Matched"] = "False";
        //        for (int a = 0; a < targetTable.Rows.Count; a++) targetTable.Rows[a]["Matched"] = "False";
        //        #endregion

        //        #region ** RUN MATCHING PROCESS **
        //        bool overallMatch = true;
        //        for (int a = 0; a < sourceTable.Rows.Count; a++)
        //        {
        //            // ** GET VARIABLES **
        //            string Set_LDrawRef = sourceTable.Rows[a]["LDraw Ref"].ToString();
        //            string Set_LDrawColourID = sourceTable.Rows[a]["LDraw Colour ID"].ToString();
        //            string SubModel_LDrawRef = "";
        //            string SubModel_LDrawColourID = "";
        //            if (a < targetTable.Rows.Count)
        //            {
        //                SubModel_LDrawRef = targetTable.Rows[a]["LDraw Ref"].ToString();
        //                SubModel_LDrawColourID = targetTable.Rows[a]["LDraw Colour ID"].ToString();
        //            }
        //            bool match = true;
        //            if (Set_LDrawRef != SubModel_LDrawRef)
        //            {
        //                match = false;                        
        //            }
        //            if (Set_LDrawColourID != SubModel_LDrawColourID)
        //            {
        //                match = false;                        
        //            }
        //            sourceTable.Rows[a]["Matched"] = match;
        //            if (a < targetTable.Rows.Count)
        //            {
        //                targetTable.Rows[a]["Matched"] = match;
        //            }
        //        }
        //        int SourceUnmatchedCount =  (from r in sourceTable.AsEnumerable()
        //                                     where r.Field<string>("Matched").Equals("False")
        //                                     select r).Count();
        //        int TargetUnmatchedCount =  (from r in targetTable.AsEnumerable()
        //                                     where r.Field<string>("Matched").Equals("False")
        //                                     select r).Count();
        //        if (SourceUnmatchedCount > 0 || TargetUnmatchedCount > 0)
        //        {
        //            overallMatch = false;
        //        }
        //        #endregion

        //        #region ** IF OVERALL MATCH = FALSE, SHOW Matching Screen **
        //        if (overallMatch == false)
        //        {
        //            // ** REARRANGE SOURCE TABLE COLUMNS **
        //            string[] columnNames = new string[] { "Step No", "Part Image", "LDraw Ref", "LDraw Colour ID", "LDraw Colour Name", "Colour Image", "Unity FBX", "Base Part Collection", "Part Type", "Is SubPart", "Step Node Index", "Placement Movements", "SuBSet Ref", "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ" };
        //            var colIndex = 0;
        //            foreach (var colName in columnNames)
        //            {
        //                sourceTable.Columns[colName].SetOrdinal(colIndex++);
        //            }

        //            // ** SHOW MATCHING SCREEN **
        //            MatchingScreen form = new MatchingScreen();
        //            form.sourceTable = sourceTable;
        //            form.targetTable = targetTable;
        //            form.Refresh_Screen();
        //            form.Visible = true;
        //            return;
        //        }
        //        #endregion

        //        #region ** UPDATE SET XML (IF OVERALL MATCH = TRUE) **
        //        int PartCount = 0;
        //        int SubPartCount = 0;
        //        for (int a = 0; a < subModelPartNodeList.Count; a++)
        //        {
        //            string Set_LDrawRef = subModelPartNodeList[a].SelectSingleNode("@LDrawRef").InnerXml;
        //            string Set_LDrawColourID = subModelPartNodeList[a].SelectSingleNode("@LDrawColourID").InnerXml;
        //            bool IsSubPart = bool.Parse(subModelPartNodeList[a].SelectSingleNode("@IsSubPart").InnerXml);
        //            if (IsSubPart == false)
        //            {
        //                PartCount += 1;
        //            }
        //            else
        //            {
        //                SubPartCount += 1;
        //            }
        //            string PosX = subModelPartNodeList[a].SelectSingleNode("@PosX").InnerXml;
        //            string PosY = subModelPartNodeList[a].SelectSingleNode("@PosY").InnerXml;
        //            string PosZ = subModelPartNodeList[a].SelectSingleNode("@PosZ").InnerXml;
        //            string RotX = subModelPartNodeList[a].SelectSingleNode("@RotX").InnerXml;
        //            string RotY = subModelPartNodeList[a].SelectSingleNode("@RotY").InnerXml;
        //            string RotZ = subModelPartNodeList[a].SelectSingleNode("@RotZ").InnerXml;
        //            setPartNodeList[a].SelectSingleNode("@PosX").InnerXml = PosX;
        //            setPartNodeList[a].SelectSingleNode("@PosY").InnerXml = PosY;
        //            setPartNodeList[a].SelectSingleNode("@PosZ").InnerXml = PosZ;
        //            setPartNodeList[a].SelectSingleNode("@RotX").InnerXml = RotX;
        //            setPartNodeList[a].SelectSingleNode("@RotY").InnerXml = RotY;
        //            setPartNodeList[a].SelectSingleNode("@RotZ").InnerXml = RotZ;
        //        }

        //        // ** Refresh screen **
        //        RefreshScreen();

        //        // ** SHOW CONFIRMATION **
        //        MessageBox.Show("Successfully updated " + PartCount + " Part(s) and " + SubPartCount + " Sub Part(s)...");

        //        #endregion

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void GenerateDATFile()
        {
            try
            {
                #region ** VALIDATION **                
                if (fldLDrawRef.Text.Equals(""))
                {
                    throw new Exception("No LDraw Ref entered...");
                }
                if (fldPartType.Text.Equals(""))
                {
                    throw new Exception("No Part Type selected...");
                }
                string LDrawRef = fldLDrawRef.Text;
                BasePart.PartType partType = (BasePart.PartType)Enum.Parse(typeof(BasePart.PartType), fldPartType.Text, true);
                #endregion

                #region ** GENERATE COMPOSITE PART DETAILS **
                List<string> SubPartList = new List<string>();
                if (partType == BasePart.PartType.BASIC)
                {
                    // ** BASIC **

                    // ** GENERATE MAIN PART **
                    string line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + LDrawRef + ".dat" + Environment.NewLine;
                    byte[] bytes = Encoding.UTF8.GetBytes(line);
                    ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
                    share.Create(bytes.Length);
                    using (MemoryStream ms = new MemoryStream(bytes))
                    {
                        share.Upload(ms);
                    }  
                }
                else if (partType == BasePart.PartType.COMPOSITE)
                {
                    // ** COMPOSITE **

                    #region ** GENERATE PARENT COMPOSITE PART .DAT FILES (JUST CONTAINS DAT'S OF SUB PARTS) **
                    //string LDrawFileText = StaticData.GetLDrawFileDetails(LDrawRef);
                    //string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    //string DAT_String = "";
                    //foreach (string fileLine in lines)
                    //{
                    //    if (fileLine.StartsWith("1"))
                    //    {
                    //        DAT_String += fileLine.Replace("1 16 ", "1 450 ") + Environment.NewLine;
                    //    }
                    //}
                    //byte[] bytes = Encoding.UTF8.GetBytes(DAT_String);
                    //ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
                    //share.Create(bytes.Length);
                    //using (var ms = new MemoryStream(bytes))
                    //{
                    //    share.Upload(ms);
                    //}
                    #endregion

                    #region ** GENERATE ALL SUB PART .DAT FILES **                    
                    ////SubPartList = GetAllSubPartsForLDrawRef(LDrawRef, -1);                    
                    //SubPartList = StaticData.GetAllSubPartLDrawRefs_FromLDrawFile(LDrawRef);
                    //foreach (string SubPart in SubPartList)
                    //{
                    //    string SubPart_LDrawRef = SubPart.Split('|')[0];
                    //    string line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + SubPart_LDrawRef + ".dat" + Environment.NewLine;
                    //    bytes = Encoding.UTF8.GetBytes(line);
                    //    share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + SubPart_LDrawRef + ".dat");
                    //    share.Create(bytes.Length);
                    //    using (MemoryStream ms = new MemoryStream(bytes))
                    //    {
                    //        share.Upload(ms);
                    //    }
                    //}
                    #endregion

                }
                #endregion

                // ** SHOW CONFIRMATION **
                string confirmation = "Created the following .DAT file(s):" + Environment.NewLine;
                confirmation += LDrawRef + Environment.NewLine;
                foreach (string subPart_LDrawRef in SubPartList)
                {
                    confirmation += subPart_LDrawRef.Split('|')[0] + Environment.NewLine;
                }
                MessageBox.Show(confirmation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** SYNC FBX FUNCTIONS **

        //private void SyncFBXFiles_OLD()
        //{
        //    try
        //    {
        //        #region ** OLD CODE **
        //        // ** Get all FBX files in Unity Resources directory **
        //        //string unityPath = @"C:\Unity Projects\Lego Unity Viewer\Assets\Resources\Lego Part Models";
        //        //List<string> FileListFull_Unity = Directory.GetFiles(unityPath).Where(s => Path.GetExtension(s).Equals(".fbx")).ToList();
        //        //List<string> FileList_Unity = Directory.GetFiles(unityPath)
        //        //                                .Where(s => Path.GetExtension(s).Equals(".fbx"))
        //        //                                .Select(Path.GetFileName).ToList();
        //        //int index = 0;
        //        //Dictionary<string, string> FileList_Map_Unity = new Dictionary<string, string>();
        //        //foreach (string filename in FileList_Unity)
        //        //{
        //        //    string fullPath = FileListFull_Unity.ToArray()[index];
        //        //    FileList_Map_Unity.Add(filename, fullPath);
        //        //    index += 1;
        //        //}

        //        // ** Get all FBX files in files-fbx location on Share **
        //        //string fsPath = @"Z:\static-data\files-fbx";                
        //        //List<string> FileListFull_FS = Directory.GetFiles(fsPath).ToList();
        //        //List<string> FileList_FS = Directory.GetFiles(fsPath).Select(Path.GetFileName).ToList();
        //        //List<string> FileListFull_FS = Directory.GetFiles(fsPath).Where(s => Path.GetExtension(s).Equals(".fbx")).ToList();
        //        //List<string> FileList_FS = Directory.GetFiles(fsPath)
        //        //                                .Where(s => Path.GetExtension(s).Equals(".fbx"))
        //        //                                .Select(Path.GetFileName).ToList();
        //        //index = 0;
        //        //Dictionary<string, string> FileList_Map_FS = new Dictionary<string, string>();
        //        //foreach (string filename in FileList_FS)
        //        //{
        //        //    string fullPath = FileListFull_FS.ToArray()[index];
        //        //    FileList_Map_FS.Add(filename, fullPath);
        //        //    index += 1;
        //        //}
        //        #endregion

        //        // ** GET ALL FBX FILES IN "static-data\files-fbx" ON AZURE SHARE **
        //        //string fsPath = @"Z:\static-data\files-fbx";
        //        //List<string> FileList_FS = Directory.GetFiles(fsPath)
        //        //                                .Where(s => Path.GetExtension(s).Equals(".fbx"))
        //        //                                .Select(Path.GetFileName).ToList();
        //        List<Azure.Storage.Files.Shares.Models.ShareFileItem> FSFileList = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFilesAndDirectories().ToList();
        //        List<string> FileList_FS = FSFileList.Select(x => x.Name).ToList();

        //        // ** GET ALL FBX FILES IN UNITY "Resources\Lego Part Models" DIRECTORY **
        //        string unityPath = @"C:\Unity Projects\Lego Unity Viewer\Assets\Resources\Lego Part Models";                
        //        List<string> FileList_Unity = Directory.GetFiles(unityPath)
        //                                        .Where(s => Path.GetExtension(s).Equals(".fbx"))
        //                                        .Select(Path.GetFileName).ToList();

        //        // ** COPY NEW FBX TO UNITY LOCATION **
        //        List<string> difList = FileList_FS.Except(FileList_Unity).ToList();
        //        if (difList.Count == 0)
        //        {
        //            throw new Exception("No FBX files need uploading...");
        //        }
        //        foreach (string filename in difList)
        //        {
        //            string source = @"Z:\static-data\files-fbx\" + filename;
        //            string target = @"C:\Unity Projects\Lego Unity Viewer\Assets\Resources\Lego Part Models\" + filename;
        //            File.Copy(source, target);
        //        }
        //        MessageBox.Show(difList.Count + " FBX file(s) uploaded to Azure FS...");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        public static void SyncFBXFiles()
        {            
            try
            {
                // ** GET ALL FBX FILES IN "static-data\files-fbx" ON AZURE SHARE **                
                List<Azure.Storage.Files.Shares.Models.ShareFileItem> FSFileList = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFilesAndDirectories().ToList();
                List<string> FileList_FS = FSFileList.Select(x => x.Name).ToList();

                // ** COPY FILES ACROSS THAT ARE NEW OR NEWER **
                List<string> updatedFileList = new List<string>();
                foreach (string filename in FileList_FS)
                {
                    ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(filename);
                    DateTime lastModified_TS = share.GetProperties().Value.LastModified.UtcDateTime;
                    DateTime lastModified_Unity;
                    bool CopyFile = false;
                    if (File.Exists(Path.Combine(Global_Variables.UnityLegoPartPath, filename)) == false)
                    {
                        CopyFile = true;
                    }
                    else
                    {
                        lastModified_Unity = new FileInfo(Path.Combine(Global_Variables.UnityLegoPartPath, filename)).LastWriteTimeUtc;
                        if (lastModified_Unity < lastModified_TS)
                        {
                            CopyFile = true;
                        }
                    }
                    if (CopyFile)
                    {
                        // ** Download file from Azure and save into Unity Resources\Lego Part Model directory **                        
                        string target = Path.Combine(Global_Variables.UnityLegoPartPath, filename);
                        byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
                        Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
                        using (var fs = new FileStream(target, FileMode.Create, FileAccess.Write))
                        {
                            download.Content.CopyTo(fs);
                        }
                        File.SetLastWriteTimeUtc(target, lastModified_TS);
                        updatedFileList.Add(filename);
                    }
                }

                // ** SHOW CONFIRMATION **
                string confirmation = updatedFileList.Count + " file(s) added/updated in Unity Resource directory" + Environment.NewLine;
                foreach (string filename in updatedFileList)
                {
                    confirmation += filename + Environment.NewLine;
                }
                MessageBox.Show(confirmation, "Syncing FBX file(s)...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** PART SUMMARY ACCELERATOR FUNCTIONS **

        private void fldLDrawRefAc_TextChanged(object sender, EventArgs e)
        {
            ProcessPartSummaryFilter();
        }

        private void fldLDrawColourNameAc_TextChanged(object sender, EventArgs e)
        {
            ProcessPartSummaryFilter();
        }

        private void chkLDrawRefAcEquals_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPartSummaryFilter();
        }

        private void chkLDrawColourNameAcEquals_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPartSummaryFilter();
        }

        private void chkFBXMissingAc_CheckedChanged(object sender, EventArgs e)
        {
            ProcessPartSummaryFilter();
        }

        private void ProcessPartSummaryFilter()
        {
            try
            {
                if (dgPartSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblPartSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgPartSummary, dgPartSummaryTable_Orig);
                    AdjustPartSummaryRowFormatting(dgPartSummary);

                    // ** Determine what filters have been applied **
                    if (fldLDrawRefAc.Text != "" || fldLDrawColourNameAc.Text != "" || chkFBXMissingAc.Checked == true)
                    {
                        List<DataRow> filteredRows = dgPartSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkLDrawRefAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Equals(fldLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Contains(fldLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for LDraw Colour Name **
                        if (filteredRows.Count > 0)
                        {
                            if (chkLDrawColourNameAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Colour Name").ToUpper().Equals(fldLDrawColourNameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Colour Name").ToUpper().Contains(fldLDrawColourNameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for FBX **
                        if (chkFBXMissingAc.Checked)
                        {
                            filteredRows = filteredRows.CopyToDataTable().AsEnumerable().Where(row => row.Field<bool>("Unity FBX") == false).ToList();
                        }
                        #endregion

                        #region ** Apply filters **
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgPartSummary, filteredRows.CopyToDataTable());
                            AdjustPartSummaryRowFormatting(dgPartSummary);
                        }
                        else
                        {
                            Delegates.DataGridView_SetDataSource(this, dgPartSummary, null);
                        }
                        lblPartSummaryItemFilteredCount.Text = filteredRows.Count + " filtered part(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        

        #region ** CONVERT TO LDR FUNCTIONS  **

        private void ConvertSetXMLToLDR()
        {
            try
            {
                // ** LOAD Set XML into Object - OLD **      
                ////String setRef = "42078-1";
                //string setRef = "TEST-1";
                //string setfileLocation = Global_Variables.SetSaveLocation + "\\" + setRef + ".xml";
                //if (File.Exists(setfileLocation) == false)
                //{
                //    throw new Exception("Set not found...");
                //}    
                //currentSetXml = new XmlDocument();
                //currentSetXml.Load(setfileLocation);

                // ** LOAD Set XML into Object - NEW **
                string setRef = "TEST-1";
                //CloudBlockBlob blob = blobClient.GetContainerReference("set-xmls").GetBlockBlobReference(setRef + ".xml");
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
                if (blob.Exists() == false)
                {
                    throw new Exception("Set not found...");
                }
                //string xmlString = Encoding.UTF8.GetString(HelperFunctions.DownloadAzureFile(blob));
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(xmlString);

                // get all part nodes
                String LDRString = "";
                XmlNodeList allPartNodes = currentSetXml.SelectNodes("//SubSetList//Part[@IsSubPart='false']");
                foreach (XmlNode partNode in allPartNodes)
                {
                    // ** Get variables **
                    String LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    String LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
                    //String posX = partNode.SelectSingleNode("@PosX").InnerXml;
                    //String posY = partNode.SelectSingleNode("@PosY").InnerXml;
                    //String posZ = partNode.SelectSingleNode("@PosZ").InnerXml;
                    double posX = double.Parse(partNode.SelectSingleNode("@PosX").InnerXml);
                    double posY = double.Parse(partNode.SelectSingleNode("@PosY").InnerXml);
                    double posZ = double.Parse(partNode.SelectSingleNode("@PosZ").InnerXml);
                    String rotX = partNode.SelectSingleNode("@RotX").InnerXml;
                    String rotY = partNode.SelectSingleNode("@RotY").InnerXml;
                    String rotZ = partNode.SelectSingleNode("@RotZ").InnerXml;

                    //  1 0 0 -24 0 1 0 0 0 1 0 0 0 1 62810.dat

                    LDRString += "1" + " ";
                    LDRString += LDrawColourID + " ";

                    // ** x, y, z **                    
                    double ldraw_posX = posZ / -0.04;
                    double ldraw_posY = posY / -0.04;
                    double ldraw_posZ = posX / -0.04;
                    LDRString += ldraw_posX + " ";
                    LDRString += ldraw_posY + " ";
                    LDRString += ldraw_posZ + " ";
                    //LDRString += "0 0" + " ";



                    // ** Rotation **
                    LDRString += "1 0 0 0 1 0 0 0 1" + " ";
                    LDRString += LDrawRef + ".dat" + Environment.NewLine;
                }

                // ** Save Set to location **                
                //string path = @"C:\LEGO STUFF - Documents\test.ldr";
                //File.WriteAllText(path, LDRString);


                // ** SHow confirmation **
                //MessageBox.Show("Set converted successfully...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        
        #endregion

        //private void ConvertSetXML()    // #### NO LONGER USED ####
        //{
        //    try
        //    {
        //        // Load Set xml
        //        String setRef = "42078-1";
        //        //String setRef = "TEST";
        //        String setfileLocation = Global_Variables.SetSaveLocation + "\\" + setRef + ".xml";
        //        if (File.Exists(setfileLocation) == false)
        //        {
        //            throw new Exception("Set not found...");
        //        }

        //        // ** LOAD Set XML into Object **              
        //        currentSetXml = new XmlDocument();
        //        currentSetXml.Load(setfileLocation);

        //        // get all part nodes
        //        XmlNodeList allPartNodes = currentSetXml.SelectNodes("//SubSetList//Part[@IsSubPart='false']");
        //        foreach (XmlNode partNode in allPartNodes)
        //        {
        //            // ** Get variables **
        //            String LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
        //            String LDrawColourID = partNode.SelectSingleNode("@LDrawColourID").InnerXml;
        //            String posX = partNode.SelectSingleNode("@PosX").InnerXml;
        //            String posY = partNode.SelectSingleNode("@PosY").InnerXml;
        //            String posZ = partNode.SelectSingleNode("@PosZ").InnerXml;
        //            String rotX = partNode.SelectSingleNode("@RotX").InnerXml;
        //            String rotY = partNode.SelectSingleNode("@RotY").InnerXml;
        //            String rotZ = partNode.SelectSingleNode("@RotZ").InnerXml;

        //            // ** GENERATE AMENDED PART **
        //            Part newPart = new Part();
        //            newPart.LDrawRef = LDrawRef;
        //            newPart.LDrawColourID = int.Parse(LDrawColourID);
        //            newPart.UnityRef = "";
        //            newPart.state = Part.PartState.NOT_COMPLETED;
        //            newPart.PosX = float.Parse(posX);
        //            newPart.PosY = float.Parse(posY);
        //            newPart.PosZ = float.Parse(posZ);
        //            newPart.RotX = float.Parse(rotX);
        //            newPart.RotY = float.Parse(rotY);
        //            newPart.RotZ = float.Parse(rotZ);

        //            // ** Add PlacementMovements to Part **
        //            List<PlacementMovement> pmList = new List<PlacementMovement>();
        //            PlacementMovement pm = new PlacementMovement();
        //            pm.Axis = "Y";
        //            pm.Value = -5;
        //            pmList.Add(pm);
        //            newPart.placementMovementList = pmList;

        //            // ** ADD SUB PARTS TO PART **
        //            XmlNodeList subPartNodeList = Global_Variables.CompositePartCollectionXML.SelectNodes("//CompositePart[@ParentLDrawRef='" + LDrawRef + "']");
        //            foreach (XmlNode subPartNode in subPartNodeList)
        //            {
        //                // ** Get variables **
        //                String subPart_LDrawRef = subPartNode.SelectSingleNode("@LDrawRef").InnerXml;
        //                int subPart_LDrawColourID = int.Parse(subPartNode.SelectSingleNode("@LDrawColourID").InnerXml);
        //                float subPart_PosX = float.Parse(subPartNode.SelectSingleNode("@PosX").InnerXml);
        //                float subPart_PosY = float.Parse(subPartNode.SelectSingleNode("@PosY").InnerXml);
        //                float subPart_PosZ = float.Parse(subPartNode.SelectSingleNode("@PosZ").InnerXml);
        //                float subPart_RotX = float.Parse(subPartNode.SelectSingleNode("@RotX").InnerXml);
        //                float subPart_RotY = float.Parse(subPartNode.SelectSingleNode("@RotY").InnerXml);
        //                float subPart_RotZ = float.Parse(subPartNode.SelectSingleNode("@RotZ").InnerXml);

        //                // ** Generate subPart and add to SubPartList **
        //                Part subPart = new Part();
        //                subPart.LDrawRef = subPart_LDrawRef;
        //                subPart.LDrawColourID = subPart_LDrawColourID;
        //                subPart.UnityRef = "";
        //                subPart.state = Part.PartState.NOT_COMPLETED;
        //                subPart.IsSubPart = true;
        //                subPart.PosX = subPart_PosX;
        //                subPart.PosY = subPart_PosY;
        //                subPart.PosZ = subPart_PosZ;
        //                subPart.RotX = subPart_RotX;
        //                subPart.RotY = subPart_RotY;
        //                subPart.RotZ = subPart_RotZ;
        //                newPart.SubPartList.Add(subPart);
        //            }

        //            // ** REPLACE PART XML NODE ** 
        //            String xmlNodeString = HelperFunctions.RemoveAllNamespaces(newPart.SerializeToString(true));
        //            XmlDocument doc = new XmlDocument();
        //            doc.LoadXml(xmlNodeString);
        //            XmlNode oldNode = partNode;
        //            XmlNode parentNode = oldNode.ParentNode;
        //            XmlNode importNode = parentNode.OwnerDocument.ImportNode(doc.DocumentElement, true);
        //            parentNode.ReplaceChild(importNode, oldNode);

        //            //currentSetXml.Save(setfileLocation);
        //        }

        //        // ** Save Set to location **                
        //        currentSetXml.Save(setfileLocation);

        //        // ** SHow confirmation **
        //        MessageBox.Show("Set converted successfully...");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
                
        private void ShowMiniFigSet()
        {
            try
            {
                // ** Validation checks **
                if (currentSetXml == null)
                {
                    throw new Exception("No Set active...");
                }
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No node selected...");
                }                
                string parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                string parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                string LDrawModelType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']/@LDrawModelType").InnerXml;
                if (LDrawModelType != "MINIFIG")
                {
                    throw new Exception("MiniFig not selected...");
                }
                string Description = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']/@Description").InnerXml;
                string MiniFigRef = Description.Split('_')[0];
                
                // ** SHow new form **
                Generator form = new Generator();
                form.Visible = true;
                form.fldCurrentSetRef.Text = MiniFigRef;
                form.LoadSet();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SubModelImportPartPosRot()
        {            
            try
            {
                #region ** VALIDATION **
                if (tvSetSummary.SelectedNode == null)
                {
                    throw new Exception("No Model or SubModel node selected...");
                }
                string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                if (Type != "MODEL" & Type != "SUBMODEL")
                {
                    throw new Exception("Can only import data on Model or SubModel nodes");
                }
                #endregion

                // ** Get current Model or SubModel **              
                string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                string SetRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/ancestor::Set/@Ref").InnerXml;

                // ** GET SET PARTS FOR SUB MODEL **                         
                string SetPartXMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']/*/Part";
                //string SetPartXMLString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Part";
                XmlNodeList setPartNodeList = currentSetXml.SelectNodes(SetPartXMLString);

                #region ** GET SUBMODEL PARTS **
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "submodel-xmls").GetBlobClient(SubSetRef + "." + SubModelRef + ".xml");
                if (blob.Exists() == false)
                {
                    throw new Exception("SubModel XML file " + SubSetRef + "." + SubModelRef + ".xml not found...");
                }
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                XmlDocument SubModelXMLDoc = new XmlDocument();
                SubModelXMLDoc.LoadXml(xmlString);
                string SubModelPartXMLString = "//Part";
                //string SubModelPartXMLString = "//Part[@IsSubPart='false']";
                XmlNodeList subModelPartNodeList = SubModelXMLDoc.SelectNodes(SubModelPartXMLString);
                #endregion

                #region ** GENERATE SOURCE AND TARGET TABLES **
                DataTable sourceTable = GenerateStepPartTable(setPartNodeList);
                sourceTable.Columns.Add("Matched", typeof(string));
                DataTable targetTable = GenerateStepPartTable(subModelPartNodeList);
                targetTable.Columns.Add("Matched", typeof(string));
                for (int a = 0; a < sourceTable.Rows.Count; a++) sourceTable.Rows[a]["Matched"] = "False";
                for (int a = 0; a < targetTable.Rows.Count; a++) targetTable.Rows[a]["Matched"] = "False";
                #endregion

                #region ** RUN MATCHING PROCESS **
                bool overallMatch = true;
                for (int a = 0; a < sourceTable.Rows.Count; a++)
                {
                    // ** GET VARIABLES **
                    string Set_LDrawRef = sourceTable.Rows[a]["LDraw Ref"].ToString();
                    string Set_LDrawColourID = sourceTable.Rows[a]["LDraw Colour ID"].ToString();
                    string SubModel_LDrawRef = "";
                    string SubModel_LDrawColourID = "";
                    if (a < targetTable.Rows.Count)
                    {
                        SubModel_LDrawRef = targetTable.Rows[a]["LDraw Ref"].ToString();
                        SubModel_LDrawColourID = targetTable.Rows[a]["LDraw Colour ID"].ToString();
                    }
                    bool match = true;
                    if (Set_LDrawRef != SubModel_LDrawRef)
                    {
                        match = false;
                    }
                    if (Set_LDrawColourID != SubModel_LDrawColourID)
                    {
                        match = false;
                    }
                    sourceTable.Rows[a]["Matched"] = match;
                    if (a < targetTable.Rows.Count)
                    {
                        targetTable.Rows[a]["Matched"] = match;
                    }
                }
                int SourceUnmatchedCount = (from r in sourceTable.AsEnumerable()
                                            where r.Field<string>("Matched").Equals("False")
                                            select r).Count();
                int TargetUnmatchedCount = (from r in targetTable.AsEnumerable()
                                            where r.Field<string>("Matched").Equals("False")
                                            select r).Count();
                if (SourceUnmatchedCount > 0 || TargetUnmatchedCount > 0)
                {
                    overallMatch = false;
                }
                #endregion

                #region ** IF OVERALL MATCH = FALSE, SHOW Matching Screen **
                if (overallMatch == false)
                {
                    // ** REARRANGE SOURCE TABLE COLUMNS **
                    string[] columnNames = new string[] { "Step No", "Part Image", "LDraw Ref", "LDraw Colour ID", "LDraw Colour Name", "Colour Image", "Unity FBX", "Base Part Collection", "Part Type", "Is SubPart", "Step Node Index", "Placement Movements", "SuBSet Ref", "PosX", "PosY", "PosZ", "RotX", "RotY", "RotZ" };
                    var colIndex = 0;
                    foreach (var colName in columnNames)
                    {
                        sourceTable.Columns[colName].SetOrdinal(colIndex++);
                    }

                    // ** SHOW MATCHING SCREEN **
                    MatchingScreen form = new MatchingScreen();
                    form.sourceTable = sourceTable;
                    form.targetTable = targetTable;
                    form.Refresh_Screen();
                    form.Visible = true;
                    return;
                }
                #endregion

                #region ** UPDATE SET XML (IF OVERALL MATCH = TRUE) **
                int PartCount = 0;
                int SubPartCount = 0;
                for (int a = 0; a < subModelPartNodeList.Count; a++)
                {
                    string Set_LDrawRef = subModelPartNodeList[a].SelectSingleNode("@LDrawRef").InnerXml;
                    string Set_LDrawColourID = subModelPartNodeList[a].SelectSingleNode("@LDrawColourID").InnerXml;
                    bool IsSubPart = bool.Parse(subModelPartNodeList[a].SelectSingleNode("@IsSubPart").InnerXml);
                    if (IsSubPart == false)
                    {
                        PartCount += 1;
                    }
                    else
                    {
                        SubPartCount += 1;
                    }
                    string PosX = subModelPartNodeList[a].SelectSingleNode("@PosX").InnerXml;
                    string PosY = subModelPartNodeList[a].SelectSingleNode("@PosY").InnerXml;
                    string PosZ = subModelPartNodeList[a].SelectSingleNode("@PosZ").InnerXml;
                    string RotX = subModelPartNodeList[a].SelectSingleNode("@RotX").InnerXml;
                    string RotY = subModelPartNodeList[a].SelectSingleNode("@RotY").InnerXml;
                    string RotZ = subModelPartNodeList[a].SelectSingleNode("@RotZ").InnerXml;
                    setPartNodeList[a].SelectSingleNode("@PosX").InnerXml = PosX;
                    setPartNodeList[a].SelectSingleNode("@PosY").InnerXml = PosY;
                    setPartNodeList[a].SelectSingleNode("@PosZ").InnerXml = PosZ;
                    setPartNodeList[a].SelectSingleNode("@RotX").InnerXml = RotX;
                    setPartNodeList[a].SelectSingleNode("@RotY").InnerXml = RotY;
                    setPartNodeList[a].SelectSingleNode("@RotZ").InnerXml = RotZ;
                }

                // ** Refresh screen **
                RefreshScreen();

                // ** SHOW CONFIRMATION **
                MessageBox.Show("Successfully updated " + PartCount + " Part(s) and " + SubPartCount + " Sub Part(s)...");

                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void EnableControls_All(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_All(value)));
            }
            else
            {
                btnExit.Enabled = value;
                fldCurrentSetRef.Enabled = value;
                btnLoadSet.Enabled = value;
                btnSaveSet.Enabled = value;
                btnDeleteSet.Enabled = value;
                btnOpenSetInstructions.Enabled = value;
                btnOpenSetURLs.Enabled = value;
                chkShowSubParts.Enabled = value;
                chkShowPages.Enabled = value;
                tabControl1.Enabled = value;
            }
        }



    }






    // Class to allow for easy async upload and download functions with progress change notifications
    // Requires references to Microsoft.WindowsAzure.Storage.dll (Storage client 2.0) and Microsoft.WindowsAzure.StorageClient.dll (Storage client 1.7).
    // See comments on UploadBlobAsync and DownloadBlobAsync functions for information on removing the 1.7 client library dependency
    //class BlobTransfer
    //    {
    //        // Public async events
    //        public event AsyncCompletedEventHandler TransferCompleted;
    //        public event EventHandler<BlobTransferProgressChangedEventArgs> TransferProgressChanged;

    //        // Public BlobTransfer properties
    //        public TransferTypeEnum TransferType;

    //        // Private variables
    //        private ICancellableAsyncResult asyncresult;
    //        private bool Working = false;
    //        private object WorkingLock = new object();
    //        private AsyncOperation asyncOp;

    //        // Used to calculate download speeds
    //        private Queue<long> timeQueue = new Queue<long>(200);
    //        private Queue<long> bytesQueue = new Queue<long>(200);
    //        private DateTime updateTime = System.DateTime.Now;

    //        // Private BlobTransfer properties
    //        private string m_FileName;
    //        private ICloudBlob m_Blob;

    //        // Helper function to allow Storage Client 1.7 (Microsoft.WindowsAzure.StorageClient) to utilize this class.
    //        // Remove this function if only using Storage Client 2.0 (Microsoft.WindowsAzure.Storage).
    //        //public void UploadBlobAsync(Microsoft.WindowsAzure.StorageClient.CloudBlob blob, string LocalFile)
    //        //{
    //        //    Microsoft.WindowsAzure.StorageCredentialsAccountAndKey account = blob.ServiceClient.Credentials as Microsoft.WindowsAzure.StorageCredentialsAccountAndKey;
    //        //    ICloudBlob blob2 = new CloudBlockBlob(blob.Attributes.Uri, new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(blob.ServiceClient.Credentials.AccountName, account.Credentials.ExportBase64EncodedKey()));
    //        //    UploadBlobAsync(blob2, LocalFile);
    //        //}

    //        // Helper function to allow Storage Client 1.7 (Microsoft.WindowsAzure.StorageClient) to utilize this class.
    //        // Remove this function if only using Storage Client 2.0 (Microsoft.WindowsAzure.Storage).
    //        //public void DownloadBlobAsync(Microsoft.WindowsAzure.StorageClient.CloudBlob blob, string LocalFile)
    //        //{
    //        //    Microsoft.WindowsAzure.StorageCredentialsAccountAndKey account = blob.ServiceClient.Credentials as Microsoft.WindowsAzure.StorageCredentialsAccountAndKey;
    //        //    ICloudBlob blob2 = new CloudBlockBlob(blob.Attributes.Uri, new Microsoft.WindowsAzure.Storage.Auth.StorageCredentials(blob.ServiceClient.Credentials.AccountName, account.Credentials.ExportBase64EncodedKey()));
    //        //    DownloadBlobAsync(blob2, LocalFile);
    //        //}

    //        public void UploadBlobAsync(ICloudBlob blob, string LocalFile)
    //        {
    //            // The class currently stores state in class level variables so calling UploadBlobAsync or DownloadBlobAsync a second time will cause problems.
    //            // A better long term solution would be to better encapsulate the state, but the current solution works for the needs of my primary client.
    //            // Throw an exception if UploadBlobAsync or DownloadBlobAsync has already been called.
    //            lock (WorkingLock)
    //            {
    //                if (!Working)
    //                    Working = true;
    //                else
    //                    throw new Exception("BlobTransfer already initiated. Create new BlobTransfer object to initiate a new file transfer.");
    //            }

    //            // Attempt to open the file first so that we throw an exception before getting into the async work
    //            using (FileStream fstemp = new FileStream(LocalFile, FileMode.Open, FileAccess.Read)) { }

    //            // Create an async op in order to raise the events back to the client on the correct thread.
    //            asyncOp = AsyncOperationManager.CreateOperation(blob);

    //            TransferType = TransferTypeEnum.Upload;
    //            m_Blob = blob;
    //            m_FileName = LocalFile;

    //            var file = new FileInfo(m_FileName);
    //            long fileSize = file.Length;

    //            FileStream fs = new FileStream(m_FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
    //            ProgressStream pstream = new ProgressStream(fs);
    //            pstream.ProgressChanged += pstream_ProgressChanged;
    //            pstream.SetLength(fileSize);
    //            m_Blob.ServiceClient.ParallelOperationThreadCount = 10;
    //            asyncresult = m_Blob.BeginUploadFromStream(pstream, BlobTransferCompletedCallback, new BlobTransferAsyncState(m_Blob, pstream));
    //        }

    //        public void DownloadBlobAsync(ICloudBlob blob, string LocalFile)
    //        {
    //            // The class currently stores state in class level variables so calling UploadBlobAsync or DownloadBlobAsync a second time will cause problems.
    //            // A better long term solution would be to better encapsulate the state, but the current solution works for the needs of my primary client.
    //            // Throw an exception if UploadBlobAsync or DownloadBlobAsync has already been called.
    //            lock (WorkingLock)
    //            {
    //                if (!Working)
    //                    Working = true;
    //                else
    //                    throw new Exception("BlobTransfer already initiated. Create new BlobTransfer object to initiate a new file transfer.");
    //            }

    //            // Create an async op in order to raise the events back to the client on the correct thread.
    //            asyncOp = AsyncOperationManager.CreateOperation(blob);

    //            TransferType = TransferTypeEnum.Download;
    //            m_Blob = blob;
    //            m_FileName = LocalFile;

    //            m_Blob.FetchAttributes();

    //            FileStream fs = new FileStream(m_FileName, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read);
    //            ProgressStream pstream = new ProgressStream(fs);
    //            pstream.ProgressChanged += pstream_ProgressChanged;
    //            pstream.SetLength(m_Blob.Properties.Length);
    //            m_Blob.ServiceClient.ParallelOperationThreadCount = 10;
    //            asyncresult = m_Blob.BeginDownloadToStream(pstream, BlobTransferCompletedCallback, new BlobTransferAsyncState(m_Blob, pstream));
    //        }

    //        private void pstream_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //        {
    //            BlobTransferProgressChangedEventArgs eArgs = null;
    //            int progress = (int)((double)e.BytesRead / e.TotalLength * 100);

    //            // raise the progress changed event on the asyncop thread
    //            eArgs = new BlobTransferProgressChangedEventArgs(e.BytesRead, e.TotalLength, progress, CalculateSpeed(e.BytesRead), null);
    //            asyncOp.Post(delegate (object e2) { OnTaskProgressChanged((BlobTransferProgressChangedEventArgs)e2); }, eArgs);
    //        }

    //        private void BlobTransferCompletedCallback(IAsyncResult result)
    //        {
    //            BlobTransferAsyncState state = (BlobTransferAsyncState)result.AsyncState;
    //            ICloudBlob blob = state.Blob;
    //            ProgressStream stream = (ProgressStream)state.Stream;

    //            try
    //            {
    //                stream.Close();

    //                // End the operation.
    //                if (TransferType == TransferTypeEnum.Download)
    //                    blob.EndDownloadToStream(result);
    //                else if (TransferType == TransferTypeEnum.Upload)
    //                    blob.EndUploadFromStream(result);

    //                // Operation completed normally, raise the completed event
    //                AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, false, null);
    //                asyncOp.PostOperationCompleted(delegate (object e) { OnTaskCompleted((AsyncCompletedEventArgs)e); }, completedArgs);
    //            }
    //            catch (StorageException ex)
    //            {
    //                if (!state.Cancelled)
    //                {
    //                    throw (ex);
    //                }

    //                // Operation was cancelled, raise the event with the cancelled flag = true
    //                AsyncCompletedEventArgs completedArgs = new AsyncCompletedEventArgs(null, true, null);
    //                asyncOp.PostOperationCompleted(delegate (object e) { OnTaskCompleted((AsyncCompletedEventArgs)e); }, completedArgs);
    //            }
    //        }

    //        // Cancel the async download
    //        public void CancelAsync()
    //        {
    //            ((BlobTransferAsyncState)asyncresult.AsyncState).Cancelled = true;
    //            asyncresult.Cancel();
    //        }

    //        // Helper function to only raise the event if the client has subscribed to it.
    //        protected virtual void OnTaskCompleted(AsyncCompletedEventArgs e)
    //        {
    //            if (TransferCompleted != null)
    //                TransferCompleted(this, e);
    //        }

    //        // Helper function to only raise the event if the client has subscribed to it.
    //        protected virtual void OnTaskProgressChanged(BlobTransferProgressChangedEventArgs e)
    //        {
    //            if (TransferProgressChanged != null)
    //                TransferProgressChanged(this, e);
    //        }

    //        // Keep the last 200 progress change notifications and use them to calculate the average speed over that duration. 
    //        private double CalculateSpeed(long BytesSent)
    //        {
    //            double speed = 0;

    //            if (timeQueue.Count >= 200)
    //            {
    //                timeQueue.Dequeue();
    //                bytesQueue.Dequeue();
    //            }

    //            timeQueue.Enqueue(System.DateTime.Now.Ticks);
    //            bytesQueue.Enqueue(BytesSent);

    //            if (timeQueue.Count > 2)
    //            {
    //                updateTime = System.DateTime.Now;
    //                speed = (bytesQueue.Max() - bytesQueue.Min()) / TimeSpan.FromTicks(timeQueue.Max() - timeQueue.Min()).TotalSeconds;
    //            }

    //            return speed;
    //        }

    //        // A modified version of the ProgressStream from https://blogs.msdn.com/b/paolos/archive/2010/05/25/large-message-transfer-with-wcf-adapters-part-1.aspx
    //        // This class allows progress changed events to be raised from the blob upload/download.
    //        private class ProgressStream : Stream
    //        {
    //            #region Private Fields
    //            private Stream stream;
    //            private long bytesTransferred;
    //            private long totalLength;
    //            #endregion

    //            #region Public Handler
    //            public event EventHandler<ProgressChangedEventArgs> ProgressChanged;
    //            #endregion

    //            #region Public Constructor
    //            public ProgressStream(Stream file)
    //            {
    //                this.stream = file;
    //                this.totalLength = file.Length;
    //                this.bytesTransferred = 0;
    //            }
    //            #endregion

    //            #region Public Properties
    //            public override bool CanRead
    //            {
    //                get
    //                {
    //                    return this.stream.CanRead;
    //                }
    //            }

    //            public override bool CanSeek
    //            {
    //                get
    //                {
    //                    return this.stream.CanSeek;
    //                }
    //            }

    //            public override bool CanWrite
    //            {
    //                get
    //                {
    //                    return this.stream.CanWrite;
    //                }
    //            }

    //            public override void Flush()
    //            {
    //                this.stream.Flush();
    //            }

    //            public override void Close()
    //            {
    //                this.stream.Close();
    //            }

    //            public override long Length
    //            {
    //                get
    //                {
    //                    return this.stream.Length;
    //                }
    //            }

    //            public override long Position
    //            {
    //                get
    //                {
    //                    return this.stream.Position;
    //                }
    //                set
    //                {
    //                    this.stream.Position = value;
    //                }
    //            }
    //            #endregion

    //            #region Public Methods
    //            public override int Read(byte[] buffer, int offset, int count)
    //            {
    //                int result = stream.Read(buffer, offset, count);
    //                bytesTransferred += result;
    //                if (ProgressChanged != null)
    //                {
    //                    try
    //                    {
    //                        OnProgressChanged(new ProgressChangedEventArgs(bytesTransferred, totalLength));
    //                        //ProgressChanged(this, new ProgressChangedEventArgs(bytesTransferred, totalLength));
    //                    }
    //                    catch (Exception)
    //                    {
    //                        ProgressChanged = null;
    //                    }
    //                }
    //                return result;
    //            }

    //            protected virtual void OnProgressChanged(ProgressChangedEventArgs e)
    //            {
    //                if (ProgressChanged != null)
    //                    ProgressChanged(this, e);
    //            }

    //            public override long Seek(long offset, SeekOrigin origin)
    //            {
    //                return this.stream.Seek(offset, origin);
    //            }

    //            public override void SetLength(long value)
    //            {
    //                totalLength = value;
    //                //this.stream.SetLength(value);
    //            }

    //            public override void Write(byte[] buffer, int offset, int count)
    //            {
    //                this.stream.Write(buffer, offset, count);
    //                bytesTransferred += count;
    //                {
    //                    try
    //                    {
    //                        OnProgressChanged(new ProgressChangedEventArgs(bytesTransferred, totalLength));
    //                        //ProgressChanged(this, new ProgressChangedEventArgs(bytesTransferred, totalLength));
    //                    }
    //                    catch (Exception)
    //                    {
    //                        ProgressChanged = null;
    //                    }
    //                }
    //            }

    //            protected override void Dispose(bool disposing)
    //            {
    //                stream.Dispose();
    //                base.Dispose(disposing);
    //            }

    //            #endregion
    //        }

    //        private class BlobTransferAsyncState
    //        {
    //            public ICloudBlob Blob;
    //            public Stream Stream;
    //            public DateTime Started;
    //            public bool Cancelled;

    //            public BlobTransferAsyncState(ICloudBlob blob, Stream stream)
    //                : this(blob, stream, DateTime.Now)
    //            { }

    //            public BlobTransferAsyncState(ICloudBlob blob, Stream stream, DateTime started)
    //            {
    //                Blob = blob;
    //                Stream = stream;
    //                Started = started;
    //                Cancelled = false;
    //            }
    //        }

    //        private class ProgressChangedEventArgs : EventArgs
    //        {
    //            #region Private Fields
    //            private long bytesRead;
    //            private long totalLength;
    //            #endregion

    //            #region Public Constructor
    //            public ProgressChangedEventArgs(long bytesRead, long totalLength)
    //            {
    //                this.bytesRead = bytesRead;
    //                this.totalLength = totalLength;
    //            }
    //            #endregion

    //            #region Public properties

    //            public long BytesRead
    //            {
    //                get
    //                {
    //                    return this.bytesRead;
    //                }
    //                set
    //                {
    //                    this.bytesRead = value;
    //                }
    //            }

    //            public long TotalLength
    //            {
    //                get
    //                {
    //                    return this.totalLength;
    //                }
    //                set
    //                {
    //                    this.totalLength = value;
    //                }
    //            }
    //            #endregion
    //        }

    //        public enum TransferTypeEnum
    //        {
    //            Download,
    //            Upload
    //        }

    //        public class BlobTransferProgressChangedEventArgs : System.ComponentModel.ProgressChangedEventArgs
    //        {
    //            private long m_BytesSent = 0;
    //            private long m_TotalBytesToSend = 0;
    //            private double m_Speed = 0;

    //            public long BytesSent
    //            {
    //                get { return m_BytesSent; }
    //            }

    //            public long TotalBytesToSend
    //            {
    //                get { return m_TotalBytesToSend; }
    //            }

    //            public double Speed
    //            {
    //                get { return m_Speed; }
    //            }

    //            public TimeSpan TimeRemaining
    //            {
    //                get
    //                {
    //                    TimeSpan time = new TimeSpan(0, 0, (int)((TotalBytesToSend - m_BytesSent) / (m_Speed == 0 ? 1 : m_Speed)));
    //                    return time;
    //                }
    //            }

    //            public BlobTransferProgressChangedEventArgs(long BytesSent, long TotalBytesToSend, int progressPercentage, double Speed, object userState)
    //                : base(progressPercentage, userState)
    //            {
    //                m_BytesSent = BytesSent;
    //                m_TotalBytesToSend = TotalBytesToSend;
    //                m_Speed = Speed;
    //            }
    //        }
    //    }


    //public static class TreeViewExtensions
    //{
    //    public static List<string> GetExpansionState(this TreeNodeCollection nodes)
    //    {
    //        return nodes.Descendants()
    //                    .Where(n => n.IsExpanded)
    //                    .Select(n => n.FullPath)
    //                    .ToList();
    //    }

    //    public static void SetExpansionState(this TreeNodeCollection nodes, List<string> savedExpansionState)
    //    {
    //        foreach (var node in nodes.Descendants()
    //                                  .Where(n => savedExpansionState.Contains(n.FullPath)))
    //        {
    //            node.Expand();
    //        }
    //    }

    //    public static IEnumerable<TreeNode> Descendants(this TreeNodeCollection c)
    //    {
    //        foreach (var node in c.OfType<TreeNode>())
    //        {
    //            yield return node;

    //            foreach (var child in node.Nodes.Descendants())
    //            {
    //                yield return child;
    //            }
    //        }
    //    }
    //}

}
