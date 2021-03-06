using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Transactions;
using System.Windows.Forms;

namespace SCIA
{
    public partial class HorizonInstallerForm : Form
    {
        const int const_DBConn_Tab = 0;
        const int const_SiteInfo_Tab = 1;
        const int const_General_Tab = 2;
        const int const_Install_Details_Tab = 3;
        const int const_Sitecore_Tab = 4;
        const int const_Solr_Tab = 5;
        string destFolder = ".";
        List<VersionPrerequisites> prereqs;
        bool xp0Install;
        string siteNamePrefixString = "sc";//sc
        string horizonSiteNameSuffixString = "horizon";
        string identityServerNameString = "identityserver";//identityserver
        string siteRootDir = "c:\\inetpub\\wwwroot";//"c:\\intetpub\\wwwroot\\
        private SettingsData settingsData { get; set; }
        public int TabIndexValue { get; set; }
        public HorizonInstallerForm()
        {
            InitializeComponent();
            this.Text = this.Text + " for Sitecore v" + Version.SitecoreVersion;
            switch (Version.SitecoreVersion)
            {
                case "10.1.0":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("horizondevsetup", Version.SitecoreVersion);
                    prereqs = CommonFunctions.GetVersionPrerequisites(Version.SitecoreVersion, "horizondevsetup");
                    break;               
                default:
                    break;
            }
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
            ToggleEnableControls(false);
            AssignStepStatus(const_DBConn_Tab);
            txtSqlDbServer.Text = DBDetails.DbServer;
            txtSqlPass.Text = DBDetails.SqlPass;
            txtSqlUser.Text = DBDetails.SqlUser;
        }


        void WriteFile(string path, bool uninstallscript)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("param (");
            file.WriteLine("  [string]$horizonInstanceName= \"" + txtHorizonInstanceName.Text + "\",");
            file.WriteLine("  [string]$horizonPhysicalPath = \"" + txtHorizonInstallDir.Text + "\",");
            file.WriteLine("  [string]$horizonAppUrl = \"" + txtHorizonAppUrl.Text + "\",");
            file.WriteLine("  [string]$sitecoreCmInstanceName = \"" + txtSiteName.Text + "\",");
            file.WriteLine("  [string]$sitecoreAdminPassword= \"" + txtSitecoreUserPassword.Text + "\",");
            file.WriteLine("  [string]$sitecoreCmInstanceUrl = \"" + txtSiteUrl.Text + "\",");
            file.WriteLine("  [string]$sitecoreCmInstanceInternalUrl = $sitecoreCmInstanceUrl,");
            file.WriteLine("  [string]$sitecoreCmInstancePath = \"" + txtSXAInstallDir.Text + "\",");
            file.WriteLine("  [string]$identityServerPoolName = \"" + txtIDServerAppPoolName.Text + "\",");
            file.WriteLine("  [string]$identityServerUrl = \"" + txtSitecoreIdentityServerUrl.Text + "\",");
            file.WriteLine("  [string]$identityServerPhysicalPath= \"" + txtIDServerInstallDir.Text + "\",");
            file.WriteLine("  [string]$solrCorePrefix = $($sitecoreCmInstanceName),");
            file.WriteLine("  [string]$licensePath=\"$PSScriptRoot\\license.xml\",");
            file.WriteLine("  [bool]$enableContentHub=$false,");
            file.WriteLine("  [bool]$enableSXA=$false,");
            file.WriteLine("  [ValidateSet(\"XP\", \"XM\")]");
            file.WriteLine("  [string]$topology=\"XP\"");
            file.WriteLine(")");
            file.WriteLine("Import-Module \"$PSScriptRoot\\InstallerModules.psm1\" -Force");
            file.WriteLine("InstallHorizonHost -horizonInstanceName $horizonInstanceName `");
            file.WriteLine("  -sitecoreInstanceUrl $sitecoreCmInstanceUrl `");
            file.WriteLine("  -sitecoreInstanceInternalUrl $sitecoreCmInstanceInternalUrl `");
            file.WriteLine("  -sitecoreAdminPassword $sitecoreAdminPassword `");
            file.WriteLine("  -licensePath $licensePath `");
            file.WriteLine("  -sitecoreInstansePath $sitecoreCmInstancePath `");
            file.WriteLine("  -identityServerPoolName $identityServerPoolName `");
            file.WriteLine("  -horizonPhysicalPath $horizonPhysicalPath `");
            file.WriteLine("  -identityServerUrl $identityServerUrl `");
            file.WriteLine("  -identityServerPhysicalPath $identityServerPhysicalPath `");
            file.WriteLine("  -solrCorePrefix $solrCorePrefix `");
            file.WriteLine("  -horizonAppUrl $horizonAppUrl `");
            file.WriteLine("  -enableContentHub $enableContentHub `");
            file.WriteLine("  -enableSXA $enableSXA `");
            file.WriteLine("  -topology $topology");

