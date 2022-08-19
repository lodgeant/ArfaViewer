using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

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
                image = StaticData.GetImage(imageType, _params);
                //Global_Variables.ImageDict[imageType].Add(itemRef, image);
                if(image != null) Global_Variables.ImageDict[imageType].Add(itemRef, image);                
            }
            return image;
        }


    }


    public enum ImageType
    {
        SET,
        PARTCOLOUR,
        ELEMENT,
        LDRAW
    }






}
