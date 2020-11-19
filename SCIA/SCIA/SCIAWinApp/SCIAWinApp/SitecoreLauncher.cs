using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
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

        public mdiSitecoreComplete()
        {
            InitializeComponent();
            SCIASettings.FilePrefixAppString = "SCIA-";
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
            if (string.IsNullOrWhiteSpace(CommonFunctions.ConnectionString)) {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is DBConnect)
                    {
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Focus();
                        return;
                    }
                }
                DBConnect formInstance = new DBConnect
                {
                    MdiParent = this
                };
                formInstance.Show();
            }
            
        }

        private void sitecoreCommerceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SitecoreCommerceInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SitecoreCommerceInstaller formInstance = new SitecoreCommerceInstaller
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void commerceContainerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SitecoreCommerceContainerInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SitecoreCommerceContainerInstaller formInstance = new SitecoreCommerceContainerInstaller
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void sitecoreCommerceContainerToolStripButton_Click(object sender, EventArgs e)
        {

            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SitecoreCommerceContainerInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SitecoreCommerceContainerInstaller formInstance = new SitecoreCommerceContainerInstaller
            {
                MdiParent = this
            };
            formInstance.Show();
        }


        private void sitecoreCommerceToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SitecoreCommerceInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SitecoreCommerceInstaller formInstance = new SitecoreCommerceInstaller();
            formInstance.MdiParent = this;
            formInstance.Show();
        }

        private void siaToolStripButton_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\" + ZipList.SitecoreDevSetupZip + "\\setup.exe"))
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is SiaPrerequisites)
                    {
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Focus();
                        return;
                    }
                }
                SiaPrerequisites formInstance = new SiaPrerequisites
                {
                    MdiParent = this
                };
                formInstance.Show();
                return;
            }
            ProcessStartInfo processStartInfo = new ProcessStartInfo();

            processStartInfo.WorkingDirectory = ZipList.SitecoreDevSetupZip;
            processStartInfo.FileName = "setup.exe";
            System.Diagnostics.Process.Start(processStartInfo);
        }

        private void linkLabel1_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SdnLoginForm)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SdnLoginForm formInstance = new SdnLoginForm
            {
                MdiParent = this
            };
            formInstance.Show();
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
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SdnLoginForm)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SdnLoginForm formInstance = new SdnLoginForm
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void installToolStripMenuItem_Click(object sender, EventArgs e)
        {
           
        }

        private void siaUninstalltoolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void siaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!File.Exists(@".\\" + ZipList.SitecoreDevSetupZip + "\\setup.exe"))
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is SdnLoginForm)
                    {
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Focus();
                        return;
                    }
                }
                SiaPrerequisites formInstance = new SiaPrerequisites
                {
                    MdiParent = this
                };
                formInstance.Show();
                return;
            }
            System.Diagnostics.Process.Start(@".\\" + ZipList.SitecoreDevSetupZip + "\\setup.exe");
        }

        private void toolStripButtonSetVersion_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SetVersion)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SetVersion formInstance = new SetVersion
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is DBConnect)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            DBConnect formInstance = new DBConnect
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripMenuItemDbConn_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is DBConnect)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            DBConnect formInstance = new DBConnect
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripMenuItemSetVersion_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SetVersion)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SetVersion formInstance = new SetVersion
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void ToggleMenuandButtonAccess(bool access)
        {
            toolStripButtonSetVersion.Enabled = access;
            toolStripMenuItemSetVersion.Enabled = access;
            sitecoreCommerceToolStripButton.Enabled = access;
            sitecoreCommerceToolStripMenuItem.Enabled = access;
            sitecoreCommerceContainerToolStripButton.Enabled = access;
            commerceContainerToolStripMenuItem.Enabled = access;
            sitecoreContainerToolStripMenuItem.Enabled = false;
            sitecoreContainerToolStripButton.Enabled = access;
            siaToolStripButton.Enabled = access;
            siaToolStripMenuItem.Enabled = access;
            siftoolStripButton.Enabled = access;
            sifStripMenuItem.Enabled = access;
            siaToolStripMenuItem.Enabled = access;
            toolStripSolrButton.Enabled = access;
        }

        private void mdiSitecoreComplete_Shown(object sender, EventArgs e)
        {
            ToggleMenuandButtonAccess(false);

            if (!CommonFunctions.CheckandSetupZipVersionsTable()) { CommonFunctions.WritetoEventLog("Unable to setup Zip Versions Data", System.Diagnostics.EventLogEntryType.Error, "Application"); return; };

            if (string.IsNullOrWhiteSpace(Version.SitecoreVersion))
            {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is SetVersion)
                    {
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Focus();
                        return;
                    }
                }
                SetVersion formInstance = new SetVersion
                {
                    MdiParent = this
                };
                formInstance.Show();
            }

            Version.SitecoreVersion = Version.SitecoreVersion;//must be stored and picked from a local file since dbconn will be established only after this
            ZipList.CommerceZip = CommonFunctions.GetZipNamefromWdpVersion("commerce", Version.SitecoreVersion);
            if (!string.IsNullOrWhiteSpace(ZipList.CommerceZip)) { sitecoreCommerceToolStripButton.Enabled = true; }
            ZipList.CommerceContainerZip = CommonFunctions.GetZipNamefromWdpVersion("commercecon", Version.SitecoreVersion);
            if (!string.IsNullOrWhiteSpace(ZipList.CommerceContainerZip)) { sitecoreCommerceContainerToolStripButton.Enabled = true; }
            ZipList.SitecoreContainerZip = CommonFunctions.GetZipNamefromWdpVersion("sitecorecon", Version.SitecoreVersion);
            if (!string.IsNullOrWhiteSpace(ZipList.SitecoreContainerZip)) { sitecoreContainerToolStripButton.Enabled = true; }
            ZipList.SitecoreDevSetupZip = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);
            if (!string.IsNullOrWhiteSpace(ZipList.SitecoreDevSetupZip)) { 
                siaToolStripButton.Enabled = true;
                siftoolStripButton.Enabled = true;
            }
        }

       
        private void sifltoolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SIFSitecoreInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SIFSitecoreInstaller formInstance = new SIFSitecoreInstaller
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripButtonSIFModuleInstall_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.FileSystemEntryExists(".\\" + SCIASettings.FilePrefixAppString + "InstallSIF.ps1", null))
            {
                WriteFile(SCIASettings.FilePrefixAppString + "InstallSIF.ps1");
            }
            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "InstallSIF.ps1");

            toolStripButtonSIFModuleInstall.Enabled = false;
        }

        void WriteFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("Register-PSRepository -Name SitecoreGallery https://sitecore.myget.org/F/sc-powershell/api/v2");
            file.WriteLine("Install-Module SitecoreInstallFramework");
            file.Dispose();
        }

        private void toolStripButton1_Click_1(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is SolrInstaller)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            SolrInstaller formInstance = new SolrInstaller
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripSolrDeleteButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is ClearAll)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            ClearAll formInstance = new ClearAll
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripDeleteDBButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is ClearDB)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            ClearDB formInstance = new ClearDB
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripDeleteWebsiteButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is DeleteAllLauncher)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            DeleteAllLauncher formInstance = new DeleteAllLauncher
            {
                MdiParent = this
            };
            formInstance.Show();
        }

        private void toolStripPortAvailabilityButton_Click(object sender, EventArgs e)
        {
            foreach (Form frm in this.MdiChildren)
            {
                if (frm is PortAvailabilityChecker)
                {
                    if (frm.WindowState == FormWindowState.Minimized)
                        frm.WindowState = FormWindowState.Normal;
                    frm.Focus();
                    return;
                }
            }
            PortAvailabilityChecker formInstance = new PortAvailabilityChecker
            {
                MdiParent = this
            };
            formInstance.Show();
        }
    }
}
