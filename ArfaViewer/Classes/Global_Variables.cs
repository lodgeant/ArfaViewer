using BaseClasses;
using System.Collections.Generic;
using System.Drawing;


namespace Generator
{
    public static class Global_Variables
    {
        public static Dictionary<ImageType, Dictionary<string, Bitmap>> ImageDict = new Dictionary<ImageType, Dictionary<string, Bitmap>>();
        public static string APIUrl2 = "https://arfabrickviewer.azurewebsites.net/";
        public static string currentUser = "LODGEANT";
        public static Dictionary<string, string> RebrickableXMLDict = new Dictionary<string, string>();
        public static PartColourCollection PartColourCollection = new PartColourCollection();

    }

}
