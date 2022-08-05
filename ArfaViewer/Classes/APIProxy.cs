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
using Microsoft.Data.SqlClient;



namespace Generator
{
    public class APIProxy
    {
        public string AzureStorageConnString;
        //public static string LDrawImageInventoryUrl_Offical = "https://www.ldraw.org/library/official/images/parts/";
        //public static string LDrawImageInventoryUrl_Unoffical = "https://www.ldraw.org/library/unofficial/images/parts/";
        //public static string ElementURL = "https://m.rebrickable.com/media/parts/ldraw";
        public string AzureDBConnString;


        public APIProxy(string AzureStorageConnString, string AzureDBConnString)
        {
            this.AzureStorageConnString = AzureStorageConnString;
            this.AzureDBConnString = AzureDBConnString;
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

        //public string GetStaticData(string report)
        //{
        //    BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient(report + ".xml");
        //    string xmlString = DownloadBlobToXMLString(blob);
        //    return xmlString;
        //}

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
                    if (imageb != null && imageb.Length > 0)
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

        public void UploadImage()   // Not used yet.
        {
        }

        public PartColourCollection GetPartColourData_All()
        {
            // ** Generate PartColourCollection from xml data in Blob **
            //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("PartColourCollection.xml");
            //string xmlString = DownloadBlobToXMLString(blob);
            //PartColourCollection coll = new PartColourCollection().DeserialiseFromXMLString(xmlString);

            // ** Generate PartColourCollection from PARTCOLOUR data in database **
            String sql = "SELECT LDRAW_COLOUR_ID,LDRAW_COLOUR_NAME,LDRAW_COLOUR_HEX,LDRAW_COLOUR_ALPHA FROM PARTCOLOUR";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            PartColourCollection coll = PartColourCollection.GetPartColourCollectionFromDataTable(results);

            return coll;
        }

        public PartColourCollection GetPartColourData_UsingLDrawColourIDList(List<int> IDList)
        {
            // ** Generate PartColourCollection from PARTCOLOUR data in database **
            PartColourCollection coll = new PartColourCollection();
            if (IDList.Count > 0)
            {
                string sql = "SELECT LDRAW_COLOUR_ID,LDRAW_COLOUR_NAME,LDRAW_COLOUR_HEX,LDRAW_COLOUR_ALPHA FROM PARTCOLOUR ";
                sql += "WHERE LDRAW_COLOUR_ID IN (" + string.Join(",", IDList) + ")";
                var results = GetSQLQueryResults(this.AzureDBConnString, sql);
                coll = PartColourCollection.GetPartColourCollectionFromDataTable(results);
            }
            return coll;
        }

        public BasePartCollection GetBasePartData_All()
        {
            // ** Generate BasePartCollection from xml data in Blob **
            //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
            //string xmlString = DownloadBlobToXMLString(blob);
            //BasePartCollection coll = new BasePartCollection().DeserialiseFromXMLString(xmlString);

            // ** Generate BasePartCollection from BASEPART data in database **
            String sql = "SELECT LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE FROM BASEPART";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            BasePartCollection coll = BasePartCollection.GetBasePartCollectionFromDataTable(results);

            return coll;
        }

        public BasePartCollection GetBasePartData_UsingLDrawRefList(List<string> IDList)
        {
            // ** Generate BasePartCollection from BASEPART data in database **
            BasePartCollection coll = new BasePartCollection();
            if (IDList.Count > 0)
            {
                string sql = "SELECT LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE FROM BASEPART ";
                sql += "WHERE LDRAW_REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
                var results = GetSQLQueryResults(this.AzureDBConnString, sql);
                coll = BasePartCollection.GetBasePartCollectionFromDataTable(results);
            }
            return coll;
        }



        public CompositePartCollection GetCompositePartData_All()
        {
            // ** Generate BasePartCollection from xml data in Blob **
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("CompositePartCollection.xml");
            string xmlString = DownloadBlobToXMLString(blob);
            CompositePartCollection coll = new CompositePartCollection().DeserialiseFromXMLString(xmlString);

            // ** Generate CompositePartCollection from COMPOSITEPART data in database **
            //String sql = "SELECT LDRAW_COLOUR_ID,LDRAW_COLOUR_NAME,LDRAW_COLOUR_HEX,LDRAW_COLOUR_ALPHA FROM PARTCOLOUR";
            //var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            //BasePartCollection coll = BasePartCollection.GetPartColourCollectionFromDataTable(results);

            return coll;
        }

        public bool CheckIfBasePartExists(string LDrawRef)
        {
            bool exists = false;
            BasePartCollection coll = GetBasePartData_UsingLDrawRefList(new List<string>() { LDrawRef });
            if (coll.BasePartList.Count > 0) exists = true;
            return exists;
        }

        public string GetLDrawDescription_UsingLDrawRef(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            String sql = "SELECT LDRAW_DESCRIPTION FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            string result = (string)results.Rows[0]["LDRAW_DESCRIPTION"];
            return result;
        }

        public int GetLDrawColourID_UsingLDrawColourName(string LDrawColourName)
        {            
            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_ID FROM PARTCOLOUR WHERE LDRAW_COLOUR_NAME='" + LDrawColourName + "'";
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            int result = (int)results.Rows[0]["LDRAW_COLOUR_ID"];
            return result;
        }

        public string GetLDrawColourName_UsingLDrawColourID(int LDrawColourID)
        {            
            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR WHERE LDRAW_COLOUR_ID=" + LDrawColourID;
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            string LDrawColourName = (string)results.Rows[0]["LDRAW_COLOUR_NAME"];
            return LDrawColourName;
        }

        public int GetLDrawSize_UsingLDrawRef(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            String sql = "SELECT LDRAW_SIZE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            int LDrawSize = (int)results.Rows[0]["LDRAW_SIZE"];
            return LDrawSize;
        }

        public string GetPartType_UsingLDrawRef(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            String sql = "SELECT PART_TYPE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);
            string partType = (string)results.Rows[0]["PART_TYPE"];
            return partType;
        }

        public List<string> GetAllLDrawColourNames()
        {
            List<string> partColourNameList = new List<string>();

            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR ORDER BY LDRAW_COLOUR_NAME";
            var results = GetSQLQueryResults(Global_Variables.AzureDBConnString, sql);

            // ** Convert data into list **
            foreach (DataRow row in results.Rows)
            {
                partColourNameList.Add((string)row["LDRAW_COLOUR_NAME"]);
            }
            return partColourNameList;
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

        private static DataTable GetSQLQueryResults(string AzureDBConnString, string sql)
        {
            var results = new DataTable();
            using (SqlConnection connection = new SqlConnection(AzureDBConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader()) results.Load(reader);
                }
            }
            return results;
        }


    }
}
