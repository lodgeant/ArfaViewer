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
    public class zzz_APIProxy
    {
        // string UnityLegoPartPath = @"C:\Unity Projects\Lego Unity Viewer\Assets\Resources\Lego Part Models";    // Used for SyncFBXFiles
        //private string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";
        //private string AzureDBConnString = "Server=tcp:arfa-db.database.windows.net,1433;Initial Catalog=ArfaDB;Persist Security Info=False;User ID=lodgeant;Password=Sammy_Lodge123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        //private string RebrickableKey = "856437d0f14f81e4d3356d27bf1b419e";




        // ** Image Functions **

        //public Bitmap GetImage(ImageType imageType, string[] _params)
        //{
        //    Bitmap image = null;

        //    #region ** Determine variables **
        //    string itemRef = "";
        //    List<string> imageUrlList = new List<string>();
        //    if (imageType == ImageType.SET)
        //    {
        //        // params[0] = SetRef
        //        itemRef = _params[0];
        //        imageUrlList.Add("https://img.bricklink.com/ItemImage/ON/0/" + itemRef + ".png");
        //        imageUrlList.Add("https://img.bricklink.com/ItemImage/MN/0/" + itemRef + ".png");

        //        //https://images.brickset.com/sets/large/1278-1.jpg

        //    }
        //    else if (imageType == ImageType.PARTCOLOUR)
        //    {
        //        // params[0] = LDrawColourID
        //        itemRef = _params[0];
        //        //imageUrl not used
        //    }
        //    else if (imageType == ImageType.ELEMENT)
        //    {
        //        // params[0] = LDrawRef
        //        // params[1] = LDrawColourID, 
        //        itemRef = _params[0] + "|" + _params[1];
        //        imageUrlList.Add("https://m.rebrickable.com/media/parts/ldraw/" + _params[1] + "/" + _params[0] + ".png");
        //    }
        //    else if (imageType == ImageType.LDRAW)
        //    {
        //        // params[0] = LDrawRef
        //        itemRef = _params[0];
        //        imageUrlList.Add("https://www.ldraw.org/library/official/images/parts/" + _params[0] + ".png");
        //        imageUrlList.Add("https://www.ldraw.org/library/unofficial/images/parts/" + _params[0] + ".png");
        //    }
        //    else if (imageType == ImageType.THEME)
        //    {
        //        // params[0] = Theme + | + SubTheme
        //        itemRef = _params[0];
        //        //imageUrl not used
        //    }
        //    #endregion

        //    #region ** Process image **
        //    BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
        //    if (blob.Exists())
        //    {
        //        image = DownloadBlobToBitmap(blob);
        //    }
        //    else
        //    {
        //        if (image == null)
        //        {
        //            // ** If the image was not already in the Azure images, upload it to Azure for use in future **
        //            // ** Download element image from source API **                    
        //            byte[] imageb = null;
        //            //if (imageUrlList.Count == 0)
        //            if (imageUrlList.Count == 1)
        //            {
        //                //imageb = new WebClient().DownloadData(imageUrlList[0]);
        //                try
        //                {
        //                    imageb = new WebClient().DownloadData(imageUrlList[0]);
        //                }
        //                catch { }
        //            }
        //            else
        //            {
        //                foreach (string imageUrl in imageUrlList)
        //                {
        //                    if (imageb == null)
        //                    {
        //                        try
        //                        {
        //                            imageb = new WebClient().DownloadData(imageUrl);
        //                        }
        //                        catch { }
        //                    }
        //                }
        //            }
        //            if (imageb != null && imageb.Length > 0)
        //            {
        //                // ** Upload the image to Azure **                        
        //                BlobClient newBlob = new BlobContainerClient(this.AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
        //                using (var ms = new MemoryStream(imageb))
        //                {
        //                    newBlob.Upload(ms, true);
        //                    image = new Bitmap(ms);
        //                }
        //            }
        //        }
        //    }
        //    #endregion


        //    return image;
        //}

        //private static Bitmap DownloadBlobToBitmap(BlobClient blob)
        //{
        //    Bitmap image = null;
        //    byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
        //    using (var ms = new MemoryStream(fileContent))
        //    {
        //        blob.DownloadTo(ms);
        //        image = new Bitmap(ms);
        //    }
        //    return image;
        //}


        // ** Functions - MOVED TO API **
        // ** PartColour Functions **

        //public PartColourCollection GetPartColourData_All_OLD()
        //{
        //    // ** Generate PartColourCollection from xml data in Blob **
        //    //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("PartColourCollection.xml");
        //    //string xmlString = DownloadBlobToXMLString(blob);
        //    //PartColourCollection coll = new PartColourCollection().DeserialiseFromXMLString(xmlString);

        //    // ** Generate PartColourCollection from PARTCOLOUR data in database **
        //    String sql = "SELECT LDRAW_COLOUR_ID,LDRAW_COLOUR_NAME,LDRAW_COLOUR_HEX,LDRAW_COLOUR_ALPHA FROM PARTCOLOUR";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    PartColourCollection coll = PartColourCollection.GetPartColourCollectionFromDataTable(results);

        //    return coll;
        //}

        //public PartColourCollection GetPartColourData_All()
        //{           
        //    string url = "https://arfabrickviewer.azurewebsites.net/source/GetPartColourData_All";
        //    string JSONString = GetJSONResponseFromURL(url);
        //    PartColourCollection coll = Newtonsoft.Json.JsonConvert.DeserializeObject<PartColourCollection>(JSONString);
        //    return coll;
        //}

        //public PartColourCollection GetPartColourData_UsingLDrawColourIDList(List<int> IDList)
        //{
        //    // ** Generate PartColourCollection from PARTCOLOUR data in database **
        //    PartColourCollection coll = new PartColourCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT LDRAW_COLOUR_ID,LDRAW_COLOUR_NAME,LDRAW_COLOUR_HEX,LDRAW_COLOUR_ALPHA FROM PARTCOLOUR ";
        //        sql += "WHERE LDRAW_COLOUR_ID IN (" + string.Join(",", IDList) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = PartColourCollection.GetPartColourCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public int GetLDrawColourID_UsingLDrawColourName(string LDrawColourName)
        //{
        //    // ** Get data from PARTCOLOUR database table **
        //    String sql = "SELECT LDRAW_COLOUR_ID FROM PARTCOLOUR WHERE LDRAW_COLOUR_NAME='" + LDrawColourName + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int result = (int)results.Rows[0]["LDRAW_COLOUR_ID"];
        //    return result;            
        //}

        //public string GetLDrawColourName_UsingLDrawColourID(int LDrawColourID)
        //{
        //    // ** Get data from PARTCOLOUR database table **
        //    String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR WHERE LDRAW_COLOUR_ID=" + LDrawColourID;
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    string LDrawColourName = (string)results.Rows[0]["LDRAW_COLOUR_NAME"];
        //    return LDrawColourName;
        //}

        //public List<string> GetAllLDrawColourNames()
        //{
        //    List<string> partColourNameList = new List<string>();

        //    // ** Get data from PARTCOLOUR database table **
        //    String sql = "SELECT LDRAW_COLOUR_NAME FROM PARTCOLOUR ORDER BY LDRAW_COLOUR_NAME";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);

        //    // ** Convert data into list **
        //    foreach (DataRow row in results.Rows)
        //    {
        //        partColourNameList.Add((string)row["LDRAW_COLOUR_NAME"]);
        //    }
        //    return partColourNameList;
        //}

        // ** SetDetails Functions **

        //public SetDetailsCollection GetSetDetailsData_UsingSetRefList(List<string> IDList)
        //{
        //    // ** Generate SetDetailsCollection from SET_DETAILS data in database **
        //    SetDetailsCollection coll = new SetDetailsCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT ID,REF,DESCRIPTION,TYPE,THEME,SUB_THEME,YEAR,PART_COUNT,SUBSET_COUNT,MODEL_COUNT,MINIFIG_COUNT,STATUS,ASSIGNED_TO,INSTRUCTIONS,INSTRUCTION_REFS FROM SET_DETAILS ";                
        //        sql += "WHERE REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = SetDetailsCollection.GetSetDetailsCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public SetDetailsCollection GetSetDetailsData_UsingThemeAndSubTheme(string theme, string subTheme)
        //{
        //    // ** Generate SetDetailsCollection from SET_DETAILS data in database **
        //    SetDetailsCollection coll = new SetDetailsCollection();            
        //    string sql = "SELECT ID,REF,DESCRIPTION,TYPE,THEME,SUB_THEME,YEAR,PART_COUNT,SUBSET_COUNT,MODEL_COUNT,MINIFIG_COUNT,STATUS,ASSIGNED_TO,INSTRUCTIONS,INSTRUCTION_REFS FROM SET_DETAILS ";
        //    sql += "WHERE THEME='" + theme.Replace("'", "''") + "'";
        //    if(subTheme != "") sql += " AND SUB_THEME='" + subTheme.Replace("'", "''") + "'";           
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    coll = SetDetailsCollection.GetSetDetailsCollectionFromDataTable(results);            
        //    return coll;
        //}



        //public void UpdateSetDetailsCounts_UsingSetRef(string SetRef, int PartCount, int SubSetCount, int ModelCount, int MiniFigCount)
        //{
        //    // check if set details exist, if they exist, do an update, if not do nothing.
        //    SetDetailsCollection sdc = GetSetDetailsData_UsingSetRefList(new List<string>() { SetRef });
        //    if (sdc.SetDetailsList.Count == 1)
        //    {
        //        // ** Generate SQL Statement **
        //        string sql = "UPDATE SET_DETAILS SET" + Environment.NewLine;
        //        sql += "PART_COUNT = " + PartCount + "," + Environment.NewLine;
        //        sql += "SUBSET_COUNT = " + SubSetCount + "," + Environment.NewLine;
        //        sql += "MODEL_COUNT = " + ModelCount + "," + Environment.NewLine;
        //        sql += "MINIFIG_COUNT = " + MiniFigCount + Environment.NewLine;
        //        sql += "WHERE REF='" + SetRef + "'" + Environment.NewLine;

        //        // ** Execute SQL statement **
        //        ExecuteSQLStatement(this.AzureDBConnString, sql);
        //    }
        //}

        //public void AddSetDetails(BaseClasses.SetDetails sd)
        //{
        //    string sql;

        //    sql = "SELECT MAX(ID) 'RESULT' FROM SET_DETAILS";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int oldID = 0;
        //    if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
        //    int newID = oldID + 1;

        //    // ** Generate SQL Statement **
        //    sql = "INSERT INTO SET_DETAILS" + Environment.NewLine;
        //    sql += "(ID,REF,DESCRIPTION,TYPE,THEME,SUB_THEME,YEAR,PART_COUNT,SUBSET_COUNT,MODEL_COUNT,MINIFIG_COUNT,STATUS,ASSIGNED_TO,INSTRUCTIONS,INSTRUCTION_REFS)" + Environment.NewLine;
        //    sql += "VALUES" + Environment.NewLine;
        //    sql += "(";
        //    sql += newID + ",";
        //    sql += "'" + sd.Ref + "',";
        //    sql += "'" + sd.Description.Replace("'", "''") + "',";
        //    sql += "'" + sd.Type + "',";
        //    sql += "'" + sd.Theme.Replace("'","''") + "',";
        //    sql += "'" + sd.SubTheme.Replace("'", "''") + "',";
        //    sql += sd.Year + ",";
        //    sql += sd.PartCount + ",";
        //    sql += sd.SubSetCount + ",";
        //    sql += sd.ModelCount + ",";
        //    sql += sd.MiniFigCount + ",";
        //    sql += "'" + sd.Status + "',";
        //    sql += "'" + sd.AssignedTo + "',";
        //    sql += "'" + sd.Instructions.Replace("'", "''") + "',";
        //    sql += "'" + String.Join(",", sd.InstructionRefList) + "'";
        //    sql += ")";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        //public void DeleteSetDetails(string SetRef)
        //{
        //    // ** Generate SQL Statement **
        //    string sql = "DELETE FROM SET_DETAILS WHERE REF='" + SetRef + "'" + Environment.NewLine;

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        //public void UpdateSetDetails(BaseClasses.SetDetails sd)
        //{
        //    // ** Generate SQL Statement **
        //    string sql = "UPDATE SET_DETAILS SET " + Environment.NewLine;
        //    sql += "DESCRIPTION='" + sd.Description.Replace("'", "''") + "',";
        //    sql += "TYPE='" + sd.Type + "',";            
        //    sql += "THEME='" + sd.Theme.Replace("'", "''") + "',";
        //    sql += "SUB_THEME='" + sd.SubTheme.Replace("'", "''") + "',";
        //    sql += "YEAR=" + sd.Year + ",";
        //    sql += "PART_COUNT=" + sd.PartCount + ",";
        //    sql += "SUBSET_COUNT=" + sd.SubSetCount + ",";
        //    sql += "MODEL_COUNT=" + sd.ModelCount + ",";
        //    sql += "MINIFIG_COUNT=" + sd.MiniFigCount + ",";
        //    sql += "STATUS='" + sd.Status + "',";
        //    sql += "ASSIGNED_TO='" + sd.AssignedTo + "',";
        //    sql += "INSTRUCTIONS='" + sd.Instructions.Replace("'", "''") + "',";
        //    sql += "INSTRUCTION_REFS='" + String.Join(",", sd.InstructionRefList) + "'";
        //    sql += " WHERE REF='" + sd.Ref + "'";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        //public bool CheckIfPDFInstructionsExistForSet(string setRef)
        //{
        //    ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-instructions").GetFileClient(setRef + ".pdf");
        //    return share.Exists();
        //}

        //public bool CheckIfSetDetailExists(string setRef)
        //{
        //    bool exists = false;
        //    SetDetailsCollection coll = GetSetDetailsData_UsingSetRefList(new List<string>() { setRef });
        //    if (coll.SetDetailsList.Count > 0) exists = true;
        //    return exists;
        //}

        //public void UpdateSetDetailsInstructions_UsingSetRef(string setRef, string xmlString)
        //{
        //    // check if set details exist, if they exist, do an update, if not do nothing.
        //    SetDetailsCollection sdc = StaticData.GetSetDetailsData_UsingSetRefList(new List<string>() { setRef });
        //    if (sdc.SetDetailsList.Count == 1)
        //    {
        //        // ** Generate SQL Statement **
        //        string sql = "UPDATE SET_DETAILS" + Environment.NewLine;
        //        sql += "SET INSTRUCTIONS = '" + xmlString + "'" + Environment.NewLine;
        //        sql += "WHERE REF='" + setRef + "'" + Environment.NewLine;

        //        // ** Execute SQL statement **
        //        ExecuteSQLStatement(this.AzureDBConnString, sql);
        //    }
        //}

        // ** FBXDetails functions **

        //public FBXDetailsCollection GetFBXDetailsData_UsingLDrawRefList(List<string> IDList)
        //{
        //    // All BASIC parts have their own FBX file.
        //    // All COMPOSITIE parts are made up of their child FBX parts

        //    // ** Get BasePart Details for all parts (Upfront) **
        //    // THIS NEEDS TO BE ADDED

        //    // ** Generate BasePartCollection from BASEPART data in database **
        //    FBXDetailsCollection coll = new FBXDetailsCollection();
        //    if (IDList.Count > 0)
        //    {
        //        foreach(string LDrawRef in IDList)
        //        {
        //            // ** Get partType for Part **
        //            string partType = GetPartType_UsingLDrawRef(LDrawRef);

        //            // ** Check Part Type **
        //            long fbxSize = 0;
        //            bool allFBXExist = false;
        //            int fbxFoundCount = 0;
        //            if (partType.Equals("BASIC"))
        //            {
        //                ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(LDrawRef + ".fbx");
        //                if (share.Exists())
        //                {
        //                    fbxSize = share.GetProperties().Value.ContentLength;
        //                    allFBXExist = true;
        //                    fbxFoundCount += 1;
        //                }
        //            }
        //            else if (partType.Equals("COMPOSITE"))
        //            {
        //                // Get all sub part Composite parts
        //                CompositePartCollection cpc = GetAllCompositeSubParts_FromLDrawDetails(LDrawRef);
        //                foreach (CompositePart cp in cpc.CompositePartList)
        //                {
        //                    ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-fbx").GetFileClient(cp.LDrawRef + ".fbx");
        //                    if (share.Exists())
        //                    {
        //                        fbxSize += share.GetProperties().Value.ContentLength;
        //                        fbxFoundCount += 1;
        //                    }
        //                }
        //                if (cpc.CompositePartList.Count == fbxFoundCount) allFBXExist = true;
        //            }

        //            // ** Generate FBXDetails object and return **
        //            FBXDetails fbxDetails = new FBXDetails();
        //            fbxDetails.FBXSize = fbxSize;
        //            fbxDetails.AllFBXExist = allFBXExist;
        //            fbxDetails.FBXCount = fbxFoundCount;
        //            coll.FBXDetailsList.Add(fbxDetails);
        //        }
        //    }
        //    return coll;
        //}

        // ** LDraw Details (File) Functions **

        //public LDrawDetailsCollection GetLDrawDetailsData_UsingLDrawRefList(List<string> IDList)
        //{
        //    // ** Generate LDrawDetailsCollection from LDRAW_DETAILS data in database **
        //    LDrawDetailsCollection coll = new LDrawDetailsCollection();
        //    if (IDList.Count > 0)
        //    {
        //        // Check if entry already exists, if yes, add it, if not create and add it
        //        // ** Try and get all the data from the database
        //        string sql = "SELECT LDRAW_REF,LDRAW_DESCRIPTION,PART_TYPE,LDRAW_PART_TYPE,SUB_PART_COUNT,DATA,SUB_PART_LDRAW_REF_LIST FROM LDRAW_DETAILS ";
        //        sql += "WHERE LDRAW_REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = LDrawDetailsCollection.GetLDrawDetailsCollectionFromDataTable(results);

        //        // ** Check if LDrawDetails are available in DB - if not download from FILESHARE **
        //        // ** Check if all data was got from DB **
        //        if (coll.LDrawDetailsList.Count != IDList.Count)
        //        {
        //            // ** Get difference between found refs and database refs **
        //            List<string> foundRefs = new List<string>();
        //            foreach (LDrawDetails ldd in coll.LDrawDetailsList) foundRefs.Add(ldd.LDrawRef);
        //            List<string> firstDiffSecond = IDList.Except(foundRefs).ToList();

        //            // Cycle through difs and add them to the database
        //            foreach (string LDrawRef in IDList)
        //            {
        //                LDrawDetails lDrawDetails = GetLDrawDetails_FromLDrawFile(LDrawRef);
        //                if(lDrawDetails != null)
        //                {
        //                    AddLDrawDetails(lDrawDetails);
        //                    coll.LDrawDetailsList.Add(lDrawDetails);
        //                }                       
        //            }
        //        }

        //        // ** Check to esnure that all ietsm have been generated **
        //        if (coll.LDrawDetailsList.Count != IDList.Count) coll = new LDrawDetailsCollection();               
        //    }
        //    return coll;
        //}

        //public LDrawDetails GetLDrawDetails_FromLDrawFile(string LDrawRef)
        //{
        //    LDrawDetails ldD = null;
        //    try
        //    {
        //        string LDrawPartType = "";
        //        ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(LDrawRef + ".dat");
        //        LDrawPartType = BasePart.LDrawPartType.OFFICIAL.ToString();
        //        if (share.Exists() == false)
        //        {
        //            share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(LDrawRef + ".dat");
        //            LDrawPartType = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //            if (share.Exists() == false)
        //            {
        //                share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(LDrawRef + ".dat");
        //                LDrawPartType = BasePart.LDrawPartType.UNOFFICIAL.ToString();
        //            }
        //        }
        //        if (share.Exists())
        //        {
        //            //DateTime lastUpdatedUTC = share.GetProperties().Value.LastModified.UtcDateTime;
        //            byte[] fileContent = new byte[share.GetProperties().Value.ContentLength];
        //            Azure.Storage.Files.Shares.Models.ShareFileDownloadInfo download = share.Download();
        //            using (var ms = new MemoryStream(fileContent)) download.Content.CopyTo(ms);
        //            string LDrawFileText = Encoding.UTF8.GetString(fileContent);

        //            // ** Get Sub Part Ref List ** 
        //            List<string> SubPartLDrawRefList = GetSubPartLDrawRefListFromLDrawFileText(LDrawFileText);

        //            // ** Update LDraw Details object **
        //            ldD = new LDrawDetails();
        //            ldD.LDrawRef = LDrawRef;
        //            ldD.LDrawDescription = LDrawDetails.GetLDrawDescriptionFromLDrawFileText(LDrawFileText);                    
        //            ldD.SubPartCount = SubPartLDrawRefList.Count;                    
        //            ldD.SubPartLDrawRefList = SubPartLDrawRefList;
        //            ldD.PartType = "BASIC";
        //            if (ldD.SubPartCount > 0) ldD.PartType = "COMPOSITE";
        //            ldD.LDrawPartType = LDrawPartType;
        //            ldD.Data = LDrawFileText;                    
        //        }
        //        return ldD;
        //    }
        //    catch (Exception)
        //    {
        //        return ldD;
        //    }
        //}

        //public CompositePartCollection GetAllCompositeSubParts_FromLDrawDetails(string LDrawRef)
        //{            
        //    try
        //    {                
        //        LDrawDetailsCollection coll = GetLDrawDetailsData_UsingLDrawRefList(new List<string>() { LDrawRef });
        //        CompositePartCollection comColl = new CompositePartCollection();
        //        foreach (string subPartDetails in coll.LDrawDetailsList[0].SubPartLDrawRefList)
        //        {
        //            string SubPart_LDrawRef = subPartDetails.Split('|')[0];
        //            int SubPart_LDrawColourID = int.Parse(subPartDetails.Split('|')[1]);

        //            // ** Get the Sub Part description from the Sub Part file in FILESHARE **
        //            LDrawDetails subPartLDD = GetLDrawDetails_FromLDrawFile(SubPart_LDrawRef);
        //            string subPart_LDrawDescription = subPartLDD.LDrawDescription;
        //            comColl.CompositePartList.Add(new CompositePart() { LDrawRef = SubPart_LDrawRef, LDrawColourID = SubPart_LDrawColourID, LDrawDescription = subPart_LDrawDescription });
        //        }
        //        // Assumes that Sub Parts don't make reference to other sub parts. Would need to change this if any Sub Parts are "c"
        //        return comColl;
        //    }
        //    catch (Exception)
        //    {
        //        return new CompositePartCollection();
        //    }
        //}

        //private List<string> GetSubPartLDrawRefListFromLDrawFileText(string LDrawFileText)
        //{
        //    List<string> SubPartLDrawRefList = new List<string>();
        //    string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //    foreach (string fileLine in lines)
        //    {
        //        if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false)
        //        {
        //            // Check if part is a real sub part **
        //            string formattedLine = fileLine.Trim().Replace("   ", " ").Replace("  ", " ");
        //            string[] DatLine = formattedLine.Split(' ');
        //            string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "");

        //            // ** Check if sub part exists in LDraw directory **
        //            ShareFileClient share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\parts").GetFileClient(SubPart_LDrawRef + ".dat");
        //            if (share.Exists() == false)
        //            {
        //                share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial\parts").GetFileClient(SubPart_LDrawRef + ".dat");
        //                if (share.Exists() == false)
        //                {
        //                    share = new ShareClient(this.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\ldraw\unofficial minifig\parts").GetFileClient(SubPart_LDrawRef + ".dat");
        //                }
        //            }
        //            if (share.Exists())
        //            {
        //                int SubPart_LDrawColourID = int.Parse(DatLine[1]);
        //                if (SubPart_LDrawColourID == 16) SubPart_LDrawColourID = -1;    // Assumes that Sub Parts don't make reference to other sub parts. Would need to change this if any Sub Parts are "c"
        //                SubPartLDrawRefList.Add(SubPart_LDrawRef + "|" + SubPart_LDrawColourID);
        //            }
        //        }
        //    }
        //    return SubPartLDrawRefList;
        //}

        //public void AddLDrawDetails(LDrawDetails ldd)
        //{
        //    string sql;

        //    sql = "SELECT MAX(ID) 'RESULT' FROM LDRAW_DETAILS";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int oldID = 0;
        //    if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
        //    int newID = oldID + 1;

        //    // ** Generate SQL Statement **
        //    sql = "INSERT INTO LDRAW_DETAILS" + Environment.NewLine;
        //    sql += "(ID,LDRAW_REF,LDRAW_DESCRIPTION,PART_TYPE,LDRAW_PART_TYPE,SUB_PART_COUNT,DATA,SUB_PART_LDRAW_REF_LIST)" + Environment.NewLine;
        //    sql += "VALUES" + Environment.NewLine;
        //    sql += "(";
        //    sql += newID + ",";
        //    sql += "'" + ldd.LDrawRef + "',";
        //    sql += "'" + ldd.LDrawDescription + "',";
        //    sql += "'" + ldd.PartType + "',";
        //    sql += "'" + ldd.LDrawPartType + "',";
        //    sql += ldd.SubPartCount + ",";
        //    sql += "'" + ldd.Data.Replace("'", "''") + "',";
        //    sql += "'" + String.Join(",", ldd.SubPartLDrawRefList) + "'";
        //    sql += ")";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        // ** ThemeDetails **

        //public ThemeDetailsCollection GetAllThemeDetails()
        //{
        //    ThemeDetailsCollection ThemeDetailsCollection = new ThemeDetailsCollection();
        //    string sql = "SELECT THEME,SUB_THEME FROM SET_DETAILS GROUP BY THEME,SUB_THEME ORDER BY THEME,SUB_THEME";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    ThemeDetailsCollection = ThemeDetailsCollection.GetThemeDetailsCollectionFromDataTable(results);
        //    return ThemeDetailsCollection;
        //}

        //public ThemeDetailsCollection GetThemeDetailsData_UsingThemeList(List<string> IDList)
        //{
        //    // ** Generate ThemeDetailsCollection from SET_DETAILS data in database **
        //    ThemeDetailsCollection coll = new ThemeDetailsCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT THEME,SUB_THEME FROM SET_DETAILS  ";
        //        sql += "WHERE THEME IN (" + string.Join(",", IDList.Select(s => "'" + s.Replace("'","''") + "'")) + ")";
        //        sql += " GROUP BY THEME,SUB_THEME ORDER BY THEME,SUB_THEME";

        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = ThemeDetailsCollection.GetThemeDetailsCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public int GetSetCountForThemeAndSubTheme(string theme, string subTheme)
        //{           
        //    String sql = "SELECT COUNT(ID) 'RESULT' FROM SET_DETAILS WHERE THEME='" + theme.Replace("'", "''") + "'";
        //    if(subTheme != "") sql += " AND SUB_THEME='" + subTheme.Replace("'", "''") + "'";           
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int count = (int)results.Rows[0]["RESULT"];           
        //    return count;
        //}


        //private static string DownloadBlobToXMLString(BlobClient blob)
        //{
        //    string xmlString = "";
        //    if (blob.Exists())
        //    {
        //        byte[] fileContent = new byte[blob.GetProperties().Value.ContentLength];
        //        using (var ms = new MemoryStream(fileContent)) blob.DownloadTo(ms);
        //        xmlString = Encoding.UTF8.GetString(fileContent);
        //    }
        //    return xmlString;
        //}

        //private static void UploadXMLStringToBlob(BlobClient blob, string xmlString)
        //{
        //    byte[] bytes = Encoding.UTF8.GetBytes(xmlString);
        //    using (var ms = new MemoryStream(bytes)) blob.Upload(ms, true);
        //}

        // ** TickBack Functions **

        //public TickBackCollection GetTickBackData_UsingTickBackNameList(List<string> IDList)
        //{
        //    // ** Generate TickBackCollection from TICKBACK data in database **
        //    TickBackCollection coll = new TickBackCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT ID,NAME,DATA FROM TICKBACK ";
        //        sql += "WHERE NAME IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = TickBackCollection.GetTickBackCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public void AddTickBack(TickBack tb)
        //{
        //    string sql;

        //    sql = "SELECT MAX(ID) 'RESULT' FROM TICKBACK";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int oldID = 0;
        //    if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
        //    int newID = oldID + 1;

        //    // ** Generate SQL Statement **
        //    sql = "INSERT INTO TICKBACK" + Environment.NewLine;
        //    sql += "(ID,NAME,DATA)" + Environment.NewLine;
        //    sql += "VALUES" + Environment.NewLine;
        //    sql += "(";
        //    sql += newID + ",";
        //    sql += "'" + tb.Name + "',";
        //    sql += "'" + tb.Data + "'";            
        //    sql += ")";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        //public void UpdateTickBack(TickBack tb)
        //{
        //    // ** Generate SQL Statement **
        //    string sql = "UPDATE TICKBACK SET " + Environment.NewLine;
        //    sql += "NAME='" + tb.Name + "',";
        //    sql += "DATA='" + tb.Data + "'";            
        //    sql += " WHERE NAME='" + tb.Name + "'";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        //public void DeleteTickBack(string TickBackName)
        //{
        //    // ** Generate SQL Statement **
        //    string sql = "DELETE FROM TICKBACK WHERE NAME='" + TickBackName + "'" + Environment.NewLine;

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);
        //}

        // ** Rebrickable Functions **

        //public string GetRebrickableSetJSONString(string SetRef)
        //{
        //    string url = "https://rebrickable.com/api/v3/lego/sets/" + SetRef + "/parts/";
        //    string JSONString = "";
        //    using (var httpClient = new HttpClient())
        //    {
        //        using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
        //        {
        //            request.Headers.TryAddWithoutValidation("Accept", "application/json");
        //            request.Headers.TryAddWithoutValidation("Authorization", "key " + this.RebrickableKey);                    
        //            var task = Task.Run(() => httpClient.SendAsync(request));
        //            task.Wait();
        //            var response = task.Result;
        //            if (response.StatusCode == HttpStatusCode.OK) JSONString = response.Content.ReadAsStringAsync().Result;                    
        //        }
        //    }
        //    return JSONString;
        //}

        // ** BasePart Functions **

        //public BasePartCollection GetBasePartData_All()
        //{
        //    // ** Generate BasePartCollection from xml data in Blob **
        //    //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
        //    //string xmlString = DownloadBlobToXMLString(blob);
        //    //BasePartCollection coll = new BasePartCollection().DeserialiseFromXMLString(xmlString);

        //    // ** Generate BasePartCollection from BASEPART data in database **
        //    String sql = "SELECT LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE,SUB_PART_COUNT FROM BASEPART";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    BasePartCollection coll = BasePartCollection.GetBasePartCollectionFromDataTable(results);

        //    return coll;
        //}

        //public BasePartCollection GetBasePartData_UsingLDrawRefList(List<string> IDList)
        //{
        //    // ** Generate BasePartCollection from BASEPART data in database **
        //    BasePartCollection coll = new BasePartCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE,SUB_PART_COUNT FROM BASEPART ";
        //        sql += "WHERE LDRAW_REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = BasePartCollection.GetBasePartCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public bool CheckIfBasePartExists(string LDrawRef)
        //{
        //    bool exists = false;
        //    BasePartCollection coll = GetBasePartData_UsingLDrawRefList(new List<string>() { LDrawRef });
        //    if (coll.BasePartList.Count > 0) exists = true;
        //    return exists;
        //}

        //public string GetLDrawDescription_UsingLDrawRef(string LDrawRef)
        //{            
        //    String sql = "SELECT LDRAW_DESCRIPTION FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    string result = (string)results.Rows[0]["LDRAW_DESCRIPTION"];
        //    return result;

        //}

        //public int GetLDrawSize_UsingLDrawRef(string LDrawRef)
        //{
        //    // ** Get data from BASEPART database table **
        //    String sql = "SELECT LDRAW_SIZE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int LDrawSize = 0;
        //    if (results.Rows.Count > 0) LDrawSize = (int)results.Rows[0]["LDRAW_SIZE"];
        //    return LDrawSize;
        //}

        //public string GetPartType_UsingLDrawRef(string LDrawRef)
        //{
        //    // ** Get data from BASEPART database table **
        //    String sql = "SELECT PART_TYPE FROM BASEPART WHERE LDRAW_REF='" + LDrawRef + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    string partType = (string)results.Rows[0]["PART_TYPE"];
        //    return partType;
        //}

        //public void AddBasePart(BasePart bp)
        //{
        //    string sql;

        //    sql = "SELECT MAX(ID) 'RESULT' FROM BASEPART";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int oldID = 0;
        //    if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
        //    int newID = oldID + 1;

        //    // ** Generate SQL Statement **
        //    sql = "INSERT INTO BASEPART" + Environment.NewLine;
        //    sql += "(ID,LDRAW_REF,LDRAW_DESCRIPTION,LDRAW_CATEGORY,LDRAW_SIZE,OFFSET_X,OFFSET_Y,OFFSET_Z,IS_SUB_PART,IS_STICKER,IS_LARGE_MODEL,PART_TYPE,LDRAW_PART_TYPE,SUB_PART_COUNT)" + Environment.NewLine;
        //    sql += "VALUES" + Environment.NewLine;
        //    sql += "(";
        //    sql += newID + ",";
        //    sql += "'" + bp.LDrawRef + "',";
        //    sql += "'" + bp.LDrawDescription + "',";
        //    sql += "'" + bp.LDrawCategory + "',";
        //    sql += bp.LDrawSize + ",";
        //    sql += bp.OffsetX + ",";
        //    sql += bp.OffsetY + ",";
        //    sql += bp.OffsetZ + ",";
        //    sql += "'" + bp.IsSubPart + "',";
        //    sql += "'" + bp.IsSticker + "',";
        //    sql += "'" + bp.IsLargeModel + "',";
        //    sql += "'" + bp.partType + "',";
        //    sql += "'" + bp.lDrawPartType + "',";
        //    sql += bp.SubPartCount;
        //    sql += ")";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);

        //    #region ** UPLOAD UPDATED BasePartCollection TO AZURE AND LOCAL CACHE **
        //    //xmlString = bpc.SerializeToString(true);
        //    //byte[] bytes = Encoding.UTF8.GetBytes(xmlString);                
        //    //blob = new BlobContainerClient(Global_Variables.AzureStorageConnString, "static-data").GetBlobClient("BasePartCollection.xml");
        //    //using (var ms = new MemoryStream(bytes))
        //    //{
        //    //    blob.Upload(ms, true);
        //    //}
        //    ////Global_Variables.BasePartCollectionXML.LoadXml(xmlString);
        //    #endregion

        //    #region ** ADD NEW .dat FILE FOR PART ** 
        //    //string line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + LDrawRef + ".dat" + Environment.NewLine;
        //    //bytes = Encoding.UTF8.GetBytes(line);
        //    //ShareFileClient share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
        //    //share.Create(bytes.Length);
        //    //using (MemoryStream ms = new MemoryStream(bytes))
        //    //{
        //    //    share.Upload(ms);
        //    //}
        //    #endregion

        //}

        // ** CompositePart Functions **

        //public CompositePartCollection GetCompositePartData_All()
        //{
        //    // ** Generate BasePartCollection from xml data in Blob **
        //    //BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "static-data").GetBlobClient("CompositePartCollection.xml");
        //    //string xmlString = DownloadBlobToXMLString(blob);
        //    //CompositePartCollection coll = new CompositePartCollection().DeserialiseFromXMLString(xmlString);

        //    // ** Generate CompositePartCollection from COMPOSITEPART data in database **
        //    string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    CompositePartCollection coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);

        //    return coll;
        //}

        //public CompositePartCollection GetCompositePartData_UsingLDrawRefList(List<string> IDList)
        //{
        //    // ** Generate CompositePartCollection from COMPOSITEPART data in database **
        //    CompositePartCollection coll = new CompositePartCollection();
        //    if (IDList.Count > 0)
        //    {
        //        string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
        //        sql += "WHERE LDRAW_REF IN (" + string.Join(",", IDList.Select(s => "'" + s + "'")) + ")";
        //        var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //        coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);
        //    }
        //    return coll;
        //}

        //public bool CheckIfCompositePartsExist(string LDrawRef)
        //{
        //    bool exists = false;
        //    String sql = "SELECT COUNT(ID) 'RESULT' FROM COMPOSITEPART WHERE PARENT_LDRAW_REF='" + LDrawRef + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int result = (int)results.Rows[0]["RESULT"];
        //    if (result > 0) exists = true;
        //    return exists;
        //}

        //public CompositePartCollection GetCompositePartData_UsingParentLDrawRefList(string ParentLDrawRef)
        //{
        //    // ** Generate CompositePartCollection from COMPOSITEPART data in database **
        //    CompositePartCollection coll = new CompositePartCollection();
        //    string sql = "SELECT ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z FROM COMPOSITEPART ";
        //    sql += "WHERE PARENT_LDRAW_REF='" + ParentLDrawRef + "'";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    coll = CompositePartCollection.GetCompositePartCollectionFromDataTable(results);
        //    return coll;
        //}

        //public void AddCompositePart(CompositePart cp)
        //{
        //    string sql;

        //    sql = "SELECT MAX(ID) 'RESULT' FROM COMPOSITEPART";
        //    var results = GetSQLQueryResults(this.AzureDBConnString, sql);
        //    int oldID = 0;
        //    if (results.Rows[0]["RESULT"].ToString() != "") oldID = (int)results.Rows[0]["RESULT"];
        //    int newID = oldID + 1;

        //    // ** Generate SQL Statement **
        //    sql = "INSERT INTO COMPOSITEPART" + Environment.NewLine;
        //    sql += "(ID,LDRAW_REF,LDRAW_DESCRIPTION,PARENT_LDRAW_REF,LDRAW_COLOUR_ID,POS_X,POS_Y,POS_Z,ROT_X,ROT_Y,ROT_Z)" + Environment.NewLine;
        //    sql += "VALUES" + Environment.NewLine;
        //    sql += "(";
        //    sql += newID + ",";
        //    sql += "'" + cp.LDrawRef + "',";
        //    sql += "'" + cp.LDrawDescription + "',";
        //    sql += "'" + cp.ParentLDrawRef + "',";
        //    sql += cp.LDrawColourID + ",";
        //    sql += cp.PosX + ",";
        //    sql += cp.PosY + ",";
        //    sql += cp.PosZ + ",";
        //    sql += cp.RotX + ",";
        //    sql += cp.RotY + ",";
        //    sql += cp.RotZ;
        //    sql += ")";

        //    // ** Execute SQL statement **
        //    ExecuteSQLStatement(this.AzureDBConnString, sql);


        //    //    #region ** ADD NEW .dat FILE FOR SUB PART ** 
        //    //    //line = "1 450 0 0 0 1 0 0 0 1 0 0 0 1 " + SubPart_LDrawRef + ".dat" + Environment.NewLine;
        //    //    //bytes = Encoding.UTF8.GetBytes(line);
        //    //    //share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + SubPart_LDrawRef + ".dat");
        //    //    //share.Create(bytes.Length);
        //    //    //using (MemoryStream ms = new MemoryStream(bytes))
        //    //    //{
        //    //    //    share.Upload(ms);
        //    //    //}
        //    //    #endregion

        //    //    #region ** CREATE Composite Part DAT file **
        //    //    string LDrawFileText = GetLDrawFileDetails(LDrawRef);                    
        //    //    string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
        //    //    string DAT_String = "";
        //    //    foreach (string fileLine in lines)
        //    //    {
        //    //        if (fileLine.StartsWith("1"))
        //    //        {
        //    //            DAT_String += fileLine.Replace("1 16 ", "1 450 ") + Environment.NewLine;
        //    //        }
        //    //    }
        //    //    bytes = Encoding.UTF8.GetBytes(DAT_String);
        //    //    share = new ShareClient(Global_Variables.AzureStorageConnString, "lodgeant-fs").GetDirectoryClient(@"static-data\files-dat").GetFileClient("p_" + LDrawRef + ".dat");
        //    //    share.Create(bytes.Length);
        //    //    using (var ms = new MemoryStream(bytes))
        //    //    {
        //    //        share.Upload(ms);                        
        //    //    }
        //    //    #endregion


        //}

        //public string UploadImageToBLOB(string sourceURL, string ImageType, string ImageName)
        //{
        //    string response = "";
        //    try
        //    {
        //        // ** Download image from Rebrickable **
        //        byte[] imageb = new byte[0];
        //        try
        //        {
        //            imageb = new WebClient().DownloadData(sourceURL);
        //        }
        //        catch
        //        { }

        //        // ** Upload image to Azure **
        //        if (imageb.Length == 0) throw new Exception("No data found for URL");
        //        BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, "images-" + ImageType.ToLower()).GetBlobClient(ImageName + ".png");
        //        using (var ms = new MemoryStream(imageb)) blob.Upload(ms, true);
        //        return response;
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        // ** HELPFUL METHODS **

        //public bool CheckIfBlobExists(string containerName, string blobName)
        //{
        //    BlobClient blob = new BlobContainerClient(this.AzureStorageConnString, containerName).GetBlobClient(blobName);
        //    return blob.Exists();
        //}

        //private static DataTable GetSQLQueryResults(string AzureDBConnString, string sql)
        //{
        //    var results = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(AzureDBConnString))
        //    {
        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            using (SqlDataReader reader = command.ExecuteReader()) results.Load(reader);
        //        }
        //    }
        //    return results;
        //}

        //private static void ExecuteSQLStatement(string AzureDBConnString, string sql)
        //{
        //    var results = new DataTable();
        //    using (SqlConnection connection = new SqlConnection(AzureDBConnString))
        //    {
        //        using (SqlCommand command = new SqlCommand(sql, connection))
        //        {
        //            connection.Open();
        //            command.ExecuteNonQuery();
        //        }
        //    }            
        //}

    }
}
