﻿namespace Generator
{
    partial class MainScreen
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainScreen));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.btnExit = new System.Windows.Forms.ToolStripButton();
            this.btnShowGeneratorScreen = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.label1 = new System.Windows.Forms.Label();
            this.fldSourceURL = new System.Windows.Forms.TextBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.fldImageName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnShowPartTickBackScreen = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGenerateMiniFigLDrawFiles = new System.Windows.Forms.Button();
            this.fldMiniFigRef = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.btnShowSetDetailsScreen = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.fldImageType = new System.Windows.Forms.ComboBox();
            this.btnShowStaticDataScreen = new System.Windows.Forms.Button();
            this.btnDownloader = new System.Windows.Forms.Button();
            this.btnOrderScreens = new System.Windows.Forms.Button();
            this.toolStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(696, 25);
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
            // btnShowGeneratorScreen
            // 
            this.btnShowGeneratorScreen.BackColor = System.Drawing.Color.PaleGreen;
            this.btnShowGeneratorScreen.Location = new System.Drawing.Point(12, 40);
            this.btnShowGeneratorScreen.Name = "btnShowGeneratorScreen";
            this.btnShowGeneratorScreen.Size = new System.Drawing.Size(102, 57);
            this.btnShowGeneratorScreen.TabIndex = 27;
            this.btnShowGeneratorScreen.Text = "Instruction Viewer";
            this.btnShowGeneratorScreen.UseVisualStyleBackColor = false;
            this.btnShowGeneratorScreen.Click += new System.EventHandler(this.btnShowGeneratorScreen_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.Yellow;
            this.btnTest.Location = new System.Drawing.Point(120, 166);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(102, 31);
            this.btnTest.TabIndex = 29;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = false;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // statusStrip2
            // 
            this.statusStrip2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.pbStatus,
            this.lblStatus});
            this.statusStrip2.Location = new System.Drawing.Point(0, 244);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(696, 22);
            this.statusStrip2.TabIndex = 36;
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
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(252, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Source URL";
            // 
            // fldSourceURL
            // 
            this.fldSourceURL.Location = new System.Drawing.Point(324, 40);
            this.fldSourceURL.Name = "fldSourceURL";
            this.fldSourceURL.Size = new System.Drawing.Size(363, 20);
            this.fldSourceURL.TabIndex = 42;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(593, 67);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(94, 46);
            this.btnUploadImage.TabIndex = 45;
            this.btnUploadImage.Text = "Upload Image to BLOB";
            this.btnUploadImage.UseVisualStyleBackColor = false;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // fldImageName
            // 
            this.fldImageName.Location = new System.Drawing.Point(324, 96);
            this.fldImageName.Name = "fldImageName";
            this.fldImageName.Size = new System.Drawing.Size(263, 20);
            this.fldImageName.TabIndex = 44;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(248, 99);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(70, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "Image Name:";
            // 
            // btnShowPartTickBackScreen
            // 
            this.btnShowPartTickBackScreen.Location = new System.Drawing.Point(120, 103);
            this.btnShowPartTickBackScreen.Name = "btnShowPartTickBackScreen";
            this.btnShowPartTickBackScreen.Size = new System.Drawing.Size(102, 57);
            this.btnShowPartTickBackScreen.TabIndex = 57;
            this.btnShowPartTickBackScreen.Text = "Part Tick Back Screen";
            this.btnShowPartTickBackScreen.UseVisualStyleBackColor = false;
            this.btnShowPartTickBackScreen.Click += new System.EventHandler(this.btnShowPartTickBackScreen_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btnGenerateMiniFigLDrawFiles);
            this.groupBox2.Controls.Add(this.fldMiniFigRef);
            this.groupBox2.Controls.Add(this.label8);
            this.groupBox2.Location = new System.Drawing.Point(324, 122);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(363, 64);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create UnOfficial MiniFigs";
            // 
            // btnGenerateMiniFigLDrawFiles
            // 
            this.btnGenerateMiniFigLDrawFiles.Location = new System.Drawing.Point(164, 22);
            this.btnGenerateMiniFigLDrawFiles.Name = "btnGenerateMiniFigLDrawFiles";
            this.btnGenerateMiniFigLDrawFiles.Size = new System.Drawing.Size(132, 31);
            this.btnGenerateMiniFigLDrawFiles.TabIndex = 60;
            this.btnGenerateMiniFigLDrawFiles.Text = "Generate LDraw Files";
            this.btnGenerateMiniFigLDrawFiles.UseVisualStyleBackColor = false;
            this.btnGenerateMiniFigLDrawFiles.Click += new System.EventHandler(this.btnGenerateMiniFigLDrawFiles_Click);
            // 
            // fldMiniFigRef
            // 
            this.fldMiniFigRef.BackColor = System.Drawing.Color.Wheat;
            this.fldMiniFigRef.Location = new System.Drawing.Point(78, 28);
            this.fldMiniFigRef.Name = "fldMiniFigRef";
            this.fldMiniFigRef.Size = new System.Drawing.Size(80, 20);
            this.fldMiniFigRef.TabIndex = 57;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(10, 31);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(63, 13);
            this.label8.TabIndex = 56;
            this.label8.Text = "MiniFig Ref:";
            // 
            // btnShowSetDetailsScreen
            // 
            this.btnShowSetDetailsScreen.BackColor = System.Drawing.Color.PaleGreen;
            this.btnShowSetDetailsScreen.Location = new System.Drawing.Point(120, 40);
            this.btnShowSetDetailsScreen.Name = "btnShowSetDetailsScreen";
            this.btnShowSetDetailsScreen.Size = new System.Drawing.Size(102, 57);
            this.btnShowSetDetailsScreen.TabIndex = 62;
            this.btnShowSetDetailsScreen.Text = "Set Details Screen";
            this.btnShowSetDetailsScreen.UseVisualStyleBackColor = false;
            this.btnShowSetDetailsScreen.Click += new System.EventHandler(this.btnShowSetDetailsScreen_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(284, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 63;
            this.label2.Text = "Type:";
            // 
            // fldImageType
            // 
            this.fldImageType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fldImageType.FormattingEnabled = true;
            this.fldImageType.Items.AddRange(new object[] {
            "Element",
            "PartColour",
            "Set",
            "Theme"});
            this.fldImageType.Location = new System.Drawing.Point(324, 66);
            this.fldImageType.Name = "fldImageType";
            this.fldImageType.Size = new System.Drawing.Size(121, 21);
            this.fldImageType.TabIndex = 43;
            // 
            // btnShowStaticDataScreen
            // 
            this.btnShowStaticDataScreen.Location = new System.Drawing.Point(12, 103);
            this.btnShowStaticDataScreen.Name = "btnShowStaticDataScreen";
            this.btnShowStaticDataScreen.Size = new System.Drawing.Size(102, 57);
            this.btnShowStaticDataScreen.TabIndex = 64;
            this.btnShowStaticDataScreen.Text = "Static Data Screen";
            this.btnShowStaticDataScreen.UseVisualStyleBackColor = false;
            this.btnShowStaticDataScreen.Click += new System.EventHandler(this.btnShowStaticDataScreen_Click);
            // 
            // btnDownloader
            // 
            this.btnDownloader.Location = new System.Drawing.Point(13, 203);
            this.btnDownloader.Name = "btnDownloader";
            this.btnDownloader.Size = new System.Drawing.Size(210, 31);
            this.btnDownloader.TabIndex = 65;
            this.btnDownloader.Text = "Downloader";
            this.btnDownloader.UseVisualStyleBackColor = false;
            this.btnDownloader.Click += new System.EventHandler(this.btnDownloader_Click);
            // 
            // btnOrderScreens
            // 
            this.btnOrderScreens.Location = new System.Drawing.Point(13, 166);
            this.btnOrderScreens.Name = "btnOrderScreens";
            this.btnOrderScreens.Size = new System.Drawing.Size(101, 31);
            this.btnOrderScreens.TabIndex = 66;
            this.btnOrderScreens.Text = "Order Screens";
            this.btnOrderScreens.UseVisualStyleBackColor = false;
            this.btnOrderScreens.Click += new System.EventHandler(this.btnOrderScreens_Click);
            // 
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(696, 266);
            this.Controls.Add(this.btnOrderScreens);
            this.Controls.Add(this.btnDownloader);
            this.Controls.Add(this.btnShowStaticDataScreen);
            this.Controls.Add(this.fldImageType);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnShowSetDetailsScreen);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnShowPartTickBackScreen);
            this.Controls.Add(this.fldImageName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.fldSourceURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.statusStrip2);
            this.Controls.Add(this.btnTest);
            this.Controls.Add(this.btnShowGeneratorScreen);
            this.Controls.Add(this.toolStrip1);
            this.Name = "MainScreen";
            this.Text = "MainScreen";
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip2.ResumeLayout(false);
            this.statusStrip2.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.Button btnShowGeneratorScreen;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fldSourceURL;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.TextBox fldImageName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnShowPartTickBackScreen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerateMiniFigLDrawFiles;
        private System.Windows.Forms.TextBox fldMiniFigRef;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button btnShowSetDetailsScreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox fldImageType;
        private System.Windows.Forms.Button btnShowStaticDataScreen;
        private System.Windows.Forms.Button btnDownloader;
        private System.Windows.Forms.Button btnOrderScreens;
    }
}