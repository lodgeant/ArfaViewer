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
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.tsPartDetails = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.fldSetRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel2 = new System.Windows.Forms.ToolStripLabel();
            this.fldDescription = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel4 = new System.Windows.Forms.ToolStripLabel();
            this.fldType = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.fldTheme = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubTheme = new System.Windows.Forms.ToolStripComboBox();
            this.lblQty = new System.Windows.Forms.ToolStripLabel();
            this.fldYear = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.fldStatus = new System.Windows.Forms.ToolStripComboBox();
            this.toolStripLabel3 = new System.Windows.Forms.ToolStripLabel();
            this.fldAssignedTo = new System.Windows.Forms.ToolStripTextBox();
            this.btnPartClear = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSetSave = new System.Windows.Forms.ToolStripButton();
            this.btnSetDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenInViewer = new System.Windows.Forms.ToolStripButton();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
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
            this.lblSetDetailsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPartSummary = new System.Windows.Forms.ToolStrip();
            this.btnSetDetailsRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnPartSummaryCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.lblLDrawRefAc = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawRefAc = new System.Windows.Forms.ToolStripTextBox();
            this.lblLDrawColourNameAc = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawColourNameAc = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip1.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.tsPartDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSetDetailsSummary)).BeginInit();
            this.c.SuspendLayout();
            this.tsPartSummary.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1768, 25);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.tsPartDetails);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.groupBox3.Location = new System.Drawing.Point(0, 600);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1768, 53);
            this.groupBox3.TabIndex = 29;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Set Details";
            // 
            // tsPartDetails
            // 
            this.tsPartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.fldSetRef,
            this.toolStripLabel2,
            this.fldDescription,
            this.toolStripLabel4,
            this.fldType,
            this.toolStripLabel5,
            this.fldTheme,
            this.toolStripLabel6,
            this.fldSubTheme,
            this.lblQty,
            this.fldYear,
            this.toolStripLabel7,
            this.fldStatus,
            this.toolStripLabel3,
            this.fldAssignedTo,
            this.btnPartClear,
            this.toolStripSeparator3,
            this.btnSetSave,
            this.btnSetDelete,
            this.toolStripSeparator2,
            this.btnOpenInViewer});
            this.tsPartDetails.Location = new System.Drawing.Point(3, 16);
            this.tsPartDetails.Name = "tsPartDetails";
            this.tsPartDetails.Size = new System.Drawing.Size(1762, 25);
            this.tsPartDetails.TabIndex = 76;
            this.tsPartDetails.Text = "toolStrip5";
            // 
            // toolStripLabel1
            // 
            this.toolStripLabel1.Name = "toolStripLabel1";
            this.toolStripLabel1.Size = new System.Drawing.Size(27, 22);
            this.toolStripLabel1.Text = "Ref:";
            // 
            // fldSetRef
            // 
            this.fldSetRef.BackColor = System.Drawing.Color.LightGray;
            this.fldSetRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSetRef.Name = "fldSetRef";
            this.fldSetRef.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel2
            // 
            this.toolStripLabel2.Name = "toolStripLabel2";
            this.toolStripLabel2.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel2.Text = "Description:";
            // 
            // fldDescription
            // 
            this.fldDescription.BackColor = System.Drawing.Color.LightGray;
            this.fldDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldDescription.Name = "fldDescription";
            this.fldDescription.Size = new System.Drawing.Size(200, 25);
            // 
            // toolStripLabel4
            // 
            this.toolStripLabel4.Name = "toolStripLabel4";
            this.toolStripLabel4.Size = new System.Drawing.Size(34, 22);
            this.toolStripLabel4.Text = "Type:";
            // 
            // fldType
            // 
            this.fldType.BackColor = System.Drawing.Color.LightGray;
            this.fldType.Items.AddRange(new object[] {
            "OFFICIAL",
            "UNOFFICIAL"});
            this.fldType.Name = "fldType";
            this.fldType.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(46, 22);
            this.toolStripLabel5.Text = "Theme:";
            // 
            // fldTheme
            // 
            this.fldTheme.AutoCompleteCustomSource.AddRange(new string[] {
            "OFFICIAL",
            "UNOFFICIAL"});
            this.fldTheme.BackColor = System.Drawing.Color.LightGray;
            this.fldTheme.Name = "fldTheme";
            this.fldTheme.Size = new System.Drawing.Size(150, 25);
            this.fldTheme.Leave += new System.EventHandler(this.fldTheme_Leave);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(69, 22);
            this.toolStripLabel6.Text = "Sub Theme:";
            // 
            // fldSubTheme
            // 
            this.fldSubTheme.AutoCompleteCustomSource.AddRange(new string[] {
            "OFFICIAL",
            "UNOFFICIAL"});
            this.fldSubTheme.BackColor = System.Drawing.Color.LightGray;
            this.fldSubTheme.Name = "fldSubTheme";
            this.fldSubTheme.Size = new System.Drawing.Size(150, 25);
            // 
            // lblQty
            // 
            this.lblQty.BackColor = System.Drawing.Color.Yellow;
            this.lblQty.Name = "lblQty";
            this.lblQty.Size = new System.Drawing.Size(32, 22);
            this.lblQty.Text = "Year:";
            // 
            // fldYear
            // 
            this.fldYear.BackColor = System.Drawing.Color.LightGray;
            this.fldYear.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldYear.Name = "fldYear";
            this.fldYear.Size = new System.Drawing.Size(30, 25);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(42, 22);
            this.toolStripLabel7.Text = "Status:";
            // 
            // fldStatus
            // 
            this.fldStatus.BackColor = System.Drawing.Color.LightGray;
            this.fldStatus.Items.AddRange(new object[] {
            "NOT_STARTED",
            "IN_PROGRESS",
            "COMPLETED"});
            this.fldStatus.Name = "fldStatus";
            this.fldStatus.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel3
            // 
            this.toolStripLabel3.Name = "toolStripLabel3";
            this.toolStripLabel3.Size = new System.Drawing.Size(73, 22);
            this.toolStripLabel3.Text = "Assigned To:";
            // 
            // fldAssignedTo
            // 
            this.fldAssignedTo.BackColor = System.Drawing.Color.LightGray;
            this.fldAssignedTo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldAssignedTo.Name = "fldAssignedTo";
            this.fldAssignedTo.Size = new System.Drawing.Size(75, 25);
            // 
            // btnPartClear
            // 
            this.btnPartClear.Image = ((System.Drawing.Image)(resources.GetObject("btnPartClear.Image")));
            this.btnPartClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartClear.Name = "btnPartClear";
            this.btnPartClear.Size = new System.Drawing.Size(54, 22);
            this.btnPartClear.Text = "Clear";
            this.btnPartClear.Click += new System.EventHandler(this.btnSetClear_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSetSave
            // 
            this.btnSetSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSetSave.Image")));
            this.btnSetSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetSave.Name = "btnSetSave";
            this.btnSetSave.Size = new System.Drawing.Size(51, 22);
            this.btnSetSave.Text = "Save";
            this.btnSetSave.Click += new System.EventHandler(this.btnSetSave_Click);
            // 
            // btnSetDelete
            // 
            this.btnSetDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnSetDelete.Image")));
            this.btnSetDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetDelete.Name = "btnSetDelete";
            this.btnSetDelete.Size = new System.Drawing.Size(60, 22);
            this.btnSetDelete.Text = "Delete";
            this.btnSetDelete.Click += new System.EventHandler(this.btnSetDelete_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOpenInViewer
            // 
            this.btnOpenInViewer.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenInViewer.Image")));
            this.btnOpenInViewer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenInViewer.Name = "btnOpenInViewer";
            this.btnOpenInViewer.Size = new System.Drawing.Size(107, 22);
            this.btnOpenInViewer.Text = "Open In Viewer";
            this.btnOpenInViewer.Click += new System.EventHandler(this.btnOpenInViewer_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(1768, 575);
            this.splitContainer1.SplitterDistance = 589;
            this.splitContainer1.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvThemesSummary);
            this.groupBox1.Controls.Add(this.toolStrip2);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(589, 575);
            this.groupBox1.TabIndex = 28;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Themes";
            // 
            // tvThemesSummary
            // 
            this.tvThemesSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvThemesSummary.HideSelection = false;
            this.tvThemesSummary.Location = new System.Drawing.Point(3, 41);
            this.tvThemesSummary.Name = "tvThemesSummary";
            this.tvThemesSummary.Size = new System.Drawing.Size(583, 531);
            this.tvThemesSummary.TabIndex = 85;
            this.tvThemesSummary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvThemesSummary_AfterSelect);
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
            this.toolStrip2.Size = new System.Drawing.Size(583, 25);
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
            this.btnThemesRefresh.Click += new System.EventHandler(this.btnThemesRefresh_Click);
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
            this.btnCollapseNodes.Click += new System.EventHandler(this.btnCollapseAll_Click);
            // 
            // btnExpandAll
            // 
            this.btnExpandAll.Image = ((System.Drawing.Image)(resources.GetObject("btnExpandAll.Image")));
            this.btnExpandAll.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnExpandAll.Name = "btnExpandAll";
            this.btnExpandAll.Size = new System.Drawing.Size(23, 22);
            this.btnExpandAll.ToolTipText = "Expand All";
            this.btnExpandAll.Click += new System.EventHandler(this.btnExpandAll_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(6, 25);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgSetDetailsSummary);
            this.groupBox2.Controls.Add(this.c);
            this.groupBox2.Controls.Add(this.tsPartSummary);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1175, 575);
            this.groupBox2.TabIndex = 29;
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
            this.dgSetDetailsSummary.Size = new System.Drawing.Size(1169, 509);
            this.dgSetDetailsSummary.TabIndex = 78;
            this.dgSetDetailsSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgSetDetailsSummary_CellClick);
            // 
            // c
            // 
            this.c.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSetDetailsCount});
            this.c.Location = new System.Drawing.Point(3, 550);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(1169, 22);
            this.c.TabIndex = 68;
            this.c.Text = "statusStrip2";
            // 
            // lblSetDetailsCount
            // 
            this.lblSetDetailsCount.Name = "lblSetDetailsCount";
            this.lblSetDetailsCount.Size = new System.Drawing.Size(104, 17);
            this.lblSetDetailsCount.Text = "lblSetDetailsCount";
            // 
            // tsPartSummary
            // 
            this.tsPartSummary.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetDetailsRefresh,
            this.toolStripSeparator4,
            this.btnPartSummaryCopyToClipboard,
            this.toolStripSeparator7,
            this.toolStripLabel8,
            this.toolStripTextBox1,
            this.lblLDrawRefAc,
            this.fldLDrawRefAc,
            this.lblLDrawColourNameAc,
            this.fldLDrawColourNameAc});
            this.tsPartSummary.Location = new System.Drawing.Point(3, 16);
            this.tsPartSummary.Name = "tsPartSummary";
            this.tsPartSummary.Size = new System.Drawing.Size(1169, 25);
            this.tsPartSummary.TabIndex = 27;
            this.tsPartSummary.Text = "toolStrip4";
            // 
            // btnSetDetailsRefresh
            // 
            this.btnSetDetailsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnSetDetailsRefresh.Image")));
            this.btnSetDetailsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetDetailsRefresh.Name = "btnSetDetailsRefresh";
            this.btnSetDetailsRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnSetDetailsRefresh.Text = "Refresh";
            this.btnSetDetailsRefresh.Click += new System.EventHandler(this.btnSetDetailsRefresh_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnPartSummaryCopyToClipboard
            // 
            this.btnPartSummaryCopyToClipboard.BackColor = System.Drawing.Color.Pink;
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
            // lblLDrawRefAc
            // 
            this.lblLDrawRefAc.Name = "lblLDrawRefAc";
            this.lblLDrawRefAc.Size = new System.Drawing.Size(42, 22);
            this.lblLDrawRefAc.Text = "Status:";
            // 
            // fldLDrawRefAc
            // 
            this.fldLDrawRefAc.BackColor = System.Drawing.Color.Wheat;
            this.fldLDrawRefAc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawRefAc.Name = "fldLDrawRefAc";
            this.fldLDrawRefAc.Size = new System.Drawing.Size(100, 25);
            // 
            // lblLDrawColourNameAc
            // 
            this.lblLDrawColourNameAc.BackColor = System.Drawing.Color.Pink;
            this.lblLDrawColourNameAc.Name = "lblLDrawColourNameAc";
            this.lblLDrawColourNameAc.Size = new System.Drawing.Size(73, 22);
            this.lblLDrawColourNameAc.Text = "Assigned To:";
            // 
            // fldLDrawColourNameAc
            // 
            this.fldLDrawColourNameAc.BackColor = System.Drawing.Color.Wheat;
            this.fldLDrawColourNameAc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawColourNameAc.Name = "fldLDrawColourNameAc";
            this.fldLDrawColourNameAc.Size = new System.Drawing.Size(125, 25);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(70, 22);
            this.toolStripLabel8.Text = "Description:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.Wheat;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // SetDetailsScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1768, 653);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.toolStrip1);
            this.Name = "SetDetailsScreen";
            this.Text = "SetDetailsScreen";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tsPartDetails.ResumeLayout(false);
            this.tsPartDetails.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ToolStrip tsPartDetails;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fldSetRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox fldDescription;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox fldType;
        private System.Windows.Forms.ToolStripLabel lblQty;
        private System.Windows.Forms.ToolStripTextBox fldYear;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox fldAssignedTo;
        private System.Windows.Forms.ToolStripButton btnPartClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnSetSave;
        private System.Windows.Forms.ToolStripButton btnSetDelete;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripComboBox fldTheme;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripComboBox fldSubTheme;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripComboBox fldStatus;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvThemesSummary;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnThemesRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStripButton btnCollapseNodes;
        private System.Windows.Forms.ToolStripButton btnExpandAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgSetDetailsSummary;
        private System.Windows.Forms.StatusStrip c;
        private System.Windows.Forms.ToolStripStatusLabel lblSetDetailsCount;
        private System.Windows.Forms.ToolStrip tsPartSummary;
        private System.Windows.Forms.ToolStripButton btnPartSummaryCopyToClipboard;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton btnOpenInViewer;
        private System.Windows.Forms.ToolStripButton btnSetDetailsRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripLabel lblLDrawRefAc;
        private System.Windows.Forms.ToolStripTextBox fldLDrawRefAc;
        private System.Windows.Forms.ToolStripLabel lblLDrawColourNameAc;
        private System.Windows.Forms.ToolStripTextBox fldLDrawColourNameAc;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
    }
}