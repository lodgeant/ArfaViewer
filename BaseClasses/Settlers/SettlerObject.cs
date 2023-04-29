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

    // ObjectType
    // FLORA, SETTLER, COMMODITY, BUILDING

    // ObjectSubType
    // FLOOR, WATER, STREAM_CORNER, STREAM_STRAIGHT_1, STREAM_STRAIGHT_2, BRIDGE, WALL

    //BOARD,
    //STONE,
    //LOG,
    //TREE,
    //STUMP,
    //STONE_OUTCROP,
    //CRATE_FRUIT,
    //FLOUR,
    //BUCKET_WATER,



    //WOODCUTTER_HUT,
    //FORESTER_HUT,        
    //STONECUTTER_HUT,
    //SAWMILL,
    //STORAGE_AREA,
    //RESIDENCE,
    //ORCHARD,

    //WOODCUTTER,
    //STONECUTTER,
    //FORESTER,
    //CARRIER,        
    //CRAFTSMAN,
    //ORCHARDIST,


    //// New objects
    //BAKERY,
    //BAKER,
    //PIE_FRUIT,
    //BREAD,

    //FARM,FARMER,WHEAT,
    //MILL,MILLER,                // WHEAT -> FLOUR        
    //WELL,WATERMAN,
    //BUILDER,LANDSCAPER,
    //PIG,SHEEP,
    //CORN,
    //SPADE,                      // LANDSCAPER        
    //SCYTHE,                     // FARMER   
    //AXE,                        // WOODCUTTER
    //HAMMER,                     // BUILDER
    //FORK


    
    [XmlInclude(typeof(StoneCutterHut))]
    [XmlInclude(typeof(Sawmill))]
    [XmlInclude(typeof(Carrier))]
    [XmlInclude(typeof(Residence))]
    [XmlInclude(typeof(Forester))]
    [XmlInclude(typeof(ForesterHut))]
    [XmlInclude(typeof(WoodcutterHut))]
    [XmlInclude(typeof(Woodcutter))]
    [XmlInclude(typeof(Tree))]
    [XmlInclude(typeof(Stump))]
    [XmlInclude(typeof(Log))]
    [Serializable]
    public class SettlerObject
    {
        [XmlAttribute("ObjectRef")]
        public string ObjectRef { get; set; }

        [XmlAttribute("ObjectType")]
        public string ObjectType { get; set; }

        [XmlAttribute("ObjectSubType")]
        public string ObjectSubType { get; set; }

        [XmlAttribute("State")]
        public string State { get; set; }

        [XmlAttribute("CurrentCycle")]
        public int CurrentCycle { get; set; }

        [XmlAttribute("RelatedBuildingRef")]
        public string RelatedBuildingRef { get; set; }

        [XmlAttribute("RelatedSettlerRef")]
        public string RelatedSettlerRef { get; set; }






        [XmlElement("Position")]
        public Vect Position { get; set; }

        [XmlElement("Rotation")]
        public Vect Rotation { get; set; }

        [XmlElement("LocalScale")]
        public Vect LocalScale { get; set; }

        [XmlElement("NearbyRadius")]
        public float NearbyRadius { get; set; }

        [XmlElement("RouteWalkSpeed")]
        public int RouteWalkSpeed { get; set; }

        [XmlElement("MaxSiloItemCount")]
        public int MaxSiloItemCount { get; set; }

        [XmlElement("TargetPosition")]
        public Vect TargetPosition { get; set; }

        [XmlElement("TargetPositionState")]
        public string TargetPositionState { get; set; }

        [XmlElement("RouteWayPointIndex")]
        public int RouteWayPointIndex { get; set; }

        [XmlElement("TargetSourceFloraRef")]
        public string TargetSourceFloraRef { get; set; }


        [XmlElement("SourceFloraGatherDelay")]
        public float SourceFloraGatherDelay { get; set; }           // the delay needed between each attempt to fell a tree (5 secs)

        [XmlElement("LastSourceFloraGatherTime")]
        public float LastSourceFloraGatherTime { get; set; }        // timestamp to indicate the last time that a tree was felled. This is updated when the Woodcutter has felled a tree.

        [XmlElement("CurrentCommodityRef")]                         // The commodity that the Settler is currently holding.
        public string CurrentCommodityRef { get; set; }

        [XmlElement("CurrentRouteRequestID")]
        public int CurrentRouteRequestID { get; set; }

        [XmlElement("CurrentTreePositionRequestID")]
        public int CurrentNewTreePositionRequestID { get; set; }

        [XmlElement("CurrentCommodityOrderID")]
        public int CurrentCommodityOrderID { get; set; }



        [XmlElement("SettlerPosition")]
        public List<SettlerPosition> SettlerPositionList { get; set; }

        [XmlElement("CurrentNearbyGORefList")]
        public List<string> CurrentNearbyGORefList { get; set; }

        [XmlElement("PotentialSourceFloraList")]
        public List<SourceFloraDetails> PotentialSourceFloraList { get; set; }
        
        [XmlElement("ObjectRefsToIgnore")]
        public List<string> ObjectRefsToIgnore { get; set; }

        [XmlElement("RouteActualVectorList")]
        public List<Vect> RouteActualVectorList { get; set; }

        [XmlElement("KnownRouteList")]
        public List<KnownRoute> KnownRouteList { get; set; }




        public SettlerObject()
        {
            CurrentNearbyGORefList = new List<string>();
            ObjectRefsToIgnore = new List<string>();
            PotentialSourceFloraList = new List<SourceFloraDetails>();
            SettlerPositionList = new List<SettlerPosition>();
            KnownRouteList = new List<KnownRoute>();
        }


       

    }

}
