using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Diagnostics;
using System.Threading;
using ScintillaNET;
using iTextSharp.text.pdf;
using Azure.Storage.Blobs;
using Azure.Storage.Files.Shares;
using BaseClasses;
using System.Runtime.Serialization.Json;
using System.Net.Http;



namespace Generator
{
    public class APIProxy
    {
        public string AzureStorageConnString;

        public APIProxy(string AzureStorageConnString)
        {
            this.AzureStorageConnString = AzureStorageConnString;

        }


        // ** PUBLIC ENDPOINT FUNCTIONS **
        //public string GetSetXMLString(string SetRef)
        //{
        //    //string xmlString = "";
        //    BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "set-xmls").GetBlobClient(SetRef + ".xml");
        //    //byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
        //    //using (var ms = new MemoryStream(fileContent)) blob.DownloadTo(ms);
        //    //string xmlString = Encoding.UTF8.GetString(fileContent);
        //    string xmlString = DownloadBlobToXMLString(blob);            
        //    return xmlString;
        //}

        public BaseClasses.Set GetSet(string SetRef)
        {
            Set set = null;
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "set-xmls").GetBlobClient(SetRef + ".xml");            
            string xmlString = DownloadBlobToXMLString(blob);
            if (xmlString != "") set = new Set().DeserialiseFromXMLString(xmlString);                      
            return set;
        }

        public void UpdateSet(Set set)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(set.Ref + ".xml");
            string xmlString = set.SerializeToString(true);
            UploadXMLStringToBlob(blob, xmlString);
        }

        public bool CheckIfSetExists(string setRef)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            return blob.Exists();
        }

        public void DeleteSet(string setRef)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            blob.Delete();
        }

        public string GetStaticData(string report)
        {
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient(report + ".xml");
            string xmlString = DownloadBlobToXMLString(blob);
            return xmlString;
        }




        // ** HELPFUL METHODS **
        private static string DownloadBlobToXMLString(BlobClient blob)
        {
            string xmlString = "";
            if (blob.Exists())
            {
                byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
                using (var ms = new MemoryStream(fileContent)) blob.DownloadTo(ms);
                xmlString = Encoding.UTF8.GetString(fileContent);
            }
            return xmlString;
        }

        private static void UploadXMLStringToBlob(BlobClient blob, string xmlString)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
            using (var ms = new MemoryStream(bytes)) blob.Upload(ms, true);
        }



    }
}
