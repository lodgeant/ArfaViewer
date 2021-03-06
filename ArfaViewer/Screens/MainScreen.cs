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
using System.Xml;
using System.Net;
using System.IO.Compression;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using BaseClasses;
using System.Net.Http;

using System.Runtime.Serialization.Json;
using System.Xml.Linq;


namespace Generator
{
    public partial class MainScreen : Form
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public MainScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Main Screen";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                log.Info(".......................................................................APPLICATION STARTED.......................................................................");
                lblStatus.Text = "";


                RefreshStaticData();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }
        
        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        private void btnShowGeneratorScreen_Click(object sender, EventArgs e)
        {
            Generator form = new Generator();
            form.Visible = true;
        }

        //private void btnShowAStarScreen_Click(object sender, EventArgs e)
        //{
        //    AStarScreen form = new AStarScreen();
        //    form.Visible = true;
        //}

        private void btnUpdateLDrawStaticDataDetails_Click(object sender, EventArgs e)
        {
            UpdateLDrawStaticDataDetails();
        }

        private void btnUploadToStorage_Click(object sender, EventArgs e)
        {
            UploadData();
        }

        private void btnUploadImage_Click(object sender, EventArgs e)
        {
            UploadImage();
        }

        private void btnFlushStaticDataFile_Click(object sender, EventArgs e)
        {
            FlushStaticDataFile();
        }

        private void btnSyncFBXFiles_Click(object sender, EventArgs e)
        {
            Generator.SyncFBXFiles();
        }

        private void btnUploadInstructionsFromWeb_Click(object sender, EventArgs e)
        {
            UploadInstructionsFromWeb();
        }

        private void btnShowPartTickBackScreen_Click(object sender, EventArgs e)
        {
            PartTickBackScreen form = new PartTickBackScreen();
            form.Visible = true;
        }

        private void btnGenerateMiniFigLDrawFiles_Click(object sender, EventArgs e)
        {
            GenerateMiniFigLDrawFiles();
        }

        #endregion


        private async Task RefreshStaticData()
        {
            try
            {               
                await Generator.RefreshStaticData_BasePartCollection();
                await Generator.RefreshStaticData_CompositePartCollection();
                await Generator.RefreshStaticData_PartColourCollection();                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        #region ** UpdateLDrawStaticDataDetails FUNCTIONS **
        // NEED TO UPDATE THIS TO USE ASYNC & AWAIT

        private BackgroundWorker bw_UpdateLDrawStaticDataDetails;

        private void UpdateLDrawStaticDataDetails()
        {
            try
            {
                // ** Run background to process functions **
                bw_UpdateLDrawStaticDataDetails = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                bw_UpdateLDrawStaticDataDetails.DoWork += new DoWorkEventHandler(bw_UpdateLDrawStaticDataDetails_DoWork);
                bw_UpdateLDrawStaticDataDetails.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_UpdateLDrawStaticDataDetails_RunWorkerCompleted);
                bw_UpdateLDrawStaticDataDetails.ProgressChanged += new ProgressChangedEventHandler(bw_UpdateLDrawStaticDataDetails_ProgressChanged);
                bw_UpdateLDrawStaticDataDetails.RunWorkerAsync();

            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                //EnableControls_RefreshStaticData(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UpdateLDrawStaticDataDetails_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                //EnableControls_RefreshStaticData(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UpdateLDrawStaticDataDetails_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //[Obsolete("Function not used anymore", true)]
        //private void bw_UpdateLDrawStaticDataDetails_DoWork_OLD(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        #region ** DOWNLOAD BasePartCollection FROM AZURE **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading BasePartCollection.xml from Azure...");
        //        lblStatus.Text = "Downloading BasePartCollection.xml from Azure...";
        //        BlobClient BasePartCollectionBlob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
        //        byte[] fileContent = new byte[BasePartCollectionBlob.GetProperties().Value.ContentLength];
        //        using (var ms = new MemoryStream(fileContent))
        //        {
        //            BasePartCollectionBlob.DownloadTo(ms);
        //        }
        //        string xmlString = Encoding.UTF8.GetString(fileContent);
        //        //BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(xmlString);
        //        XmlDocument BasePartCollectionXML = new XmlDocument();
        //        BasePartCollectionXML.LoadXml(xmlString);
        //        #endregion

        //        bool dataUpdated = false;

        //        #region ** GET ALL PARTS WITH A BLANK DESCRIPTION **
        //        XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawDescription='']/@LDrawRef");
        //        //XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart/@LDrawRef");
        //        List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
        //                                           .Select(x => x.InnerText)
        //                                           .OrderBy(x => x).ToList();

        //        // ** Get descriptions for all parts **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
        //        List<string> foundList_LDrawDescription = new List<string>();
        //        List<string> notFoundList_LDrawDescription = new List<string>();
        //        int index = 0;
        //        int processedCount = 0;
        //        Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
        //        foreach (string LDrawRef in LDrawRefList)
        //        {
        //            string LDrawDescription = new System.Xml.Linq.XText(Generator.GetLDrawDescription(LDrawRef)).ToString();
        //            if (LDrawDescription.Equals(""))
        //            {
        //                notFoundList_LDrawDescription.Add(LDrawRef);
        //            }
        //            else
        //            {
        //                BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawDescription").InnerXml = LDrawDescription;
        //                foundList_LDrawDescription.Add(LDrawRef);
        //                dataUpdated = true;
        //            }

        //            // ** UPDATE SCREEN **
        //            bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
        //            if (index == 10)
        //            {
        //                Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
        //                index = 0;
        //            }
        //            processedCount += 1;
        //            index += 1;
        //        }
        //        #endregion

        //        #region ** GET ALL PARTS WITH "UNKNOWN" LDraw Part Type **
        //        LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawPartType='" + BasePart.LDrawPartType.UNKNOWN.ToString() + "']/@LDrawRef");
        //        LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
        //                                           .Select(x => x.InnerText)
        //                                           .OrderBy(x => x).ToList();

        //        // ** Get LDrawPartType for all parts **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s)...");
        //        List<string> foundList_LDrawPartType = new List<string>();
        //        List<string> notFoundList_LDrawPartType = new List<string>();
        //        index = 0;
        //        processedCount = 0;
        //        Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
        //        foreach (string LDrawRef in LDrawRefList)
        //        {
        //            string LDrawPartType = Generator.GetLDrawPartType(LDrawRef);
        //            if (LDrawPartType.Equals("") || LDrawPartType.Equals(BasePart.LDrawPartType.UNKNOWN.ToString()))
        //            {
        //                notFoundList_LDrawPartType.Add(LDrawRef);
        //            }
        //            else
        //            {
        //                BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawPartType").InnerXml = LDrawPartType;
        //                foundList_LDrawPartType.Add(LDrawRef);
        //                dataUpdated = true;
        //            }

        //            // ** UPDATE SCREEN **
        //            bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
        //            if (index == 10)
        //            {
        //                Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawPartType.Count.ToString("#,##0"));
        //                index = 0;
        //            }
        //            processedCount += 1;
        //            index += 1;
        //        }
        //        #endregion

        //        #region ** UPLOAD BasePartCollection TO AZURE **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading BasePartCollection.xml to Azure...");
        //        if (dataUpdated)
        //        {
        //            BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(BasePartCollectionXML.OuterXml);
        //            xmlString = bpc.SerializeToString(true);
        //            //xmlString = BasePartCollectionXML.OuterXml;
        //            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
        //            using (var ms = new MemoryStream(bytes))
        //            {
        //                BasePartCollectionBlob.Upload(ms, true);
        //            }
        //        }
        //        #endregion

        //        #region ** SHOW CONFIRMATION **
        //        string confirmation = "Completed successfully..." + Environment.NewLine;
        //        confirmation += "LDrawDescription: Found = " + foundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine;
        //        foreach (string LDrawRef in foundList_LDrawDescription)
        //        {
        //            confirmation += LDrawRef + Environment.NewLine;
        //        }
        //        confirmation += "Not Found=" + notFoundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine + Environment.NewLine;
        //        confirmation += "LDrawPartType: Found = " + foundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
        //        foreach (string LDrawRef in foundList_LDrawPartType)
        //        {
        //            confirmation += LDrawRef + Environment.NewLine;
        //        }
        //        confirmation  += "Not Found=" + notFoundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
        //        MessageBox.Show(confirmation, "Updating LDraw details...");
        //        #endregion
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        private void bw_UpdateLDrawStaticDataDetails_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                ActionUpdate("BasePartCollection");
                ActionUpdate("CompositePartCollection");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void ActionUpdate(string filename)
        {
            try
            {
                #region ** DOWNLOAD FILE FROM AZURE **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading " + filename + ".xml from Azure...");
                lblStatus.Text = "Downloading " + filename + ".xml from Azure...";
                BlobClient Blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient(filename + ".xml");
                byte[] fileContent = new byte[Blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    Blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                XmlDocument CollectionXML = new XmlDocument();
                CollectionXML.LoadXml(xmlString);
                #endregion

                bool dataUpdated = false;

                #region ** GET ALL PARTS WITH A BLANK DESCRIPTION **    
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
                List<string> foundList_LDrawDescription = new List<string>();
                List<string> notFoundList_LDrawDescription = new List<string>();
                int index = 0;
                int processedCount = 0;
                string XMLElementName = "BasePart";
                if (filename.Equals("CompositePartCollection")) XMLElementName = "CompositePart";                
                XmlNodeList LDrawRefNodeList = CollectionXML.SelectNodes("//" + XMLElementName + "[@LDrawDescription='']");
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefNodeList.Count);
                foreach (XmlNode LDrawRefNode in LDrawRefNodeList)
                {
                    string LDrawRef = LDrawRefNode.SelectSingleNode("@LDrawRef").InnerXml;
                    string LDrawDescription = new System.Xml.Linq.XText(Generator.GetLDrawDescription(LDrawRef)).ToString();
                    if (LDrawDescription.Equals(""))
                    {
                        notFoundList_LDrawDescription.Add(LDrawRef);
                    }
                    else
                    {                        
                        LDrawRefNode.SelectSingleNode("@LDrawDescription").InnerXml = LDrawDescription;
                        foundList_LDrawDescription.Add(LDrawRef);
                        dataUpdated = true;
                    }

                    // ** UPDATE SCREEN **
                    bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
                    if (index == 10)
                    {
                        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefNodeList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
                        index = 0;
                    }
                    processedCount += 1;
                    index += 1;
                }
                #endregion

                #region ** GET ALL PARTS WITH "UNKNOWN" LDraw Part Type (BasePartCollection ONLY) **
                List<string> foundList_LDrawPartType = new List<string>();
                List<string> notFoundList_LDrawPartType = new List<string>();
                if (filename.Equals("BasePartCollection"))
                {
                    LDrawRefNodeList = CollectionXML.SelectNodes("//BasePart[@LDrawPartType='" + BasePart.LDrawPartType.UNKNOWN.ToString() + "']/@LDrawRef");
                    List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
                                                       .Select(x => x.InnerText)
                                                       .OrderBy(x => x).ToList();

                    // ** Get LDrawPartType for all parts **
                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s)...");
                    
                    index = 0;
                    processedCount = 0;
                    Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
                    foreach (string LDrawRef in LDrawRefList)
                    {
                        string LDrawPartType = Generator.GetLDrawPartType(LDrawRef);
                        if (LDrawPartType.Equals("") || LDrawPartType.Equals(BasePart.LDrawPartType.UNKNOWN.ToString()))
                        {
                            notFoundList_LDrawPartType.Add(LDrawRef);
                        }
                        else
                        {
                            CollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawPartType").InnerXml = LDrawPartType;
                            foundList_LDrawPartType.Add(LDrawRef);
                            dataUpdated = true;
                        }

                        // ** UPDATE SCREEN **
                        bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
                        if (index == 10)
                        {
                            Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawPartType.Count.ToString("#,##0"));
                            index = 0;
                        }
                        processedCount += 1;
                        index += 1;
                    }
                }                
                #endregion

                #region ** UPLOAD FILE TO AZURE & UPDATE CACHE **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading " + filename + ".xml to Azure...");
                if (dataUpdated)
                {
                    if (filename.Equals("BasePartCollection"))
                    {
                        BasePartCollection pc = new BasePartCollection().DeserialiseFromXMLString(CollectionXML.OuterXml);
                        xmlString = pc.SerializeToString(true);
                        Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
                    }
                    else
                    {
                        CompositePartCollection pc = new CompositePartCollection().DeserialiseFromXMLString(CollectionXML.OuterXml);
                        xmlString = pc.SerializeToString(true);
                        Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
                    }                    
                    byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                    using (var ms = new MemoryStream(bytes))
                    {
                        Blob.Upload(ms, true);
                    }
                }
                #endregion

                #region ** SHOW CONFIRMATION **
                string confirmation = filename + " completed successfully..." + Environment.NewLine;
                confirmation += "LDrawDescription: Found = " + foundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine;
                foreach (string LDrawRef in foundList_LDrawDescription)
                {
                    confirmation += LDrawRef + Environment.NewLine;
                }
                confirmation += "Not Found=" + notFoundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine + Environment.NewLine;
                confirmation += "LDrawPartType: Found = " + foundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
                foreach (string LDrawRef in foundList_LDrawPartType)
                {
                    confirmation += LDrawRef + Environment.NewLine;
                }
                confirmation += "Not Found=" + notFoundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
                MessageBox.Show(confirmation, "Updating LDraw details...");
                #endregion

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        //[Obsolete("Function migrated to use Azure ldraw-parts storage", true)]
        //public static string GetLDrawDescription(string LDrawRef, byte[] partsOfficial, byte[] partsUnofficial)
        //{
        //    string value = "";

        //    // ** Search for file in Official ZIP **
        //    if (value.Equals("") && partsOfficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsOfficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                         where r.Name.Equals(LDrawRef + ".dat")
        //                                         select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    using (var stream = entry.Open())
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        string fileText = reader.ReadToEnd();
        //                        string[] lines = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //                        value = lines[0].Replace("0 ", "");
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // ** Search for file in UnOfficial ZIP **
        //    if (value.Equals("") && partsUnofficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsUnofficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                         where r.Name.Equals(LDrawRef + ".dat")
        //                                         select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    using (var stream = entry.Open())
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        string fileText = reader.ReadToEnd();
        //                        string[] lines = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //                        value = lines[0].Replace("0 ", "");
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return value;
        //}

        //[Obsolete("Function migrated to use Azure ldraw-parts storage", true)]
        //public static string GetLDrawPartType(string LDrawRef, byte[] partsOfficial, byte[] partsUnofficial)
        //{
        //    string value = "";

        //    // ** Search for file in Official ZIP **
        //    if (value.Equals("") && partsOfficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsOfficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                         where r.Name.Equals(LDrawRef + ".dat")
        //                                         select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    value = BasePart.LDrawPartType.OFFICIAL.ToString();
        //                }
        //            }
        //        }
        //    }

        //    // ** Search for file in UnOfficial ZIP **
        //    if (value.Equals("") && partsUnofficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsUnofficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                         where r.Name.Equals(LDrawRef + ".dat")
        //                                         select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //                }
        //            }
        //        }
        //    }

        //    return value;
        //}

        //[Obsolete("Function migrated to use Azure ldraw-parts storage", true)]
        //public static string GetLDrawFile(string LDrawRef, byte[] partsOfficial, byte[] partsUnofficial)
        //{
        //    string value = "";

        //    // ** Search for file in Official ZIP **
        //    if (value.Equals("") && partsOfficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsOfficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                         where r.Name.Equals(LDrawRef + ".dat")
        //                                         select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    using (var stream = entry.Open())
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        value = reader.ReadToEnd();
        //                        //string[] lines = fileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //                        //value = lines[0].Replace("0 ", "");
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    // ** Search for file in UnOfficial ZIP * *
        //    if (value.Equals("") && partsUnofficial != null)
        //    {
        //        using (var zippedStream = new MemoryStream(partsUnofficial))
        //        {
        //            using (var archive = new ZipArchive(zippedStream))
        //            {
        //                ZipArchiveEntry entry = (from r in archive.Entries
        //                                            where r.Name.Equals(LDrawRef + ".dat")
        //                                            select r).FirstOrDefault();
        //                if (entry != null)
        //                {
        //                    using (var stream = entry.Open())
        //                    using (var reader = new StreamReader(stream))
        //                    {
        //                        value = reader.ReadToEnd();
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    return value;
        //}

        //public static string GetLDrawDescription(string LDrawRef)
        //{
        //    string value = "";
        //    try
        //    {
        //        ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //        if (share.Exists() == false)
        //        {
        //            share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //        }
        //        if (share.Exists())
        //        {
        //            byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
        //            Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
        //            using (var ms = new MemoryStream(fileContent))
        //            {
        //                download.Content.CopyTo(ms);
        //            }
        //            string LDrawFileText = Encoding.UTF8.GetString(fileContent);
        //            string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //            value = lines[0].Replace("0 ", "");
        //        }
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return value;
        //    }
        //}

        //public static string GetLDrawPartType(string LDrawRef)
        //{
        //    string value = BasePart.LDrawPartType.UNKNOWN.ToString();
        //    try
        //    {
        //        // ** CHECK IF PART EXISTS IN OFFICIAL/UNOFFIAL LDRAW PARTS **            
        //        ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //        if (share.Exists())
        //        {
        //            value = BasePart.LDrawPartType.OFFICIAL.ToString();
        //        }
        //        share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //        if (share.Exists())
        //        {
        //            value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //        }
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return value;
        //    }
        //}

        //public static string GetLDrawFileDetails(string LDrawRef)
        //{
        //    string value = "";
        //    try
        //    {
        //        ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //        if (share.Exists() == false)
        //        {
        //            share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //        }
        //        if (share.Exists())
        //        {
        //            byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
        //            Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
        //            using (var ms = new MemoryStream(fileContent))
        //            {
        //                download.Content.CopyTo(ms);
        //            }
        //            value = Encoding.UTF8.GetString(fileContent);
        //            //string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //            //value = lines[0].Replace("0 ", "");
        //        }
        //        return value;
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //        return value;
        //    }
        //}

        private async Task UpdateLDrawStaticDataDetails_NEW()
        {
            // FUNCTION BELOW STILL NEEDS TO BE FINISHED
            try
            {
                // ** DOWNLOAD BasePartCollection FROM AZURE **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading BasePartCollection.xml from Azure...");
                lblStatus.Text = "Downloading BasePartCollection.xml from Azure...";
                BlobClient BasePartCollectionBlob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
                byte[] fileContent = new byte[BasePartCollectionBlob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    //BasePartCollectionBlob.DownloadTo(ms);
                    await BasePartCollectionBlob.DownloadToAsync(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                //BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(xmlString);
                XmlDocument BasePartCollectionXML = new XmlDocument();
                BasePartCollectionXML.LoadXml(xmlString);

                bool dataUpdated = false;







                #region ** GET ALL PARTS WITH A BLANK DESCRIPTION **
                XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawDescription='']/@LDrawRef");
                //XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart/@LDrawRef");
                List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
                                                   .Select(x => x.InnerText)
                                                   .OrderBy(x => x).ToList();

                // ** Get descriptions for all parts **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
                lblStatus.Text = "Getting LDraw Description for Part(s)...";
                List<string> foundList_LDrawDescription = new List<string>();
                List<string> notFoundList_LDrawDescription = new List<string>();
                int index = 0;
                int processedCount = 0;
                //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
                foreach (string LDrawRef in LDrawRefList)
                {
                    string LDrawDescription = new System.Xml.Linq.XText(Generator.GetLDrawDescription(LDrawRef)).ToString();
                    if (LDrawDescription.Equals(""))
                    {
                        notFoundList_LDrawDescription.Add(LDrawRef);
                    }
                    else
                    {
                        BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawDescription").InnerXml = LDrawDescription;
                        foundList_LDrawDescription.Add(LDrawRef);
                        dataUpdated = true;
                    }

                    // ** UPDATE SCREEN **
                    //bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");                    
                    if (index == 10)
                    {
                        //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
                        lblStatus.Text = "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0");
                        index = 0;
                    }
                    processedCount += 1;
                    index += 1;
                }
                #endregion








                #region ** GET ALL PARTS WITH "UNKNOWN" LDraw Part Type **
                LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawPartType='" + BasePart.LDrawPartType.UNKNOWN.ToString() + "']/@LDrawRef");
                LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
                                                   .Select(x => x.InnerText)
                                                   .OrderBy(x => x).ToList();

                // ** Get LDrawPartType for all parts **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s)...");
                List<string> foundList_LDrawPartType = new List<string>();
                List<string> notFoundList_LDrawPartType = new List<string>();
                index = 0;
                processedCount = 0;
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
                foreach (string LDrawRef in LDrawRefList)
                {
                    string LDrawPartType = Generator.GetLDrawPartType(LDrawRef);
                    if (LDrawPartType.Equals("") || LDrawPartType.Equals(BasePart.LDrawPartType.UNKNOWN.ToString()))
                    {
                        notFoundList_LDrawPartType.Add(LDrawRef);
                    }
                    else
                    {
                        BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawPartType").InnerXml = LDrawPartType;
                        foundList_LDrawPartType.Add(LDrawRef);
                        dataUpdated = true;
                    }

                    // ** UPDATE SCREEN **
                    //bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
                    if (index == 10)
                    {
                        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawPartType.Count.ToString("#,##0"));
                        index = 0;
                    }
                    processedCount += 1;
                    index += 1;
                }
                #endregion

                //await RunTaskAsync(BasePartCollectionXML);

                // ** UPLOAD BasePartCollection TO AZURE **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading BasePartCollection.xml to Azure...");
                if (dataUpdated)
                {
                    BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(BasePartCollectionXML.OuterXml);
                    xmlString = bpc.SerializeToString(true);
                    byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                    using (var ms = new MemoryStream(bytes))
                    {
                        //await BasePartCollectionBlob.UploadAsync(ms, true);
                    }
                }

                // ** Show confirmation **
                lblStatus.Text = "";
                string confirmation = "Completed successfully..." + Environment.NewLine;
                confirmation += "LDrawDescription: Found = " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found=" + notFoundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine;
                confirmation += "LDrawPartType: Found = " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found=" + notFoundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
                MessageBox.Show(confirmation);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // NEED TO FINISH THE BELOW
        //private Task<List<string>> RunTaskAsync(XmlDocument BasePartCollectionXML)
        //{
        //    //XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawDescription='']/@LDrawRef");
        //    XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart/@LDrawRef");
        //    List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
        //                                       .Select(x => x.InnerText)
        //                                       .OrderBy(x => x).ToList();

        //    // ** Get descriptions for all parts **
        //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
        //    List<string> foundList_LDrawDescription = new List<string>();
        //    List<string> notFoundList_LDrawDescription = new List<string>();
        //    int index = 0;
        //    int processedCount = 0;
        //    //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
        //    foreach (string LDrawRef in LDrawRefList)
        //    {
        //        string LDrawDescription = new System.Xml.Linq.XText(GetLDrawDescription(LDrawRef)).ToString();
        //        if (LDrawDescription.Equals(""))
        //        {
        //            notFoundList_LDrawDescription.Add(LDrawRef);
        //        }
        //        else
        //        {
        //            BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawDescription").InnerXml = LDrawDescription;
        //            foundList_LDrawDescription.Add(LDrawRef);
        //            //dataUpdated = true;
        //        }

        //        // ** UPDATE SCREEN **
        //        //bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
        //        if (index == 10)
        //        {
        //            Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
        //            index = 0;
        //        }
        //        processedCount += 1;
        //        index += 1;
        //    }

        //    //return foundList_LDrawDescription;
        //    //return new string() { "" };
        //}

        #endregion

        private void FlushStaticDataFile()
        {
            //string fileName = "BasePartCollection.xml";
            string fileName = "CompositePartCollection.xml";
            try
            {
                // ** DOWNLOAD Collection FROM AZURE **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading " + fileName + " from Azure...");
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient(fileName);
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent))
                {
                    blob.DownloadTo(ms);
                }
                string xmlString = Encoding.UTF8.GetString(fileContent);
                //BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(xmlString);
                CompositePartCollection cpc = new CompositePartCollection().DeserialiseFromXMLString(xmlString);

                // ** UPLOAD Collection TO AZURE **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading " + fileName + " to Azure...");
                //xmlString = bpc.SerializeToString(true);
                xmlString = cpc.SerializeToString(true);
                byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                using (var ms = new MemoryStream(bytes))
                {
                    blob.Upload(ms, true);
                }

                // ** Show confirmation **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(fileName + " successfully flushed...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void UploadImage()
        {
            try
            {
                // ** VALIDATIONS **
                if (fldLDrawRef.Text.Equals(""))
                {
                    throw new Exception("No LDraw Ref entered...");
                }
                if (fldLDrawColourID.Text.Equals(""))
                {
                    throw new Exception("No LDraw Colour ID entered...");
                }
                //if (fldElementRef.Text.Equals(""))
                //{
                //    throw new Exception("No Element Ref entered...");
                //}
                if (fldSourceURL.Text.Equals(""))
                {
                    throw new Exception("No Source URL entered...");
                }
                string LDrawRef = fldLDrawRef.Text;
                string LDrawColourID = fldLDrawColourID.Text;
                string elementRef = fldElementRef.Text;
                //string elementURL = "https://cdn.rebrickable.com/media/thumbs/parts/elements/" + elementRef + ".jpg/250x250p.jpg";
                string sourceURL = fldSourceURL.Text;

                // ** Download image from Rebrickable **
                byte[] imageb = new byte[0];
                try
                {
                    //imageb = new WebClient().DownloadData(elementURL);
                    imageb = new WebClient().DownloadData(sourceURL);
                }
                catch
                { }

                // ** Upload image to Azure **
                if (imageb.Length == 0)
                {
                    throw new Exception("No data found on Rebrickable for " + elementRef);
                }
                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "images-element").GetBlobClient(LDrawRef + "_" + LDrawColourID + ".png");
                using (var ms = new MemoryStream(imageb))
                {
                    blob.Upload(ms, true);
                }

                // Show confirmation **
                MessageBox.Show(LDrawRef + "_" + LDrawColourID + ".png uploaded to Azure...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async void UploadInstructionsFromWeb()
        {
            try
            {
                #region ** VALIDATIONS **
                if (fldInstructionsSetRef.Text.Equals(""))
                {
                    throw new Exception("No Set Ref entered...");
                }
                if (fldSetInstructions.Text.Equals(""))
                {
                    throw new Exception("No Instructions entered...");
                }
                string setRef = fldInstructionsSetRef.Text;
                List<string> insRefList = fldSetInstructions.Text.Split(',').ToList();

                // ** Check whether instructions are already present. If they are, confirm whether they should be downloaded again **
                ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
                if (share.Exists())
                {
                    // Make sure user wants to re-upload instructions
                    DialogResult res = MessageBox.Show("Instructions already exist for " + setRef + " - do you really want to re-upload again?", "Instruction Re-Upload Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (res == DialogResult.No)
                    {
                        return;
                    }
                }
                #endregion

                #region ** DOWNLOAD INSTRUCTIONS FROM BrickSet.com TO TEMP **
                List<string> filelist = new List<string>();
                int index = 1;
                foreach (string insRef in insRefList)
                {
                    string url = "https://www.lego.com/cdn/product-assets/product.bi.core.pdf/" + insRef + ".pdf";
                    string downloadPath = Path.Combine(Path.GetTempPath(), insRef + ".pdf");
                    using (WebClient webClient = new WebClient())
                    {
                        webClient.DownloadProgressChanged += (s, e1) =>
                        {
                            pbStatus.Value = e1.ProgressPercentage;
                            lblStatus.Text = "Downloading " + insRef + " from Brickset.com (" + index + " of " + insRefList.Count + ") | Downloaded " + e1.ProgressPercentage + "%";
                        };
                        webClient.DownloadFileCompleted += (s, e1) =>
                        {
                            pbStatus.Value = 0;
                            lblStatus.Text = "";
                        };
                        Task downloadTask = webClient.DownloadFileTaskAsync(new Uri(url), downloadPath);
                        await downloadTask;
                    }
                    filelist.Add(downloadPath);
                    index += 1;
                }
                #endregion

                #region ** COMBINE ALL PDFs INTO A SINGLE ONE **
                lblStatus.Text = "Merging PDFs...";
                string targetPdf = Path.Combine(Path.GetTempPath(), setRef + ".pdf");
                using (FileStream stream = new FileStream(targetPdf, FileMode.Create))
                {
                    using (iTextSharp.text.Document document = new iTextSharp.text.Document())
                    {
                        PdfCopy pdf = new PdfCopy(document, stream);
                        document.Open();
                        document.NewPage();
                        foreach (string file in filelist)
                        {
                            using (PdfReader reader = new PdfReader(file))
                            {
                                pdf.AddDocument(reader);
                            }
                        }
                    }
                }
                foreach (string file in filelist)
                {
                    File.Delete(file);
                }
                #endregion

                // ** Upload data to Azure BLOB **               
                //lblStatus.Text = "Uploading " + setRef + " to Azure BLOB...";               
                //CloudBlockBlob blockBlob = blobClient.GetContainerReference("files-instructions").GetBlockBlobReference(setRef + ".pdf");
                //Task uploadTask = blockBlob.UploadFromFileAsync(targetPdf, FileMode.Open);
                //await uploadTask;

                // ** Upload data to Azure FS - USES DIRECT LINK TO FOLDER **  
                //lblStatus.Text = "Uploading " + setRef + " to Azure FS...";
                //string FSPath = Path.Combine(@"\\lodgeaccount.file.core.windows.net\lodgeant-fs\files-instructions", setRef + ".pdf");                
                //using (Stream source = File.Open(targetPdf, FileMode.Open))
                //{
                //    using (Stream destination = File.Create(FSPath))
                //    {
                //        await source.CopyToAsync(destination);
                //    }
                //}

                #region ** UPLOAD DATA TO AZURE FS **  
                lblStatus.Text = "Uploading " + setRef + " to Azure FS...";
                share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
                const int AzureUploadLimit = 4194304;
                //byte[] bytes = File.ReadAllBytes(targetPdf);
                using (var stream = new MemoryStream(File.ReadAllBytes(targetPdf)))
                //using (FileStream stream = File.OpenRead(targetPdf))
                {
                    share.Create(stream.Length);
                    pbStatus.Maximum = (int)stream.Length;
                    long uploadIndex = 0;

                    var progressHandler = new Progress<long>();
                    progressHandler.ProgressChanged += (s, e1) =>
                    {
                        int uploadedValue = (int)uploadIndex + (int)e1;
                        double pc = (uploadedValue / (double)(stream.Length)) * 100;
                        pbStatus.Value = uploadedValue;
                        lblStatus.Text = "Uploading " + setRef + " to Azure FS | Uploaded " + pc.ToString("#,##0") + "%";
                    };

                    if (stream.Length <= AzureUploadLimit)
                    {
                        await share.UploadRangeAsync(new Azure.HttpRange(0, stream.Length), stream, progressHandler: progressHandler);
                    }
                    else
                    {
                        int bytesRead;
                        byte[] buffer = new byte[AzureUploadLimit];
                        while ((bytesRead = stream.Read(buffer, 0, buffer.Length)) > 0)
                        {
                            MemoryStream ms = new MemoryStream(buffer, 0, bytesRead);
                            await share.UploadRangeAsync(new Azure.HttpRange(uploadIndex, ms.Length), ms, progressHandler: progressHandler);
                            uploadIndex += ms.Length;
                        }
                    }
                }
                lblStatus.Text = "";
                pbStatus.Value = 0;
                #endregion

                // ** Delete TEMP file **
                File.Delete(targetPdf);

                // ** CLEAR FIELDS **
                lblStatus.Text = "";
                fldInstructionsSetRef.Text = "";
                fldSetInstructions.Text = "";

                // ** SHOW CONFIRMATION **                
                MessageBox.Show(setRef + " uploaded to Azure...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void GenerateMiniFigLDrawFiles()
        {
            try
            {
                // ** VALIDATION **
                if (fldMiniFigRef.Text.Equals(""))
                {
                    throw new Exception("No MiniFig Ref entered...");
                }
                string MiniFigRef = fldMiniFigRef.Text;

                #region ** CREATE Legs File & UPLOAD TO AZURE **
                string datText = "";
                datText += "0 Minifig Hips and Legs (" + MiniFigRef + ")" + Environment.NewLine;
                datText += "0 Name: " + MiniFigRef + "Legs.dat" + Environment.NewLine;
                datText += "0 Author: Antony Lodge" + Environment.NewLine;
                datText += "0 !LDRAW_ORG Shortcut UPDATE 2016-01" + Environment.NewLine;
                datText += "0 !LICENSE Redistributable under CCAL version 2.0 : see CAreadme.txt" + Environment.NewLine;
                datText += Environment.NewLine;
                datText += "0 BFC CERTIFY CCW" + Environment.NewLine;
                datText += Environment.NewLine;
                datText += "1 15 0 0 0 1 0 0 0 1 0 0 0 1 3815.dat" + Environment.NewLine;
                datText += "1 15 0 12 0 1 0 0 0 1 0 0 0 1 3816.dat" + Environment.NewLine;
                datText += "1 15 0 12 0 1 0 0 0 1 0 0 0 1 3817.dat" + Environment.NewLine;
                byte[] bytes = Encoding.UTF8.GetBytes(datText);
                ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(MiniFigRef + "Legs.dat");
                share.Create(bytes.Length);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    share.Upload(ms);
                }
                #endregion

                #region ** CREATE TORSO File & UPLOAD TO AZURE **
                datText = "";
                datText += "0 Minifig Torso (" + MiniFigRef + ")" + Environment.NewLine;
                datText += "0 Name: " + MiniFigRef + "Torso.dat" + Environment.NewLine;
                datText += "0 Author: Antony Lodge" + Environment.NewLine;
                datText += "0 !LDRAW_ORG Shortcut UPDATE 2016-01" + Environment.NewLine;
                datText += "0 !LICENSE Redistributable under CCAL version 2.0 : see CAreadme.txt" + Environment.NewLine;
                datText += Environment.NewLine;
                datText += "0 BFC CERTIFY CCW" + Environment.NewLine;
                datText += Environment.NewLine;
                datText += "1 15 0 0 0 1 0 0 0 1 0 0 0 1 973.dat" + Environment.NewLine;
                datText += "1 15 -15.552 9 0 0.985 -0.17 0 0.17 0.985 0 0 0 1 3818.dat" + Environment.NewLine;
                datText += "1 15 15.552 9 0 0.985 0.17 0 -0.17 0.985 0 0 0 1 3819.dat" + Environment.NewLine;
                datText += "1 14 -23.6904 26.774 -9.8982 0.985 -0.1202 0.1202 0.17 0.6964 -0.6964 0 0.707 0.707 3820.dat" + Environment.NewLine;
                datText += "1 14 23.6904 26.774 -9.8982 0.985 0.1202 -0.1202 -0.17 0.6964 -0.6964 0 0.707 0.707 3820.dat" + Environment.NewLine;
                bytes = Encoding.UTF8.GetBytes(datText);
                share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(MiniFigRef + "Torso.dat");
                share.Create(bytes.Length);
                using (MemoryStream ms = new MemoryStream(bytes))
                {
                    share.Upload(ms);
                }
                #endregion

                // ** SHOW CONFIRMATION **
                string confString = "LDraw parts successfully created for:" + Environment.NewLine;
                confString += MiniFigRef + "Legs" + Environment.NewLine;
                confString += MiniFigRef + "Torso" + Environment.NewLine;
                MessageBox.Show(confString);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        // ######################################################################################################################################################

       

        // NEED TO UPDATE THE BELOW TO USE ASYNC & AWAIT

        #region ** UPLOAD DATA FUNCTIONS **

        private BackgroundWorker bw_UploadData;

        private void UploadData()
        {
            try
            {                
                // ** Run background to process functions **
                bw_UploadData = new BackgroundWorker
                {
                    WorkerReportsProgress = true,
                    WorkerSupportsCancellation = true
                };
                //bw_UploadData.DoWork += new DoWorkEventHandler(bw_UploadData_DoWork);
                bw_UploadData.DoWork += new DoWorkEventHandler(bw_UploadData_DoWork1);
                bw_UploadData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_UploadData_RunWorkerCompleted);
                bw_UploadData.ProgressChanged += new ProgressChangedEventHandler(bw_UploadData_ProgressChanged);
                bw_UploadData.RunWorkerAsync();
               
            }
            catch (Exception ex)
            {
                //Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
                //btnRefreshStaticData.ForeColor = Color.Black;
                pbStatus.Value = 0;
                //EnableControls_RefreshStaticData(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UploadData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                //Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
                //EnableControls_RefreshStaticData(true);
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UploadData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                Delegates.ToolStripProgressBar_SetValue(this, pbStatus, e.ProgressPercentage);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UploadData_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {                
                // ** PROCESS FILES IN ELEMENT DIRECTORY **
                string filePath = @"D:\LEGO STUFF - Documents\IMAGES\BASEPART";
                List<string> PathList = Directory.GetFiles(filePath).ToList();
                Delegates.ToolStripProgressBar_SetMax(this, pbStatus, PathList.Count);
                int index = 0;
                int uploadedFileCount = 0;
                foreach (string path in PathList)
                {
                    if (uploadedFileCount == 100) break;

                    // ** Upload BLOB **
                    string filename = Path.GetFileNameWithoutExtension(path) + ".png";
                    BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "images-ldraw").GetBlobClient("official/" + filename);
                    using (var stream = File.OpenRead(path))
                    {                        
                        blob.Upload(stream, true);
                        //await blob.UploadAsync(ms, true);                        
                    }

                    // ** UPDATE SCREEN **
                    bw_UploadData.ReportProgress(uploadedFileCount, "Working...");
                    if (index == 5)
                    {
                        Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading file(s) to Azure | Processed " + filename + " (" + uploadedFileCount.ToString("#,##0") + " of " + PathList.Count.ToString("#,##0") + ")");
                        index = 0;
                    }
                    index += 1;
                    uploadedFileCount += 1;
                }
                //int blobCount = container.ListBlobs().ToList().Count; 
                MessageBox.Show("Processed " + PathList.Count.ToString("#,##0") + " file(s) successfully...");               
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void bw_UploadData_DoWork1(object sender, DoWorkEventArgs e)
        {
            try
            {               
                string LDrawType = "official";
                //string LDrawType = "unofficial";
                byte[] partBytes = File.ReadAllBytes(@"D:\LEGO STUFF - Documents\" + LDrawType + ".zip");
                using (var zippedStream = new MemoryStream(partBytes))
                {
                    using (var archive = new ZipArchive(zippedStream))
                    {
                        // ** Get list of all part files **
                        List<ZipArchiveEntry> entryList =   (from r in archive.Entries
                                                            where r.FullName.StartsWith("ldraw/parts")
                                                            && (r.FullName.StartsWith("ldraw/parts/s") == false && r.FullName.StartsWith("ldraw/parts/textures") == false)
                                                            //where r.FullName.StartsWith("parts")
                                                            //&& (r.FullName.StartsWith("parts/s") == false && r.FullName.StartsWith("parts/textures") == false)
                                                            select r).ToList();
                        Delegates.ToolStripProgressBar_SetMax(this, pbStatus, entryList.Count);
                        int index = 0;
                        int uploadedFileCount = 0;
                        foreach (ZipArchiveEntry entry in entryList)
                        {
                            //if (uploadedFileCount == 100) break;

                            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "ldraw-parts").GetBlobClient(LDrawType + "/" + entry.Name);
                            using (var stream = entry.Open())
                            using (var reader = new StreamReader(stream))
                            {
                                string fileText = reader.ReadToEnd();                               
                                byte[] bytes = Encoding.UTF8.GetBytes(fileText);
                                using (var ms = new MemoryStream(bytes))
                                {
                                    // ** Upload BLOB **                                                               
                                    blob.Upload(stream, true);
                                    //await blob.UploadAsync(ms, true);  
                                }
                            }

                            // ** UPDATE SCREEN **
                            bw_UploadData.ReportProgress(uploadedFileCount, "Working...");
                            if (index == 5)
                            {
                                Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading file(s) to Azure | Processed " + entry.Name + " (" + uploadedFileCount.ToString("#,##0") + " of " + entryList.Count.ToString("#,##0") + ")");
                                index = 0;
                            }
                            index += 1;
                            uploadedFileCount += 1;
                        }
                        //int blobCount = container.GetDirectoryReference(LDrawType).ListBlobs().ToList().Count;
                        MessageBox.Show("Processed " + entryList.Count.ToString("#,##0") + " file(s) successfully...");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        #endregion

        private void UploadBlob()
        {
            //string fileName = "BasePartCollection.xml";
            string fileName = "CompositePartCollection.xml";
            try
            {
                // ** UPLOAD Collection TO AZURE **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading " + fileName + " to Azure...");
                ////xmlString = bpc.SerializeToString(true);
                //xmlString = cpc.SerializeToString(true);
                //byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                //using (var ms = new MemoryStream(bytes))
                //{
                //    blob.Upload(ms, true);
                //    //await blob.UploadAsync(ms, true);
                //}

                //var file = new FileInfo(fileToUploadPath);
                //uploadFileSize = file.Length; //Get the file size. This is need to calculate the file upload progress

                ////Initialize a progress handler. When the file is being uploaded, the current uploaded bytes will be published back to us using this progress handler by the Blob Storage Service
                //var progressHandler = new Progress();
                //progressHandler.ProgressChanged += UploadProgressChanged;

                //var blob = new BlobClient(connectionString, containerName, file.Name); //Initialize the blob client
                //blob.Upload(fileToUploadPath, progressHandler: progressHandler); //Make sure to pass the progress handler here

                // ** Show confirmation **
                Delegates.ToolStripLabel_SetText(this, lblStatus, "");
                MessageBox.Show(fileName + " successfully flushed...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }




        private void btnTest_Click(object sender, EventArgs e)
        {
            //GetRebrickableData();
            GetRebrickablePrice();
        }


        private async void GetRebrickableData()
        {
            try
            {
                

                string url = "https://rebrickable.com/api/v3/lego/sets/41621-1/parts/";
                string contents = "";
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                    {
                        request.Headers.TryAddWithoutValidation("Accept", "application/json");
                        request.Headers.TryAddWithoutValidation("Authorization", "key " + Global_Variables.RebrickableKey);
                        var response = await httpClient.SendAsync(request);
                        if (response.StatusCode == HttpStatusCode.OK)
                        {
                            contents = await response.Content.ReadAsStringAsync();
                        }
                    }
                }

                // ** Load JSON string to XML **
                var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(contents), new XmlDictionaryReaderQuotas()));
                string XMLString = xml.ToString();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(XMLString);


                XmlNodeList partItemList = doc.SelectNodes("//item[@type='object']");


                string test = "";

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void GetRebrickablePrice()
        {
            try
            {

                //string url = @"https://rebrickable.com/parts/3001/";
                string url = @"https://www.brickowl.com/catalog/lego-brick-2-x-4-3001-15589";


                // ** Download element image from source API **
                string result = "";
                using (HttpClient client = new HttpClient())
                {
                    using (HttpResponseMessage response = client.GetAsync(url).Result)
                    {
                        using (HttpContent content = response.Content)
                        {
                            result = content.ReadAsStringAsync().Result;
                        }
                    }
                }


                byte[] pageByte = new byte[0];
                try
                {
                    pageByte = new WebClient().DownloadData(url);
                }
                catch
                { }
                if (pageByte.Length > 0)
                {
                    string pagetext = System.Text.Encoding.UTF8.GetString(pageByte);
                    string[] lines = pagetext.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);


                    //      <td><a href="/parts/3001/brick-2-x-4/69/">Light Purple</a></td>

                    string startsWithString = "<td><a href=" + @"""" + "/parts/3001/";
                    string containsString = ">Light Purple<";
                    string endsWithString = "</a></td>";

                    int index = 0;
                    foreach (string line in lines)
                    {
                        if(line.StartsWith(startsWithString) && line.Contains(containsString) && line.EndsWith(endsWithString))
                        {
                            break;
                        }
                        index += 1;
                    }

                    string correctLine = lines[index + 6];

                    string test = "";


                    
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
