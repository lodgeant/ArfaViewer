using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Linq;
//using Microsoft.WindowsAzure.Storage.Blob;


namespace Generator
{
    public class HelperFunctions
    {

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