            file.WriteLine();
            file.Dispose();
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            switch (Version.SitecoreVersion)
            {
                case "10.1.0":
                    WriteFile(@".\" + ZipList.HorizonZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Horizon_Install_Script.ps1", false);
                    break;
                default:
                    break;
            }

            lblStatus.Text = "Script generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        private void ToggleEnableDbControls(bool enabled)
        {
            txtSqlPass.Enabled = enabled;
            txtSqlUser.Enabled = enabled;
            txtSqlDbServer.Enabled = enabled;
            btnDbConn.Enabled = enabled;
        }


        private void ToggleEnableControls(bool enabled)
        {
            ToggleButtonControls(enabled);
            MenubarControls(enabled);
        }

        private void MenubarControls(bool enabled)
        {
            btnLast.Enabled = enabled;
            btnFirst.Enabled = enabled;
            btnPrevious.Enabled = enabled;
            btnSolr.Enabled = enabled;
            //btnAppSettings.Enabled = enabled;
            chkStepsList.Enabled = enabled;
            btnNext.Enabled = enabled;
            btnValidateAll.Enabled = enabled;
            btnPrerequisites.Enabled = enabled;
        }

        private void ToggleButtonControls(bool enabled)
        {
            btnInstall.Enabled = enabled;
            btnGenerate.Enabled = enabled;
        }

        private bool CheckSitecoreInstallDir()
        {
            if (Directory.Exists(txtHorizonInstallDir.Text)) return true;

            return false;
        }

        private bool DbConnTabValidations()
        {
            if (!ValidateData(txtSqlDbServer, "Db Server", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return false;

            return true;
        }

        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            chkStepsList.SelectedIndex = tabIndex;
            tabDetails.SelectedIndex = tabIndex;
            chkStepsList.SetItemChecked(tabIndex, true);
            switch (tabIndex)
            {
                case const_DBConn_Tab:
                    lblStepInfo.Text = "Step 1 of 6: DB Connection";
                    break;
                case const_SiteInfo_Tab:
                    ToggleEnableControls(false);
                    btnNext.Enabled = true;
                    btnPrerequisites.Enabled = true;
                    //btnAppSettings.Enabled = true;
                    lblStepInfo.Text = "Step 2 of 6: Site Info";
                    break;
                case const_General_Tab:
                    MenubarControls(true);
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 3 of 6: General Info";
                    break;
                case const_Install_Details_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 4 of 6: Install Details";
                    break;
                case const_Sitecore_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 5 of 6: Sitecore Details";
                    break;
                case const_Solr_Tab:
                    btnSolr.Enabled = true;
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 6 of 6: Solr Details";
                    break;
            }

        }

        private bool SiteInfoTabValidations()
        {
            if (!ValidateData(txtSiteNamePrefix, "Site Prefix", const_SiteInfo_Tab)) return false;
            return true;
        }

        private bool SolrValidations()
        {
            if (!ValidateData(txtSolrUrl, "Solr Url", const_Solr_Tab)) return false;
            if (!ValidateData(txtSolrRoot, "Solr Root Path", const_Solr_Tab)) return false;
            if (!ValidateData(txtSolrService, "Solr Service Name", const_Solr_Tab)) return false;
            if (!CommonFunctions.VersionCheck(txtSolrVersion.Text, Version.SitecoreVersion))
            {
                SetStatusMessage("Incompatible Solr Version....", Color.Red);
                return false;
            }

            return true;
        }

        private bool ValidateData(TextBox control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                lblStatus.Text = controlString + " needed... ";
                lblStatus.ForeColor = Color.Red;
                AssignStepStatus(tabIndex);
                Valid = false;
            }
            return Valid;
        }

