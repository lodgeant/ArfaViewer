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
        //public static string LDrawImageInventoryUrl_Offical = "https://www.ldraw.org/library/official/images/parts/";
        //public static string LDrawImageInventoryUrl_Unoffical = "https://www.ldraw.org/library/unofficial/images/parts/";
        //public static string ElementURL = "https://m.rebrickable.com/media/parts/ldraw";



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

        public void DeleteSet(string setRef)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            blob.Delete();
        }

        public bool CheckIfSetExists(string setRef)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            return blob.Exists();
        }

        public bool CheckIfBlobExists(string containerName, string blobName)
        {
            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, containerName).GetBlobClient(blobName);
            return blob.Exists();
        }

        public string GetStaticData(string report)
        {
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient(report + ".xml");
            string xmlString = DownloadBlobToXMLString(blob);
            return xmlString;
        }

        public Bitmap GetImage(ImageType imageType, string[] _params)
        {
            Bitmap image = null;

            #region ** Determine variables **
            string itemRef = "";
            List<string> imageUrlList = new List<string>();
            if (imageType == ImageType.SET)
            {
                // params[0] = SetRef
                itemRef = _params[0];
                imageUrlList.Add("https://img.bricklink.com/ItemImage/ON/0/" + itemRef + ".png");
            }
            else if (imageType == ImageType.PARTCOLOUR)
            {
                // params[0] = LDrawColourID
                itemRef = _params[0];
                //imageUrl not used
            }
            else if (imageType == ImageType.ELEMENT)
            {
                // params[0] = LDrawRef
                // params[1] = LDrawColourID, 
                itemRef = _params[0] + "|" + _params[1];
                imageUrlList.Add("https://m.rebrickable.com/media/parts/ldraw/" + _params[1] + "/" + _params[0] + ".png");
            }
            else if (imageType == ImageType.LDRAW)
            {
                // params[0] = LDrawRef
                itemRef = _params[0];
                imageUrlList.Add("https://www.ldraw.org/library/official/images/parts/" + _params[0] + ".png");
                imageUrlList.Add("https://www.ldraw.org/library/unofficial/images/parts/" + _params[0] + ".png");
            }
            #endregion

            BlobClient blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
            if(blob.Exists())
            {
                image = DownloadBlobToBitmap(blob);
            }
            else
            {
                if (image == null)
                {
                    // ** If the image was not already in the Azure images, upload it to Azure for use in future **
                    // ** Download element image from source API **                    
                    byte[] imageb = null;
                    if (imageUrlList.Count == 0)
                    {
                        imageb = new WebClient().DownloadData(imageUrlList[0]);
                    }
                    else
                    {
                        foreach (string imageUrl in imageUrlList)
                        {
                            if (imageb == null)
                            {
                                try
                                {
                                    imageb = new WebClient().DownloadData(imageUrl);
                                }
                                catch { }
                            }
                        }
                    }
                    if (imageb.Length > 0)
                    {                        
                        // ** Upload the image to Azure **                        
                        BlobClient newBlob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
                        using (var ms = new MemoryStream(imageb))
                        {
                            newBlob.Upload(ms, true);
                            image = new Bitmap(ms);
                        }                        
                    }
                }
            }
            return image;
        }

       
        
        public void UploadImage()
        {
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

        private static Bitmap DownloadBlobToBitmap(BlobClient blob)
        {
            Bitmap image = null;
            byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
            using (var ms = new MemoryStream(fileContent))
            {
                blob.DownloadTo(ms);
                image = new Bitmap(ms);
            }
            return image;
        }


    }
}
