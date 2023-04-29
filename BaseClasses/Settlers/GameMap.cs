using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{

    [Serializable]
    public class GameMap
    {        
        [XmlAttribute("Name")]
        public string Name { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlAttribute("GameTime")]
        public float GameTime { get; set; }

        [XmlAttribute("RequestIndex")]
        public int RequestIndex { get; set; }

        [XmlAttribute("CommodityOrderIndex")]
        public int CommodityOrderIndex { get; set; }


        [XmlElement("MapWidth")]
        public int MapWidth { get; set; }

        [XmlElement("MapHeight")]
        public int MapHeight { get; set; }

        [XmlElement("MapTopLeftX")]
        public int MapTopLeftX { get; set; }

        [XmlElement("MapTopLeftY")]
        public int MapTopLeftY { get; set; }

        [XmlElement("MapFactor")]
        public int MapFactor { get; set; }

        [XmlElement("MapScreenWidth")]
        public int MapScreenWidth { get; set; }

        [XmlElement("MapScreenHeight")]
        public int MapScreenHeight { get; set; }

        [XmlElement("MapScreenTopLeftX")]
        public int MapScreenTopLeftX { get; set; }

        [XmlElement("MapScreenTopLeftY")]
        public int MapScreenTopLeftY { get; set; }



        [XmlElement("CarrierDelay")]
        public float CarrierDelay { get; set; }

        [XmlAttribute("LastCommodityOrderTS")]
        public float LastCommodityOrderTS { get; set; }

        [XmlElement("ClearStumpDelay")]
        public float ClearStumpDelay { get; set; }

        [XmlElement("ProcessingRequests")]
        public bool ProcessingRequests { get; set; }

        [XmlElement("ProcessingOrders")]
        public bool ProcessingOrders { get; set; }


      


        [XmlElement("SettlerObjectList")]
        public SettlerObjectCollection SettlerObjectList { get; set; }

        [XmlElement("GlobalInventorySiloList")]
        public SiloCollection GlobalInventorySiloList { get; set; }

        [XmlElement("RequestOpenList")]
        public RequestCollection RequestOpenList { get; set; }

        [XmlElement("RequestClosedList")]
        public RequestCollection RequestClosedList { get; set; }

        [XmlElement("CommodityOrderOpenList")]
        public CommodityOrderCollection CommodityOrderOpenList { get; set; }

        [XmlElement("CommodityOrderClosedList")]
        public CommodityOrderCollection CommodityOrderClosedList { get; set; }






        public GameMap()
        {
            SettlerObjectList = new SettlerObjectCollection();
            GlobalInventorySiloList = new SiloCollection();
            RequestOpenList = new RequestCollection();
            RequestClosedList = new RequestCollection();
            CommodityOrderOpenList = new CommodityOrderCollection();
            CommodityOrderClosedList = new CommodityOrderCollection();
        }





        public int GetNextRequestID()
        {
            RequestIndex += 1;
            return RequestIndex;
        }

        public int GetNextCommodityOrderID()
        {
            CommodityOrderIndex += 1;
            return CommodityOrderIndex;
        }


        public bool CheckIfPositionIsOnMap(Vect potentialPos)
        {
            int mapEdgeBuffer = 2;
            float xMin = MapScreenTopLeftX + mapEdgeBuffer;
            float xMax = MapScreenTopLeftX + MapScreenWidth - mapEdgeBuffer;
            float zMax = MapScreenTopLeftY - mapEdgeBuffer;
            float zMin = MapScreenTopLeftY - MapScreenHeight + mapEdgeBuffer;
            bool withinMap = false;
            if (potentialPos.X > xMin && potentialPos.X < xMax && potentialPos.Z > zMin && potentialPos.Z < zMax)
            {
                withinMap = true;
            }
            return withinMap;
        }

        public int GenerateSettlerObjectIndex(string objectSubType)
        {
            // ** Get all object refs and Work out index based on existing objects **
            int highestIndex = 0;
            List<string> objectRefList = (from r in this.SettlerObjectList.Items
                                          where r.ObjectSubType.Equals(objectSubType)
                                          select r.ObjectRef).ToList();
            foreach (string objectRef in objectRefList)
            {
                string objectType = objectRef.Split('.')[0];
                if (objectType.Equals(objectSubType))
                {
                    int lastIndex = int.Parse(objectRef.Split('.')[1]);
                    if (lastIndex > highestIndex) highestIndex = lastIndex;
                }
            }
            int objectIndex = highestIndex + 1;
            return objectIndex;
        }

        public static string ConvertObjectMapToCSV(int[,] ObjectMap, string seperator, bool includeAxis)
        {
            // ** GET VARIABLES **
            StringBuilder sb = new StringBuilder();
            int width = ObjectMap.GetLength(0);
            int height = ObjectMap.GetLength(1);

            // ** ADD AXIS HEADER (IF REQUIRED) **
            if (includeAxis)
            {
                sb.Append(seperator);
                for (int x = 0; x < width; x++) sb.Append(x + seperator);
                sb.Append(Environment.NewLine);
            }

            // ** ADD ROWS **
            for (int y = 0; y < height; y++)
            {
                if (includeAxis) sb.Append(y + seperator);
                for (int x = 0; x < width; x++)
                {
                    sb.Append(ObjectMap[x, y] + seperator);
                }
                sb.Append(Environment.NewLine);
            }
            return sb.ToString();
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

        public GameMap DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (GameMap)serializer.Deserialize(reader);
            }
        }

        

    }

}
