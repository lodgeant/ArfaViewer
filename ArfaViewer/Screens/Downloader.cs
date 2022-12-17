using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
    public partial class Downloader : Form
    {
        private string targetPath = @"D:\TONY\webp\new1";

        public Downloader()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Downloader";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];

               
                lblDownloadDataStatus.Text = "";
                //lblImportDataStatus.Text = "";
                //fldBaseUrl.Text = "https://cdni.pornpics.com/1280/5/66/65966579/65966579_001_";
                //fldBaseUrl.Text = "http://galleries.haileyshideaway.com/sheer/"; //001

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        #region ** BUTTON FUNCTIONS **

        private void ts_Exit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            DownloadData();
        }

        #endregion

        #region ** DOWNLOAD FUNCTIONS **

        private BackgroundWorker bw_DownloadData;
        private void EnableControls_DownloadData(bool value)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new MethodInvoker(() => EnableControls_DownloadData(value)));
            }
            else
            {
                ts_Exit.Enabled = value;
                fldBaseUrl.Enabled = value;
                fldStartIndex.Enabled = value;
                fldEndIndex.Enabled = value;
            }
        }

        private void DownloadData()
        {
            try
            {
                if (btnDownload.Text.Equals("Download"))
                {
                    // ** Update status toolstrip **                
                    btnDownload.Text = "Stop downloading";
                    btnDownload.ForeColor = Color.Red;
                    EnableControls_DownloadData(false);

                    // ** Run background to process functions **
                    bw_DownloadData = new BackgroundWorker
                    {
                        WorkerReportsProgress = true,
                        WorkerSupportsCancellation = true
                    };
                    bw_DownloadData.DoWork += new DoWorkEventHandler(bw_DownloadData_DoWork);
                    bw_DownloadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_DownloadData_RunWorkerCompleted);
                    bw_DownloadData.ProgressChanged += new ProgressChangedEventHandler(bw_DownloadData_ProgressChanged);
                    bw_DownloadData.RunWorkerAsync();
                }
                else
                {
                    Delegates.ToolStripButton_SetText(this, btnDownload, "Cancelling...");
                    Delegates.ToolStripLabel_SetText(this, lblDownloadDataStatus, "Cancelling...");
                    bw_DownloadData.CancelAsync();
                }
            }
            catch (Exception ex)
            {
                Delegates.ToolStripButton_SetText(this, btnDownload, "Download");
                btnDownload.ForeColor = Color.Black;
                pbDownloadStatus.Value = 0;
                EnableControls_DownloadData(true);
                Delegates.ToolStripLabel_SetText(this, lblDownloadDataStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_DownloadData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbDownloadStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_DownloadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                Delegates.ToolStripButton_SetTextAndForeColor(this, btnDownload, "Download", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbDownloadStatus, 0);
                EnableControls_DownloadData(true);
                Delegates.ToolStripLabel_SetText(this, lblDownloadDataStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_DownloadData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                // ** Determine next index **
                //List<string> fileList = Directory.GetFiles(targetPath).ToList();
                //string lastIndex = Path.GetFileNameWithoutExtension(fileList[fileList.Count-1]);
                List<int> fileList = Directory.GetFiles(targetPath)
                                        .Select(Path.GetFileNameWithoutExtension)
                                        .Select(p => int.Parse(p)).ToList();
                int lastIndex = fileList.Max();
                int index = lastIndex + 1;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                // ** Get download location details **
                //String baseUrl = "http://galleries.cosmid.net/2575/";                                  
                string baseUrl = fldBaseUrl.Text;
                int startIndex = int.Parse(fldStartIndex.Text);
                int endIndex = int.Parse(fldEndIndex.Text);
                string mask = fldMask.Text;
                int imageCount = 0;
                Delegates.ToolStripProgressBar_SetMax(this, pbDownloadStatus, endIndex);
                for (int a = startIndex; a < endIndex + 1; a++)
                {
                    // ** Download data **                   
                    string imageUrl = baseUrl + a.ToString(mask) + fldPostString.Text + "." + fldExt.Text;
                    Delegates.ToolStripLabel_SetText(this, lblDownloadDataStatus, "Downloading " + imageUrl + " to " + index.ToString("0000") + ".jpg");
                    bw_DownloadData.ReportProgress(imageCount, "Working...");
                    if (bw_DownloadData.CancellationPending == false)
                    {
                        byte[] imageb = new WebClient().DownloadData(imageUrl);
                        if (imageb.Length > 0)
                        {
                            string targetFilename = targetPath + "\\" + index.ToString("0000") + ".jpg";
                            using (var fs = new FileStream(targetFilename, FileMode.Create, FileAccess.Write))
                            {
                                fs.Write(imageb, 0, imageb.Length);
                            }
                            index += 1;
                            imageCount += 1;
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
