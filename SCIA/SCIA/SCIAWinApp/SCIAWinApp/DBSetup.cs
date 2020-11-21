using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows.Forms;

namespace SCIA
{
    public partial class DBSetup : Form
    {
        public DBSetup()
        {
            InitializeComponent();
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {

            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void btnCreate_Click(object sender, EventArgs e)
        {
            btnCreate.Enabled = false;

            if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", DBDetails.DbServer, DBDetails.SqlUser, DBDetails.SqlPass))
            {
                using SqlConnection sqlConn = CommonFunctions.CreateDatabase("SCIA_DB", DBDetails.DbServer, DBDetails.SqlUser, DBDetails.SqlPass);
                if (sqlConn == null) { SetStatusMessage("Unable to create SCIA_DB, Check details entered and try again....", Color.Red); return; }
            }

            using TransactionScope scope = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(CommonFunctions.BuildConnectionString(DBDetails.DbServer, "SCIA_DB", DBDetails.SqlUser, DBDetails.SqlPass)))
            {
                connection.Open();

                if (chkVersionPrerequisites.Checked)
                {
                    if (CommonFunctions.CreateVersionPrerequisitesTable(connection) == 0)
                    {
                        { SetStatusMessage("Unable to create VersionPrerequisites table, Check details entered and try again....", Color.Red); return; }
                    }

                    if (CommonFunctions.InsertDataintoVersionPrerequisitesTable(connection) == 0)
                    {
                        { SetStatusMessage("Unable to insert data into VersionPrerequisites table, Check details entered and try again....", Color.Red); return; }
                    }
                }

                if (chkZipVersions.Checked)
                {
                    if (CommonFunctions.CreateZipVersionsTable(connection) == 0)
                    {
                        { SetStatusMessage("Unable to create ZipVersions table, Check details entered and try again....", Color.Red); return; }
                    }

                    if (CommonFunctions.InsertDataintoZipVersionsTable(connection) == 0)
                    {
                        { SetStatusMessage("Unable to insert data into ZipVersions table, Check details entered and try again....", Color.Red); return; }
                    }
                }
            }

            // if all the coperations complete successfully, this would be called and commit the transaction. 
            // In case of an exception, it wont be called and transaction is rolled back
            scope.Complete();

            SetStatusMessage("DB Tables Setup Successfully", Color.Green); return;
        }
    }
}
