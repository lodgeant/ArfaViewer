namespace Generator
{
    partial class MatchingScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MatchingScreen));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnRefresh = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gpSource = new System.Windows.Forms.GroupBox();
            this.dgSourcePartSummary = new System.Windows.Forms.DataGridView();
            this.chkIsLargeModel = new System.Windows.Forms.CheckBox();
            this.chkIsSticker = new System.Windows.Forms.CheckBox();
            this.c = new System.Windows.Forms.StatusStrip();
            this.lblSourcePartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.gpTarget = new System.Windows.Forms.GroupBox();
            this.dgTargetPartSummary = new System.Windows.Forms.DataGridView();
            this.checkBox1 = new System.Windows.Forms.CheckBox();
            this.checkBox2 = new System.Windows.Forms.CheckBox();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblTargetPartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpSource.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSourcePartSummary)).BeginInit();
            this.c.SuspendLayout();
            this.gpTarget.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTargetPartSummary)).BeginInit();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit,
            this.toolStripSeparator1,
            this.btnRefresh});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1207, 25);
            this.toolStrip1.TabIndex = 26;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // btnExit
            // 
            this.btnExit.Image = ((System.Drawing.Image)(resources.GetObject("btnExit.Image")));
            this.btnExit.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(46, 22);
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
            // 
            // btnRefresh
            // 
            this.btnRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnRefresh.Image")));
            this.btnRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnRefresh.Text = "Refresh";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gpSource);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gpTarget);
            this.splitContainer1.Size = new System.Drawing.Size(1207, 675);
            this.splitContainer1.SplitterDistance = 559;
            this.splitContainer1.TabIndex = 37;
            // 
            // gpSource
            // 
            this.gpSource.Controls.Add(this.dgSourcePartSummary);
            this.gpSource.Controls.Add(this.chkIsLargeModel);
            this.gpSource.Controls.Add(this.chkIsSticker);
            this.gpSource.Controls.Add(this.c);
            this.gpSource.Controls.Add(this.toolStrip4);
            this.gpSource.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpSource.Location = new System.Drawing.Point(0, 0);
            this.gpSource.Name = "gpSource";
            this.gpSource.Size = new System.Drawing.Size(559, 675);
            this.gpSource.TabIndex = 101;
            this.gpSource.TabStop = false;
            this.gpSource.Text = "Source";
            // 
            // dgSourcePartSummary
            // 
            this.dgSourcePartSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgSourcePartSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSourcePartSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSourcePartSummary.Location = new System.Drawing.Point(3, 41);
            this.dgSourcePartSummary.Name = "dgSourcePartSummary";
            this.dgSourcePartSummary.Size = new System.Drawing.Size(553, 609);
            this.dgSourcePartSummary.TabIndex = 77;
            this.dgSourcePartSummary.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgSourcePartSummary_ColumnHeaderMouseClick);
            // 
            // chkIsLargeModel
            // 
            this.chkIsLargeModel.AutoSize = true;
            this.chkIsLargeModel.Location = new System.Drawing.Point(596, 433);
            this.chkIsLargeModel.Name = "chkIsLargeModel";
            this.chkIsLargeModel.Size = new System.Drawing.Size(102, 17);
            this.chkIsLargeModel.TabIndex = 76;
            this.chkIsLargeModel.Text = "Is Large Model?";
            this.chkIsLargeModel.UseVisualStyleBackColor = false;
            // 
            // chkIsSticker
            // 
            this.chkIsSticker.AutoSize = true;
            this.chkIsSticker.Location = new System.Drawing.Point(503, 433);
            this.chkIsSticker.Name = "chkIsSticker";
            this.chkIsSticker.Size = new System.Drawing.Size(76, 17);
            this.chkIsSticker.TabIndex = 75;
            this.chkIsSticker.Text = "Is Sticker?";
            this.chkIsSticker.UseVisualStyleBackColor = false;
            // 
            // c
            // 
            this.c.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSourcePartCount});
            this.c.Location = new System.Drawing.Point(3, 650);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(553, 22);
            this.c.TabIndex = 67;
            this.c.Text = "statusStrip2";
            // 
            // lblSourcePartCount
            // 
            this.lblSourcePartCount.Name = "lblSourcePartCount";
            this.lblSourcePartCount.Size = new System.Drawing.Size(110, 17);
            this.lblSourcePartCount.Text = "lblSourcePartCount";
            // 
            // toolStrip4
            // 
            this.toolStrip4.Location = new System.Drawing.Point(3, 16);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(553, 25);
            this.toolStrip4.TabIndex = 26;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // gpTarget
            // 
            this.gpTarget.Controls.Add(this.dgTargetPartSummary);
            this.gpTarget.Controls.Add(this.checkBox1);
            this.gpTarget.Controls.Add(this.checkBox2);
            this.gpTarget.Controls.Add(this.statusStrip1);
            this.gpTarget.Controls.Add(this.toolStrip2);
            this.gpTarget.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpTarget.Location = new System.Drawing.Point(0, 0);
            this.gpTarget.Name = "gpTarget";
            this.gpTarget.Size = new System.Drawing.Size(644, 675);
            this.gpTarget.TabIndex = 102;
            this.gpTarget.TabStop = false;
            this.gpTarget.Text = "Target";
            // 
            // dgTargetPartSummary
            // 
            this.dgTargetPartSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgTargetPartSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgTargetPartSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgTargetPartSummary.Location = new System.Drawing.Point(3, 41);
            this.dgTargetPartSummary.Name = "dgTargetPartSummary";
            this.dgTargetPartSummary.Size = new System.Drawing.Size(638, 609);
            this.dgTargetPartSummary.TabIndex = 77;
            this.dgTargetPartSummary.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dgTargetPartSummary_ColumnHeaderMouseClick);
            // 
            // checkBox1
            // 
            this.checkBox1.AutoSize = true;
            this.checkBox1.Location = new System.Drawing.Point(596, 433);
            this.checkBox1.Name = "checkBox1";
            this.checkBox1.Size = new System.Drawing.Size(102, 17);
            this.checkBox1.TabIndex = 76;
            this.checkBox1.Text = "Is Large Model?";
            this.checkBox1.UseVisualStyleBackColor = false;
            // 
            // checkBox2
            // 
            this.checkBox2.AutoSize = true;
            this.checkBox2.Location = new System.Drawing.Point(503, 433);
            this.checkBox2.Name = "checkBox2";
            this.checkBox2.Size = new System.Drawing.Size(76, 17);
            this.checkBox2.TabIndex = 75;
            this.checkBox2.Text = "Is Sticker?";
            this.checkBox2.UseVisualStyleBackColor = false;
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblTargetPartCount});
            this.statusStrip1.Location = new System.Drawing.Point(3, 650);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(638, 22);
            this.statusStrip1.TabIndex = 67;
            this.statusStrip1.Text = "statusStrip2";
            // 
            // lblTargetPartCount
            // 
            this.lblTargetPartCount.Name = "lblTargetPartCount";
            this.lblTargetPartCount.Size = new System.Drawing.Size(106, 17);
            this.lblTargetPartCount.Text = "lblTargetPartCount";
            // 
            // toolStrip2
            // 
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(638, 25);
            this.toolStrip2.TabIndex = 26;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // MatchingScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1207, 700);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MatchingScreen";
            this.Text = "MatchingScreen";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpSource.ResumeLayout(false);
            this.gpSource.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSourcePartSummary)).EndInit();
            this.c.ResumeLayout(false);
            this.c.PerformLayout();
            this.gpTarget.ResumeLayout(false);
            this.gpTarget.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgTargetPartSummary)).EndInit();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripButton btnRefresh;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gpSource;
        private System.Windows.Forms.DataGridView dgSourcePartSummary;
        private System.Windows.Forms.CheckBox chkIsLargeModel;
        private System.Windows.Forms.CheckBox chkIsSticker;
        private System.Windows.Forms.StatusStrip c;
        private System.Windows.Forms.ToolStripStatusLabel lblSourcePartCount;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.GroupBox gpTarget;
        private System.Windows.Forms.DataGridView dgTargetPartSummary;
        private System.Windows.Forms.CheckBox checkBox1;
        private System.Windows.Forms.CheckBox checkBox2;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblTargetPartCount;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}