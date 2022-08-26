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

        public static Bitmap GetImage(ImageType imageType, string[] _params)
        {
            return Global_Variables.APIProxy.GetImage(imageType, _params);
        }

        public static string UploadImageToBLOB(string sourceURL, string ImageType, string ImageName)
        {
            return Global_Variables.APIProxy.UploadImageToBLOB(sourceURL, ImageType, ImageName);
        }


        // ** PartColour functions **

        public static PartColourCollection GetPartColourData_UsingLDrawColourIDList(List<int> IDList)
        {
            //return Global_Variables.APIProxy.GetPartColourData_UsingLDrawColourIDList(IDList);

            string url = Global_Variables.APIUrl + "GetPartColourData_UsingLDrawColourIDList?";
            foreach (int id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            PartColourCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<PartColourCollection>(JSONString);
            return coll;
        }

        public static int GetLDrawColourID(string LDrawColourName)
        {
            //int LDrawColourID = int.Parse(Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourName='" + LDrawColourName + "']/@LDrawColourID").InnerXml);
            //int LDrawColourID = (from r in Global_Variables.PartColourCollection.PartColourList
            //                    where r.LDrawColourName == LDrawColourName
            //                    select r.LDrawColourID).FirstOrDefault();

            // ** Get data from API **
            //return Global_Variables.APIProxy.GetLDrawColourID_UsingLDrawColourName(LDrawColourName);

            string url = Global_Variables.APIUrl + "GetLDrawColourID_UsingLDrawColourName?LDrawColourName=" + LDrawColourName;
            string JSONString = GetJSONResponseFromURL(url);
            return int.Parse(JSONString);
        }

        public static string GetLDrawColourName(int LDrawColourID)
        {
            // ** Get data from PartColourCollection XML **
            //string LDrawColourName = Global_Variables.PartColourCollectionXML.SelectSingleNode("//PartColour[@LDrawColourID='" + LDrawColourID + "']/@LDrawColourName").InnerXml;

            // ** Get data from PartColourCollection object **
            //string LDrawColourName = (from r in Global_Variables.PartColourCollection.PartColourList
            //                          where r.LDrawColourID == LDrawColourID
            //                          select r.LDrawColourName).FirstOrDefault();

            // ** Get data from API **
            //return Global_Variables.APIProxy.GetLDrawColourName_UsingLDrawColourID(LDrawColourID);

            string url = Global_Variables.APIUrl + "GetLDrawColourName_UsingLDrawColourID?LDrawColourID=" + LDrawColourID;
            string JSONString = GetJSONResponseFromURL(url).Replace("\"", "");
            return JSONString;
        }

        public static List<string> GetAllLDrawColourNames()
        {
            // ** Get data from API **
            //return Global_Variables.APIProxy.GetAllLDrawColourNames();

            string url = Global_Variables.APIUrl + "GetAllLDrawColourNames";
            string JSONString = GetJSONResponseFromURL(url);
            List<string> partColourNameList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(JSONString);
            return partColourNameList;
        }


        // ** SetDetails Functions **

        public static SetDetails GetSetDetails(string SetRef)
        {
            //SetDetails sd = null;
            //SetDetailsCollection sdc = Global_Variables.APIProxy.GetSetDetailsData_UsingSetRefList(new List<string>() { SetRef });
            //if (sdc.SetDetailsList.Count > 0) sd = sdc.SetDetailsList[0];            
            //return sd;

            SetDetails sd = null;
            string url = Global_Variables.APIUrl + "GetSetDetailsData_UsingSetRefList?IDList=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection sdc = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            if (sdc.SetDetailsList.Count > 0) sd = sdc.SetDetailsList[0];
            return sd;
        }

        public static SetDetailsCollection GetSetDetailsData_UsingSetRefList(List<string> IDList)
        {
            string url = Global_Variables.APIUrl + "GetSetDetailsData_UsingSetRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            return coll;
        }

        public static SetDetailsCollection GetSetDetailsData_UsingThemeAndSubTheme(string theme, string subTheme)
        {
            //return Global_Variables.APIProxy.GetSetDetailsData_UsingThemeAndSubTheme(theme, subTheme);

            string url = Global_Variables.APIUrl + "GetSetDetailsData_UsingThemeAndSubTheme?theme=" + theme;
            if(subTheme != "") url += "&subTheme=" + subTheme;            
            string JSONString = GetJSONResponseFromURL(url);
            SetDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetDetailsCollection>(JSONString);
            return coll;
        }

        public static void UpdateSetDetailsCounts_UsingSetRef(string SetRef, int PartCount, int SubSetCount, int ModelCount, int MiniFigCount)
        {
            //Global_Variables.APIProxy.UpdateSetDetailsCounts_UsingSetRef(SetRef, PartCount, SubSetCount, ModelCount, MiniFigCount);

            string url = Global_Variables.APIUrl + "UpdateSetDetailsCounts_UsingSetRef?SetRef=" + SetRef + "&PartCount=" + PartCount + "&SubSetCount=" + SubSetCount + "&ModelCount=" + ModelCount + "&MiniFigCount=" + MiniFigCount;
            PostRequestFromURL(url);
        }

        public static void AddSetDetails(SetDetails sd)
        {
            //Global_Variables.APIProxy.AddSetDetails(sd);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string url = Global_Variables.APIUrl + "AddSetDetails";
            PostJSONRequestFromURL(url, json);
        }

        public static void UpdateSetDetails(SetDetails sd)
        {
            //Global_Variables.APIProxy.UpdateSetDetails(sd);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(sd);
            string url = Global_Variables.APIUrl + "UpdateSetDetails";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteSetDetails(string SetRef)
        {
            //Global_Variables.APIProxy.DeleteSetDetails(SetRef);

            string url = Global_Variables.APIUrl + "DeleteSetDetails?setRef=" + SetRef;
            PostRequestFromURL(url);
        }

        public static bool CheckIfPDFInstructionsExistForSet(string setRef)
        {
            //return Global_Variables.APIProxy.CheckIfPDFInstructionsExistForSet(setRef);

            string url = Global_Variables.APIUrl + "CheckIfPDFInstructionsExistForSet?setRef=" + setRef;
            string JSONString = GetJSONResponseFromURL(url);
            return bool.Parse(JSONString);
        }

        public static bool CheckIfSetDetailExists(string SetRef)
        {
            //return Global_Variables.APIProxy.CheckIfSetDetailExists(SetRef);

            string url = Global_Variables.APIUrl + "CheckIfSetDetailExists?setRef=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            return bool.Parse(JSONString);
        }


        // ** SetInstructions Functions **

        public static SetInstructions GetSetInstructions(string SetRef)
        {
            SetInstructions si = null;
            string url = Global_Variables.APIUrl + "GetSetInstructionsData_UsingSetRefList?IDList=" + SetRef;
            string JSONString = GetJSONResponseFromURL(url);
            SetInstructionsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<SetInstructionsCollection>(JSONString);
            if (coll.SetInstructionsList.Count > 0) si = coll.SetInstructionsList[0];
            return si;
        }

        public static void UpdateSetInstructions(SetInstructions si)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(si);
            string url = Global_Variables.APIUrl + "UpdateSetInstructions";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteSetInstructions(string SetRef)
        {
            string url = Global_Variables.APIUrl + "DeleteSetInstructions?setRef=" + SetRef;
            PostRequestFromURL(url);
        }

        public static void AddSetInstructions(SetInstructions si)
        {           
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(si);
            string url = Global_Variables.APIUrl + "AddSetInstructions";
            PostJSONRequestFromURL(url, json);
        }


        // ** LDrawDetails functions **

        public static LDrawDetailsCollection GetLDrawDetailsData_UsingLDrawRefList(List<string> IDList)
        {
            //return Global_Variables.APIProxy.GetLDrawDetailsData_UsingLDrawRefList(IDList);

            string url = Global_Variables.APIUrl + "GetLDrawDetailsData_UsingLDrawRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            LDrawDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<LDrawDetailsCollection>(JSONString);
            return coll;
        }

        public static void AddLDrawDetails(LDrawDetails ldd)
        {
            //Global_Variables.APIProxy.AddLDrawDetails(ldd);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(ldd);
            string url = Global_Variables.APIUrl + "AddLDrawDetails";
            PostJSONRequestFromURL(url, json);
        }


        // ** FBXDetails functions **

        public static FBXDetailsCollection GetFBXDetailsData_UsingLDrawRefList(List<string> IDList)
        {
            //return Global_Variables.APIProxy.GetFBXDetailsData_UsingLDrawRefList(IDList);

            string url = Global_Variables.APIUrl + "GetFBXDetailsData_UsingLDrawRefList?";
            foreach (string id in IDList) url += "IDList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            FBXDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<FBXDetailsCollection>(JSONString);
            return coll;
        }

        public static FBXDetails GetFBXDetails(string LDrawRef)
        {
            //return Global_Variables.APIProxy.GetFBXDetails(LDrawRef, partType);

            FBXDetails td = null;
            FBXDetailsCollection tdc = GetFBXDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
            if (tdc.FBXDetailsList.Count > 0) td = tdc.FBXDetailsList[0];
            return td;
        }


        // ** ThemeDetails Functions **

        public static ThemeDetailsCollection GetThemeDetailsData_UsingThemeList(List<string> ThemeList)
        {            
            string url = Global_Variables.APIUrl + "GetThemeDetailsData_UsingThemeList?";
            foreach (string id in ThemeList) url += "ThemeList=" + id + "&";
            string JSONString = GetJSONResponseFromURL(url);
            ThemeDetailsCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<ThemeDetailsCollection>(JSONString);
            return coll;
        }

        public static ThemeDetailsCollection GetAllThemeDetails()
        {
            //return Global_Variables.APIProxy.GetAllThemeDetails();

            string url = Global_Variables.APIUrl + "GetAllThemeDetails";
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

        public static int GetSetCountForThemeAndSubTheme(string theme, string subTheme)
        {
            //return Global_Variables.APIProxy.GetSetCountForThemeAndSubTheme(theme, subTheme);

            string url = Global_Variables.APIUrl + "GetSetCountForThemeAndSubTheme?theme=" + theme;
            if (subTheme != "") url += "&subTheme=" + subTheme;
            string JSONString = GetJSONResponseFromURL(url);            
            return int.Parse(JSONString);

        }


        // ** TickBack Functions **

        public static TickBackCollection GetTickBackData_UsingTickBackNameList(List<string> NameList)
        {
            string url = Global_Variables.APIUrl + "GetTickBackData_UsingTickBackNameList?";
            foreach (string id in NameList) url += "NameList=" + id + "&";
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
            //Global_Variables.APIProxy.AddTickBack(tb);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(tb);
            string url = Global_Variables.APIUrl + "AddTickBack";
            PostJSONRequestFromURL(url, json);
        }

        public static void UpdateTickBack(TickBack tb)
        {
            //Global_Variables.APIProxy.UpdateTickBack(tb);

            string json = Newtonsoft.Json.JsonConvert.SerializeObject(tb);
            string url = Global_Variables.APIUrl + "UpdateTickBack";
            PostJSONRequestFromURL(url, json);
        }

        public static void DeleteTickBack(string TickBackName)
        {
            //Global_Variables.APIProxy.DeleteTickBack(TickBackName);

            string url = Global_Variables.APIUrl + "DeleteTickBack?TickBackName=" + TickBackName;
            PostRequestFromURL(url);
        }




















        // ** BasePart functions **

        public static BasePartCollection GetBasePartData_UsingLDrawRefList(List<string> IDList)
        {  
            return Global_Variables.APIProxy.GetBasePartData_UsingLDrawRefList(IDList);
        }

        public static string GetLDrawDescription(string LDrawRef)
        {
            return Global_Variables.APIProxy.GetLDrawDescription_UsingLDrawRef(LDrawRef);
        }

        public static int GetLDrawSize(string LDrawRef)
        {            
            return Global_Variables.APIProxy.GetLDrawSize_UsingLDrawRef(LDrawRef);
        }

        public static string GetPartType(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            //String sql = "SELECT PART_TYPE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            //var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            //string partType = (string)results.Rows[0]["PART_TYPE"];
            //return partType;

            // ** Get data from API **
            return Global_Variables.APIProxy.GetPartType_UsingLDrawRef(LDrawRef);
        }

        public static bool CheckIfBasePartExists(string LDrawRef)
        {            
            return Global_Variables.APIProxy.CheckIfBasePartExists(LDrawRef);
        }

        public static void AddBasePart(BaseClasses.BasePart p)
        {
            Global_Variables.APIProxy.AddBasePart(p);
        }


        // ** CompositePart functions **

        public static CompositePartCollection GetCompositePartData_UsingLDrawRefList(List<string> IDList)
        {           
            return Global_Variables.APIProxy.GetCompositePartData_UsingLDrawRefList(IDList);
        }

        public static CompositePartCollection GetCompositePartData_UsingParentLDrawRefList(string ParentLDrawRef)
        {
            return Global_Variables.APIProxy.GetCompositePartData_UsingParentLDrawRefList(ParentLDrawRef);
        }

        public static bool CheckIfCompositePartsExist(string LDrawRef)
        {
            return Global_Variables.APIProxy.CheckIfCompositePartsExist(LDrawRef);
        }

        public static void AddCompositePart(CompositePart p)
        {
            Global_Variables.APIProxy.AddCompositePart(p);
        }

        public static CompositePartCollection GetAllCompositeSubParts_FromLDrawDetails(string LDrawRef)
        {
            //return Global_Variables.APIProxy.GetAllCompositeSubParts_FromLDrawDetails(LDrawRef);

            string url = Global_Variables.APIUrl + "GetAllCompositeSubParts_FromLDrawDetails?LDrawRef=" + LDrawRef;
            string JSONString = GetJSONResponseFromURL(url);
            CompositePartCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<CompositePartCollection>(JSONString);
            return coll;
        }



        














        // ** Rebrickable Functions

        public static string GetRebrickableSetJSONString(string SetRef)
        {
            return Global_Variables.APIProxy.GetRebrickableSetJSONString(SetRef);
        }



        






        // ** Other functions - not sure where to put them **

        public static Dictionary<string, XmlDocument> GetMiniFigXMLDict(XmlDocument setXML)
        {
            return Global_Variables.APIProxy.GetMiniFigXMLDict(setXML);
        }

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
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Request failed: " + response.StatusCode);
                    }
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
                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        throw new Exception("Request failed: " + response.StatusCode);
                    }
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
