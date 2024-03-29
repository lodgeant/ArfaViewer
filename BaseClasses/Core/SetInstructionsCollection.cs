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
    public class SetInstructionsCollection
    {
        [XmlElement("SetInstructions")]
        public List<SetInstructions> SetInstructionsList { get; set; }

        public SetInstructionsCollection()
        {
            SetInstructionsList = new List<SetInstructions>();
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

        public SetInstructionsCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (SetInstructionsCollection)serializer.Deserialize(reader);
            }
        }

        public static SetInstructionsCollection GetSetInstructionsCollectionFromDataTable(DataTable table)
        {
            SetInstructionsCollection coll = new SetInstructionsCollection();
            foreach (DataRow row in table.Rows)
            {
                SetInstructions item = SetInstructions.GetSetInstructionsFromDBDataRow(row);
                coll.SetInstructionsList.Add(item);
            }
            return coll;
        }


        public static DataTable GetDatatableFromSetInstructionsCollection(SetInstructionsCollection coll)
        {
            DataTable table = new DataTable("", "");
            table.Columns.Add("Ref", typeof(string));
            table.Columns.Add("Data", typeof(string));            
            foreach (SetInstructions item in coll.SetInstructionsList)
            {
                DataRow newRow = table.NewRow();
                newRow["Ref"] = item.Ref;
                newRow["Data"] = item.Data;               
                table.Rows.Add(newRow);
            }
            return table;
        }





    }
}
