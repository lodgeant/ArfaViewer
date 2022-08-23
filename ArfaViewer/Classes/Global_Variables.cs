using System.Collections.Generic;
using System.Drawing;


namespace Generator
{
    public static class Global_Variables
    {
        public static APIProxy APIProxy;
        public static Dictionary<ImageType, Dictionary<string, Bitmap>> ImageDict = new Dictionary<ImageType, Dictionary<string, Bitmap>>();
        public static string APIUrl = "https://arfabrickviewer.azurewebsites.net/source/";

    }

}
