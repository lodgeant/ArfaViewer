﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;

namespace BaseClasses
{
    [Serializable]
    public class SetDetails
    {
        [XmlAttribute("Ref")]
        public string Ref;

        [XmlAttribute("Description")]
        public string Description;

        [XmlAttribute("Type")]
        public string Type;

        [XmlAttribute("Theme")]
        public string Theme;

        [XmlAttribute("SubTheme")]
        public string SubTheme;

        [XmlAttribute("Year")]
        public int Year;

        [XmlAttribute("PartCount")]
        public int PartCount;

        [XmlAttribute("SubSetCount")]
        public int SubSetCount;

        [XmlAttribute("ModelCount")]
        public int ModelCount;

        [XmlAttribute("MiniFigCount")]
        public int MiniFigCount;

        [XmlAttribute("Status")]
        public string Status;

        [XmlAttribute("AssignedTo")]
        public string AssignedTo;

        [XmlAttribute("Instructions")]
        public string Instructions;



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
            item.Instructions = row["INSTRUCTIONS"].ToString();             
            return item;
        }

    }
}
