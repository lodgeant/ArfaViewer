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



namespace Generator
{
    public partial class MatchingScreen : Form
    {
        public DataTable sourceTable = new DataTable();
        public DataTable targetTable = new DataTable();
        public string sourceTableName;
        public string targetTableName;



        public MatchingScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Matching Screen";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                
                // ** FORMAT SUMMARIES **
                string[] DGnames = new string[] { "dgSourcePartSummary", "dgTargetPartSummary" };
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

                // ** RESET LABELS **
                lblSourcePartCount.Text = "";
                lblTargetPartCount.Text = "";                
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
            }
        }
        
        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            Refresh_Screen();
        }

        private void dgSourcePartSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustPartSummaryRowFormatting(dgSourcePartSummary);
        }

        private void dgTargetPartSummary_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            AdjustPartSummaryRowFormatting(dgTargetPartSummary);
        }

        #endregion

        #region ** REFRESH SCREEN FUNCTIONS **

        public void Refresh_Screen()
        {
            try
            {
                // ** POST SOURCE **
                dgSourcePartSummary.DataSource = sourceTable;
                AdjustPartSummaryRowFormatting(dgSourcePartSummary);
                lblSourcePartCount.Text = sourceTable.Rows.Count.ToString("#,##0") + " item(s)";
                gpSource.Text = "Source [" + sourceTableName + "]";

                // ** POST TARGET **
                dgTargetPartSummary.DataSource = targetTable;
                AdjustPartSummaryRowFormatting(dgTargetPartSummary);
                lblTargetPartCount.Text = targetTable.Rows.Count.ToString("#,##0") + " item(s)";
                gpTarget.Text = "Target [" + targetTableName + "]";

                //AdjustRowColours();
            }
            catch (Exception ex)
            {
                MessageBox.Show("ERROR: " + new StackTrace(ex).GetFrame(0).GetMethod().Name + "|" + (new StackTrace(ex, true)).GetFrame(0).GetFileLineNumber() + ": " + ex.Message);
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
                if (dg.Columns["Matched"] != null)
                {
                    dg.Columns["Matched"].Visible = false;
                }
                dg.AutoResizeColumns();

                // ** Change colours of rows **
                foreach (DataGridViewRow row in dg.Rows)
                {
                    //if (row.Cells["Is SubPart"].Value.ToString().Equals("True"))
                    //{
                    //    row.DefaultCellStyle.Font = new Font(this.Font, FontStyle.Italic);
                    //    row.DefaultCellStyle.ForeColor = Color.Gray;
                    //}
                    if (dg.Columns["Is SubPart"] != null)
                    {
                        if (row.Cells["Is SubPart"].Value.ToString().Equals("True"))
                        {
                            row.DefaultCellStyle.Font = new Font(this.Font, FontStyle.Italic);
                            row.DefaultCellStyle.ForeColor = Color.Gray;
                        }
                    }
                    if (row.Cells["Matched"].Value.ToString().Equals("True"))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightGreen;
                    }
                    if (row.Cells["Matched"].Value.ToString().Equals("False"))
                    {
                        row.DefaultCellStyle.BackColor = Color.LightSalmon;
                    }
                }
            }
        }
        
        private void AdjustRowColours()
        {
            try
            {
                for (int a = 0; a < sourceTable.Rows.Count; a++)
                {
                    bool match = true;
                    String source_LDrawRef = sourceTable.Rows[a]["LDraw Ref"].ToString();
                    String source_LDrawColourID = sourceTable.Rows[a]["LDraw Colour ID"].ToString();                    
                    String target_LDrawRef = "";
                    String target_LDrawColourID = "";
                    if (a < targetTable.Rows.Count)
                    {
                        target_LDrawRef = targetTable.Rows[a]["LDraw Ref"].ToString();
                        target_LDrawColourID = targetTable.Rows[a]["LDraw Colour ID"].ToString();
                    }
                    if (source_LDrawRef != target_LDrawRef)
                    {
                        match = false;
                    }
                    if (source_LDrawColourID != target_LDrawColourID)
                    {
                        match = false;
                    }
                    if (match == false)
                    {
                        dgSourcePartSummary.Rows[a].DefaultCellStyle.BackColor = Color.LightSalmon;
                        if (a < targetTable.Rows.Count)
                        {
                            dgTargetPartSummary.Rows[a].DefaultCellStyle.BackColor = Color.LightSalmon;
                        }                            
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        
    }
}

