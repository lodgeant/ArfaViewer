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
using System.Runtime.InteropServices;
using System.Data.SqlTypes;
using Newtonsoft.Json;




namespace Generator
{
    public partial class InstructionViewer : Form
    {
        // ** Variables **
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);        
        private Scintilla SetXML = new Scintilla();
        private Scintilla SetWithMFXML = new Scintilla();
        private Scintilla RebrickableXML = new Scintilla();
        private Scintilla LDRString = new Scintilla();
        private XmlDocument currentSetXml;
        private XmlDocument fullSetXml;
        private DataTable dgPartSummaryTable_Orig;
        private TreeNode lastSelectedNode;
        private string lastSelectedNodeFullPath = "";
        private string mode = "EDIT";
        
        public InstructionViewer(string prePopulatedSetRef)
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                PostHeader();
                log.Info(".......................................................................GENERATOR SCREEN STARTED.......................................................................");

                #region ** FORMAT SUMMARIES **
                String[] DGnames = new string[] { "dgPartSummary", "dgPartListSummary", "dgMiniFigsPartListSummary", "dgPartListWithMFsSummary", "dgSetSubModelPartSummary", "dgUnitySubModelPartSummary" };
                foreach (String dgName in DGnames)
                {
                    DataGridView dgv = (DataGridView)(this.Controls.Find(dgName, true)[0]);
                    dgv.AllowUserToAddRows = false;
                    dgv.AllowUserToDeleteRows = false;
                    dgv.AllowUserToOrderColumns = true;
                    dgv.MultiSelect = true;
                    dgv.CellBorderStyle = DataGridViewCellBorderStyle.None;
                    dgv.EnableHeadersVisualStyles = false;
                    dgv.SelectionMode = DataGridViewSelectionMode.CellSelect;
                    //dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
                    dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.EnableResizing;
                    dgv.ColumnHeadersHeight = 30;
                }
                lblStatus.Text = "";
                lblPartCount.Text = "";                
                lblPartSummaryItemFilteredCount.Text = "";
                lblPartListCount.Text = "";
                lblPartListWithMFsCount.Text = "";
                lblMiniFigsPartListCount.Text = "";
                lblSetSubModelPartCount.Text = "";
                lblSetSubModelPartSummaryItemFilteredCount.Text = "";
                lblUnitySubModelPartCount.Text = "";
                lblUnitySubModelPartSummaryItemFilteredCount.Text = "";
                #endregion

