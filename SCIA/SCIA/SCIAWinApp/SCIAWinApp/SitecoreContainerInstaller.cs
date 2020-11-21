using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class SitecoreContainerInstaller : Form
    {
        public SitecoreContainerInstaller()
        {
            InitializeComponent();
        }

        private void txtSqlPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlPass_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlDbServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreDbServer_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteNameSuffix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteNamePrefix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUserPassword_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUsername_Leave(object sender, EventArgs e)
        {

        }

        private bool CheckAllValidations(bool uninstall = false, bool generatescript = false)
        {
            return true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;
            //WriteAutoFillFile(".\\" + ZipList.SitecoreContainerZip + "\\compose\\ltsc2019\\xp0\\init.ps1");

            CommonFunctions.LaunchPSScript(".\\init.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + txtSitecoreUsername.Text + "\" -SitecoreAdminPassword \"" + txtSitecoreUserPassword.Text + "\" -SqlSaPassword \"" + txtSqlPass.Text + "\" -LicenseXmlPath \"license.xml\"", ".\\" + ZipList.CommerceContainerZip + "\\compose\\ltsc2019\\xp0");

            lblStatus.Text = ".env file generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