        private bool ValidateAll(bool unInstall = false)
        {
            if (!DbConnTabValidations()) return false;
            if (!SiteInfoTabValidations()) return false;

            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_General_Tab)) return false;
            if (!ValidateData(txtSitecoreIdentityServerUrl, "Sitecore Id Server Url", const_General_Tab)) return false;

            if (!ValidateData(txtSXAInstallDir, "Sitecore SXA Install Directory", const_Install_Details_Tab)) return false;
            if (!ValidateData(txtHorizonInstallDir, "Sitecore xConnect Install Directory", const_Install_Details_Tab)) return false;

            if (!ValidateData(txtSitecoreUsername, "Sitecore Username", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", const_Sitecore_Tab)) return false;

            if (!SolrValidations())
            {
                ToggleEnableControls(false);
                btnSolr.Enabled = true;
                AssignStepStatus(const_Solr_Tab);
                return false;
            }

            return true;
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private bool CheckAllValidations(bool uninstall = false, bool generatescript = false)
        {
            ToggleButtonControls(false);
            uninstall = CheckSitecoreInstallDir();


            if (!CommonFunctions.FileSystemEntryExists(destFolder,null,"folder",false))
            {
                SetStatusMessage("One or more pre-requisites missing.... Click Pre-requisites button to check...", Color.Red);
                return false;
            }

            if (!CommonFunctions.FileSystemEntryExists("C:\\Windows\\System32\\inetsrv", null, "folder", true))
            {
                SetStatusMessage("Some pre-requisites missing.... Click 'Install Pre-requisites' button in Prerequisites form...", Color.Red);
                return false;
            }

            if (!ValidateAll(uninstall)) return false;
            if (!SiteInfoTabValidations()) return false;

            ToggleEnableControls(true);

            return true;
        }

        private bool SaveSCIADatatoDBSuccess(SqlConnection sqlConn)
        {
            try
            {

                var query = "delete from  SCIA where SiteName='" + txtSiteName.Text + "' and InstallType='SIF'; INSERT INTO SCIA(SiteNameSuffix ,[SiteNamePrefix],SiteName ,[IDServerSiteName], [SitecoreIdentityServerUrl] ,[SXAInstallDir] ,XConnectInstallDir,SitecoreUsername ,SitecoreUserPassword ,SolrUrl , SolrRoot , SolrService ,SolrVersion,InstallType) VALUES (@SiteNameSuffix, @SitePrefix,@SiteName, @IdentityServerSiteName, @SitecoreIdServerUrl, @SXASiteInstallDir, @XConnectInstallDir,  @SitecoreUsername, @SitecoreUserPassword,  @SolrUrl, @SolrRoot, @SolrService, @SolrVersion, @InstallType)";

                SqlCommand sqlcommand = new SqlCommand(query, sqlConn);
                sqlcommand.Parameters.AddWithValue("@SiteNameSuffix", txtSiteNameSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@SitePrefix", txtSiteNamePrefix.Text);
                sqlcommand.Parameters.AddWithValue("@SiteName", txtSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@IdentityServerSiteName", txtIDServerSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreIdServerUrl", txtSitecoreIdentityServerUrl.Text);
                sqlcommand.Parameters.AddWithValue("@SXASiteInstallDir", txtSXAInstallDir.Text);
                sqlcommand.Parameters.AddWithValue("@XConnectInstallDir", txtHorizonInstallDir.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUsername", txtSitecoreUsername.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUserPassword", txtSitecoreUserPassword.Text);
                sqlcommand.Parameters.AddWithValue("@SolrUrl", txtSolrUrl.Text);
                sqlcommand.Parameters.AddWithValue("@SolrRoot", txtSolrRoot.Text);
                sqlcommand.Parameters.AddWithValue("@SolrService", txtSolrService.Text);
                sqlcommand.Parameters.AddWithValue("@SolrVersion", txtSolrVersion.Text);
                sqlcommand.Parameters.AddWithValue("@InstallType", "SIF");

                int numberOfInsertedRows = sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Error saving SIF settings data to SCIA_DB DB table " + ex.Message, EventLogEntryType.Error);
                return false;
            }

            return true;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            switch (Version.SitecoreVersion)
            {
                case "10.1.0":
                    WriteFile(@".\" + ZipList.HorizonZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Horizon_Install_Script.ps1", false);
                    CommonFunctions.LaunchPSScript(@".\'"  + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Horizon_Install_Script.ps1'", destFolder);
                    break;                
                default:
                    break;
            }

            lblStatus.Text = "Installation successfully launched through Powershell....";
            lblStatus.ForeColor = Color.DarkGreen;
            ToggleEnableControls(false);
        }

        private void DisplaySettingsDialog(bool settingsDataPresent)
        {
            DBServerDetails dBServerDetails = new DBServerDetails();
            dBServerDetails.Username = txtSqlUser.Text;
            dBServerDetails.Password = txtSqlPass.Text;
            dBServerDetails.Server = txtSqlDbServer.Text;
            dBServerDetails.IsSettingsPresent = settingsDataPresent;
            Settings settings = new Settings(dBServerDetails);
            settings.ShowDialog();
        }

        private bool LoadSettingsFormRoutine(bool settingsDataPresent)
        {
            SetStatusMessage("Successfully established DB Connection... Settings unavailable... Save settings and restart application...", Color.Red);
            ToggleEnableDbControls(false);
            ToggleEnableControls(false);
            DisplaySettingsDialog(settingsDataPresent);
            return true;
        }

        private bool PopulateSettingsData()
        {
            SqlConnection connection;
            if (CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                {
                    connection.Open();
                    if (!CommonFunctions.DbTableExists("Settings", connection)) { LoadSettingsFormRoutine(false); return false; }

                    settingsData = CommonFunctions.GetSettingsData(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text));

                    if (settingsData == null) { LoadSettingsFormRoutine(false); return false; }

                    txtSiteNameSuffix.Text = settingsData.SiteNameSuffix.Trim();
                    siteNamePrefixString = settingsData.SitePrefixString.Trim();
                    identityServerNameString = settingsData.IdentityServerNameString.Trim();
                    siteRootDir = settingsData.SiteRootDir.Trim();
                    txtSitecoreUsername.Text = settingsData.SitecoreUsername.Trim();

                    SetFieldValues();

                }
            }
            return true;
        }

        private void btnGenerate_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void btnInstall_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void btnUninstall_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void SetFieldValues()
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + txtSiteNameSuffix.Text;
            txtHorizonInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text;
            txtHorizonInstanceName.Text = txtSiteNamePrefix.Text + horizonSiteNameSuffixString + txtSiteNameSuffix.Text ;
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtSiteUrl.Text = "https://" + txtSiteName.Text;
            txtHorizonAppUrl.Text = "https://" + txtHorizonInstanceName.Text;
            txtSXAInstallDir.Text = siteRootDir + txtSiteName.Text;
            if (xp0Install)
            {
                txtSXAInstallDir.Text = siteRootDir + txtSiteName.Text;
            }
            txtHorizonInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text  + txtSiteNameSuffix.Text;
            txtIDServerAppPoolName.Text = txtIDServerSiteName.Text;
            txtIDServerInstallDir.Text = siteRootDir + txtIDServerSiteName.Text;
        }

        private void btnDbConn_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "master", txtSqlUser.Text, txtSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                AssignStepStatus(const_DBConn_Tab);
                ToggleEnableControls(false);
                return;
            }
            if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                LoadSettingsFormRoutine(false);
                return;
                /* if no records in settings table, just enable settings button
                * else load next tab
                * */

            }

