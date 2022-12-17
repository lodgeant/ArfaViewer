using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BaseClasses
{
    [Serializable]
    public class RecentSetMapping
    {
        [XmlAttribute("UserID")]
        public string UserID { get; set; }

        [XmlAttribute("CreatedTS")]
        public DateTime CreatedTS { get; set; }

        [XmlAttribute("SetRef")]
        public string SetRef { get; set; }

        [XmlAttribute("SetDescription")]
        public string SetDescription { get; set; }




        public static RecentSetMapping GetRecentSetMappingFromDBDataRow(DataRow row)
        {
            RecentSetMapping item = new RecentSetMapping();
            item.UserID = (string)row["USER_ID"];
            item.CreatedTS = DateTime.Parse(row["CREATED_TS"].ToString());
            item.SetRef = (string)row["SET_REF"];
            item.SetDescription = (string)row["SET_DESCRIPTION"];

            //item.LDrawDescription = (string)row["LDRAW_DESCRIPTION"];
            //item.LDrawCategory = (string)row["LDRAW_CATEGORY"];
            //item.LDrawSize = (int)row["LDRAW_SIZE"];
            //item.OffsetX = float.Parse(row["OFFSET_X"].ToString());
            //item.OffsetY = float.Parse(row["OFFSET_Y"].ToString());
            //item.OffsetZ = float.Parse(row["OFFSET_Z"].ToString());
            //item.IsSubPart = bool.Parse(row["IS_SUB_PART"].ToString());
            //item.IsSticker = bool.Parse(row["IS_STICKER"].ToString());
            //item.IsLargeModel = bool.Parse(row["IS_LARGE_MODEL"].ToString());
            //item.partType = (BasePart.PartType)Enum.Parse(typeof(BasePart.PartType), (string)row["PART_TYPE"], true);
            //item.lDrawPartType = (BasePart.LDrawPartType)Enum.Parse(typeof(BasePart.LDrawPartType), (string)row["LDRAW_PART_TYPE"], true);
            //item.SubPartCount = (int)row["SUB_PART_COUNT"];
            return item;
        }



    }

}
