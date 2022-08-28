﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml;
using System.Xml.Linq;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Drawing;
using BaseClasses;
using System.Runtime.Serialization.Json;
using System.Net.Http;




namespace Generator
{
    public class StaticData
    {
        // ** Image functions **

        public static void UploadImageToBLOB_UsingURL(string SourceURL, string ImageType, string ImageName)
        {
            string url = Global_Variables.APIUrl2 + "Image/UploadImageToBLOB_UsingURL?SourceURL=" + SourceURL + "&ImageType=" + ImageType + "&ImageName=" + ImageName;
            PostRequestFromURL(url);
        }

        public static byte[] DownloadDataFromBLOB(string Container, string BlobName)
        {
            string url = Global_Variables.APIUrl2 + "Image/DownloadDataFromBLOB?Container=" + Container + "&BlobName=" + BlobName;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            //byte[] data = new UTF8Encoding().GetBytes(JSONString);
            byte[] data = Encoding.ASCII.GetBytes(JSONString);
            return data;
        }





        // ** PartColour functions **

        public static PartColourCollection GetPartColourData_UsingLDrawColourIDList(List<int> IDList)
        {
            string url = Global_Variables.APIUrl2 + "PartColour/GetPartColourData_UsingLDrawColourIDList?";
            foreach (int id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            PartColourCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<PartColourCollection>(JSONString);
            return coll;
        }

        public static string GetLDrawColourID(string LDrawColourName)
        {            
            string url = Global_Variables.APIUrl2 + "PartColour/GetLDrawColourID_UsingLDrawColourName?LDrawColourName=" + LDrawColourName;
            string JSONString = GetJSONResponseFromURL(url);
            return JSONString;
        }

        public static string GetLDrawColourName(string LDrawColourID)
        {
            string url = Global_Variables.APIUrl2 + "PartColour/GetLDrawColourName_UsingLDrawColourID?LDrawColourID=" + LDrawColourID;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return JSONString;
        }

        public static List<string> GetAllLDrawColourNames()
        {
            string url = Global_Variables.APIUrl2 + "PartColour/GetAllLDrawColourNames";
            string JSONString = GetJSONResponseFromURL(url);
            List<string> partColourNameList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(JSONString);
            return partColourNameList;
        }


        // ** SetDetails Functions **

        public static SetDetailsCollection GetSetDetailsData_UsingSetRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/GetSetDetailsData_UsingSetRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            return coll;
        }

        public static SetDetailsCollection GetSetDetailsData_UsingThemeAndSubTheme(string Theme, string SubTheme)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/GetSetDetailsData_UsingThemeAndSubTheme?Theme=" + Theme;
            if (SubTheme != "") url += "&SubTheme=" + SubTheme;
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            return coll;
        }

        public static SetDetails GetSetDetails(string SetRef)
        {
            SetDetails sd = null;
            string url = Global_Variables.APIUrl2 + "SetDetails/GetSetDetailsData_UsingSetRefList?IDList=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection sdc = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            if (sdc.SetDetailsList.Count > 0) sd = sdc.SetDetailsList[0];
            return sd;
        }

