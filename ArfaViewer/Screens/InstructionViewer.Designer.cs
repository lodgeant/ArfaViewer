namespace Generator
{
    partial class InstructionViewer
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(InstructionViewer));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.lblSetRef = new System.Windows.Forms.ToolStripLabel();
            this.fldCurrentSetRef = new System.Windows.Forms.ToolStripTextBox();
            this.btnLoadSet = new System.Windows.Forms.ToolStripButton();
            this.btnSaveSet = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.btnOpenSetInstructions = new System.Windows.Forms.ToolStripButton();
            this.btnOpenSetURLs = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator22 = new System.Windows.Forms.ToolStripSeparator();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAddStepToEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAdd5Steps = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInsertStepBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInsertStepAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStepDuplicateToEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStepDuplicateToBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsStepDuplicateToAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator14 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAddSubModelAtEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInsertSubModelBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsInsertSubModelAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSubModelDuplicateToEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSubModelDuplicateToBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSubModelDuplicateToAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator12 = new System.Windows.Forms.ToolStripSeparator();
            this.tsDuplicateObjectToBefore = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDuplicateObjectToAfter = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDuplicateObjectToEnd = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator13 = new System.Windows.Forms.ToolStripSeparator();
            this.tsShowMiniFigSet = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator16 = new System.Windows.Forms.ToolStripSeparator();
            this.tsAddSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddSubSet = new System.Windows.Forms.ToolStripMenuItem();
            this.tsAddModel = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator23 = new System.Windows.Forms.ToolStripSeparator();
            this.tsRecalulatePartList = new System.Windows.Forms.ToolStripMenuItem();
            this.tsRecalulateSubSetRefs = new System.Windows.Forms.ToolStripMenuItem();
            this.chkShowSubParts = new System.Windows.Forms.CheckBox();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.gpSetStructure = new System.Windows.Forms.GroupBox();
            this.tvSetSummary = new System.Windows.Forms.TreeView();
            this.pnlSetImage = new System.Windows.Forms.Panel();
            this.toolStrip2 = new System.Windows.Forms.ToolStrip();
            this.btnSetStructureRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator21 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCollapseNodes = new System.Windows.Forms.ToolStripButton();
            this.btnExpandAll = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.btnMoveUp = new System.Windows.Forms.ToolStripButton();
            this.btnMoveDown = new System.Windows.Forms.ToolStripButton();
            this.btnMoveUpBy5 = new System.Windows.Forms.ToolStripButton();
            this.fldBulkValue = new System.Windows.Forms.ToolStripTextBox();
            this.btnMoveDownBy5 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSetClear = new System.Windows.Forms.ToolStripButton();
            this.gpNodeMgmt = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.chkFBXMissingAc = new System.Windows.Forms.CheckBox();
            this.chkLDrawColourNameAcEquals = new System.Windows.Forms.CheckBox();
            this.chkLDrawRefAcEquals = new System.Windows.Forms.CheckBox();
            this.chkIsSubPart = new System.Windows.Forms.CheckBox();
            this.dgPartSummary = new System.Windows.Forms.DataGridView();
            this.chkIsLargeModel = new System.Windows.Forms.CheckBox();
            this.chkIsSticker = new System.Windows.Forms.CheckBox();
            this.c = new System.Windows.Forms.StatusStrip();
            this.lblPartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblPartSummaryItemFilteredCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPartSummary = new System.Windows.Forms.ToolStrip();
            this.btnPartSummaryCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.lblLDrawRefAc = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawRefAc = new System.Windows.Forms.ToolStripTextBox();
            this.lblLDrawColourNameAc = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawColourNameAc = new System.Windows.Forms.ToolStripTextBox();
            this.gbPartDetails = new System.Windows.Forms.GroupBox();
            this.tsBasePartCollection = new System.Windows.Forms.ToolStrip();
            this.lblPartType = new System.Windows.Forms.ToolStripLabel();
            this.fldPartType = new System.Windows.Forms.ToolStripComboBox();
            this.lblLDrawSize = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawSize = new System.Windows.Forms.ToolStripTextBox();
            this.tsPartPosDetails = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel31 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartPosX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel32 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartPosY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel33 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartPosZ = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel34 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartRotX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel35 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartRotY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel36 = new System.Windows.Forms.ToolStripLabel();
            this.fldPartRotZ = new System.Windows.Forms.ToolStripTextBox();
            this.chkBasePartCollection = new System.Windows.Forms.CheckBox();
            this.tsPartDetails = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel1 = new System.Windows.Forms.ToolStripLabel();
            this.fldLDrawRef = new System.Windows.Forms.ToolStripTextBox();
            this.fldLDrawImage = new System.Windows.Forms.ToolStripButton();
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
            this.btnAddPartToBasePartCollection = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator24 = new System.Windows.Forms.ToolStripSeparator();
            this.btnGenerateDatFile = new System.Windows.Forms.ToolStripButton();
            this.gpStep = new System.Windows.Forms.GroupBox();
            this.toolStrip8 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel22 = new System.Windows.Forms.ToolStripLabel();
            this.fldStepParentSubSetRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel23 = new System.Windows.Forms.ToolStripLabel();
            this.fldStepParentModelRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel18 = new System.Windows.Forms.ToolStripLabel();
            this.fldPureStepNo = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel19 = new System.Windows.Forms.ToolStripLabel();
            this.fldStepLevel = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel21 = new System.Windows.Forms.ToolStripLabel();
            this.fldStepBook = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel24 = new System.Windows.Forms.ToolStripLabel();
            this.fldStepPage = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator17 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel37 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelRotX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel38 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelRotY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel39 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelRotZ = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator19 = new System.Windows.Forms.ToolStripSeparator();
            this.btnStepSave = new System.Windows.Forms.ToolStripButton();
            this.btnStepDelete = new System.Windows.Forms.ToolStripButton();
            this.gpSubModel = new System.Windows.Forms.GroupBox();
            this.toolStrip9 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel16 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelCurrentRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel17 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelNewRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel20 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelDescription = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel25 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelPosX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel26 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelPosY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel27 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelPosZ = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator10 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel28 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelRotX = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel29 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelRotY = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel30 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubModelRotZ = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripSeparator11 = new System.Windows.Forms.ToolStripSeparator();
            this.btnSubModelSave = new System.Windows.Forms.ToolStripButton();
            this.btnSubModelDelete = new System.Windows.Forms.ToolStripButton();
            this.gpModel = new System.Windows.Forms.GroupBox();
            this.toolStrip7 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel14 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelCurrentRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel10 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelNewRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel11 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelDescription = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel12 = new System.Windows.Forms.ToolStripLabel();
            this.fldModelType = new System.Windows.Forms.ToolStripComboBox();
            this.btnModelSave = new System.Windows.Forms.ToolStripButton();
            this.btnModelDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.btnAdjustModelStepNos = new System.Windows.Forms.ToolStripButton();
            this.gpSubSet = new System.Windows.Forms.GroupBox();
            this.toolStrip3 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel13 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubSetCurrentRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel5 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubSetNewRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel6 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubSetDescription = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel7 = new System.Windows.Forms.ToolStripLabel();
            this.fldSubSetType = new System.Windows.Forms.ToolStripComboBox();
            this.btnSubSetSave = new System.Windows.Forms.ToolStripButton();
            this.btnSubSetDelete = new System.Windows.Forms.ToolStripButton();
            this.gpSet = new System.Windows.Forms.GroupBox();
            this.toolStrip6 = new System.Windows.Forms.ToolStrip();
            this.toolStripLabel15 = new System.Windows.Forms.ToolStripLabel();
            this.fldSetCurrentRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel8 = new System.Windows.Forms.ToolStripLabel();
            this.fldSetNewRef = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel9 = new System.Windows.Forms.ToolStripLabel();
            this.fldSetDescription = new System.Windows.Forms.ToolStripTextBox();
            this.btnSetSaveNode = new System.Windows.Forms.ToolStripButton();
            this.tabPage7 = new System.Windows.Forms.TabPage();
            this.splitContainer4 = new System.Windows.Forms.SplitContainer();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.pnlSetXML = new System.Windows.Forms.Panel();
            this.toolStrip13 = new System.Windows.Forms.ToolStrip();
            this.btnShowBaseXMLInNotePadPlus = new System.Windows.Forms.ToolStripButton();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.pnlSetWithMFXML = new System.Windows.Forms.Panel();
            this.toolStrip14 = new System.Windows.Forms.ToolStrip();
            this.btnShowWithMFXMLInNotePadPlus = new System.Windows.Forms.ToolStripButton();
            this.tabPage5 = new System.Windows.Forms.TabPage();
            this.pnlRebrickableXML = new System.Windows.Forms.Panel();
            this.toolStrip5 = new System.Windows.Forms.ToolStrip();
            this.btnShowRebrickableXMLInNotePadPlus = new System.Windows.Forms.ToolStripButton();
            this.tabPage6 = new System.Windows.Forms.TabPage();
            this.pnlLDRString = new System.Windows.Forms.Panel();
            this.tpPartList = new System.Windows.Forms.TabPage();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.gpPartListBasic = new System.Windows.Forms.GroupBox();
            this.dgPartListSummary = new System.Windows.Forms.DataGridView();
            this.toolStrip11 = new System.Windows.Forms.ToolStrip();
            this.btnPartListBasicCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.lblPartListCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpPartListMiniFigs = new System.Windows.Forms.GroupBox();
            this.dgMiniFigsPartListSummary = new System.Windows.Forms.DataGridView();
            this.toolStrip4 = new System.Windows.Forms.ToolStrip();
            this.btnPartListMFCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.statusStrip4 = new System.Windows.Forms.StatusStrip();
            this.pbPartlist = new System.Windows.Forms.ToolStripProgressBar();
            this.lblPartListStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblMiniFigsPartListCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.gpPartListWithMF = new System.Windows.Forms.GroupBox();
            this.dgPartListWithMFsSummary = new System.Windows.Forms.DataGridView();
            this.toolStrip12 = new System.Windows.Forms.ToolStrip();
            this.btnPartListWithMFCopyToClipboard = new System.Windows.Forms.ToolStripButton();
            this.statusStrip3 = new System.Windows.Forms.StatusStrip();
            this.lblPartListWithMFsCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsPartListHeader = new System.Windows.Forms.ToolStrip();
            this.btnPartListRefresh = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.btnCompareWithRebrickable = new System.Windows.Forms.ToolStripButton();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.splitContainer5 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.tvSetSubModels = new System.Windows.Forms.TreeView();
            this.toolStrip10 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton2 = new System.Windows.Forms.ToolStripButton();
            this.toolStripButton3 = new System.Windows.Forms.ToolStripButton();
            this.splitContainer7 = new System.Windows.Forms.SplitContainer();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.dgSetSubModelPartSummary = new System.Windows.Forms.DataGridView();
            this.statusStrip5 = new System.Windows.Forms.StatusStrip();
            this.lblSetSubModelPartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblSetSubModelPartSummaryItemFilteredCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip16 = new System.Windows.Forms.ToolStrip();
            this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator25 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel40 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox1 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel41 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox2 = new System.Windows.Forms.ToolStripTextBox();
            this.gpUnitySubModelParts = new System.Windows.Forms.GroupBox();
            this.dgUnitySubModelPartSummary = new System.Windows.Forms.DataGridView();
            this.statusStrip6 = new System.Windows.Forms.StatusStrip();
            this.lblUnitySubModelPartCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lblUnitySubModelPartSummaryItemFilteredCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStrip18 = new System.Windows.Forms.ToolStrip();
            this.btnSyncSubModelPositions = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator18 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripButton6 = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator28 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripLabel42 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox3 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStripLabel43 = new System.Windows.Forms.ToolStripLabel();
            this.toolStripTextBox4 = new System.Windows.Forms.ToolStripTextBox();
            this.toolStrip17 = new System.Windows.Forms.ToolStrip();
            this.btnUnitySubModelsRefresh = new System.Windows.Forms.ToolStripButton();
            this.chkShowPages = new System.Windows.Forms.CheckBox();
            this.chkShowPartcolourImages = new System.Windows.Forms.CheckBox();
            this.chkShowElementImages = new System.Windows.Forms.CheckBox();
            this.chkShowFBXDetails = new System.Windows.Forms.CheckBox();
            this.toolStrip1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.gpSetStructure.SuspendLayout();
            this.toolStrip2.SuspendLayout();
            this.gpNodeMgmt.SuspendLayout();
            this.groupBox3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartSummary)).BeginInit();
            this.c.SuspendLayout();
            this.tsPartSummary.SuspendLayout();
            this.gbPartDetails.SuspendLayout();
            this.tsBasePartCollection.SuspendLayout();
            this.tsPartPosDetails.SuspendLayout();
            this.tsPartDetails.SuspendLayout();
            this.gpStep.SuspendLayout();
            this.toolStrip8.SuspendLayout();
            this.gpSubModel.SuspendLayout();
            this.toolStrip9.SuspendLayout();
            this.gpModel.SuspendLayout();
            this.toolStrip7.SuspendLayout();
            this.gpSubSet.SuspendLayout();
            this.toolStrip3.SuspendLayout();
            this.gpSet.SuspendLayout();
            this.toolStrip6.SuspendLayout();
            this.tabPage7.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).BeginInit();
            this.splitContainer4.Panel1.SuspendLayout();
            this.splitContainer4.Panel2.SuspendLayout();
            this.splitContainer4.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.toolStrip13.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.toolStrip14.SuspendLayout();
            this.tabPage5.SuspendLayout();
            this.toolStrip5.SuspendLayout();
            this.tabPage6.SuspendLayout();
            this.tpPartList.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.gpPartListBasic.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartListSummary)).BeginInit();
            this.toolStrip11.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.gpPartListMiniFigs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMiniFigsPartListSummary)).BeginInit();
            this.toolStrip4.SuspendLayout();
            this.statusStrip4.SuspendLayout();
            this.gpPartListWithMF.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartListWithMFsSummary)).BeginInit();
            this.toolStrip12.SuspendLayout();
            this.statusStrip3.SuspendLayout();
            this.tsPartListHeader.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).BeginInit();
            this.splitContainer5.Panel1.SuspendLayout();
            this.splitContainer5.Panel2.SuspendLayout();
            this.splitContainer5.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.toolStrip10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).BeginInit();
            this.splitContainer7.Panel1.SuspendLayout();
            this.splitContainer7.Panel2.SuspendLayout();
            this.splitContainer7.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSetSubModelPartSummary)).BeginInit();
            this.statusStrip5.SuspendLayout();
            this.toolStrip16.SuspendLayout();
            this.gpUnitySubModelParts.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnitySubModelPartSummary)).BeginInit();
            this.statusStrip6.SuspendLayout();
            this.toolStrip18.SuspendLayout();
            this.toolStrip17.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit,
            this.toolStripSeparator1,
            this.lblSetRef,
            this.fldCurrentSetRef,
            this.btnLoadSet,
            this.btnSaveSet,
            this.toolStripSeparator2,
            this.btnOpenSetInstructions,
            this.btnOpenSetURLs,
            this.toolStripSeparator22});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(1493, 25);
            this.toolStrip1.TabIndex = 25;
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
            // lblSetRef
            // 
            this.lblSetRef.Name = "lblSetRef";
            this.lblSetRef.Size = new System.Drawing.Size(46, 22);
            this.lblSetRef.Text = "Set Ref:";
            // 
            // fldCurrentSetRef
            // 
            this.fldCurrentSetRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldCurrentSetRef.Name = "fldCurrentSetRef";
            this.fldCurrentSetRef.Size = new System.Drawing.Size(100, 25);
            this.fldCurrentSetRef.Text = "TEST";
            // 
            // btnLoadSet
            // 
            this.btnLoadSet.Image = ((System.Drawing.Image)(resources.GetObject("btnLoadSet.Image")));
            this.btnLoadSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnLoadSet.Name = "btnLoadSet";
            this.btnLoadSet.Size = new System.Drawing.Size(53, 22);
            this.btnLoadSet.Text = "Load";
            this.btnLoadSet.Click += new System.EventHandler(this.btnLoadSet_Click);
            // 
            // btnSaveSet
            // 
            this.btnSaveSet.Image = ((System.Drawing.Image)(resources.GetObject("btnSaveSet.Image")));
            this.btnSaveSet.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSaveSet.Name = "btnSaveSet";
            this.btnSaveSet.Size = new System.Drawing.Size(51, 22);
            this.btnSaveSet.Text = "Save";
            this.btnSaveSet.Click += new System.EventHandler(this.btnSaveSet_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 25);
            // 
            // btnOpenSetInstructions
            // 
            this.btnOpenSetInstructions.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSetInstructions.Image")));
            this.btnOpenSetInstructions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenSetInstructions.Name = "btnOpenSetInstructions";
            this.btnOpenSetInstructions.Size = new System.Drawing.Size(140, 22);
            this.btnOpenSetInstructions.Text = "Open Set Instructions";
            this.btnOpenSetInstructions.Click += new System.EventHandler(this.btnOpenSetInstructions_Click);
            // 
            // btnOpenSetURLs
            // 
            this.btnOpenSetURLs.Image = ((System.Drawing.Image)(resources.GetObject("btnOpenSetURLs.Image")));
            this.btnOpenSetURLs.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnOpenSetURLs.Name = "btnOpenSetURLs";
            this.btnOpenSetURLs.Size = new System.Drawing.Size(104, 22);
            this.btnOpenSetURLs.Text = "Open Set URLs";
            this.btnOpenSetURLs.Click += new System.EventHandler(this.btnOpenSetURLs_Click);
            // 
            // toolStripSeparator22
            // 
            this.toolStripSeparator22.Name = "toolStripSeparator22";
            this.toolStripSeparator22.Size = new System.Drawing.Size(6, 25);
            // 
            // imageList1
            // 
            this.imageList1.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList1.ImageStream")));
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList1.Images.SetKeyName(0, "package.png");
            this.imageList1.Images.SetKeyName(1, "basket.png");
            this.imageList1.Images.SetKeyName(2, "lorry.png");
            this.imageList1.Images.SetKeyName(3, "car.png");
            this.imageList1.Images.SetKeyName(4, "plugin.png");
            this.imageList1.Images.SetKeyName(5, "brick.png");
            this.imageList1.Images.SetKeyName(6, "tick.png");
            this.imageList1.Images.SetKeyName(7, "user.png");
            this.imageList1.Images.SetKeyName(8, "arrow_switch.png");
            this.imageList1.Images.SetKeyName(9, "photo.png");
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddStepToEnd,
            this.tsAdd5Steps,
            this.tsInsertStepBefore,
            this.tsInsertStepAfter,
            this.tsStepDuplicateToEnd,
            this.tsStepDuplicateToBefore,
            this.tsStepDuplicateToAfter,
            this.toolStripSeparator14,
            this.tsAddSubModelAtEnd,
            this.tsInsertSubModelBefore,
            this.tsInsertSubModelAfter,
            this.tsSubModelDuplicateToEnd,
            this.tsSubModelDuplicateToBefore,
            this.tsSubModelDuplicateToAfter,
            this.toolStripSeparator12,
            this.tsDuplicateObjectToBefore,
            this.tsDuplicateObjectToAfter,
            this.tsDuplicateObjectToEnd,
            this.toolStripSeparator13,
            this.tsShowMiniFigSet,
            this.toolStripSeparator16,
            this.tsAddSet,
            this.tsAddSubSet,
            this.tsAddModel,
            this.toolStripSeparator23,
            this.tsRecalulatePartList,
            this.tsRecalulateSubSetRefs});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(241, 518);
            // 
            // tsAddStepToEnd
            // 
            this.tsAddStepToEnd.Image = ((System.Drawing.Image)(resources.GetObject("tsAddStepToEnd.Image")));
            this.tsAddStepToEnd.Name = "tsAddStepToEnd";
            this.tsAddStepToEnd.Size = new System.Drawing.Size(240, 22);
            this.tsAddStepToEnd.Text = "Step - Add at End";
            this.tsAddStepToEnd.Click += new System.EventHandler(this.tsAddStepToEnd_Click);
            // 
            // tsAdd5Steps
            // 
            this.tsAdd5Steps.Image = ((System.Drawing.Image)(resources.GetObject("tsAdd5Steps.Image")));
            this.tsAdd5Steps.Name = "tsAdd5Steps";
            this.tsAdd5Steps.Size = new System.Drawing.Size(240, 22);
            this.tsAdd5Steps.Text = "Step - Add 5 at End";
            this.tsAdd5Steps.Click += new System.EventHandler(this.tsAdd5StepsToEnd_Click);
            // 
            // tsInsertStepBefore
            // 
            this.tsInsertStepBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsInsertStepBefore.Image")));
            this.tsInsertStepBefore.Name = "tsInsertStepBefore";
            this.tsInsertStepBefore.Size = new System.Drawing.Size(240, 22);
            this.tsInsertStepBefore.Text = "Step - Insert Before";
            this.tsInsertStepBefore.Click += new System.EventHandler(this.tsInsertStepBefore_Click);
            // 
            // tsInsertStepAfter
            // 
            this.tsInsertStepAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsInsertStepAfter.Image")));
            this.tsInsertStepAfter.Name = "tsInsertStepAfter";
            this.tsInsertStepAfter.Size = new System.Drawing.Size(240, 22);
            this.tsInsertStepAfter.Text = "Step - Insert After";
            this.tsInsertStepAfter.Click += new System.EventHandler(this.tsInsertStepAfter_Click);
            // 
            // tsStepDuplicateToEnd
            // 
            this.tsStepDuplicateToEnd.Image = ((System.Drawing.Image)(resources.GetObject("tsStepDuplicateToEnd.Image")));
            this.tsStepDuplicateToEnd.Name = "tsStepDuplicateToEnd";
            this.tsStepDuplicateToEnd.Size = new System.Drawing.Size(240, 22);
            this.tsStepDuplicateToEnd.Text = "Step - Duplicate to End";
            this.tsStepDuplicateToEnd.Click += new System.EventHandler(this.tsStepDuplicateToEnd_Click);
            // 
            // tsStepDuplicateToBefore
            // 
            this.tsStepDuplicateToBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsStepDuplicateToBefore.Image")));
            this.tsStepDuplicateToBefore.Name = "tsStepDuplicateToBefore";
            this.tsStepDuplicateToBefore.Size = new System.Drawing.Size(240, 22);
            this.tsStepDuplicateToBefore.Text = "Step - Duplicate to Before";
            this.tsStepDuplicateToBefore.Click += new System.EventHandler(this.tsStepDuplicateToBefore_Click);
            // 
            // tsStepDuplicateToAfter
            // 
            this.tsStepDuplicateToAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsStepDuplicateToAfter.Image")));
            this.tsStepDuplicateToAfter.Name = "tsStepDuplicateToAfter";
            this.tsStepDuplicateToAfter.Size = new System.Drawing.Size(240, 22);
            this.tsStepDuplicateToAfter.Text = "Step - Duplicate to After";
            this.tsStepDuplicateToAfter.Click += new System.EventHandler(this.tsStepDuplicateToAfter_Click);
            // 
            // toolStripSeparator14
            // 
            this.toolStripSeparator14.Name = "toolStripSeparator14";
            this.toolStripSeparator14.Size = new System.Drawing.Size(237, 6);
            // 
            // tsAddSubModelAtEnd
            // 
            this.tsAddSubModelAtEnd.Image = ((System.Drawing.Image)(resources.GetObject("tsAddSubModelAtEnd.Image")));
            this.tsAddSubModelAtEnd.Name = "tsAddSubModelAtEnd";
            this.tsAddSubModelAtEnd.Size = new System.Drawing.Size(240, 22);
            this.tsAddSubModelAtEnd.Text = "SubModel - Add at End";
            this.tsAddSubModelAtEnd.Click += new System.EventHandler(this.tsAddSubModelAtEnd_Click);
            // 
            // tsInsertSubModelBefore
            // 
            this.tsInsertSubModelBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsInsertSubModelBefore.Image")));
            this.tsInsertSubModelBefore.Name = "tsInsertSubModelBefore";
            this.tsInsertSubModelBefore.Size = new System.Drawing.Size(240, 22);
            this.tsInsertSubModelBefore.Text = "SubModel - Insert Before";
            this.tsInsertSubModelBefore.Click += new System.EventHandler(this.tsInsertSubModelBefore_Click);
            // 
            // tsInsertSubModelAfter
            // 
            this.tsInsertSubModelAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsInsertSubModelAfter.Image")));
            this.tsInsertSubModelAfter.Name = "tsInsertSubModelAfter";
            this.tsInsertSubModelAfter.Size = new System.Drawing.Size(240, 22);
            this.tsInsertSubModelAfter.Text = "SubModel - Insert After";
            this.tsInsertSubModelAfter.Click += new System.EventHandler(this.tsInsertSubModelAfter_Click);
            // 
            // tsSubModelDuplicateToEnd
            // 
            this.tsSubModelDuplicateToEnd.Image = ((System.Drawing.Image)(resources.GetObject("tsSubModelDuplicateToEnd.Image")));
            this.tsSubModelDuplicateToEnd.Name = "tsSubModelDuplicateToEnd";
            this.tsSubModelDuplicateToEnd.Size = new System.Drawing.Size(240, 22);
            this.tsSubModelDuplicateToEnd.Text = "SubModel - Duplicate to End";
            this.tsSubModelDuplicateToEnd.Click += new System.EventHandler(this.tsSubModelDuplicateToEnd_Click);
            // 
            // tsSubModelDuplicateToBefore
            // 
            this.tsSubModelDuplicateToBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsSubModelDuplicateToBefore.Image")));
            this.tsSubModelDuplicateToBefore.Name = "tsSubModelDuplicateToBefore";
            this.tsSubModelDuplicateToBefore.Size = new System.Drawing.Size(240, 22);
            this.tsSubModelDuplicateToBefore.Text = "SubModel - Duplicate to Before";
            this.tsSubModelDuplicateToBefore.Click += new System.EventHandler(this.tsSubModelDuplicateToBefore_Click);
            // 
            // tsSubModelDuplicateToAfter
            // 
            this.tsSubModelDuplicateToAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsSubModelDuplicateToAfter.Image")));
            this.tsSubModelDuplicateToAfter.Name = "tsSubModelDuplicateToAfter";
            this.tsSubModelDuplicateToAfter.Size = new System.Drawing.Size(240, 22);
            this.tsSubModelDuplicateToAfter.Text = "SubModel - Duplicate to After";
            this.tsSubModelDuplicateToAfter.Click += new System.EventHandler(this.tsSubModelDuplicateToAfter_Click);
            // 
            // toolStripSeparator12
            // 
            this.toolStripSeparator12.Name = "toolStripSeparator12";
            this.toolStripSeparator12.Size = new System.Drawing.Size(237, 6);
            // 
            // tsDuplicateObjectToBefore
            // 
            this.tsDuplicateObjectToBefore.BackColor = System.Drawing.Color.Pink;
            this.tsDuplicateObjectToBefore.Image = ((System.Drawing.Image)(resources.GetObject("tsDuplicateObjectToBefore.Image")));
            this.tsDuplicateObjectToBefore.Name = "tsDuplicateObjectToBefore";
            this.tsDuplicateObjectToBefore.Size = new System.Drawing.Size(240, 22);
            this.tsDuplicateObjectToBefore.Text = "Duplicate Object to Before";
            // 
            // tsDuplicateObjectToAfter
            // 
            this.tsDuplicateObjectToAfter.BackColor = System.Drawing.Color.Pink;
            this.tsDuplicateObjectToAfter.Image = ((System.Drawing.Image)(resources.GetObject("tsDuplicateObjectToAfter.Image")));
            this.tsDuplicateObjectToAfter.Name = "tsDuplicateObjectToAfter";
            this.tsDuplicateObjectToAfter.Size = new System.Drawing.Size(240, 22);
            this.tsDuplicateObjectToAfter.Text = "Duplicate Object to After";
            // 
            // tsDuplicateObjectToEnd
            // 
            this.tsDuplicateObjectToEnd.BackColor = System.Drawing.Color.Pink;
            this.tsDuplicateObjectToEnd.Image = ((System.Drawing.Image)(resources.GetObject("tsDuplicateObjectToEnd.Image")));
            this.tsDuplicateObjectToEnd.Name = "tsDuplicateObjectToEnd";
            this.tsDuplicateObjectToEnd.Size = new System.Drawing.Size(240, 22);
            this.tsDuplicateObjectToEnd.Text = "Duplicate Object to End";
            // 
            // toolStripSeparator13
            // 
            this.toolStripSeparator13.Name = "toolStripSeparator13";
            this.toolStripSeparator13.Size = new System.Drawing.Size(237, 6);
            // 
            // tsShowMiniFigSet
            // 
            this.tsShowMiniFigSet.Image = ((System.Drawing.Image)(resources.GetObject("tsShowMiniFigSet.Image")));
            this.tsShowMiniFigSet.Name = "tsShowMiniFigSet";
            this.tsShowMiniFigSet.Size = new System.Drawing.Size(240, 22);
            this.tsShowMiniFigSet.Text = "Show MiniFig Set";
            this.tsShowMiniFigSet.Click += new System.EventHandler(this.tsShowMiniFigSet_Click);
            // 
            // toolStripSeparator16
            // 
            this.toolStripSeparator16.Name = "toolStripSeparator16";
            this.toolStripSeparator16.Size = new System.Drawing.Size(237, 6);
            // 
            // tsAddSet
            // 
            this.tsAddSet.Image = ((System.Drawing.Image)(resources.GetObject("tsAddSet.Image")));
            this.tsAddSet.Name = "tsAddSet";
            this.tsAddSet.Size = new System.Drawing.Size(240, 22);
            this.tsAddSet.Text = "Add Set";
            this.tsAddSet.Click += new System.EventHandler(this.tsAddSet_Click);
            // 
            // tsAddSubSet
            // 
            this.tsAddSubSet.Image = ((System.Drawing.Image)(resources.GetObject("tsAddSubSet.Image")));
            this.tsAddSubSet.Name = "tsAddSubSet";
            this.tsAddSubSet.Size = new System.Drawing.Size(240, 22);
            this.tsAddSubSet.Text = "Add SubSet";
            this.tsAddSubSet.Click += new System.EventHandler(this.tsAddSubSet_Click);
            // 
            // tsAddModel
            // 
            this.tsAddModel.Image = ((System.Drawing.Image)(resources.GetObject("tsAddModel.Image")));
            this.tsAddModel.Name = "tsAddModel";
            this.tsAddModel.Size = new System.Drawing.Size(240, 22);
            this.tsAddModel.Text = "Add Model";
            this.tsAddModel.Click += new System.EventHandler(this.tsAddModel_Click);
            // 
            // toolStripSeparator23
            // 
            this.toolStripSeparator23.Name = "toolStripSeparator23";
            this.toolStripSeparator23.Size = new System.Drawing.Size(237, 6);
            // 
            // tsRecalulatePartList
            // 
            this.tsRecalulatePartList.Image = ((System.Drawing.Image)(resources.GetObject("tsRecalulatePartList.Image")));
            this.tsRecalulatePartList.Name = "tsRecalulatePartList";
            this.tsRecalulatePartList.Size = new System.Drawing.Size(240, 22);
            this.tsRecalulatePartList.Text = "Recalculate Part List";
            this.tsRecalulatePartList.Click += new System.EventHandler(this.tsRecalulatePartList_Click);
            // 
            // tsRecalulateSubSetRefs
            // 
            this.tsRecalulateSubSetRefs.Image = ((System.Drawing.Image)(resources.GetObject("tsRecalulateSubSetRefs.Image")));
            this.tsRecalulateSubSetRefs.Name = "tsRecalulateSubSetRefs";
            this.tsRecalulateSubSetRefs.Size = new System.Drawing.Size(240, 22);
            this.tsRecalulateSubSetRefs.Text = "Recalculate SubSet Refs";
            this.tsRecalulateSubSetRefs.Click += new System.EventHandler(this.tsRecalulateSubSetRefs_Click);
            // 
            // chkShowSubParts
            // 
            this.chkShowSubParts.AutoSize = true;
            this.chkShowSubParts.Location = new System.Drawing.Point(1661, 12);
            this.chkShowSubParts.Name = "chkShowSubParts";
            this.chkShowSubParts.Size = new System.Drawing.Size(99, 17);
            this.chkShowSubParts.TabIndex = 34;
            this.chkShowSubParts.Text = "Show SubParts";
            this.chkShowSubParts.UseVisualStyleBackColor = false;
            this.chkShowSubParts.CheckedChanged += new System.EventHandler(this.chkShowSubParts_CheckedChanged);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbStatus,
            this.lblStatus});
            this.statusStrip2.Location = new System.Drawing.Point(0, 727);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(1493, 22);
            this.statusStrip2.TabIndex = 35;
            this.statusStrip2.Text = "statusStrip2";
            // 
            // pbStatus
            // 
            this.pbStatus.ForeColor = System.Drawing.Color.Lime;
            this.pbStatus.Name = "pbStatus";
            this.pbStatus.Size = new System.Drawing.Size(100, 16);
            // 
            // lblStatus
            // 
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(52, 17);
            this.lblStatus.Text = "lblStatus";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage7);
            this.tabControl1.Controls.Add(this.tabPage5);
            this.tabControl1.Controls.Add(this.tabPage6);
            this.tabControl1.Controls.Add(this.tpPartList);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(0, 25);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1493, 702);
            this.tabControl1.TabIndex = 36;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.splitContainer1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1485, 676);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Summary";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(3, 3);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.gpSetStructure);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.gpNodeMgmt);
            this.splitContainer1.Size = new System.Drawing.Size(1479, 670);
            this.splitContainer1.SplitterDistance = 356;
            this.splitContainer1.TabIndex = 32;
            // 
            // gpSetStructure
            // 
            this.gpSetStructure.Controls.Add(this.tvSetSummary);
            this.gpSetStructure.Controls.Add(this.pnlSetImage);
            this.gpSetStructure.Controls.Add(this.toolStrip2);
            this.gpSetStructure.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpSetStructure.Location = new System.Drawing.Point(0, 0);
            this.gpSetStructure.Name = "gpSetStructure";
            this.gpSetStructure.Size = new System.Drawing.Size(356, 670);
            this.gpSetStructure.TabIndex = 28;
            this.gpSetStructure.TabStop = false;
            this.gpSetStructure.Text = "Set Structure";
            // 
            // tvSetSummary
            // 
            this.tvSetSummary.ContextMenuStrip = this.contextMenuStrip1;
            this.tvSetSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSetSummary.HideSelection = false;
            this.tvSetSummary.ImageIndex = 6;
            this.tvSetSummary.ImageList = this.imageList1;
            this.tvSetSummary.Location = new System.Drawing.Point(3, 41);
            this.tvSetSummary.Name = "tvSetSummary";
            this.tvSetSummary.SelectedImageIndex = 0;
            this.tvSetSummary.Size = new System.Drawing.Size(350, 426);
            this.tvSetSummary.TabIndex = 84;
            this.tvSetSummary.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSetSummary_AfterSelect);
            // 
            // pnlSetImage
            // 
            this.pnlSetImage.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlSetImage.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSetImage.Location = new System.Drawing.Point(3, 467);
            this.pnlSetImage.Name = "pnlSetImage";
            this.pnlSetImage.Size = new System.Drawing.Size(350, 200);
            this.pnlSetImage.TabIndex = 83;
            // 
            // toolStrip2
            // 
            this.toolStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSetStructureRefresh,
            this.toolStripSeparator21,
            this.btnCollapseNodes,
            this.btnExpandAll,
            this.toolStripSeparator6,
            this.btnMoveUp,
            this.btnMoveDown,
            this.btnMoveUpBy5,
            this.fldBulkValue,
            this.btnMoveDownBy5,
            this.toolStripSeparator5,
            this.btnSetClear});
            this.toolStrip2.Location = new System.Drawing.Point(3, 16);
            this.toolStrip2.Name = "toolStrip2";
            this.toolStrip2.Size = new System.Drawing.Size(350, 25);
            this.toolStrip2.TabIndex = 26;
            this.toolStrip2.Text = "toolStrip2";
            // 
            // btnSetStructureRefresh
            // 
            this.btnSetStructureRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnSetStructureRefresh.Image")));
            this.btnSetStructureRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetStructureRefresh.Name = "btnSetStructureRefresh";
            this.btnSetStructureRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnSetStructureRefresh.Text = "Refresh";
            this.btnSetStructureRefresh.Click += new System.EventHandler(this.btnSetStructureRefresh_Click);
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
            // btnMoveUp
            // 
            this.btnMoveUp.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUp.Image")));
            this.btnMoveUp.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUp.Name = "btnMoveUp";
            this.btnMoveUp.Size = new System.Drawing.Size(23, 22);
            this.btnMoveUp.Click += new System.EventHandler(this.btnMoveUp_Click);
            // 
            // btnMoveDown
            // 
            this.btnMoveDown.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDown.Image")));
            this.btnMoveDown.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDown.Name = "btnMoveDown";
            this.btnMoveDown.Size = new System.Drawing.Size(23, 22);
            this.btnMoveDown.Click += new System.EventHandler(this.btnMoveDown_Click);
            // 
            // btnMoveUpBy5
            // 
            this.btnMoveUpBy5.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveUpBy5.Image")));
            this.btnMoveUpBy5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveUpBy5.Name = "btnMoveUpBy5";
            this.btnMoveUpBy5.Size = new System.Drawing.Size(23, 22);
            this.btnMoveUpBy5.Click += new System.EventHandler(this.btnMoveUpBy5_Click);
            // 
            // fldBulkValue
            // 
            this.fldBulkValue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldBulkValue.Name = "fldBulkValue";
            this.fldBulkValue.Size = new System.Drawing.Size(20, 25);
            this.fldBulkValue.Text = "5";
            this.fldBulkValue.TextBoxTextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnMoveDownBy5
            // 
            this.btnMoveDownBy5.Image = ((System.Drawing.Image)(resources.GetObject("btnMoveDownBy5.Image")));
            this.btnMoveDownBy5.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnMoveDownBy5.Name = "btnMoveDownBy5";
            this.btnMoveDownBy5.Size = new System.Drawing.Size(23, 22);
            this.btnMoveDownBy5.Click += new System.EventHandler(this.btnMoveDownBy5_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(6, 25);
            // 
            // btnSetClear
            // 
            this.btnSetClear.Image = ((System.Drawing.Image)(resources.GetObject("btnSetClear.Image")));
            this.btnSetClear.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetClear.Name = "btnSetClear";
            this.btnSetClear.Size = new System.Drawing.Size(54, 22);
            this.btnSetClear.Text = "Clear";
            this.btnSetClear.Click += new System.EventHandler(this.btnSetClear_Click);
            // 
            // gpNodeMgmt
            // 
            this.gpNodeMgmt.Controls.Add(this.groupBox3);
            this.gpNodeMgmt.Controls.Add(this.gbPartDetails);
            this.gpNodeMgmt.Controls.Add(this.gpStep);
            this.gpNodeMgmt.Controls.Add(this.gpSubModel);
            this.gpNodeMgmt.Controls.Add(this.gpModel);
            this.gpNodeMgmt.Controls.Add(this.gpSubSet);
            this.gpNodeMgmt.Controls.Add(this.gpSet);
            this.gpNodeMgmt.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpNodeMgmt.Location = new System.Drawing.Point(0, 0);
            this.gpNodeMgmt.Name = "gpNodeMgmt";
            this.gpNodeMgmt.Size = new System.Drawing.Size(1119, 670);
            this.gpNodeMgmt.TabIndex = 33;
            this.gpNodeMgmt.TabStop = false;
            this.gpNodeMgmt.Text = "Node Management";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.chkFBXMissingAc);
            this.groupBox3.Controls.Add(this.chkLDrawColourNameAcEquals);
            this.groupBox3.Controls.Add(this.chkLDrawRefAcEquals);
            this.groupBox3.Controls.Add(this.chkIsSubPart);
            this.groupBox3.Controls.Add(this.dgPartSummary);
            this.groupBox3.Controls.Add(this.chkIsLargeModel);
            this.groupBox3.Controls.Add(this.chkIsSticker);
            this.groupBox3.Controls.Add(this.c);
            this.groupBox3.Controls.Add(this.tsPartSummary);
            this.groupBox3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox3.Location = new System.Drawing.Point(3, 241);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(1113, 327);
            this.groupBox3.TabIndex = 100;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Part Summary";
            // 
            // chkFBXMissingAc
            // 
            this.chkFBXMissingAc.AutoSize = true;
            this.chkFBXMissingAc.Location = new System.Drawing.Point(743, 18);
            this.chkFBXMissingAc.Name = "chkFBXMissingAc";
            this.chkFBXMissingAc.Size = new System.Drawing.Size(84, 17);
            this.chkFBXMissingAc.TabIndex = 81;
            this.chkFBXMissingAc.Text = "FBX Missing";
            this.chkFBXMissingAc.UseVisualStyleBackColor = true;
            this.chkFBXMissingAc.CheckedChanged += new System.EventHandler(this.chkFBXMissingAc_CheckedChanged);
            // 
            // chkLDrawColourNameAcEquals
            // 
            this.chkLDrawColourNameAcEquals.AutoSize = true;
            this.chkLDrawColourNameAcEquals.Location = new System.Drawing.Point(679, 18);
            this.chkLDrawColourNameAcEquals.Name = "chkLDrawColourNameAcEquals";
            this.chkLDrawColourNameAcEquals.Size = new System.Drawing.Size(32, 17);
            this.chkLDrawColourNameAcEquals.TabIndex = 80;
            this.chkLDrawColourNameAcEquals.Text = "=";
            this.chkLDrawColourNameAcEquals.UseVisualStyleBackColor = true;
            this.chkLDrawColourNameAcEquals.CheckedChanged += new System.EventHandler(this.chkLDrawColourNameAcEquals_CheckedChanged);
            // 
            // chkLDrawRefAcEquals
            // 
            this.chkLDrawRefAcEquals.AutoSize = true;
            this.chkLDrawRefAcEquals.Location = new System.Drawing.Point(631, 18);
            this.chkLDrawRefAcEquals.Name = "chkLDrawRefAcEquals";
            this.chkLDrawRefAcEquals.Size = new System.Drawing.Size(32, 17);
            this.chkLDrawRefAcEquals.TabIndex = 79;
            this.chkLDrawRefAcEquals.Text = "=";
            this.chkLDrawRefAcEquals.UseVisualStyleBackColor = true;
            this.chkLDrawRefAcEquals.CheckedChanged += new System.EventHandler(this.chkLDrawRefAcEquals_CheckedChanged);
            // 
            // chkIsSubPart
            // 
            this.chkIsSubPart.AutoSize = true;
            this.chkIsSubPart.Location = new System.Drawing.Point(336, 433);
            this.chkIsSubPart.Name = "chkIsSubPart";
            this.chkIsSubPart.Size = new System.Drawing.Size(81, 17);
            this.chkIsSubPart.TabIndex = 78;
            this.chkIsSubPart.Text = "Is SubPart?";
            this.chkIsSubPart.UseVisualStyleBackColor = false;
            // 
            // dgPartSummary
            // 
            this.dgPartSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgPartSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPartSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPartSummary.Location = new System.Drawing.Point(3, 41);
            this.dgPartSummary.Name = "dgPartSummary";
            this.dgPartSummary.Size = new System.Drawing.Size(1107, 261);
            this.dgPartSummary.TabIndex = 77;
            this.dgPartSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPartSummary_CellClick);
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
            this.lblPartCount,
            this.lblPartSummaryItemFilteredCount});
            this.c.Location = new System.Drawing.Point(3, 302);
            this.c.Name = "c";
            this.c.Size = new System.Drawing.Size(1107, 22);
            this.c.TabIndex = 67;
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
            this.toolStripSeparator7,
            this.lblLDrawRefAc,
            this.fldLDrawRefAc,
            this.lblLDrawColourNameAc,
            this.fldLDrawColourNameAc});
            this.tsPartSummary.Location = new System.Drawing.Point(3, 16);
            this.tsPartSummary.Name = "tsPartSummary";
            this.tsPartSummary.Size = new System.Drawing.Size(1107, 25);
            this.tsPartSummary.TabIndex = 26;
            this.tsPartSummary.Text = "toolStrip4";
            // 
            // btnPartSummaryCopyToClipboard
            // 
            this.btnPartSummaryCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnPartSummaryCopyToClipboard.Image")));
            this.btnPartSummaryCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartSummaryCopyToClipboard.Name = "btnPartSummaryCopyToClipboard";
            this.btnPartSummaryCopyToClipboard.Size = new System.Drawing.Size(124, 22);
            this.btnPartSummaryCopyToClipboard.Text = "Copy to Clipboard";
            this.btnPartSummaryCopyToClipboard.Click += new System.EventHandler(this.btnPartSummaryCopyToClipboard_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(6, 25);
            // 
            // lblLDrawRefAc
            // 
            this.lblLDrawRefAc.Name = "lblLDrawRefAc";
            this.lblLDrawRefAc.Size = new System.Drawing.Size(63, 22);
            this.lblLDrawRefAc.Text = "LDraw Ref:";
            // 
            // fldLDrawRefAc
            // 
            this.fldLDrawRefAc.BackColor = System.Drawing.Color.Wheat;
            this.fldLDrawRefAc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawRefAc.Name = "fldLDrawRefAc";
            this.fldLDrawRefAc.Size = new System.Drawing.Size(100, 25);
            this.fldLDrawRefAc.TextChanged += new System.EventHandler(this.fldLDrawRefAc_TextChanged);
            // 
            // lblLDrawColourNameAc
            // 
            this.lblLDrawColourNameAc.Name = "lblLDrawColourNameAc";
            this.lblLDrawColourNameAc.Size = new System.Drawing.Size(117, 22);
            this.lblLDrawColourNameAc.Text = "LDraw Colour Name:";
            // 
            // fldLDrawColourNameAc
            // 
            this.fldLDrawColourNameAc.BackColor = System.Drawing.Color.Wheat;
            this.fldLDrawColourNameAc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawColourNameAc.Name = "fldLDrawColourNameAc";
            this.fldLDrawColourNameAc.Size = new System.Drawing.Size(125, 25);
            this.fldLDrawColourNameAc.TextChanged += new System.EventHandler(this.fldLDrawColourNameAc_TextChanged);
            // 
            // gbPartDetails
            // 
            this.gbPartDetails.Controls.Add(this.tsBasePartCollection);
            this.gbPartDetails.Controls.Add(this.tsPartPosDetails);
            this.gbPartDetails.Controls.Add(this.chkBasePartCollection);
            this.gbPartDetails.Controls.Add(this.tsPartDetails);
            this.gbPartDetails.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbPartDetails.Location = new System.Drawing.Point(3, 568);
            this.gbPartDetails.Name = "gbPartDetails";
            this.gbPartDetails.Size = new System.Drawing.Size(1113, 99);
            this.gbPartDetails.TabIndex = 99;
            this.gbPartDetails.TabStop = false;
            this.gbPartDetails.Text = "Part Details";
            // 
            // tsBasePartCollection
            // 
            this.tsBasePartCollection.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPartType,
            this.fldPartType,
            this.lblLDrawSize,
            this.fldLDrawSize});
            this.tsBasePartCollection.Location = new System.Drawing.Point(3, 66);
            this.tsBasePartCollection.Name = "tsBasePartCollection";
            this.tsBasePartCollection.Size = new System.Drawing.Size(1107, 25);
            this.tsBasePartCollection.TabIndex = 79;
            this.tsBasePartCollection.Text = "toolStrip10";
            // 
            // lblPartType
            // 
            this.lblPartType.Name = "lblPartType";
            this.lblPartType.Size = new System.Drawing.Size(55, 22);
            this.lblPartType.Text = "Part Type";
            // 
            // fldPartType
            // 
            this.fldPartType.Items.AddRange(new object[] {
            "BASIC",
            "COMPOSITE"});
            this.fldPartType.Name = "fldPartType";
            this.fldPartType.Size = new System.Drawing.Size(100, 25);
            this.fldPartType.Text = "BASIC";
            // 
            // lblLDrawSize
            // 
            this.lblLDrawSize.Name = "lblLDrawSize";
            this.lblLDrawSize.Size = new System.Drawing.Size(63, 22);
            this.lblLDrawSize.Text = "LDraw Size";
            // 
            // fldLDrawSize
            // 
            this.fldLDrawSize.BackColor = System.Drawing.Color.LightGray;
            this.fldLDrawSize.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldLDrawSize.Name = "fldLDrawSize";
            this.fldLDrawSize.Size = new System.Drawing.Size(25, 25);
            // 
            // tsPartPosDetails
            // 
            this.tsPartPosDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel31,
            this.fldPartPosX,
            this.toolStripLabel32,
            this.fldPartPosY,
            this.toolStripLabel33,
            this.fldPartPosZ,
            this.toolStripSeparator15,
            this.toolStripLabel34,
            this.fldPartRotX,
            this.toolStripLabel35,
            this.fldPartRotY,
            this.toolStripLabel36,
            this.fldPartRotZ});
            this.tsPartPosDetails.Location = new System.Drawing.Point(3, 41);
            this.tsPartPosDetails.Name = "tsPartPosDetails";
            this.tsPartPosDetails.Size = new System.Drawing.Size(1107, 25);
            this.tsPartPosDetails.TabIndex = 78;
            this.tsPartPosDetails.Text = "toolStrip10";
            // 
            // toolStripLabel31
            // 
            this.toolStripLabel31.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel31.Name = "toolStripLabel31";
            this.toolStripLabel31.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel31.Text = "PosX";
            // 
            // fldPartPosX
            // 
            this.fldPartPosX.BackColor = System.Drawing.Color.LightGray;
            this.fldPartPosX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartPosX.Name = "fldPartPosX";
            this.fldPartPosX.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripLabel32
            // 
            this.toolStripLabel32.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel32.Name = "toolStripLabel32";
            this.toolStripLabel32.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel32.Text = "PosY";
            // 
            // fldPartPosY
            // 
            this.fldPartPosY.BackColor = System.Drawing.Color.LightGray;
            this.fldPartPosY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartPosY.Name = "fldPartPosY";
            this.fldPartPosY.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripLabel33
            // 
            this.toolStripLabel33.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel33.Name = "toolStripLabel33";
            this.toolStripLabel33.Size = new System.Drawing.Size(33, 22);
            this.toolStripLabel33.Text = "PosZ";
            // 
            // fldPartPosZ
            // 
            this.fldPartPosZ.BackColor = System.Drawing.Color.LightGray;
            this.fldPartPosZ.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartPosZ.Name = "fldPartPosZ";
            this.fldPartPosZ.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripSeparator15
            // 
            this.toolStripSeparator15.Name = "toolStripSeparator15";
            this.toolStripSeparator15.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel34
            // 
            this.toolStripLabel34.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel34.Name = "toolStripLabel34";
            this.toolStripLabel34.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel34.Text = "RotX";
            // 
            // fldPartRotX
            // 
            this.fldPartRotX.BackColor = System.Drawing.Color.LightGray;
            this.fldPartRotX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartRotX.Name = "fldPartRotX";
            this.fldPartRotX.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripLabel35
            // 
            this.toolStripLabel35.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel35.Name = "toolStripLabel35";
            this.toolStripLabel35.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel35.Text = "RotY";
            // 
            // fldPartRotY
            // 
            this.fldPartRotY.BackColor = System.Drawing.Color.LightGray;
            this.fldPartRotY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartRotY.Name = "fldPartRotY";
            this.fldPartRotY.Size = new System.Drawing.Size(50, 25);
            // 
            // toolStripLabel36
            // 
            this.toolStripLabel36.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel36.Name = "toolStripLabel36";
            this.toolStripLabel36.Size = new System.Drawing.Size(32, 22);
            this.toolStripLabel36.Text = "RotZ";
            // 
            // fldPartRotZ
            // 
            this.fldPartRotZ.BackColor = System.Drawing.Color.LightGray;
            this.fldPartRotZ.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPartRotZ.Name = "fldPartRotZ";
            this.fldPartRotZ.Size = new System.Drawing.Size(50, 25);
            // 
            // chkBasePartCollection
            // 
            this.chkBasePartCollection.AutoSize = true;
            this.chkBasePartCollection.Enabled = false;
            this.chkBasePartCollection.Location = new System.Drawing.Point(1141, 19);
            this.chkBasePartCollection.Name = "chkBasePartCollection";
            this.chkBasePartCollection.Size = new System.Drawing.Size(15, 14);
            this.chkBasePartCollection.TabIndex = 76;
            this.chkBasePartCollection.UseVisualStyleBackColor = false;
            // 
            // tsPartDetails
            // 
            this.tsPartDetails.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel1,
            this.fldLDrawRef,
            this.fldLDrawImage,
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
            this.btnPartDelete,
            this.btnAddPartToBasePartCollection,
            this.toolStripSeparator24,
            this.btnGenerateDatFile});
            this.tsPartDetails.Location = new System.Drawing.Point(3, 16);
            this.tsPartDetails.Name = "tsPartDetails";
            this.tsPartDetails.Size = new System.Drawing.Size(1107, 25);
            this.tsPartDetails.TabIndex = 75;
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
            this.fldLDrawRef.Leave += new System.EventHandler(this.fldLDrawRef_Leave);
            // 
            // fldLDrawImage
            // 
            this.fldLDrawImage.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.fldLDrawImage.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.fldLDrawImage.Name = "fldLDrawImage";
            this.fldLDrawImage.Size = new System.Drawing.Size(23, 22);
            this.fldLDrawImage.Click += new System.EventHandler(this.fldLDrawImage_Click);
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
            this.fldLDrawColourID.Leave += new System.EventHandler(this.fldLDrawColourID_Leave);
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
            this.fldLDrawColourName.SelectedIndexChanged += new System.EventHandler(this.fldLDrawColourName_SelectedIndexChanged);
            this.fldLDrawColourName.Leave += new System.EventHandler(this.fldLDrawColourName_Leave);
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
            this.btnPartClear.Click += new System.EventHandler(this.btnPartClear_Click);
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
            this.btnPartAdd.Click += new System.EventHandler(this.btnPartAdd_Click);
            // 
            // btnPartSave
            // 
            this.btnPartSave.Image = ((System.Drawing.Image)(resources.GetObject("btnPartSave.Image")));
            this.btnPartSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartSave.Name = "btnPartSave";
            this.btnPartSave.Size = new System.Drawing.Size(51, 22);
            this.btnPartSave.Text = "Save";
            this.btnPartSave.Click += new System.EventHandler(this.btnPartSave_Click);
            // 
            // btnPartDelete
            // 
            this.btnPartDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnPartDelete.Image")));
            this.btnPartDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartDelete.Name = "btnPartDelete";
            this.btnPartDelete.Size = new System.Drawing.Size(60, 22);
            this.btnPartDelete.Text = "Delete";
            this.btnPartDelete.Click += new System.EventHandler(this.btnPartDelete_Click);
            // 
            // btnAddPartToBasePartCollection
            // 
            this.btnAddPartToBasePartCollection.Enabled = false;
            this.btnAddPartToBasePartCollection.Image = ((System.Drawing.Image)(resources.GetObject("btnAddPartToBasePartCollection.Image")));
            this.btnAddPartToBasePartCollection.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAddPartToBasePartCollection.Name = "btnAddPartToBasePartCollection";
            this.btnAddPartToBasePartCollection.Size = new System.Drawing.Size(23, 22);
            this.btnAddPartToBasePartCollection.Click += new System.EventHandler(this.btnAddPartToBasePartCollection2_Click);
            // 
            // toolStripSeparator24
            // 
            this.toolStripSeparator24.Name = "toolStripSeparator24";
            this.toolStripSeparator24.Size = new System.Drawing.Size(6, 25);
            // 
            // btnGenerateDatFile
            // 
            this.btnGenerateDatFile.Image = ((System.Drawing.Image)(resources.GetObject("btnGenerateDatFile.Image")));
            this.btnGenerateDatFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnGenerateDatFile.Name = "btnGenerateDatFile";
            this.btnGenerateDatFile.Size = new System.Drawing.Size(120, 22);
            this.btnGenerateDatFile.Text = "Generate .DAT file";
            this.btnGenerateDatFile.Click += new System.EventHandler(this.btnGenerateDatFile_Click);
            // 
            // gpStep
            // 
            this.gpStep.Controls.Add(this.toolStrip8);
            this.gpStep.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpStep.Location = new System.Drawing.Point(3, 196);
            this.gpStep.Name = "gpStep";
            this.gpStep.Size = new System.Drawing.Size(1113, 45);
            this.gpStep.TabIndex = 97;
            this.gpStep.TabStop = false;
            this.gpStep.Text = "Step";
            // 
            // toolStrip8
            // 
            this.toolStrip8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip8.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel22,
            this.fldStepParentSubSetRef,
            this.toolStripLabel23,
            this.fldStepParentModelRef,
            this.toolStripLabel18,
            this.fldPureStepNo,
            this.toolStripLabel19,
            this.fldStepLevel,
            this.toolStripLabel21,
            this.fldStepBook,
            this.toolStripLabel24,
            this.fldStepPage,
            this.toolStripSeparator17,
            this.toolStripLabel37,
            this.fldModelRotX,
            this.toolStripLabel38,
            this.fldModelRotY,
            this.toolStripLabel39,
            this.fldModelRotZ,
            this.toolStripSeparator19,
            this.btnStepSave,
            this.btnStepDelete});
            this.toolStrip8.Location = new System.Drawing.Point(3, 16);
            this.toolStrip8.Name = "toolStrip8";
            this.toolStrip8.Size = new System.Drawing.Size(1107, 26);
            this.toolStrip8.TabIndex = 73;
            this.toolStrip8.Text = "toolStrip8";
            // 
            // toolStripLabel22
            // 
            this.toolStripLabel22.Name = "toolStripLabel22";
            this.toolStripLabel22.Size = new System.Drawing.Size(63, 23);
            this.toolStripLabel22.Text = "SubSetRef:";
            // 
            // fldStepParentSubSetRef
            // 
            this.fldStepParentSubSetRef.Enabled = false;
            this.fldStepParentSubSetRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStepParentSubSetRef.Name = "fldStepParentSubSetRef";
            this.fldStepParentSubSetRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel23
            // 
            this.toolStripLabel23.Name = "toolStripLabel23";
            this.toolStripLabel23.Size = new System.Drawing.Size(64, 23);
            this.toolStripLabel23.Text = "Model Ref:";
            // 
            // fldStepParentModelRef
            // 
            this.fldStepParentModelRef.Enabled = false;
            this.fldStepParentModelRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStepParentModelRef.Name = "fldStepParentModelRef";
            this.fldStepParentModelRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel18
            // 
            this.toolStripLabel18.Name = "toolStripLabel18";
            this.toolStripLabel18.Size = new System.Drawing.Size(79, 23);
            this.toolStripLabel18.Text = "Pure Step No:";
            // 
            // fldPureStepNo
            // 
            this.fldPureStepNo.Enabled = false;
            this.fldPureStepNo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldPureStepNo.Name = "fldPureStepNo";
            this.fldPureStepNo.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel19
            // 
            this.toolStripLabel19.Name = "toolStripLabel19";
            this.toolStripLabel19.Size = new System.Drawing.Size(63, 23);
            this.toolStripLabel19.Text = "Step Level:";
            // 
            // fldStepLevel
            // 
            this.fldStepLevel.Enabled = false;
            this.fldStepLevel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStepLevel.Name = "fldStepLevel";
            this.fldStepLevel.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel21
            // 
            this.toolStripLabel21.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel21.Name = "toolStripLabel21";
            this.toolStripLabel21.Size = new System.Drawing.Size(23, 23);
            this.toolStripLabel21.Text = "Bk:";
            // 
            // fldStepBook
            // 
            this.fldStepBook.BackColor = System.Drawing.Color.LightGray;
            this.fldStepBook.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStepBook.Name = "fldStepBook";
            this.fldStepBook.Size = new System.Drawing.Size(25, 26);
            // 
            // toolStripLabel24
            // 
            this.toolStripLabel24.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel24.Name = "toolStripLabel24";
            this.toolStripLabel24.Size = new System.Drawing.Size(24, 23);
            this.toolStripLabel24.Text = "Pg:";
            // 
            // fldStepPage
            // 
            this.fldStepPage.BackColor = System.Drawing.Color.LightGray;
            this.fldStepPage.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldStepPage.Name = "fldStepPage";
            this.fldStepPage.Size = new System.Drawing.Size(25, 26);
            // 
            // toolStripSeparator17
            // 
            this.toolStripSeparator17.Name = "toolStripSeparator17";
            this.toolStripSeparator17.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel37
            // 
            this.toolStripLabel37.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel37.Name = "toolStripLabel37";
            this.toolStripLabel37.Size = new System.Drawing.Size(69, 23);
            this.toolStripLabel37.Text = "Model RotX";
            // 
            // fldModelRotX
            // 
            this.fldModelRotX.BackColor = System.Drawing.Color.LightGray;
            this.fldModelRotX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelRotX.Name = "fldModelRotX";
            this.fldModelRotX.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel38
            // 
            this.toolStripLabel38.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel38.Name = "toolStripLabel38";
            this.toolStripLabel38.Size = new System.Drawing.Size(69, 23);
            this.toolStripLabel38.Text = "Model RotY";
            // 
            // fldModelRotY
            // 
            this.fldModelRotY.BackColor = System.Drawing.Color.LightGray;
            this.fldModelRotY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelRotY.Name = "fldModelRotY";
            this.fldModelRotY.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel39
            // 
            this.toolStripLabel39.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel39.Name = "toolStripLabel39";
            this.toolStripLabel39.Size = new System.Drawing.Size(69, 23);
            this.toolStripLabel39.Text = "Model RotZ";
            // 
            // fldModelRotZ
            // 
            this.fldModelRotZ.BackColor = System.Drawing.Color.LightGray;
            this.fldModelRotZ.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelRotZ.Name = "fldModelRotZ";
            this.fldModelRotZ.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripSeparator19
            // 
            this.toolStripSeparator19.Name = "toolStripSeparator19";
            this.toolStripSeparator19.Size = new System.Drawing.Size(6, 26);
            // 
            // btnStepSave
            // 
            this.btnStepSave.Image = ((System.Drawing.Image)(resources.GetObject("btnStepSave.Image")));
            this.btnStepSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStepSave.Name = "btnStepSave";
            this.btnStepSave.Size = new System.Drawing.Size(51, 23);
            this.btnStepSave.Text = "Save";
            this.btnStepSave.Click += new System.EventHandler(this.btnStepSave_Click);
            // 
            // btnStepDelete
            // 
            this.btnStepDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnStepDelete.Image")));
            this.btnStepDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnStepDelete.Name = "btnStepDelete";
            this.btnStepDelete.Size = new System.Drawing.Size(60, 20);
            this.btnStepDelete.Text = "Delete";
            this.btnStepDelete.Click += new System.EventHandler(this.btnStepDelete_Click);
            // 
            // gpSubModel
            // 
            this.gpSubModel.Controls.Add(this.toolStrip9);
            this.gpSubModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpSubModel.Location = new System.Drawing.Point(3, 151);
            this.gpSubModel.Name = "gpSubModel";
            this.gpSubModel.Size = new System.Drawing.Size(1113, 45);
            this.gpSubModel.TabIndex = 96;
            this.gpSubModel.TabStop = false;
            this.gpSubModel.Text = "SubModel";
            // 
            // toolStrip9
            // 
            this.toolStrip9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip9.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel16,
            this.fldSubModelCurrentRef,
            this.toolStripLabel17,
            this.fldSubModelNewRef,
            this.toolStripLabel20,
            this.fldSubModelDescription,
            this.toolStripSeparator9,
            this.toolStripLabel25,
            this.fldSubModelPosX,
            this.toolStripLabel26,
            this.fldSubModelPosY,
            this.toolStripLabel27,
            this.fldSubModelPosZ,
            this.toolStripSeparator10,
            this.toolStripLabel28,
            this.fldSubModelRotX,
            this.toolStripLabel29,
            this.fldSubModelRotY,
            this.toolStripLabel30,
            this.fldSubModelRotZ,
            this.toolStripSeparator11,
            this.btnSubModelSave,
            this.btnSubModelDelete});
            this.toolStrip9.Location = new System.Drawing.Point(3, 16);
            this.toolStrip9.Name = "toolStrip9";
            this.toolStrip9.Size = new System.Drawing.Size(1107, 26);
            this.toolStrip9.TabIndex = 73;
            this.toolStrip9.Text = "toolStrip9";
            // 
            // toolStripLabel16
            // 
            this.toolStripLabel16.Name = "toolStripLabel16";
            this.toolStripLabel16.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel16.Text = "Current Ref:";
            // 
            // fldSubModelCurrentRef
            // 
            this.fldSubModelCurrentRef.Enabled = false;
            this.fldSubModelCurrentRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelCurrentRef.Name = "fldSubModelCurrentRef";
            this.fldSubModelCurrentRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel17
            // 
            this.toolStripLabel17.Name = "toolStripLabel17";
            this.toolStripLabel17.Size = new System.Drawing.Size(54, 23);
            this.toolStripLabel17.Text = "New Ref:";
            // 
            // fldSubModelNewRef
            // 
            this.fldSubModelNewRef.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelNewRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelNewRef.Name = "fldSubModelNewRef";
            this.fldSubModelNewRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel20
            // 
            this.toolStripLabel20.Name = "toolStripLabel20";
            this.toolStripLabel20.Size = new System.Drawing.Size(67, 23);
            this.toolStripLabel20.Text = "Description";
            // 
            // fldSubModelDescription
            // 
            this.fldSubModelDescription.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelDescription.Name = "fldSubModelDescription";
            this.fldSubModelDescription.Size = new System.Drawing.Size(200, 26);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel25
            // 
            this.toolStripLabel25.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel25.Name = "toolStripLabel25";
            this.toolStripLabel25.Size = new System.Drawing.Size(33, 23);
            this.toolStripLabel25.Text = "PosX";
            // 
            // fldSubModelPosX
            // 
            this.fldSubModelPosX.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelPosX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelPosX.Name = "fldSubModelPosX";
            this.fldSubModelPosX.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel26
            // 
            this.toolStripLabel26.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel26.Name = "toolStripLabel26";
            this.toolStripLabel26.Size = new System.Drawing.Size(33, 23);
            this.toolStripLabel26.Text = "PosY";
            // 
            // fldSubModelPosY
            // 
            this.fldSubModelPosY.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelPosY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelPosY.Name = "fldSubModelPosY";
            this.fldSubModelPosY.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel27
            // 
            this.toolStripLabel27.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel27.Name = "toolStripLabel27";
            this.toolStripLabel27.Size = new System.Drawing.Size(33, 23);
            this.toolStripLabel27.Text = "PosZ";
            // 
            // fldSubModelPosZ
            // 
            this.fldSubModelPosZ.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelPosZ.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelPosZ.Name = "fldSubModelPosZ";
            this.fldSubModelPosZ.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripSeparator10
            // 
            this.toolStripSeparator10.Name = "toolStripSeparator10";
            this.toolStripSeparator10.Size = new System.Drawing.Size(6, 26);
            // 
            // toolStripLabel28
            // 
            this.toolStripLabel28.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel28.Name = "toolStripLabel28";
            this.toolStripLabel28.Size = new System.Drawing.Size(32, 23);
            this.toolStripLabel28.Text = "RotX";
            // 
            // fldSubModelRotX
            // 
            this.fldSubModelRotX.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelRotX.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelRotX.Name = "fldSubModelRotX";
            this.fldSubModelRotX.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel29
            // 
            this.toolStripLabel29.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel29.Name = "toolStripLabel29";
            this.toolStripLabel29.Size = new System.Drawing.Size(32, 23);
            this.toolStripLabel29.Text = "RotY";
            // 
            // fldSubModelRotY
            // 
            this.fldSubModelRotY.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelRotY.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelRotY.Name = "fldSubModelRotY";
            this.fldSubModelRotY.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripLabel30
            // 
            this.toolStripLabel30.BackColor = System.Drawing.Color.Yellow;
            this.toolStripLabel30.Name = "toolStripLabel30";
            this.toolStripLabel30.Size = new System.Drawing.Size(32, 23);
            this.toolStripLabel30.Text = "RotZ";
            // 
            // fldSubModelRotZ
            // 
            this.fldSubModelRotZ.BackColor = System.Drawing.Color.LightGray;
            this.fldSubModelRotZ.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubModelRotZ.Name = "fldSubModelRotZ";
            this.fldSubModelRotZ.Size = new System.Drawing.Size(50, 26);
            // 
            // toolStripSeparator11
            // 
            this.toolStripSeparator11.Name = "toolStripSeparator11";
            this.toolStripSeparator11.Size = new System.Drawing.Size(6, 26);
            // 
            // btnSubModelSave
            // 
            this.btnSubModelSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSubModelSave.Image")));
            this.btnSubModelSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubModelSave.Name = "btnSubModelSave";
            this.btnSubModelSave.Size = new System.Drawing.Size(51, 20);
            this.btnSubModelSave.Text = "Save";
            this.btnSubModelSave.Click += new System.EventHandler(this.btnSubModelSave_Click);
            // 
            // btnSubModelDelete
            // 
            this.btnSubModelDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnSubModelDelete.Image")));
            this.btnSubModelDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubModelDelete.Name = "btnSubModelDelete";
            this.btnSubModelDelete.Size = new System.Drawing.Size(60, 20);
            this.btnSubModelDelete.Text = "Delete";
            this.btnSubModelDelete.Click += new System.EventHandler(this.btnSubModelDelete_Click);
            // 
            // gpModel
            // 
            this.gpModel.Controls.Add(this.toolStrip7);
            this.gpModel.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpModel.Location = new System.Drawing.Point(3, 106);
            this.gpModel.Name = "gpModel";
            this.gpModel.Size = new System.Drawing.Size(1113, 45);
            this.gpModel.TabIndex = 95;
            this.gpModel.TabStop = false;
            this.gpModel.Text = "Model";
            // 
            // toolStrip7
            // 
            this.toolStrip7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip7.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel14,
            this.fldModelCurrentRef,
            this.toolStripLabel10,
            this.fldModelNewRef,
            this.toolStripLabel11,
            this.fldModelDescription,
            this.toolStripLabel12,
            this.fldModelType,
            this.btnModelSave,
            this.btnModelDelete,
            this.toolStripSeparator8,
            this.btnAdjustModelStepNos});
            this.toolStrip7.Location = new System.Drawing.Point(3, 16);
            this.toolStrip7.Name = "toolStrip7";
            this.toolStrip7.Size = new System.Drawing.Size(1107, 26);
            this.toolStrip7.TabIndex = 73;
            this.toolStrip7.Text = "toolStrip7";
            // 
            // toolStripLabel14
            // 
            this.toolStripLabel14.Name = "toolStripLabel14";
            this.toolStripLabel14.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel14.Text = "Current Ref:";
            // 
            // fldModelCurrentRef
            // 
            this.fldModelCurrentRef.Enabled = false;
            this.fldModelCurrentRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelCurrentRef.Name = "fldModelCurrentRef";
            this.fldModelCurrentRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel10
            // 
            this.toolStripLabel10.Name = "toolStripLabel10";
            this.toolStripLabel10.Size = new System.Drawing.Size(54, 23);
            this.toolStripLabel10.Text = "New Ref:";
            // 
            // fldModelNewRef
            // 
            this.fldModelNewRef.BackColor = System.Drawing.Color.LightGray;
            this.fldModelNewRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelNewRef.Name = "fldModelNewRef";
            this.fldModelNewRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel11
            // 
            this.toolStripLabel11.Name = "toolStripLabel11";
            this.toolStripLabel11.Size = new System.Drawing.Size(67, 23);
            this.toolStripLabel11.Text = "Description";
            // 
            // fldModelDescription
            // 
            this.fldModelDescription.BackColor = System.Drawing.Color.LightGray;
            this.fldModelDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldModelDescription.Name = "fldModelDescription";
            this.fldModelDescription.Size = new System.Drawing.Size(200, 26);
            // 
            // toolStripLabel12
            // 
            this.toolStripLabel12.Name = "toolStripLabel12";
            this.toolStripLabel12.Size = new System.Drawing.Size(104, 23);
            this.toolStripLabel12.Text = "LDraw Model Type";
            // 
            // fldModelType
            // 
            this.fldModelType.BackColor = System.Drawing.Color.LightGray;
            this.fldModelType.Items.AddRange(new object[] {
            "MODEL",
            "MINIFIG"});
            this.fldModelType.Name = "fldModelType";
            this.fldModelType.Size = new System.Drawing.Size(121, 26);
            // 
            // btnModelSave
            // 
            this.btnModelSave.Image = ((System.Drawing.Image)(resources.GetObject("btnModelSave.Image")));
            this.btnModelSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModelSave.Name = "btnModelSave";
            this.btnModelSave.Size = new System.Drawing.Size(51, 23);
            this.btnModelSave.Text = "Save";
            this.btnModelSave.Click += new System.EventHandler(this.btnModelSave_Click);
            // 
            // btnModelDelete
            // 
            this.btnModelDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnModelDelete.Image")));
            this.btnModelDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnModelDelete.Name = "btnModelDelete";
            this.btnModelDelete.Size = new System.Drawing.Size(60, 23);
            this.btnModelDelete.Text = "Delete";
            this.btnModelDelete.Click += new System.EventHandler(this.btnModelDelete_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(6, 26);
            // 
            // btnAdjustModelStepNos
            // 
            this.btnAdjustModelStepNos.Image = ((System.Drawing.Image)(resources.GetObject("btnAdjustModelStepNos.Image")));
            this.btnAdjustModelStepNos.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnAdjustModelStepNos.Name = "btnAdjustModelStepNos";
            this.btnAdjustModelStepNos.Size = new System.Drawing.Size(111, 23);
            this.btnAdjustModelStepNos.Text = "Adjust Step Nos";
            this.btnAdjustModelStepNos.Click += new System.EventHandler(this.btnAdjustModelStepNos_Click);
            // 
            // gpSubSet
            // 
            this.gpSubSet.Controls.Add(this.toolStrip3);
            this.gpSubSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpSubSet.Location = new System.Drawing.Point(3, 61);
            this.gpSubSet.Name = "gpSubSet";
            this.gpSubSet.Size = new System.Drawing.Size(1113, 45);
            this.gpSubSet.TabIndex = 94;
            this.gpSubSet.TabStop = false;
            this.gpSubSet.Text = "SubSet";
            // 
            // toolStrip3
            // 
            this.toolStrip3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel13,
            this.fldSubSetCurrentRef,
            this.toolStripLabel5,
            this.fldSubSetNewRef,
            this.toolStripLabel6,
            this.fldSubSetDescription,
            this.toolStripLabel7,
            this.fldSubSetType,
            this.btnSubSetSave,
            this.btnSubSetDelete});
            this.toolStrip3.Location = new System.Drawing.Point(3, 16);
            this.toolStrip3.Name = "toolStrip3";
            this.toolStrip3.Size = new System.Drawing.Size(1107, 26);
            this.toolStrip3.TabIndex = 73;
            this.toolStrip3.Text = "toolStrip3";
            // 
            // toolStripLabel13
            // 
            this.toolStripLabel13.Name = "toolStripLabel13";
            this.toolStripLabel13.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel13.Text = "Current Ref:";
            // 
            // fldSubSetCurrentRef
            // 
            this.fldSubSetCurrentRef.Enabled = false;
            this.fldSubSetCurrentRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubSetCurrentRef.Name = "fldSubSetCurrentRef";
            this.fldSubSetCurrentRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel5
            // 
            this.toolStripLabel5.Name = "toolStripLabel5";
            this.toolStripLabel5.Size = new System.Drawing.Size(54, 23);
            this.toolStripLabel5.Text = "New Ref:";
            // 
            // fldSubSetNewRef
            // 
            this.fldSubSetNewRef.BackColor = System.Drawing.Color.LightGray;
            this.fldSubSetNewRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubSetNewRef.Name = "fldSubSetNewRef";
            this.fldSubSetNewRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel6
            // 
            this.toolStripLabel6.Name = "toolStripLabel6";
            this.toolStripLabel6.Size = new System.Drawing.Size(67, 23);
            this.toolStripLabel6.Text = "Description";
            // 
            // fldSubSetDescription
            // 
            this.fldSubSetDescription.BackColor = System.Drawing.Color.LightGray;
            this.fldSubSetDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSubSetDescription.Name = "fldSubSetDescription";
            this.fldSubSetDescription.Size = new System.Drawing.Size(200, 26);
            // 
            // toolStripLabel7
            // 
            this.toolStripLabel7.Name = "toolStripLabel7";
            this.toolStripLabel7.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel7.Text = "SubSet Type";
            // 
            // fldSubSetType
            // 
            this.fldSubSetType.BackColor = System.Drawing.Color.LightGray;
            this.fldSubSetType.Items.AddRange(new object[] {
            "UNOFFICIAL",
            "OFFICIAL"});
            this.fldSubSetType.Name = "fldSubSetType";
            this.fldSubSetType.Size = new System.Drawing.Size(121, 26);
            // 
            // btnSubSetSave
            // 
            this.btnSubSetSave.Image = ((System.Drawing.Image)(resources.GetObject("btnSubSetSave.Image")));
            this.btnSubSetSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubSetSave.Name = "btnSubSetSave";
            this.btnSubSetSave.Size = new System.Drawing.Size(51, 23);
            this.btnSubSetSave.Text = "Save";
            this.btnSubSetSave.Click += new System.EventHandler(this.btnSubSetSave_Click);
            // 
            // btnSubSetDelete
            // 
            this.btnSubSetDelete.Image = ((System.Drawing.Image)(resources.GetObject("btnSubSetDelete.Image")));
            this.btnSubSetDelete.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSubSetDelete.Name = "btnSubSetDelete";
            this.btnSubSetDelete.Size = new System.Drawing.Size(60, 23);
            this.btnSubSetDelete.Text = "Delete";
            this.btnSubSetDelete.Click += new System.EventHandler(this.btnSubSetDelete_Click);
            // 
            // gpSet
            // 
            this.gpSet.Controls.Add(this.toolStrip6);
            this.gpSet.Dock = System.Windows.Forms.DockStyle.Top;
            this.gpSet.Location = new System.Drawing.Point(3, 16);
            this.gpSet.Name = "gpSet";
            this.gpSet.Size = new System.Drawing.Size(1113, 45);
            this.gpSet.TabIndex = 92;
            this.gpSet.TabStop = false;
            this.gpSet.Text = "Set";
            // 
            // toolStrip6
            // 
            this.toolStrip6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripLabel15,
            this.fldSetCurrentRef,
            this.toolStripLabel8,
            this.fldSetNewRef,
            this.toolStripLabel9,
            this.fldSetDescription,
            this.btnSetSaveNode});
            this.toolStrip6.Location = new System.Drawing.Point(3, 16);
            this.toolStrip6.Name = "toolStrip6";
            this.toolStrip6.Size = new System.Drawing.Size(1107, 26);
            this.toolStrip6.TabIndex = 73;
            this.toolStrip6.Text = "toolStrip6";
            // 
            // toolStripLabel15
            // 
            this.toolStripLabel15.Name = "toolStripLabel15";
            this.toolStripLabel15.Size = new System.Drawing.Size(70, 23);
            this.toolStripLabel15.Text = "Current Ref:";
            // 
            // fldSetCurrentRef
            // 
            this.fldSetCurrentRef.Enabled = false;
            this.fldSetCurrentRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSetCurrentRef.Name = "fldSetCurrentRef";
            this.fldSetCurrentRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel8
            // 
            this.toolStripLabel8.Name = "toolStripLabel8";
            this.toolStripLabel8.Size = new System.Drawing.Size(54, 23);
            this.toolStripLabel8.Text = "New Ref:";
            // 
            // fldSetNewRef
            // 
            this.fldSetNewRef.BackColor = System.Drawing.Color.LightGray;
            this.fldSetNewRef.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSetNewRef.Name = "fldSetNewRef";
            this.fldSetNewRef.Size = new System.Drawing.Size(75, 26);
            // 
            // toolStripLabel9
            // 
            this.toolStripLabel9.Name = "toolStripLabel9";
            this.toolStripLabel9.Size = new System.Drawing.Size(67, 23);
            this.toolStripLabel9.Text = "Description";
            // 
            // fldSetDescription
            // 
            this.fldSetDescription.BackColor = System.Drawing.Color.LightGray;
            this.fldSetDescription.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.fldSetDescription.Name = "fldSetDescription";
            this.fldSetDescription.Size = new System.Drawing.Size(200, 26);
            // 
            // btnSetSaveNode
            // 
            this.btnSetSaveNode.Image = ((System.Drawing.Image)(resources.GetObject("btnSetSaveNode.Image")));
            this.btnSetSaveNode.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSetSaveNode.Name = "btnSetSaveNode";
            this.btnSetSaveNode.Size = new System.Drawing.Size(51, 23);
            this.btnSetSaveNode.Text = "Save";
            this.btnSetSaveNode.Click += new System.EventHandler(this.btnSetSaveNode_Click);
            // 
            // tabPage7
            // 
            this.tabPage7.Controls.Add(this.splitContainer4);
            this.tabPage7.Location = new System.Drawing.Point(4, 22);
            this.tabPage7.Name = "tabPage7";
            this.tabPage7.Size = new System.Drawing.Size(1485, 676);
            this.tabPage7.TabIndex = 6;
            this.tabPage7.Text = "Set XMLs";
            this.tabPage7.UseVisualStyleBackColor = true;
            // 
            // splitContainer4
            // 
            this.splitContainer4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer4.Location = new System.Drawing.Point(0, 0);
            this.splitContainer4.Name = "splitContainer4";
            // 
            // splitContainer4.Panel1
            // 
            this.splitContainer4.Panel1.Controls.Add(this.groupBox5);
            // 
            // splitContainer4.Panel2
            // 
            this.splitContainer4.Panel2.Controls.Add(this.groupBox6);
            this.splitContainer4.Size = new System.Drawing.Size(1485, 676);
            this.splitContainer4.SplitterDistance = 801;
            this.splitContainer4.TabIndex = 1;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.pnlSetXML);
            this.groupBox5.Controls.Add(this.toolStrip13);
            this.groupBox5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox5.Location = new System.Drawing.Point(0, 0);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(801, 676);
            this.groupBox5.TabIndex = 1;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Base";
            // 
            // pnlSetXML
            // 
            this.pnlSetXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetXML.Location = new System.Drawing.Point(3, 41);
            this.pnlSetXML.Name = "pnlSetXML";
            this.pnlSetXML.Size = new System.Drawing.Size(795, 632);
            this.pnlSetXML.TabIndex = 28;
            // 
            // toolStrip13
            // 
            this.toolStrip13.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowBaseXMLInNotePadPlus});
            this.toolStrip13.Location = new System.Drawing.Point(3, 16);
            this.toolStrip13.Name = "toolStrip13";
            this.toolStrip13.Size = new System.Drawing.Size(795, 25);
            this.toolStrip13.TabIndex = 27;
            this.toolStrip13.Text = "toolStrip13";
            // 
            // btnShowBaseXMLInNotePadPlus
            // 
            this.btnShowBaseXMLInNotePadPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnShowBaseXMLInNotePadPlus.Image")));
            this.btnShowBaseXMLInNotePadPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowBaseXMLInNotePadPlus.Name = "btnShowBaseXMLInNotePadPlus";
            this.btnShowBaseXMLInNotePadPlus.Size = new System.Drawing.Size(134, 22);
            this.btnShowBaseXMLInNotePadPlus.Text = "Open in Notepad++";
            this.btnShowBaseXMLInNotePadPlus.Click += new System.EventHandler(this.btnShowBaseXMLInNotePadPlus_Click);
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.pnlSetWithMFXML);
            this.groupBox6.Controls.Add(this.toolStrip14);
            this.groupBox6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox6.Location = new System.Drawing.Point(0, 0);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(680, 676);
            this.groupBox6.TabIndex = 2;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "With MiniFigs";
            // 
            // pnlSetWithMFXML
            // 
            this.pnlSetWithMFXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSetWithMFXML.Location = new System.Drawing.Point(3, 41);
            this.pnlSetWithMFXML.Name = "pnlSetWithMFXML";
            this.pnlSetWithMFXML.Size = new System.Drawing.Size(674, 632);
            this.pnlSetWithMFXML.TabIndex = 29;
            // 
            // toolStrip14
            // 
            this.toolStrip14.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowWithMFXMLInNotePadPlus});
            this.toolStrip14.Location = new System.Drawing.Point(3, 16);
            this.toolStrip14.Name = "toolStrip14";
            this.toolStrip14.Size = new System.Drawing.Size(674, 25);
            this.toolStrip14.TabIndex = 28;
            this.toolStrip14.Text = "toolStrip14";
            // 
            // btnShowWithMFXMLInNotePadPlus
            // 
            this.btnShowWithMFXMLInNotePadPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnShowWithMFXMLInNotePadPlus.Image")));
            this.btnShowWithMFXMLInNotePadPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowWithMFXMLInNotePadPlus.Name = "btnShowWithMFXMLInNotePadPlus";
            this.btnShowWithMFXMLInNotePadPlus.Size = new System.Drawing.Size(134, 22);
            this.btnShowWithMFXMLInNotePadPlus.Text = "Open in Notepad++";
            this.btnShowWithMFXMLInNotePadPlus.Click += new System.EventHandler(this.btnShowWithMFXMLInNotePadPlus_Click);
            // 
            // tabPage5
            // 
            this.tabPage5.Controls.Add(this.pnlRebrickableXML);
            this.tabPage5.Controls.Add(this.toolStrip5);
            this.tabPage5.Location = new System.Drawing.Point(4, 22);
            this.tabPage5.Name = "tabPage5";
            this.tabPage5.Size = new System.Drawing.Size(1485, 676);
            this.tabPage5.TabIndex = 4;
            this.tabPage5.Text = "Rebrickable XML";
            this.tabPage5.UseVisualStyleBackColor = true;
            // 
            // pnlRebrickableXML
            // 
            this.pnlRebrickableXML.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRebrickableXML.Location = new System.Drawing.Point(0, 25);
            this.pnlRebrickableXML.Name = "pnlRebrickableXML";
            this.pnlRebrickableXML.Size = new System.Drawing.Size(1485, 651);
            this.pnlRebrickableXML.TabIndex = 30;
            // 
            // toolStrip5
            // 
            this.toolStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnShowRebrickableXMLInNotePadPlus});
            this.toolStrip5.Location = new System.Drawing.Point(0, 0);
            this.toolStrip5.Name = "toolStrip5";
            this.toolStrip5.Size = new System.Drawing.Size(1485, 25);
            this.toolStrip5.TabIndex = 29;
            this.toolStrip5.Text = "toolStrip5";
            // 
            // btnShowRebrickableXMLInNotePadPlus
            // 
            this.btnShowRebrickableXMLInNotePadPlus.Image = ((System.Drawing.Image)(resources.GetObject("btnShowRebrickableXMLInNotePadPlus.Image")));
            this.btnShowRebrickableXMLInNotePadPlus.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnShowRebrickableXMLInNotePadPlus.Name = "btnShowRebrickableXMLInNotePadPlus";
            this.btnShowRebrickableXMLInNotePadPlus.Size = new System.Drawing.Size(134, 22);
            this.btnShowRebrickableXMLInNotePadPlus.Text = "Open in Notepad++";
            this.btnShowRebrickableXMLInNotePadPlus.Click += new System.EventHandler(this.btnShowRebrickableXMLInNotePadPlus_Click);
            // 
            // tabPage6
            // 
            this.tabPage6.Controls.Add(this.pnlLDRString);
            this.tabPage6.Location = new System.Drawing.Point(4, 22);
            this.tabPage6.Name = "tabPage6";
            this.tabPage6.Size = new System.Drawing.Size(1485, 676);
            this.tabPage6.TabIndex = 5;
            this.tabPage6.Text = "LDR";
            this.tabPage6.UseVisualStyleBackColor = true;
            // 
            // pnlLDRString
            // 
            this.pnlLDRString.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLDRString.Location = new System.Drawing.Point(0, 0);
            this.pnlLDRString.Name = "pnlLDRString";
            this.pnlLDRString.Size = new System.Drawing.Size(1485, 676);
            this.pnlLDRString.TabIndex = 29;
            // 
            // tpPartList
            // 
            this.tpPartList.Controls.Add(this.splitContainer2);
            this.tpPartList.Controls.Add(this.tsPartListHeader);
            this.tpPartList.Location = new System.Drawing.Point(4, 22);
            this.tpPartList.Name = "tpPartList";
            this.tpPartList.Size = new System.Drawing.Size(1485, 676);
            this.tpPartList.TabIndex = 2;
            this.tpPartList.Text = "Part List";
            this.tpPartList.UseVisualStyleBackColor = true;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 25);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.splitContainer3);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.gpPartListWithMF);
            this.splitContainer2.Size = new System.Drawing.Size(1485, 651);
            this.splitContainer2.SplitterDistance = 720;
            this.splitContainer2.TabIndex = 105;
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.gpPartListBasic);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.gpPartListMiniFigs);
            this.splitContainer3.Size = new System.Drawing.Size(720, 651);
            this.splitContainer3.SplitterDistance = 419;
            this.splitContainer3.TabIndex = 1;
            // 
            // gpPartListBasic
            // 
            this.gpPartListBasic.Controls.Add(this.dgPartListSummary);
            this.gpPartListBasic.Controls.Add(this.toolStrip11);
            this.gpPartListBasic.Controls.Add(this.statusStrip1);
            this.gpPartListBasic.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpPartListBasic.Location = new System.Drawing.Point(0, 0);
            this.gpPartListBasic.Name = "gpPartListBasic";
            this.gpPartListBasic.Size = new System.Drawing.Size(720, 419);
            this.gpPartListBasic.TabIndex = 2;
            this.gpPartListBasic.TabStop = false;
            this.gpPartListBasic.Text = "Basic";
            // 
            // dgPartListSummary
            // 
            this.dgPartListSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgPartListSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPartListSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPartListSummary.Location = new System.Drawing.Point(3, 41);
            this.dgPartListSummary.Name = "dgPartListSummary";
            this.dgPartListSummary.Size = new System.Drawing.Size(714, 353);
            this.dgPartListSummary.TabIndex = 109;
            this.dgPartListSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPartListSummary_CellClick);
            // 
            // toolStrip11
            // 
            this.toolStrip11.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPartListBasicCopyToClipboard});
            this.toolStrip11.Location = new System.Drawing.Point(3, 16);
            this.toolStrip11.Name = "toolStrip11";
            this.toolStrip11.Size = new System.Drawing.Size(714, 25);
            this.toolStrip11.TabIndex = 108;
            this.toolStrip11.Text = "toolStrip11";
            // 
            // btnPartListBasicCopyToClipboard
            // 
            this.btnPartListBasicCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnPartListBasicCopyToClipboard.Image")));
            this.btnPartListBasicCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartListBasicCopyToClipboard.Name = "btnPartListBasicCopyToClipboard";
            this.btnPartListBasicCopyToClipboard.Size = new System.Drawing.Size(124, 22);
            this.btnPartListBasicCopyToClipboard.Text = "Copy to Clipboard";
            this.btnPartListBasicCopyToClipboard.Click += new System.EventHandler(this.btnPartListBasicCopyToClipboard_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPartListCount});
            this.statusStrip1.Location = new System.Drawing.Point(3, 394);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(714, 22);
            this.statusStrip1.TabIndex = 106;
            this.statusStrip1.Text = "statusStrip2";
            // 
            // lblPartListCount
            // 
            this.lblPartListCount.Name = "lblPartListCount";
            this.lblPartListCount.Size = new System.Drawing.Size(92, 17);
            this.lblPartListCount.Text = "lblPartListCount";
            // 
            // gpPartListMiniFigs
            // 
            this.gpPartListMiniFigs.Controls.Add(this.dgMiniFigsPartListSummary);
            this.gpPartListMiniFigs.Controls.Add(this.toolStrip4);
            this.gpPartListMiniFigs.Controls.Add(this.statusStrip4);
            this.gpPartListMiniFigs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpPartListMiniFigs.Location = new System.Drawing.Point(0, 0);
            this.gpPartListMiniFigs.Name = "gpPartListMiniFigs";
            this.gpPartListMiniFigs.Size = new System.Drawing.Size(720, 228);
            this.gpPartListMiniFigs.TabIndex = 3;
            this.gpPartListMiniFigs.TabStop = false;
            this.gpPartListMiniFigs.Text = "MiniFig(s)";
            // 
            // dgMiniFigsPartListSummary
            // 
            this.dgMiniFigsPartListSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgMiniFigsPartListSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgMiniFigsPartListSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgMiniFigsPartListSummary.Location = new System.Drawing.Point(3, 41);
            this.dgMiniFigsPartListSummary.Name = "dgMiniFigsPartListSummary";
            this.dgMiniFigsPartListSummary.Size = new System.Drawing.Size(714, 162);
            this.dgMiniFigsPartListSummary.TabIndex = 109;
            this.dgMiniFigsPartListSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgMiniFigsPartListSummary_CellClick);
            // 
            // toolStrip4
            // 
            this.toolStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPartListMFCopyToClipboard});
            this.toolStrip4.Location = new System.Drawing.Point(3, 16);
            this.toolStrip4.Name = "toolStrip4";
            this.toolStrip4.Size = new System.Drawing.Size(714, 25);
            this.toolStrip4.TabIndex = 108;
            this.toolStrip4.Text = "toolStrip4";
            // 
            // btnPartListMFCopyToClipboard
            // 
            this.btnPartListMFCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnPartListMFCopyToClipboard.Image")));
            this.btnPartListMFCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartListMFCopyToClipboard.Name = "btnPartListMFCopyToClipboard";
            this.btnPartListMFCopyToClipboard.Size = new System.Drawing.Size(124, 22);
            this.btnPartListMFCopyToClipboard.Text = "Copy to Clipboard";
            this.btnPartListMFCopyToClipboard.Click += new System.EventHandler(this.btnPartListMFCopyToClipboard_Click);
            // 
            // statusStrip4
            // 
            this.statusStrip4.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbPartlist,
            this.lblPartListStatus,
            this.lblMiniFigsPartListCount});
            this.statusStrip4.Location = new System.Drawing.Point(3, 203);
            this.statusStrip4.Name = "statusStrip4";
            this.statusStrip4.Size = new System.Drawing.Size(714, 22);
            this.statusStrip4.TabIndex = 106;
            this.statusStrip4.Text = "statusStrip2";
            // 
            // pbPartlist
            // 
            this.pbPartlist.ForeColor = System.Drawing.Color.Lime;
            this.pbPartlist.Name = "pbPartlist";
            this.pbPartlist.Size = new System.Drawing.Size(100, 16);
            // 
            // lblPartListStatus
            // 
            this.lblPartListStatus.Name = "lblPartListStatus";
            this.lblPartListStatus.Size = new System.Drawing.Size(91, 17);
            this.lblPartListStatus.Text = "lblPartListStatus";
            // 
            // lblMiniFigsPartListCount
            // 
            this.lblMiniFigsPartListCount.Name = "lblMiniFigsPartListCount";
            this.lblMiniFigsPartListCount.Size = new System.Drawing.Size(137, 17);
            this.lblMiniFigsPartListCount.Text = "lblMiniFigsPartListCount";
            // 
            // gpPartListWithMF
            // 
            this.gpPartListWithMF.Controls.Add(this.dgPartListWithMFsSummary);
            this.gpPartListWithMF.Controls.Add(this.toolStrip12);
            this.gpPartListWithMF.Controls.Add(this.statusStrip3);
            this.gpPartListWithMF.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpPartListWithMF.Location = new System.Drawing.Point(0, 0);
            this.gpPartListWithMF.Name = "gpPartListWithMF";
            this.gpPartListWithMF.Size = new System.Drawing.Size(761, 651);
            this.gpPartListWithMF.TabIndex = 1;
            this.gpPartListWithMF.TabStop = false;
            this.gpPartListWithMF.Text = "With MFs";
            // 
            // dgPartListWithMFsSummary
            // 
            this.dgPartListWithMFsSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgPartListWithMFsSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgPartListWithMFsSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgPartListWithMFsSummary.Location = new System.Drawing.Point(3, 41);
            this.dgPartListWithMFsSummary.Name = "dgPartListWithMFsSummary";
            this.dgPartListWithMFsSummary.Size = new System.Drawing.Size(755, 585);
            this.dgPartListWithMFsSummary.TabIndex = 110;
            this.dgPartListWithMFsSummary.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgPartListWithMFsSummary_CellClick);
            // 
            // toolStrip12
            // 
            this.toolStrip12.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPartListWithMFCopyToClipboard});
            this.toolStrip12.Location = new System.Drawing.Point(3, 16);
            this.toolStrip12.Name = "toolStrip12";
            this.toolStrip12.Size = new System.Drawing.Size(755, 25);
            this.toolStrip12.TabIndex = 109;
            this.toolStrip12.Text = "toolStrip12";
            // 
            // btnPartListWithMFCopyToClipboard
            // 
            this.btnPartListWithMFCopyToClipboard.Image = ((System.Drawing.Image)(resources.GetObject("btnPartListWithMFCopyToClipboard.Image")));
            this.btnPartListWithMFCopyToClipboard.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartListWithMFCopyToClipboard.Name = "btnPartListWithMFCopyToClipboard";
            this.btnPartListWithMFCopyToClipboard.Size = new System.Drawing.Size(124, 22);
            this.btnPartListWithMFCopyToClipboard.Text = "Copy to Clipboard";
            this.btnPartListWithMFCopyToClipboard.Click += new System.EventHandler(this.btnPartListWithMFCopyToClipboard_Click);
            // 
            // statusStrip3
            // 
            this.statusStrip3.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblPartListWithMFsCount});
            this.statusStrip3.Location = new System.Drawing.Point(3, 626);
            this.statusStrip3.Name = "statusStrip3";
            this.statusStrip3.Size = new System.Drawing.Size(755, 22);
            this.statusStrip3.TabIndex = 107;
            this.statusStrip3.Text = "statusStrip2";
            // 
            // lblPartListWithMFsCount
            // 
            this.lblPartListWithMFsCount.Name = "lblPartListWithMFsCount";
            this.lblPartListWithMFsCount.Size = new System.Drawing.Size(139, 17);
            this.lblPartListWithMFsCount.Text = "lblPartListWithMFsCount";
            // 
            // tsPartListHeader
            // 
            this.tsPartListHeader.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnPartListRefresh,
            this.toolStripSeparator4,
            this.btnCompareWithRebrickable});
            this.tsPartListHeader.Location = new System.Drawing.Point(0, 0);
            this.tsPartListHeader.Name = "tsPartListHeader";
            this.tsPartListHeader.Size = new System.Drawing.Size(1485, 25);
            this.tsPartListHeader.TabIndex = 102;
            this.tsPartListHeader.Text = "toolStrip10";
            // 
            // btnPartListRefresh
            // 
            this.btnPartListRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnPartListRefresh.Image")));
            this.btnPartListRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnPartListRefresh.Name = "btnPartListRefresh";
            this.btnPartListRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnPartListRefresh.Text = "Refresh";
            this.btnPartListRefresh.Click += new System.EventHandler(this.btnPartListRefresh_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
            // 
            // btnCompareWithRebrickable
            // 
            this.btnCompareWithRebrickable.Image = ((System.Drawing.Image)(resources.GetObject("btnCompareWithRebrickable.Image")));
            this.btnCompareWithRebrickable.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnCompareWithRebrickable.Name = "btnCompareWithRebrickable";
            this.btnCompareWithRebrickable.Size = new System.Drawing.Size(166, 22);
            this.btnCompareWithRebrickable.Text = "Compare with Rebrickable";
            this.btnCompareWithRebrickable.Click += new System.EventHandler(this.btnCompareWithRebrickable_Click);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.splitContainer5);
            this.tabPage2.Controls.Add(this.toolStrip17);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(1485, 676);
            this.tabPage2.TabIndex = 7;
            this.tabPage2.Text = "Unity SubModels";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // splitContainer5
            // 
            this.splitContainer5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer5.Location = new System.Drawing.Point(0, 25);
            this.splitContainer5.Name = "splitContainer5";
            // 
            // splitContainer5.Panel1
            // 
            this.splitContainer5.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer5.Panel2
            // 
            this.splitContainer5.Panel2.Controls.Add(this.splitContainer7);
            this.splitContainer5.Size = new System.Drawing.Size(1485, 651);
            this.splitContainer5.SplitterDistance = 376;
            this.splitContainer5.TabIndex = 30;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tvSetSubModels);
            this.groupBox1.Controls.Add(this.toolStrip10);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(376, 651);
            this.groupBox1.TabIndex = 3;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Set SubModels";
            // 
            // tvSetSubModels
            // 
            this.tvSetSubModels.ContextMenuStrip = this.contextMenuStrip1;
            this.tvSetSubModels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tvSetSubModels.HideSelection = false;
            this.tvSetSubModels.ImageIndex = 6;
            this.tvSetSubModels.ImageList = this.imageList1;
            this.tvSetSubModels.Location = new System.Drawing.Point(3, 41);
            this.tvSetSubModels.Name = "tvSetSubModels";
            this.tvSetSubModels.SelectedImageIndex = 0;
            this.tvSetSubModels.Size = new System.Drawing.Size(370, 607);
            this.tvSetSubModels.TabIndex = 85;
            this.tvSetSubModels.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.tvSetSubModels_AfterSelect);
            // 
            // toolStrip10
            // 
            this.toolStrip10.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton2,
            this.toolStripButton3});
            this.toolStrip10.Location = new System.Drawing.Point(3, 16);
            this.toolStrip10.Name = "toolStrip10";
            this.toolStrip10.Size = new System.Drawing.Size(370, 25);
            this.toolStrip10.TabIndex = 28;
            this.toolStrip10.Text = "toolStrip10";
            // 
            // toolStripButton2
            // 
            this.toolStripButton2.BackColor = System.Drawing.Color.Pink;
            this.toolStripButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton2.Image")));
            this.toolStripButton2.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton2.Name = "toolStripButton2";
            this.toolStripButton2.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton2.ToolTipText = "Collapse";
            // 
            // toolStripButton3
            // 
            this.toolStripButton3.BackColor = System.Drawing.Color.Pink;
            this.toolStripButton3.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton3.Image")));
            this.toolStripButton3.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton3.Name = "toolStripButton3";
            this.toolStripButton3.Size = new System.Drawing.Size(23, 22);
            this.toolStripButton3.ToolTipText = "Expand All";
            // 
            // splitContainer7
            // 
            this.splitContainer7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer7.Location = new System.Drawing.Point(0, 0);
            this.splitContainer7.Name = "splitContainer7";
            this.splitContainer7.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer7.Panel1
            // 
            this.splitContainer7.Panel1.Controls.Add(this.groupBox2);
            // 
            // splitContainer7.Panel2
            // 
            this.splitContainer7.Panel2.Controls.Add(this.gpUnitySubModelParts);
            this.splitContainer7.Size = new System.Drawing.Size(1105, 651);
            this.splitContainer7.SplitterDistance = 297;
            this.splitContainer7.TabIndex = 0;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.dgSetSubModelPartSummary);
            this.groupBox2.Controls.Add(this.statusStrip5);
            this.groupBox2.Controls.Add(this.toolStrip16);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(1105, 297);
            this.groupBox2.TabIndex = 3;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Set SubModel Parts";
            // 
            // dgSetSubModelPartSummary
            // 
            this.dgSetSubModelPartSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgSetSubModelPartSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgSetSubModelPartSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgSetSubModelPartSummary.Location = new System.Drawing.Point(3, 41);
            this.dgSetSubModelPartSummary.Name = "dgSetSubModelPartSummary";
            this.dgSetSubModelPartSummary.Size = new System.Drawing.Size(1099, 231);
            this.dgSetSubModelPartSummary.TabIndex = 78;
            // 
            // statusStrip5
            // 
            this.statusStrip5.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblSetSubModelPartCount,
            this.lblSetSubModelPartSummaryItemFilteredCount});
            this.statusStrip5.Location = new System.Drawing.Point(3, 272);
            this.statusStrip5.Name = "statusStrip5";
            this.statusStrip5.Size = new System.Drawing.Size(1099, 22);
            this.statusStrip5.TabIndex = 68;
            this.statusStrip5.Text = "statusStrip2";
            // 
            // lblSetSubModelPartCount
            // 
            this.lblSetSubModelPartCount.Name = "lblSetSubModelPartCount";
            this.lblSetSubModelPartCount.Size = new System.Drawing.Size(144, 17);
            this.lblSetSubModelPartCount.Text = "lblSetSubModelPartCount";
            // 
            // lblSetSubModelPartSummaryItemFilteredCount
            // 
            this.lblSetSubModelPartSummaryItemFilteredCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSetSubModelPartSummaryItemFilteredCount.ForeColor = System.Drawing.Color.Blue;
            this.lblSetSubModelPartSummaryItemFilteredCount.Name = "lblSetSubModelPartSummaryItemFilteredCount";
            this.lblSetSubModelPartSummaryItemFilteredCount.Size = new System.Drawing.Size(274, 17);
            this.lblSetSubModelPartSummaryItemFilteredCount.Text = "lblSetSubModelPartSummaryItemFilteredCount";
            // 
            // toolStrip16
            // 
            this.toolStrip16.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton4,
            this.toolStripSeparator25,
            this.toolStripLabel40,
            this.toolStripTextBox1,
            this.toolStripLabel41,
            this.toolStripTextBox2});
            this.toolStrip16.Location = new System.Drawing.Point(3, 16);
            this.toolStrip16.Name = "toolStrip16";
            this.toolStrip16.Size = new System.Drawing.Size(1099, 25);
            this.toolStrip16.TabIndex = 27;
            this.toolStrip16.Text = "toolStrip4";
            // 
            // toolStripButton4
            // 
            this.toolStripButton4.BackColor = System.Drawing.Color.Pink;
            this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
            this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton4.Name = "toolStripButton4";
            this.toolStripButton4.Size = new System.Drawing.Size(124, 22);
            this.toolStripButton4.Text = "Copy to Clipboard";
            // 
            // toolStripSeparator25
            // 
            this.toolStripSeparator25.Name = "toolStripSeparator25";
            this.toolStripSeparator25.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel40
            // 
            this.toolStripLabel40.Name = "toolStripLabel40";
            this.toolStripLabel40.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel40.Text = "LDraw Ref:";
            // 
            // toolStripTextBox1
            // 
            this.toolStripTextBox1.BackColor = System.Drawing.Color.Wheat;
            this.toolStripTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox1.Name = "toolStripTextBox1";
            this.toolStripTextBox1.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel41
            // 
            this.toolStripLabel41.Name = "toolStripLabel41";
            this.toolStripLabel41.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel41.Text = "LDraw Colour Name:";
            // 
            // toolStripTextBox2
            // 
            this.toolStripTextBox2.BackColor = System.Drawing.Color.Wheat;
            this.toolStripTextBox2.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox2.Name = "toolStripTextBox2";
            this.toolStripTextBox2.Size = new System.Drawing.Size(125, 25);
            // 
            // gpUnitySubModelParts
            // 
            this.gpUnitySubModelParts.Controls.Add(this.dgUnitySubModelPartSummary);
            this.gpUnitySubModelParts.Controls.Add(this.statusStrip6);
            this.gpUnitySubModelParts.Controls.Add(this.toolStrip18);
            this.gpUnitySubModelParts.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gpUnitySubModelParts.Location = new System.Drawing.Point(0, 0);
            this.gpUnitySubModelParts.Name = "gpUnitySubModelParts";
            this.gpUnitySubModelParts.Size = new System.Drawing.Size(1105, 350);
            this.gpUnitySubModelParts.TabIndex = 4;
            this.gpUnitySubModelParts.TabStop = false;
            this.gpUnitySubModelParts.Text = "Unity SubModel Parts";
            // 
            // dgUnitySubModelPartSummary
            // 
            this.dgUnitySubModelPartSummary.BackgroundColor = System.Drawing.Color.White;
            this.dgUnitySubModelPartSummary.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgUnitySubModelPartSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgUnitySubModelPartSummary.Location = new System.Drawing.Point(3, 41);
            this.dgUnitySubModelPartSummary.Name = "dgUnitySubModelPartSummary";
            this.dgUnitySubModelPartSummary.Size = new System.Drawing.Size(1099, 284);
            this.dgUnitySubModelPartSummary.TabIndex = 78;
            // 
            // statusStrip6
            // 
            this.statusStrip6.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lblUnitySubModelPartCount,
            this.lblUnitySubModelPartSummaryItemFilteredCount});
            this.statusStrip6.Location = new System.Drawing.Point(3, 325);
            this.statusStrip6.Name = "statusStrip6";
            this.statusStrip6.Size = new System.Drawing.Size(1099, 22);
            this.statusStrip6.TabIndex = 68;
            this.statusStrip6.Text = "statusStrip2";
            // 
            // lblUnitySubModelPartCount
            // 
            this.lblUnitySubModelPartCount.Name = "lblUnitySubModelPartCount";
            this.lblUnitySubModelPartCount.Size = new System.Drawing.Size(156, 17);
            this.lblUnitySubModelPartCount.Text = "lblUnitySubModelPartCount";
            // 
            // lblUnitySubModelPartSummaryItemFilteredCount
            // 
            this.lblUnitySubModelPartSummaryItemFilteredCount.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUnitySubModelPartSummaryItemFilteredCount.ForeColor = System.Drawing.Color.Blue;
            this.lblUnitySubModelPartSummaryItemFilteredCount.Name = "lblUnitySubModelPartSummaryItemFilteredCount";
            this.lblUnitySubModelPartSummaryItemFilteredCount.Size = new System.Drawing.Size(285, 17);
            this.lblUnitySubModelPartSummaryItemFilteredCount.Text = "lblUnitySubModelPartSummaryItemFilteredCount";
            // 
            // toolStrip18
            // 
            this.toolStrip18.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnSyncSubModelPositions,
            this.toolStripSeparator18,
            this.toolStripButton6,
            this.toolStripSeparator28,
            this.toolStripLabel42,
            this.toolStripTextBox3,
            this.toolStripLabel43,
            this.toolStripTextBox4});
            this.toolStrip18.Location = new System.Drawing.Point(3, 16);
            this.toolStrip18.Name = "toolStrip18";
            this.toolStrip18.Size = new System.Drawing.Size(1099, 25);
            this.toolStrip18.TabIndex = 28;
            this.toolStrip18.Text = "toolStrip4";
            // 
            // btnSyncSubModelPositions
            // 
            this.btnSyncSubModelPositions.Image = ((System.Drawing.Image)(resources.GetObject("btnSyncSubModelPositions.Image")));
            this.btnSyncSubModelPositions.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnSyncSubModelPositions.Name = "btnSyncSubModelPositions";
            this.btnSyncSubModelPositions.Size = new System.Drawing.Size(160, 22);
            this.btnSyncSubModelPositions.Text = "Sync SubModel Positions";
            this.btnSyncSubModelPositions.Click += new System.EventHandler(this.btnSyncSubModelPositions_Click);
            // 
            // toolStripSeparator18
            // 
            this.toolStripSeparator18.Name = "toolStripSeparator18";
            this.toolStripSeparator18.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripButton6
            // 
            this.toolStripButton6.BackColor = System.Drawing.Color.Pink;
            this.toolStripButton6.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton6.Image")));
            this.toolStripButton6.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripButton6.Name = "toolStripButton6";
            this.toolStripButton6.Size = new System.Drawing.Size(124, 22);
            this.toolStripButton6.Text = "Copy to Clipboard";
            // 
            // toolStripSeparator28
            // 
            this.toolStripSeparator28.Name = "toolStripSeparator28";
            this.toolStripSeparator28.Size = new System.Drawing.Size(6, 25);
            // 
            // toolStripLabel42
            // 
            this.toolStripLabel42.Name = "toolStripLabel42";
            this.toolStripLabel42.Size = new System.Drawing.Size(63, 22);
            this.toolStripLabel42.Text = "LDraw Ref:";
            // 
            // toolStripTextBox3
            // 
            this.toolStripTextBox3.BackColor = System.Drawing.Color.Wheat;
            this.toolStripTextBox3.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox3.Name = "toolStripTextBox3";
            this.toolStripTextBox3.Size = new System.Drawing.Size(100, 25);
            // 
            // toolStripLabel43
            // 
            this.toolStripLabel43.Name = "toolStripLabel43";
            this.toolStripLabel43.Size = new System.Drawing.Size(117, 22);
            this.toolStripLabel43.Text = "LDraw Colour Name:";
            // 
            // toolStripTextBox4
            // 
            this.toolStripTextBox4.BackColor = System.Drawing.Color.Wheat;
            this.toolStripTextBox4.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripTextBox4.Name = "toolStripTextBox4";
            this.toolStripTextBox4.Size = new System.Drawing.Size(125, 25);
            // 
            // toolStrip17
            // 
            this.toolStrip17.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnUnitySubModelsRefresh});
            this.toolStrip17.Location = new System.Drawing.Point(0, 0);
            this.toolStrip17.Name = "toolStrip17";
            this.toolStrip17.Size = new System.Drawing.Size(1485, 25);
            this.toolStrip17.TabIndex = 29;
            this.toolStrip17.Text = "toolStrip17";
            // 
            // btnUnitySubModelsRefresh
            // 
            this.btnUnitySubModelsRefresh.Image = ((System.Drawing.Image)(resources.GetObject("btnUnitySubModelsRefresh.Image")));
            this.btnUnitySubModelsRefresh.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.btnUnitySubModelsRefresh.Name = "btnUnitySubModelsRefresh";
            this.btnUnitySubModelsRefresh.Size = new System.Drawing.Size(66, 22);
            this.btnUnitySubModelsRefresh.Text = "Refresh";
            this.btnUnitySubModelsRefresh.Click += new System.EventHandler(this.btnUnitySubModelsRefresh_Click);
            // 
            // chkShowPages
            // 
            this.chkShowPages.AutoSize = true;
            this.chkShowPages.Location = new System.Drawing.Point(1569, 12);
            this.chkShowPages.Name = "chkShowPages";
            this.chkShowPages.Size = new System.Drawing.Size(86, 17);
            this.chkShowPages.TabIndex = 76;
            this.chkShowPages.Text = "Show Pages";
            this.chkShowPages.UseVisualStyleBackColor = false;
            this.chkShowPages.CheckedChanged += new System.EventHandler(this.chkShowPages_CheckedChanged);
            // 
            // chkShowPartcolourImages
            // 
            this.chkShowPartcolourImages.AutoSize = true;
            this.chkShowPartcolourImages.Checked = true;
            this.chkShowPartcolourImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowPartcolourImages.Location = new System.Drawing.Point(1085, 27);
            this.chkShowPartcolourImages.Name = "chkShowPartcolourImages";
            this.chkShowPartcolourImages.Size = new System.Drawing.Size(141, 17);
            this.chkShowPartcolourImages.TabIndex = 110;
            this.chkShowPartcolourImages.Text = "Show Partcolour Images";
            this.chkShowPartcolourImages.UseVisualStyleBackColor = false;
            // 
            // chkShowElementImages
            // 
            this.chkShowElementImages.AutoSize = true;
            this.chkShowElementImages.Checked = true;
            this.chkShowElementImages.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkShowElementImages.Location = new System.Drawing.Point(1232, 20);
            this.chkShowElementImages.Name = "chkShowElementImages";
            this.chkShowElementImages.Size = new System.Drawing.Size(131, 17);
            this.chkShowElementImages.TabIndex = 111;
            this.chkShowElementImages.Text = "Show Element Images";
            this.chkShowElementImages.UseVisualStyleBackColor = false;
            // 
            // chkShowFBXDetails
            // 
            this.chkShowFBXDetails.AutoSize = true;
            this.chkShowFBXDetails.Location = new System.Drawing.Point(1246, 2);
            this.chkShowFBXDetails.Name = "chkShowFBXDetails";
            this.chkShowFBXDetails.Size = new System.Drawing.Size(111, 17);
            this.chkShowFBXDetails.TabIndex = 112;
            this.chkShowFBXDetails.Text = "Show FBX Details";
            this.chkShowFBXDetails.UseVisualStyleBackColor = false;
            // 
            // InstructionViewer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1493, 749);
            this.Controls.Add(this.chkShowFBXDetails);
            this.Controls.Add(this.chkShowElementImages);
            this.Controls.Add(this.chkShowPartcolourImages);
            this.Controls.Add(this.chkShowPages);
            this.Controls.Add(this.tabControl1);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.chkShowSubParts);
            this.Controls.Add(this.toolStrip1);
            this.Name = "InstructionViewer";
            this.Text = "Generator";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.contextMenuStrip1.ResumeLayout(false);
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.gpSetStructure.ResumeLayout(false);
            this.gpSetStructure.PerformLayout();
            this.toolStrip2.ResumeLayout(false);
            this.toolStrip2.PerformLayout();
            this.gpNodeMgmt.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartSummary)).EndInit();
            this.c.ResumeLayout(false);
            this.c.PerformLayout();
            this.tsPartSummary.ResumeLayout(false);
            this.tsPartSummary.PerformLayout();
            this.gbPartDetails.ResumeLayout(false);
            this.gbPartDetails.PerformLayout();
            this.tsBasePartCollection.ResumeLayout(false);
            this.tsBasePartCollection.PerformLayout();
            this.tsPartPosDetails.ResumeLayout(false);
            this.tsPartPosDetails.PerformLayout();
            this.tsPartDetails.ResumeLayout(false);
            this.tsPartDetails.PerformLayout();
            this.gpStep.ResumeLayout(false);
            this.gpStep.PerformLayout();
            this.toolStrip8.ResumeLayout(false);
            this.toolStrip8.PerformLayout();
            this.gpSubModel.ResumeLayout(false);
            this.gpSubModel.PerformLayout();
            this.toolStrip9.ResumeLayout(false);
            this.toolStrip9.PerformLayout();
            this.gpModel.ResumeLayout(false);
            this.gpModel.PerformLayout();
            this.toolStrip7.ResumeLayout(false);
            this.toolStrip7.PerformLayout();
            this.gpSubSet.ResumeLayout(false);
            this.gpSubSet.PerformLayout();
            this.toolStrip3.ResumeLayout(false);
            this.toolStrip3.PerformLayout();
            this.gpSet.ResumeLayout(false);
            this.gpSet.PerformLayout();
            this.toolStrip6.ResumeLayout(false);
            this.toolStrip6.PerformLayout();
            this.tabPage7.ResumeLayout(false);
            this.splitContainer4.Panel1.ResumeLayout(false);
            this.splitContainer4.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer4)).EndInit();
            this.splitContainer4.ResumeLayout(false);
            this.groupBox5.ResumeLayout(false);
            this.groupBox5.PerformLayout();
            this.toolStrip13.ResumeLayout(false);
            this.toolStrip13.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.groupBox6.PerformLayout();
            this.toolStrip14.ResumeLayout(false);
            this.toolStrip14.PerformLayout();
            this.tabPage5.ResumeLayout(false);
            this.tabPage5.PerformLayout();
            this.toolStrip5.ResumeLayout(false);
            this.toolStrip5.PerformLayout();
            this.tabPage6.ResumeLayout(false);
            this.tpPartList.ResumeLayout(false);
            this.tpPartList.PerformLayout();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.gpPartListBasic.ResumeLayout(false);
            this.gpPartListBasic.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartListSummary)).EndInit();
            this.toolStrip11.ResumeLayout(false);
            this.toolStrip11.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.gpPartListMiniFigs.ResumeLayout(false);
            this.gpPartListMiniFigs.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgMiniFigsPartListSummary)).EndInit();
            this.toolStrip4.ResumeLayout(false);
            this.toolStrip4.PerformLayout();
            this.statusStrip4.ResumeLayout(false);
            this.statusStrip4.PerformLayout();
            this.gpPartListWithMF.ResumeLayout(false);
            this.gpPartListWithMF.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgPartListWithMFsSummary)).EndInit();
            this.toolStrip12.ResumeLayout(false);
            this.toolStrip12.PerformLayout();
            this.statusStrip3.ResumeLayout(false);
            this.statusStrip3.PerformLayout();
            this.tsPartListHeader.ResumeLayout(false);
            this.tsPartListHeader.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.splitContainer5.Panel1.ResumeLayout(false);
            this.splitContainer5.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer5)).EndInit();
            this.splitContainer5.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.toolStrip10.ResumeLayout(false);
            this.toolStrip10.PerformLayout();
            this.splitContainer7.Panel1.ResumeLayout(false);
            this.splitContainer7.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer7)).EndInit();
            this.splitContainer7.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgSetSubModelPartSummary)).EndInit();
            this.statusStrip5.ResumeLayout(false);
            this.statusStrip5.PerformLayout();
            this.toolStrip16.ResumeLayout(false);
            this.toolStrip16.PerformLayout();
            this.gpUnitySubModelParts.ResumeLayout(false);
            this.gpUnitySubModelParts.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgUnitySubModelPartSummary)).EndInit();
            this.statusStrip6.ResumeLayout(false);
            this.statusStrip6.PerformLayout();
            this.toolStrip18.ResumeLayout(false);
            this.toolStrip18.PerformLayout();
            this.toolStrip17.ResumeLayout(false);
            this.toolStrip17.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripLabel lblSetRef;
        private System.Windows.Forms.ToolStripTextBox fldCurrentSetRef;
        private System.Windows.Forms.ToolStripButton btnSaveSet;
        private System.Windows.Forms.ToolStripButton btnLoadSet;
        private System.Windows.Forms.ImageList imageList1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.CheckBox chkShowSubParts;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsAddStepToEnd;
        private System.Windows.Forms.ToolStripMenuItem tsAddSubModelAtEnd;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox gpSetStructure;
        private System.Windows.Forms.ToolStrip toolStrip2;
        private System.Windows.Forms.ToolStripButton btnCollapseNodes;
        private System.Windows.Forms.ToolStripButton btnExpandAll;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripButton btnMoveUp;
        private System.Windows.Forms.ToolStripButton btnMoveDown;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripButton btnSetClear;
        private System.Windows.Forms.GroupBox gpNodeMgmt;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox chkIsSubPart;
        private System.Windows.Forms.DataGridView dgPartSummary;
        private System.Windows.Forms.CheckBox chkIsLargeModel;
        private System.Windows.Forms.CheckBox chkIsSticker;
        private System.Windows.Forms.StatusStrip c;
        private System.Windows.Forms.ToolStripStatusLabel lblPartCount;
        private System.Windows.Forms.ToolStrip tsPartSummary;
        private System.Windows.Forms.GroupBox gbPartDetails;
        private System.Windows.Forms.ToolStrip tsPartDetails;
        private System.Windows.Forms.ToolStripLabel toolStripLabel1;
        private System.Windows.Forms.ToolStripTextBox fldLDrawRef;
        private System.Windows.Forms.ToolStripButton fldLDrawImage;
        private System.Windows.Forms.ToolStripLabel toolStripLabel2;
        private System.Windows.Forms.ToolStripTextBox fldLDrawColourID;
        private System.Windows.Forms.ToolStripLabel toolStripLabel4;
        private System.Windows.Forms.ToolStripComboBox fldLDrawColourName;
        private System.Windows.Forms.ToolStripLabel toolStripLabel3;
        private System.Windows.Forms.ToolStripTextBox fldPlacementMovements;
        private System.Windows.Forms.ToolStripButton btnPartClear;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripButton btnPartAdd;
        private System.Windows.Forms.ToolStripButton btnPartSave;
        private System.Windows.Forms.ToolStripButton btnPartDelete;
        private System.Windows.Forms.GroupBox gpStep;
        private System.Windows.Forms.ToolStrip toolStrip8;
        private System.Windows.Forms.ToolStripLabel toolStripLabel22;
        private System.Windows.Forms.ToolStripTextBox fldStepParentSubSetRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel23;
        private System.Windows.Forms.ToolStripTextBox fldStepParentModelRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel18;
        private System.Windows.Forms.ToolStripTextBox fldPureStepNo;
        private System.Windows.Forms.ToolStripLabel toolStripLabel19;
        private System.Windows.Forms.ToolStripTextBox fldStepLevel;
        private System.Windows.Forms.ToolStripButton btnStepDelete;
        private System.Windows.Forms.GroupBox gpSubModel;
        private System.Windows.Forms.ToolStrip toolStrip9;
        private System.Windows.Forms.ToolStripLabel toolStripLabel16;
        private System.Windows.Forms.ToolStripTextBox fldSubModelCurrentRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel17;
        private System.Windows.Forms.ToolStripTextBox fldSubModelNewRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel20;
        private System.Windows.Forms.ToolStripTextBox fldSubModelDescription;
        private System.Windows.Forms.ToolStripButton btnSubModelSave;
        private System.Windows.Forms.ToolStripButton btnSubModelDelete;
        private System.Windows.Forms.GroupBox gpModel;
        private System.Windows.Forms.ToolStrip toolStrip7;
        private System.Windows.Forms.ToolStripLabel toolStripLabel14;
        private System.Windows.Forms.ToolStripTextBox fldModelCurrentRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel10;
        private System.Windows.Forms.ToolStripTextBox fldModelNewRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel11;
        private System.Windows.Forms.ToolStripTextBox fldModelDescription;
        private System.Windows.Forms.ToolStripLabel toolStripLabel12;
        private System.Windows.Forms.ToolStripComboBox fldModelType;
        private System.Windows.Forms.ToolStripButton btnModelSave;
        private System.Windows.Forms.ToolStripButton btnModelDelete;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripButton btnAdjustModelStepNos;
        private System.Windows.Forms.GroupBox gpSubSet;
        private System.Windows.Forms.ToolStrip toolStrip3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel13;
        private System.Windows.Forms.ToolStripTextBox fldSubSetCurrentRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel5;
        private System.Windows.Forms.ToolStripTextBox fldSubSetNewRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel6;
        private System.Windows.Forms.ToolStripTextBox fldSubSetDescription;
        private System.Windows.Forms.ToolStripLabel toolStripLabel7;
        private System.Windows.Forms.ToolStripComboBox fldSubSetType;
        private System.Windows.Forms.ToolStripButton btnSubSetSave;
        private System.Windows.Forms.ToolStripButton btnSubSetDelete;
        private System.Windows.Forms.GroupBox gpSet;
        private System.Windows.Forms.ToolStrip toolStrip6;
        private System.Windows.Forms.ToolStripLabel toolStripLabel15;
        private System.Windows.Forms.ToolStripTextBox fldSetCurrentRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel8;
        private System.Windows.Forms.ToolStripTextBox fldSetNewRef;
        private System.Windows.Forms.ToolStripLabel toolStripLabel9;
        private System.Windows.Forms.ToolStripTextBox fldSetDescription;
        private System.Windows.Forms.ToolStripButton btnSetSaveNode;
        private System.Windows.Forms.TabPage tpPartList;
        private System.Windows.Forms.CheckBox chkBasePartCollection;
        private System.Windows.Forms.ToolStripButton btnAddPartToBasePartCollection;
        private System.Windows.Forms.ToolStripLabel lblQty;
        private System.Windows.Forms.ToolStripTextBox fldQty;
        private System.Windows.Forms.ToolStripLabel toolStripLabel21;
        private System.Windows.Forms.ToolStripTextBox fldStepBook;
        private System.Windows.Forms.ToolStripLabel toolStripLabel24;
        private System.Windows.Forms.ToolStripTextBox fldStepPage;
        private System.Windows.Forms.ToolStripButton btnStepSave;
        private System.Windows.Forms.CheckBox chkShowPages;
        private System.Windows.Forms.ToolStripLabel toolStripLabel25;
        private System.Windows.Forms.ToolStripTextBox fldSubModelPosX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel26;
        private System.Windows.Forms.ToolStripTextBox fldSubModelPosY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel27;
        private System.Windows.Forms.ToolStripTextBox fldSubModelPosZ;
        private System.Windows.Forms.ToolStripLabel toolStripLabel28;
        private System.Windows.Forms.ToolStripTextBox fldSubModelRotX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel29;
        private System.Windows.Forms.ToolStripTextBox fldSubModelRotY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel30;
        private System.Windows.Forms.ToolStripTextBox fldSubModelRotZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator10;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator11;
        private System.Windows.Forms.ToolStripButton btnOpenSetURLs;
        private System.Windows.Forms.ToolStripButton btnOpenSetInstructions;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.GroupBox gpPartListWithMF;
        private System.Windows.Forms.StatusStrip statusStrip3;
        private System.Windows.Forms.ToolStripStatusLabel lblPartListWithMFsCount;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator12;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator13;
        private System.Windows.Forms.ToolStripMenuItem tsShowMiniFigSet;
        private System.Windows.Forms.ToolStripMenuItem tsAdd5Steps;
        private System.Windows.Forms.ToolStripMenuItem tsInsertStepBefore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator14;
        private System.Windows.Forms.ToolStripButton btnMoveUpBy5;
        private System.Windows.Forms.ToolStripButton btnMoveDownBy5;
        private System.Windows.Forms.ToolStripMenuItem tsInsertSubModelBefore;
        private System.Windows.Forms.ToolStripTextBox fldBulkValue;
        private System.Windows.Forms.ToolStrip tsPartPosDetails;
        private System.Windows.Forms.ToolStripLabel toolStripLabel31;
        private System.Windows.Forms.ToolStripTextBox fldPartPosX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel32;
        private System.Windows.Forms.ToolStripTextBox fldPartPosY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel33;
        private System.Windows.Forms.ToolStripTextBox fldPartPosZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator15;
        private System.Windows.Forms.ToolStripLabel toolStripLabel34;
        private System.Windows.Forms.ToolStripTextBox fldPartRotX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel35;
        private System.Windows.Forms.ToolStripTextBox fldPartRotY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel36;
        private System.Windows.Forms.ToolStripTextBox fldPartRotZ;
        private System.Windows.Forms.ToolStripMenuItem tsDuplicateObjectToBefore;
        private System.Windows.Forms.ToolStripMenuItem tsDuplicateObjectToEnd;
        private System.Windows.Forms.ToolStripMenuItem tsInsertStepAfter;
        private System.Windows.Forms.ToolStripMenuItem tsDuplicateObjectToAfter;
        private System.Windows.Forms.ToolStripMenuItem tsInsertSubModelAfter;
        private System.Windows.Forms.ToolStripMenuItem tsSubModelDuplicateToAfter;
        private System.Windows.Forms.ToolStripMenuItem tsSubModelDuplicateToEnd;
        private System.Windows.Forms.ToolStripMenuItem tsSubModelDuplicateToBefore;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator16;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator17;
        private System.Windows.Forms.ToolStripLabel toolStripLabel37;
        private System.Windows.Forms.ToolStripTextBox fldModelRotX;
        private System.Windows.Forms.ToolStripLabel toolStripLabel38;
        private System.Windows.Forms.ToolStripTextBox fldModelRotY;
        private System.Windows.Forms.ToolStripLabel toolStripLabel39;
        private System.Windows.Forms.ToolStripTextBox fldModelRotZ;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator19;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripButton btnSetStructureRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator21;
        private System.Windows.Forms.ToolStrip tsBasePartCollection;
        private System.Windows.Forms.ToolStripLabel lblPartType;
        private System.Windows.Forms.ToolStripComboBox fldPartType;
        private System.Windows.Forms.ToolStripLabel lblLDrawSize;
        private System.Windows.Forms.ToolStripTextBox fldLDrawSize;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator24;
        private System.Windows.Forms.ToolStripButton btnGenerateDatFile;
        private System.Windows.Forms.ToolStripMenuItem tsAddSet;
        private System.Windows.Forms.ToolStripMenuItem tsAddSubSet;
        private System.Windows.Forms.ToolStripMenuItem tsAddModel;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator23;
        private System.Windows.Forms.ToolStripMenuItem tsRecalulatePartList;
        private System.Windows.Forms.ToolStripMenuItem tsRecalulateSubSetRefs;
        private System.Windows.Forms.ToolStripButton btnPartSummaryCopyToClipboard;
        private System.Windows.Forms.DataGridView dgPartListWithMFsSummary;
        private System.Windows.Forms.ToolStrip toolStrip12;
        private System.Windows.Forms.ToolStripButton btnPartListWithMFCopyToClipboard;
        private System.Windows.Forms.ToolStrip tsPartListHeader;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripLabel lblLDrawRefAc;
        private System.Windows.Forms.ToolStripTextBox fldLDrawRefAc;
        private System.Windows.Forms.ToolStripStatusLabel lblPartSummaryItemFilteredCount;
        private System.Windows.Forms.CheckBox chkLDrawRefAcEquals;
        private System.Windows.Forms.CheckBox chkLDrawColourNameAcEquals;
        private System.Windows.Forms.ToolStripLabel lblLDrawColourNameAc;
        private System.Windows.Forms.ToolStripTextBox fldLDrawColourNameAc;
        private System.Windows.Forms.CheckBox chkFBXMissingAc;
        private System.Windows.Forms.ToolStripMenuItem tsStepDuplicateToEnd;
        private System.Windows.Forms.ToolStripButton btnCompareWithRebrickable;
        private System.Windows.Forms.TreeView tvSetSummary;
        private System.Windows.Forms.Panel pnlSetImage;
        private System.Windows.Forms.ToolStripMenuItem tsStepDuplicateToBefore;
        private System.Windows.Forms.ToolStripMenuItem tsStepDuplicateToAfter;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator22;
        private System.Windows.Forms.CheckBox chkShowPartcolourImages;
        private System.Windows.Forms.CheckBox chkShowElementImages;
        private System.Windows.Forms.CheckBox chkShowFBXDetails;
        private System.Windows.Forms.TabPage tabPage5;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.GroupBox gpPartListBasic;
        private System.Windows.Forms.DataGridView dgPartListSummary;
        private System.Windows.Forms.ToolStrip toolStrip11;
        private System.Windows.Forms.ToolStripButton btnPartListBasicCopyToClipboard;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel lblPartListCount;
        private System.Windows.Forms.GroupBox gpPartListMiniFigs;
        private System.Windows.Forms.DataGridView dgMiniFigsPartListSummary;
        private System.Windows.Forms.ToolStrip toolStrip4;
        private System.Windows.Forms.ToolStripButton btnPartListMFCopyToClipboard;
        private System.Windows.Forms.StatusStrip statusStrip4;
        private System.Windows.Forms.ToolStripStatusLabel lblMiniFigsPartListCount;
        private System.Windows.Forms.TabPage tabPage6;
        private System.Windows.Forms.Panel pnlLDRString;
        private System.Windows.Forms.ToolStripButton btnPartListRefresh;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.TabPage tabPage7;
        private System.Windows.Forms.SplitContainer splitContainer4;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.Panel pnlSetXML;
        private System.Windows.Forms.ToolStrip toolStrip13;
        private System.Windows.Forms.ToolStripButton btnShowBaseXMLInNotePadPlus;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.Panel pnlSetWithMFXML;
        private System.Windows.Forms.ToolStrip toolStrip14;
        private System.Windows.Forms.ToolStripButton btnShowWithMFXMLInNotePadPlus;
        private System.Windows.Forms.Panel pnlRebrickableXML;
        private System.Windows.Forms.ToolStrip toolStrip5;
        private System.Windows.Forms.ToolStripButton btnShowRebrickableXMLInNotePadPlus;
        private System.Windows.Forms.ToolStripProgressBar pbPartlist;
        private System.Windows.Forms.ToolStripStatusLabel lblPartListStatus;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStrip toolStrip17;
        private System.Windows.Forms.SplitContainer splitContainer5;
        private System.Windows.Forms.SplitContainer splitContainer7;
        private System.Windows.Forms.GroupBox gpUnitySubModelParts;
        private System.Windows.Forms.DataGridView dgUnitySubModelPartSummary;
        private System.Windows.Forms.StatusStrip statusStrip6;
        private System.Windows.Forms.ToolStripStatusLabel lblUnitySubModelPartCount;
        private System.Windows.Forms.ToolStripStatusLabel lblUnitySubModelPartSummaryItemFilteredCount;
        private System.Windows.Forms.ToolStrip toolStrip18;
        private System.Windows.Forms.ToolStripButton toolStripButton6;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator28;
        private System.Windows.Forms.ToolStripLabel toolStripLabel42;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox3;
        private System.Windows.Forms.ToolStripLabel toolStripLabel43;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox4;
        private System.Windows.Forms.ToolStripButton btnUnitySubModelsRefresh;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView tvSetSubModels;
        private System.Windows.Forms.ToolStrip toolStrip10;
        private System.Windows.Forms.ToolStripButton toolStripButton2;
        private System.Windows.Forms.ToolStripButton toolStripButton3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.DataGridView dgSetSubModelPartSummary;
        private System.Windows.Forms.StatusStrip statusStrip5;
        private System.Windows.Forms.ToolStripStatusLabel lblSetSubModelPartCount;
        private System.Windows.Forms.ToolStripStatusLabel lblSetSubModelPartSummaryItemFilteredCount;
        private System.Windows.Forms.ToolStrip toolStrip16;
        private System.Windows.Forms.ToolStripButton toolStripButton4;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator25;
        private System.Windows.Forms.ToolStripLabel toolStripLabel40;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox1;
        private System.Windows.Forms.ToolStripLabel toolStripLabel41;
        private System.Windows.Forms.ToolStripTextBox toolStripTextBox2;
        private System.Windows.Forms.ToolStripButton btnSyncSubModelPositions;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator18;
    }
}

