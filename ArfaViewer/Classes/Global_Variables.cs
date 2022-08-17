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
        public static APIProxy APIProxy;
        public static Dictionary<ImageType, Dictionary<string, Bitmap>> ImageDict = new Dictionary<ImageType, Dictionary<string, Bitmap>>();
        
        //TODO_H: Move this variable to the API class as the client should never know these.
        public static string AzureStorageConnString = "DefaultEndpointsProtocol=https;AccountName=lodgeaccount;AccountKey=j3PZRNLxF00NZqpjfyZ+I1SqDTvdGOkgacv4/SGBSVoz6Zyl394bIZNQVp7TfqIg+d/anW9R0bSUh44ogoJ39Q==;EndpointSuffix=core.windows.net";
        
          

    }

}
