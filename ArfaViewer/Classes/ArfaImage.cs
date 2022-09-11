using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using Azure.Storage.Blobs;
using System.IO;
using System.Net;
using BaseClasses;

namespace Generator
{



    public class ArfaImage
    {
       
        public static Bitmap GetImage(ImageType imageType, string[] _params)
        {
            // ** Check if base key is present - if not, add it **
            if (Global_Variables.ImageDict.ContainsKey(imageType) == false) Global_Variables.ImageDict.Add(imageType, new Dictionary<string, Bitmap>());

            // ** Determine variables **                   
            string itemRef = "";
            switch (imageType)
            {
                case ImageType.SET:                             // params[0] = SetRef
                case ImageType.PARTCOLOUR:                      // params[0] = LDrawColourID
                case ImageType.LDRAW:                           // params[0] = LDrawRef
                case ImageType.THEME:                           // params[0] = Theme + | + SubTheme
                    itemRef = _params[0];
                    break;

                case ImageType.ELEMENT:
                    itemRef = _params[0] + "|" + _params[1];    // params[0] = LDrawRef, params[1] = LDrawColourID
                    break;

            }

            // [1] Check if image is already in local cache.
            Bitmap image = null;
            if (Global_Variables.ImageDict[imageType].Keys.Count > 0 && Global_Variables.ImageDict[imageType].Keys.Contains(itemRef))
            {
                image = Global_Variables.ImageDict[imageType][itemRef];
            }
            else
            {
                // [2] If image is NOT in local cache, check whether image is available on Azure storage. If the image is NOT in Azure storage either, the image will be downloaded from a 3rd party url.
                //image = StaticData.GetImage(imageType, _params);
                image = GetImageDetails(imageType, _params);
                if (image != null && Global_Variables.ImageDict[imageType].Keys.Contains(itemRef) == false)
                {
                    Global_Variables.ImageDict[imageType].Add(itemRef, image);
                }
            }
            return image;
        }

        private static Bitmap GetImageDetails(ImageType imageType, string[] _params)
        {
            //string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";

            Bitmap image = null;

            #region ** Determine variables **
            string itemRef = "";
            List<string> imageUrlList = new List<string>();
            if (imageType == ImageType.SET)
            {
                // params[0] = SetRef
                itemRef = _params[0];
                imageUrlList.Add("https://img.bricklink.com/ItemImage/ON/0/" + itemRef + ".png");
                imageUrlList.Add("https://img.bricklink.com/ItemImage/MN/0/" + itemRef + ".png");

                //https://images.brickset.com/sets/large/1278-1.jpg

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
            else if (imageType == ImageType.THEME)
            {
                // params[0] = Theme + | + SubTheme
                itemRef = _params[0];
                //imageUrl not used
            }
            #endregion

            #region ** Process image **

            // Check if data exists for the BLOB
            ImageObject io = StaticData.GetImageObject_UsingContainerAndBlobName("images-" + imageType.ToString().ToLower(), itemRef + ".png");            
            if(io.Name != null && io.Name != "")
            //BlobClient blob = new BlobContainerClient(AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
            //if (blob.Exists())
            {
                //image = DownloadBlobToBitmap(blob);
                using (var ms = new MemoryStream(io.Data)) image = new Bitmap(ms);
            }
            else
            {
                if (image == null)
                {
                    // ** If the image was not already in the Azure images, upload it to Azure for use in future **
                    // ** Download element image from source API **                    
                    byte[] imageb = null;
                    string foundUrl = "";
                    if (imageUrlList.Count == 1)
                    {                        
                        try
                        {
                            imageb = new WebClient().DownloadData(imageUrlList[0]);
                            foundUrl = imageUrlList[0];
                        }
                        catch { }
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
                                    foundUrl = imageUrl;
                                }
                                catch { }
                            }
                        }
                    }
                    if (imageb != null && imageb.Length > 0)
                    {
                        // ** Upload the image to Azure **                        
                        //BlobClient newBlob = new BlobContainerClient(AzureStorageConnString, "images-" + imageType.ToString().ToLower()).GetBlobClient(itemRef + ".png");
                        //using (var ms = new MemoryStream(imageb))
                        //{
                        //    newBlob.Upload(ms, true);
                        //    image = new Bitmap(ms);
                        //}
                        StaticData.UploadImageToBLOB_UsingURL(foundUrl, imageType.ToString().ToLower(), itemRef);
                    }
                }
            }
            #endregion

            return image;
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


    public enum ImageType
    {
        SET,
        PARTCOLOUR,
        ELEMENT,
        LDRAW,
        THEME
    }






}
