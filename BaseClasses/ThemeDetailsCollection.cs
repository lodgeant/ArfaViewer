using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.Data;
using System.Linq;
using System.Windows.Forms;


namespace BaseClasses
{
    [Serializable]
    public class ThemeDetailsCollection
    {
        [XmlElement("ThemeDetails")]
        public List<ThemeDetails> ThemeDetailsList = new List<ThemeDetails>();

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

        public ThemeDetailsCollection DeserialiseFromXMLString(string XMLString)
        {
            var serializer = new XmlSerializer(this.GetType());
            using (TextReader reader = new StringReader(XMLString))
            {
                return (ThemeDetailsCollection)serializer.Deserialize(reader);
            }
        }

        public TreeNode[] ConvertToTreeNodeList()
        {
            List<TreeNode> TreeNodeList = new List<TreeNode>();
            foreach(ThemeDetails themeDetail in this.ThemeDetailsList)
            {
                TreeNode themeNode = new TreeNode() { Text = themeDetail.Theme, Tag = themeDetail.Theme };
                if(themeDetail.SubThemeList.Count > 0)
                {
                    foreach(string subTheme in themeDetail.SubThemeList)
                    {
                        TreeNode subThemeNode = new TreeNode() { Text = subTheme, Tag = themeDetail.Theme + "|" + subTheme };
                        themeNode.Nodes.Add(subThemeNode);
                    }
                }
                TreeNodeList.Add(themeNode);
            }
            return TreeNodeList.ToArray();
        }






        // ** STATIC FUNCTIONS **

        public static ThemeDetailsCollection GetThemeDetailsCollectionFromDataTable(DataTable table)
        {
            ThemeDetailsCollection coll = new ThemeDetailsCollection();
            foreach (DataRow row in table.Rows)
            {
                // ** Get variables **
                string theme = (string)row["THEME"];
                string subTheme = row["SUB_THEME"].ToString();

                // ** Check if Theme already exists in the collection, if not add it **
                ThemeDetails td = new ThemeDetails();
                var existingTheme = (from r in coll.ThemeDetailsList
                                     where r.Theme == theme
                                     select r).FirstOrDefault();
                if (existingTheme == null) coll.ThemeDetailsList.Add(new ThemeDetails() { Theme = theme });

                // ** Check if SubTheme already exists in the collection, if not add it **
                if (subTheme != "")
                {
                    var existingSubTheme = (from r in coll.ThemeDetailsList
                                            where r.Theme == theme
                                            && r.SubThemeList.Contains(subTheme)
                                            select r).FirstOrDefault();
                    if (existingSubTheme == null)
                    {
                        var themeDetails = (from r in coll.ThemeDetailsList
                                            where r.Theme == theme
                                            select r).FirstOrDefault();
                        themeDetails.SubThemeList.Add(subTheme);
                    }
                }
            }
            return coll;
        }


       



    }


}
