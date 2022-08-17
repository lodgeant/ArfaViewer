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
                #endregion

                RefreshScreen();
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
            RefreshScreen();
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

        #endregion

        #region ** REFRESH FUNCTIONS **

        private void RefreshScreen()
        {
            try
            {
                // ** Get all Theme Details from SET_DETAILS **
                BaseClasses.ThemeDetailsCollection ThemeDetailsCollection = StaticData.GetAllThemeDetails();

                // ** Convert ThemeDetails into TreeNode **
                TreeNode[] ThemeTreeNodes = ThemeDetailsCollection.ConvertToTreeNodeList();

                // ** Add Theme counts **                
                ThemeTreeNodes = UpdateThemeCounts(ThemeTreeNodes);

                // ** Set tvThemesSummary **
                tvThemesSummary.Nodes.Clear();
                tvThemesSummary.Nodes.AddRange(ThemeTreeNodes);

                // ** Set Theme dropdown values **
                fldTheme.Items.Clear();
                foreach(ThemeDetails td in ThemeDetailsCollection.ThemeDetailsList) fldTheme.Items.Add(td.Theme);
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
                List<string> themeStringList = tvThemesSummary.SelectedNode.Tag.ToString().Split('|').ToList();
                string theme = themeStringList[0];
                string subTheme = "";
                if (themeStringList.Count > 1) subTheme = themeStringList[1];


                RefreshSetDetailsSummary();


                //MessageBox.Show("Theme: " + theme + " | SubTheme: " + subTheme);


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

        private void RefreshSetDetailsSummary()
        {
            try
            {
                // ** Clear fields **
                dgSetDetailsSummary.DataSource = null;
                lblSetDetailsCount.Text = "";

                // ** Determine Theme & SubTheme **                
                //List<string> themeStringList = tvThemesSummary.SelectedNode.Tag.ToString().Split('|').ToList();
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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private DataTable GenerateSetDetailsTable(SetDetailsCollection coll)
        {
            try
            {
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
                    newRow["Instructions Exist"] = StaticData.CheckIfPDFInstructionsExistForSet(sd.Ref);
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
                RefreshScreen();
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
                RefreshScreen();
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
            fldTheme.Text = "";
            fldSubTheme.Text = "";
            fldYear.Text = "";
            fldStatus.Text = "";
            fldAssignedTo.Text = "";
            fldInstructionRefs.Text = "";
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


        #endregion


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
            try
            {
                #region ** VALIDATIONS **
                if (fldSetRef.Text.Equals("")) throw new Exception("No Set Ref entered...");
                if (fldInstructionRefs.Text.Equals("")) throw new Exception("No Instruction Refs entered...");
                string setRef = fldSetRef.Text;
                List<string> insRefList = fldInstructionRefs.Text.Split(',').ToList();

                // ** Check whether instructions are already present. If they are, confirm whether they should be downloaded again **
                ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
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
                share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
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




    }
}


       







