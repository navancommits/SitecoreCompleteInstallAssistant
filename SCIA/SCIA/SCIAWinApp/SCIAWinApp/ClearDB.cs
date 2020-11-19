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
    public partial class ClearDB : Form
    {
        public ClearDB()
        {
            InitializeComponent();
        }

        private void DeleteScript(string path)
        {
            var alterDbStmtstring = "alter DATABASE [";
            var setStmtString = "set single_user with rollback immediate";
            var dropStmtstring = "DROP DATABASE IF EXISTS [";
            var coreDBSuffix = "_Core";
            var masterDBSuffix = "_Master";
            var webDBSuffix = "_Web";
            var exmMasterDBSuffix = "_EXM.Master";
            var refDataDBSuffix = "_ReferenceData";
            var reportingDBSuffix = "_Reporting";
            var expFormsDBSuffix = "_ExperienceForms";
            var marketingAutomationDBSuffix = "_MarketingAutomation";
            var processingPoolsDBSuffix = "_Processing.Pools";
            var processingTasksDBSuffix = "_Processing.Tasks";
            var processingEngineStorageDBSuffix = "_ProcessingEngineStorage";
            var processingEngineTasksDBSuffix = "_ProcessingEngineTasks";
            var collectionShard0DBSuffix = "_Xdb.Collection.Shard0";
            var collectionShard1DBSuffix = "_Xdb.Collection.Shard1";
            var collectionShardMapManagerDBSuffix = "_Xdb.Collection.ShardMapManager";
            var messagingDBSuffix = "_Messaging";
            var globalDBSuffix = "_SitecoreCommerce_Global";
            var sharedenvDBSuffix = "_SitecoreCommerce_SharedEnvironments";

            using var file = new StreamWriter(path);
            file.WriteLine("Param(");
            if (rbDBPrefixName.Checked)
            {
                file.WriteLine("\t[string]$Prefix = \"" + txtNameString.Text + "\",");
                file.WriteLine("\t[string]$CommDbPrefix = \"" + txtNameString.Text + "_SitecoreCommerce" + "\",");
            }
            else
            {
                file.WriteLine("\t[string]$DbName = \"" + txtNameString.Text + "\",");
            }
            file.WriteLine("\t[string]$SqlServer = \"" + DBDetails.DbServer + "\",");
            file.WriteLine("\t[string]$SqlAccount = \"" + DBDetails.SqlUser + "\",");
            file.WriteLine("\t[string]$SqlPassword = \"" + DBDetails.SqlPass + "\"");
            file.WriteLine(")");
            file.WriteLine();


            file.WriteLine("#Drop databases from SQL");
            file.WriteLine("Write-Host \"Dropping databases from SQL server\"");
            file.WriteLine("push-location");
            file.WriteLine("import-module sqlps");
            //alter database YourDb set single_user with rollback immediate
            file.WriteLine("Write-Host \"Dropping databases from SQL server\"");
            if (rbDBPrefixName.Checked)
            {
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + coreDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + coreDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + masterDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + masterDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + webDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + webDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + exmMasterDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + exmMasterDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + refDataDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + refDataDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + reportingDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + reportingDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + expFormsDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + expFormsDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + marketingAutomationDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + marketingAutomationDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + processingPoolsDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + processingPoolsDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + processingTasksDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + processingTasksDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + processingEngineStorageDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + processingEngineStorageDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + processingEngineTasksDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + processingEngineTasksDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + collectionShard0DBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + collectionShard0DBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + collectionShard1DBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + collectionShard1DBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + collectionShardMapManagerDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + collectionShardMapManagerDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + messagingDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + messagingDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + sharedenvDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + sharedenvDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $Prefix +\"" + globalDBSuffix + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + globalDBSuffix + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            }
            else
            {
                file.WriteLine("$sitecoreDbPrefix = \"" + alterDbStmtstring + "\"+ $DbName +\"" + "] " + setStmtString + "\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
                file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $DbName +\""  + "]\"");
                file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            }
            file.WriteLine("Write-Host \"Databases dropped successfully\"");
            file.WriteLine();

            file.WriteLine("pop-location");
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtNameString.Text)) { SetStatusMessage("DB Prefix/Name required....", Color.Red); return; }

            DeleteScript(SCIASettings.FilePrefixAppString + txtNameString.Text + "_Delete_DB_Script.ps1");
            DeleteAll deleteAll = new DeleteAll(SCIASettings.FilePrefixAppString + txtNameString.Text + "_Delete_DB_Script.ps1");
            deleteAll.ShowDialog();
        }

        private void ClearDB_Load(object sender, EventArgs e)
        {

        }
    }
}
