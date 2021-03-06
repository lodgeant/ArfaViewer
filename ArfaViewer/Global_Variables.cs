using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Xml;


namespace Generator
{
    public static class Global_Variables
    {        
        public static Dictionary<int, Bitmap> PartColourImage_Dict = new Dictionary<int, Bitmap>();
        public static Dictionary<string, Bitmap> ElementImage_Dict = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> LDrawImage_Dict = new Dictionary<string, Bitmap>();
        public static Dictionary<string, Bitmap> SetImage_Dict = new Dictionary<string, Bitmap>();        
        public static Dictionary<string, string> LDrawDATDetails_Dict = new Dictionary<string, string>();

        public static XmlDocument BasePartCollectionXML = new XmlDocument();
        public static XmlDocument CompositePartCollectionXML = new XmlDocument();
        public static XmlDocument PartColourCollectionXML = new XmlDocument();
        
        public static string LDrawImageInventoryUrl_Offical = "https://www.ldraw.org/library/official/images/parts/";
        public static string LDrawImageInventoryUrl_Unoffical = "https://www.ldraw.org/library/unofficial/images/parts/";
        public static string elementURL = "https://m.rebrickable.com/media/parts/ldraw";
        public static string UnityLegoPartPath = @"C:\Unity Projects\Lego Unity Viewer\Assets\Resources\Lego Part Models";    // Used for SyncFBXFiles
        public static string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";

        public static string RebrickableKey = "856437d0f14f81e4d3356d27bf1b419e";


    }

}
