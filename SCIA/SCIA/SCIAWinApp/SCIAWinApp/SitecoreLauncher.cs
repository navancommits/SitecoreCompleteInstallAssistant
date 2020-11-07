using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class mdiSitecoreComplete : Form
    {
        private int childFormNumber = 0;

        public mdiSitecoreComplete()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void mdiSitecoreComplete_Load(object sender, EventArgs e)
        {

        }

        private void sitecoreCommerceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SitecoreCommerceInstaller sitecoreCommerceInstaller = new SitecoreCommerceInstaller();
            sitecoreCommerceInstaller.ShowDialog();
        }

        private void commerceContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SitecoreCommerceContainerInstaller sitecoreCommerceContainerInstaller = new SitecoreCommerceContainerInstaller();
            sitecoreCommerceContainerInstaller.ShowDialog();
        }

        private void sitecoreCommerceContainerToolStripButton_Click(object sender, EventArgs e)
        {
            SitecoreCommerceContainerInstaller sitecoreCommerceContainerInstaller = new SitecoreCommerceContainerInstaller();
            sitecoreCommerceContainerInstaller.ShowDialog();
        }

        private void sitecoreCommerceToolStripButton_Click(object sender, EventArgs e)
        {
            SitecoreCommerceInstaller sitecoreCommerceInstaller = new SitecoreCommerceInstaller();
            sitecoreCommerceInstaller.ShowDialog();
        }

        private void siaToolStripButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe"))
            {
                SiaPrerequisites siaPrerequisites = new SiaPrerequisites();
                siaPrerequisites.ShowDialog();
                return;
            }
            System.Diagnostics.Process.Start(@"Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe");
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {            
            SdnLoginForm sdnLoginForm = new SdnLoginForm();
            sdnLoginForm.ShowDialog();
            
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Login.Success = false;
           
        }

        private void sdnLogintoolStripButton_Click(object sender, EventArgs e)
        {
            
            if (Login.Success)
            {
                
                Login.Success = false;
                Login.username = string.Empty;
                Login.password = string.Empty;
                Login.rememberMe = false;
                sdnLogintoolStripButton.ToolTipText = "SDN Login";
                return;
            }

            sdnLogintoolStripButton.ToolTipText = "SDN Logout";
            SdnLoginForm sdnLoginForm = new SdnLoginForm();
            sdnLoginForm.ShowDialog();
        }

        private void installToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void siaUninstalltoolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void siaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe"))
            {
                SiaPrerequisites siaPrerequisites = new SiaPrerequisites();
                siaPrerequisites.ShowDialog();
                return;
            }
            System.Diagnostics.Process.Start(@"Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe");
        }
    }
}