                #region ** ADD MAIN HEADER LINE TOOLSTRIP ITEMS **
                toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                btnExit,
                                toolStripSeparator1,
                                fldSetRecent,
                                lblSetRef,
                                fldCurrentSetRef,
                                btnLoadSet,
                                btnSaveSet,                                                                                         
                                new ToolStripControlHost(chkShowSubParts),
                                new ToolStripControlHost(chkShowPages),
                                toolStripSeparator2,
                                btnOpenSetURLs,
                                btnOpenSetInstructions,
                                toolStripSeparator22,
                                new ToolStripControlHost(chkShowPartcolourImages),
                                new ToolStripControlHost(chkShowElementImages),
                                new ToolStripControlHost(chkShowFBXDetails)

                                });
                #endregion

                #region ** ADD PART DETAILS TOOLSTRIP ITEMS **
                tsPartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                                toolStripLabel1,
                                fldLDrawRef,
                                fldLDrawImage,
                                new ToolStripControlHost(chkBasePartCollection),
                                btnAddPartToBasePartCollection,
                                toolStripLabel2,
                                fldLDrawColourID,
                                toolStripLabel4,
                                fldLDrawColourName,
                                lblQty,
                                fldQty,
                                //new ToolStripControlHost(chkBasePartCollection),                                                
                                toolStripLabel3,
                                fldPlacementMovements,
                                btnPartClear,
                                toolStripSeparator3,
                                btnPartAdd,
                                btnPartSave,
                                btnPartDelete,
                                toolStripSeparator24,                                
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
                                    //btnAddPartToBasePartCollection
                                    });
                #endregion

                // ** Populate the Ref field based on external request **
                fldCurrentSetRef.Text = prePopulatedSetRef;

                // ** Set up Scintilla **               
                pnlRebrickableXML.Controls.Add(RebrickableXML);
                ApplyDefaultScintillaStyles(RebrickableXML);
                pnlSetXML.Controls.Add(SetXML);
                ApplyDefaultScintillaStyles(SetXML);
                pnlSetWithMFXML.Controls.Add(SetWithMFXML);
                ApplyDefaultScintillaStyles(SetWithMFXML);
                pnlLDRString.Controls.Add(LDRString);
                ApplyDefaultScintillaStyles(LDRString);

                // ** REFRESH STATIC DATA **    
                RefreshLDrawColourNameDropdown();

                // ** Populate the Recent set items **
                // #### DOESN'T CURRENTLY WORK - NEED TO INVESTIGATE ####
                //fldSetRecent.DropDownItems.Clear();
                //RecentSetMappingCollection RecentSetMappingCollection = StaticData.GetRecentSetMappingData_UsingUserIDList(new List<string>() { Global_Variables.CurrentUserID });
                //foreach (RecentSetMapping rsm in RecentSetMappingCollection.RecentSetMappingList) fldSetRecent.DropDownItems.Add(rsm.SetRef + "|" + rsm.SetDescription);


                // ** UPDATE LABELS **                
                //fldCurrentSetRef.Text = "621-1";
                //fldCurrentSetRef.Text = "7327-1";
                //fldCurrentSetRef.Text = "621-2";
                //fldCurrentSetRef.Text = "41621-1";
                //fldCurrentSetRef.Text = "TEST-1";
                //fldCurrentSetRef.Text = "7305-1";
                //fldCurrentSetRef.Text = "8092-1";


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

        private void ApplyDefaultScintillaStyles(Scintilla TextArea)
        {
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
            TextArea.Lexer = Lexer.Xml;
            TextArea.StyleClearAll();

            // ** Configure the CPP Lexer styles **
            TextArea.Styles[Style.Xml.Comment].ForeColor = Color.Gray;
            TextArea.Styles[Style.Xml.Tag].ForeColor = Color.White;
            TextArea.Styles[Style.Xml.Attribute].ForeColor = Color.Red;
            TextArea.Styles[Style.Xml.DoubleString].ForeColor = Color.Yellow;
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

        //private void btnDeleteSet_Click(object sender, EventArgs e)
        //{
        //    DeleteSet();
        //}

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

        //private void btnConvertToLDR_Click(object sender, EventArgs e)
        //{
        //    ConvertSetXMLToLDR();
        //}

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
                
        private void btnShowBaseXMLInNotePadPlus_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldCurrentSetRef.Text;

                // ** Save file to temp location then open In Notepadd ++ **
                string tempfileLocation = Path.GetTempPath() + SetRef + "_Base.xml";
                File.WriteAllText(tempfileLocation, SetXML.Text);
                Process.Start("notepad++.exe", "\"" + tempfileLocation + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowWithMFXMLInNotePadPlus_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldCurrentSetRef.Text;

                // ** Save file to temp location then open In Notepadd ++ **
                string tempfileLocation = Path.GetTempPath() + SetRef + "_WithMF.xml";
                File.WriteAllText(tempfileLocation, SetWithMFXML.Text);
                Process.Start("notepad++.exe", "\"" + tempfileLocation + "\"");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnShowRebrickableXMLInNotePadPlus_Click(object sender, EventArgs e)
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldCurrentSetRef.Text;

                // ** Save file to temp location then open In Notepadd ++ **
                string tempfileLocation = Path.GetTempPath() + SetRef + "_Rebrickable.xml";
                File.WriteAllText(tempfileLocation, RebrickableXML.Text);
                Process.Start("notepad++.exe", "\"" + tempfileLocation + "\"");
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

        private void tsAdd3Steps_Click(object sender, EventArgs e)
        {
            InsertStep(3, "END");
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
                if (dgPartSummary.Rows.Count == 0) throw new Exception("No data to copy from " + dgPartSummary.Name + "...");                
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

        private void btnPartListMFCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopyToClipboard(dgMiniFigsPartListSummary);
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

        private void btnPartListRefresh_Click(object sender, EventArgs e)
        {
            RefreshPartList();
        }

        private void btnUnitySubModelsRefresh_Click(object sender, EventArgs e)
        {
            RefreshSetSubModels();
        }

        private void btnSyncSubModelPositions_Click(object sender, EventArgs e)
        {
            SyncSubModelPositions();
        }

        #endregion

        #region ** REFRESH STATIC DATA FUNCTIONS - DEMISED **

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

        #region ** SET FUNCTIONS **

        public void LoadSet()
        {
            try
            {
                // ** Validation Checks **
                if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldCurrentSetRef.Text;

                // ** Get Set Instructions from API **
                SetInstructions setInstructions = StaticData.GetSetInstructions(SetRef);
                if (setInstructions == null) throw new Exception("Set " + SetRef + " not found...");
                string setXML = setInstructions.Data;
                currentSetXml = new XmlDocument();
                currentSetXml.LoadXml(setXML);

                // ** Determine mode using SetDetails **
                SetDetails setDetails = StaticData.GetSetDetails(SetRef);
                if(setDetails != null)
                {
                    mode = "EDIT";
                    if (setDetails.AssignedTo != Global_Variables.CurrentUserID) mode = "READ-ONLY";
                }


                // UPDATE RECENT_SET_MAPPING FOR USER
                RecentSetMapping rsm = new RecentSetMapping();
                rsm.UserID = Global_Variables.CurrentUserID;
                rsm.CreatedTS = DateTime.Now;
                rsm.SetRef = SetRef;
                rsm.SetDescription = setDetails.Description;
                StaticData.AddRecentSetMapping(rsm);



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

                // ** Update SetInstructions **
                SetInstructions setInstructions = new SetInstructions() { Ref = fldCurrentSetRef.Text, Data = currentSetXml.OuterXml };
                StaticData.UpdateSetInstructions(setInstructions);

                // ** Update counts on SetDetails **
                int PartCount = 0;
                XmlNodeList PartListNodes = currentSetXml.SelectNodes("//PartListPart/@Qty");
                foreach(XmlNode node in PartListNodes) PartCount += int.Parse(node.InnerXml);                         
                int SubSetCount = currentSetXml.SelectNodes("//SubSet").Count;
                int ModelCount = currentSetXml.SelectNodes("//SubModel[@LDrawModelType='MODEL']").Count;
                int MiniFigCount = currentSetXml.SelectNodes("//SubModel[@LDrawModelType='MINIFIG']").Count;
                StaticData.UpdateSetDetailsCounts_UsingSetRef(fldCurrentSetRef.Text, PartCount, SubSetCount, ModelCount, MiniFigCount);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
               
        //private void DeleteSet()
        //{
        //    try
        //    {
        //        // ** Validation Checks **
        //        string setRef = fldCurrentSetRef.Text;
        //        if (setRef.Equals("")) throw new Exception("No Set Ref entered...");
        //        if (currentSetXml == null) throw new Exception("No Set currently loaded...");
        //        //if (StaticData.CheckIfSetExists(setRef) == false) throw new Exception("Set " + setRef + " not found...");

        //        // Make sure user wants to delete
        //        DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        //        if (res == DialogResult.Yes)
        //        {
        //            // ** Delete Set using API **
        //            StaticData.DeleteSet(setRef);

        //            // ** Tidy up & refresh screen **
        //            currentSetXml = null;
        //            fullSetXml = null;
        //            ClearAllFields();
        //            RefreshScreen();

        //            // ** Show confirm **
        //            MessageBox.Show("Set " + setRef + " successfully deleted...");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}
                
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
                btnOpenSetInstructions.Enabled = value;
                btnOpenSetURLs.Enabled = value;
                chkShowSubParts.Enabled = value;
                chkShowPages.Enabled = value;
                tabControl1.Enabled = value;                
                chkShowPartcolourImages.Enabled = value;
                chkShowElementImages.Enabled = value;
                chkShowFBXDetails.Enabled = value;                
            }
        }

        private void RefreshScreen()
        {
            string perfLog = "";
            Stopwatch watch = new Stopwatch();
            try
            {
                // ** Store Treeview node positions **
                watch.Reset(); watch.Start();
                savedExpansionState = tvSetSummary.Nodes.GetExpansionState();
                watch.Stop(); perfLog += "Store Treeview node positions:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                // ** CLEAR FIELDS ** 
                tvSetSummary.Nodes.Clear();
                dgPartSummary.DataSource = null;
                lblPartCount.Text = "";
                SetXML.Text = "";
                SetWithMFXML.Text = "";
                LDRString.Text = "";
                dgPartListSummary.DataSource = null;
                lblPartListCount.Text = "";
                dgPartListWithMFsSummary.DataSource = null;
                lblPartListWithMFsCount.Text = "";
                dgMiniFigsPartListSummary.DataSource = null;
                lblMiniFigsPartListCount.Text = "";


                // ** Run background to process functions **
                bw_RefreshScreen = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshScreen.DoWork += new DoWorkEventHandler(bw_RefreshScreen_DoWork);
                //bw_RefreshScreen.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshScreen_RunWorkerCompleted);
                bw_RefreshScreen.RunWorkerAsync();                
            }
            catch (Exception ex)
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_RefreshScreen(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshScreen_DoWork(object sender, DoWorkEventArgs e)
        {
            string perfLog = "";
            Stopwatch watch = new Stopwatch();
            try
            {
                // ** Process refresh only if a SET has been loaded **
                if (currentSetXml != null)
                {
                    watch.Reset(); watch.Start();
                    EnableControls_RefreshScreen(false);
                    watch.Stop(); perfLog += "Enable controls (false):\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** MERGE MINIFIG XML's INTO SET XML **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Merging MiniFig XML's...");                    
                    fullSetXml = new XmlDocument();
                    fullSetXml.LoadXml(currentSetXml.OuterXml);
                    //TODO: This function below needs to be made faster - AT-193
                    List<string> MiniFigSetList = Set.GetMinFigSetRefsFromSetXML(currentSetXml);
                    SetInstructionsCollection siColl = StaticData.GetSetInstructionsData_UsingSetRefList(MiniFigSetList);
                    if(siColl.SetInstructionsList.Count > 0) fullSetXml = Set.MergeMiniFigsIntoSetXML(fullSetXml, siColl);
                    watch.Stop(); perfLog += "Merge MiniFig XMLs with Set XML:\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Trigger Partlist refresh **
                    //RefreshPartList();

                    // ** Populate Summary Treeview with data **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Generating Treeview...");
                    Delegates.TreeView_AddNodes(this, tvSetSummary, Set.GetSetTreeViewFromSetXML(currentSetXml, chkShowPages.Checked, true, true, true));
                    watch.Stop(); perfLog += "Populate Summary Treeview with data:\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Update Set XML areas **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Updating Set XML areas...");
                    Delegates.Scintilla_SetText(this, SetXML, XDocument.Parse(currentSetXml.OuterXml).ToString());
                    Delegates.Scintilla_SetText(this, SetWithMFXML, XDocument.Parse(fullSetXml.OuterXml).ToString());
                    watch.Stop(); perfLog += "Update Set XML areas:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Update Rebrickable XML area **
                    //watch.Reset(); watch.Start();
                    //Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Updating Rebrickable XML area...");
                    //string SetRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;                    
                    //string XMLString = GetRebrickableXML(SetRef);
                    //Delegates.Scintilla_SetText(this, RebrickableXML, XMLString);
                    //watch.Stop(); perfLog += "Update Rebrickable XML area:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Update LDR text area **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Updating LDR text area...");
                    Delegates.Scintilla_SetText(this, LDRString, Set.ConvertSetXMLToLDRString(currentSetXml));
                    watch.Stop(); perfLog += "Update LDR text area:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Update Set Image **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Updating Set image...");
                    string SetRef = currentSetXml.SelectSingleNode("//Set/@Ref").InnerXml;
                    pnlSetImage.BackgroundImage = ArfaImage.GetImage(ImageType.SET, new string[] { SetRef });
                    watch.Stop(); perfLog += "Update Set Image:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Apply mode settings **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Applying mode settings...");
                    ApplyModeSettings();
                    watch.Stop(); perfLog += "Apply mode settings:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Trigger Partlist refresh **
                    RefreshPartList();

                    // ** Refresh SubModel view **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Updating Set Sub Models...");
                    RefreshSetSubModels();
                    watch.Stop(); perfLog += "Update Set Image:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Update Treeview selections **
                    watch.Reset(); watch.Start();                    
                    UpdateTreeviewSelections();
                    watch.Stop(); perfLog += "Update Treeview selections:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;

                    // ** Tidy up **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                    EnableControls_RefreshScreen(true);
                    watch.Stop(); perfLog += "Enable controls (true):\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }            
        }

        //private void bw_RefreshScreen_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    string perfLog = "";
        //    Stopwatch watch = new Stopwatch();
        //    try
        //    {
        //        // ** Update Treeview selections **
        //        watch.Reset(); watch.Start();
        //        tvSetSummary.Nodes.SetExpansionState(savedExpansionState);
        //        UpdateTreeviewSelections();
        //        watch.Stop(); perfLog += "Update Treeview selections:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private DataTable GeneratePartListTable(XmlNodeList partListNodeList, BasePartCollection BasePartCollection)
        {
            try
            {               
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
                    string LDrawColourName = (from r in Global_Variables.PartColourCollection.PartColourList
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

        private void UpdateTreeviewSelections()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => UpdateTreeviewSelections()));
            }
            else
            {
                // ** Set expansion state for the Treeview **
                tvSetSummary.Nodes.SetExpansionState(savedExpansionState);

                // ** Update Treeview selections **
                if (lastSelectedNode != null)
                {
                    TreeNode SelectedNode = tvSetSummary.Nodes.Descendants().Where(n => n.FullPath.Equals(lastSelectedNodeFullPath)).FirstOrDefault();
                    if (SelectedNode != null)
                    {
                        SelectedNode.BackColor = SystemColors.HighlightText;
                        tvSetSummary.Focus();
                    }
                    tvSetSummary.SelectedNode = SelectedNode;
                }
            }
        }

        private string GetRebrickableXML(string SetRef)
        {           
            try
            {
                // [1] Check if string is already in local cache.
                string XMLString = "";
                if (Global_Variables.RebrickableXMLDict.Keys.Count > 0 && Global_Variables.RebrickableXMLDict.ContainsKey(SetRef))
                {
                    XMLString = Global_Variables.RebrickableXMLDict[SetRef];
                }
                else
                {
                    string JSONString = StaticData.GetRebrickableSetJSONString(SetRef);
                    XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(JSONString), new XmlDictionaryReaderQuotas()));
                    XMLString = xml.ToString();
                    Global_Variables.RebrickableXMLDict.Add(SetRef, XMLString);
                }
                return XMLString;
            }
            catch(Exception ex)
            {               
                return ex.Message;
            }
        }

        #endregion
            
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

        #region ** REFRESH PARTLIST FUNCTIONS **

        private BackgroundWorker bw_RefreshPartList;

        private void EnableControls_RefreshPartList(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshPartList(value)));
            }
            else
            {
                tsPartListHeader.Enabled = value;
                gpPartListBasic.Enabled = value;
                gpPartListMiniFigs.Enabled = value;
                gpPartListWithMF.Enabled = value;
                //btnOpenSetInstructions.Enabled = value;
                //btnOpenSetURLs.Enabled = value;
                //chkShowSubParts.Enabled = value;
                //chkShowPages.Enabled = value;
                //tabControl1.Enabled = value;
                //chkShowPartcolourImages.Enabled = value;
                //chkShowElementImages.Enabled = value;
                //chkShowFBXDetails.Enabled = value;
            }
        }

        private void bw_RefreshPartList_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //string perfLog = "";
            //Stopwatch watch = new Stopwatch();
            try
            {
                //tpPartList.Text = "Part List";
                Delegates.TabPage_SetText(this, tpPartList, "Part List");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void RefreshPartList()
        {
            //string perfLog = "";
            Stopwatch watch = new Stopwatch();
            try
            {   
                Delegates.TabPage_SetText(this, tpPartList, "Part List (refreshing...)");

                // ** CLEAR FIELDS **                
                dgPartListSummary.DataSource = null;
                lblPartListCount.Text = "";
                dgPartListWithMFsSummary.DataSource = null;
                lblPartListWithMFsCount.Text = "";
                dgMiniFigsPartListSummary.DataSource = null;
                lblMiniFigsPartListCount.Text = "";

                // ** Run background to process functions **
                bw_RefreshPartList = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshPartList.DoWork += new DoWorkEventHandler(bw_RefreshPartList_DoWork);
                bw_RefreshPartList.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshPartList_RunWorkerCompleted);
                bw_RefreshPartList.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_RefreshPartList(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshPartList_DoWork(object sender, DoWorkEventArgs e)
        {
            string perfLog = "";
            Stopwatch watch = new Stopwatch();
            try
            {
                // ** Process refresh only if a SET has been loaded **
                if (currentSetXml != null)
                {
                    #region ** GET DATA UPFRONT **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblPartListStatus, "Refreshing - Getting part data (up front)...");
                    XmlNodeList allPartsNodeList = fullSetXml.SelectNodes("//PartListPart");
                    Delegates.ToolStripProgressBar_SetMax(this, pbPartlist, allPartsNodeList.Count);
                    Delegates.ToolStripProgressBar_SetValue(this, pbPartlist, 0);
                    int index = 0;
                    //List<int> LDrawColourIDList = new List<int>();
                    List<string> LDrawRefList = new List<string>();
                    foreach (XmlNode partNode in allPartsNodeList)
                    {
                        Delegates.ToolStripProgressBar_SetValue(this, pbPartlist, index);
                        int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        //if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                        if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
                        if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                        index += 1;
                    }
                    //artColourCollection PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
                    BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                    Delegates.ToolStripProgressBar_SetValue(this, pbPartlist, 0);
                    watch.Stop(); perfLog += "Get upfront part data:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                    #endregion

                    #region ** UPDATE PART LIST SUMMARIES - Current Set XML **
                    {
                        watch.Reset(); watch.Start();
                        Delegates.ToolStripLabel_SetText(this, lblPartListStatus, "Refreshing - Updating Part List: Basic...");
                        XmlNodeList partListNodeList = currentSetXml.SelectNodes("//PartListPart");
                        DataTable partListTable = GeneratePartListTable(partListNodeList, BasePartCollection);
                        partListTable.DefaultView.Sort = "LDraw Colour Name";
                        Delegates.DataGridView_SetDataSource(this, dgPartListSummary, partListTable.DefaultView.ToTable());
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
                        watch.Stop(); perfLog += "Update Part Summary - Current Set:\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                    }
                    #endregion

                    #region ** UPDATE PART LIST SUMMARIES - MINIFIGS **
                    {
                        watch.Reset(); watch.Start();

                        List<string> MiniFigSetList = Set.GetMinFigSetRefsFromSetXML(currentSetXml);
                        SetInstructionsCollection siColl = StaticData.GetSetInstructionsData_UsingSetRefList(MiniFigSetList);

                        Delegates.ToolStripLabel_SetText(this, lblPartListStatus, "Refreshing - Updating Part List: MiniFigs(s)...");
                        PartList pl = PartList.GetPartList_FromSetInstructionsCollection(siColl);
                        XmlDocument doc = new XmlDocument();
                        doc.LoadXml(pl.SerializeToString(true));
                        XmlNodeList partListNodeList = doc.SelectNodes("//PartListPart");
                        DataTable partListTable = GeneratePartListTable(partListNodeList, BasePartCollection);
                        partListTable.DefaultView.Sort = "LDraw Colour Name";
                        Delegates.DataGridView_SetDataSource(this, dgMiniFigsPartListSummary, partListTable.DefaultView.ToTable());
                        AdjustPartListSummaryRowFormatting(dgMiniFigsPartListSummary);

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
                        Delegates.ToolStripLabel_SetText(this, lblMiniFigsPartListCount, partCount.ToString("#,##0") + " Part(s), " + elementCount.ToString("#,##0") + " Element(s), " + lDrawPartCount.ToString("#,##0") + " LDraw Part(s), " + colourCount.ToString("#,##0") + " Colour(s)");
                        watch.Stop(); perfLog += "Update Part Summary - MiniFigs:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                    }
                    #endregion

                    #region ** UPDATE PART LIST SUMMARIES - Full Set XML **
                    watch.Reset(); watch.Start();
                    Delegates.ToolStripLabel_SetText(this, lblPartListStatus, "Refreshing - Updating Part List: Full...");
                    if (fullSetXml != null)
                    {
                        XmlNodeList partListNodeList = fullSetXml.SelectNodes("//PartListPart");
                        DataTable partListTable = GeneratePartListTable(partListNodeList, BasePartCollection);
                        partListTable.DefaultView.Sort = "LDraw Colour Name";
                        Delegates.DataGridView_SetDataSource(this, dgPartListWithMFsSummary, partListTable.DefaultView.ToTable());
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
                        watch.Stop(); perfLog += "Update Part Summary - Full Set:\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                    }
                    #endregion


                    // ** Tidy up **
                    Delegates.ToolStripLabel_SetText(this, lblPartListStatus, "");                    
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** TREENODE FUNCTIONS **

        private void tvSetSummary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // ** Clear all fields & disable groupboxes **
                //ClearAllFields();
                gpSet.Enabled = false;
                gpSubSet.Enabled = false;
                gpSubModel.Enabled = false;
                gpModel.Enabled = false;
                gpStep.Enabled = false;
                gbPartDetails.Enabled = false;
                ClearPartSummaryFields();
                dgPartSummary.DataSource = null;
                lblPartCount.Text = "";
                
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
                    string Description = "";
                    if (currentSetXml.SelectSingleNode("//Set[@Ref='" + SetRef + "']/@Description") != null)
                    {
                        Description = currentSetXml.SelectSingleNode("//Set[@Ref='" + SetRef + "']/@Description").InnerXml;
                    }

                    // ** Post data **
                    fldSetCurrentRef.Text = SetRef;
                    fldSetDescription.Text = Description;                    
                    gpSet.Enabled = true;
                }
                else if (Type.Equals("SUBSET"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string Description = "";
                    if(currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/@Description") != null)
                    {
                        Description = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/@Description").InnerXml;
                    }
                    string SubSetType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']/@SubSetType").InnerXml;

                    // ** Post data **                    
                    fldSubSetCurrentRef.Text = SubSetRef;
                    fldSubSetDescription.Text = Description;
                    fldSubSetType.Text = SubSetType;
                    gpSubSet.Enabled = true;

                    // ** POST PART DATA FOR SUBSET **
                    string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//Part";
                    if (chkShowSubParts.Checked == false) xmlString += "[@IsSubPart='false']";
                    XmlNodeList partNodeList = fullSetXml.SelectNodes(xmlString);
                    dgPartSummaryTable_Orig = GenerateStepPartTable(partNodeList);
                    dgPartSummary.DataSource = dgPartSummaryTable_Orig;
                    AdjustPartSummaryRowFormatting(dgPartSummary);
                    ProcessPartSummaryFilter();
                    lblPartCount.Text = dgPartSummaryTable_Orig.Rows.Count.ToString("#,##0") + " Part(s)";
                }
                else if (Type.Equals("MODEL"))
                {
                    // ** Get variables **
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];                    
                    //string SubSetRef = currentSetXml.SelectSingleNode("//SubModel[@Ref='" + ModelRef + "']/ancestor::SubSet/@Ref").InnerXml;
                    string ModelDescription = "";
                    if(currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@Description") != null)
                    {
                        ModelDescription = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@Description").InnerXml;
                    }
                    string ModelType = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@LDrawModelType").InnerXml;

                    // ** Post data **                    
                    fldModelCurrentRef.Text = ModelRef;
                    fldModelDescription.Text = ModelDescription;
                    fldModelType.Text = ModelType;
                    gpModel.Enabled = true;

                    // ** POST PART DATA FOR MODEL **
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part[@IsSubPart='false']");
                    string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part";
                    if (chkShowSubParts.Checked == false) xmlString += "[@IsSubPart='false']";                   
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

                    // ** POST PART DATA FOR SUBMODEL **
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part[@IsSubPart='false']");
                    //XmlNodeList partNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + Ref + "']//Part");                    
                    String xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//Part";
                    if (chkShowSubParts.Checked == false) xmlString += "[@IsSubPart='false']";                   
                    XmlNodeList partNodeList = currentSetXml.SelectNodes(xmlString);
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
                #endregion

                // ** Check if part list is null - if so just return an empty table with column headers only **
                if (partNodeList != null)
                {
                    #region ** GET DATA UPFRONT **
                    List<string> LDrawRefList = new List<string>();
                    int index = 0;
                    foreach (XmlNode partNode in partNodeList)
                    {
                        int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
                        if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                        index += 1;
                    }
                    // ** Get a Collections for this data **
                    BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                    LDrawDetailsCollection LDrawDetailsCollection = StaticData.GetLDrawDetailsData_UsingLDrawRefList(LDrawRefList);
                    FBXDetailsCollection FBXDetailsCollection = new FBXDetailsCollection();
                    if (chkShowFBXDetails.Checked) FBXDetailsCollection = StaticData.GetFBXDetailsData_UsingLDrawRefList(LDrawRefList);
                    #endregion

                    #region ** CYCLE THROUGH PART NODES AND GENERATE PART ROWS **  
                    int stepNodeIndex = 0;
                    int previousStepNo = 1;
                    foreach (XmlNode partNode in partNodeList)
                    {
                        #region ** GET LDRAW VARIABLES ** 
                        string SubSetRef = "";
                        if (partNode.SelectSingleNode("@SubSetRef") != null) SubSetRef = partNode.SelectSingleNode("@SubSetRef").InnerXml;
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                        bool IsSubPart = bool.Parse(partNode.SelectSingleNode("@IsSubPart").InnerXml);
                        int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                        string LDrawColourName = (from r in Global_Variables.PartColourCollection.PartColourList
                                                  where r.LDrawColourID == LDrawColourID
                                                  select r.LDrawColourName).FirstOrDefault();
                        string partType = (from r in BasePartCollection.BasePartList
                                           where r.LDrawRef.Equals(LDrawRef)
                                           select r.partType.ToString()).FirstOrDefault();
                        string LDrawDescription = (from r in BasePartCollection.BasePartList
                                                   where r.LDrawRef.Equals(LDrawRef)
                                                   select r.LDrawDescription).FirstOrDefault();
                        string LDrawPartType = (from r in BasePartCollection.BasePartList
                                                where r.LDrawRef.Equals(LDrawRef)
                                                select r.lDrawPartType.ToString()).FirstOrDefault();
                        if (LDrawPartType == null) LDrawPartType = "";
                        #endregion

                        // ** Check for official/unoffical part **                    
                        bool IsOfficial = false;
                        if (LDrawPartType.Equals("OFFICIAL")) IsOfficial = true;

                        // Get LDrawDetails for part **                    
                        LDrawDetails LDrawDetails = (from r in LDrawDetailsCollection.LDrawDetailsList
                                                     where r.LDrawRef.Equals(LDrawRef)
                                                     select r).FirstOrDefault();

                        // ** GET FBX DETAILS FOR PART MODEL **                                      
                        FBXDetails fbxDetails = new FBXDetails();
                        if (chkShowFBXDetails.Checked)
                        {
                            fbxDetails = (from r in FBXDetailsCollection.FBXDetailsList
                                          where r.LDrawRef.Equals(LDrawRef)
                                          select r).FirstOrDefault();
                        }

                        // ** Check BasePart Collection **
                        bool basePartCollection = true;
                        BasePart BasePart = (from r in BasePartCollection.BasePartList
                                             where r.LDrawRef.Equals(LDrawRef)
                                             select r).FirstOrDefault();
                        if (BasePart == null) basePartCollection = false;

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
                        newRow["Unity FBX"] = fbxDetails.AllFBXExist;
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
                        newRow["FBX Size"] = fbxDetails.FBXSize;
                        newRow["FBX Count"] = fbxDetails.FBXCount;
                        newRow["LDraw Part Type"] = LDrawPartType;
                        newRow["LDraw Description"] = LDrawDescription;
                        partTable.Rows.Add(newRow);
                    }
                    #endregion

                }
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
                    if (chkShowFBXDetails.Checked == true)
                    {
                        if (row.Cells["Unity FBX"].Value.ToString().ToUpper().Equals("FALSE"))
                        {
                            row.DefaultCellStyle.BackColor = Color.LightSalmon;
                        }
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

        private void CompareSetPartsWithRebrickable()
        {
            try
            {
                // ** Validation Checks **               
                if (fullSetXml == null) throw new Exception("No Set Ref loaded...");
                string SetRef = fldCurrentSetRef.Text;



                #region ** GET DATA UPFRONT **
                //watch.Reset(); watch.Start();
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Getting part data (up front)...");
                XmlNodeList allPartsNodeList = fullSetXml.SelectNodes("//PartListPart");
                //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, allPartsNodeList.Count);
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                int index = 0;
                //List<int> LDrawColourIDList = new List<int>();
                List<string> LDrawRefList = new List<string>();
                foreach (XmlNode partNode in allPartsNodeList)
                {
                    //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, index);
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("@LDrawColourID").InnerXml);
                    string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;
                    //if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
                    if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                    index += 1;
                }
                //PartColourCollection PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
                BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                //watch.Stop(); perfLog += "Get upfront part data:\t\t\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                #endregion



                // ** GET SOURCE TABLE FROM FULL SET XML **
                XmlNodeList partListNodeList = currentSetXml.SelectNodes("//PartListPart");
                //DataTable sourceTable = GeneratePartListTable(partListNodeList);
                DataTable sourceTable = GeneratePartListTable(partListNodeList, BasePartCollection);
                sourceTable.DefaultView.Sort = "LDraw Colour ID, LDraw Ref";
                sourceTable = sourceTable.DefaultView.ToTable();
                sourceTable.Columns.Add("Matched", typeof(string));
                for (int a = 0; a < sourceTable.Rows.Count; a++) sourceTable.Rows[a]["Matched"] = "False";

                // ** GET TARGET TABLE FROM REBRICKABLE **
                string JSONString = StaticData.GetRebrickableSetJSONString(SetRef);
                DataTable targetTable = GeneratePartListTable_FromRebrickableJSON(JSONString);
                targetTable.Columns.Add("Matched", typeof(string));
                for (int a = 0; a < targetTable.Rows.Count; a++) targetTable.Rows[a]["Matched"] = "False";

                #region ** RUN MATCHING PROCESS **
                //bool overallMatch = true;
                for (int a = 0; a < sourceTable.Rows.Count; a++)
                {
                    string Set_LDrawRef = sourceTable.Rows[a]["LDraw Ref"].ToString();
                    int Set_LDrawColourID = int.Parse(sourceTable.Rows[a]["LDraw Colour ID"].ToString());
                    int Set_Qty = int.Parse(sourceTable.Rows[a]["Qty"].ToString());

                    // ** Find match **
                    var targetPart = (from r in targetTable.AsEnumerable()
                                      where r.Field<string>("Matched").Equals("False")
                                      //&& r.Field<string>("LDraw Ref").Equals(Set_LDrawRef)
                                      && r.Field<string>("LDraw Ref List").Contains(Set_LDrawRef + "|")                                      
                                      && r.Field<int>("LDraw Colour ID") == Set_LDrawColourID
                                      && r.Field<int>("Qty") == Set_Qty
                                      select r).FirstOrDefault();
                    if (targetPart != null)
                    {
                        sourceTable.Rows[a]["Matched"] = true;
                        targetPart["Matched"] = true;
                        targetPart["LDraw Ref"] = Set_LDrawRef;
                        targetPart["LDraw Description"] = "Description";
                    }
                }
                //int SourceUnmatchedCount = (from r in sourceTable.AsEnumerable()
                //                            where r.Field<string>("Matched").Equals("False")
                //                            select r).Count();
                //int TargetUnmatchedCount = (from r in targetTable.AsEnumerable()
                //                            where r.Field<string>("Matched").Equals("False")
                //                            select r).Count();
                //if (SourceUnmatchedCount > 0 || TargetUnmatchedCount > 0)
                //{
                //    overallMatch = false;
                //}
                #endregion

                // ** Adjust target table sorting **
                targetTable.DefaultView.Sort = "LDraw Colour ID, LDraw Ref";
                targetTable = targetTable.DefaultView.ToTable();

                #region ** GET DATA UPFRONT **
                // Get a list of LDrawColourIDs & LDrawRefs
                //LDrawColourIDList = new List<int>();
                LDrawRefList = new List<string>();
                foreach (DataRow row in targetTable.Rows)
                {
                    string LDrawRef = row["LDraw Ref"].ToString();
                    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);                    
                    int LDrawColourID = int.Parse(row["LDraw Colour ID"].ToString());
                    //if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);

                    // ** Get Element images **                
                    if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
                }
                //PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
                BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
                #endregion

                #region ** Enrich Target table with relevant details **
                foreach (DataRow row in targetTable.Rows)
                {
                    // ** Get LDraw variables **
                    string LDrawRef = row["LDraw Ref"].ToString();
                    int LDrawColourID = int.Parse(row["LDraw Colour ID"].ToString());
                    string LDrawColourName = (from r in Global_Variables.PartColourCollection.PartColourList
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

                    // ** Update Target table **
                    row["Part Image"] = elementImage;
                    row["LDraw Description"] = LDrawDescription;
                    row["LDraw Colour Name"] = LDrawColourName;
                    row["Colour Image"] = partColourImage;  
                }
                #endregion

                // ** Show Matching Screen **                
                MatchingScreen form = new MatchingScreen() { sourceTable = sourceTable, sourceTableName = "Arfa Set", targetTable = targetTable, targetTableName = "Rebrickable" };                    
                form.Refresh_Screen();
                form.Visible = true;
                return;                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //private DataTable GeneratePartListTable_FromRebrickableJSON_old(string JSONString)
        //{
        //    DataTable partListTable = new DataTable("partListTable", "partListTable");
        //    string LDrawRef_debug = "";
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

        //        #region ** GET DATA UPFRONT **
        //        //// Get a list of LDrawColourIDs & LDrawRefs
        //        //List<int> LDrawColourIDList = new List<int>();
        //        //List<string> LDrawRefList = new List<string>();
        //        //foreach (XmlNode partNode in partItemList)
        //        //{
        //        //    string LDrawRef = partNode.SelectSingleNode("part/part_num").InnerXml;
        //        //    if (partNode.SelectSingleNode("part/external_ids/LDraw") != null) LDrawRef = partNode.SelectSingleNode("part/external_ids/LDraw/item").InnerXml;                   
        //        //    if (LDrawRefList.Contains(LDrawRef) == false) LDrawRefList.Add(LDrawRef);
        //        //    LDrawRef_debug = LDrawRef;                   
        //        //    int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/external_ids/LDraw/ext_ids/item[1]").InnerXml);
        //        //    if (LDrawColourIDList.Contains(LDrawColourID) == false) LDrawColourIDList.Add(LDrawColourID);
                    
        //        //    // ** Get Element images **                
        //        //    if (chkShowElementImages.Checked) ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });                    
        //        //}
        //        //PartColourCollection PartColourCollection = StaticData.GetPartColourData_UsingLDrawColourIDList(LDrawColourIDList);
        //        //BasePartCollection BasePartCollection = StaticData.GetBasePartData_UsingLDrawRefList(LDrawRefList);
        //        #endregion

        //        // ** Cycle through nodes and generate table rows **               
        //        foreach (XmlNode partNode in partItemList)
        //        {
        //            // ** GET LDRAW VARIABLES **                    
        //            string LDrawRef = "";
        //            //if (partNode.SelectSingleNode("part/part_num") != null)
        //            //{
        //            //    LDrawRef = partNode.SelectSingleNode("part/part_num").InnerXml;
        //            //}
        //            //if (partNode.SelectSingleNode("part/external_ids/LDraw") != null)
        //            //{
        //            //    LDrawRef = partNode.SelectSingleNode("part/external_ids/LDraw/item").InnerXml;
        //            //}                    
        //            XmlNodeList nodeList = partNode.SelectNodes("part/external_ids/LDraw/item");
        //            foreach(XmlNode node in nodeList)
        //            {
        //                LDrawRef += node.InnerText + "|";
        //            }
        //            LDrawRef_debug = LDrawRef;


        //            // LDrawColourID = int.Parse(partNode.SelectSingleNode("color/id").InnerXml);
        //            int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/external_ids/LDraw/ext_ids/item[1]").InnerXml);
        //            int Qty = int.Parse(partNode.SelectSingleNode("quantity").InnerXml); 
        //            //string LDrawColourName = (from r in PartColourCollection.PartColourList
        //            //                          where r.LDrawColourID == LDrawColourID
        //            //                          select r.LDrawColourName).FirstOrDefault();
        //            //string LDrawDescription = (from r in BasePartCollection.BasePartList
        //            //                           where r.LDrawRef.Equals(LDrawRef)
        //            //                           select r.LDrawDescription).FirstOrDefault();

        //            // ** Get element & Partcolour images **
        //            //Bitmap elementImage = null;
        //            //Bitmap partColourImage = null;
        //            //if (chkShowElementImages.Checked) elementImage = ArfaImage.GetImage(ImageType.ELEMENT, new string[] { LDrawRef, LDrawColourID.ToString() });
        //            //if (chkShowPartcolourImages.Checked) partColourImage = ArfaImage.GetImage(ImageType.PARTCOLOUR, new string[] { LDrawColourID.ToString() });

        //            // ** Build row and add to table **                     
        //            DataRow newRow = partListTable.NewRow();
        //            //newRow["Part Image"] = elementImage;
        //            //newRow["LDraw Ref"] = LDrawRef;
        //            newRow["LDraw Ref List"] = LDrawRef;
        //            //newRow["LDraw Description"] = LDrawDescription;
        //            newRow["LDraw Colour ID"] = LDrawColourID;
        //            //newRow["LDraw Colour Name"] = LDrawColourName;
        //            //newRow["Colour Image"] = partColourImage;
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

        private DataTable GeneratePartListTable_FromRebrickableJSON(string JSONString)
        {
            DataTable partListTable = new DataTable("partListTable", "partListTable");
            //string LDrawRef_debug = "";
            try
            {
                // ** GENERATE COLUMNS **
                partListTable.Columns.Add("Part Image", typeof(Bitmap));
                partListTable.Columns.Add("LDraw Ref", typeof(string));
                partListTable.Columns.Add("LDraw Ref List", typeof(string));
                partListTable.Columns.Add("LDraw Description", typeof(string));
                partListTable.Columns.Add("LDraw Colour ID", typeof(int));
                partListTable.Columns.Add("LDraw Colour Name", typeof(string));
                partListTable.Columns.Add("Colour Image", typeof(Bitmap));
                partListTable.Columns.Add("Qty", typeof(int));

                // ** Load JSON string to XML **                
                XDocument xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(JSONString), new XmlDictionaryReaderQuotas()));
                string XMLString = xml.ToString();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);
                XmlNodeList partItemList = doc.SelectNodes("//item[@type='object' and is_spare='false']");

                // ** Cycle through nodes and generate table rows **               
                foreach (XmlNode partNode in partItemList)
                {
                    // ** GET LDRAW VARIABLES **                    
                    string LDrawRef = "";                      
                    XmlNodeList nodeList = partNode.SelectNodes("part/external_ids/LDraw/item");
                    foreach (XmlNode node in nodeList) LDrawRef += node.InnerText + "|";                    
                    //LDrawRef_debug = LDrawRef;
                    int LDrawColourID = int.Parse(partNode.SelectSingleNode("color/external_ids/LDraw/ext_ids/item[1]").InnerXml);
                    int Qty = int.Parse(partNode.SelectSingleNode("quantity").InnerXml);
                    
                    // ** Build row and add to table **                     
                    DataRow newRow = partListTable.NewRow();                   
                    newRow["LDraw Ref List"] = LDrawRef;
                    newRow["LDraw Colour ID"] = LDrawColourID;
                    newRow["Qty"] = Qty;
                    partListTable.Rows.Add(newRow);
                }
                return partListTable;
            }
            catch (Exception)
            {
                return partListTable;
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
                    //string LDrawColourID = StaticData.GetLDrawColourID(fldLDrawColourName.Text);
                    string LDrawColourID = (from r in Global_Variables.PartColourCollection.PartColourList
                                              where r.LDrawColourName.ToUpper().Equals(fldLDrawColourName.Text.ToUpper())
                                              select r.LDrawColourID).FirstOrDefault().ToString();
                    fldLDrawColourID.Text = LDrawColourID;
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
                    //string LDrawColourName = StaticData.GetLDrawColourName(fldLDrawColourID.Text);
                    string LDrawColourName = (from r in Global_Variables.PartColourCollection.PartColourList
                                              where r.LDrawColourID == int.Parse(fldLDrawColourID.Text)
                                              select r.LDrawColourName).FirstOrDefault();
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
                //fldLDrawRef.Text = fldLDrawRef.Text.ToLower();
                if (fldLDrawRef.Text.EndsWith("Legs") == false && fldLDrawRef.Text.EndsWith("Torso") == false) fldLDrawRef.Text = fldLDrawRef.Text.ToLower();
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
                    btnAddPartToBasePartCollection.Enabled = false;
                    btnAddPartToBasePartCollection.BackColor = Color.Transparent;
                    tsBasePartCollection.Enabled = false;
                }
                else
                {
                    chkBasePartCollection.Checked = false;
                    btnAddPartToBasePartCollection.Enabled = true;
                    btnAddPartToBasePartCollection.BackColor = Color.Red;
                    tsBasePartCollection.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

        #region ** PART SUMMARY FUNCTIONS CLICK **

        private void Handle_dgPartSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0 || e.ColumnIndex == 4)
                {
                    //Bitmap image = (Bitmap)dgPartSummary.SelectedRows[0].Cells[e.ColumnIndex].Value;                   
                    Bitmap image = (Bitmap)dgPartSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
                else
                {                   
                    // ** Get variables **
                    //string pureStepNo = fldPureStepNo.Text;
                    //string pureStepNo = dgPartSummary.Rows[e.RowIndex].Cells["Step No"].Value.ToString();
                    //string parentSubSetRef = fldStepParentSubSetRef.Text;
                    //string parentSubModelRef = fldStepParentModelRef.Text;
                    string PartSubSetRef = (string)dgPartSummary.Rows[e.RowIndex].Cells["SubSet Ref"].Value;

                    // ** Get Part Xml node **                
                    //string xmlString = "";
                    //if (chkShowSubParts.Checked)
                    //{
                    //    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + (e.RowIndex + 1) + "]";                        
                    //}
                    //else
                    //{
                    //    //xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + (e.RowIndex + 1) + "]";
                    //    //xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Part)[@SubSetRef='" + PartSubSetRef + "']";
                    //    xmlString = "//Part[@SubSetRef='" + PartSubSetRef + "']";
                    //}
                    string xmlString = "//Part[@SubSetRef='" + PartSubSetRef + "']";
                    XmlNode partNode = fullSetXml.SelectSingleNode(xmlString);                    
                    if (partNode != null)
                    {
                        // ** Update UI based on whether part is Sub Part or not **                        
                        bool IsSubPart = false;
                        if (partNode.SelectSingleNode("@IsSubPart") != null) IsSubPart = bool.Parse(partNode.SelectSingleNode("@IsSubPart").InnerXml);                       
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

                        // ** Post Part Positions & Rotations **
                        fldPartPosX.Text = partNode.SelectSingleNode("@PosX").InnerXml;
                        fldPartPosY.Text = partNode.SelectSingleNode("@PosY").InnerXml;
                        fldPartPosZ.Text = partNode.SelectSingleNode("@PosZ").InnerXml;
                        fldPartRotX.Text = partNode.SelectSingleNode("@RotX").InnerXml;
                        fldPartRotY.Text = partNode.SelectSingleNode("@RotY").InnerXml;
                        fldPartRotZ.Text = partNode.SelectSingleNode("@RotZ").InnerXml;

                        // ** Post LDraw Size **
                        string LDrawRef = partNode.SelectSingleNode("@LDrawRef").InnerXml;                        
                        int LDrawSize = StaticData.GetLDrawSize(LDrawRef);                        
                        if (LDrawSize > 0) fldLDrawSize.Text = LDrawSize.ToString();

                        // ** Trigger leave functions **                        
                        ProcessLDrawRef_Leave();
                        ProcessLDrawColourID_Leave();
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
                    Bitmap LDrawImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                    if (LDrawImage == null) throw new Exception("LDraw image for " + LDrawRef + " not found in Azure...");                    
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
                StringBuilder sb = HelperFunctions.GenerateClipboardStringFromDataTable(dg);
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

        //private void MoveNode_OLD(int directionAdj)
        //{
        //    try
        //    {
        //        // ** Get selected node details **               
        //        string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
        //        //String Ref = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];

        //        // ** Get node details **
        //        XmlNode selectedNode = null;
        //        XmlNodeList parentNodeList = null;
        //        string parentSubSetRef = "";
        //        string parentModelRef = "";
        //        if (Type.Equals("SUBSET"))
        //        {
        //            string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //            selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']");
        //            parentNodeList = currentSetXml.SelectNodes("//SubSet");
        //        }
        //        else if (Type.Equals("MODEL"))
        //        {
        //            string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //            parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //            selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + ModelRef + "']");
        //            parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@SubModelLevel=1]");
        //        }
        //        else if (Type.Equals("SUBMODEL"))
        //        {
        //            // ** Get variables **
        //            string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //            parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //            string parentSubModelRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//parent::SubModel/@Ref").InnerXml;
        //            selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']");
        //            parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']/SubModel");
        //        }
        //        else if (Type.Equals("STEP"))
        //        {
        //            // ** Get variables **                   
        //            parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //            string pureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
        //            parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //            selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']");
        //            parentNodeList = selectedNode.ParentNode.ChildNodes;
        //        }
        //        else if (Type.Equals("PART"))
        //        {
        //            parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
        //            parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
        //            string parentPureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
        //            string LDrawRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[4];
        //            string LDrawColourID = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[5];
        //            int nodeIndex = int.Parse(tvSetSummary.SelectedNode.Tag.ToString().Split('|')[6]);
        //            selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + parentPureStepNo + "']//Part[" + (nodeIndex + 1) + "]");
        //            parentNodeList = selectedNode.ParentNode.ChildNodes;
        //        }
        //        else
        //        {
        //            throw new Exception("Cannot move selected node");
        //        }





        //        //XmlNode parent = priusNode.ParentNode.
        //        //XmlNode previousNode = priusNode.PreviousSibling;
        //        //parent.InsertBefore(priusNode, previousNode);



        //        // ** Work out new node index **
        //        int selectedNodeIndex = 0;
        //        for (int a = 0; a < parentNodeList.Count; a++)
        //        {
        //            if (parentNodeList[a] == selectedNode)
        //            {
        //                selectedNodeIndex = a;
        //                break;
        //            }
        //        }
        //        int newNodeIndex = selectedNodeIndex + directionAdj;
        //        if (newNodeIndex < 0) newNodeIndex = 0;


        //        // ** Add the nodes back into the parent except the SelectedNode **
        //        XmlNode parentNode = selectedNode.ParentNode;
        //        List<XmlNode> nodeList = new List<XmlNode>();
        //        foreach (XmlNode childNode in parentNode.ChildNodes)
        //        {
        //            if (childNode != selectedNode)
        //            {
        //                nodeList.Add(childNode);
        //            }
        //        }
        //        nodeList.Insert(newNodeIndex, selectedNode);

        //        // ** Post nodes back into parent **                
        //        foreach (XmlNode childNode in parentNode.ChildNodes)
        //        {
        //            parentNode.RemoveChild(childNode);
        //        }
        //        foreach (XmlNode node in nodeList)
        //        {
        //            parentNode.AppendChild(node);
        //        }

        //        // ** Adjust SubSet PureStep Numbers **
        //        if (Type.Equals("SUBMODEL") || Type.Equals("STEP"))
        //        {
        //            AdjustSubSetStepNumbers(parentSubSetRef, parentModelRef);
        //        }

        //        // ** Clear fields **
        //        ClearAllFields();

        //        // ** Refresh screen **
        //        RefreshScreen();
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void MoveNode(int directionAdj)
        {
            try
            {
                // ** Get selected node details **               
                string Type = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[0];
                //String Ref = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];

                // ** Get node details **
                XmlNode selectedNode = null;
                //XmlNodeList parentNodeList = null;
                string parentSubSetRef = "";
                string parentModelRef = "";
                if (Type.Equals("SUBSET"))
                {
                    string SubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']");
                    //parentNodeList = currentSetXml.SelectNodes("//SubSet");
                }
                else if (Type.Equals("MODEL"))
                {
                    string ModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + ModelRef + "']");
                    //parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@SubModelLevel=1]");
                }
                else if (Type.Equals("SUBMODEL"))
                {
                    // ** Get variables **
                    string SubModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    string parentSubModelRef = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']//parent::SubModel/@Ref").InnerXml;
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + SubModelRef + "']");
                    //parentNodeList = currentSetXml.SelectNodes("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']/SubModel");
                }
                else if (Type.Equals("STEP"))
                {
                    // ** Get variables **                   
                    parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    string pureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']");
                    //parentNodeList = selectedNode.ParentNode.ChildNodes;
                }
                else if (Type.Equals("PART"))
                {
                    parentSubSetRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[1];
                    parentModelRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[2];
                    string parentPureStepNo = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[3];
                    string LDrawRef = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[4];
                    string LDrawColourID = tvSetSummary.SelectedNode.Tag.ToString().Split('|')[5];
                    //int nodeIndex = int.Parse(tvSetSummary.SelectedNode.Tag.ToString().Split('|')[6]);
                    int nodeIndex = tvSetSummary.SelectedNode.Index;
                    selectedNode = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentModelRef + "']//Step[@PureStepNo='" + parentPureStepNo + "']/Part[" + (nodeIndex + 1) + "]");
                    //parentNodeList = selectedNode.ParentNode.ChildNodes;
                }
                else
                {
                    throw new Exception("Cannot move selected node");
                }

                // ** Move node in relevant direction **
                XmlNode parent = selectedNode.ParentNode;
                if(directionAdj == -1)
                {
                    XmlNode previousNode = selectedNode.PreviousSibling;
                    parent.InsertBefore(selectedNode, previousNode);
                }
                else
                {
                    XmlNode nextNode = selectedNode.NextSibling;
                    parent.InsertAfter(selectedNode, nextNode);
                }

                // ** Adjust SubSet PureStep Numbers **
                if (Type.Equals("SUBMODEL") || Type.Equals("STEP")) AdjustSubSetStepNumbers(parentSubSetRef, parentModelRef);
                
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
                if (partType.Equals("COMPOSITE") && StaticData.CheckIfSubPartMappingPartsExist(LDrawRef) == false)
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

                // ** GET SUB PART COLLECTION **
                SubPartMappingCollection coll = new SubPartMappingCollection();
                if (partType.Equals("COMPOSITE")) coll = StaticData.GetSubPartMappingData_UsingParentLDrawRef(LDrawRef);
                
                // ** ADD NEW PART AND SUB PART(s) IN REQUIRED QTY's **                
                int qty = int.Parse(fldQty.Text);
                for (int a = 0; a < qty; a++)
                {
                    #region ** GENERATE NEW PART **
                    Part newPart = new Part();
                    newPart.LDrawRef = LDrawRef;
                    newPart.LDrawColourID = LDrawColourID;
                    newPart.SubSetRef = parentSubSetRef + "|" + SubSetIndex;                    
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
                    //SubPartMappingCollection coll = StaticData.GetSubPartMappingData_UsingParentLDrawRef(LDrawRef);
                    foreach (SubPartMapping compPart in coll.SubPartMappingList)
                    {
                        // ** Generate subPart and add to SubPartList **
                        Part subPart = new Part();
                        subPart.LDrawRef = compPart.SubPartLDrawRef;
                        subPart.LDrawColourID = compPart.LDrawColourID;
                        subPart.SubSetRef = parentSubSetRef + "|" + SubSetIndex;
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

                    // ** ADD NEW PART XML NODE **    
                    string xmlNodeString = BaseClasses.HelperFunctions.RemoveAllNamespaces(newPart.SerializeToString(true));
                    XmlDocument doc = new XmlDocument();
                    doc.LoadXml(xmlNodeString);
                    XmlNode newNode = doc.DocumentElement;
                    XmlNode importNode = parentNode.OwnerDocument.ImportNode(newNode, true);                    
                    parentNode.AppendChild(importNode);
                }

                // ** Update PartList **                
                RecalculatePartList(currentSetXml);

                // ** CLEAR UP **
                ClearPartSummaryFields();
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
                if (partType.Equals("COMPOSITE") && StaticData.CheckIfSubPartMappingPartsExist(LDrawRef) == false)
                {
                    throw new Exception("SubParts for Part not found in Composite Part Collection");
                }
                #endregion

                // ** GET VARIABLES **
                //string pureStepNo = fldPureStepNo.Text;
                //string parentSubSetRef = fldStepParentSubSetRef.Text;
                //string parentSubModelRef = fldStepParentModelRef.Text;
                //int nodeIndex = (int)dgPartSummary.SelectedRows[0].Cells["Step Node Index"].Value;
                //string pureStepNo = dgPartSummary.SelectedCells[0].OwningRow.Cells["Step No"].Value.ToString();
                string PartSubSetRef = dgPartSummary.SelectedCells[0].OwningRow.Cells["SubSet Ref"].Value.ToString();
                float posX = 0; if (fldPartPosX.Text != "") posX = float.Parse(fldPartPosX.Text);
                float posY = 0; if (fldPartPosY.Text != "") posY = float.Parse(fldPartPosY.Text);
                float posZ = 0; if (fldPartPosZ.Text != "") posZ = float.Parse(fldPartPosZ.Text);
                float rotX = 0; if (fldPartRotX.Text != "") rotX = float.Parse(fldPartRotX.Text);
                float rotY = 0; if (fldPartRotY.Text != "") rotY = float.Parse(fldPartRotY.Text);
                float rotZ = 0; if (fldPartRotZ.Text != "") rotZ = float.Parse(fldPartRotZ.Text);

                // ** GET OLD PART NODE **                
                //string xmlString = "";
                //if (chkShowSubParts.Checked)
                //{
                //    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + nodeIndex + "]";
                //}
                //else
                //{
                //    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + nodeIndex + "]";
                //}
                string xmlString = "//Part[@SubSetRef='" + PartSubSetRef + "']";
                XmlNode oldNode = currentSetXml.SelectSingleNode(xmlString);
                string oldSubSetRef = oldNode.SelectSingleNode("@SubSetRef").InnerXml;
                
                #region ** GENERATE NEW PART **
                Part newPart = new Part();
                newPart.LDrawRef = LDrawRef;
                newPart.LDrawColourID = LDrawColourID;                
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
                SubPartMappingCollection coll = StaticData.GetSubPartMappingData_UsingParentLDrawRef(LDrawRef);
                foreach (SubPartMapping compPart in coll.SubPartMappingList)
                {
                    // ** Generate subPart and add to SubPartList **
                    Part subPart = new Part();                    
                    subPart.LDrawRef = compPart.SubPartLDrawRef;
                    subPart.LDrawColourID = compPart.LDrawColourID;                    
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
                
                // ** TIDY UP **
                ClearPartSummaryFields();                
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
                //string pureStepNo = fldPureStepNo.Text;
                //string parentSubSetRef = fldStepParentSubSetRef.Text;
                //string parentSubModelRef = fldStepParentModelRef.Text;                
                //int nodeIndex = (int)dgPartSummary.SelectedRows[0].Cells["Step Node Index"].Value;
                string PartSubSetRef = dgPartSummary.SelectedCells[0].OwningRow.Cells["SubSet Ref"].Value.ToString();

                #region ** REMOVE DELETED NODE FROM XML **                
                //string xmlString = "";
                //if (chkShowSubParts.Checked)
                //{                    
                //    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']//Part)[" + nodeIndex + "]";
                //}
                //else
                //{                    
                //    xmlString = "(//SubSet[@Ref='" + parentSubSetRef + "']//SubModel[@Ref='" + parentSubModelRef + "']//Step[@PureStepNo='" + pureStepNo + "']/./Part)[" + nodeIndex + "]";
                //}
                string xmlString = "//Part[@SubSetRef='" + PartSubSetRef + "']";
                XmlNode removalNode = currentSetXml.SelectSingleNode(xmlString);
                XmlNode parentNode = removalNode.ParentNode;
                parentNode.RemoveChild(removalNode);
                #endregion

                // ** UPDATE PartList & UnityRefs **                
                RecalculatePartList(currentSetXml);
                
                // ** TIDY UP **
                ClearPartSummaryFields();
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
               
        private void AddPartToBasePartCollection()
        {            
            try
            {
                // ** VALIDATION **
                if (fldLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");                               
                if (fldPartType.Text.Equals("")) throw new Exception("No Part Type selected...");                
                if (fldLDrawRef.Text.Contains("c") && fldPartType.Text.Equals("BASIC"))
                {
                    // ** Make sure user wants to create strange part **
                    DialogResult res = MessageBox.Show("The part contains 'c' - do you really want to create a BASIC part and not a COMPOSITE one?", "BASIC Part Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No) return;                    
                }
                int LDrawSize = 0;
                int.TryParse(fldLDrawSize.Text, out LDrawSize);

                // ** Add Part and all related stuff **
                string response = StaticData.AddPartToBasePartCollection(fldLDrawRef.Text, fldPartType.Text, LDrawSize, chkIsSticker.Checked, chkIsLargeModel.Checked);
                if (response != "") throw new Exception(response);

                // ** Refresh Part Details ** 
                ProcessLDrawRef_Leave();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

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

                // ** Show new form **
                InstructionViewer form = new InstructionViewer("");
                form.Visible = true;
                form.fldCurrentSetRef.Text = MiniFigRef;
                form.LoadSet();
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
                //btnDeleteSet.Enabled = value;
                btnOpenSetInstructions.Enabled = value;
                btnOpenSetURLs.Enabled = value;
                chkShowSubParts.Enabled = value;
                chkShowPages.Enabled = value;
                tabControl1.Enabled = value;
            }
        }

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

        private void dgPartListSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Bitmap image = (Bitmap)dgPartListSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgPartListWithMFsSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Bitmap image = (Bitmap)dgPartListWithMFsSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgMiniFigsPartListSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Bitmap image = (Bitmap)dgMiniFigsPartListSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }
    
        private void PostHeader()
        {
            string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
            this.Text = "Instruction Viewer";
            this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
        }

        private void ApplyModeSettings()
        {  
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => ApplyModeSettings()));
            }
            else
            {
                if (mode.Equals("EDIT"))
                {
                    // ** Post header **            
                    PostHeader();

                    // ** Disable buttons **
                    btnSaveSet.Enabled = true;
                    gpSet.Enabled = true;
                    gpSubSet.Enabled = true;
                    gpModel.Enabled = true;
                    gpSubModel.Enabled = true;
                    gpStep.Enabled = true;
                    btnPartAdd.Enabled = true;
                    btnPartSave.Enabled = true;
                    btnPartDelete.Enabled = true;
                }
                else if (mode.Equals("READ-ONLY"))
                {
                    // ** Post header **            
                    PostHeader();
                    this.Text += " |  #### READ-ONLY ####";

                    // ** Disable buttons **
                    btnSaveSet.Enabled = false;
                    gpSet.Enabled = false;
                    gpSubSet.Enabled = false;
                    gpModel.Enabled = false;
                    gpSubModel.Enabled = false;
                    gpStep.Enabled = false;
                    btnPartAdd.Enabled = false;
                    btnPartSave.Enabled = false;
                    btnPartDelete.Enabled = false;
                }
            }
        }

        private void GenerateDATFile()
        {
            try
            {
                // ** Validation **                
                if (fldLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");
                string LDrawRef = fldLDrawRef.Text;
                
                // ** Generate .DAT files using API **
                List<string> LDrawRefList = StaticData.GenerateDATFiles_ForLDrawDetails(LDrawRef);

                // ** SHOW CONFIRMATION **
                string confirmation = "Created the following .DAT file(s) for " + LDrawRef + ":" + Environment.NewLine;                
                foreach (string Ref in LDrawRefList) confirmation += Ref + Environment.NewLine;                
                MessageBox.Show(confirmation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ** REFRESH UNITY SUBMODEL FUNCTIONS **

        private DataTable dgSetSubModelPartSummaryTable_Orig;
        private DataTable dgUnitySubModelPartSummaryTable_Orig;

        private void RefreshSetSubModels()
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => RefreshSetSubModels()));
            }
            else
            {
                try
                {
                    // ** Validation **
                    if (fldCurrentSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                    string SetRef = fldCurrentSetRef.Text;

                    // ** Clear existing data **
                    tvSetSubModels.Nodes.Clear();
                    //Delegates.TreeView_ClearNodes(this, tvSetSubModels);
                    gpUnitySubModelParts.Text = "Unity SubModel Parts";
                    dgSetSubModelPartSummary.DataSource = null;
                    dgUnitySubModelPartSummary.DataSource = null;
                    lblSetSubModelPartCount.Text = "";
                    lblUnitySubModelPartCount.Text = "";

                    // ** Populate Summary Treeview with data **
                    //watch.Reset(); watch.Start();
                    //Delegates.ToolStripLabel_SetText(this, lblStatus, "Refreshing - Generating Treeview...");
                    Delegates.TreeView_AddNodes(this, tvSetSubModels, Set.GetSetTreeViewFromSetXML(currentSetXml, false, false, false, false));
                    //watch.Stop(); perfLog += "Populate Summary Treeview with data:\t" + watch.ElapsedMilliseconds + "ms" + Environment.NewLine;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void tvSetSubModels_AfterSelect(object sender, TreeViewEventArgs e)
        {
            // ** Reset fields **
            gpUnitySubModelParts.Text = "Unity SubModel Parts";
            dgSetSubModelPartSummary.DataSource = null;
            dgUnitySubModelPartSummary.DataSource = null;
            lblSetSubModelPartCount.Text = "";
            lblUnitySubModelPartCount.Text = "";

            // ** Determine type of model selected **
            string Type = tvSetSubModels.SelectedNode.Tag.ToString().Split('|')[0];
            if (Type.Equals("MODEL") || Type.Equals("SUBMODEL"))
            {

                // ** Get variables **
                string SubSetRef = tvSetSubModels.SelectedNode.Tag.ToString().Split('|')[1];
                string ModelRef = tvSetSubModels.SelectedNode.Tag.ToString().Split('|')[2];
                string SubModelLevel = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@SubModelLevel").InnerXml;

                // ** Get Set SubModel data **
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/*/Part";
                //xmlString += "[@IsSubPart='false']";
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part[parent::SubModel/@Ref='" + ModelRef + "']";
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//Part[parent::SubModel/@Ref='" + ModelRef + "']";
                string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//Part[ancestor::SubModel/@Ref='" + ModelRef + "' and ancestor::Step/@StepLevel='" + SubModelLevel + "']";
                //XmlNodeList partNodeList = fullSetXml.SelectNodes(xmlString);
                XmlNodeList partNodeList = currentSetXml.SelectNodes(xmlString);
                dgSetSubModelPartSummaryTable_Orig = StaticData.GeneratePartTable(partNodeList, true, true, false);

                // ** Get Unity SubModel data **
                string UnitySubModelRef = SubSetRef + "." + ModelRef;
                SetInstructions si = StaticData.GetSetInstructions(UnitySubModelRef);
                XmlNodeList UnitypartNodeList = null;
                if (si != null)
                {
                    XmlDocument UnitySubModelXML = new XmlDocument();
                    UnitySubModelXML.LoadXml(si.Data);
                    xmlString = "//Part";
                    UnitypartNodeList = UnitySubModelXML.SelectNodes(xmlString);
                    dgUnitySubModelPartSummaryTable_Orig = GenerateStepPartTable(UnitypartNodeList);
                }
                dgUnitySubModelPartSummaryTable_Orig = StaticData.GeneratePartTable(UnitypartNodeList, true, true, false);

                // ** Run matching **                
                dgSetSubModelPartSummaryTable_Orig.Columns.Add("Matched", typeof(bool));
                foreach (DataRow row in dgSetSubModelPartSummaryTable_Orig.Rows) row["Matched"] = false;
                dgUnitySubModelPartSummaryTable_Orig.Columns.Add("Matched", typeof(bool));
                foreach (DataRow row in dgUnitySubModelPartSummaryTable_Orig.Rows) row["Matched"] = false;
                for (int a = 0; a < dgSetSubModelPartSummaryTable_Orig.Rows.Count; a++)
                {
                    if (a >= dgUnitySubModelPartSummaryTable_Orig.Rows.Count) break;

                    // ** Check variables **
                    string set_LDrawRef = (string)dgSetSubModelPartSummaryTable_Orig.Rows[a]["LDraw Ref"];
                    string unity_LDrawRef = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["LDraw Ref"];
                    string set_LDrawColourID = dgSetSubModelPartSummaryTable_Orig.Rows[a]["LDraw Colour ID"].ToString();
                    string unity_LDrawColourID = dgUnitySubModelPartSummaryTable_Orig.Rows[a]["LDraw Colour ID"].ToString();
                    bool IsSubPart = (bool)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["Is SubPart"];
                    if (set_LDrawRef.Equals(unity_LDrawRef))
                    {
                        if (IsSubPart == true)
                        {
                            dgSetSubModelPartSummaryTable_Orig.Rows[a]["Matched"] = true;
                            dgUnitySubModelPartSummaryTable_Orig.Rows[a]["Matched"] = true;
                        }
                        else
                        {
                            if (set_LDrawColourID.Equals(unity_LDrawColourID))
                            {
                                dgSetSubModelPartSummaryTable_Orig.Rows[a]["Matched"] = true;
                                dgUnitySubModelPartSummaryTable_Orig.Rows[a]["Matched"] = true;
                            }
                        }
                    }
                }

                // ** Show Set summary data **
                dgSetSubModelPartSummary.DataSource = dgSetSubModelPartSummaryTable_Orig;
                AdjustSubModelMatchingSummaryRowFormatting(dgSetSubModelPartSummary);
                lblSetSubModelPartCount.Text = dgSetSubModelPartSummaryTable_Orig.Rows.Count.ToString("#,##0") + " Part(s)";

                // ** Show Unity summary data **
                gpUnitySubModelParts.Text = "Unity SubModel Parts | " + UnitySubModelRef;
                dgUnitySubModelPartSummary.DataSource = dgUnitySubModelPartSummaryTable_Orig;
                AdjustSubModelMatchingSummaryRowFormatting(dgUnitySubModelPartSummary);
                lblUnitySubModelPartCount.Text = dgUnitySubModelPartSummary.Rows.Count.ToString("#,##0") + " Part(s)";
            }
        }

        private void AdjustSubModelMatchingSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustSubModelMatchingSummaryRowFormatting(dg)));
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
                }

                // ** Hide certain columns **
                List<string> ColumnList = new List<string>() {"Is Official", "Unity FBX", "Base Part Collection", "FBX Count", "FBX Size", "Step Node Index", "LDraw Part Type", "LDraw Description" };
                foreach(string ColumnName in ColumnList) dg.Columns[ColumnName].Visible = false;

                // ** Change colours of rows **
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.Cells["Matched"].Value.ToString().ToUpper().Equals("FALSE"))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                }
            }
        }

        #endregion

        private void SyncSubModelPositions()
        {
            try
            {
                // ** Validations **
                if (tvSetSubModels.SelectedNode == null) throw new Exception("No Model or SubModel node selected...");               
                if(dgUnitySubModelPartSummaryTable_Orig.Rows.Count == 0) throw new Exception("Unity SubModel has no parts...");

                // Check whether all parts between the models match
                //foreach(DataRow row in dgSetSubModelPartSummaryTable_Orig.Rows)
                //{
                //    bool matched = (bool)row["Matched"];
                //    if(matched == false) throw new Exception("Not all parts match...");               
                //}

                // ** Get variables **
                string SubSetRef = tvSetSubModels.SelectedNode.Tag.ToString().Split('|')[1];
                string ModelRef = tvSetSubModels.SelectedNode.Tag.ToString().Split('|')[2];
                string SubModelLevel = currentSetXml.SelectSingleNode("//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/@SubModelLevel").InnerXml;
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']/*/Part";
                //string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//SubModel[@Ref='" + ModelRef + "']//Part";
                //xmlString += "[@IsSubPart='false']";
                string xmlString = "//SubSet[@Ref='" + SubSetRef + "']//Part[ancestor::SubModel/@Ref='" + ModelRef + "' and ancestor::Step/@StepLevel='" + SubModelLevel + "']";

                // ** Get Set SubModel Parts **
                XmlNodeList SetSubModelPartNodeList = currentSetXml.SelectNodes(xmlString);

                // ** Update Set XML **
                int PartCount = 0;
                int SubPartCount = 0;
                int IgnoredPartCount = 0;                
                for (int a = 0; a < dgSetSubModelPartSummaryTable_Orig.Rows.Count; a++)
                {
                    string Set_LDrawRef = (string)dgSetSubModelPartSummaryTable_Orig.Rows[a]["LDraw Ref"];
                    int Set_LDrawColourID = (int)dgSetSubModelPartSummaryTable_Orig.Rows[a]["LDraw Colour ID"];
                    bool IsSubPart = (bool)dgSetSubModelPartSummaryTable_Orig.Rows[a]["Is SubPart"];
                    bool Matched = (bool)dgSetSubModelPartSummaryTable_Orig.Rows[a]["Matched"];
                    if(Matched == false)
                    {
                        IgnoredPartCount += 1;
                        continue;
                    }
                    else
                    {
                        if (IsSubPart == false)
                        {
                            PartCount += 1;
                        }
                        else
                        {
                            SubPartCount += 1;
                        }
                        string PosX = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["PosX"];
                        string PosY = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["PosY"];
                        string PosZ = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["PosZ"];
                        string RotX = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["RotX"];
                        string RotY = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["RotY"];
                        string RotZ = (string)dgUnitySubModelPartSummaryTable_Orig.Rows[a]["RotZ"];
                        SetSubModelPartNodeList[a].SelectSingleNode("@PosX").InnerXml = PosX;
                        SetSubModelPartNodeList[a].SelectSingleNode("@PosY").InnerXml = PosY;
                        SetSubModelPartNodeList[a].SelectSingleNode("@PosZ").InnerXml = PosZ;
                        SetSubModelPartNodeList[a].SelectSingleNode("@RotX").InnerXml = RotX;
                        SetSubModelPartNodeList[a].SelectSingleNode("@RotY").InnerXml = RotY;
                        SetSubModelPartNodeList[a].SelectSingleNode("@RotZ").InnerXml = RotZ;
                    }
                }

                // ** Refresh & Tidy Up **
                RefreshSetSubModels();
                MessageBox.Show("Successfully updated " + PartCount + " Part(s) and " + SubPartCount + " Sub Part(s). " + IgnoredPartCount + " Part(s) were ignored...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void fldSetRecent_DropDownItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            HandleRecentSetClick(e);
        }

        private void HandleRecentSetClick(ToolStripItemClickedEventArgs e)
        {
            fldCurrentSetRef.Text = e.ClickedItem.Text.Split('|')[0];
            LoadSet();
        }





        private void btnUnitySubModelsCollapseNodes_Click(object sender, EventArgs e)
        {
            if (tvSetSubModels.Nodes.Count > 0) tvSetSubModels.Nodes[0].Collapse(false);
        }

        private void btnUnitySubModelsExpandAll_Click(object sender, EventArgs e)
        {
            if (tvSetSubModels.Nodes.Count > 0) tvSetSubModels.Nodes[0].ExpandAll();
        }





    }






}
