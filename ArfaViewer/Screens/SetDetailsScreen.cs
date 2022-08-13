using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Generator
{
    public partial class SetDetailsScreen : Form
    {
        // ** Variables **
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        public SetDetailsScreen()
        {
            InitializeComponent();
            try
            {
                // ** Post header **            
                string[] versionArray = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString().Split('.');
                this.Text = "Set Details";
                this.Text += " | " + "v" + versionArray[0] + "." + versionArray[1] + "." + versionArray[2];
                log.Info(".......................................................................SET DETAILS SCREEN STARTED.......................................................................");


                RefreshScreen();

            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

      

        #region ** BUTTON FUNCTIONS **

        private void btnExit_Click(object sender, EventArgs e)
        {
            Dispose();
        }

        #endregion



        private void RefreshScreen()
        {
            try
            {

                // Get all Theme Details from SET_DETAILS
                BaseClasses.ThemeDetailsCollection ThemeDetailsCollection = StaticData.GetAllThemeDetails();



                // Convert ThemeDetails into TreeNode
                TreeNode[] ThemeTreeNodes = null;


                // Set tvThemesSummary
                tvThemesSummary.Nodes.Clear();
                tvThemesSummary.Nodes.AddRange(ThemeTreeNodes);



            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }



    }
}
