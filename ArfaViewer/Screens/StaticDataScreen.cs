using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using System.Runtime.Serialization.Json;
using System.Xml.Linq;
using System.Xml;
using BaseClasses;
using ScintillaNET;
using System.IO;
using Azure.Storage.Files.Shares;
using Org.BouncyCastle.Ocsp;
using Azure.Storage.Files.Shares.Models;

namespace Generator
{
    public partial class StaticDataScreen : Form
    {
        private Scintilla LDrawDetailsData = new Scintilla();
        private Scintilla SubPartMappingData = new Scintilla();
        private Scintilla FilesDatData = new Scintilla();
        private DataTable dgBasePartSummaryTable_Orig;
        private DataTable dgLDrawDetailsSummaryTable_Orig;
        private DataTable dgSubPartMappingSummaryTable_Orig;
        private DataTable dgFilesDatSummaryTable_Orig;
        private DataTable dgFilesFbxSummaryTable_Orig;
        private DataTable dgFilesUnityFbxSummaryTable_Orig;


        public StaticDataScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Static Data";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];

                #region ** FORMAT SUMMARIES **
                String[] DGnames = new string[] { "dgBasePartSummary", "dgLDrawDetailsSummary", "dgSubPartMappingSummary", "dgFilesDatSummary", "dgFilesFbxSummary", "dgFilesUnityFbxSummary" };
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
                lblBasePartStatus.Text = "";
                lblBasePartCount.Text = "";
                lblBasePartSummaryItemFilteredCount.Text = "";
                lblLDrawDetailsStatus.Text = "";
                lblLDrawDetailsCount.Text = "";
                lblLDrawDetailsSummaryItemFilteredCount.Text = "";
                lblSubPartMappingStatus.Text = "";
                lblSubPartMappingCount.Text = "";
                lblSubPartMappingSummaryItemFilteredCount.Text = "";
                lblFilesDatStatus.Text = "";
                lblFilesDatCount.Text = "";
                lblFilesDatSummaryItemFilteredCount.Text = "";
                lblFilesFbxStatus.Text = "";
                lblFilesFbxCount.Text = "";
                lblFilesFbxSummaryItemFilteredCount.Text = "";
                lblFilesUnityFbxStatus.Text = "";
                lblFilesUnityFbxCount.Text = "";
                lblFilesUnityFbxSummaryItemFilteredCount.Text = "";
                #endregion

                #region ** ADD BASEPART SUMMARY TOOLSTRIP ITEMS **
                toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {                   
                    btnRefreshAll,
                    toolStripSeparator23,
                    new ToolStripControlHost(chkShowPartImages)
                });
                #endregion

                #region ** ADD BASEPART HEADER TOOLSTRIP ITEMS **
                tsBasePartHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnBasePartRefresh,
                    toolStripSeparator1,
                    btnBasePartSummaryCopyToClipboard,
                    toolStripSeparator7,
                    lblBasePartLDrawRefAc,
                    new ToolStripControlHost(chkBasePartLDrawRefAcEquals),
                    fldBasePartLDrawRefAc,
                    new ToolStripControlHost(chkLockLDrawRef),
                    
                    lblBasePartLDrawDescriptionAc,
                    new ToolStripControlHost(chkBasePartLDrawDescriptionAcEquals),
                    fldBasePartLDrawDescriptionAc,

