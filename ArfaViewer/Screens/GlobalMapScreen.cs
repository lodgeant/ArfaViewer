using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


namespace Generator.Screens
{
    public partial class GlobalMapScreen : Form
    {
        public GlobalMapScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Global Map Screen";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];

                #region ** FORMAT SUMMARIES **
                string[] DGnames = new string[] { "dgRouteRequestSummary" };
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
                #endregion

                #region ** ADD MAIN HEADER LINE TOOLSTRIP ITEMS **
                //toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
                //                btnExit,
                //                toolStripSeparator1,
                //                btnRefreshScreen,
                //                toolStripSeparator4,
                //                lblSquareSize,
                //                fldSquareSize,
                //                toolStripSeparator5,
                //                btnLoadMapFromCode,
                //                toolStripSeparator3,
                //                fldBitmapFilePath,
                //                btnSelectBitmapFile,
                //                btnLoadMapFromBitmap,
                //                toolStripSeparator2,
                //                lblMapFactor,
                //                fldMapFactor,
                //                lblTopLeft,
                //                fldTopLeft,
                //                lblMapWidth,
                //                fldMapWidth,
                //                lblMapHeight,
                //                fldMapHeight,
                //                toolStripSeparator6,

                //                lblUnityTopLeft,
                //                fldUnityTopLeft,

                //                lblUnityMapWidth,
                //                fldUnityMapWidth,
                //                lblUnityMapHeight,
                //                fldUnityMapHeight
                //                });
                #endregion

                // ** Clear initial labels **
                //lblCoreSummaryStatus.Text = "";
                //lblRealSummaryStatus.Text = "";
                //lblBitmapSummaryStatus.Text = "";
                //fldSquareSize.Text = squareSize.ToString();

                //fldBitmapFilePath.Text = @"C:\SETTLERS STUFF\MAP3.png";
                //fldTopLeft.Text = "(-40, 25)";
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

        private void btnRefreshScreen_Click(object sender, EventArgs e)
        {
            RefreshScreen();
        }

        #endregion









        private void RefreshScreen()
        {
            try
            {


                // Load GlobalMap XML **
                string xmlString = File.ReadAllText(@"Z:\SETTLERS STUFF\GlobalMap.xml");
                GlobalMap globalMap = new GlobalMap().DeserialiseFromXMLString(xmlString);

                string test = "";


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




    }
}
