using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using System.Xml.Linq;

using System.Text;
using System.Windows.Forms;






namespace BaseClasses
{
    public class HelperFunctions
    {

        //Implemented based on interface, not part of algorithm
        public static string RemoveAllNamespaces(string xmlDocument)
        {
            XElement xmlDocumentWithoutNs = RemoveAllNamespaces(XElement.Parse(xmlDocument));
            return xmlDocumentWithoutNs.ToString();
        }

        public static XElement RemoveAllNamespaces(XElement e)
        {
            return new XElement(e.Name.LocalName,
              (from n in e.Nodes()
               select ((n is XElement) ? RemoveAllNamespaces(n as XElement) : n)),
                  (e.HasAttributes) ?
                    (from a in e.Attributes()
                     where (!a.IsNamespaceDeclaration)
                     select new XAttribute(a.Name.LocalName, a.Value)) : null);
        }


        public static StringBuilder GenerateClipboardStringFromDataTable(DataGridView dg)
        {
            StringBuilder sb = new StringBuilder();
            foreach (DataGridViewColumn column in dg.Columns)
            {
                if (column.Visible && column.ValueType.Name != "Bitmap") sb.Append(column.Name + ",");
            }
            sb.Append(Environment.NewLine);
            foreach (DataGridViewRow row in dg.Rows)
            {
                foreach (DataGridViewCell cell in row.Cells)
                {
                    if (cell.OwningColumn.Visible && cell.ValueType.Name != "Bitmap") sb.Append(cell.Value + ",");
                }
                sb.Append(Environment.NewLine);
            }
            return sb;
        }


    }


}
