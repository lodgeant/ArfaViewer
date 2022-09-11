using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Runtime.InteropServices;
using System.IO;
using System.Xml.Linq;
using System.Xml;
using ScintillaNET;


namespace Generator
{
    //TODO_H: Move Delegates class to BaseClasses
    public class Delegates
    {
        public static void ToolStripButton_SetText(Form obj, ToolStripButton tsb, String text)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripButton_SetText(obj, tsb, text)));
            }
            else
            {
                if (text != null) tsb.Text = text;
            }
        }

        public static void ToolStripLabel_SetText(Form obj, ToolStripLabel tsl, String text)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripLabel_SetText(obj, tsl, text)));
            }
            else
            {
                tsl.Text = text;
            }
        }

        public static void ToolStripButton_SetTextAndForeColor(Form obj, ToolStripButton tsb, String text, Color color)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripButton_SetTextAndForeColor(obj, tsb, text, color)));
            }
            else
            {
                if (text != null) tsb.Text = text;
                if (color != null) tsb.ForeColor = color;
            }
        }

        public static void ToolStripProgressBar_SetValue(Form obj, ToolStripProgressBar bar, int value)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripProgressBar_SetValue(obj, bar, value)));
            }
            else
            {
                bar.Value = value;
            }
        }

        public static void ToolStripProgressBar_SetMax(Form obj, ToolStripProgressBar bar, int value)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripProgressBar_SetMax(obj, bar, value)));
            }
            else
            {
                bar.Maximum = value;
            }
        }



        public static void ToolStripComboBox_AddItems(Form obj, ToolStripComboBox tscb, List<string> itemList)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripComboBox_AddItems(obj, tscb, itemList)));
            }
            else
            {
                tscb.Items.AddRange(itemList.ToArray());
                //if (text != null) tsb.Text = text;
                //if (color != null) tsb.ForeColor = color;
            }
        }

        public static void ToolStripComboBox_ClearItems(Form obj, ToolStripComboBox tscb)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ToolStripComboBox_ClearItems(obj, tscb)));
            }
            else
            {
                tscb.Items.Clear();
                
            }
        }



        




        public static void DataGridView_SetDataSource(Form obj, DataGridView dg, object source)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => DataGridView_SetDataSource(obj, dg, source)));
            }
            else
            {
                dg.DataSource = source;
            }
        }


        public static void Scintilla_SetText(Form obj, Scintilla s, String text)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => Scintilla_SetText(obj, s, text)));
            }
            else
            {
                if (text != null) s.Text = text;
            }
        }


        public static void TreeView_AddNodes(Form obj, TreeView tv, TreeNode tn)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => TreeView_AddNodes(obj, tv, tn)));
            }
            else
            {                
                tv.Nodes.Add(tn);
            }
        }

        public static void TreeView_AddRange(Form obj, TreeView tv, TreeNode[] tns)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => TreeView_AddRange(obj, tv, tns)));
            }
            else
            {
                tv.Nodes.AddRange(tns);
            }
        }


        public static void TreeView_ClearNodes(Form obj, TreeView tv)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => TreeView_ClearNodes(obj, tv)));
            }
            else
            {
                tv.Nodes.Clear();
            }
        }

        public static void TreeView_SelectNode(Form obj, TreeView tv, TreeNode tn)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => TreeView_SelectNode(obj, tv, tn)));
            }
            else
            {
                tv.SelectedNode = tn;
                //tv.Select();
                //tv.Focus();
                //tn.Expand();
                //tv.SelectedNode.BackColor = SystemColors.HighlightText;
            }
        }

        public static void GroupBox_AddControlRange(Form obj, GroupBox gb, List<Control> cl)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => GroupBox_AddControlRange(obj, gb, cl)));
            }
            else
            {
                gb.Controls.AddRange(cl.ToArray());                
            }
        }

        public static void Panel_AddControlRange(Form obj, Panel p, List<Control> cl)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => Panel_AddControlRange(obj, p, cl)));
            }
            else
            {
                p.Controls.AddRange(cl.ToArray());
            }
        }





        public static void ImageList_AddItem(Form obj, ImageList il, Bitmap image)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => ImageList_AddItem(obj, il, image)));
            }
            else
            {
                il.Images.Add(image);                
            }
        }




        public static void TabPage_SetText(Form obj, TabPage tp, string text)
        {
            if (obj.InvokeRequired)
            {
                obj.BeginInvoke(new MethodInvoker(() => TabPage_SetText(obj, tp, text)));
            }
            else
            {
               tp.Text = text;                
            }
        }



    }
}
