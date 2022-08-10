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
        public string data;

        public List<string> SubPartList = new List<string>();



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
                if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false)
                {
                    subPartCount += 1;
                }
            }
            return subPartCount;
        }

        public static string GetPartTypeFromLDrawFileText(string LDrawFileText)
        {
            //TODO_H: This function needs to be adjusted to get sub parts correctly.
            string value = "BASIC";
            try
            {
                // ** Get LDraw details for part **
                string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
                foreach (string fileLine in lines)
                {
                    // ** Check if part contains references to any other parts - if it does then the part is COMPOSITE
                    if (fileLine.Trim().StartsWith("1"))
                    {
                        string formattedLine = fileLine.Trim().Replace("   ", " ").Replace("  ", " ");
                        string[] DatLine = formattedLine.Split(' ');
                        string SubPart_LDrawRef = DatLine[14].ToLower().Replace(".dat", "").Replace("s\\", "");
                        ////string SubPart_LDrawFileText = GetLDrawFileDetails(SubPart_LDrawRef);
                        //LDrawDetails ldd = GetLDrawDetails_FromLDrawFile(SubPart_LDrawRef);
                        //string SubPart_LDrawFileText = ldd.data;
                        //if (SubPart_LDrawFileText != "")
                        //{
                        //    value = "COMPOSITE";
                        //    break;
                        //}
                    }
                }
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }





        public static List<string> GetSubPartRefsFromLDrawFileText(string LDrawFileText)
        {
            //int subPartCount = 0;
            //string[] lines = LDrawFileText.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            //foreach (string fileLine in lines)
            //{
            //    if (fileLine.Trim().StartsWith("1") && fileLine.Contains("s\\") == false)
            //    {
            //        subPartCount += 1;
            //    }
            //}
            return null;
        }





        public static LDrawDetails GetLDrawDetailsFromDBDataRow(DataRow row)
        {
            LDrawDetails item = new LDrawDetails();
            item.LDrawRef = (string)row["LDRAW_REF"];
            item.LDrawDescription = (string)row["LDRAW_DESCRIPTION"];
            item.PartType = (string)row["PART_TYPE"];
            item.LDrawPartType = (string)row["LDRAW_PART_TYPE"];
            item.SubPartCount = (int)row["SUB_PART_COUNT"];
            item.data = (string)row["DATA"];
            return item;
        }


    }

}
