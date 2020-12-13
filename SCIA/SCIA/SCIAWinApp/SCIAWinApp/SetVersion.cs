using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class SetVersion : Form
    {
        public SetVersion()
        {
            InitializeComponent();
            GetZipVersionsData();
        }

        private void GetZipVersionsData()
        {
            if (string.IsNullOrWhiteSpace(CommonFunctions.ConnectionString))
            {
                lblStatus.Text = "Error getting Versions Data";
                lblStatus.ForeColor = Color.Red;
                return;
            }
                
            var sql = "select distinct Version from ZipVersions";

            try
            {
                using SqlConnection conn = new SqlConnection(CommonFunctions.ConnectionString);
                conn.Open();
                SqlDataAdapter da = new SqlDataAdapter(sql, conn);
                DataSet ds = new DataSet();
                da.Fill(ds, "ZipVersions");
                cmbVersionList.DisplayMember = "Version";
                cmbVersionList.ValueMember = "Version";
                cmbVersionList.DataSource = ds.Tables["ZipVersions"];

            }
            catch (SqlException ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Unable to retrieve data from ZipVersions Table" + ex.Message, EventLogEntryType.Error);
                lblStatus.Text = "Error loading Versions Data";
                lblStatus.ForeColor = Color.Red;
            }
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            Version.SitecoreVersion = cmbVersionList.Text;
            Form frm = (Form)this.MdiParent;
            ToolStrip toolStrip = (ToolStrip)frm.Controls["toolStrip"];
            ToolStripButton toolCommerceStripButton = (ToolStripButton)toolStrip.Items["sitecoreCommerceToolStripButton"];
            ToolStripButton toolSitecoreConStripButton = (ToolStripButton)toolStrip.Items["sitecoreContainerToolStripButton"];
            ToolStripButton toolsiaStripButton = (ToolStripButton)toolStrip.Items["siaToolStripButton"];
            ToolStripButton toolsifStripButton = (ToolStripButton)toolStrip.Items["siftoolStripButton"];
            ToolStripButton toolCommerceConStripButton = (ToolStripButton)toolStrip.Items["sitecoreCommerceContainerToolStripButton"];
            ToolStripButton toolStripSolrButton = (ToolStripButton)toolStrip.Items["toolStripSolrButton"];

            toolCommerceStripButton.Enabled =true;
            toolSitecoreConStripButton.Enabled = true;
            toolsifStripButton.Enabled = true;
            toolsiaStripButton.Enabled = true;
            toolCommerceConStripButton.Enabled = true;
            toolStripSolrButton.Enabled = true;

            ZipList.CommerceZip = CommonFunctions.GetZipNamefromWdpVersion("commerce", Version.SitecoreVersion);
            if (ZipList.CommerceZip == null) { toolCommerceStripButton.Enabled = false; }
            ZipList.CommerceContainerZip = CommonFunctions.GetZipNamefromWdpVersion("commercecon", Version.SitecoreVersion);
            if (ZipList.CommerceContainerZip == null) { toolCommerceConStripButton.Enabled = false;  }
            ZipList.SitecoreContainerZip = CommonFunctions.GetZipNamefromWdpVersion("sitecorecon", Version.SitecoreVersion);
            if (ZipList.SitecoreContainerZip == null) { toolSitecoreConStripButton.Enabled = false;  }
            ZipList.SitecoreDevSetupZip = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);
            ZipList.SitecoreSifZip = CommonFunctions.GetZipNamefromWdpVersion("sitecoresif", Version.SitecoreVersion);
            if (ZipList.SitecoreSifZip == null) { toolsifStripButton.Enabled = false; }
            if (ZipList.SitecoreDevSetupZip == null) {
                toolsiaStripButton.Enabled = false;
            }
            else
            {
                toolsifStripButton.Enabled = true;
            }

            if (!CommonFunctions.FileSystemEntryExists("C:\\Program Files\\WindowsPowerShell\\Modules\\SitecoreInstallFramework", null, "folder", true))
            {
                toolsifStripButton.Enabled = false;
                toolStripSolrButton.Enabled = false;
                SetStatusMessage("Missing-C:\\ProgramFiles\\WindowsPowerShell\\Modules\\SitecoreInstallFramework....", Color.Red);
                return;
            }

            this.Hide();
        }

    }
}
