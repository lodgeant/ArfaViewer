using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;




namespace BaseClasses
{
    [Serializable]
    public class SetDetails
    {
        [XmlAttribute("Ref")]
        public string Ref { get; set; }

        [XmlAttribute("Description")]
        public string Description { get; set; }

        [XmlAttribute("Type")]
        public string Type { get; set; }

        [XmlAttribute("Theme")]
        public string Theme { get; set; }

        [XmlAttribute("SubTheme")]
        public string SubTheme { get; set; }

        [XmlAttribute("Year")]
        public int Year { get; set; }

        [XmlAttribute("PartCount")]
        public int PartCount { get; set; }

        [XmlAttribute("SubSetCount")]
        public int SubSetCount { get; set; }

        [XmlAttribute("ModelCount")]
        public int ModelCount { get; set; }

        [XmlAttribute("MiniFigCount")]
        public int MiniFigCount { get; set; }

        [XmlAttribute("Status")]
        public string Status { get; set; }

        [XmlAttribute("AssignedTo")]
        public string AssignedTo { get; set; }

        [XmlAttribute("InstructionRefList")]
        public List<string> InstructionRefList { get; set; }


        public SetDetails()
        {
            this.InstructionRefList = new List<string>();
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

        public SetDetails DeserialiseFromXMLString(String XMLString)
        {
            // ** IMPROVED METHOD **           
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (SetDetails)serializer.Deserialize(reader);
            }
        }

        public static SetDetails GetSetDetailsFromDBDataRow(DataRow row)
        {
            SetDetails item = new SetDetails();
            item.Ref = (string)row["REF"];
            item.Description = (string)row["DESCRIPTION"];            
            item.Type = (string)row["TYPE"];
            item.Theme = (string)row["THEME"];
            item.SubTheme = (string)row["SUB_THEME"];
            item.Year = (int)row["YEAR"];
            item.PartCount = (int)row["PART_COUNT"];
            item.SubSetCount = (int)row["SUBSET_COUNT"];
            item.ModelCount = (int)row["MODEL_COUNT"];
            item.MiniFigCount = (int)row["MINIFIG_COUNT"];
            item.Status = (string)row["STATUS"];
            item.AssignedTo = (string)row["ASSIGNED_TO"];            
            string InstructionRefsListString = ((string)row["INSTRUCTION_REFS"]);
            item.InstructionRefList = new List<string>();
            if (InstructionRefsListString != "") item.InstructionRefList = InstructionRefsListString.Split(',').ToList();            
            return item;
        }

    }
}