                    lblBasePartPartTypeAc,
                    new ToolStripControlHost(chkBasePartPartTypeAcEquals),
                    fldBasePartPartTypeAc,
                    lblBasePartOffsetXAc,
                    new ToolStripControlHost(chkBasePartOffsetXAcEquals),
                    fldBasePartOffsetXAc,

                });
                #endregion

                #region ** ADD BASEPART SUMMARY TOOLSTRIP ITEMS **
                tsBasePartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] 
                {
                    lblBasePartLDrawRef,
                    fldBasePartLDrawRef,
                    fldBasePartLDrawImage,
                    lblBasePartLDrawDescription,
                    fldBasePartLDrawDescription,
                    lblBasePartLDrawSize,
                    fldBasePartLDrawSize,
                    lblBasePartPartType,
                    fldBasePartPartType,
                    lblBasePartLDrawPartType,
                    fldBasePartLDrawPartType,
                    new ToolStripControlHost(chkBasePartIsSubPart),
                    new ToolStripControlHost(chkBasePartIsSticker),
                    new ToolStripControlHost(chkBasePartIsLargeModel),
                    lblBasePartOffsetX,
                    fldBasePartOffsetX,
                    lblBasePartOffsetY,
                    fldBasePartOffsetY,
                    lblBasePartOffsetZ,
                    fldBasePartOffsetZ,
                    lblBasePartSubPartCount,
                    fldBasePartSubPartCount,
                    toolStripSeparator15,                    
                    btnBasePartClear,
                    toolStripSeparator3,                    
                    btnBasePartSave,
                    btnBasePartDelete
                });
                #endregion

                #region ** ADD LDRAWDETAILS HEADER TOOLSTRIP ITEMS **
                tsLDrawDetailsHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnLDrawDetailsRefresh,
                    toolStripSeparator4,
                    btnLDrawDetailsSummaryCopyToClipboard,
                    toolStripSeparator7,
                    lblLDrawDetailsLDrawRefAc,
                    new ToolStripControlHost(chkLDrawDetailsLDrawRefAcEquals),
                    fldLDrawDetailsLDrawRefAc,
                    lblLDrawDetailsLDrawDescriptionAc,
                    new ToolStripControlHost(chkLDrawDetailsLDrawDescriptionAcEquals),
                    fldLDrawDetailsLDrawDescriptionAc,                    
                });
                #endregion

                #region ** ADD SUB PART MAPPING HEADER TOOLSTRIP ITEMS **
                tsSubPartMappingHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnSubPartMappingRefresh,
                    toolStripSeparator9,
                    btnSubPartMappingSummaryCopyToClipboard,
                    toolStripSeparator10,
                    lblSubPartMappingParentLDrawRefAc,                    
                    new ToolStripControlHost(chkSubPartMappingParentLDrawRefAcEquals),
                    fldSubPartMappingParentLDrawRefAc
                });
                #endregion

                #region ** ADD FILES DAT HEADER TOOLSTRIP ITEMS **
                tsFilesDatHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnFilesDatRefresh,
                    toolStripSeparator14,
                    btnFilesDatSummaryCopyToClipboard,
                    toolStripSeparator16,
                    lblFilesDatFilenameAc,
                    new ToolStripControlHost(chkFilesDatFilenameAcEquals),
                    fldFilesDatFilenameAc,
                    new ToolStripControlHost(chkFilesDatLock),
                });
                #endregion

                #region ** ADD FILES FBX HEADER TOOLSTRIP ITEMS **
                tsFilesFbxHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnFilesFbxRefresh,
                    toolStripSeparator17,
                    btnFilesFbxSummaryCopyToClipboard,
                    toolStripSeparator18,
                    lblFilesFbxFilenameAc,
                    new ToolStripControlHost(chkFilesFbxFilenameAcEquals),
                    fldFilesFbxFilenameAc,                    
                });
                #endregion

                #region ** ADD FILES FBX HEADER TOOLSTRIP ITEMS **
                tsFilesUnityFbxHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnFilesUnityFbxRefresh,
                    toolStripSeparator19,
                    btnFilesUnityFbxSummaryCopyToClipboard,
                    toolStripSeparator20,
                    lblFilesUnityFbxFilenameAc,
                    new ToolStripControlHost(chkFilesUnityFbxFilenameAcEquals),
                    fldFilesUnityFbxFilenameAc,
                });
                #endregion

                #region ** ADD PART DETAILS HEADER TOOLSTRIP ITEMS **
                tsPartDetails2.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    lblPartType,
                    fldPartType,
                    lblLDrawSize,
                    fldLDrawSize,
                    new ToolStripControlHost(chkIsSubPart),
                    new ToolStripControlHost(chkIsSticker),
                    new ToolStripControlHost(chkIsLargeModel)

                });
                #endregion

                // ** Set up Scintilla **               
                pnlLDrawDetailsData.Controls.Add(LDrawDetailsData);
                ApplyDefaultScintillaStyles(LDrawDetailsData);
                pnlSubPartMappingData.Controls.Add(SubPartMappingData);
                ApplyDefaultScintillaStyles(SubPartMappingData);
                pnlFilesDatData.Controls.Add(FilesDatData);
                ApplyDefaultScintillaStyles(FilesDatData);

                // ** Populate all PartType related dropdowns **
                PopulatePartType_Dropdowns();
            }
            catch (Exception ex)
            {                
                MessageBox.Show(ex.Message);
            }
        }

        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnRefreshAll_Click(object sender, EventArgs e)
        {
            RefreshBasePart();
            RefreshLDrawDetails();
            RefreshSubPartMapping();
        }

        private void btnBasePartRefresh_Click(object sender, EventArgs e)
        {
            RefreshBasePart();
        }

        private void btnLDrawDetailsRefresh_Click(object sender, EventArgs e)
        {
            RefreshLDrawDetails();
        }

        private void btnSubPartMappingRefresh_Click(object sender, EventArgs e)
        {
            RefreshSubPartMapping();
        }

        private void fldBasePartLDrawRef_Leave(object sender, EventArgs e)
        {
            fldBasePartLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { fldBasePartLDrawRef.Text });
        }

        private void fldLDrawDetailsLDrawRef_Leave(object sender, EventArgs e)
        {
            fldLDrawDetailsLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { fldLDrawDetailsLDrawRef.Text });
        }

        private void fldBasePartLDrawImage_Click(object sender, EventArgs e)
        {
            ShowPart(fldBasePartLDrawRef.Text, fldBasePartLDrawImage.Image);
        }

        private void fldLDrawDetailsLDrawImage_Click(object sender, EventArgs e)
        {
            ShowPart(fldLDrawDetailsLDrawRef.Text, fldLDrawDetailsLDrawImage.Image);
        }

        private void btnBasePartClear_Click(object sender, EventArgs e)
        {
            BasePart_Clear();
        }

        private void btnLDrawDetailsClear_Click(object sender, EventArgs e)
        {
            LDrawDetails_Clear();
        }

        private void btnSubPartMappingClear_Click(object sender, EventArgs e)
        {
            SubPartMapping_Clear();
        }

        private void btnBasePartSave_Click(object sender, EventArgs e)
        {
            BasePart_Save();
        }

        private void btnBasePartDelete_Click(object sender, EventArgs e)
        {
            BasePart_Delete();
        }

        private void chkLockLDrawRef_CheckedChanged(object sender, EventArgs e)
        {
            if (chkLockLDrawRef.Checked)
            {
                fldLDrawDetailsLDrawRefAc.Text = fldBasePartLDrawRefAc.Text;
                fldSubPartMappingParentLDrawRefAc.Text = fldBasePartLDrawRefAc.Text;
            }
        }

        private void fldBasePartLDrawRefAc_TextChanged(object sender, EventArgs e)
        {
            ProcessBasePartSummaryFilter();
            if (chkLockLDrawRef.Checked)
            {
                fldLDrawDetailsLDrawRefAc.Text = fldBasePartLDrawRefAc.Text;
                fldSubPartMappingParentLDrawRefAc.Text = fldBasePartLDrawRefAc.Text;
            }
        }

        private void fldBasePartLDrawDescriptionAc_TextChanged(object sender, EventArgs e)
        {
            ProcessBasePartSummaryFilter();
        }

        private void fldBasePartPartTypeAc_TextChanged(object sender, EventArgs e)
        {
            ProcessBasePartSummaryFilter();
        }

        private void fldBasePartOffsetXAc_TextChanged(object sender, EventArgs e)
        {
            ProcessBasePartSummaryFilter();
        }

        private void fldLDrawDetailsLDrawRefAc_TextChanged(object sender, EventArgs e)
        {
            ProcessLDrawDetailsSummaryFilter();
        }

        private void fldLDrawDetailsLDrawDescriptionAc_TextChanged(object sender, EventArgs e)
        {
            ProcessLDrawDetailsSummaryFilter();
        }

        private void fldSubPartMappingParentLDrawRefAc_TextChanged(object sender, EventArgs e)
        {
            ProcessSubPartMappingSummaryFilter();
        }

        private void btnFilesDatRefresh_Click(object sender, EventArgs e)
        {
            RefreshFilesDat();
        }

        private void btnFBXRefreshAll_Click(object sender, EventArgs e)
        {
            RefreshFilesDat();
            RefreshFilesFbx();
            RefreshFilesUnityFbx();
        }

        private void btnFilesFbxRefresh_Click(object sender, EventArgs e)
        {
            RefreshFilesFbx();
        }

        private void btnFilesUnityFbxRefresh_Click(object sender, EventArgs e)
        {
            RefreshFilesUnityFbx();
        }

        private void fldFilesDatFilenameAc_TextChanged(object sender, EventArgs e)
        {
            ProcessFilesDatSummaryFilter();
            if (chkFilesDatLock.Checked)
            {
                fldFilesFbxFilenameAc.Text = fldFilesDatFilenameAc.Text;
                fldFilesUnityFbxFilenameAc.Text = fldFilesDatFilenameAc.Text;
            }
        }

        private void fldFilesFbxFilenameAc_TextChanged(object sender, EventArgs e)
        {
            ProcessFilesFbxSummaryFilter();
        }

        private void chkFilesDatLock_CheckedChanged(object sender, EventArgs e)
        {
            if (chkFilesDatLock.Checked)
            {
                fldFilesFbxFilenameAc.Text = fldFilesDatFilenameAc.Text;
                fldFilesUnityFbxFilenameAc.Text = fldFilesDatFilenameAc.Text;
            }
        }

        private void fldFilesUnityFbxFilenameAc_TextChanged(object sender, EventArgs e)
        {
            ProcessFilesUnityFbxSummaryFilter();
        }

        private void fldLDrawRef_Leave(object sender, EventArgs e)
        {
            ProcessLDrawRef_Leave();
        }

        private void btnGenerateDatFile_Click(object sender, EventArgs e)
        {
            GenerateDATFile();
        }

        private void btnSyncFBXFiles_Click(object sender, EventArgs e)
        {
            SyncFBXFiles();
        }

        private void btnAddPartToBasePartCollection_Click(object sender, EventArgs e)
        {
            AddPartToBasePartCollection();
        }

        private void btnPartClear_Click(object sender, EventArgs e)
        {
            PartDetailsClear();
        }

        private void btnSubPartMappingDelete_Click(object sender, EventArgs e)
        {
            SubPartMapping_Delete();
        }

        private void btnSubPartMappingSave_Click(object sender, EventArgs e)
        {
            SubPartMapping_Save();
        }

        private void btnRefreshAllSummaries_Click(object sender, EventArgs e)
        {
            RefreshBasePart();
            RefreshLDrawDetails();
            RefreshSubPartMapping();
            RefreshFilesDat();
            RefreshFilesFbx();
            RefreshFilesUnityFbx();
        }

        private void btnLDrawDetailsSave_Click(object sender, EventArgs e)
        {
            LDrawDetails_Save();
        }

        private void btnLDrawDetailsDelete_Click(object sender, EventArgs e)
        {
            LDrawDetails_Delete();
        }

        #endregion

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

        private void PopulatePartType_Dropdowns()
        {
            List<BasePart.PartType> PartTypeList = Enum.GetValues(typeof(BasePart.PartType)).Cast<BasePart.PartType>().ToList();
            List<BasePart.LDrawPartType> LDrawPartTypeList = Enum.GetValues(typeof(BasePart.LDrawPartType)).Cast<BasePart.LDrawPartType>().ToList();

            fldBasePartPartType.Items.Clear();
            fldBasePartPartType.Text = "";
            foreach (var value in PartTypeList) fldBasePartPartType.Items.Add(value);
            fldBasePartLDrawPartType.Items.Clear();
            fldBasePartLDrawPartType.Text = "";
            foreach (var value in LDrawPartTypeList) fldBasePartLDrawPartType.Items.Add(value);

        }

        private void ShowPart(string LDrawRef, Image Image)
        {
            try
            {
                if (LDrawRef != "" && Image != null)
                {
                    PartViewer.image = (Bitmap)Image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #region ** REFRESH BASEPART FUNCTIONS **

        private BackgroundWorker bw_RefreshBasePart;

        private void EnableControls_RefreshBasePart(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshBasePart(value)));
            }
            else
            {
                btnExit.Enabled = value;
                //tsBasePartSummary.Enabled = value;
                //tsBasePartDetails.Enabled = value;
                gpBasePart.Enabled = value;
            }
        }

        private void RefreshBasePart()
        {
            try
            {
                EnableControls_RefreshBasePart(false);

                // ** CLEAR FIELDS ** 
                dgBasePartSummary.DataSource = null;                
                lblBasePartCount.Text = "";
                lblBasePartSummaryItemFilteredCount.Text = "";
                BasePart_Clear();
                fldBasePartLDrawRefAc.Text = "";
                fldBasePartLDrawDescriptionAc.Text = "";

                // ** Run background to process functions **
                bw_RefreshBasePart = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshBasePart.DoWork += new DoWorkEventHandler(bw_RefreshBasePart_DoWork);
                bw_RefreshBasePart.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshBasePart_RunWorkerCompleted);
                bw_RefreshBasePart.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshBasePart_ProgressChanged);
                bw_RefreshBasePart.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbBasePartStatus.Value = 0;
                EnableControls_RefreshBasePart(true);
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshBasePart_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbBasePartStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshBasePart_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbBasePartStatus, 0);               
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "");
                EnableControls_RefreshBasePart(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshBasePart_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get BasePart data **
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "Refreshing - Getting BasePart data...");
                BasePartCollection bpc = StaticData.GetBasePartData_All();
                DataTable table = BasePartCollection.GetDatatableFromBasePartCollection(bpc);
                
                // ** Enrich images onto data **
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "Refreshing - Enriching Part images...");
                Delegates.ToolStripProgressBar_SetMax(this, pbBasePartStatus, bpc.BasePartList.Count);
                int imageIndex = 0;
                table.Columns.Add("Part Image", typeof(Bitmap));
                table.Columns["Part Image"].SetOrdinal(0);
                if (chkShowPartImages.Checked)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bw_RefreshBasePart.ReportProgress(imageIndex, "Working...");
                        string LDrawRef = row["LDraw Ref"].ToString();
                        Bitmap PartImage = null;
                        if (LDrawRef.EndsWith("Torso") == false && LDrawRef.EndsWith("Legs") == false) PartImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                        row["Part Image"] = PartImage;
                        imageIndex += 1;
                    }
                }
                dgBasePartSummaryTable_Orig = table;
                Delegates.ToolStripProgressBar_SetValue(this, pbBasePartStatus, 0);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "Refreshing - Posting data to summary...");                
                Delegates.DataGridView_SetDataSource(this, dgBasePartSummary, table);                
                Delegates.ToolStripLabel_SetText(this, lblBasePartCount, table.Rows.Count.ToString("#,##0") + " Part(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "Refreshing - Formatting summary data...");                
                AdjustBasePartSummaryRowFormatting(dgBasePartSummary);
                Delegates.ToolStripLabel_SetText(this, lblBasePartStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdjustBasePartSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustBasePartSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;                
                dg.AutoResizeColumns();

                // ** Adjust row formatting **
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.Cells["Is Sub Part"].Value.ToString().ToUpper().Equals("TRUE"))
                    {
                        row.DefaultCellStyle.Font = new System.Drawing.Font(this.Font, FontStyle.Italic);
                        row.DefaultCellStyle.ForeColor = Color.Gray;
                    }
                    if (row.Cells["Part Type"].Value.ToString().ToUpper().Equals("COMPOSITE"))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
            }
        }

        #endregion

        #region ** REFRESH LDRAW DETAILS FUNCTIONS **

        private BackgroundWorker bw_RefreshLDrawDetails;

        private void EnableControls_RefreshLDrawDetails(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshLDrawDetails(value)));
            }
            else
            {
                btnExit.Enabled = value;
                //tsLDrawDetailsSummary.Enabled = value;
                //tsLDrawDetailsDetails.Enabled = value;
                gpLDrawDetails.Enabled = value;
            }
        }

        private void RefreshLDrawDetails()
        {
            try
            {
                EnableControls_RefreshLDrawDetails(false);

                // ** CLEAR FIELDS ** 
                dgLDrawDetailsSummary.DataSource = null;
                lblLDrawDetailsCount.Text = "";
                lblLDrawDetailsSummaryItemFilteredCount.Text = "";
                LDrawDetailsData.Text = "";
                LDrawDetails_Clear();
                fldLDrawDetailsLDrawRefAc.Text = "";
                fldLDrawDetailsLDrawDescriptionAc.Text = "";

                // ** Run background to process functions **
                bw_RefreshLDrawDetails = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshLDrawDetails.DoWork += new DoWorkEventHandler(bw_RefreshLDrawDetails_DoWork);
                bw_RefreshLDrawDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshLDrawDetails_RunWorkerCompleted);
                bw_RefreshLDrawDetails.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshLDrawDetails_ProgressChanged);
                bw_RefreshLDrawDetails.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbLDrawDetailsStatus.Value = 0;
                EnableControls_RefreshLDrawDetails(true);
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshLDrawDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbLDrawDetailsStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshLDrawDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbLDrawDetailsStatus, 0);                
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "");
                EnableControls_RefreshLDrawDetails(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshLDrawDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get data **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Getting LDrawDetails data...");
                LDrawDetailsCollection ldc = StaticData.GetLDrawDetailsData_All();
                DataTable table = LDrawDetailsCollection.GetDatatableFromLDrawDetailsCollection(ldc);

                // ** Enrich images onto data **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Enriching Part images...");
                Delegates.ToolStripProgressBar_SetMax(this, pbLDrawDetailsStatus, ldc.LDrawDetailsList.Count);
                int imageIndex = 0;
                table.Columns.Add("Part Image", typeof(Bitmap));
                table.Columns["Part Image"].SetOrdinal(0);
                if (chkShowPartImages.Checked)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bw_RefreshLDrawDetails.ReportProgress(imageIndex, "Working...");
                        string LDrawRef = row["LDraw Ref"].ToString();
                        Bitmap PartImage = null;
                        if (LDrawRef.EndsWith("Torso") == false && LDrawRef.EndsWith("Legs") == false) PartImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                        row["Part Image"] = PartImage;
                        imageIndex += 1;
                    }
                }
                dgLDrawDetailsSummaryTable_Orig = table;
                Delegates.ToolStripProgressBar_SetValue(this, pbLDrawDetailsStatus, 0);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgLDrawDetailsSummary, table);
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsCount, table.Rows.Count.ToString("#,##0") + " Part(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Formatting summary data...");
                AdjustLDrawDetailsSummaryRowFormatting(dgLDrawDetailsSummary);
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdjustLDrawDetailsSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustLDrawDetailsSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();

                // ** Adjust row formatting **
                foreach (DataGridViewRow row in dg.Rows)
                {
                    if (row.Cells["Part Type"].Value.ToString().ToUpper().Equals("COMPOSITE"))
                    {
                        row.DefaultCellStyle.ForeColor = Color.Blue;
                    }
                }
            }
        }

        #endregion

        #region ** REFRESH SUB PART MAPPING FUNCTIONS **

        private BackgroundWorker bw_RefreshSubPartMapping;

        private void EnableControls_RefreshSubPartMapping(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshSubPartMapping(value)));
            }
            else
            {
                btnExit.Enabled = value;                
                gpSubPartMapping.Enabled = value;
            }
        }

        private void RefreshSubPartMapping()
        {
            try
            {
                EnableControls_RefreshSubPartMapping(false);

                // ** CLEAR FIELDS ** 
                dgSubPartMappingSummary.DataSource = null;
                lblSubPartMappingCount.Text = "";
                lblSubPartMappingSummaryItemFilteredCount.Text = "";
                SubPartMappingData.Text = "";

                // ** Run background to process functions **
                bw_RefreshSubPartMapping = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshSubPartMapping.DoWork += new DoWorkEventHandler(bw_RefreshSubPartMappings_DoWork);
                bw_RefreshSubPartMapping.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshSubPartMapping_RunWorkerCompleted);
                bw_RefreshSubPartMapping.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshSubPartMapping_ProgressChanged);
                bw_RefreshSubPartMapping.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbSubPartMappingStatus.Value = 0;
                EnableControls_RefreshSubPartMapping(true);
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshSubPartMapping_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbSubPartMappingStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshSubPartMapping_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbSubPartMappingStatus, 0);
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "");
                EnableControls_RefreshSubPartMapping(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshSubPartMappings_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get data **
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "Refreshing - Getting SubPartMapping data...");
                SubPartMappingCollection coll = StaticData.GetSubPartMappingData_All();
                DataTable table = SubPartMappingCollection.GetDatatableFromSubPartMappingCollection(coll);

                // ** Enrich images onto data **
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "Refreshing - Enriching Part images...");
                Delegates.ToolStripProgressBar_SetMax(this, pbSubPartMappingStatus, coll.SubPartMappingList.Count);
                int imageIndex = 0;
                table.Columns.Add("Parent Part Image", typeof(Bitmap));
                table.Columns["Parent Part Image"].SetOrdinal(0);
                table.Columns.Add("Sub Part Image", typeof(Bitmap));
                table.Columns["Sub Part Image"].SetOrdinal(2);
                if (chkShowPartImages.Checked)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        bw_RefreshSubPartMapping.ReportProgress(imageIndex, "Working...");
                        string ParentLDrawRef = row["Parent LDraw Ref"].ToString();
                        string SubPartLDrawRef = row["Sub Part LDraw Ref"].ToString();

                        Bitmap ParentPartImage = null;
                        Bitmap SubPartImage = null;
                        if(ParentLDrawRef.EndsWith("Torso") == false && ParentLDrawRef.EndsWith("Legs") == false) ParentPartImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { ParentLDrawRef });                        
                        if (SubPartLDrawRef.EndsWith("Torso") == false && SubPartLDrawRef.EndsWith("Legs") == false) SubPartImage = ArfaImage.GetImage(ImageType.LDRAW, new string[] { SubPartLDrawRef });                       
                        row["Parent Part Image"] = ParentPartImage;
                        row["Sub Part Image"] = SubPartImage;

                        imageIndex += 1;
                    }
                }
                dgSubPartMappingSummaryTable_Orig = table;
                Delegates.ToolStripProgressBar_SetValue(this, pbSubPartMappingStatus, 0);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgSubPartMappingSummary, table);
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingCount, table.Rows.Count.ToString("#,##0") + " Mapping(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "Refreshing - Formatting summary data...");
                AdjustSubPartMappingSummaryRowFormatting(dgSubPartMappingSummary);
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdjustSubPartMappingSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustSubPartMappingSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Parent Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Parent Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.Columns["Sub Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Sub Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();

                // ** Adjust row formatting **
                //foreach (DataGridViewRow row in dg.Rows)
                //{
                //    if (row.Cells["Is Sub Part"].Value.ToString().ToUpper().Equals("TRUE"))
                //    {
                //        row.DefaultCellStyle.Font = new System.Drawing.Font(this.Font, FontStyle.Italic);
                //        row.DefaultCellStyle.ForeColor = Color.Gray;
                //    }
                //}
            }
        }

        #endregion

        #region ** REFRESH FILES DAT FUNCTIONS

        private BackgroundWorker bw_RefreshFilesDat;

        private void EnableControls_RefreshFilesDat(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshFilesDat(value)));
            }
            else
            {
                btnExit.Enabled = value;
                gpFilesDat.Enabled = value;
            }
        }

        private void RefreshFilesDat()
        {
            try
            {
                EnableControls_RefreshFilesDat(false);

                // ** CLEAR FIELDS ** 
                dgFilesDatSummary.DataSource = null;
                lblFilesDatCount.Text = "";
                lblFilesDatSummaryItemFilteredCount.Text = "";
                fldFilesDatFilenameAc.Text = "";
                FilesDatData.Text = "";

                // ** Run background to process functions **
                bw_RefreshFilesDat = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshFilesDat.DoWork += new DoWorkEventHandler(bw_RefreshFilesDat_DoWork);
                bw_RefreshFilesDat.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshFilesDat_RunWorkerCompleted);
                bw_RefreshFilesDat.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshFilesDat_ProgressChanged);
                bw_RefreshFilesDat.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbFilesDatStatus.Value = 0;
                EnableControls_RefreshFilesDat(true);
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesDat_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesDatStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesDat_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesDatStatus, 0);
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "");
                EnableControls_RefreshFilesDat(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesDat_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get all files in static-data/files-dat directory **
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "Refreshing - Getting all files from Azure...");
                FileDetailsCollection fdc = StaticData.GetFileDetailsData_FromContainer(@"static-data\files-dat");
                dgFilesDatSummaryTable_Orig = FileDetailsCollection.GetDatatableFromFileDetailsCollection(fdc);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgFilesDatSummary, dgFilesDatSummaryTable_Orig);
                Delegates.ToolStripLabel_SetText(this, lblFilesDatCount, fdc.FileDetailsList.Count.ToString("#,##0") + " file(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "Refreshing - Formatting summary data...");
                AdjustFBXSummaryRowFormatting(dgFilesDatSummary);
                Delegates.ToolStripLabel_SetText(this, lblFilesDatStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdjustFBXSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustFBXSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **
                dg.Columns["Size"].DefaultCellStyle.Format = "#,###";
                if (dg.Columns["Created TS"] != null) dg.Columns["Created TS"].DefaultCellStyle.Format = "dd-MMM-yy HH:mm:ss";
                if (dg.Columns["Last Updated TS"] != null) dg.Columns["Last Updated TS"].DefaultCellStyle.Format = "dd-MMM-yy HH:mm:ss";
                dg.AutoResizeColumns();
            }
        }

        #endregion

        #region ** REFRESH FILES FBX FUNCTIONS

        private BackgroundWorker bw_RefreshFilesFbx;

        private void EnableControls_RefreshFilesFbx(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshFilesFbx(value)));
            }
            else
            {
                btnExit.Enabled = value;
                gpFilesFbx.Enabled = value;
            }
        }

        private void RefreshFilesFbx()
        {
            try
            {
                EnableControls_RefreshFilesFbx(false);

                // ** CLEAR FIELDS ** 
                dgFilesFbxSummary.DataSource = null;
                lblFilesFbxCount.Text = "";
                lblFilesFbxSummaryItemFilteredCount.Text = "";
                fldFilesFbxFilenameAc.Text = "";

                // ** Run background to process functions **
                bw_RefreshFilesFbx = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshFilesFbx.DoWork += new DoWorkEventHandler(bw_RefreshFilesFbx_DoWork);
                bw_RefreshFilesFbx.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshFilesFbx_RunWorkerCompleted);
                bw_RefreshFilesFbx.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshFilesFbx_ProgressChanged);
                bw_RefreshFilesFbx.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbFilesFbxStatus.Value = 0;
                EnableControls_RefreshFilesFbx(true);
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesFbx_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesFbxStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesFbx_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesFbxStatus, 0);
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "");
                EnableControls_RefreshFilesFbx(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesFbx_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get all files in static-data/files-dat directory **
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "Refreshing - Getting all files from Azure...");
                FileDetailsCollection fdc = StaticData.GetFileDetailsData_FromContainer(@"static-data\files-fbx");
                dgFilesFbxSummaryTable_Orig = FileDetailsCollection.GetDatatableFromFileDetailsCollection(fdc);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgFilesFbxSummary, dgFilesFbxSummaryTable_Orig);
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxCount, fdc.FileDetailsList.Count.ToString("#,##0") + " file(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "Refreshing - Formatting summary data...");
                AdjustFBXSummaryRowFormatting(dgFilesFbxSummary);
                Delegates.ToolStripLabel_SetText(this, lblFilesFbxStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** REFRESH FILES FBX FUNCTIONS

        private BackgroundWorker bw_RefreshFilesUnityFbx;

        private void EnableControls_RefreshFilesUnityFbx(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshFilesUnityFbx(value)));
            }
            else
            {
                btnExit.Enabled = value;
                gpFilesUnityFbx.Enabled = value;
            }
        }

        private void RefreshFilesUnityFbx()
        {
            try
            {
                EnableControls_RefreshFilesUnityFbx(false);

                // ** CLEAR FIELDS ** 
                dgFilesUnityFbxSummary.DataSource = null;
                lblFilesUnityFbxCount.Text = "";
                lblFilesUnityFbxSummaryItemFilteredCount.Text = "";
                fldFilesUnityFbxFilenameAc.Text = "";

                // ** Run background to process functions **
                bw_RefreshFilesUnityFbx = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshFilesUnityFbx.DoWork += new DoWorkEventHandler(bw_RefreshFilesUnityFbx_DoWork);
                bw_RefreshFilesUnityFbx.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshFilesUnityFbx_RunWorkerCompleted);
                bw_RefreshFilesUnityFbx.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshFilesUnityFbx_ProgressChanged);
                bw_RefreshFilesUnityFbx.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbFilesUnityFbxStatus.Value = 0;
                EnableControls_RefreshFilesUnityFbx(true);
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesUnityFbx_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesUnityFbxStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesUnityFbx_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbFilesUnityFbxStatus, 0);
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "");
                EnableControls_RefreshFilesUnityFbx(true);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshFilesUnityFbx_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get all files in static-data/files-dat directory **
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "Refreshing - Getting all files from Azure...");
                FileDetailsCollection fdc = StaticData.GetFileDetailsData_FromLocalLocation(Global_Variables.UnityFBXLocation);
                dgFilesUnityFbxSummaryTable_Orig = FileDetailsCollection.GetDatatableFromFileDetailsCollection(fdc);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgFilesUnityFbxSummary, dgFilesUnityFbxSummaryTable_Orig);
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxCount, fdc.FileDetailsList.Count.ToString("#,##0") + " file(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "Refreshing - Formatting summary data...");
                AdjustFBXSummaryRowFormatting(dgFilesUnityFbxSummary);
                Delegates.ToolStripLabel_SetText(this, lblFilesUnityFbxStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** CELL CLICK FUNCTIONS **

        private void dgBasePartSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {                
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        var obj = dgBasePartSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        if (obj != DBNull.Value)
                        {
                            Bitmap image = (Bitmap)dgBasePartSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                            PartViewer.image = image;
                            PartViewer form = new PartViewer();
                            form.Visible = true;
                        }
                    }
                    else
                    {
                        // Get Set_Details for Set Ref              
                        string LDrawRef = dgBasePartSummary.Rows[e.RowIndex].Cells["LDraw Ref"].Value.ToString();
                        BasePart bp = StaticData.GetBasePart(LDrawRef);

                        // ** Post data to form **
                        fldBasePartLDrawRef.Text = bp.LDrawRef;
                        fldBasePartLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                        fldBasePartLDrawDescription.Text = bp.LDrawDescription;
                        fldBasePartLDrawSize.Text = "";
                        if (bp.LDrawSize > 0) fldBasePartLDrawSize.Text = bp.LDrawSize.ToString();
                        fldBasePartPartType.Text = bp.partType.ToString();
                        fldBasePartLDrawPartType.Text = bp.lDrawPartType.ToString();
                        chkBasePartIsSubPart.Checked = bp.IsSubPart;
                        chkBasePartIsSticker.Checked = bp.IsSticker;
                        chkBasePartIsLargeModel.Checked = bp.IsLargeModel;
                        fldBasePartOffsetX.Text = bp.OffsetX.ToString();
                        fldBasePartOffsetY.Text = bp.OffsetY.ToString();
                        fldBasePartOffsetZ.Text = bp.OffsetZ.ToString();
                        fldBasePartSubPartCount.Text = bp.SubPartCount.ToString();
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgLDrawDetailsSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        var obj = dgLDrawDetailsSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        if (obj != DBNull.Value)
                        {
                            Bitmap image = (Bitmap)obj;
                            PartViewer.image = image;
                            PartViewer form = new PartViewer();
                            form.Visible = true;
                        }
                    }
                    else
                    {
                        // Get LDrawDetails for LDraw Ref              
                        string LDrawRef = dgLDrawDetailsSummary.Rows[e.RowIndex].Cells["LDraw Ref"].Value.ToString();
                        LDrawDetails item = StaticData.GetLDrawDetails(LDrawRef);

                        // ** Post data to form **
                        fldLDrawDetailsLDrawRef.Text = item.LDrawRef;
                        fldLDrawDetailsLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { LDrawRef });
                        fldLDrawDetailsLDrawDescription.Text = item.LDrawDescription;
                        fldLDrawDetailsPartType.Text = item.PartType.ToString();
                        fldLDrawDetailsLDrawPartType.Text = item.LDrawPartType.ToString();
                        fldLDrawDetailsSubPartCount.Text = item.SubPartCount.ToString();
                        fldLDrawDetailsLDrawRefList.Text = String.Join(",", item.SubPartLDrawRefList);
                        LDrawDetailsData.Text = item.Data;
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgSubPartMappingSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {
                    if (e.ColumnIndex == 0)
                    {
                        var obj = dgSubPartMappingSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        if (obj != DBNull.Value)
                        {
                            Bitmap image = (Bitmap)obj;
                            PartViewer.image = image;
                            PartViewer form = new PartViewer();
                            form.Visible = true;
                        }
                    }
                    else
                    {
                        // Get SubPartMapping for Parent LDraw Ref & Sub Part LDraw Ref **
                        string ParentLDrawRef = dgSubPartMappingSummary.Rows[e.RowIndex].Cells["Parent LDraw Ref"].Value.ToString();
                        string SubPartLDrawRef = dgSubPartMappingSummary.Rows[e.RowIndex].Cells["Sub Part LDraw Ref"].Value.ToString();
                        SubPartMapping spm = StaticData.GetSubPartMapping(ParentLDrawRef, SubPartLDrawRef);
                        LDrawDetails ldd = StaticData.GetLDrawDetails(SubPartLDrawRef);

                        // ** Post data to form **
                        fldSubPartMappingParentLDrawRef.Text = spm.ParentLDrawRef;
                        fldSubPartMappingParentLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { ParentLDrawRef });
                        fldSubPartMappingSubPartLDrawRef.Text = spm.SubPartLDrawRef;
                        fldSubPartMappingSubPartLDrawImage.Image = ArfaImage.GetImage(ImageType.LDRAW, new string[] { SubPartLDrawRef });
                        fldSubPartMappingSubPartLDrawColourID.Text = spm.LDrawColourID.ToString();
                        fldSubPartMappingPosX.Text = spm.PosX.ToString();
                        fldSubPartMappingPosY.Text = spm.PosY.ToString();
                        fldSubPartMappingPosZ.Text = spm.PosZ.ToString();
                        fldSubPartMappingRotX.Text = spm.RotX.ToString();
                        fldSubPartMappingRotY.Text = spm.RotY.ToString();
                        fldSubPartMappingRotZ.Text = spm.RotZ.ToString();
                        SubPartMappingData.Text = ldd.Data;
                    }
                }                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgBasePartSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustBasePartSummaryRowFormatting(dgBasePartSummary);
        }

        private void dgLDrawDetailsSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustLDrawDetailsSummaryRowFormatting(dgLDrawDetailsSummary);
        }

        private void dgSubPartMappingSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustSubPartMappingSummaryRowFormatting(dgSubPartMappingSummary);
        }

        #endregion

        #region ** ACCELERATOR FUNCTIONS **

        private void ProcessBasePartSummaryFilter()
        {
            try
            {
                if (dgBasePartSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblBasePartSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgBasePartSummary, dgBasePartSummaryTable_Orig);
                    AdjustBasePartSummaryRowFormatting(dgBasePartSummary);

                    // ** Determine what filters have been applied **
                    if (fldBasePartLDrawRefAc.Text != "" || fldBasePartLDrawDescriptionAc.Text != "" || fldBasePartPartTypeAc.Text != "" || fldBasePartOffsetXAc.Text != "")
                    //if (fldLDrawRefAc.Text != "" || fldLDrawColourNameAc.Text != "" || chkFBXMissingAc.Checked == true)
                    {
                        List<DataRow> filteredRows = dgBasePartSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkBasePartLDrawRefAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Equals(fldBasePartLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Contains(fldBasePartLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for LDraw Description **
                        if (filteredRows.Count > 0)
                        {
                            if (chkBasePartLDrawDescriptionAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Description").ToUpper().Equals(fldBasePartLDrawDescriptionAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Description").ToUpper().Contains(fldBasePartLDrawDescriptionAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for Part Type **
                        if (filteredRows.Count > 0)
                        {
                            if (chkBasePartPartTypeAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Part Type").ToUpper().Equals(fldBasePartPartTypeAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Part Type").ToUpper().Contains(fldBasePartPartTypeAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for Offset X **
                        if (filteredRows.Count > 0)
                        {
                            if (chkBasePartOffsetXAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<float>("Offset X").ToString().ToUpper().Equals(fldBasePartOffsetXAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<float>("Offset X").ToString().ToUpper().Contains(fldBasePartOffsetXAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for FBX **
                        //if (chkFBXMissingAc.Checked)
                        //{
                        //    filteredRows = filteredRows.CopyToDataTable().AsEnumerable().Where(row => row.Field<bool>("Unity FBX") == false).ToList();
                        //}
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgBasePartSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgBasePartSummary, filteredRows.CopyToDataTable());
                            AdjustBasePartSummaryRowFormatting(dgBasePartSummary);
                        }
                        lblBasePartSummaryItemFilteredCount.Text = filteredRows.Count + " filtered part(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessLDrawDetailsSummaryFilter()
        {
            try
            {
                if (dgLDrawDetailsSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblLDrawDetailsSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgLDrawDetailsSummary, dgLDrawDetailsSummaryTable_Orig);
                    AdjustLDrawDetailsSummaryRowFormatting(dgLDrawDetailsSummary);

                    // ** Determine what filters have been applied **
                    if (fldLDrawDetailsLDrawRefAc.Text != "" || fldLDrawDetailsLDrawDescriptionAc.Text != "")
                    //if (fldLDrawRefAc.Text != "" || fldLDrawColourNameAc.Text != "" || chkFBXMissingAc.Checked == true)
                    {
                        List<DataRow> filteredRows = dgLDrawDetailsSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkLDrawDetailsLDrawRefAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Equals(fldLDrawDetailsLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Ref").ToUpper().Contains(fldLDrawDetailsLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for LDraw Description **
                        if (filteredRows.Count > 0)
                        {
                            if (chkLDrawDetailsLDrawDescriptionAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Description").ToUpper().Equals(fldLDrawDetailsLDrawDescriptionAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("LDraw Description").ToUpper().Contains(fldLDrawDetailsLDrawDescriptionAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for FBX **
                        //if (chkFBXMissingAc.Checked)
                        //{
                        //    filteredRows = filteredRows.CopyToDataTable().AsEnumerable().Where(row => row.Field<bool>("Unity FBX") == false).ToList();
                        //}
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgLDrawDetailsSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgLDrawDetailsSummary, filteredRows.CopyToDataTable());
                            AdjustLDrawDetailsSummaryRowFormatting(dgLDrawDetailsSummary);
                        }
                        lblLDrawDetailsSummaryItemFilteredCount.Text = filteredRows.Count + " filtered part(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessSubPartMappingSummaryFilter()
        {
            try
            {
                if (dgSubPartMappingSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblSubPartMappingSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgSubPartMappingSummary, dgSubPartMappingSummaryTable_Orig);
                    AdjustSubPartMappingSummaryRowFormatting(dgSubPartMappingSummary);

                    // ** Determine what filters have been applied **
                    if (fldSubPartMappingParentLDrawRefAc.Text != "")
                    //if (fldLDrawRefAc.Text != "" || fldLDrawColourNameAc.Text != "" || chkFBXMissingAc.Checked == true)
                    {
                        List<DataRow> filteredRows = dgSubPartMappingSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for Parent LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkSubPartMappingParentLDrawRefAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Parent LDraw Ref").ToUpper().Equals(fldSubPartMappingParentLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Parent LDraw Ref").ToUpper().Contains(fldSubPartMappingParentLDrawRefAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for LDraw Description **
                        //if (filteredRows.Count > 0)
                        //{
                        //    if (chkLDrawDetailsLDrawDescriptionAcEquals.Checked)
                        //    {
                        //        filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                        //                                    .Where(row => row.Field<string>("LDraw Description").ToUpper().Equals(fldLDrawDetailsLDrawDescriptionAc.Text.ToUpper()))
                        //                                    .ToList();
                        //    }
                        //    else
                        //    {
                        //        filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                        //                                    .Where(row => row.Field<string>("LDraw Description").ToUpper().Contains(fldLDrawDetailsLDrawDescriptionAc.Text.ToUpper()))
                        //                                    .ToList();
                        //    }
                        //}
                        #endregion

                        #region ** Apply filtering for FBX **
                        //if (chkFBXMissingAc.Checked)
                        //{
                        //    filteredRows = filteredRows.CopyToDataTable().AsEnumerable().Where(row => row.Field<bool>("Unity FBX") == false).ToList();
                        //}
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgSubPartMappingSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgSubPartMappingSummary, filteredRows.CopyToDataTable());
                            AdjustSubPartMappingSummaryRowFormatting(dgSubPartMappingSummary);
                        }
                        lblSubPartMappingSummaryItemFilteredCount.Text = filteredRows.Count + " filtered Mapping(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessFilesDatSummaryFilter()
        {
            try
            {
                if (dgFilesDatSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblFilesDatSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgFilesDatSummary, dgFilesDatSummaryTable_Orig);
                    AdjustFBXSummaryRowFormatting(dgFilesDatSummary);

                    // ** Determine what filters have been applied **
                    if (fldFilesDatFilenameAc.Text != "")
                    {
                        List<DataRow> filteredRows = dgFilesDatSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkFilesDatFilenameAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Equals(fldFilesDatFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Contains(fldFilesDatFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filtering for LDraw Description **
                        //if (filteredRows.Count > 0)
                        //{
                        //    if (chkBasePartLDrawDescriptionAcEquals.Checked)
                        //    {
                        //        filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                        //                                    .Where(row => row.Field<string>("LDraw Description").ToUpper().Equals(fldBasePartLDrawDescriptionAc.Text.ToUpper()))
                        //                                    .ToList();
                        //    }
                        //    else
                        //    {
                        //        filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                        //                                    .Where(row => row.Field<string>("LDraw Description").ToUpper().Contains(fldBasePartLDrawDescriptionAc.Text.ToUpper()))
                        //                                    .ToList();
                        //    }
                        //}
                        #endregion

                        #region ** Apply filtering for FBX **
                        //if (chkFBXMissingAc.Checked)
                        //{
                        //    filteredRows = filteredRows.CopyToDataTable().AsEnumerable().Where(row => row.Field<bool>("Unity FBX") == false).ToList();
                        //}
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgFilesDatSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgFilesDatSummary, filteredRows.CopyToDataTable());
                            AdjustFBXSummaryRowFormatting(dgFilesDatSummary);
                        }
                        lblFilesDatSummaryItemFilteredCount.Text = filteredRows.Count + " filtered file(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessFilesFbxSummaryFilter()
        {
            try
            {
                if (dgFilesFbxSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblFilesFbxSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgFilesFbxSummary, dgFilesFbxSummaryTable_Orig);
                    AdjustFBXSummaryRowFormatting(dgFilesFbxSummary);

                    // ** Determine what filters have been applied **
                    if (fldFilesFbxFilenameAc.Text != "")
                    {
                        List<DataRow> filteredRows = dgFilesFbxSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkFilesFbxFilenameAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Equals(fldFilesFbxFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Contains(fldFilesFbxFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgFilesFbxSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgFilesFbxSummary, filteredRows.CopyToDataTable());
                            AdjustFBXSummaryRowFormatting(dgFilesFbxSummary);
                        }
                        lblFilesFbxSummaryItemFilteredCount.Text = filteredRows.Count + " filtered file(s)";
                        #endregion
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ProcessFilesUnityFbxSummaryFilter()
        {
            try
            {
                if (dgFilesUnityFbxSummaryTable_Orig.Rows.Count > 0)
                {
                    // ** Reset summaey screen **
                    lblFilesUnityFbxSummaryItemFilteredCount.Text = "";
                    Delegates.DataGridView_SetDataSource(this, dgFilesUnityFbxSummary, dgFilesUnityFbxSummaryTable_Orig);
                    AdjustFBXSummaryRowFormatting(dgFilesUnityFbxSummary);

                    // ** Determine what filters have been applied **
                    if (fldFilesUnityFbxFilenameAc.Text != "")
                    {
                        List<DataRow> filteredRows = dgFilesUnityFbxSummaryTable_Orig.AsEnumerable().CopyToDataTable().AsEnumerable().ToList();

                        #region ** Apply filtering for LDraw Ref **
                        if (filteredRows.Count > 0)
                        {
                            if (chkFilesUnityFbxFilenameAcEquals.Checked)
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Equals(fldFilesUnityFbxFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                            else
                            {
                                filteredRows = filteredRows.CopyToDataTable().AsEnumerable()
                                                            .Where(row => row.Field<string>("Name").ToUpper().Contains(fldFilesUnityFbxFilenameAc.Text.ToUpper()))
                                                            .ToList();
                            }
                        }
                        #endregion

                        #region ** Apply filters **
                        Delegates.DataGridView_SetDataSource(this, dgFilesUnityFbxSummary, null);
                        if (filteredRows.Count > 0)
                        {
                            Delegates.DataGridView_SetDataSource(this, dgFilesUnityFbxSummary, filteredRows.CopyToDataTable());
                            AdjustFBXSummaryRowFormatting(dgFilesUnityFbxSummary);
                        }
                        lblFilesUnityFbxSummaryItemFilteredCount.Text = filteredRows.Count + " filtered file(s)";
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

        #region ** BASEPART FUNCTIONS **

        private void BasePart_Save()
        {
            try
            {
                // ** Validation Checks **               
                if (fldBasePartLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");
                string LDrawRef = fldBasePartLDrawRef.Text;
                if (fldBasePartLDrawDescription.Text.Equals("")) throw new Exception("No LDraw Description entered...");
                if (fldBasePartPartType.Text.Equals("")) throw new Exception("No Part Type entered...");
                if (fldBasePartLDrawPartType.Text.Equals("")) throw new Exception("No LDraw Part Type entered...");

                // Check if BasePart already exists - if so update it, if not, add it.
                string action = "UPDATE";
                BasePart bp = StaticData.GetBasePart(LDrawRef);
                if (bp == null)
                {
                    action = "ADD";
                    bp = new BasePart();                    
                }
                bp.LDrawRef = LDrawRef;
                bp.LDrawDescription = fldBasePartLDrawDescription.Text;
                int LDrawSize = 0; ;
                int.TryParse(fldBasePartLDrawSize.Text, out LDrawSize);
                bp.LDrawSize = LDrawSize;
                bp.partType = (BasePart.PartType)Enum.Parse(typeof(BasePart.PartType), fldBasePartPartType.Text, true);
                bp.lDrawPartType = (BasePart.LDrawPartType)Enum.Parse(typeof(BasePart.LDrawPartType), fldBasePartLDrawPartType.Text, true);
                bp.IsSubPart = chkBasePartIsSubPart.Checked;
                bp.IsSticker = chkBasePartIsSticker.Checked;
                bp.IsLargeModel = chkBasePartIsLargeModel.Checked;
                int OffsetX = 0; ;
                int.TryParse(fldBasePartOffsetX.Text, out OffsetX);
                bp.OffsetX = OffsetX;
                int OffsetY = 0; ;
                int.TryParse(fldBasePartOffsetY.Text, out OffsetY);
                bp.OffsetY = OffsetY;
                int OffsetZ = 0; ;
                int.TryParse(fldBasePartOffsetZ.Text, out OffsetZ);
                bp.OffsetZ = OffsetZ;
                int SubPartCount = 0; ;
                int.TryParse(fldBasePartSubPartCount.Text, out SubPartCount);
                bp.SubPartCount = SubPartCount;

                // ** Determine what action to take **
                if (action.Equals("ADD")) StaticData.AddBasePart(bp);                
                else if (action.Equals("UPDATE")) StaticData.UpdateBasePart(bp);
                
                // ** Tidy Up **
                //ClearAllSetDetailsFields();
                RefreshBasePart();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BasePart_Delete()
        {
            try
            {
                // ** Validations **
                if (fldBasePartLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");
                string LDrawRef = fldBasePartLDrawRef.Text;

                // ** Check if LDrawRef exists **
                bool exists = StaticData.CheckIfBasePartExists(LDrawRef);
                if (exists == false) throw new Exception("LDraw Ref doesn't exist for " + LDrawRef);

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // Check if all Sub Part Mappings should be deleted as well
                    bool DeleteSubPartMappings = true;  // Maybe we will take a look at this future. Currently will delete all SubPartMappings as well by default.

                    // ** Delete BasePart and all associated SubPartMappings **
                    List<string> LDrawRefList = StaticData.DeleteBasePart(LDrawRef, DeleteSubPartMappings);

                    // ** Tidy Up **
                    BasePart_Clear();
                    RefreshBasePart();
                    if (DeleteSubPartMappings) RefreshSubPartMapping();

                    // ** Show confirmation **
                    string confString = "BasePart " + LDrawRef + " deleted successfully..." + Environment.NewLine;
                    if (DeleteSubPartMappings)
                    {
                        List<string> SubPartMappingRefList = new List<string>(LDrawRefList);
                        SubPartMappingRefList.Remove(LDrawRef);
                        confString += "The following SubPartMappings were also deleted:" + Environment.NewLine;
                        confString += String.Join(",", SubPartMappingRefList);
                    }
                    MessageBox.Show(confString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BasePart_Clear()
        {
            fldBasePartLDrawRef.Text = "";
            fldBasePartLDrawImage.Image = null;
            fldBasePartLDrawDescription.Text = "";
            fldBasePartLDrawSize.Text = "";
            fldBasePartPartType.Text = "";
            fldBasePartLDrawPartType.Text = "";
            chkBasePartIsSubPart.Checked = false;
            chkBasePartIsSticker.Checked = false;
            chkBasePartIsLargeModel.Checked = false;
            fldBasePartOffsetX.Text = "";
            fldBasePartOffsetY.Text = "";
            fldBasePartOffsetZ.Text = "";
            fldBasePartSubPartCount.Text = "";
        }

        #endregion

        #region ** SUB PART MAPPING FUNCTIONS **

        private void SubPartMapping_Save()
        {
            try
            {
                // ** Validation Checks **               
                if (fldSubPartMappingParentLDrawRef.Text.Equals("")) throw new Exception("No Parent LDraw Ref entered...");
                string ParentLDrawRef = fldSubPartMappingParentLDrawRef.Text;
                if (fldSubPartMappingSubPartLDrawRef.Text.Equals("")) throw new Exception("No Sub Part LDraw Ref entered...");
                string SubPartLDrawRef = fldSubPartMappingSubPartLDrawRef.Text;
                if (fldSubPartMappingSubPartLDrawColourID.Text.Equals("")) throw new Exception("No LDrawColour ID entered...");
                int LDrawColourID;
                if(int.TryParse(fldSubPartMappingSubPartLDrawColourID.Text, out LDrawColourID) == false) throw new Exception("No LDrawColour ID not in correct format...");
               
                // Check if SubPartMapping already exists - if so update it, if not, add it.
                string action = "UPDATE";
                SubPartMapping spm = StaticData.GetSubPartMapping(ParentLDrawRef, SubPartLDrawRef);
                if (spm == null)
                {
                    action = "ADD";
                    spm = new SubPartMapping();
                }
                spm.ParentLDrawRef = fldSubPartMappingParentLDrawRef.Text;
                spm.SubPartLDrawRef = fldSubPartMappingSubPartLDrawRef.Text;
                spm.LDrawColourID = LDrawColourID;
                float PosX = 0;
                float.TryParse(fldSubPartMappingPosX.Text, out PosX);
                spm.PosX = PosX;
                float PosY = 0;
                float.TryParse(fldSubPartMappingPosY.Text, out PosY);
                spm.PosY = PosY;
                float PosZ = 0;
                float.TryParse(fldSubPartMappingPosZ.Text, out PosZ);
                spm.PosZ = PosZ;
                float RotX = 0;
                float.TryParse(fldSubPartMappingRotX.Text, out RotX);
                spm.RotX = RotX;
                float RotY = 0;
                float.TryParse(fldSubPartMappingRotY.Text, out RotY);
                spm.RotY = RotY;
                float RotZ = 0;
                float.TryParse(fldSubPartMappingRotZ.Text, out RotZ);
                spm.RotZ = RotZ;

                // ** Determine what action to take **
                if (action.Equals("ADD")) StaticData.AddSubPartMapping(spm);
                else if (action.Equals("UPDATE")) StaticData.UpdateSubPartMapping(spm);

                // ** Tidy Up **               
                RefreshSubPartMapping();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SubPartMapping_Delete()
        {
            try
            {
                // ** Validations **
                if (fldSubPartMappingParentLDrawRef.Text.Equals("")) throw new Exception("No Parent LDraw Ref entered...");
                string ParentLDrawRef = fldSubPartMappingParentLDrawRef.Text;
                if (fldSubPartMappingSubPartLDrawRef.Text.Equals("")) throw new Exception("No Sub Part LDraw Ref entered...");
                string SubPartLDrawRef = fldSubPartMappingSubPartLDrawRef.Text;

                // ** Check if SubPartMapping exists **
                bool exists = StaticData.CheckIfSubPartMappingExists(ParentLDrawRef, SubPartLDrawRef);
                if (exists == false) throw new Exception("Sub Part Mapping doesn't exist for " + ParentLDrawRef + " and " + SubPartLDrawRef);

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // ** Delete SubPartMapping **
                    StaticData.DeleteSubPartMapping(ParentLDrawRef, SubPartLDrawRef);

                    // ** Tidy Up **
                    SubPartMapping_Clear();
                    RefreshSubPartMapping();
                    MessageBox.Show("SubPartMapping for " + ParentLDrawRef + " and " + SubPartLDrawRef + " deleted successfully...");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SubPartMapping_Clear()
        {
            fldSubPartMappingParentLDrawRef.Text = "";
            fldSubPartMappingParentLDrawImage.Image = null;
            fldSubPartMappingSubPartLDrawRef.Text = "";
            fldSubPartMappingSubPartLDrawImage.Image = null;
            fldSubPartMappingSubPartLDrawColourID.Text = "";
            fldSubPartMappingPosX.Text = "";
            fldSubPartMappingPosY.Text = "";
            fldSubPartMappingPosZ.Text = "";
            fldSubPartMappingRotX.Text = "";
            fldSubPartMappingRotY.Text = "";
            fldSubPartMappingRotZ.Text = "";
            SubPartMappingData.Text = "";
        }

        #endregion

        #region ** LDRAW DETAILS FUNCTIONS **

        private void LDrawDetails_Save()
        {
            try
            {
                // ** Validation Checks **               
                if (fldLDrawDetailsLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");
                string LDrawRef = fldLDrawDetailsLDrawRef.Text;
                if (fldLDrawDetailsLDrawDescription.Text.Equals("")) throw new Exception("No LDraw Description entered...");
                if (fldLDrawDetailsPartType.Text.Equals("")) throw new Exception("No Part Type entered...");
                if (fldLDrawDetailsLDrawPartType.Text.Equals("")) throw new Exception("No LDraw Part Type entered...");

                // Check if LDrawDetails already exists - if so update it, if not, add it.
                string action = "UPDATE";
                LDrawDetails ldd = StaticData.GetLDrawDetails(LDrawRef);
                if (ldd == null)
                {
                    action = "ADD";
                    ldd = new LDrawDetails();
                }
                ldd.LDrawRef = LDrawRef;
                ldd.LDrawDescription = fldLDrawDetailsLDrawDescription.Text;
                ldd.PartType = fldLDrawDetailsPartType.Text;
                ldd.LDrawPartType = fldLDrawDetailsLDrawPartType.Text;
                int SubPartCount = 0;
                int.TryParse(fldLDrawDetailsSubPartCount.Text, out SubPartCount);
                ldd.SubPartCount = SubPartCount;
                ldd.SubPartLDrawRefList = new List<string>();
                if (fldLDrawDetailsLDrawRefList.Text != "") ldd.SubPartLDrawRefList = fldLDrawDetailsLDrawRefList.Text.Split(',').ToList();

                // ** Determine what action to take **
                if (action.Equals("ADD")) StaticData.AddLDrawDetails(ldd);
                else if (action.Equals("UPDATE")) StaticData.UpdateLDrawDetails(ldd);

                // ** Tidy Up **               
                RefreshLDrawDetails();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LDrawDetails_Delete()
        {
            try
            {
                // ** Validations **
                if (fldLDrawDetailsLDrawRef.Text.Equals("")) throw new Exception("No LDraw Ref entered...");
                string LDrawRef = fldLDrawDetailsLDrawRef.Text;

                // ** Check if LDrawDetails exists **
                bool exists = StaticData.CheckIfLDrawDetailsExist(LDrawRef);
                if (exists == false) throw new Exception("LDraw Details don't exist for " + LDrawRef);

                // Make sure user wants to delete
                DialogResult res = MessageBox.Show("Are you sure you want to Delete?", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.Yes)
                {
                    // Check if all Sub Parts should be deleted as well
                    bool DeleteSubParts = true; // Maybe we will take a look at this future. Currently will delete all SubParts as well by default.

                    // ** Delete parent LDrawDetails and all associated Sub Parts **
                    List<string> LDrawRefList = StaticData.DeleteLDrawDetails(LDrawRef, DeleteSubParts);

                    // ** Tidy Up **
                    LDrawDetails_Clear();
                    RefreshLDrawDetails();

                    // ** Show confirmation **
                    string confString = "LDrawDetails for " + LDrawRef + " deleted successfully..." + Environment.NewLine;
                    if (DeleteSubParts)
                    {
                        List<string> SubPartRefList = new List<string>(LDrawRefList);
                        SubPartRefList.Remove(LDrawRef);
                        confString += "The following Sub Parts were also deleted:" + Environment.NewLine;
                        confString += String.Join(",", SubPartRefList);
                    }
                    MessageBox.Show(confString);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void LDrawDetails_Clear()
        {
            fldLDrawDetailsLDrawRef.Text = "";
            fldLDrawDetailsLDrawImage.Image = null;
            fldLDrawDetailsLDrawDescription.Text = "";
            fldLDrawDetailsPartType.Text = "";
            fldLDrawDetailsLDrawPartType.Text = "";
            fldLDrawDetailsSubPartCount.Text = "";
            fldLDrawDetailsLDrawRefList.Text = "";
            LDrawDetailsData.Text = "";
        }

        #endregion

        #region ** COPY TO CLIPBOARD FUNCTIONS **

        private void btnBasePartSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgBasePartSummary);
        }

        private void btnLDrawDetailsSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgLDrawDetailsSummary);
        }

        private void btnSubPartMappingSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgSubPartMappingSummary);
        }

        private void btnFilesDatSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgFilesDatSummary);
        }

        private void btnFilesFbxSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgFilesFbxSummary);
        }

        private void btnFilesUnityFbxSummaryCopyToClipboard_Click(object sender, EventArgs e)
        {
            CopySummaryToClipboard(dgFilesUnityFbxSummary);
        }

        private void CopySummaryToClipboard(DataGridView dg)
        {
            try
            {
                if (dg.Rows.Count == 0) throw new Exception("No data to copy from " + dg.Name + "...");
                StringBuilder sb = BaseClasses.HelperFunctions.GenerateClipboardStringFromDataTable(dg);
                Clipboard.SetText(sb.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void ProcessLDrawRef_Leave()
        {
            try
            {
                // ** GET VARIABLES **                
                //if(fldLDrawRef.Text.Contains("Legs") == false && fldLDrawRef.Text.Contains("Torso") == false) fldLDrawRef.Text = fldLDrawRef.Text.ToLower();                
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
                    if (description != "") gbPartDetails.Text = "Part Details | " + description;
                    fldPartType.Text = coll.BasePartList[0].partType.ToString();
                    fldLDrawSize.Text = "";
                    if (coll.BasePartList[0].LDrawSize > 0) fldLDrawSize.Text = coll.BasePartList[0].LDrawSize.ToString();
                    chkIsSubPart.Checked = coll.BasePartList[0].IsSubPart;
                    chkIsSticker.Checked = coll.BasePartList[0].IsSticker;
                    chkIsLargeModel.Checked = coll.BasePartList[0].IsLargeModel;
                }
                else
                {
                    // check if LDrawDetails exist, if yes, use the value, if not save the ldrawdetails                    
                    LDrawDetailsCollection ldd_coll = StaticData.GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
                    if (ldd_coll.LDrawDetailsList.Count > 0)
                    {
                        fldPartType.Text = ldd_coll.LDrawDetailsList[0].PartType.ToString();
                        gbPartDetails.Text = "Part Details | " + ldd_coll.LDrawDetailsList[0].LDrawDescription;                        
                    }
                }

                // ** UPDATE BASE PART COLLECTION BOOLEAN - CHECK IF PART IS IN BASE PART COLLECTION **
                if (coll.BasePartList.Count > 0)
                {
                    //chkBasePartCollection.Checked = true;
                    btnAddPartToBasePartCollection.Enabled = false;
                    btnAddPartToBasePartCollection.BackColor = Color.Transparent;
                    tsPartDetails2.Enabled = false;
                }
                else
                {
                    //chkBasePartCollection.Checked = false;
                    btnAddPartToBasePartCollection.Enabled = true;
                    btnAddPartToBasePartCollection.BackColor = Color.Red;
                    tsPartDetails2.Enabled = true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
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

                // ** Refresh DAT Summary **
                RefreshFilesDat();

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

        public static void SyncFBXFiles()
        {
            //TODO_H: Need to remove this key from the client App
            string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";
            try
            {
                // ** GET ALL FBX FILES IN "static-data\files-fbx" ON AZURE SHARE **                
                //List<Azure.Storage.Files.Shares.Models.ShareFileItem> FSFileList = new ShareClient(AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFilesAndDirectories().ToList();
                //List<string> FileList_FS = FSFileList.Select(x => x.Name).ToList();
                FileDetailsCollection fdc = StaticData.GetFileDetailsData_FromContainer(@"static-data\files-fbx");

                // ** COPY FILES ACROSS THAT ARE NEW OR NEWER **
                List<string> updatedFileList = new List<string>();
                foreach (FileDetails fd in fdc.FileDetailsList)
                {
                    DateTime lastModified_Unity;
                    bool CopyFile = false;
                    if (File.Exists(Path.Combine(Global_Variables.UnityFBXLocation, fd.Name)) == false) CopyFile = true;                    
                    else
                    {
                        lastModified_Unity = new FileInfo(Path.Combine(Global_Variables.UnityFBXLocation, fd.Name)).LastWriteTimeUtc;
                        if (lastModified_Unity < fd.LastUpdatedTS) CopyFile = true;                        
                    }
                    if (CopyFile)
                    {
                        // ** Download file from Azure and save into Unity Resources\Lego Part Model directory **                        
                        string targetPath = Path.Combine(Global_Variables.UnityFBXLocation, fd.Name);
                        byte[] fileContent = new byte[fd.Size];
                        //TODO_H: Need to get this from API instead.
                        ShareFileClient share = new ShareClient(AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(fd.Name);
                        ShareFileDownloadInfo download = share.Download();
                        using (var fs = new FileStream(targetPath, FileMode.Create, FileAccess.Write)) download.Content.CopyTo(fs);                       
                        File.SetLastWriteTimeUtc(targetPath, fd.LastUpdatedTS);
                        updatedFileList.Add(fd.Name);
                    }
                }

                // ** SHOW CONFIRMATION **
                string confirmation = updatedFileList.Count + " file(s) added/updated in Unity Resource directory" + Environment.NewLine;
                foreach (string filename in updatedFileList) confirmation += filename + Environment.NewLine;
                MessageBox.Show(confirmation, "Syncing FBX file(s)...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void PartDetailsClear()
        {
            fldLDrawRef.Text = "";
            fldLDrawImage.Image = null;
            fldPartType.Text = "";
            fldLDrawSize.Text = "";
        }

        private void dgFilesDatSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.RowIndex != -1)
                {  
                    string Filename = dgFilesDatSummary.Rows[e.RowIndex].Cells["Name"].Value.ToString().ToLower();
                    FileDetails fd = StaticData.GetFileDetails_FromContainerAndFilename(@"static-data\files-dat", Filename);                    
                    FilesDatData.Text = fd.Data;                   
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }






    }
}
