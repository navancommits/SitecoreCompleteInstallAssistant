using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SCIA
{
    public partial class DBConnect : Form
    {
        public DBConnect()
        {
            InitializeComponent();
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "master", txtSqlUser.Text, txtSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                return;
            }
            if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                using SqlConnection sqlConnection = CommonFunctions.CreateDatabase("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text);
                if (sqlConnection==null) { SetStatusMessage("Check Connection Details, Unable to create SCIA_DB", Color.Red); }
                return;
            }

            DBDetails.DbServer = txtSqlDbServer.Text;
            DBDetails.SqlUser = txtSqlUser.Text;
            DBDetails.SqlPass = txtSqlPass.Text;
            CommonFunctions.ConnectionString=CommonFunctions.BuildConnectionString(txtSqlDbServer.Text,"SCIA_DB" , txtSqlUser.Text, txtSqlPass.Text);

            //if (!CommonFunctions.CheckandSetupZipVersionsTable()) { CommonFunctions.WritetoEventLog("Unable to setup Zip Versions Data", System.Diagnostics.EventLogEntryType.Error, "Application"); return; };

            Form frm = (Form)this.MdiParent;
            ToolStrip toolStrip = (ToolStrip)frm.Controls["toolStrip"];
            ToolStripButton toolSetVersionMenuButton = (ToolStripButton)toolStrip.Items["toolStripButtonSetVersion"];
            MenuStrip menuStrip = (MenuStrip)frm.Controls["menuStrip"];
            ToolStripMenuItem toolSetVersionMenuItem = (ToolStripMenuItem)menuStrip.Items["toolStripMenuItemSetVersion"];
            ToolStripMenuItem toolStripMenuItemDbConn = (ToolStripMenuItem)menuStrip.Items["toolStripMenuItemDbConn"];
            ToolStripButton toolStripDBConnectButton = (ToolStripButton)toolStrip.Items["toolStripDBConnectButton"];
            ToolStripButton toolStripDBDeleteButton = (ToolStripButton)toolStrip.Items["toolStripDeleteDBButton"];
            ToolStripButton toolStripSetupDBButton = (ToolStripButton)toolStrip.Items["toolStripSetupDBButton"];
            toolSetVersionMenuButton.Enabled = true;
            toolSetVersionMenuItem.Enabled = true;
            toolStripMenuItemDbConn.Enabled = true;
            toolStripDBDeleteButton.Enabled = true;
            toolStripSetupDBButton.Enabled = true;

            toolStripDBConnectButton.Enabled = false;
            SetStatusMessage("Successfully established DB Connection", Color.Green);
            this.Hide();
        }
    }
}