        public static bool CheckIfPDFInstructionsExistForSet(string SetRef)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/CheckIfPDFInstructionsExistForSet?SetRef=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            return bool.Parse(JSONString);
        }

        public static bool CheckIfSetDetailExists(string SetRef)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/CheckIfSetDetailExists?SetRef=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            return bool.Parse(JSONString);
        }

        public static void UpdateSetDetailsCounts_UsingSetRef(string SetRef, int PartCount, int SubSetCount, int ModelCount, int MiniFigCount)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/UpdateSetDetailsCounts_UsingSetRef?SetRef=" + SetRef + "&PartCount=" + PartCount + "&SubSetCount=" + SubSetCount + "&ModelCount=" + ModelCount + "&MiniFigCount=" + MiniFigCount;
            PostRequestFromURL(url);
        }

        public static void AddSetDetails(SetDetails sd)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string url = Global_Variables.APIUrl2 + "SetDetails/AddSetDetails";
            PostJSONRequestFromURL(url, json);
        }

        public static void UpdateSetDetails(SetDetails sd)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string url = Global_Variables.APIUrl2 + "SetDetails/UpdateSetDetails";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteSetDetails(string SetRef)
        {
            string url = Global_Variables.APIUrl2 + "SetDetails/DeleteSetDetails?setRef=" + SetRef;
            PostRequestFromURL(url);
        }


        // ** SetInstructions Functions **

        public static SetInstructionsCollection GetSetInstructionsData_UsingSetRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "SetInstructions/GetSetInstructionsData_UsingSetRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            SetInstructionsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetInstructionsCollection>(JSONString);
            return coll;
        }

        public static SetInstructions GetSetInstructions(string SetRef)
        {
            SetInstructions si = null;
            string url = Global_Variables.APIUrl2 + "SetInstructions/GetSetInstructionsData_UsingSetRefList?IDList=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            SetInstructionsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetInstructionsCollection>(JSONString);
            if (coll.SetInstructionsList.Count > 0) si = coll.SetInstructionsList[0];
            return si;
        }

        public static void UpdateSetInstructions(SetInstructions si)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(si);
            string url = Global_Variables.APIUrl2 + "SetInstructions/UpdateSetInstructions";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteSetInstructions(string SetRef)
        {
            string url = Global_Variables.APIUrl2 + "SetInstructions/DeleteSetInstructions?setRef=" + SetRef;
            PostRequestFromURL(url);
        }

        public static void AddSetInstructions(SetInstructions si)
        {           
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(si);
            string url = Global_Variables.APIUrl2 + "SetInstructions/AddSetInstructions";
            PostJSONRequestFromURL(url, json);
        }


        // ** LDrawDetails functions **

        public static LDrawDetailsCollection GetLDrawDetailsData_UsingLDrawRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "LDrawDetails/GetLDrawDetailsData_UsingLDrawRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            LDrawDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<LDrawDetailsCollection>(JSONString);
            return coll;
        }

        public static LDrawDetails GetLDrawDetails(string LDrawRef)
        {
            LDrawDetails ldd = null;
            LDrawDetailsCollection lddc = GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
            if (lddc.LDrawDetailsList.Count > 0) ldd = lddc.LDrawDetailsList[0];
            return ldd;
        }

        public static void AddLDrawDetails(LDrawDetails ldd)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ldd);
            string url = Global_Variables.APIUrl2 + "LDrawDetails/AddLDrawDetails";
            PostJSONRequestFromURL(url, json);
        }


        // ** FBXDetails functions **

        public static FBXDetailsCollection GetFBXDetailsData_UsingLDrawRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "FBXDetails/GetFBXDetailsData_UsingLDrawRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            FBXDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<FBXDetailsCollection>(JSONString);
            return coll;
        }

        public static FBXDetails GetFBXDetails(string LDrawRef)
        {
            FBXDetails td = null;
            FBXDetailsCollection tdc = GetFBXDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
            if (tdc.FBXDetailsList.Count > 0) td = tdc.FBXDetailsList[0];
            return td;
        }


        // ** ThemeDetails Functions **

        public static ThemeDetailsCollection GetThemeDetailsData_UsingThemeList(List<string> IDList)
        {            
            string url = Global_Variables.APIUrl2 + "ThemeDetails/GetThemeDetailsData_UsingThemeList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            ThemeDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<ThemeDetailsCollection>(JSONString);
            return coll;
        }

        public static ThemeDetailsCollection GetAllThemeDetails()
        {
            string url = Global_Variables.APIUrl2 + "ThemeDetails/GetAllThemeDetails";
            string JSONString = GetJSONResponseFromURL(url);
            ThemeDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<ThemeDetailsCollection>(JSONString);
            return coll;
        }

        public static ThemeDetails GetThemeDetails(string Theme)
        {
            ThemeDetails td = null;
            ThemeDetailsCollection tdc = GetThemeDetailsData_UsingThemeList(new List<string>() { Theme });
            if (tdc.ThemeDetailsList.Count > 0) td = tdc.ThemeDetailsList[0];
            return td;
        }

        public static int GetSetCountForThemeAndSubTheme(string Theme, string SubTheme)
        {
            string url = Global_Variables.APIUrl2 + "ThemeDetails/GetSetCountForThemeAndSubTheme?Theme=" + Theme;
            if (SubTheme != "") url += "&SubTheme=" + SubTheme;
            string JSONString = GetJSONResponseFromURL(url);            
            return int.Parse(JSONString);
        }


        // ** TickBack Functions **

        public static TickBackCollection GetTickBackData_UsingTickBackNameList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "TickBack/GetTickBackData_UsingTickBackNameList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            TickBackCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<TickBackCollection>(JSONString);
            return coll;
        }

        public static TickBack GetTickBack(string TickBackName)
        {
            TickBack tb = null;
            TickBackCollection tbc = GetTickBackData_UsingTickBackNameList(new List<string>() { TickBackName });
            if (tbc.TickBackList.Count > 0) tb = tbc.TickBackList[0];
            return tb;
        }

        public static void AddTickBack(TickBack tb)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(tb);
            string url = Global_Variables.APIUrl2 + "TickBack/AddTickBack";
            PostJSONRequestFromURL(url, json);
        }

        public static void UpdateTickBack(TickBack tb)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(tb);
            string url = Global_Variables.APIUrl2 + "TickBack/UpdateTickBack";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteTickBack(string TickBackName)
        {
            string url = Global_Variables.APIUrl2 + "TickBack/DeleteTickBack?TickBackName=" + TickBackName;
            PostRequestFromURL(url);
        }


        // ** Rebrickable Functions

        public static string GetRebrickableSetJSONString(string SetRef)
        {
            string url = Global_Variables.APIUrl2 + "/GetRebrickableSetJSONString?SetRef=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            return JSONString;
        }


        // ** BasePart functions **

        public static BasePartCollection GetBasePartData_UsingLDrawRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl2 + "BasePart/GetBasePartData_UsingLDrawRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            BasePartCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<BasePartCollection>(JSONString);
            return coll;
        }

        public static string GetLDrawDescription(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "BasePart/GetLDrawDescription_UsingLDrawRef?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return JSONString;
        }

        public static int GetLDrawSize(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "BasePart/GetLDrawSize_UsingLDrawRef?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return int.Parse(JSONString);
        }

        public static string GetPartType(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "BasePart/GetPartType_UsingLDrawRef?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return JSONString;
        }

        public static bool CheckIfBasePartExists(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "BasePart/CheckIfBasePartExists?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return bool.Parse(JSONString);

        }

        public static void AddBasePart(BasePart bp)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(bp);
            string url = Global_Variables.APIUrl2 + "BasePart/AddBasePart";
            PostJSONRequestFromURL(url, json);
        }


        // ** SubPartMapping functions **

        public static SubPartMappingCollection GetSubPartMappingData_UsingParentLDrawRefList(string ParentLDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "SubPartMapping/GetSubPartMappingData_UsingParentLDrawRefList?ParentLDrawRef=" + ParentLDrawRef;
            string JSONString = GetJSONResponseFromURL(url);
            SubPartMappingCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SubPartMappingCollection>(JSONString);
            return coll;
        }

        public static bool CheckIfSubPartMappingPartsExist(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "SubPartMapping/CheckIfSubPartMappingPartsExist?ParentLDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return bool.Parse(JSONString);
        }

        public static void AddSubPartMapping(SubPartMapping spm)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(spm);
            string url = Global_Variables.APIUrl2 + "SubPartMapping/AddSubPartMapping";
            PostJSONRequestFromURL(url, json);
        }

       







        // ** CompositePart functions **

        public static CompositePartCollection GetAllCompositeSubParts_FromLDrawDetails(string LDrawRef)
        {
            string url = Global_Variables.APIUrl2 + "LDrawDetails/GetAllCompositeSubParts_FromLDrawDetails?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url);
            CompositePartCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<CompositePartCollection>(JSONString);
            return coll;
        }












        // ** Other functions - not sure where to put them **

        public static string GetJSONResponseFromURL(string url)
        {
            string JSONString = "";
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
                {
                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                    var task = Task.Run(() => httpClient.SendAsync(request));
                    task.Wait();
                    var response = task.Result;
                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception("Request failed: " + response.StatusCode);
                    if (response.StatusCode == HttpStatusCode.OK) JSONString = response.Content.ReadAsStringAsync().Result;
                }
            }
            return JSONString;
        }

        public static void PostRequestFromURL(string url)
        {            
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), url))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent("");
                    request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                    var task = Task.Run(() => httpClient.SendAsync(request));
                    task.Wait();
                    var response = task.Result;
                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception("Request failed: " + response.StatusCode);                    
                }
            }            
        }

        public static void PostJSONRequestFromURL(string url, string content)
        {
            using (var httpClient = new HttpClient())
            {
                using (var request = new HttpRequestMessage(new HttpMethod("POST"), url))
                {
                    request.Headers.TryAddWithoutValidation("accept", "*/*");
                    request.Content = new StringContent(content);
                    request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json");
                    
                    var task = Task.Run(() => httpClient.SendAsync(request));
                    task.Wait();
                    var response = task.Result;
                    if (response.StatusCode != HttpStatusCode.OK) throw new Exception("Request failed: " + response.StatusCode);                   
                }
            }
        }



        //public static void RefreshStaticData_All()
        //{
        //    string xmlString = "";

        //    // ** PARTCOLOUR **
        //    //Global_Variables.PartColourCollection = Global_Variables.APIProxy.GetPartColourData_All();
        //    //string xmlString = Global_Variables.PartColourCollection.SerializeToString(true);
        //    //Global_Variables.PartColourCollectionXML.LoadXml(xmlString);

        //    // ** BASEPART **
        //    //xmlString = Global_Variables.APIProxy.GetStaticData(StaticData.Filename.BasePartCollection.ToString());
        //    //Global_Variables.BasePartCollection = Global_Variables.APIProxy.GetBasePartData_All();
        //    //string result = BaseClasses.BasePartCollection.ConvertBasePartCollectionToDBInsertValuesString(Global_Variables.BasePartCollection);
        //    //xmlString = Global_Variables.BasePartCollection.SerializeToString(true);
        //    //Global_Variables.BasePartCollectionXML.LoadXml(xmlString);

        //    // ** COMPOSITEPART **
        //    //xmlString = Global_Variables.APIProxy.GetStaticData(StaticData.Filename.CompositePartCollection.ToString());
        //    //BaseClasses.CompositePartCollection cpc = Global_Variables.APIProxy.GetCompositePartData_All();
        //    //xmlString = cpc.SerializeToString(true);
        //    //Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);                        
        //}




        //public static async Task RefreshStaticData(StaticData.Filename report)
        //{
        //    try
        //    {
        //        // ** LOAD BasePartCollection STATIC DATA FROM XML **
        //        string url = "https://lodgeaccount.blob.core.windows.net/static-data/" + report + ".xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                //pbStatus.Value = e1.ProgressPercentage;
        //                //lblStatus.Text = "Downloading BasePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                //pbStatus.Value = 0;
        //                //lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            if (report.Equals(Filename.BasePartCollection)) Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //            if (report.Equals(Filename.CompositePartCollection)) Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
        //            if (report.Equals(Filename.PartColourCollection)) Global_Variables.PartColourCollectionXML.LoadXml(xmlString);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        //MessageBox.Show(ex.Message);
        //    }
        //}

        //public enum Filename
        //{
        //    BasePartCollection,
        //    CompositePartCollection,
        //    PartColourCollection
        //}

        //public static async Task RefreshStaticData_BasePartCollection()
        //{
        //    try
        //    {
        //        // ** LOAD BasePartCollection STATIC DATA FROM XML **
        //        string url = "https://lodgeaccount.blob.core.windows.net/static-data/BasePartCollection.xml";                
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                //pbStatus.Value = e1.ProgressPercentage;
        //                //lblStatus.Text = "Downloading BasePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                //pbStatus.Value = 0;
        //                //lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;                    
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //        }

        //        // ** LOAD BasePartCollection STATIC DATA FROM XML - NEW WAY USING Blob Classes **
        //        ////string container = "webgl";
        //        ////string filename = @"Viewer\Build\Web GL.data";
        //        ////var blobToDownload = new BlobContainerClient(Global_Variables.AzureStorageConnString, container).GetBlobClient(filename).Download().Value;
        //        //var blobToDownload = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml").Download().Value;                
        //        //var downloadBuffer = new byte[81920];                
        //        //int totalBytesDownloaded = 0;
        //        //byte[] fileContent = new byte[blobToDownload.ContentLength];
        //        //using (var stream = new MemoryStream(fileContent))
        //        //{
        //        //    bool readData = true;
        //        //    while (readData)
        //        //    {                        
        //        //        Task<int> downloadTask = blobToDownload.Content.ReadAsync(downloadBuffer, 0, downloadBuffer.Length);
        //        //        int bytesRead = await downloadTask;
        //        //        if (bytesRead == 0) readData = false;
        //        //        stream.Write(downloadBuffer, 0, bytesRead);        
        //        //        totalBytesDownloaded += bytesRead;                  

        //        //        double pc = (totalBytesDownloaded / (double)(blobToDownload.ContentLength)) * 100;
        //        //        lblStatus.Text = "Downloading BasePartCollection.xml from Azure | Downloaded " + pc.ToString("#,##0") + "%";
        //        //    }
        //        //    string xmlString = Encoding.UTF8.GetString(fileContent);
        //        //    Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //        //}

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public static async Task RefreshStaticData_CompositePartCollection()
        //{
        //    try
        //    {
        //        // ** LOAD BasePartCollection STATIC DATA FROM XML **
        //        string url = "https://lodgeaccount.blob.core.windows.net/static-data/CompositePartCollection.xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                //pbStatus.Value = e1.ProgressPercentage;
        //                //lblStatus.Text = "Downloading CompositePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                //pbStatus.Value = 0;
        //                //lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //public static async Task RefreshStaticData_PartColourCollection()
        //{
        //    try
        //    {
        //        // ** LOAD PartColourCollection STATIC DATA FROM XML **
        //        string url = "https://lodgeaccount.blob.core.windows.net/static-data/PartColourCollection.xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                //pbStatus.Value = e1.ProgressPercentage;
        //                //lblStatus.Text = "Downloading CompositePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                //pbStatus.Value = 0;
        //                //lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.PartColourCollectionXML.LoadXml(xmlString);
        //            //Global_Variables.pcc = new PartColourCollection().DeserialiseFromXMLString(xmlString);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}


        //public static void RefreshStaticDataCache_OLD()
        //{
        //    //String ref_debug = "";      
        //    try
        //    {
        //        log.Info("Refresh Static Data - START");

        //        #region ** LOAD PARTCOLOUR IMAGES - OLD **
        //        //// ** Get new filenames from disk **
        //        //HashSet<int> new_PartColourImageList = new HashSet<int>();
        //        //foreach (string filename in Directory.GetFiles(Global_Variables.PartColourImageLocation))
        //        //{
        //        //    int LDrawColourID = int.Parse(Path.GetFileNameWithoutExtension(filename));
        //        //    new_PartColourImageList.Add(LDrawColourID);
        //        //}

        //        //// ** Compare new with current dictionary **
        //        //HashSet<int> missing_PartColourImageList = new HashSet<int>();
        //        //foreach (int LDrawColourID in new_PartColourImageList)
        //        //{
        //        //    if (Global_Variables.PartColourImage_Dict.ContainsKey(LDrawColourID) == false)
        //        //    {
        //        //        missing_PartColourImageList.Add(LDrawColourID);
        //        //    }
        //        //}
        //        //log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count + " | new_PartColourImageList.Count=" + new_PartColourImageList.Count + " | missing_PartColourImageList.Count=" + missing_PartColourImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //foreach (int LDrawColourID in missing_PartColourImageList)
        //        //{
        //        //    string filename = Global_Variables.PartColourImageLocation + "\\" + LDrawColourID + ".png";
        //        //    using (var image = new Bitmap(filename))
        //        //    {
        //        //        Global_Variables.PartColourImage_Dict.Add(LDrawColourID, new Bitmap(image));
        //        //    }
        //        //}
        //        //log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count);
        //        #endregion

        //        #region ** LOAD PARTCOLOUR IMAGES ** 

        //        // ** Get new file names from Azure Storage **
        //        CloudBlobContainer container = blobClient.GetContainerReference("images-partcolour");
        //        List<string> blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name.Replace(".png", "")).ToList();
        //        HashSet<int> new_PartColourImageList = new HashSet<int>(blobNames.Select(int.Parse).ToList());

        //        // ** Compare new with current dictionary **
        //        HashSet<int> missing_PartColourImageList = new HashSet<int>();
        //        foreach (int LDrawColourID in new_PartColourImageList)
        //        {
        //            if (Global_Variables.PartColourImage_Dict.ContainsKey(LDrawColourID) == false)
        //            {
        //                missing_PartColourImageList.Add(LDrawColourID);
        //            }
        //        }
        //        log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count + " | new_PartColourImageList.Count=" + new_PartColourImageList.Count + " | missing_PartColourImageList.Count=" + missing_PartColourImageList.Count);

        //        // ** Add new images to dictionary **
        //        foreach (int LDrawColourID in missing_PartColourImageList)
        //        {
        //            CloudBlockBlob blockBlob = container.GetBlockBlobReference(LDrawColourID + ".png");
        //            blockBlob.FetchAttributes();
        //            long fileByteLength = blockBlob.Properties.Length;
        //            byte[] fileContent = new byte[fileByteLength];
        //            for (int i = 0; i < fileByteLength; i++)
        //            {
        //                fileContent[i] = 0x20;
        //            }
        //            blockBlob.DownloadToByteArray(fileContent, 0);
        //            using (var ms = new MemoryStream(fileContent))
        //            {
        //                Global_Variables.PartColourImage_Dict.Add(LDrawColourID, new Bitmap(ms));
        //            }
        //        }
        //        log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count);

        //        #endregion


        //        #region ** LOAD ELEMENT IMAGES - OLD **
        //        //// ** Get new filenames from disk **
        //        //HashSet<string> new_ElementImageList = new HashSet<string>();
        //        //foreach (string filename in Directory.GetFiles(Global_Variables.ElementImageLocation))
        //        //{
        //        //    string elementRef = Path.GetFileNameWithoutExtension(filename);
        //        //    string elementExt = Path.GetExtension(filename).ToUpper();                    
        //        //    new_ElementImageList.Add(elementRef + "|" + elementExt);
        //        //}

        //        //// ** Compare new with current dictionary **
        //        //HashSet<string> missing_ElementImageList = new HashSet<string>();
        //        //foreach (string elementRefAndExt in new_ElementImageList)
        //        //{
        //        //    string elementRef = elementRefAndExt.Split('|')[0];                   
        //        //    string LDrawRef = elementRef.Split('_')[0];
        //        //    string LDrawColourID = elementRef.Split('_')[1];
        //        //    string key = LDrawRef + "|" + LDrawColourID;
        //        //    if (Global_Variables.ElementImage_Dict.ContainsKey(key) == false)
        //        //    {
        //        //        missing_ElementImageList.Add(elementRefAndExt);
        //        //    }
        //        //}
        //        //log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count + " | new_ElementImageList.Count=" + new_ElementImageList.Count + " | missing_ElementImageList.Count=" + missing_ElementImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //foreach (string elementRefAndExt in missing_ElementImageList)
        //        //{
        //        //    string elementRef = elementRefAndExt.Split('|')[0];
        //        //    string elementExt = elementRefAndExt.Split('|')[1];                  
        //        //    string filename = Global_Variables.ElementImageLocation + "\\" + elementRef + elementExt;
        //        //    using (var image = new Bitmap(filename))
        //        //    {
        //        //        string LDrawRef = elementRef.Split('_')[0];
        //        //        string LDrawColourID = elementRef.Split('_')[1];
        //        //        string key = LDrawRef + "|" + LDrawColourID;
        //        //        Global_Variables.ElementImage_Dict.Add(key, new Bitmap(image));
        //        //    }
        //        //}
        //        //log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count);
        //        #endregion

        //        #region ** LOAD ELEMENT IMAGES - NEW **  

        //        // ** Get new file names from Azure Storage **
        //        container = blobClient.GetContainerReference("images-element");
        //        //int blobCount = container.ListBlobs().ToList().Count;
        //        blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name).ToList();
        //        HashSet<string> new_ElementImageList = new HashSet<string>(blobNames);

        //        // ** Compare new with current dictionary **
        //        HashSet<string> missing_ElementImageList = new HashSet<string>();
        //        foreach (string elementRefAndExt in new_ElementImageList)
        //        {
        //            string elementRef = elementRefAndExt.Split('.')[0];
        //            string LDrawRef = elementRef.Split('_')[0];
        //            string LDrawColourID = elementRef.Split('_')[1];
        //            string key = LDrawRef + "|" + LDrawColourID;
        //            if (Global_Variables.ElementImage_Dict.ContainsKey(key) == false)
        //            {
        //                missing_ElementImageList.Add(elementRefAndExt);
        //            }
        //        }
        //        log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count + " | new_ElementImageList.Count=" + new_ElementImageList.Count + " | missing_ElementImageList.Count=" + missing_ElementImageList.Count);

        //        // ** Add new images to dictionary **
        //        foreach (string elementRefAndExt in missing_ElementImageList)
        //        {
        //            string elementRef = elementRefAndExt.Split('.')[0];                   
        //            string LDrawRef = elementRef.Split('_')[0];
        //            string LDrawColourID = elementRef.Split('_')[1];
        //            string key = LDrawRef + "|" + LDrawColourID;
        //            CloudBlockBlob blockBlob = container.GetBlockBlobReference(elementRefAndExt);
        //            blockBlob.FetchAttributes();
        //            long fileByteLength = blockBlob.Properties.Length;
        //            byte[] fileContent = new byte[fileByteLength];
        //            for (int i = 0; i < fileByteLength; i++)
        //            {
        //                fileContent[i] = 0x20;
        //            }
        //            blockBlob.DownloadToByteArray(fileContent, 0);
        //            using (var ms = new MemoryStream(fileContent))
        //            {
        //                Global_Variables.ElementImage_Dict.Add(key, new Bitmap(ms));
        //            }
        //        }
        //        log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count);
        //        #endregion


        //        #region ** LOAD LDRAW IMAGES **  

        //        //// ** Get new filenames from disk **
        //        //HashSet<string> new_LDrawImageList = new HashSet<string>();
        //        //foreach (String filename in Directory.GetFiles(Global_Variables.LDrawImageLocation))
        //        //{
        //        //    string LDrawRef = Path.GetFileNameWithoutExtension(filename);
        //        //    new_LDrawImageList.Add(LDrawRef);
        //        //}

        //        //// ** Compare new with current dictionary **
        //        //HashSet<string> missing_LDrawImageList = new HashSet<string>();
        //        //foreach (string LDrawRef in new_LDrawImageList)
        //        //{
        //        //    if (Global_Variables.LDrawImage_Dict.ContainsKey(LDrawRef) == false)
        //        //    {
        //        //        missing_LDrawImageList.Add(LDrawRef);
        //        //    }
        //        //}
        //        //log.Info("LDrawImage_Dict.Count=" + Global_Variables.LDrawImage_Dict.Count + " | new_LDrawImageList.Count=" + new_LDrawImageList.Count + " | missing_LDrawImageList.Count=" + missing_LDrawImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //foreach (string LDrawRef in missing_LDrawImageList)
        //        //{
        //        //    string filename = Global_Variables.LDrawImageLocation + "\\" + LDrawRef + ".png";
        //        //    using (var image = new Bitmap(filename))
        //        //    {
        //        //        Global_Variables.LDrawImage_Dict.Add(LDrawRef, new Bitmap(image));
        //        //    }
        //        //}
        //        //log.Info("LDrawImage_Dict.Count=" + Global_Variables.LDrawImage_Dict.Count);
        //        #endregion

        //        // ** UPDATE Base Part & Composite Part Collections **
        //        Global_Variables.BasePartCollectionXML.Load(Global_Variables.StaticDataLocation + "\\BasePartCollection.xml");
        //        Global_Variables.CompositePartCollectionXML.Load(Global_Variables.StaticDataLocation + "\\CompositePartCollection.xml");
        //        log.Info("BasePart & CompositePart Collection XMLs updated");

        //        // ** LOAD PARTCOLOUR STATIC DATA FROM XML **
        //        XmlDocument xmlDoc = new XmlDocument();
        //        xmlDoc.Load(Global_Variables.StaticDataLocation + "\\PartColourCollection.xml");
        //        Global_Variables.pcc = new PartColourCollection().DeserialiseFromXMLString(xmlDoc.OuterXml);
        //        log.Info("PartColourCollection pcc.Count=" + Global_Variables.pcc.PartColourList.Count);

        //        log.Info("Refresh Static Data - END");
        //    }
        //    catch (Exception ex)
        //    {
        //        log.Error(ex.Message);
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private BackgroundWorker bw_RefreshStaticData;


        //private void RefreshStaticData_OLD()
        //{
        //    try
        //    {
        //        if (btnRefreshStaticData.Text.Equals("Refresh Static Data"))
        //        {
        //            // ** Update status toolstrip **                
        //            btnRefreshStaticData.Text = "Stop refreshing";
        //            btnRefreshStaticData.ForeColor = Color.Red;
        //            EnableControls_RefreshStaticData(false);

        //            fldLDrawColourName.Items.Clear();

        //            // ** Run background to process functions **
        //            bw_RefreshStaticData = new BackgroundWorker
        //            {
        //                WorkerReportsProgress = true,
        //                WorkerSupportsCancellation = true
        //            };
        //            bw_RefreshStaticData.DoWork += new DoWorkEventHandler(bw_RefreshStaticData_DoWork);
        //            bw_RefreshStaticData.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RefreshStaticData_RunWorkerCompleted);
        //            bw_RefreshStaticData.ProgressChanged += new ProgressChangedEventHandler(bw_RefreshStaticData_ProgressChanged);
        //            bw_RefreshStaticData.RunWorkerAsync();
        //        }
        //        else
        //        {
        //            Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Cancelling...");
        //            Delegates.ToolStripLabel_SetText(this, lblStatus, "Cancelling...");
        //            bw_RefreshStaticData.CancelAsync();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Delegates.ToolStripButton_SetText(this, btnRefreshStaticData, "Refresh Static Data");
        //        btnRefreshStaticData.ForeColor = Color.Black;
        //        pbStatus.Value = 0;
        //        EnableControls_RefreshStaticData(true);
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "");
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void bw_RefreshStaticData_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        //{
        //    try
        //    {
        //        Delegates.ToolStripButton_SetTextAndForeColor(this, btnRefreshStaticData, "Refresh Static Data", Color.Black);
        //        Delegates.ToolStripProgressBar_SetValue(this, pbStatus, 0);
        //        EnableControls_RefreshStaticData(true);
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void bw_RefreshStaticData_ProgressChanged(object sender, ProgressChangedEventArgs e)
        //{
        //    try
        //    {
        //        Delegates.ToolStripProgressBar_SetValue(this, pbStatus, e.ProgressPercentage);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private void bw_RefreshStaticData_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    try
        //    {
        //        log.Info("Refresh Static Data - START");

        //        #region ** LOAD PARTCOLOUR IMAGES - DEMISED ** 

        //        //// ** Get new file names from Azure Storage **
        //        //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Partcolour Images from Azure...");
        //        //CloudBlobContainer container = blobClient.GetContainerReference("images-partcolour");
        //        //List<string> blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name.Replace(".png", "")).ToList();
        //        //HashSet<int> new_PartColourImageList = new HashSet<int>(blobNames.Select(int.Parse).ToList());

        //        //// ** Compare new with current dictionary **
        //        //HashSet<int> missing_PartColourImageList = new HashSet<int>();
        //        //foreach (int LDrawColourID in new_PartColourImageList)
        //        //{
        //        //    if (Global_Variables.PartColourImage_Dict.ContainsKey(LDrawColourID) == false)
        //        //    {
        //        //        missing_PartColourImageList.Add(LDrawColourID);
        //        //    }
        //        //}
        //        //log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count + " | new_PartColourImageList.Count=" + new_PartColourImageList.Count + " | missing_PartColourImageList.Count=" + missing_PartColourImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, missing_PartColourImageList.Count);
        //        //int index = 0;
        //        //int downloadCount = 0;
        //        //foreach (int LDrawColourID in missing_PartColourImageList)
        //        //{
        //        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference(LDrawColourID + ".png");
        //        //    blockBlob.FetchAttributes();
        //        //    long fileByteLength = blockBlob.Properties.Length;
        //        //    byte[] fileContent = new byte[fileByteLength];
        //        //    for (int i = 0; i < fileByteLength; i++)
        //        //    {
        //        //        fileContent[i] = 0x20;
        //        //    }
        //        //    blockBlob.DownloadToByteArray(fileContent, 0);
        //        //    using (var ms = new MemoryStream(fileContent))
        //        //    {
        //        //        Global_Variables.PartColourImage_Dict.Add(LDrawColourID, new Bitmap(ms));
        //        //    }

        //        //    // ** UPDATE SCREEN **
        //        //    bw_RefreshStaticData.ReportProgress(downloadCount, "Working...");
        //        //    if (index == 5)
        //        //    {
        //        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Partcolour Images from Azure | Downloading " + LDrawColourID + ".png (" + downloadCount + " of " + missing_PartColourImageList.Count + ")");
        //        //        index = 0;
        //        //    }
        //        //    index += 1;
        //        //    downloadCount += 1;
        //        //}
        //        //log.Info("PartColourImage_Dict.Count=" + Global_Variables.PartColourImage_Dict.Count);

        //        #endregion

        //        #region ** LOAD ELEMENT IMAGES - DEMISED **  

        //        //// ** Get new file names from Azure Storage **
        //        //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Element Images from Azure...");
        //        //container = blobClient.GetContainerReference("images-element");
        //        ////int blobCount = container.ListBlobs().ToList().Count;
        //        //blobNames = container.ListBlobs().OfType<CloudBlockBlob>().Select(b => b.Name).ToList();
        //        //HashSet<string> new_ElementImageList = new HashSet<string>(blobNames);

        //        //// ** Compare new with current dictionary **
        //        //HashSet<string> missing_ElementImageList = new HashSet<string>();
        //        //foreach (string elementRefAndExt in new_ElementImageList)
        //        //{
        //        //    string elementRef = elementRefAndExt.Split('.')[0];
        //        //    string LDrawRef = elementRef.Split('_')[0];
        //        //    string LDrawColourID = elementRef.Split('_')[1];
        //        //    string key = LDrawRef + "|" + LDrawColourID;
        //        //    if (Global_Variables.ElementImage_Dict.ContainsKey(key) == false)
        //        //    {
        //        //        //missing_ElementImageList.Add(elementRefAndExt);
        //        //    }
        //        //}
        //        //log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count + " | new_ElementImageList.Count=" + new_ElementImageList.Count + " | missing_ElementImageList.Count=" + missing_ElementImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //Delegates.ToolStripProgressBar_SetMax(this, pbStatus, missing_ElementImageList.Count);
        //        //index = 0;
        //        //downloadCount = 0;
        //        //foreach (string elementRefAndExt in missing_ElementImageList)
        //        //{
        //        //    string elementRef = elementRefAndExt.Split('.')[0];
        //        //    string LDrawRef = elementRef.Split('_')[0];
        //        //    string LDrawColourID = elementRef.Split('_')[1];
        //        //    string key = LDrawRef + "|" + LDrawColourID;
        //        //    CloudBlockBlob blockBlob = container.GetBlockBlobReference(elementRefAndExt);
        //        //    blockBlob.FetchAttributes();
        //        //    long fileByteLength = blockBlob.Properties.Length;
        //        //    byte[] fileContent = new byte[fileByteLength];
        //        //    for (int i = 0; i < fileByteLength; i++)
        //        //    {
        //        //        fileContent[i] = 0x20;
        //        //    }
        //        //    blockBlob.DownloadToByteArray(fileContent, 0);
        //        //    using (var ms = new MemoryStream(fileContent))
        //        //    {
        //        //        Global_Variables.ElementImage_Dict.Add(key, new Bitmap(ms));
        //        //    }

        //        //    // ** UPDATE SCREEN **
        //        //    bw_RefreshStaticData.ReportProgress(downloadCount, "Working...");
        //        //    if (index == 5)
        //        //    {
        //        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Element Images from Azure | Downloading " + elementRef + " (" + downloadCount + " of " + missing_ElementImageList.Count + ")");
        //        //        index = 0;
        //        //    }
        //        //    index += 1;
        //        //    downloadCount += 1;
        //        //}
        //        //log.Info("ElementImage_Dict.Count=" + Global_Variables.ElementImage_Dict.Count);
        //        #endregion

        //        #region ** LOAD LDRAW IMAGES - DEMISED **  

        //        //// ** Get new filenames from disk **
        //        //HashSet<string> new_LDrawImageList = new HashSet<string>();
        //        //foreach (String filename in Directory.GetFiles(Global_Variables.LDrawImageLocation))
        //        //{
        //        //    string LDrawRef = Path.GetFileNameWithoutExtension(filename);
        //        //    new_LDrawImageList.Add(LDrawRef);
        //        //}

        //        //// ** Compare new with current dictionary **
        //        //HashSet<string> missing_LDrawImageList = new HashSet<string>();
        //        //foreach (string LDrawRef in new_LDrawImageList)
        //        //{
        //        //    if (Global_Variables.LDrawImage_Dict.ContainsKey(LDrawRef) == false)
        //        //    {
        //        //        missing_LDrawImageList.Add(LDrawRef);
        //        //    }
        //        //}
        //        //log.Info("LDrawImage_Dict.Count=" + Global_Variables.LDrawImage_Dict.Count + " | new_LDrawImageList.Count=" + new_LDrawImageList.Count + " | missing_LDrawImageList.Count=" + missing_LDrawImageList.Count);

        //        //// ** Add new images to dictionary **
        //        //foreach (string LDrawRef in missing_LDrawImageList)
        //        //{
        //        //    string filename = Global_Variables.LDrawImageLocation + "\\" + LDrawRef + ".png";
        //        //    using (var image = new Bitmap(filename))
        //        //    {
        //        //        Global_Variables.LDrawImage_Dict.Add(LDrawRef, new Bitmap(image));
        //        //    }
        //        //}
        //        //log.Info("LDrawImage_Dict.Count=" + Global_Variables.LDrawImage_Dict.Count);
        //        #endregion

        //        // ** UPDATE Base Part & Composite Part Collections - OLD **
        //        //Delegates.ToolStripLabel_SetText(this, lblStatus, "Getting Base Part & Composite Part Collections from local files...");
        //        //Global_Variables.BasePartCollectionXML.Load(Global_Variables.StaticDataLocation + "\\BasePartCollection.xml");
        //        //Global_Variables.CompositePartCollectionXML.Load(Global_Variables.StaticDataLocation + "\\CompositePartCollection.xml");
        //        //log.Info("BasePart & CompositePart Collection XMLs updated");

        //        // ** LOAD PARTCOLOUR STATIC DATA FROM XML - OLD **
        //        //XmlDocument xmlDoc = new XmlDocument();
        //        //xmlDoc.Load(Global_Variables.StaticDataLocation + "\\PartColourCollection.xml");
        //        //Global_Variables.pcc = new PartColourCollection().DeserialiseFromXMLString(xmlDoc.OuterXml);
        //        //log.Info("PartColourCollection pcc.Count=" + Global_Variables.pcc.PartColourList.Count);

        //        // ** LOAD BasePartCollection STATIC DATA FROM XML - NEW **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Updating Static Data | Getting BasePartCollection.xml from Azure...");
        //        CloudBlockBlob blockBlob = blobClient.GetContainerReference("static-data").GetBlockBlobReference("BasePartCollection.xml");
        //        string xmlString = Encoding.UTF8.GetString(HelperFunctions.DownloadAzureFile(blockBlob));
        //        Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //        log.Info("BasePart Collection XMLs updated");

        //        //BlobContainerClient container = new BlobContainerClient(Global_Variables.AzureStorgeConnString, "static-data");
        //        //BlobClient blob = container.GetBlobClient("BasePartCollection.xml");

        //        // ** LOAD CompositePartCollection STATIC DATA FROM XML - NEW **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Updating Static Data | Getting CompositePartCollection.xml from Azure...");
        //        blockBlob = blobClient.GetContainerReference("static-data").GetBlockBlobReference("CompositePartCollection.xml");
        //        xmlString = Encoding.UTF8.GetString(HelperFunctions.DownloadAzureFile(blockBlob));
        //        Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
        //        log.Info("CompositePart Collection XMLs updated");

        //        // ** LOAD PARTCOLOUR STATIC DATA FROM XML - NEW **
        //        Delegates.ToolStripLabel_SetText(this, lblStatus, "Updating Static Data | Getting PartColourCollection.xml from Azure...");
        //        blockBlob = blobClient.GetContainerReference("static-data").GetBlockBlobReference("PartColourCollection.xml");                
        //        xmlString = Encoding.UTF8.GetString(HelperFunctions.DownloadAzureFile(blockBlob));
        //        Global_Variables.PartColourCollectionXML.LoadXml(xmlString);
        //        //Global_Variables.pcc = new PartColourCollection().DeserialiseFromXMLString(xmlString);
        //        log.Info("PartColourCollection XMLs updated");

        //        // ** Populate the LDrawColour Name dropdown **
        //        //List<string> partColourNameList =   (from r in Global_Variables.pcc.PartColourList
        //        //                                    select r.LDrawColourName).OrderBy(x => x).ToList();                
        //        XmlNodeList LDrawColourNameNodeList = Global_Variables.PartColourCollectionXML.SelectNodes("//PartColour/@LDrawColourName");
        //        List<string> partColourNameList =   LDrawColourNameNodeList.Cast<XmlNode>()
        //                                           .Select(x => x.InnerText)
        //                                           .OrderBy(x => x).ToList();
        //        Delegates.ToolStripComboBox_AddItems(this, fldLDrawColourName, partColourNameList);

        //        log.Info("Refresh Static Data - END");
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        // OBSOLETE

        //private async void RefreshStaticData_NEW_OLD()
        //{
        //    try
        //    {
        //        #region ** LOAD BasePartCollection STATIC DATA FROM XML **
        //        string url = "https://lodgeaccount.blob.core.windows.net/static-data/BasePartCollection.xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                pbStatus.Value = e1.ProgressPercentage;
        //                lblStatus.Text = "Downloading BasePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                pbStatus.Value = 0;
        //                lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //        }
        //        #endregion

        //        #region ** LOAD CompositePartCollection STATIC DATA FROM XML **
        //        url = "https://lodgeaccount.blob.core.windows.net/static-data/CompositePartCollection.xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                pbStatus.Value = e1.ProgressPercentage;
        //                lblStatus.Text = "Downloading CompositePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                pbStatus.Value = 0;
        //                lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.CompositePartCollectionXML.LoadXml(xmlString);
        //        }
        //        #endregion

        //        #region ** LOAD PARTCOLOUR STATIC DATA FROM XML **               
        //        url = "https://lodgeaccount.blob.core.windows.net/static-data/PartColourCollection.xml";
        //        using (WebClient webClient = new WebClient())
        //        {
        //            webClient.DownloadProgressChanged += (s, e1) =>
        //            {
        //                pbStatus.Value = e1.ProgressPercentage;
        //                lblStatus.Text = "Downloading CompositePartCollection.xml from Azure | Downloaded " + e1.ProgressPercentage + "%";
        //            };
        //            webClient.DownloadDataCompleted += (s, e1) =>
        //            {
        //                pbStatus.Value = 0;
        //                lblStatus.Text = "";
        //            };
        //            Task<byte[]> downloadTask = webClient.DownloadDataTaskAsync(new Uri(url));
        //            byte[] result = await downloadTask;
        //            string xmlString = Encoding.UTF8.GetString(result);
        //            Global_Variables.PartColourCollectionXML.LoadXml(xmlString);
        //            //Global_Variables.pcc = new PartColourCollection().DeserialiseFromXMLString(xmlString);
        //        }
        //        #endregion

        //        #region ** Populate the LDrawColour Name dropdown **
        //        //List<string> partColourNameList =   (from r in Global_Variables.pcc.PartColourList
        //        //                                    select r.LDrawColourName).OrderBy(x => x).ToList();                
        //        XmlNodeList LDrawColourNameNodeList = Global_Variables.PartColourCollectionXML.SelectNodes("//PartColour/@LDrawColourName");
        //        List<string> partColourNameList = LDrawColourNameNodeList.Cast<XmlNode>()
        //                                           .Select(x => x.InnerText)
        //                                           .OrderBy(x => x).ToList();
        //        //Delegates.ToolStripComboBox_AddItems(this, fldLDrawColourName, partColourNameList);
        //        fldLDrawColourName.Items.AddRange(partColourNameList.ToArray());
        //        #endregion




        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show(ex.Message);
        //    }
        //}

        //private async Task RefreshStaticData()


    }



}
