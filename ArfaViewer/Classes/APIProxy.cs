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
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "set-xmls").GetBlobClient(set.Ref + ".xml");
            string xmlString = set.SerializeToString(true);
            UploadXMLStringToBlob(blob, xmlString);
        }

        public void DeleteSet(string setRef)
        {
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            blob.Delete();
        }

        public bool CheckIfSetExists(string setRef)
        {
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "set-xmls").GetBlobClient(setRef + ".xml");
            return blob.Exists();
        }

        public bool CheckIfBlobExists(string containerName, string blobName)
        {
            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, containerName).GetBlobClient(blobName);
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

            BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
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
                        BlobClient newBlob = new BlobContainerClient(this.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
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

        public bool CheckIfBasePartExists(string LDrawRef)
        {
            bool exists = false;
            BasePartCollection coll = GetBasePartData_UsingLDrawRefList(new List<string>() { LDrawRef });
            if (coll.BasePartList.Count > 0) exists = true;
            return exists;
        }

        public string GetLDrawDescription_UsingLDrawRef(string LDrawRef)
        {
            string result = "";
            //try
            //{
                // ** Get data from BASEPART database table **
                String sql = "SELECT LDRAW_DESCRIPTION FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
                var results = GetSQLQueryResults(this.AzureDBConnString, sql);
                result = (string)results.Rows[0]["LDRAW_DESCRIPTION"];
                return result;
            //}
            //catch(Exception ex)
            //{
            //    return "ERROR|" + ex.Message;
            //}            
        }




        public SetDetailsCollection GetSetDetailsData_UsingSetRefList(List<string> IDList)
        {
            // ** Generate SetDetailsCollection from SET_DETAILS data in database **
            SetDetailsCollection coll = new SetDetailsCollection();
            if (IDList.Count > 0)
            {
                string sql = "SELECT ID,REF,DESCRIPTION,TYPE,THEME,SUB_THEME,YEAR,PART_COUNT,SUBSET_COUNT,MODEL_COUNT,MINIFIG_COUNT,STATUS,ASSIGNED_TO,INSTRUCTIONS FROM SET_DETAILS ";                
                sql += "WHERE REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
                var results = GetSQLQueryResults(this.AzureDBConnString, sql);
                coll = SetDetailsCollection.GetSetDetailsCollectionFromDataTable(results);
            }
            return coll;
        }



        public void UpdateSetDetailsInstructions_UsingSetRef(string setRef, string xmlString)
        {
            // check if set details already exist
            // if they exist, do an update
            // if they don't exist, do an insert



        }





            public CompositePartCollection GetCompositePartData_All()
        {
            // ** Generate BasePartCollection from xml data in Blob **
            //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("CompositePartCollection.xml");
            //string xmlString = DownloadBlobToXMLString(blob);
            //CompositePartCollection coll = new CompositePartCollection().DeserialiseFromXMLString(xmlString);

            // ** Generate CompositePartCollection from COMPOSITEPART data in database **
            string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            CompositePartCollection coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);

            return coll;
        }

        public CompositePartCollection GetCompositePartData_UsingLDrawRefList(List<string> IDList)
        {
            // ** Generate CompositePartCollection from COMPOSITEPART data in database **
            CompositePartCollection coll = new CompositePartCollection();
            if (IDList.Count > 0)
            {
                string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
                sql += "WHERE LDRAW_REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
                var results = GetSQLQueryResults(this.AzureDBConnString, sql);
                coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);
            }
            return coll;
        }

        public bool CheckIfCompositePartsExist(string LDrawRef)
        {
            bool exists = false;            
            String sql = "SELECT COUNT(ID) 'RESULT' FROM COMPOSITEPART WHERE PARENT_LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            int result = (int)results.Rows[0]["RESULT"];
            if(result > 0) exists = true;
            return exists;
        }

        public CompositePartCollection GetCompositePartData_UsingParentLDrawRefList(string ParentLDrawRef)
        {
            // ** Generate CompositePartCollection from COMPOSITEPART data in database **
            CompositePartCollection coll = new CompositePartCollection();            
            string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
            sql += "WHERE PARENT_LDRAW_REF='" + ParentLDrawRef + "'";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);            
            return coll;
        }






        public int GetLDrawColourID_UsingLDrawColourName(string LDrawColourName)
        {            
            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_ID FROM PARTCOLOUR WHERE LDRAW_COLOUR_NAME='" + LDrawColourName + "'";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            int result = (int)results.Rows[0]["LDRAW_COLOUR_ID"];
            return result;
        }

        public string GetLDrawColourName_UsingLDrawColourID(int LDrawColourID)
        {            
            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR WHERE LDRAW_COLOUR_ID=" + LDrawColourID;
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            string LDrawColourName = (string)results.Rows[0]["LDRAW_COLOUR_NAME"];
            return LDrawColourName;
        }

        public int GetLDrawSize_UsingLDrawRef(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            String sql = "SELECT LDRAW_SIZE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            int LDrawSize = (int)results.Rows[0]["LDRAW_SIZE"];
            return LDrawSize;
        }

        public string GetPartType_UsingLDrawRef(string LDrawRef)
        {
            // ** Get data from BASEPART database table **
            String sql = "SELECT PART_TYPE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            string partType = (string)results.Rows[0]["PART_TYPE"];
            return partType;
        }

        public List<string> GetAllLDrawColourNames()
        {
            List<string> partColourNameList = new List<string>();

            // ** Get data from PARTCOLOUR database table **
            String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR ORDER BY LDRAW_COLOUR_NAME";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);

            // ** Convert data into list **
            foreach (DataRow row in results.Rows)
            {
                partColourNameList.Add((string)row["LDRAW_COLOUR_NAME"]);
            }
            return partColourNameList;
        }

        public string GetLDrawFileDetails(string LDrawRef)
        {
            string value = "";
            try
            {               
                ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
                if (share.Exists() == false)
                {
                    share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
                    if (share.Exists() == false)
                    {
                        share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
                    }
                }
                if (share.Exists())
                {
                    //DateTime lastUpdatedUTC = share.GetProperties().Value.LastModified.UtcDateTime;
                    byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
                    Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
                    using (var ms = new MemoryStream(fileContent)) download.Content.CopyTo(ms);                    
                    value = Encoding.UTF8.GetString(fileContent);                    
                }               
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string GetLDrawDescription_FromLDrawFile(string LDrawRef)
        {
            string value = "";
            try
            {
                string LDrawFileText = GetLDrawFileDetails(LDrawRef);
                if (LDrawFileText != "")
                {
                    string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                    value = lines[0].Replace("0 ", "");
                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string GetPartType_FromLDrawFile(string LDrawRef)
        {
            string value = "BASIC";
            try
            {
                // ** Get LDraw details for part **
                string LDrawFileText = StaticData.GetLDrawFileDetails(LDrawRef);
                string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string fileLine in lines)
                {
                    // ** Check if part contains references to any other parts - if it does then the part is COMPOSITE
                    if (fileLine.Trim().StartsWith("1"))
                    {
                        string formattedLine = fileLine.Trim().Replace("   ", " ").Replace("  ", " ");
                        string[] DatLine = formattedLine.Split(' ');
                        string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "").Replace("s\\", "");
                        string SubPart_LDrawFileText = StaticData.GetLDrawFileDetails(SubPart_LDrawRef);
                        if (SubPart_LDrawFileText != "")
                        {
                            value = "COMPOSITE";
                            break;
                        }
                    }
                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public string GetLDrawPartType_FromLDrawFile(string LDrawRef)
        {
            string value = BasePart.LDrawPartType.UNKNOWN.ToString();
            try
            {
                // ** CHECK IF PART EXISTS IN OFFICIAL/UNOFFIAL LDRAW PARTS **            
                ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
                if (share.Exists())
                {
                    value = BasePart.LDrawPartType.OFFICIAL.ToString();
                }
                else
                {
                    share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
                    if (share.Exists())
                    {
                        value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
                    }
                    else
                    {
                        share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
                        if (share.Exists())
                        {
                            value = BasePart.LDrawPartType.UNOFFICIAL.ToString();
                        }
                    }
                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }

        public bool CheckIfLDrawFileDetailsExist(string LDrawRef)
        {
            //bool exists = false;
            ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
            if (share.Exists() == false)
            {
                share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
                if (share.Exists() == false)
                {
                    share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
                }
            }
            //if (share.Exists()) exists = true;            
            return share.Exists();
        }


        public CompositePartCollection GetAllCompositeSubParts_FromLDrawFile(string LDrawRef)
        {            
            CompositePartCollection coll = new CompositePartCollection();
            try
            {
                string ParentLDrawFileText = GetLDrawFileDetails(LDrawRef);
                string[] lines = ParentLDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string fileLine in lines)
                {                    
                    if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false)
                    {
                        string formattedLine = fileLine.Trim().Replace("   ", " ").Replace("  "," ");
                        string[] DatLine = formattedLine.Split(' ');
                        string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "");
                        int SubPart_LDrawColourID = int.Parse(DatLine[1]);
                        string subPart_LDrawDescription = GetLDrawDescription_FromLDrawFile(SubPart_LDrawRef);
                        if (SubPart_LDrawColourID == 16) SubPart_LDrawColourID = -1;    // Assumes that Sub Parts don't make reference to other sub parts. Would need to change this if any Sub Parts are "c"
                        coll.CompositePartList.Add(new CompositePart() { LDrawRef = SubPart_LDrawRef, LDrawColourID = SubPart_LDrawColourID, LDrawDescription = subPart_LDrawDescription });

                        //if (SubPart_LDrawRef.Contains("c0"))
                        //{
                        //    // LINE HAS REFERENCE TO ANOTHER PART WITH HAS SUB PARTS **
                        //    List<string> SubPartList2 = GetAllSubPartsForLDrawRef(SubPart_LDrawRef, SubPart_LDrawColourID);
                        //    SubPartList.AddRange(SubPartList2);
                        //}                       
                    }
                }
                return coll;
            }
            catch (Exception)
            {
                return coll;
            }
        }




        public void AddBasePart(BaseClasses.BasePart bp)
        {
            string sql;

            sql = "SELECT MAX(ID) 'RESULT' FROM BASEPART";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            int oldID = 0;              
            if(results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];                      
            int newID = oldID + 1;
            
            // ** Generate SQL Statement **
            sql = "INSERT INTO BASEPART" + Environment.NewLine;
            sql += "(ID,LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE)" + Environment.NewLine;
            sql += "VALUES" + Environment.NewLine;
            sql += "(";
            sql += newID + ",";
            sql += "'" + bp.LDrawRef + "',";
            sql += "'" + bp.LDrawDescription + "',";
            sql += "'" + bp.LDrawCategory + "',";
            sql += bp.LDrawSize + ",";            
            sql += bp.OffsetX + ",";
            sql += bp.OffsetY + ",";
            sql += bp.OffsetZ + ",";
            sql += "'" + bp.IsSubPart + "',";
            sql += "'" + bp.IsSticker + "',";
            sql += "'" + bp.IsLargeModel + "',";
            sql += "'" + bp.partType + "',";
            sql += "'" + bp.lDrawPartType + "'";               
            sql += ")";

            // ** Execute SQL statement **
            ExecuteSQLStatement(this.AzureDBConnString, sql);

            #region ** UPLOAD UPDATED BasePartCollection TO AZURE AND LOCAL CACHE **
            //xmlString = bpc.SerializeToString(true);
            //byte[] bytes = Encoding.UTF8.GetBytes(xmlString);                
            //blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
            //using (var ms = new MemoryStream(bytes))
            //{
            //    blob.Upload(ms, true);
            //}
            ////Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
            #endregion

            #region ** ADD NEW .dat FILE FOR PART ** 
            //string line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + LDrawRef + ".dat" + Environment.NewLine;
            //bytes = Encoding.UTF8.GetBytes(line);
            //ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
            //share.Create(bytes.Length);
            //using (MemoryStream ms = new MemoryStream(bytes))
            //{
            //    share.Upload(ms);
            //}
            #endregion

        }

        public void AddCompositePart(BaseClasses.CompositePart cp)
        {
            string sql;

            sql = "SELECT MAX(ID) 'RESULT' FROM COMPOSITEPART";
            var results = GetSQLQueryResults(this.AzureDBConnString, sql);
            int oldID = 0;
            if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
            int newID = oldID + 1;

            // ** Generate SQL Statement **
            sql = "INSERT INTO COMPOSITEPART" + Environment.NewLine;
            sql += "(ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z)" + Environment.NewLine;
            sql += "VALUES" + Environment.NewLine;
            sql += "(";
            sql += newID + ",";
            sql += "'" + cp.LDrawRef + "',";
            sql += "'" + cp.LDrawDescription + "',";
            sql += "'" + cp.ParentLDrawRef + "',";
            sql += cp.LDrawColourID + ",";
            sql += cp.PosX + ",";
            sql += cp.PosY + ",";
            sql += cp.PosZ + ",";
            sql += cp.RotX + ",";
            sql += cp.RotY + ",";
            sql += cp.RotZ;
            sql += ")";

            // ** Execute SQL statement **
            ExecuteSQLStatement(this.AzureDBConnString, sql);


            //    #region ** ADD NEW .dat FILE FOR SUB PART ** 
            //    //line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + SubPart_LDrawRef + ".dat" + Environment.NewLine;
            //    //bytes = Encoding.UTF8.GetBytes(line);
            //    //share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + SubPart_LDrawRef + ".dat");
            //    //share.Create(bytes.Length);
            //    //using (MemoryStream ms = new MemoryStream(bytes))
            //    //{
            //    //    share.Upload(ms);
            //    //}
            //    #endregion

            //    #region ** CREATE Composite Part DAT file **
            //    string LDrawFileText = GetLDrawFileDetails(LDrawRef);                    
            //    string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            //    string DAT_String = "";
            //    foreach (string fileLine in lines)
            //    {
            //        if (fileLine.StartsWith("1"))
            //        {
            //            DAT_String += fileLine.Replace("1 16 ", "1 450 ") + Environment.NewLine;
            //        }
            //    }
            //    bytes = Encoding.UTF8.GetBytes(DAT_String);
            //    share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
            //    share.Create(bytes.Length);
            //    using (var ms = new MemoryStream(bytes))
            //    {
            //        share.Upload(ms);                        
            //    }
            //    #endregion


        }



        //public long GetFBXSize(string LDrawRef)
        //{
        //    // All BASIC parts have their own FBX file.
        //    // All COMPOSITIE parts are made up of their child FBX parts

        //    // ** Check Part Type **
        //    long fbxSize = 0;
        //    string partType = GetPartType_FromLDrawFile(LDrawRef);
        //    if (partType.Equals("BASIC"))
        //    {
        //        ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(LDrawRef + ".fbx");
        //        if (share.Exists()) fbxSize = share.GetProperties().Value.ContentLength;
        //    }
        //    else if (partType.Equals("COMPOSITE"))
        //    {
        //        // Get all sub part Composite parts
        //        int fbxFoundCount = 0;
        //        CompositePartCollection coll = GetAllCompositeSubParts_FromLDrawFile(LDrawRef);
        //        foreach(CompositePart cp in coll.CompositePartList)
        //        {
        //            ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(cp.LDrawRef + ".fbx");
        //            if (share.Exists())
        //            {
        //                fbxSize += share.GetProperties().Value.ContentLength;
        //                fbxFoundCount += 1;
        //            }                
        //        }
        //    }
        //    return fbxSize;
        //}

        //public bool CheckIfAllFBXExist(string LDrawRef)
        //{
        //    bool exists = false;
        //    string partType = GetPartType_FromLDrawFile(LDrawRef);
        //    if (partType.Equals("BASIC"))
        //    {
        //        ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(LDrawRef + ".fbx");
        //        if (share.Exists()) exists = true;                             
        //    }
        //    else if (partType.Equals("COMPOSITE"))
        //    {
        //        // Get all sub part Composite parts
        //        int fbxFoundCount = 0;
        //        CompositePartCollection coll = GetAllCompositeSubParts_FromLDrawFile(LDrawRef);
        //        foreach (CompositePart cp in coll.CompositePartList)
        //        {
        //            ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(cp.LDrawRef + ".fbx");
        //            if (share.Exists()) fbxFoundCount += 1;                    
        //        }
        //        if(coll.CompositePartList.Count == fbxFoundCount) exists = true;               
        //    }
        //    return exists;
        //}

        //public int GetFBXCount(string LDrawRef)
        //{
        //    int fbxFoundCount = 0;
        //    string partType = GetPartType_FromLDrawFile(LDrawRef);
        //    if (partType.Equals("BASIC"))
        //    {
        //        ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(LDrawRef + ".fbx");
        //        if (share.Exists()) fbxFoundCount += 1;
        //    }
        //    else if (partType.Equals("COMPOSITE"))
        //    {
        //        // Get all sub part Composite parts
        //        CompositePartCollection coll = GetAllCompositeSubParts_FromLDrawFile(LDrawRef);
        //        foreach (CompositePart cp in coll.CompositePartList)
        //        {
        //            ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(cp.LDrawRef + ".fbx");
        //            if (share.Exists()) fbxFoundCount += 1;
        //        }                
        //    }
        //    return fbxFoundCount;
        //}

        public FBXDetails GetFBXDetails(string LDrawRef)
        {
            

            // All BASIC parts have their own FBX file.
            // All COMPOSITIE parts are made up of their child FBX parts

            // ** Check Part Type **
            long fbxSize = 0;
            bool allFBXExist = false;
            int fbxFoundCount = 0;
            string partType = GetPartType_FromLDrawFile(LDrawRef);
            if (partType.Equals("BASIC"))
            {
                ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(LDrawRef + ".fbx");
                if (share.Exists())
                {
                    fbxSize = share.GetProperties().Value.ContentLength;
                    allFBXExist = true;
                    fbxFoundCount += 1;
                }
            }
            else if (partType.Equals("COMPOSITE"))
            {
                // Get all sub part Composite parts
                //int fbxFoundCount = 0;
                CompositePartCollection coll = GetAllCompositeSubParts_FromLDrawFile(LDrawRef);
                foreach (CompositePart cp in coll.CompositePartList)
                {
                    ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(cp.LDrawRef + ".fbx");
                    if (share.Exists())
                    {
                        fbxSize += share.GetProperties().Value.ContentLength;
                        fbxFoundCount += 1;
                    }
                }
                if (coll.CompositePartList.Count == fbxFoundCount) allFBXExist = true;
            }


            FBXDetails fbxDetails = new FBXDetails();
            fbxDetails.fbxSize = fbxSize;
            fbxDetails.allFBXExist = allFBXExist;
            fbxDetails.fbxCount = fbxFoundCount;



            return fbxDetails;
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

        private static void ExecuteSQLStatement(string AzureDBConnString, string sql)
        {
            var results = new DataTable();
            using (SqlConnection connection = new SqlConnection(AzureDBConnString))
            {
                using (SqlCommand command = new SqlCommand(sql, connection))
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }            
        }




    }
}
