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
using Newtonsoft.Json;
using System.Runtime.InteropServices;



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
                lblStatus.Text = "";
                log.Info(".......................................................................APPLICATION STARTED.......................................................................");

                // ** Get PartColour collection static data from API **
                Global_Variables.PartColourCollection = StaticData.GetPartColourData_All();

                // ** Copy BaseClasses.dll to Unity directory **
                List<string> TargetPathList = new List<string>();
                TargetPathList.Add(@"C:\Unity Projects\Lego Unity Viewer\Assets\Scripts\BaseClasses.dll");
                TargetPathList.Add(@"C:\Unity Projects\Settlers\Assets\Scripts\BaseClasses.dll");
                try
                {
                    string SourcePath = @"C:\Source Code\CS\Arfa Viewer\BaseClasses\bin\Debug\BaseClasses.dll";                   
                    foreach(string targetPath in TargetPathList) File.Copy(SourcePath, targetPath, true);                   
                }
                catch (Exception) { }


                


               





                //LDrawDetails ldd = StaticData.GetLDrawDetails_FromLDrawFile("3001");
                //LDrawDetails ldd = StaticData.GetLDrawDetails_FromLDrawFile("122c01");
                //LDrawDetails ldd = StaticData.GetLDrawDetails_FromLDrawFile("54701p01c01");
                //List<string> subPartList = LDrawDetails.GetSubPartLDrawRefsFromLDrawFileText(ldd.data);

                //string itemRef = "3001|0";
                //string container = "images-element";
                //string blobName = itemRef + ".png";
                //Bitmap image = null;
                //byte[] data = StaticData.DownloadDataFromBLOB(container, blobName);
                //using (var ms = new MemoryStream(data))
                //{
                //    image = new Bitmap(ms);
                //}



                //string Container = "images-element";
                //string BlobName = "3001|0.png";
                //ImageObject io = StaticData.GetImageObject_UsingContainerAndBlobName(Container, BlobName);
                //Bitmap image = null;
                //using (var ms = new MemoryStream(io.Data)) image = new Bitmap(ms);

                //SettlerTest();

                //AsyncTest();



                string test = "";


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
            InstructionViewer form = new InstructionViewer("");
            form.Visible = true;
        }

        private void btnShowSetDetailsScreen_Click(object sender, EventArgs e)
        {
            SetDetailsScreen form = new SetDetailsScreen();
            form.Visible = true;
        }

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

        private void btnShowPartTickBackScreen_Click(object sender, EventArgs e)
        {
            PartTickBackScreen form = new PartTickBackScreen();
            form.Visible = true;
        }

        private void btnGenerateMiniFigLDrawFiles_Click(object sender, EventArgs e)
        {
            GenerateMiniFigLDrawFiles();
        }

        private void btnShowStaticDataScreen_Click(object sender, EventArgs e)
        {
            StaticDataScreen form = new StaticDataScreen();
            form.Visible = true;
        }

        #endregion

        private void UploadImage()
        {
            try
            {
                // ** VALIDATIONS **
                if (fldSourceURL.Text.Equals("")) throw new Exception("No Source URL entered...");
                if (fldImageType.Text.Equals("")) throw new Exception("No Type selected...");
                if (fldImageName.Text.Equals("")) throw new Exception("No Image Name entered...");

                // ** Request data upload via API **
                StaticData.UploadImageToBLOB_UsingURL(fldSourceURL.Text, fldImageType.Text, fldImageName.Text);
                //string response = StaticData.UploadImageToBLOB(fldSourceURL.Text, fldImageType.Text, fldImageName.Text);
                //if (response != "") throw new Exception("Error ocurred while uploading: " + response);

                // Show confirmation **
                MessageBox.Show(fldImageName.Text + " uploaded to Azure...");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



        private void SettlerTest()
        {
            try
            {

                Woodcutter wc = new Woodcutter();
                wc.ObjectRef = "WOODCUTTER.1";
                wc.ObjectType = "SETTLER";
                wc.ObjectSubType = "WOODCUTTER";
                wc.RelatedBuildingRef = "WOODCUTTER_HUT.1";

                wc.TargetPosition = new Vect(0, 0, 0);

                string xmlString_wc = wc.SerializeToString(false);


                //Tree t1 = new Tree();
                //t1.ObjectRef = "TREE.1";
                //t1.ObjectType = "FLORA";
                //t1.ObjectSubType = "TREE";
                //t1.TreeSize = "LARGE";
                //t1.TreeType = "T1";
                //t1.State = "IDLE";
                //t1.LastTransitionTime = 0f;
                //t1.TransitionDelay = 10f;
                //t1.FallRate = 0f;

                GameMap gm = new GameMap();
                gm.SettlerObjectList.Items.Add(wc);

                //zzz_MapTemplate mt = new zzz_MapTemplate();
                //mt.Name = "Tree_Test";
                //mt.Description = "Test of new code using single tree";
                //mt.ObjList.Add(wc);
                //mt.ObjList.Add(t1);


                string xmlString = gm.SerializeToString(false);

                

                //MapTemplate mt2 = new MapTemplate().DeserialiseFromXMLString(xmlString);


                string test = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private async Task AsyncTest()
        {
            try
            {
                Task<int> downloading = DownloadDocsMainPageAsync();
                Console.WriteLine($"{nameof(AsyncTest)}: Launched downloading.");

                int bytesLoaded = await downloading;
                Console.WriteLine($"{nameof(AsyncTest)}: Downloaded {bytesLoaded} bytes.");



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }


        private static async Task<int> DownloadDocsMainPageAsync()
        {
            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: About to start downloading.");

            var client = new HttpClient();
            byte[] content = await client.GetByteArrayAsync("https://docs.microsoft.com/en-us/");

            System.Threading.Thread.Sleep(5000);


            Console.WriteLine($"{nameof(DownloadDocsMainPageAsync)}: Finished downloading.");
            return content.Length;
        }



        //private void APITest()
        //{
        //    try
        //    {

        //        string url = "https://arfabrickviewer.azurewebsites.net/source/GetPartColourData_All";
        //        string JSONString = "";
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
        //            {
        //                request.Headers.TryAddWithoutValidation("Accept", "application/json");
        //                var task = Task.Run(() => httpClient.SendAsync(request));
        //                task.Wait();
        //                var response = task.Result;
        //                if (response.StatusCode == HttpStatusCode.OK)
        //                {
        //                    JSONString = response.Content.ReadAsStringAsync().Result;
        //                }
        //            }
        //        }
        //        PartColourCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<PartColourCollection>(JSONString);

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}



        // ######################################################################################################################################################

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
                //#region ** DOWNLOAD FILE FROM AZURE **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading " + filename + ".xml from Azure...");
                //lblStatus.Text = "Downloading " + filename + ".xml from Azure...";
                //BlobClient Blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient(filename + ".xml");
                //byte[] fileContent = new byte[Blob.GetProperties().Value.ContentLength];
                //using (var ms = new MemoryStream(fileContent))
                //{
                //    Blob.DownloadTo(ms);
                //}
                //string xmlString = Encoding.UTF8.GetString(fileContent);
                //XmlDocument CollectionXML = new XmlDocument();
                //CollectionXML.LoadXml(xmlString);
                //#endregion

                //bool dataUpdated = false;

                //#region ** GET ALL PARTS WITH A BLANK DESCRIPTION **    
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
                //List<string> foundList_LDrawDescription = new List<string>();
                //List<string> notFoundList_LDrawDescription = new List<string>();
                //int index = 0;
                //int processedCount = 0;
                //string XMLElementName = "BasePart";
                //if (filename.Equals("CompositePartCollection")) XMLElementName = "CompositePart";                
                //XmlNodeList LDrawRefNodeList = CollectionXML.SelectNodes("//" + XMLElementName + "[@LDrawDescription='']");
                //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefNodeList.Count);
                //foreach (XmlNode LDrawRefNode in LDrawRefNodeList)
                //{
                //    string LDrawRef = LDrawRefNode.SelectSingleNode("@LDrawRef").InnerXml;
                //    //string LDrawDescription = new System.Xml.Linq.XText(StaticData.GetLDrawDescription_FromLDrawFile(LDrawRef)).ToString();
                //    //if (LDrawDescription.Equals(""))
                //    //{
                //    //    notFoundList_LDrawDescription.Add(LDrawRef);
                //    //}
                //    //else
                //    //{                        
                //    //    LDrawRefNode.SelectSingleNode("@LDrawDescription").InnerXml = LDrawDescription;
                //    //    foundList_LDrawDescription.Add(LDrawRef);
                //    //    dataUpdated = true;
                //    //}

                //    // ** UPDATE SCREEN **
                //    bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
                //    if (index == 10)
                //    {
                //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefNodeList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
                //        index = 0;
                //    }
                //    processedCount += 1;
                //    index += 1;
                //}
                //#endregion

                //#region ** GET ALL PARTS WITH "UNKNOWN" LDraw Part Type (BasePartCollection ONLY) **
                //List<string> foundList_LDrawPartType = new List<string>();
                //List<string> notFoundList_LDrawPartType = new List<string>();
                //if (filename.Equals("BasePartCollection"))
                //{
                //    LDrawRefNodeList = CollectionXML.SelectNodes("//BasePart[@LDrawPartType='" + BasePart.LDrawPartType.UNKNOWN.ToString() + "']/@LDrawRef");
                //    List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
                //                                       .Select(x => x.InnerText)
                //                                       .OrderBy(x => x).ToList();

                //    // ** Get LDrawPartType for all parts **
                //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s)...");
                    
                //    index = 0;
                //    processedCount = 0;
                //    Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
                //    foreach (string LDrawRef in LDrawRefList)
                //    {
                //        //string LDrawPartType = StaticData.GetLDrawPartType_FromLDrawFile(LDrawRef);
                //        //if (LDrawPartType.Equals("") || LDrawPartType.Equals(BasePart.LDrawPartType.UNKNOWN.ToString()))
                //        //{
                //        //    notFoundList_LDrawPartType.Add(LDrawRef);
                //        //}
                //        //else
                //        //{
                //        //    CollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawPartType").InnerXml = LDrawPartType;
                //        //    foundList_LDrawPartType.Add(LDrawRef);
                //        //    dataUpdated = true;
                //        //}

                //        // ** UPDATE SCREEN **
                //        bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
                //        if (index == 10)
                //        {
                //            Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawPartType.Count.ToString("#,##0"));
                //            index = 0;
                //        }
                //        processedCount += 1;
                //        index += 1;
                //    }
                //}                
                //#endregion

                //#region ** UPLOAD FILE TO AZURE & UPDATE CACHE **
                //Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading " + filename + ".xml to Azure...");
                //if (dataUpdated)
                //{
                //    if (filename.Equals("BasePartCollection"))
                //    {
                //        BasePartCollection pc = new BasePartCollection().DeserialiseFromXMLString(CollectionXML.OuterXml);
                //        xmlString = pc.SerializeToString(true);
                //        //Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
                //    }
                //    else
                //    {
                //        CompositePartCollection pc = new CompositePartCollection().DeserialiseFromXMLString(CollectionXML.OuterXml);
                //        xmlString = pc.SerializeToString(true);
                //        //Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
                //    }                    
                //    byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
                //    using (var ms = new MemoryStream(bytes))
                //    {
                //        Blob.Upload(ms, true);
                //    }
                //}
                //#endregion

                //#region ** SHOW CONFIRMATION **
                //string confirmation = filename + " completed successfully..." + Environment.NewLine;
                //confirmation += "LDrawDescription: Found = " + foundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine;
                //foreach (string LDrawRef in foundList_LDrawDescription)
                //{
                //    confirmation += LDrawRef + Environment.NewLine;
                //}
                //confirmation += "Not Found=" + notFoundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine + Environment.NewLine;
                //confirmation += "LDrawPartType: Found = " + foundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
                //foreach (string LDrawRef in foundList_LDrawPartType)
                //{
                //    confirmation += LDrawRef + Environment.NewLine;
                //}
                //confirmation += "Not Found=" + notFoundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
                //MessageBox.Show(confirmation, "Updating LDraw details...");
                //#endregion

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

        private void UpdateLDrawStaticDataDetails_NEW()
        {
            // FUNCTION BELOW STILL NEEDS TO BE FINISHED
            //try
            //{
            //    // ** DOWNLOAD BasePartCollection FROM AZURE **
            //    //Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading BasePartCollection.xml from Azure...");
            //    lblStatus.Text = "Downloading BasePartCollection.xml from Azure...";
            //    BlobClient BasePartCollectionBlob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
            //    byte[] fileContent = new byte[BasePartCollectionBlob.GetProperties().Value.ContentLength];
            //    using (var ms = new MemoryStream(fileContent))
            //    {
            //        //BasePartCollectionBlob.DownloadTo(ms);
            //        await BasePartCollectionBlob.DownloadToAsync(ms);
            //    }
            //    string xmlString = Encoding.UTF8.GetString(fileContent);
            //    //BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(xmlString);
            //    XmlDocument BasePartCollectionXML = new XmlDocument();
            //    BasePartCollectionXML.LoadXml(xmlString);

            //    bool dataUpdated = false;







            //    #region ** GET ALL PARTS WITH A BLANK DESCRIPTION **
            //    XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawDescription='']/@LDrawRef");
            //    //XmlNodeList LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart/@LDrawRef");
            //    List<string> LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
            //                                       .Select(x => x.InnerText)
            //                                       .OrderBy(x => x).ToList();

            //    // ** Get descriptions for all parts **
            //    //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s)...");
            //    lblStatus.Text = "Getting LDraw Description for Part(s)...";
            //    List<string> foundList_LDrawDescription = new List<string>();
            //    List<string> notFoundList_LDrawDescription = new List<string>();
            //    int index = 0;
            //    int processedCount = 0;
            //    //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
            //    foreach (string LDrawRef in LDrawRefList)
            //    {
            //        //string LDrawDescription = new System.Xml.Linq.XText(StaticData.GetLDrawDescription_FromLDrawFile(LDrawRef)).ToString();
            //        //if (LDrawDescription.Equals(""))
            //        //{
            //        //    notFoundList_LDrawDescription.Add(LDrawRef);
            //        //}
            //        //else
            //        //{
            //        //    BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawDescription").InnerXml = LDrawDescription;
            //        //    foundList_LDrawDescription.Add(LDrawRef);
            //        //    dataUpdated = true;
            //        //}

            //        // ** UPDATE SCREEN **
            //        //bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");                    
            //        if (index == 10)
            //        {
            //            //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0"));
            //            lblStatus.Text = "Getting LDraw Description for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawDescription.Count.ToString("#,##0");
            //            index = 0;
            //        }
            //        processedCount += 1;
            //        index += 1;
            //    }
            //    #endregion








            //    #region ** GET ALL PARTS WITH "UNKNOWN" LDraw Part Type **
            //    LDrawRefNodeList = BasePartCollectionXML.SelectNodes("//BasePart[@LDrawPartType='" + BasePart.LDrawPartType.UNKNOWN.ToString() + "']/@LDrawRef");
            //    LDrawRefList = LDrawRefNodeList.Cast<XmlNode>()
            //                                       .Select(x => x.InnerText)
            //                                       .OrderBy(x => x).ToList();

            //    // ** Get LDrawPartType for all parts **
            //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s)...");
            //    List<string> foundList_LDrawPartType = new List<string>();
            //    List<string> notFoundList_LDrawPartType = new List<string>();
            //    index = 0;
            //    processedCount = 0;
            //    Delegates.ToolStripProgressBar_SetMax(this, pbStatus, LDrawRefList.Count);
            //    foreach (string LDrawRef in LDrawRefList)
            //    {
            //        //string LDrawPartType = StaticData.GetLDrawPartType_FromLDrawFile(LDrawRef);
            //        //if (LDrawPartType.Equals("") || LDrawPartType.Equals(BasePart.LDrawPartType.UNKNOWN.ToString()))
            //        //{
            //        //    notFoundList_LDrawPartType.Add(LDrawRef);
            //        //}
            //        //else
            //        //{
            //        //    BasePartCollectionXML.SelectSingleNode("//BasePart[@LDrawRef='" + LDrawRef + "']/@LDrawPartType").InnerXml = LDrawPartType;
            //        //    foundList_LDrawPartType.Add(LDrawRef);
            //        //    dataUpdated = true;
            //        //}

            //        // ** UPDATE SCREEN **
            //        //bw_UpdateLDrawStaticDataDetails.ReportProgress(processedCount, "Working...");
            //        if (index == 10)
            //        {
            //            Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting LDraw Part Type for Part(s) | Processing " + LDrawRef + " (" + processedCount.ToString("#,##0") + " of " + LDrawRefList.Count.ToString("#,##0") + ") | Found: " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found: " + notFoundList_LDrawPartType.Count.ToString("#,##0"));
            //            index = 0;
            //        }
            //        processedCount += 1;
            //        index += 1;
            //    }
            //    #endregion

            //    //await RunTaskAsync(BasePartCollectionXML);

            //    // ** UPLOAD BasePartCollection TO AZURE **
            //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading BasePartCollection.xml to Azure...");
            //    if (dataUpdated)
            //    {
            //        BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(BasePartCollectionXML.OuterXml);
            //        xmlString = bpc.SerializeToString(true);
            //        byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            //        using (var ms = new MemoryStream(bytes))
            //        {
            //            //await BasePartCollectionBlob.UploadAsync(ms, true);
            //        }
            //    }

            //    // ** Show confirmation **
            //    lblStatus.Text = "";
            //    string confirmation = "Completed successfully..." + Environment.NewLine;
            //    confirmation += "LDrawDescription: Found = " + foundList_LDrawDescription.Count.ToString("#,##0") + " | Not Found=" + notFoundList_LDrawDescription.Count.ToString("#,##0") + Environment.NewLine;
            //    confirmation += "LDrawPartType: Found = " + foundList_LDrawPartType.Count.ToString("#,##0") + " | Not Found=" + notFoundList_LDrawPartType.Count.ToString("#,##0") + Environment.NewLine;
            //    MessageBox.Show(confirmation);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
            ////string fileName = "BasePartCollection.xml";
            //string fileName = "CompositePartCollection.xml";
            //try
            //{
            //    // ** DOWNLOAD Collection FROM AZURE **
            //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Downloading " + fileName + " from Azure...");
            //    BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient(fileName);
            //    byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
            //    using (var ms = new MemoryStream(fileContent))
            //    {
            //        blob.DownloadTo(ms);
            //    }
            //    string xmlString = Encoding.UTF8.GetString(fileContent);
            //    //BasePartCollection bpc = new BasePartCollection().DeserialiseFromXMLString(xmlString);
            //    CompositePartCollection cpc = new CompositePartCollection().DeserialiseFromXMLString(xmlString);

            //    // ** UPLOAD Collection TO AZURE **
            //    Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading " + fileName + " to Azure...");
            //    //xmlString = bpc.SerializeToString(true);
            //    xmlString = cpc.SerializeToString(true);
            //    byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            //    using (var ms = new MemoryStream(bytes))
            //    {
            //        blob.Upload(ms, true);
            //    }

            //    // ** Show confirmation **
            //    Delegates.ToolStripLabel_SetText(this, lblStatus, "");
            //    MessageBox.Show(fileName + " successfully flushed...");
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void GenerateMiniFigLDrawFiles()
        {
            //try
            //{
            //    // ** VALIDATION **
            //    if (fldMiniFigRef.Text.Equals(""))
            //    {
            //        throw new Exception("No MiniFig Ref entered...");
            //    }
            //    string MiniFigRef = fldMiniFigRef.Text;

            //    #region ** CREATE Legs File & UPLOAD TO AZURE **
            //    string datText = "";
            //    datText += "0 Minifig Hips and Legs (" + MiniFigRef + ")" + Environment.NewLine;
            //    datText += "0 Name: " + MiniFigRef + "Legs.dat" + Environment.NewLine;
            //    datText += "0 Author: Antony Lodge" + Environment.NewLine;
            //    datText += "0 !LDRAW_ORG Shortcut UPDATE 2016-01" + Environment.NewLine;
            //    datText += "0 !LICENSE Redistributable under CCAL version 2.0 : see CAreadme.txt" + Environment.NewLine;
            //    datText += Environment.NewLine;
            //    datText += "0 BFC CERTIFY CCW" + Environment.NewLine;
            //    datText += Environment.NewLine;
            //    datText += "1 15 0 0 0 1 0 0 0 1 0 0 0 1 3815.dat" + Environment.NewLine;
            //    datText += "1 15 0 12 0 1 0 0 0 1 0 0 0 1 3816.dat" + Environment.NewLine;
            //    datText += "1 15 0 12 0 1 0 0 0 1 0 0 0 1 3817.dat" + Environment.NewLine;
            //    byte[] bytes = Encoding.UTF8.GetBytes(datText);
            //    ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(MiniFigRef + "Legs.dat");
            //    share.Create(bytes.Length);
            //    using (MemoryStream ms = new MemoryStream(bytes))
            //    {
            //        share.Upload(ms);
            //    }
            //    #endregion

            //    #region ** CREATE TORSO File & UPLOAD TO AZURE **
            //    datText = "";
            //    datText += "0 Minifig Torso (" + MiniFigRef + ")" + Environment.NewLine;
            //    datText += "0 Name: " + MiniFigRef + "Torso.dat" + Environment.NewLine;
            //    datText += "0 Author: Antony Lodge" + Environment.NewLine;
            //    datText += "0 !LDRAW_ORG Shortcut UPDATE 2016-01" + Environment.NewLine;
            //    datText += "0 !LICENSE Redistributable under CCAL version 2.0 : see CAreadme.txt" + Environment.NewLine;
            //    datText += Environment.NewLine;
            //    datText += "0 BFC CERTIFY CCW" + Environment.NewLine;
            //    datText += Environment.NewLine;
            //    datText += "1 15 0 0 0 1 0 0 0 1 0 0 0 1 973.dat" + Environment.NewLine;
            //    datText += "1 15 -15.552 9 0 0.985 -0.17 0 0.17 0.985 0 0 0 1 3818.dat" + Environment.NewLine;
            //    datText += "1 15 15.552 9 0 0.985 0.17 0 -0.17 0.985 0 0 0 1 3819.dat" + Environment.NewLine;
            //    datText += "1 14 -23.6904 26.774 -9.8982 0.985 -0.1202 0.1202 0.17 0.6964 -0.6964 0 0.707 0.707 3820.dat" + Environment.NewLine;
            //    datText += "1 14 23.6904 26.774 -9.8982 0.985 0.1202 -0.1202 -0.17 0.6964 -0.6964 0 0.707 0.707 3820.dat" + Environment.NewLine;
            //    bytes = Encoding.UTF8.GetBytes(datText);
            //    share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(MiniFigRef + "Torso.dat");
            //    share.Create(bytes.Length);
            //    using (MemoryStream ms = new MemoryStream(bytes))
            //    {
            //        share.Upload(ms);
            //    }
            //    #endregion

            //    // ** SHOW CONFIRMATION **
            //    string confString = "LDraw parts successfully created for:" + Environment.NewLine;
            //    confString += MiniFigRef + "Legs" + Environment.NewLine;
            //    confString += MiniFigRef + "Torso" + Environment.NewLine;
            //    MessageBox.Show(confString);
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

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
            //try
            //{                
            //    // ** PROCESS FILES IN ELEMENT DIRECTORY **
            //    string filePath = @"D:\LEGO STUFF - Documents\IMAGES\BASEPART";
            //    List<string> PathList = Directory.GetFiles(filePath).ToList();
            //    Delegates.ToolStripProgressBar_SetMax(this, pbStatus, PathList.Count);
            //    int index = 0;
            //    int uploadedFileCount = 0;
            //    foreach (string path in PathList)
            //    {
            //        if (uploadedFileCount == 100) break;

            //        // ** Upload BLOB **
            //        string filename = Path.GetFileNameWithoutExtension(path) + ".png";
            //        BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "images-ldraw").GetBlobClient("official/" + filename);
            //        using (var stream = File.OpenRead(path))
            //        {                        
            //            blob.Upload(stream, true);
            //            //await blob.UploadAsync(ms, true);                        
            //        }

            //        // ** UPDATE SCREEN **
            //        bw_UploadData.ReportProgress(uploadedFileCount, "Working...");
            //        if (index == 5)
            //        {
            //            Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading file(s) to Azure | Processed " + filename + " (" + uploadedFileCount.ToString("#,##0") + " of " + PathList.Count.ToString("#,##0") + ")");
            //            index = 0;
            //        }
            //        index += 1;
            //        uploadedFileCount += 1;
            //    }
            //    //int blobCount = container.ListBlobs().ToList().Count; 
            //    MessageBox.Show("Processed " + PathList.Count.ToString("#,##0") + " file(s) successfully...");               
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
        }

        private void bw_UploadData_DoWork1(object sender, DoWorkEventArgs e)
        {
            //try
            //{               
            //    string LDrawType = "official";
            //    //string LDrawType = "unofficial";
            //    byte[] partBytes = File.ReadAllBytes(@"D:\LEGO STUFF - Documents\" + LDrawType + ".zip");
            //    using (var zippedStream = new MemoryStream(partBytes))
            //    {
            //        using (var archive = new ZipArchive(zippedStream))
            //        {
            //            // ** Get list of all part files **
            //            List<ZipArchiveEntry> entryList =   (from r in archive.Entries
            //                                                where r.FullName.StartsWith("ldraw/parts")
            //                                                && (r.FullName.StartsWith("ldraw/parts/s") == false && r.FullName.StartsWith("ldraw/parts/textures") == false)
            //                                                //where r.FullName.StartsWith("parts")
            //                                                //&& (r.FullName.StartsWith("parts/s") == false && r.FullName.StartsWith("parts/textures") == false)
            //                                                select r).ToList();
            //            Delegates.ToolStripProgressBar_SetMax(this, pbStatus, entryList.Count);
            //            int index = 0;
            //            int uploadedFileCount = 0;
            //            foreach (ZipArchiveEntry entry in entryList)
            //            {
            //                //if (uploadedFileCount == 100) break;

            //                BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "ldraw-parts").GetBlobClient(LDrawType + "/" + entry.Name);
            //                using (var stream = entry.Open())
            //                using (var reader = new StreamReader(stream))
            //                {
            //                    string fileText = reader.ReadToEnd();                               
            //                    byte[] bytes = Encoding.UTF8.GetBytes(fileText);
            //                    using (var ms = new MemoryStream(bytes))
            //                    {
            //                        // ** Upload BLOB **                                                               
            //                        blob.Upload(stream, true);
            //                        //await blob.UploadAsync(ms, true);  
            //                    }
            //                }

            //                // ** UPDATE SCREEN **
            //                bw_UploadData.ReportProgress(uploadedFileCount, "Working...");
            //                if (index == 5)
            //                {
            //                    Delegates.ToolStripLabel_SetText(this, lblStatus, "Uploading file(s) to Azure | Processed " + entry.Name + " (" + uploadedFileCount.ToString("#,##0") + " of " + entryList.Count.ToString("#,##0") + ")");
            //                    index = 0;
            //                }
            //                index += 1;
            //                uploadedFileCount += 1;
            //            }
            //            //int blobCount = container.GetDirectoryReference(LDrawType).ListBlobs().ToList().Count;
            //            MessageBox.Show("Processed " + entryList.Count.ToString("#,##0") + " file(s) successfully...");
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
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
            //GetRebrickablePrice();
            //SettlerTest();
            AsyncTest();
        }

        //private async void GetRebrickableData()
        //{
        //    try
        //    {
                

        //        string url = "https://rebrickable.com/api/v3/lego/sets/41621-1/parts/";
        //        string contents = "";
        //        using (var httpClient = new HttpClient())
        //        {
        //            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
        //            {
        //                request.Headers.TryAddWithoutValidation("Accept", "application/json");
        //                request.Headers.TryAddWithoutValidation("Authorization", "key " + Global_Variables.RebrickableKey);
        //                var response = await httpClient.SendAsync(request);
        //                if (response.StatusCode == HttpStatusCode.OK)
        //                {
        //                    contents = await response.Content.ReadAsStringAsync();
        //                }
        //            }
        //        }

        //        // ** Load JSON string to XML **
        //        var xml = XDocument.Load(JsonReaderWriterFactory.CreateJsonReader(Encoding.ASCII.GetBytes(contents), new XmlDictionaryReaderQuotas()));
        //        string XMLString = xml.ToString();
        //        XmlDocument doc = new XmlDocument();
        //        doc.LoadXml(XMLString);


        //        XmlNodeList partItemList = doc.SelectNodes("//item[@type='object']");


        //        string test = "";

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

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

                    

                    
                }



            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDownloader_Click(object sender, EventArgs e)
        {
            Downloader form = new Downloader();
            form.Visible = true;
        }

        private void btnOrderScreens_Click(object sender, EventArgs e)
        {
            OrderScreens();
        }





        #region ** ORDER SCREENS FUNCTIONS **

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);

        const uint SWP_NOSIZE = 0x0001;
        const uint SWP_NOZORDER = 0x0004;

        private delegate bool EnumWindowsProc(IntPtr hWnd, int lParam);

        [DllImport("USER32.DLL")]
        private static extern bool EnumWindows(EnumWindowsProc enumFunc, int lParam);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);

        [DllImport("USER32.DLL")]
        private static extern int GetWindowTextLength(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        [DllImport("USER32.DLL")]
        private static extern IntPtr GetShellWindow();

        public static IDictionary<IntPtr, string> GetOpenWindows()
        {
            IntPtr shellWindow = GetShellWindow();
            Dictionary<IntPtr, string> windows = new Dictionary<IntPtr, string>();

            EnumWindows(delegate (IntPtr hWnd, int lParam)
            {
                if (hWnd == shellWindow) return true;
                if (!IsWindowVisible(hWnd)) return true;

                int length = GetWindowTextLength(hWnd);
                if (length == 0) return true;

                StringBuilder builder = new StringBuilder(length);
                GetWindowText(hWnd, builder, length + 1);

                windows[hWnd] = builder.ToString();
                return true;

            }, 0);

            return windows;
        }

        //IntPtr hWnd = FindWindow("Notepad", null);
        //IntPtr hWnd = FindWindow(null, "Untitled - Notepad");
        //string windowName = GetWindowTitle(hWnd);
        // If found, position it.
        //if (hWnd != IntPtr.Zero)
        //{
        //    // Move the window to (0,0) without changing its size or position in the Z order.
        //    int xPos = 2000;
        //    int yPos = -500;
        //    SetWindowPos(hWnd, IntPtr.Zero, xPos, yPos, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
        //}

        private void OrderScreens()
        {
            try
            {
                IDictionary<IntPtr, string> test1 = GetOpenWindows();
                foreach (KeyValuePair<IntPtr, string> window in GetOpenWindows())
                {
                    IntPtr handle = window.Key;
                    string title = window.Value;

                    if (title.Contains("Rebrickable"))
                    {
                        int xPos = 1920;
                        int yPos = -500;
                        int width = 1100;
                        int height = 2100;
                        //SetWindowPos(handle, IntPtr.Zero, xPos, yPos, 0, 0, SWP_NOSIZE | SWP_NOZORDER);
                        SetWindowPos(handle, IntPtr.Zero, xPos, yPos, width, height, 0);
                    }

                    if (title.Contains(".pdf"))
                    {
                        int xPos = 3010;
                        int yPos = -500;
                        int width = 1080;
                        int height = 2100;
                        SetWindowPos(handle, IntPtr.Zero, xPos, yPos, width, height, 0);
                    }

                    if (title.Contains("Blender"))
                    {
                        int xPos = 1920;
                        int yPos = -1000;
                        int width = 1100;
                        int height = 500;
                        SetWindowPos(handle, IntPtr.Zero, xPos, yPos, width, height, 0);
                    }

                    if (title.Contains("Lego Unity Viewer"))
                    {
                        int xPos = 3010;
                        int yPos = -1000;
                        int width = 1080;
                        int height = 500;
                        SetWindowPos(handle, IntPtr.Zero, xPos, yPos, width, height, 0);
                    }

                    if (title.Contains("ArfaViewer Wiki"))
                    {
                        int xPos = 1920;
                        int yPos = -2100;
                        int width = 1100;
                        int height = 1100;
                        SetWindowPos(handle, IntPtr.Zero, xPos, yPos, width, height, 0);
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
