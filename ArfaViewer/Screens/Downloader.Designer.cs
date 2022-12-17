namespace Generator
{
    partial class Downloader
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Downloader));
            this.tsHeader1 = new System.Windows.Forms.ToolStrip();
            this.ts_Exit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.fldBaseUrl = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.fldStartIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.fldEndIndex = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.fldMask = new System.Windows.Forms.ToolStripTextBox();
            this.fldPostStringLabel = new System.Windows.Forms.ToolStripLabel();
            this.fldPostString = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.fldExt = new System.Windows.Forms.ToolStripTextBox();
            this.btnDownload = new System.Windows.Forms.ToolStripButton();
            this.pbDownloadStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblDownloadDataStatus = new System.Windows.Forms.ToolStripLabel();
            this.tsHeader1.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tsHeader1
            // 
            this.tsHeader1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ts_Exit,
            this.toolStripSeparator9,
            this.toolStripLabel1,
            this.fldBaseUrl});
            this.tsHeader1.Location = new System.Drawing.Point(0, 0);
            this.tsHeader1.Name = "tsHeader1";
            this.tsHeader1.Size = new System.Drawing.Size(1468, 25);
            this.tsHeader1.TabIndex = 387;
            this.tsHeader1.Text = "toolStrip12";
            // 
            // ts_Exit
            // 
            this.ts_Exit.Image = ((System.Drawing.Image)(resources.GetObject("ts_Exit.Image")));
            this.ts_Exit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ts_Exit.Name = "ts_Exit";
            this.ts_Exit.Size = new System.Drawing.Size(46, 22);
            this.ts_Exit.Text = "Exit";
            this.ts_Exit.Click += new System.EventHandler(this.ts_Exit_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(49, 22);
            this.toolStripLabel1.Text = "BaseUrl:";
            // 
            // fldBaseUrl
            // 
            this.fldBaseUrl.BackColor = System.Drawing.Color.LightGray;
            this.fldBaseUrl.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldBaseUrl.Name = "fldBaseUrl";
            this.fldBaseUrl.Size = new System.Drawing.Size(1000, 25);
            this.fldBaseUrl.Text = "http://galleries.cosmid.net/2575/";
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel2,
            this.fldStartIndex,
            this.toolStripLabel3,
            this.fldEndIndex,
            this.toolStripLabel4,
            this.fldMask,
            this.fldPostStringLabel,
            this.fldPostString,
            this.toolStripLabel5,
            this.fldExt,
            this.btnDownload,
            this.pbDownloadStatus,
            this.lblDownloadDataStatus});
            this.toolStrip1.Location = new System.Drawing.Point(0, 25);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1468, 25);
            this.toolStrip1.TabIndex = 388;
            this.toolStrip1.Text = "toolStrip12";
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel2.Text = "Start Index";
            // 
            // fldStartIndex
            // 
            this.fldStartIndex.BackColor = System.Drawing.Color.Wheat;
            this.fldStartIndex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStartIndex.Name = "fldStartIndex";
            this.fldStartIndex.Size = new System.Drawing.Size(100, 25);
            this.fldStartIndex.Text = "1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(59, 22);
            this.toolStripLabel3.Text = "End Index";
            // 
            // fldEndIndex
            // 
            this.fldEndIndex.BackColor = System.Drawing.Color.Wheat;
            this.fldEndIndex.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldEndIndex.Name = "fldEndIndex";
            this.fldEndIndex.Size = new System.Drawing.Size(100, 25);
            this.fldEndIndex.Text = "16";
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(35, 22);
            this.toolStripLabel4.Text = "Mask";
            // 
            // fldMask
            // 
            this.fldMask.BackColor = System.Drawing.Color.Wheat;
            this.fldMask.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldMask.Name = "fldMask";
            this.fldMask.Size = new System.Drawing.Size(30, 25);
            this.fldMask.Text = "00";
            // 
            // fldPostStringLabel
            // 
            this.fldPostStringLabel.Name = "fldPostStringLabel";
            this.fldPostStringLabel.Size = new System.Drawing.Size(64, 22);
            this.fldPostStringLabel.Text = "Post String";
            // 
            // fldPostString
            // 
            this.fldPostString.BackColor = System.Drawing.Color.Wheat;
            this.fldPostString.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPostString.Name = "fldPostString";
            this.fldPostString.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(26, 22);
            this.toolStripLabel5.Text = "Ext.";
            // 
            // fldExt
            // 
            this.fldExt.BackColor = System.Drawing.Color.Wheat;
            this.fldExt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldExt.Name = "fldExt";
            this.fldExt.Size = new System.Drawing.Size(50, 25);
            this.fldExt.Text = "jpg";
            // 
            // btnDownload
            // 
            this.btnDownload.Image = ((System.Drawing.Image)(resources.GetObject("btnDownload.Image")));
            this.btnDownload.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(81, 22);
            this.btnDownload.Text = "Download";
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // pbDownloadStatus
            // 
            this.pbDownloadStatus.Name = "pbDownloadStatus";
            this.pbDownloadStatus.Size = new System.Drawing.Size(100, 22);
            // 
            // lblDownloadDataStatus
            // 
            this.lblDownloadDataStatus.Name = "lblDownloadDataStatus";
            this.lblDownloadDataStatus.Size = new System.Drawing.Size(130, 22);
            this.lblDownloadDataStatus.Text = "lblDownloadDataStatus";
            // 
            // Downloader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1468, 61);
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.tsHeader1);
            this.Name = "Downloader";
            this.Text = "Downloader";
            this.tsHeader1.ResumeLayout(false);
            this.tsHeader1.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip tsHeader1;
        private System.Windows.Forms.ToolStripButton ts_Exit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fldBaseUrl;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox fldStartIndex;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox fldEndIndex;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripTextBox fldMask;
        private System.Windows.Forms.ToolStripLabel fldPostStringLabel;
        private System.Windows.Forms.ToolStripTextBox fldPostString;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox fldExt;
        private System.Windows.Forms.ToolStripButton btnDownload;
        private System.Windows.Forms.ToolStripProgressBar pbDownloadStatus;
        private System.Windows.Forms.ToolStripLabel lblDownloadDataStatus;
    }
}