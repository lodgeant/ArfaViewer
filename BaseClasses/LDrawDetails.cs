using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace BaseClasses
{
    public class LDrawDetails
    {
        public string LDrawRef;
        public string LDrawDescription;
        public string PartType;
        public string LDrawPartType;
        public int SubPartCount;
        public string Data;
        public List<string> SubPartLDrawRefList = new List<string>();



        public static string GetLDrawDescriptionFromLDrawFileText(string LDrawFileText)
        {
            string LDrawDescription = "";
            if (LDrawFileText != "")
            {
                string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                LDrawDescription = lines[0].Replace("0 ", "");
            }
            return LDrawDescription;
        }

        public static int GetSubPartCountFromLDrawFileText(string LDrawFileText)
        {
            int subPartCount = 0;
            string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string fileLine in lines)
            {
                if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false) subPartCount += 1;               
            }
            return subPartCount;
        }

        public static string GetPartTypeFromLDrawFileText(string LDrawFileText)
        {
            string value = "BASIC";
            List<string> subPartList = LDrawDetails.GetSubPartLDrawRefsFromLDrawFileText(LDrawFileText);
            if (subPartList.Count > 0) value = "COMPOSITE";
            return value;
        }

        public static List<string> GetSubPartLDrawRefsFromLDrawFileText(string LDrawFileText)
        {
            List<string> SubPartLDrawRefList = new List<string>();
            string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            foreach (string fileLine in lines)
            {
                if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false)
                {
                    string formattedLine = fileLine.Trim().Replace("   ", " ").Replace("  ", " ");
                    string[] DatLine = formattedLine.Split(' ');
                    string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "");
                    int SubPart_LDrawColourID = int.Parse(DatLine[1]);
                    if (SubPart_LDrawColourID == 16) SubPart_LDrawColourID = -1;    // Assumes that Sub Parts don't make reference to other sub parts. Would need to change this if any Sub Parts are "c"
                    SubPartLDrawRefList.Add(SubPart_LDrawRef + "|" + SubPart_LDrawColourID);  
                }
            }
            return SubPartLDrawRefList;
        }

        public static LDrawDetails GetLDrawDetailsFromDBDataRow(DataRow row)
        {
            LDrawDetails item = new LDrawDetails();
            item.LDrawRef = (string)row["LDRAW_REF"];
            item.LDrawDescription = (string)row["LDRAW_DESCRIPTION"];
            item.PartType = (string)row["PART_TYPE"];
            item.LDrawPartType = (string)row["LDRAW_PART_TYPE"];
            item.SubPartCount = (int)row["SUB_PART_COUNT"];
            item.Data = (string)row["DATA"];
            item.SubPartLDrawRefList = ((string)row["SUB_PART_LDRAW_REF_LIST"]).Split(',').ToList();
            return item;
        }


    }

}
