using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Generator
{
    public partial class PartViewer : Form
    {        
        public static Bitmap image;
        
        public PartViewer()
        {
            InitializeComponent();
            try
            {   
                panel1.BackgroundImage = image;
                if (image != null)
                {
                    this.Width = image.Width + 100;
                    this.Height = image.Height + 100;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
