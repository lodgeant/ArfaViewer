using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{


        
    [Serializable]
    public class RouteRequest : Request
    {       
        [XmlAttribute("RouteFound")]
        public bool RouteFound;

        [XmlElement("UseAStar")]
        public bool UseAStar;

        [XmlElement("UseDiagonals")]
        public bool UseDiagonals;

        [XmlElement("MaxCalcWaitSecs")]
        public int MaxCalcWaitSecs;

        [XmlElement("TimeTakenCalculationMS")]
        public double TimeTaken_CalculationMS;

        [XmlElement("ActualStartPoint")]
        public Vect ActualStartPoint;

        [XmlElement("ActualTargetPoint")]
        public Vect ActualTargetPoint;

        [XmlElement("CoreStartPoint")]
        public Point CoreStartPoint;

        [XmlElement("CoreTargetPoint")]
        public Point CoreTargetPoint;

        [XmlElement("RealStartPoint")]
        public Point RealStartPoint;

        [XmlElement("RealTargetPoint")]
        public Point RealTargetPoint;

        [XmlElement("NodeCountOpen")]
        public int NodeCountOpen;

        [XmlElement("NodeCountClosed")]
        public int NodeCountClosed;

        [XmlElement("MapScreenShotFilename")]
        public string MapScreenShotFilename;



        [XmlElement("ObjectRefsToIgnore")]
        public List<string> ObjectRefsToIgnore { get; set; }

        [XmlElement("RouteActualVectorList")]
        public List<Vect> RouteActualVectorList { get; set; }

        



        //public string PackageFilename;
        //public string PerformanceData;
        private int[,] ObjectMap;
        private byte[] ObjectMapImage;
        private byte[] MapScreenShot;



        // ** CONSTRUCTORS **
        public RouteRequest()
        {
            RouteActualVectorList = new List<Vect>();
            ObjectRefsToIgnore = new List<string>();
        }



        // ** METHODS **
        public void Set_ObjectMap(int[,] ObjectMap)
        {
            this.ObjectMap = ObjectMap;
        }

        public int[,] Get_ObjectMap()
        {
            return ObjectMap;
        }

        public void Set_ObjectMapImage(byte[] ObjectMapImage)
        {
            this.ObjectMapImage = ObjectMapImage;
        }

        public byte[] Get_ObjectMapImage()
        {
            return this.ObjectMapImage;
        }

        public void Set_MapScreenShot(byte[] MapScreenShot)
        {
            this.MapScreenShot = MapScreenShot;
        }

        public byte[] Get_MapScreenShot()
        {
            return this.MapScreenShot;
        }





        public string SerializeToString(bool omitDeclaration)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(this.GetType());
            using (StringWriter textWriter = new Utf8StringWriter())
            {
                xmlSerializer.Serialize(textWriter, this);
                if (omitDeclaration)
                {
                    return textWriter.ToString().Replace(@"<?xml version=""1.0"" encoding=""utf-8""?>", "");
                }
                else
                {
                    return textWriter.ToString();
                }
            }
        }

        public class Utf8StringWriter : StringWriter
        {
            // Use UTF8 encoding but write no BOM to the wire
            public override Encoding Encoding
            {
                get { return new UTF8Encoding(false); } // in real code I'll cache this encoding.
            }
        }

        public RouteRequest DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (RouteRequest)serializer.Deserialize(reader);
            }
        }



        public void CalculateRoute()
        {
            try
            {
                


            }
            catch (Exception ex)
            {
                
            }
        }


        
    }

}
