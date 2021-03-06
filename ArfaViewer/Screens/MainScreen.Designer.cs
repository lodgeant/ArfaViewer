namespace Generator
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
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.btnShowGeneratorScreen = new System.Windows.Forms.Button();
            this.btnTest = new System.Windows.Forms.Button();
            this.statusStrip2 = new System.Windows.Forms.StatusStrip();
            this.pbStatus = new System.Windows.Forms.ToolStripProgressBar();
            this.lblStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.btnUploadToStorage = new System.Windows.Forms.Button();
            this.btnFlushStaticDataFile = new System.Windows.Forms.Button();
            this.btnUpdateLDrawStaticDataDetails = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.fldSourceURL = new System.Windows.Forms.TextBox();
            this.btnUploadImage = new System.Windows.Forms.Button();
            this.fldElementRef = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.fldLDrawRef = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.fldLDrawColourID = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnSyncFBXFiles = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.btnUploadInstructionsFromWeb = new System.Windows.Forms.Button();
            this.fldSetInstructions = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.fldInstructionsSetRef = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnShowPartTickBackScreen = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnGenerateMiniFigLDrawFiles = new System.Windows.Forms.Button();
            this.fldMiniFigRef = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.toolStrip1.SuspendLayout();
            this.statusStrip2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnExit,
            this.toolStripSeparator1});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(825, 25);
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
            // btnShowGeneratorScreen
            // 
            this.btnShowGeneratorScreen.BackColor = System.Drawing.Color.PaleGreen;
            this.btnShowGeneratorScreen.Location = new System.Drawing.Point(12, 40);
            this.btnShowGeneratorScreen.Name = "btnShowGeneratorScreen";
            this.btnShowGeneratorScreen.Size = new System.Drawing.Size(102, 57);
            this.btnShowGeneratorScreen.TabIndex = 27;
            this.btnShowGeneratorScreen.Text = "Generator Screen";
            this.btnShowGeneratorScreen.UseVisualStyleBackColor = false;
            this.btnShowGeneratorScreen.Click += new System.EventHandler(this.btnShowGeneratorScreen_Click);
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.Yellow;
            this.btnTest.Location = new System.Drawing.Point(277, 75);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(134, 31);
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
            this.statusStrip2.Location = new System.Drawing.Point(0, 295);
            this.statusStrip2.Name = "statusStrip2";
            this.statusStrip2.Size = new System.Drawing.Size(825, 22);
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
            // btnUploadToStorage
            // 
            this.btnUploadToStorage.BackColor = System.Drawing.Color.Yellow;
            this.btnUploadToStorage.Location = new System.Drawing.Point(277, 38);
            this.btnUploadToStorage.Name = "btnUploadToStorage";
            this.btnUploadToStorage.Size = new System.Drawing.Size(134, 31);
            this.btnUploadToStorage.TabIndex = 37;
            this.btnUploadToStorage.Text = "Upload to Storage";
            this.btnUploadToStorage.UseVisualStyleBackColor = false;
            this.btnUploadToStorage.Click += new System.EventHandler(this.btnUploadToStorage_Click);
            // 
            // btnFlushStaticDataFile
            // 
            this.btnFlushStaticDataFile.Location = new System.Drawing.Point(136, 38);
            this.btnFlushStaticDataFile.Name = "btnFlushStaticDataFile";
            this.btnFlushStaticDataFile.Size = new System.Drawing.Size(135, 31);
            this.btnFlushStaticDataFile.TabIndex = 39;
            this.btnFlushStaticDataFile.Text = "Flush Static Data File";
            this.btnFlushStaticDataFile.UseVisualStyleBackColor = false;
            this.btnFlushStaticDataFile.Click += new System.EventHandler(this.btnFlushStaticDataFile_Click);
            // 
            // btnUpdateLDrawStaticDataDetails
            // 
            this.btnUpdateLDrawStaticDataDetails.Location = new System.Drawing.Point(136, 75);
            this.btnUpdateLDrawStaticDataDetails.Name = "btnUpdateLDrawStaticDataDetails";
            this.btnUpdateLDrawStaticDataDetails.Size = new System.Drawing.Size(135, 46);
            this.btnUpdateLDrawStaticDataDetails.TabIndex = 40;
            this.btnUpdateLDrawStaticDataDetails.Text = "Update LDraw Static Data Details";
            this.btnUpdateLDrawStaticDataDetails.UseVisualStyleBackColor = false;
            this.btnUpdateLDrawStaticDataDetails.Click += new System.EventHandler(this.btnUpdateLDrawStaticDataDetails_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(429, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 41;
            this.label1.Text = "Source URL";
            // 
            // fldSourceURL
            // 
            this.fldSourceURL.Location = new System.Drawing.Point(501, 40);
            this.fldSourceURL.Name = "fldSourceURL";
            this.fldSourceURL.Size = new System.Drawing.Size(312, 20);
            this.fldSourceURL.TabIndex = 42;
            // 
            // btnUploadImage
            // 
            this.btnUploadImage.Location = new System.Drawing.Point(719, 66);
            this.btnUploadImage.Name = "btnUploadImage";
            this.btnUploadImage.Size = new System.Drawing.Size(94, 46);
            this.btnUploadImage.TabIndex = 43;
            this.btnUploadImage.Text = "Upload Image to BLOB";
            this.btnUploadImage.UseVisualStyleBackColor = false;
            this.btnUploadImage.Click += new System.EventHandler(this.btnUploadImage_Click);
            // 
            // fldElementRef
            // 
            this.fldElementRef.Enabled = false;
            this.fldElementRef.Location = new System.Drawing.Point(501, 92);
            this.fldElementRef.Name = "fldElementRef";
            this.fldElementRef.Size = new System.Drawing.Size(80, 20);
            this.fldElementRef.TabIndex = 45;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Enabled = false;
            this.label2.Location = new System.Drawing.Point(429, 95);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 44;
            this.label2.Text = "Element Ref:";
            // 
            // fldLDrawRef
            // 
            this.fldLDrawRef.Location = new System.Drawing.Point(501, 66);
            this.fldLDrawRef.Name = "fldLDrawRef";
            this.fldLDrawRef.Size = new System.Drawing.Size(80, 20);
            this.fldLDrawRef.TabIndex = 47;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(436, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 46;
            this.label3.Text = "LDraw Ref:";
            // 
            // fldLDrawColourID
            // 
            this.fldLDrawColourID.Location = new System.Drawing.Point(670, 66);
            this.fldLDrawColourID.Name = "fldLDrawColourID";
            this.fldLDrawColourID.Size = new System.Drawing.Size(43, 20);
            this.fldLDrawColourID.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(585, 69);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(88, 13);
            this.label4.TabIndex = 48;
            this.label4.Text = "LDraw Colour ID:";
            // 
            // btnSyncFBXFiles
            // 
            this.btnSyncFBXFiles.Location = new System.Drawing.Point(136, 127);
            this.btnSyncFBXFiles.Name = "btnSyncFBXFiles";
            this.btnSyncFBXFiles.Size = new System.Drawing.Size(135, 31);
            this.btnSyncFBXFiles.TabIndex = 50;
            this.btnSyncFBXFiles.Text = "Sync FBX Files";
            this.btnSyncFBXFiles.UseVisualStyleBackColor = false;
            this.btnSyncFBXFiles.Click += new System.EventHandler(this.btnSyncFBXFiles_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.btnUploadInstructionsFromWeb);
            this.groupBox1.Controls.Add(this.fldSetInstructions);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.fldInstructionsSetRef);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Location = new System.Drawing.Point(422, 127);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(320, 90);
            this.groupBox1.TabIndex = 56;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Upload Instructions";
            // 
            // btnUploadInstructionsFromWeb
            // 
            this.btnUploadInstructionsFromWeb.Location = new System.Drawing.Point(174, 17);
            this.btnUploadInstructionsFromWeb.Name = "btnUploadInstructionsFromWeb";
            this.btnUploadInstructionsFromWeb.Size = new System.Drawing.Size(135, 31);
            this.btnUploadInstructionsFromWeb.TabIndex = 60;
            this.btnUploadInstructionsFromWeb.Text = "Upload from Web";
            this.btnUploadInstructionsFromWeb.UseVisualStyleBackColor = false;
            this.btnUploadInstructionsFromWeb.Click += new System.EventHandler(this.btnUploadInstructionsFromWeb_Click);
            // 
            // fldSetInstructions
            // 
            this.fldSetInstructions.BackColor = System.Drawing.Color.Wheat;
            this.fldSetInstructions.Location = new System.Drawing.Point(78, 54);
            this.fldSetInstructions.Name = "fldSetInstructions";
            this.fldSetInstructions.Size = new System.Drawing.Size(231, 20);
            this.fldSetInstructions.TabIndex = 59;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(8, 57);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(64, 13);
            this.label5.TabIndex = 58;
            this.label5.Text = "Instructions:";
            // 
            // fldInstructionsSetRef
            // 
            this.fldInstructionsSetRef.BackColor = System.Drawing.Color.Wheat;
            this.fldInstructionsSetRef.Location = new System.Drawing.Point(78, 28);
            this.fldInstructionsSetRef.Name = "fldInstructionsSetRef";
            this.fldInstructionsSetRef.Size = new System.Drawing.Size(80, 20);
            this.fldInstructionsSetRef.TabIndex = 57;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(26, 31);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 56;
            this.label6.Text = "Set Ref:";
            // 
            // btnShowPartTickBackScreen
            // 
            this.btnShowPartTickBackScreen.BackColor = System.Drawing.Color.PaleGreen;
            this.btnShowPartTickBackScreen.Location = new System.Drawing.Point(12, 103);
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
            this.groupBox2.Location = new System.Drawing.Point(422, 223);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(320, 64);
            this.groupBox2.TabIndex = 61;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Create UnOfficial MiniFigs";
            // 
            // btnGenerateMiniFigLDrawFiles
            // 
            this.btnGenerateMiniFigLDrawFiles.Location = new System.Drawing.Point(174, 17);
            this.btnGenerateMiniFigLDrawFiles.Name = "btnGenerateMiniFigLDrawFiles";
            this.btnGenerateMiniFigLDrawFiles.Size = new System.Drawing.Size(135, 31);
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
            // MainScreen
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(825, 317);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btnShowPartTickBackScreen);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btnSyncFBXFiles);
            this.Controls.Add(this.fldLDrawColourID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.fldLDrawRef);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.fldElementRef);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnUploadImage);
            this.Controls.Add(this.fldSourceURL);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnUpdateLDrawStaticDataDetails);
            this.Controls.Add(this.btnFlushStaticDataFile);
            this.Controls.Add(this.btnUploadToStorage);
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
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton btnExit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.Button btnShowGeneratorScreen;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.StatusStrip statusStrip2;
        private System.Windows.Forms.ToolStripProgressBar pbStatus;
        private System.Windows.Forms.ToolStripStatusLabel lblStatus;
        private System.Windows.Forms.Button btnUploadToStorage;
        private System.Windows.Forms.Button btnFlushStaticDataFile;
        private System.Windows.Forms.Button btnUpdateLDrawStaticDataDetails;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox fldSourceURL;
        private System.Windows.Forms.Button btnUploadImage;
        private System.Windows.Forms.TextBox fldElementRef;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox fldLDrawRef;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox fldLDrawColourID;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnSyncFBXFiles;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnUploadInstructionsFromWeb;
        private System.Windows.Forms.TextBox fldSetInstructions;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox fldInstructionsSetRef;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Button btnShowPartTickBackScreen;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Button btnGenerateMiniFigLDrawFiles;
        private System.Windows.Forms.TextBox fldMiniFigRef;
        private System.Windows.Forms.Label label8;
    }
}