            if (!PopulateSettingsData()) return;
            SetStatusMessage("Successfully established DB Connection", Color.DarkGreen);

            CommonFunctions.ConnectionString = CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text);

            ToggleEnableDbControls(false);
            btnDbConn.Enabled = false;
            AssignStepStatus(const_SiteInfo_Tab);
        }

        private string StringRight(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        private bool FillSolrDetails()
        {
            SolrInfo info = CommonFunctions.GetSolrInformation(txtSolrUrl.Text);
            if (info == null)
            {
                SetStatusMessage("Check if Solr Service is running - " + txtSolrUrl.Text, Color.Red);
                return false;
            }
            txtSolrRoot.Text = info.solr_home.Replace("\\server\\solr", string.Empty);
            int lastIndexofSlash = txtSolrRoot.Text.LastIndexOf("\\");
            txtSolrService.Text = StringRight(txtSolrRoot.Text, txtSolrRoot.Text.Length - lastIndexofSlash - 1);
            txtSolrVersion.Text = info.lucene.SolrSpecVersion;
            return true;
        }

        private void PopulateSIFData()
        {

            SqlConnection connection;
            using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
            {
                connection.Open();

                SIFDetails siteData = CommonFunctions.GetSIFData(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text), txtSiteName.Text);
                if (siteData == null) return;
                txtSiteNameSuffix.Text = siteData.SiteNameSuffix.Trim();
                txtSiteNameSuffix.Text = siteData.SiteNameSuffix.Trim();
                txtIDServerSiteName.Text = siteData.IDServerSiteName.Trim();
                txtSitecoreIdentityServerUrl.Text = siteData.SitecoreIdentityServerUrl.Trim();
                txtSitecoreUsername.Text = siteData.SitecoreUsername.Trim();
                txtSolrRoot.Text = siteData.SolrRoot;
                txtSolrUrl.Text = siteData.SolrUrl;
                txtSolrService.Text = siteData.SolrService;
                txtSolrVersion.Text = siteData.SolrVersion;
                //Uninstall = true;
            }
        }

        private bool SiteInfoFolderValidations()
        {
            if (CheckSitecoreInstallDir())
            {
                lblStatus.Text = "Horizon already installed for this site - " + txtSXAInstallDir.Text;
                TabIndexValue = const_SiteInfo_Tab;
                AssignStepStatus(TabIndexValue);
                lblStatus.ForeColor = Color.Red;
                return false;
            }

            if (!Directory.Exists(txtSXAInstallDir.Text))
            {
                lblStatus.Text = "Missing Directory! Install Sitecore site at - " + txtSXAInstallDir.Text;
                TabIndexValue = const_SiteInfo_Tab;
                AssignStepStatus(TabIndexValue);
                lblStatus.ForeColor = Color.Red;
                return false;
            }

            return true;
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TabIndexValue == const_DBConn_Tab) if (!DbConnTabValidations() || settingsData == null) return;

            //Get data from SCIA table
            if (TabIndexValue == const_SiteInfo_Tab)
            {
                if (!SiteInfoTabValidations() || !SiteInfoFolderValidations()) return;
                if (!CommonFunctions.FileSystemEntryExists(txtSXAInstallDir.Text,null,"folder",true))
                {
                    SetStatusMessage("Missing Site Details....  " + txtSXAInstallDir.Text, Color.Red);
                    return;
                }

                txtSolrUrl.Text = CommonFunctions.GetSolrUrl(txtSXAInstallDir.Text);
                if (string.IsNullOrWhiteSpace(txtSolrUrl.Text))
                {
                    SetStatusMessage("Missing Solr Details.... probably missing site prefix.... ", Color.Red);
                    return;
                }
                if (!FillSolrDetails()) return;
            }
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 2) TabIndexValue += 1;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_Click(object sender, EventArgs e)
        {
            TabIndexValue = chkStepsList.SelectedIndex;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < chkStepsList.Items.Count; ++ix)
                if (ix != e.Index) chkStepsList.SetItemChecked(ix, false);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            AssignStepStatus(const_Solr_Tab);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            AssignStepStatus(const_DBConn_Tab);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabDetails.TabCount - 1) TabIndexValue -= 1;
            AssignStepStatus(TabIndexValue);
        }

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
                foreach (Form frm in this.MdiChildren)
                {
                    if (frm is SifPrerequisites)
                    {
                        if (frm.WindowState == FormWindowState.Minimized)
                            frm.WindowState = FormWindowState.Normal;
                        frm.Focus();
                        return;
                    }
                }

                DBServerDetails dbDetails = new DBServerDetails();
                dbDetails.Username = DBDetails.SqlUser;
                dbDetails.Password = DBDetails.SqlPass;
                dbDetails.Server = DBDetails.DbServer;
                HorizonPrerequisites formInstance = new HorizonPrerequisites(dbDetails);
                formInstance.Show();
                return;
        }

        private void btnSolr_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnSolr.Enabled = false;
            ClearSolrControls();
            SetStatusMessage("Processing....", Color.Orange);

            if(string.IsNullOrWhiteSpace(txtSolrUrl.Text)) {
                SetStatusMessage("Solr Url is needed...", Color.Red);
                AssignStepStatus(const_Solr_Tab);
                Cursor.Current = Cursors.Default;
                return; }

            SolrInfo info = null;
            SolrXmlObject resp = null;
            info = CommonFunctions.GetSolrInformation(txtSolrUrl.Text);
            if (info == null)
            {
                //then it could be a xml response compatible with 6.x.x solr
                resp = CommonFunctions.GetSolrXMLInformation(txtSolrUrl.Text);
            }
            //var solrspecversion = resp.Lsts[1].Strs[0].SolrSpecversion;

            if (string.IsNullOrWhiteSpace(txtSolrUrl.Text) || (info == null && resp==null))
            {
                TabIndexValue = const_Solr_Tab;
                ToggleButtonControls(false);
                MenubarControls(true);
                AssignStepStatus(TabIndexValue);
                btnSolr.Enabled = true;
            }

            if (string.IsNullOrWhiteSpace(txtSolrUrl.Text))
            {
                SetStatusMessage("Missing ConnectionStrings Config file at - " + txtSXAInstallDir.Text, Color.Red);
                Cursor.Current = Cursors.Default;
                return;
            }

            if (info!=null)
            {
                txtSolrRoot.Text = info.solr_home.Replace("\\server\\solr", string.Empty);
                FillSolrDetails();
                txtSolrVersion.Text = info.lucene.SolrSpecVersion;
            }

            if (resp != null)
            {
                txtSolrRoot.Text = resp.SolrRoot.Replace("\\server\\solr", string.Empty);
                txtSolrService.Text = resp.SolrService;
                txtSolrVersion.Text = resp.SolrVersion;
            }

            if (Version.SitecoreVersion.Substring(0, 3) != "9.0")
            {
                if (info == null && resp==null)
                {
                    SetStatusMessage("Missing Solr Url Info... check if Solr is hosted and running...", Color.Red);
                    Cursor.Current = Cursors.Default;
                    return;
                }
            }
            else
            {
                if (string.IsNullOrWhiteSpace(txtSolrUrl.Text) || string.IsNullOrWhiteSpace(txtSolrVersion.Text) || string.IsNullOrWhiteSpace(txtSolrRoot.Text) || string.IsNullOrWhiteSpace(txtSolrService.Text))
                {
                    SetStatusMessage("Please input Solr Details manually for Solr version 6.x.x....", Color.Red);
                    Cursor.Current = Cursors.Default;
                    ToggleButtonControls(false);
                    return;
                }
            }
            ToggleEnableControls(true);
            if (!CommonFunctions.VersionCheck(txtSolrVersion.Text, Version.SitecoreVersion))
            {
                SetStatusMessage("Incompatible Solr Version....", Color.Red);
                Cursor.Current = Cursors.Default;
                ToggleButtonControls(false);
                return;
            }

            SetStatusMessage("All seems fine with Solr....", Color.DarkGreen);

            btnSolr.Enabled = true;
            Cursor.Current = Cursors.Default;
        }

        private void ClearSolrControls()
        {
            txtSolrRoot.Text = string.Empty;
            txtSolrService.Text = string.Empty;
            txtSolrVersion.Text = string.Empty;
        }

        private void btnValidateAll_Click(object sender, EventArgs e)
        {
            if (CheckAllValidations()) {
                ToggleButtonControls(true);
                SetStatusMessage("Congrats! Passed all Validations!", Color.DarkGreen); }
        }

        private void btnAppSettings_Click(object sender, EventArgs e)
        {
            DisplaySettingsDialog(true);
        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
                SetFieldValues();
        }

        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + txtSiteNameSuffix.Text;
            txtSXAInstallDir.Text = siteRootDir;
            txtHorizonInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + txtSiteNameSuffix.Text;
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;

        }

        private void btnSolrInstall_Click(object sender, EventArgs e)
        {
            SolrInstaller solrInstaller = new SolrInstaller();
            solrInstaller.ShowDialog();
        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
