using System.Collections.Generic;
using System.Drawing;


namespace Generator
{
    public static class Global_Variables
    {
        public static Dictionary<ImageType, Dictionary<string, Bitmap>> ImageDict = new Dictionary<ImageType, Dictionary<string, Bitmap>>();
        public static string APIUrl2 = "https://arfabrickviewer.azurewebsites.net/";
        public static string currentUser = "LODGEANT";
        //public static zzz_APIProxy APIProxy;
        //public static string APIUrl = "https://arfabrickviewer.azurewebsites.net/source/";

    }

}
