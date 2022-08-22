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
    public partial class SetDetailsScreen : Form
    {
        // ** Variables **
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private TreeNode lastSelectedNode;
        private string lastSelectedNodeFullPath = "";


        public SetDetailsScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Set Details";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                log.Info(".......................................................................SET DETAILS SCREEN STARTED.......................................................................");

                #region ** FORMAT SUMMARIES **
                String[] DGnames = new string[] { "dgSetDetailsSummary" };
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
                lblSetDetailsCount.Text = "";
                lblStatus.Text = "";
                fldType.SelectedIndex = 0;                
                fldStatus.SelectedIndex = 0;
                #endregion

                // ** Refresh Screen **
                RefreshThemeTreeview();
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnSetSave_Click(object sender, EventArgs e)
        {
            SaveSet();
        }

        private void fldTheme_Leave(object sender, EventArgs e)
        {
            ProcessTheme_Leave();
        }

        private void btnSetDelete_Click(object sender, EventArgs e)
        {
            DeleteSet();
        }

        private void btnSetClear_Click(object sender, EventArgs e)
        {
            ClearAllSetDetailsFields();
        }

        private void btnThemesRefresh_Click(object sender, EventArgs e)
        {
            RefreshThemeTreeview();
        }

        private void btnOpenInViewer_Click(object sender, EventArgs e)
        {
            OpenSetInViewer();
        }

        private void btnSetDetailsRefresh_Click(object sender, EventArgs e)
        {
            RefreshSetDetailsSummary();
        }

        private void btnUploadInstructionsFromWeb_Click(object sender, EventArgs e)
        {
            UploadInstructionsFromWeb();
        }

        private void btnImportSetDetailsFromCSVFile_Click(object sender, EventArgs e)
        {
            ImportSetDetails();
        }

        #endregion

        #region ** REFRESH THEME TREEVIEW FUNCTIONS **

        //private void RefreshThemeTreeview_OLD()
        //{
        //    try
        //    {
        //        // ** Get all Theme Details from SET_DETAILS **
        //        BaseClasses.ThemeDetailsCollection ThemeDetailsCollection = StaticData.GetAllThemeDetails();

        //        // ** Convert ThemeDetails into TreeNode **
        //        TreeNode[] ThemeTreeNodes = ThemeDetailsCollection.ConvertToTreeNodeList();

        //        // ** Add Theme counts **                
        //        ThemeTreeNodes = UpdateThemeCounts(ThemeTreeNodes);

        //        // ** Add images to Themes & SubThemes **
        //        ThemeTreeNodes = UpdateThemeImages(ThemeTreeNodes);
                
        //        // ** Set tvThemesSummary **
        //        tvThemesSummary.Nodes.Clear();
        //        tvThemesSummary.Nodes.AddRange(ThemeTreeNodes);

        //        // ** Set Theme dropdown values **
        //        fldTheme.Items.Clear();
        //        foreach(ThemeDetails td in ThemeDetailsCollection.ThemeDetailsList) fldTheme.Items.Add(td.Theme);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private BackgroundWorker bw_RefreshThemeTreeview;

        private void EnableControls_RefreshThemeTreeview(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshThemeTreeview(value)));
            }
            else
            {
                btnExit.Enabled = value;
                fldImportFilePath.Enabled = value;
                fldImportFilePathBrowse.Enabled = value;
                btnImportSetDetailsFromCSVFile.Enabled = value;
                gpThemes.Enabled = value;
                gpThemeSummary.Enabled = value;
                gpSetDetails.Enabled = value;
            }
        }

        private void RefreshThemeTreeview()
        {
            try
            {
                EnableControls_RefreshThemeTreeview(false);

                // ** Reset imagelists **
                ilTheme.Images.Clear();
                ilTheme.Images.Add(ilThemeTemplate.Images[0]);

                // ** Run background to process functions **
                bw_RefreshThemeTreeview = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshThemeTreeview.DoWork += new DoWorkEventHandler(bw_RefreshThemeTreeview_DoWork);
                bw_RefreshThemeTreeview.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshThemeTreeview_RunWorkerCompleted);
                bw_RefreshThemeTreeview.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshThemeTreeview_ProgressChanged);
                bw_RefreshThemeTreeview.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                EnableControls_RefreshThemeTreeview(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshThemeTreeview_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshThemeTreeview_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_RefreshThemeTreeview(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshThemeTreeview_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get all Theme Details from SET_DETAILS **
                BaseClasses.ThemeDetailsCollection ThemeDetailsCollection = StaticData.GetAllThemeDetails();

                // ** Convert ThemeDetails into TreeNode **
                TreeNode[] ThemeTreeNodes = ThemeDetailsCollection.ConvertToTreeNodeList();

                // ** Get all image data up front **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Theme image data...");
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, ThemeTreeNodes.Length);
                int imageIndex = 0;
                foreach(ThemeDetails td in ThemeDetailsCollection.ThemeDetailsList)
                {
                    bw_RefreshThemeTreeview.ReportProgress(imageIndex, "Working...");
                    string theme = td.Theme;
                    ArfaImage.GetImage(ImageType.THEME, new string[] { theme });
                    foreach(string subTheme in td.SubThemeList) ArfaImage.GetImage(ImageType.THEME, new string[] { subTheme });                    
                    imageIndex += 1;
                }
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);

                // ** Add Theme counts **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Adding Theme counts...");
                ThemeTreeNodes = UpdateThemeCounts(ThemeTreeNodes);

                // ** Add images to Themes & SubThemes **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Adding Theme images...");
                ThemeTreeNodes = UpdateThemeImages(ThemeTreeNodes);

                // ** Set tvThemesSummary **                
                Delegates.TreeView_ClearNodes(this, tvThemesSummary);
                Delegates.TreeView_AddRange(this, tvThemesSummary, ThemeTreeNodes);

                // ** Set Theme dropdown values **
                List<string> themeList = new List<string>();
                foreach (ThemeDetails td in ThemeDetailsCollection.ThemeDetailsList) themeList.Add(td.Theme);                
                Delegates.ToolStripComboBox_ClearItems(this, fldTheme);
                Delegates.ToolStripComboBox_AddItems(this, fldTheme, themeList);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private TreeNode[] UpdateThemeCounts(TreeNode[] ThemeTreeNodes)
        {
            foreach (TreeNode themeNode in ThemeTreeNodes)
            {
                string theme = themeNode.Tag.ToString().Split('|')[0];
                int themeCount = StaticData.GetSetCountForThemeAndSubTheme(theme, "");
                themeNode.Text += " [" + themeCount + "]";
                if (themeNode.Nodes.Count > 0)
                {
                    foreach (TreeNode subThemeNode in themeNode.Nodes)
                    {
                        string subTheme = subThemeNode.Tag.ToString().Split('|')[1];
                        int subThemeCount = StaticData.GetSetCountForThemeAndSubTheme(theme, subTheme);
                        subThemeNode.Text += " [" + subThemeCount + "]";
                    }
                }
            }
            return ThemeTreeNodes;
        }

        private TreeNode[] UpdateThemeImages(TreeNode[] ThemeTreeNodes)
        {
            int imageCount = 1;            
            foreach (TreeNode themeNode in ThemeTreeNodes)
            {
                // Get Theme + SubTheme image
                string themeImageName = themeNode.Tag.ToString();
                Bitmap themeImage = ArfaImage.GetImage(ImageType.THEME, new string[] { themeImageName });

                // Add image to Theme imagelist
                int imageIndex = 0;
                if (themeImage != null)
                {                    
                    Delegates.ImageList_AddItem(this, ilTheme, themeImage);
                    imageIndex = imageCount;
                    imageCount += 1;
                }
                themeNode.ImageIndex = imageIndex;
                themeNode.SelectedImageIndex = imageIndex;

                // ** Check for Sub Themes **
                if (themeNode.Nodes.Count > 0)
                {
                    foreach (TreeNode subThemeNode in themeNode.Nodes)
                    {
                        // Get Theme + SubTheme image
                        string subThemeImageName = subThemeNode.Tag.ToString();
                        Bitmap subThemeImage = ArfaImage.GetImage(ImageType.THEME, new string[] { subThemeImageName });

                        // Add image to Theme imagelist
                        int subTheme_imageIndex = 0;
                        if (subThemeImage != null)
                        {                           
                            Delegates.ImageList_AddItem(this, ilTheme, subThemeImage);
                            subTheme_imageIndex = imageCount;
                            imageCount += 1;
                        }
                        subThemeNode.ImageIndex = subTheme_imageIndex;
                        subThemeNode.SelectedImageIndex = subTheme_imageIndex;
                    }
                }
            }
            return ThemeTreeNodes;
        }

        #endregion

        #region ** TREENODE FUNCTIONS **

        private void tvThemesSummary_AfterSelect(object sender, TreeViewEventArgs e)
        {
            try
            {
                // ** Clear all fields & disable groupboxes **
                //ClearAllFields();
                //gpSet.Enabled = false;
                //gpSubSet.Enabled = false;
                //gpSubModel.Enabled = false;
                //gpModel.Enabled = false;
                //gpStep.Enabled = false;
                //gbPartDetails.Enabled = false;

                // ** Update treeview node highlighting **
                if (lastSelectedNode != null)
                {
                    lastSelectedNode.BackColor = Color.White;
                    lastSelectedNode.ForeColor = Color.Black;
                }
                tvThemesSummary.SelectedNode.BackColor = SystemColors.Highlight;
                tvThemesSummary.SelectedNode.ForeColor = SystemColors.HighlightText;

                // ** Determine Theme & SubTheme **
                lastSelectedNode = tvThemesSummary.SelectedNode;
                lastSelectedNodeFullPath = tvThemesSummary.SelectedNode.FullPath;

                // ** Update Theme image **
                string theme = tvThemesSummary.SelectedNode.Tag.ToString();
                pnlThemeImage.BackgroundImage = ArfaImage.GetImage(ImageType.THEME, new string[] { theme });

                // ** Refresh Set Details Summary **
                RefreshSetDetailsSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnExpandAll_Click(object sender, EventArgs e)
        {
            //if (tvThemesSummary.Nodes.Count > 0) tvThemesSummary.Nodes[0].ExpandAll();
            if (tvThemesSummary.Nodes.Count > 0)
            {
                foreach(TreeNode node in tvThemesSummary.Nodes) node.ExpandAll();                
            }
        }

        private void btnCollapseAll_Click(object sender, EventArgs e)
        {
            // (tvThemesSummary.Nodes.Count > 0) tvThemesSummary.Nodes[0].Collapse(false);
            if (tvThemesSummary.Nodes.Count > 0)
            {
                foreach (TreeNode node in tvThemesSummary.Nodes) node.Collapse(false);
            }
        }

        #endregion

        #region ** REFRESH SET DETAILS SUMMARY FUNCTIONS **

        private BackgroundWorker bw_RefreshSetDetailsSummary;

        private void EnableControls_RefreshSetDetailsSummary(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_RefreshSetDetailsSummary(value)));
            }
            else
            {
                btnExit.Enabled = value;
                fldImportFilePath.Enabled = value;
                fldImportFilePathBrowse.Enabled = value;
                btnImportSetDetailsFromCSVFile.Enabled = value;
                gpThemes.Enabled = value;
                gpThemeSummary.Enabled = value;
                gpSetDetails.Enabled = value;
            }
        }

        private void RefreshSetDetailsSummary()
        {
            try
            {
                EnableControls_RefreshSetDetailsSummary(false);

                // ** CLEAR FIELDS ** 
                //tvSetSummary.Nodes.Clear();
                //TextArea.Text = "";
                //TextArea2.Text = "";
                //dgPartListSummary.DataSource = null;
                //lblPartListCount.Text = "";
                //dgPartListWithMFsSummary.DataSource = null;
                //lblPartListWithMFsCount.Text = "";
                dgSetDetailsSummary.DataSource = null;
                lblSetDetailsCount.Text = "";


                // ** Run background to process functions **
                bw_RefreshSetDetailsSummary = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_RefreshSetDetailsSummary.DoWork += new DoWorkEventHandler(bw_RefreshSetDetailsSummary_DoWork);
                bw_RefreshSetDetailsSummary.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshSetDetailsSummary_RunWorkerCompleted);
                //bw_RefreshSetDetailsSummary.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshScreen_ProgressChanged);
                bw_RefreshSetDetailsSummary.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                EnableControls_RefreshSetDetailsSummary(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshSetDetailsSummary_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_RefreshSetDetailsSummary(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_RefreshSetDetailsSummary_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Determine Theme & SubTheme **
                if(lastSelectedNode != null)
                {
                    List<string> themeStringList = lastSelectedNode.Tag.ToString().Split('|').ToList();
                    string theme = themeStringList[0];
                    string subTheme = "";
                    if (themeStringList.Count > 1) subTheme = themeStringList[1];

                    // Get SetDetails for Theme & SubTheme
                    BaseClasses.SetDetailsCollection coll = StaticData.GetSetDetailsData_UsingThemeAndSubTheme(theme, subTheme);

                    // ** Post Set Details data **
                    DataTable setDetailsTable = GenerateSetDetailsTable(coll);
                    setDetailsTable.DefaultView.Sort = "Theme, Sub Theme, Ref";
                    setDetailsTable = setDetailsTable.DefaultView.ToTable();
                    Delegates.DataGridView_SetDataSource(this, dgSetDetailsSummary, setDetailsTable);
                    AdjustSetDetailsSummaryRowFormatting(dgSetDetailsSummary);

                    // ** Update summary label **
                    int setCount = setDetailsTable.Rows.Count;
                    Delegates.ToolStripLabel_SetText(this, lblSetDetailsCount, setCount.ToString("#,##0") + " Set(s)");
                }               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable GenerateSetDetailsTable(SetDetailsCollection coll)
        {
            try
            {
                #region ** GET DATA UPFRONT **                
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Refresh Set Detail Summary - Getting image and instruction data...");
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, coll.SetDetailsList.Count);
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                Dictionary<string, bool> SetInstructionsExist = new Dictionary<string, bool>();
                int index = 0;                
                foreach (SetDetails sd in coll.SetDetailsList)
                {
                    Delegates.ToolStripProgressBar_SetValue(this, pbStatus, index);
                    if(SetInstructionsExist.ContainsKey(sd.Ref) == false) SetInstructionsExist.Add(sd.Ref, false);                   
                    SetInstructionsExist[sd.Ref] = StaticData.CheckIfPDFInstructionsExistForSet(sd.Ref);
                    ArfaImage.GetImage(ImageType.SET, new string[] { sd.Ref });
                    index += 1;
                }
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);                
                #endregion

                // ** GENERATE COLUMNS **
                DataTable setDetailsTable = new DataTable("SetDetailsTable", "SetDetailsTable");
                setDetailsTable.Columns.Add("Set Image", typeof(Bitmap));
                setDetailsTable.Columns.Add("Ref", typeof(string));
                setDetailsTable.Columns.Add("Description", typeof(string));
                setDetailsTable.Columns.Add("Type", typeof(string));
                setDetailsTable.Columns.Add("Theme", typeof(string));
                setDetailsTable.Columns.Add("Sub Theme", typeof(string));
                setDetailsTable.Columns.Add("Year", typeof(int));
                setDetailsTable.Columns.Add("Part Count", typeof(int));
                setDetailsTable.Columns.Add("SubSet Count", typeof(int));
                setDetailsTable.Columns.Add("Model Count", typeof(int));
                setDetailsTable.Columns.Add("MiniFig Count", typeof(int));
                setDetailsTable.Columns.Add("Status", typeof(string));
                setDetailsTable.Columns.Add("Assigned To", typeof(string));
                setDetailsTable.Columns.Add("Instruction Refs", typeof(string));
                setDetailsTable.Columns.Add("Instructions Exist", typeof(bool));

                // ** Cycle through details and populate rows **
                foreach (SetDetails sd in coll.SetDetailsList)
                {
                    // ** Build row and add to table **                    
                    DataRow newRow = setDetailsTable.NewRow();
                    newRow["Set Image"] = ArfaImage.GetImage(ImageType.SET, new string[] { sd.Ref });
                    newRow["Ref"] = sd.Ref;
                    newRow["Description"] = sd.Description;
                    newRow["Type"] = sd.Type;
                    newRow["Theme"] = sd.Theme;
                    newRow["Sub Theme"] = sd.SubTheme;
                    newRow["Year"] = sd.Year;
                    newRow["Part Count"] = sd.PartCount;
                    newRow["SubSet Count"] = sd.SubSetCount;
                    newRow["Model Count"] = sd.ModelCount;
                    newRow["MiniFig Count"] = sd.MiniFigCount;
                    newRow["Status"] = sd.Status;
                    newRow["Assigned To"] = sd.AssignedTo;
                    newRow["Instruction Refs"] = String.Join(",", sd.InstructionRefList);                    
                    newRow["Instructions Exist"] = SetInstructionsExist[sd.Ref];
                    setDetailsTable.Rows.Add(newRow);
                }
                return setDetailsTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
                return null;
            }
        }

        private void AdjustSetDetailsSummaryRowFormatting(DataGridView dg)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => AdjustSetDetailsSummaryRowFormatting(dg)));
            }
            else
            {
                // ** Format columns **                
                dg.Columns["Set Image"].HeaderText = "";
                ((DataGridViewImageColumn)dg.Columns["Set Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                //dg.Columns["Colour Image"].HeaderText = "";
                //((DataGridViewImageColumn)dg.Columns["Colour Image"]).ImageLayout = DataGridViewImageCellLayout.Zoom;
                dg.AutoResizeColumns();
            }
        }

        #endregion

        #region ** FIELD LEAVE FUNCTIONS **

        private void ProcessTheme_Leave()
        {
            try
            {
                // ** Get ThemeDetails **
                fldSubTheme.Items.Clear();
                string theme = fldTheme.Text;
                if (theme != "")
                {
                    ThemeDetails td = StaticData.GetThemeDetails(theme);
                    if (td != null)
                    {
                        foreach (string SubTheme in td.SubThemeList) fldSubTheme.Items.Add(SubTheme);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** SET DETAILS FUNCTIONS **

        private void SaveSet()
        {
            try
            {
                // ** Validation Checks **               
                // Mandatory fields -> Ref, Description, Type, Theme, status
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string setRef = fldSetRef.Text;
                if (fldDescription.Text.Equals("")) throw new Exception("No Description entered...");
                if (fldType.Text.Equals("")) throw new Exception("No Type entered...");
                if (fldTheme.Text.Equals("")) throw new Exception("No Theme entered...");
                if (fldStatus.Text.Equals("")) throw new Exception("No Status entered...");

                // Check if Set already exists - if so update it, if not, add it.
                string action = "UPDATE";                
                SetDetails setDetails = StaticData.GetSetDetails(setRef);
                if (setDetails == null)
                {
                    action = "ADD";
                    setDetails = new SetDetails();
                    setDetails.PartCount = 0;
                    setDetails.SubSetCount = 1;
                    setDetails.ModelCount = 1;
                    setDetails.MiniFigCount = 0;

                    // ** Generate base instructions and add to Set Details **
                    Set set = Set.GenerateBaseSet(setRef, fldDescription.Text, fldType.Text);
                    setDetails.Instructions = set.SerializeToString(true);
                }
                setDetails.Ref = setRef;
                setDetails.Description = fldDescription.Text;
                setDetails.Type = fldType.Text;
                setDetails.Theme = fldTheme.Text;
                setDetails.SubTheme = fldSubTheme.Text;
                setDetails.Year = int.Parse(fldYear.Text);                
                setDetails.Status = fldStatus.Text;
                setDetails.AssignedTo = fldAssignedTo.Text;
                setDetails.InstructionRefList = fldInstructionRefs.Text.Split(',').ToList();
                                
                // ** Determine what action to take **
                if (action.Equals("ADD")) StaticData.AddSetDetails(setDetails);               
                else if(action.Equals("UPDATE")) StaticData.UpdateSetDetails(setDetails);
               
                // ** Tidy Up **
                ClearAllSetDetailsFields();
                RefreshThemeTreeview();
                RefreshSetDetailsSummary();
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
                // ** Validations **
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldSetRef.Text;

                // Check if SetRef exists
                SetDetails sd = StaticData.GetSetDetails(SetRef);
                if (sd == null) throw new Exception("Set Details don't exist for " + SetRef);

                // ** Delete Set Details **
                StaticData.DeleteSetDetails(SetRef);

                // ** Tidy Up **
                ClearAllSetDetailsFields();
                RefreshThemeTreeview();
                RefreshSetDetailsSummary();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ClearAllSetDetailsFields()
        {
            fldSetRef.Text = "";
            fldDescription.Text = "";            
            fldType.Text = "";
            fldType.SelectedIndex = 0;
            fldTheme.Text = "";
            fldSubTheme.Text = "";
            fldYear.Text = "";
            fldStatus.Text = "";
            fldStatus.SelectedIndex = 0;
            fldAssignedTo.Text = "";
            fldInstructionRefs.Text = "";
            //pnlThemeImage.BackgroundImage = null;
        }

        private void OpenSetInViewer()
        {
            try
            {
                // ** Validation **
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                string SetRef = fldSetRef.Text;

                // Check if SetRef exists
                SetDetails sd = StaticData.GetSetDetails(SetRef);
                if (sd == null) throw new Exception("Set Details don't exist for " + SetRef);

                // ** Load Viewer Screen **
                Generator form = new Generator(SetRef);
                form.LoadSet();
                form.Visible = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void dgSetDetailsSummary_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                if (e.ColumnIndex == 0)
                {
                    Bitmap image = (Bitmap)dgSetDetailsSummary.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                    PartViewer.image = image;
                    PartViewer form = new PartViewer();
                    form.Visible = true;
                }
                else
                {
                    // Get Set_Details for Set Ref              
                    string SetRef = dgSetDetailsSummary.Rows[e.RowIndex].Cells["Ref"].Value.ToString();
                    SetDetails SetDetails = StaticData.GetSetDetails(SetRef);

                    // ** Post data to form **
                    fldSetRef.Text = SetDetails.Ref;
                    fldDescription.Text = SetDetails.Description;
                    fldType.Text = SetDetails.Type;
                    fldTheme.Text = SetDetails.Theme;
                    fldSubTheme.Text = SetDetails.SubTheme;
                    fldYear.Text = SetDetails.Year.ToString();
                    fldStatus.Text = SetDetails.Status;
                    fldAssignedTo.Text = SetDetails.AssignedTo;
                    fldInstructionRefs.Text = String.Join(",", SetDetails.InstructionRefList);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }

        private async void UploadInstructionsFromWeb()
        {
            //TODO_H: Change this function to let the API do all the heavy lifting - just providfe the API with the Set Ref and the list of Instruction Refs
            string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";
            try
            {
                #region ** VALIDATIONS **
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                if (fldInstructionRefs.Text.Equals("")) throw new Exception("No Instruction Refs entered...");
                string setRef = fldSetRef.Text;
                List<string> insRefList = fldInstructionRefs.Text.Split(',').ToList();

                // ** Check whether instructions are already present. If they are, confirm whether they should be downloaded again **
                ShareFileClient share = new ShareClient(AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
                if (share.Exists())
                //bool PDFExists = StaticData.CheckIfPDFInstructionsExistForSet(setRef);
                //if (PDFExists)
                {
                    // Make sure user wants to re-upload instructions
                    DialogResult res = MessageBox.Show("Instructions already exist for " + setRef + " - do you really want to re-upload again?", "Instruction Re-Upload Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No) return;
                }
                #endregion

                #region ** DOWNLOAD INSTRUCTIONS FROM BrickSet.com TO TEMP **
                List<string> filelist = new List<string>();
                int index = 1;
                foreach (string insRef in insRefList)
                {
                    string url = "https://www.lego.com/cdn/product-assets/product.bi.core.pdf/" + insRef + ".pdf";
                    string downloadPath = Path.Combine(Path.GetTempPath(), insRef + ".pdf");
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadProgressChanged += (s, e1) =>
                        {
                            pbStatus.Value = e1.ProgressPercentage;
                            lblStatus.Text = "Downloading " + insRef + " from Brickset.com (" + index + " of " + insRefList.Count + ") | Downloaded " + e1.ProgressPercentage + "%";
                        };
                        webClient.DownloadFileCompleted += (s, e1) =>
                        {
                            pbStatus.Value = 0;
                            lblStatus.Text = "";
                        };
                        Task downloadTask = webClient.DownloadFileTaskAsync(new Uri(url), downloadPath);
                        await downloadTask;
                    }
                    filelist.Add(downloadPath);
                    index += 1;
                }
                #endregion

                #region ** COMBINE ALL PDFs INTO A SINGLE ONE **
                lblStatus.Text = "Merging PDFs...";
                string targetPdf = Path.Combine(Path.GetTempPath(), setRef + ".pdf");
                using (FileStream stream = new FileStream(targetPdf, FileMode.Create))
                {
                    using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                    {
                        PdfCopy pdf = new PdfCopy(document, stream);
                        document.Open();
                        document.NewPage();
                        foreach (string file in filelist)
                        {
                            using (PdfReader reader = new PdfReader(file)) pdf.AddDocument(reader);
                        }
                    }
                }
                foreach (string file in filelist) File.Delete(file);
                #endregion

                // ** Upload data to Azure BLOB **               
                //lblStatus.Text = "Uploading " + setRef + " to Azure BLOB...";               
                //CloudBlockBlob blockBlob = blobClient.GetContainerReference("files-instructions").GetBlockBlobReference(setRef + ".pdf");
                //Task uploadTask = blockBlob.UploadFromFileAsync(targetPdf, FileMode.Open);
                //await uploadTask;

                // ** Upload data to Azure FS - USES DIRECT LINK TO FOLDER **  
                //lblStatus.Text = "Uploading " + setRef + " to Azure FS...";
                //string FSPath = Path.Combine(@"\\lodgeaccount.file.core.windows.net\lodgeant-fs\files-instructions", setRef + ".pdf");                
                //using (Stream source = File.Open(targetPdf, FileMode.Open))
                //{
                //    using (Stream destination = File.Create(FSPath))
                //    {
                //        await source.CopyToAsync(destination);
                //    }
                //}

                #region ** UPLOAD DATA TO AZURE FS **  
                lblStatus.Text = "Uploading " + setRef + " to Azure FS...";
                share = new ShareClient(AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
                const int AzureUploadLimit = 4194304;
                using (var stream = new MemoryStream(File.ReadAllBytes(targetPdf)))
                {
                    share.Create(stream.Length);
                    pbStatus.Maximum = (int)stream.Length;
                    long uploadIndex = 0;

                    var progressHandler = new Progress<long>();
                    progressHandler.ProgressChanged += (s, e1) =>
                    {
                        int uploadedValue = (int)uploadIndex + (int)e1;
                        double pc = (uploadedValue / (double)(stream.Length)) * 100;
                        pbStatus.Value = uploadedValue;
                        lblStatus.Text = "Uploading " + setRef + " to Azure FS | Uploaded " + pc.ToString("#,##0") + "%";
                    };

                    if (stream.Length <= AzureUploadLimit)
                    {
                        await share.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream, progressHandler: progressHandler);
                    }
                    else
                    {
                        int bytesRead;
                        byte[] buffer = new byte[AzureUploadLimit];
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            MemoryStream ms = new MemoryStream(buffer, 0, bytesRead);
                            await share.UploadRangeAsync(new Azure.HttpRange(uploadIndex, ms.Length), ms, progressHandler: progressHandler);
                            uploadIndex += ms.Length;
                        }
                    }
                }
                lblStatus.Text = "";
                pbStatus.Value = 0;
                #endregion

                // ** Delete TEMP file **
                File.Delete(targetPdf);

                // ** Clear fields & Tidy up **
                lblStatus.Text = "";
                RefreshSetDetailsSummary();

                // ** SHOW CONFIRMATION **                
                MessageBox.Show(setRef + " uploaded to Azure...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        #region ** IMPORT SET DETAILS FUNCTIONS **

        private BackgroundWorker bw_ImportSetDetails;        
        private string importFilePath = "";
        private int importedSetCount = 0;
        private int ignoredSetCount = 0;

        private void EnableControls_ImportSetDetails(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_ImportSetDetails(value)));
            }
            else
            {
                btnExit.Enabled = value;
                fldImportFilePath.Enabled = value;
                fldImportFilePathBrowse.Enabled = value;
                btnImportSetDetailsFromCSVFile.Enabled = value;
                gpThemes.Enabled = value;
                gpThemeSummary.Enabled = value;
                gpSetDetails.Enabled = value;
            }
        }

        private void ImportSetDetails()
        {
            try
            {
                // ** Validation **
                if (fldImportFilePath.Text.Equals("")) throw new Exception("No File Path entered...");
                importFilePath = fldImportFilePath.Text;
                if(File.Exists(importFilePath) == false) throw new Exception("File Path doesn't exist...");

                EnableControls_ImportSetDetails(false);

                // ** Run background to process functions **
                bw_ImportSetDetails = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_ImportSetDetails.DoWork += new DoWorkEventHandler(bw_ImportSetDetails_DoWork);
                bw_ImportSetDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_ImportSetDetails_RunWorkerCompleted);
                bw_ImportSetDetails.ProgressChanged += new ProgressChangedEventHandler(bw_ImportSetDetails_ProgressChanged);
                bw_ImportSetDetails.RunWorkerAsync();
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                EnableControls_ImportSetDetails(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }
               
        private void bw_ImportSetDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_ImportSetDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                //Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                //EnableControls_ImportSetDetails(true);
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                if (importedSetCount > 0)
                {
                    RefreshThemeTreeview();
                    RefreshSetDetailsSummary();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_ImportSetDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Get Set Details from CSV file **                
                string[] fileLines = File.ReadAllLines(importFilePath);
                
                // ** Cycle through each line **
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, fileLines.Length);
                importedSetCount = 0;
                ignoredSetCount = 0;
                int lineIndex = 0;
                List<string> failedImportList = new List<string>();
                foreach (string line in fileLines)
                {
                    if (lineIndex > 0)
                    {
                        bw_ImportSetDetails.ReportProgress(lineIndex, "Working...");
                        string setRef = "";
                        try
                        {
                            // ** Get variables **
                            string[] columnDetails = line.Split(',');
                            setRef = columnDetails[0];
                            string setDescription = columnDetails[1];
                            List<string> themeDetails = columnDetails[2].Split('-').ToList();
                            string theme = themeDetails[0].Trim();
                            string subTheme = "";
                            if (themeDetails.Count > 1) subTheme = themeDetails[1].Trim();
                            int setYear = int.Parse(columnDetails[3]);

                            // Check if set already exists - if so, don't import again.
                            Delegates.ToolStripLabel_SetText(this, lblStatus, "Importing file | Processed (" + lineIndex.ToString("#,##0") + " of " + fileLines.Length.ToString("#,##0") + ") " + setRef + ": " + setDescription);
                            bool exists = StaticData.CheckIfSetDetailExists(setRef);
                            if (exists)
                            {
                                ignoredSetCount += 1;
                            }
                            else
                            {
                                // ** Generate new Set Details object **
                                SetDetails setDetails = new SetDetails();
                                setDetails.Ref = setRef;
                                setDetails.Description = setDescription;
                                setDetails.Type = "OFFICIAL";
                                setDetails.Theme = theme;
                                setDetails.SubTheme = subTheme;
                                setDetails.Year = setYear;
                                setDetails.Status = "NOT_STARTED";
                                setDetails.PartCount = 0;
                                setDetails.SubSetCount = 1;
                                setDetails.ModelCount = 1;
                                setDetails.MiniFigCount = 0;

                                // ** Generate base instructions and add to Set Details **
                                Set set = Set.GenerateBaseSet(setRef, setDescription, "OFFICIAL");
                                setDetails.Instructions = set.SerializeToString(true);

                                // ** Add Set to DB **
                                StaticData.AddSetDetails(setDetails);

                                // ** Get image for set and upload to BLOB **
                                ArfaImage.GetImage(ImageType.SET, new string[] { setDetails.Ref });

                                importedSetCount += 1;
                            }
                        }
                        catch(Exception ex)
                        {
                            failedImportList.Add(setRef + "|" + ex.Message);
                        }
                    }
                    lineIndex += 1;
                }
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_ImportSetDetails(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");

                // ** Show confirmation **
                string msg = "Successfully uploaded " + importedSetCount + " sets." + Environment.NewLine;
                msg += "Ignored imports: " + ignoredSetCount + Environment.NewLine;
                msg += "Failed imports: " + failedImportList.Count + Environment.NewLine;
                foreach(string setRef in failedImportList) msg += setRef + Environment.NewLine;                
                MessageBox.Show(msg);
            }
            catch (Exception ex)
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                EnableControls_ImportSetDetails(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);                
            }
        }
             
        #endregion




    }
}


       







