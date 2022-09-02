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

namespace Generator
{
    public partial class StaticDataScreen : Form
    {
        private Scintilla LDrawDetailsData = new ScintillaNET.Scintilla();
        private Scintilla SubPartMappingData = new ScintillaNET.Scintilla();


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
                String[] DGnames = new string[] { "dgBasePartSummary", "dgLDrawDetailsSummary", "dgSubPartMappingSummary" };
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
                #endregion

                #region ** ADD HEADER SUMMARY TOOLSTRIP ITEMS **
                tsHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[]
                {
                    btnExit,
                    toolStripSeparator2,
                    btnRefreshAll,
                    toolStripSeparator13,
                    new ToolStripControlHost(chkShowPartImages)
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
                    toolStripSeparator15,                    
                    btnBasePartClear,
                    toolStripSeparator3,                    
                    btnBasePartSave,
                    btnBasePartDelete
                });
                #endregion

                // ** Set up Scintilla **               
                pnlLDrawDetailsData.Controls.Add(LDrawDetailsData);
                ApplyDefaultScintillaStyles(LDrawDetailsData);
                pnlSubPartMappingData.Controls.Add(SubPartMappingData);
                ApplyDefaultScintillaStyles(SubPartMappingData);

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
                Delegates.ToolStripProgressBar_SetValue(this, pbLDrawDetailsStatus, 0);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgLDrawDetailsSummary, table);
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsCount, table.Rows.Count.ToString("#,##0") + " Part(s)");

                // ** Format summary **
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "Refreshing - Formatting summary data...");
                AdjustBaseLDrawDetailsSummaryRowFormatting(dgLDrawDetailsSummary);
                Delegates.ToolStripLabel_SetText(this, lblLDrawDetailsStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AdjustBaseLDrawDetailsSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustBaseLDrawDetailsSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Part Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Part Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
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
                Delegates.ToolStripProgressBar_SetValue(this, pbSubPartMappingStatus, 0);

                // ** Posting data to summary **
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingStatus, "Refreshing - Posting data to summary...");
                Delegates.DataGridView_SetDataSource(this, dgSubPartMappingSummary, table);
                Delegates.ToolStripLabel_SetText(this, lblSubPartMappingCount, table.Rows.Count.ToString("#,##0") + " Part(s)");

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

        #region ** CELL CLICK FUNCTIONS **

        private void dgBasePartSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {                    
                    var obj = dgBasePartSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    if(obj != DBNull.Value)
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private void dgSubPartMappingSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
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
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        #endregion


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



    }
}
