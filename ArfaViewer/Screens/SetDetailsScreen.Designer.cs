namespace Generator
{
    partial class SetDetailsScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SetDetailsScreen));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvThemesSummary = new System.Windows.Forms.TreeView();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnThemesRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCollapseNodes = new System.Windows.Forms.ToolStripButton();
            this.btnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgSetDetailsSummary = new System.Windows.Forms.DataGridView();
            this.c = new System.Windows.Forms.StatusStrip();
            this.lblPartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPartSummaryItemFilteredCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPartSummary = new System.Windows.Forms.ToolStrip();
            this.btnPartSummaryCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tsPartDetails = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawColourID = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawColourName = new System.Windows.Forms.ToolStripComboBox();
            this.lblQty = new System.Windows.Forms.ToolStripLabel();
            this.fldQty = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.fldPlacementMovements = new System.Windows.Forms.ToolStripTextBox();
            this.btnPartClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPartAdd = new System.Windows.Forms.ToolStripButton();
            this.btnPartSave = new System.Windows.Forms.ToolStripButton();
            this.btnPartDelete = new System.Windows.Forms.ToolStripButton();
            this.chkShowAll = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSetDetailsSummary)).BeginInit();
            this.c.SuspendLayout();
            this.tsPartSummary.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tsPartDetails.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1347, 25);
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
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvThemesSummary);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Location = new System.Drawing.Point(30, 73);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(282, 429);
            this.groupBox1.TabIndex = 27;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Themes";
            // 
            // tvThemesSummary
            // 
            this.tvThemesSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvThemesSummary.HideSelection = false;
            this.tvThemesSummary.Location = new System.Drawing.Point(3, 41);
            this.tvThemesSummary.Name = "tvThemesSummary";
            this.tvThemesSummary.Size = new System.Drawing.Size(276, 385);
            this.tvThemesSummary.TabIndex = 85;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnThemesRefresh,
            this.toolStripSeparator21,
            this.btnCollapseNodes,
            this.btnExpandAll,
            this.toolStripSeparator6});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(276, 25);
            this.toolStrip2.TabIndex = 27;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnThemesRefresh
            // 
            this.btnThemesRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnThemesRefresh.Image")));
            this.btnThemesRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnThemesRefresh.Name = "btnThemesRefresh";
            this.btnThemesRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnThemesRefresh.Text = "Refresh";
            // 
            // toolStripSeparator21
            // 
            this.toolStripSeparator21.Name = "toolStripSeparator21";
            this.toolStripSeparator21.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCollapseNodes
            // 
            this.btnCollapseNodes.Image = ((System.Drawing.Image)(resources.GetObject("btnCollapseNodes.Image")));
            this.btnCollapseNodes.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCollapseNodes.Name = "btnCollapseNodes";
            this.btnCollapseNodes.Size = new System.Drawing.Size(23, 22);
            this.btnCollapseNodes.ToolTipText = "Collapse";
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(23, 22);
            this.btnExpandAll.ToolTipText = "Expand All";
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.chkShowAll);
            this.groupBox2.Controls.Add(this.dgSetDetailsSummary);
            this.groupBox2.Controls.Add(this.c);
            this.groupBox2.Controls.Add(this.tsPartSummary);
            this.groupBox2.Location = new System.Drawing.Point(331, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(985, 429);
            this.groupBox2.TabIndex = 28;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set Details Summary for Theme";
            // 
            // dgSetDetailsSummary
            // 
            this.dgSetDetailsSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgSetDetailsSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSetDetailsSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSetDetailsSummary.Location = new System.Drawing.Point(3, 41);
            this.dgSetDetailsSummary.Name = "dgSetDetailsSummary";
            this.dgSetDetailsSummary.Size = new System.Drawing.Size(979, 363);
            this.dgSetDetailsSummary.TabIndex = 78;
            // 
            // c
            // 
            this.c.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPartCount,
            this.lblPartSummaryItemFilteredCount});
            this.c.Location = new System.Drawing.Point(3, 404);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(979, 22);
            this.c.TabIndex = 68;
            this.c.Text = "statusStrip2";
            // 
            // lblPartCount
            // 
            this.lblPartCount.Name = "lblPartCount";
            this.lblPartCount.Size = new System.Drawing.Size(74, 17);
            this.lblPartCount.Text = "lblPartCount";
            // 
            // lblPartSummaryItemFilteredCount
            // 
            this.lblPartSummaryItemFilteredCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPartSummaryItemFilteredCount.ForeColor = System.Drawing.Color.Blue;
            this.lblPartSummaryItemFilteredCount.Name = "lblPartSummaryItemFilteredCount";
            this.lblPartSummaryItemFilteredCount.Size = new System.Drawing.Size(199, 17);
            this.lblPartSummaryItemFilteredCount.Text = "lblPartSummaryItemFilteredCount";
            // 
            // tsPartSummary
            // 
            this.tsPartSummary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPartSummaryCopyToClipboard,
            this.toolStripSeparator7});
            this.tsPartSummary.Location = new System.Drawing.Point(3, 16);
            this.tsPartSummary.Name = "tsPartSummary";
            this.tsPartSummary.Size = new System.Drawing.Size(979, 25);
            this.tsPartSummary.TabIndex = 27;
            this.tsPartSummary.Text = "toolStrip4";
            // 
            // btnPartSummaryCopyToClipboard
            // 
            this.btnPartSummaryCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnPartSummaryCopyToClipboard.Image")));
            this.btnPartSummaryCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartSummaryCopyToClipboard.Name = "btnPartSummaryCopyToClipboard";
            this.btnPartSummaryCopyToClipboard.Size = new System.Drawing.Size(124, 22);
            this.btnPartSummaryCopyToClipboard.Text = "Copy to Clipboard";
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tsPartDetails);
            this.groupBox3.Location = new System.Drawing.Point(30, 522);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1307, 96);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set Details";
            // 
            // tsPartDetails
            // 
            this.tsPartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.fldLDrawRef,
            this.toolStripLabel2,
            this.fldLDrawColourID,
            this.toolStripLabel4,
            this.fldLDrawColourName,
            this.lblQty,
            this.fldQty,
            this.toolStripLabel3,
            this.fldPlacementMovements,
            this.btnPartClear,
            this.toolStripSeparator3,
            this.btnPartAdd,
            this.btnPartSave,
            this.btnPartDelete});
            this.tsPartDetails.Location = new System.Drawing.Point(3, 16);
            this.tsPartDetails.Name = "tsPartDetails";
            this.tsPartDetails.Size = new System.Drawing.Size(1301, 25);
            this.tsPartDetails.TabIndex = 76;
            this.tsPartDetails.Text = "toolStrip5";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel1.Text = "Ref:";
            // 
            // fldLDrawRef
            // 
            this.fldLDrawRef.BackColor = System.Drawing.Color.LightGray;
            this.fldLDrawRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawRef.Name = "fldLDrawRef";
            this.fldLDrawRef.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(96, 22);
            this.toolStripLabel2.Text = "LDraw Colour ID:";
            // 
            // fldLDrawColourID
            // 
            this.fldLDrawColourID.BackColor = System.Drawing.Color.LightGray;
            this.fldLDrawColourID.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawColourID.Name = "fldLDrawColourID";
            this.fldLDrawColourID.Size = new System.Drawing.Size(30, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel4.Text = "LDraw Colour Name:";
            // 
            // fldLDrawColourName
            // 
            this.fldLDrawColourName.BackColor = System.Drawing.Color.LightGray;
            this.fldLDrawColourName.Name = "fldLDrawColourName";
            this.fldLDrawColourName.Size = new System.Drawing.Size(150, 25);
            // 
            // lblQty
            // 
            this.lblQty.BackColor = System.Drawing.Color.Yellow;
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(29, 22);
            this.lblQty.Text = "Qty:";
            // 
            // fldQty
            // 
            this.fldQty.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldQty.Name = "fldQty";
            this.fldQty.Size = new System.Drawing.Size(30, 25);
            this.fldQty.Text = "1";
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel3.Text = "PMs:";
            // 
            // fldPlacementMovements
            // 
            this.fldPlacementMovements.BackColor = System.Drawing.Color.LightGray;
            this.fldPlacementMovements.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPlacementMovements.Name = "fldPlacementMovements";
            this.fldPlacementMovements.Size = new System.Drawing.Size(75, 25);
            this.fldPlacementMovements.Text = "Y=-5";
            // 
            // btnPartClear
            // 
            this.btnPartClear.Image = ((System.Drawing.Image)(resources.GetObject("btnPartClear.Image")));
            this.btnPartClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartClear.Name = "btnPartClear";
            this.btnPartClear.Size = new System.Drawing.Size(54, 22);
            this.btnPartClear.Text = "Clear";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPartAdd
            // 
            this.btnPartAdd.Image = ((System.Drawing.Image)(resources.GetObject("btnPartAdd.Image")));
            this.btnPartAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartAdd.Name = "btnPartAdd";
            this.btnPartAdd.Size = new System.Drawing.Size(49, 22);
            this.btnPartAdd.Text = "Add";
            // 
            // btnPartSave
            // 
            this.btnPartSave.Image = ((System.Drawing.Image)(resources.GetObject("btnPartSave.Image")));
            this.btnPartSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartSave.Name = "btnPartSave";
            this.btnPartSave.Size = new System.Drawing.Size(51, 22);
            this.btnPartSave.Text = "Save";
            // 
            // btnPartDelete
            // 
            this.btnPartDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnPartDelete.Image")));
            this.btnPartDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartDelete.Name = "btnPartDelete";
            this.btnPartDelete.Size = new System.Drawing.Size(60, 22);
            this.btnPartDelete.Text = "Delete";
            // 
            // chkShowAll
            // 
            this.chkShowAll.AutoSize = true;
            this.chkShowAll.Location = new System.Drawing.Point(170, 19);
            this.chkShowAll.Name = "chkShowAll";
            this.chkShowAll.Size = new System.Drawing.Size(67, 17);
            this.chkShowAll.TabIndex = 112;
            this.chkShowAll.Text = "Show All";
            this.chkShowAll.UseVisualStyleBackColor = false;
            // 
            // SetDetailsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1347, 653);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SetDetailsScreen";
            this.Text = "SetDetailsScreen";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSetDetailsSummary)).EndInit();
            this.c.ResumeLayout(false);
            this.c.PerformLayout();
            this.tsPartSummary.ResumeLayout(false);
            this.tsPartSummary.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tsPartDetails.ResumeLayout(false);
            this.tsPartDetails.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnThemesRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripButton btnCollapseNodes;
        private System.Windows.Forms.ToolStripButton btnExpandAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.TreeView tvThemesSummary;
        private System.Windows.Forms.ToolStrip tsPartSummary;
        private System.Windows.Forms.ToolStripButton btnPartSummaryCopyToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.StatusStrip c;
        private System.Windows.Forms.ToolStripStatusLabel lblPartCount;
        private System.Windows.Forms.ToolStripStatusLabel lblPartSummaryItemFilteredCount;
        private System.Windows.Forms.DataGridView dgSetDetailsSummary;
        private System.Windows.Forms.ToolStrip tsPartDetails;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fldLDrawRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox fldLDrawColourID;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox fldLDrawColourName;
        private System.Windows.Forms.ToolStripLabel lblQty;
        private System.Windows.Forms.ToolStripTextBox fldQty;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox fldPlacementMovements;
        private System.Windows.Forms.ToolStripButton btnPartClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPartAdd;
        private System.Windows.Forms.ToolStripButton btnPartSave;
        private System.Windows.Forms.ToolStripButton btnPartDelete;
        private System.Windows.Forms.CheckBox chkShowAll;
    }
}