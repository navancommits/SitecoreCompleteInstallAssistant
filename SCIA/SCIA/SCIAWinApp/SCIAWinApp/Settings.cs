﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Transactions;
using System.Windows.Forms;

namespace SCIA
{
    public partial class Settings : Form
    {
        string DefaultStatusMessage = "Happy Sitecoring!";
        const int const_Port_Tab = 7;
        const int const_Redis_Tab = 6;
        const int const_DBConn_Tab = 0;

        const int const_SiteInfo_Tab = 1;
        const int const_General_Tab = 2;
        const int const_Install_Details_Tab = 3;
        const int const_Sitecore_Tab = 4;
        const int const_Solr_Tab = 5;
        const int const_Environments_Tab = 8;
        const int const_Win_User_Tab = 9;
        const int const_Braintree_User_Tab = 10;

        DBServerDetails dbServer;
        public Settings(DBServerDetails dbServerDetails)
        {
            InitializeComponent();
            tabSiteDetails.Region = new Region(tabSiteDetails.DisplayRectangle);
            dbServer = dbServerDetails;
            PopulateSettingsInfo();
            txtDbServer.Text=dbServer.Server;
            txtSqlUser.Text = dbServer.Username;
            txtSqlPass.Text = dbServer.Password;
            AssignStepStatus(const_DBConn_Tab);

        }

        private void ToggleButtonEnable(bool state)
        {
            btnSave.Enabled = state;
            chkStepList.Enabled = state;
            btnFirst.Enabled = state;
            btnPrevious.Enabled = state;
            btnLast.Enabled = state;
        }

        private void LoadDefaultSettingsData()
        {
            txtSiteNameSuffix.Text = ".dev.local";
            txtSitePrefixAdditional.Text = "sc";
            txtIdentityServerNameAdditional.Text = "identityserver";
            txtxConnectString.Text = "xconnect";
            txtCommerceEngineConnectClientId.Text = "CommerceEngineConnect";
            txtCommerceEngineConnectClientSecret.Text = "fe6g2c5+YBGh5180qjB6N91nKGNn+gvgS0n51ixHnNY=";
            txtSiteRootDir.Text = "c:\\inetpub\\wwwroot\\";
            txtSitecoreDomain.Text = "sitecore";
            txtSitecoreUserName.Text = "admin";
            txtSearchIndexPrefix.Text = "sitecore";
            txtRedisHost.Text = "localhost";
            txtRedisPort.Text = "6379";
            txtBizFxSitePrefix.Text = "SitecoreBizFx-";
            txtEnvironmentPrefix.Text = "Habitat";
            txtCommerceDbNameString.Text = "_SitecoreCommerce_";
            txtUserDomain.Text = "sitecore";
            txtBraintreeEnvironment.Text = "sandbox";
            txtBraintreePrivateKey.Text = string.Empty;
            txtBraintreePublicKey.Text = string.Empty;
            txtBraintreeMerchantId.Text = string.Empty;
            txtCoreDbSuffix.Text= "_Core";
            txtCommerceGlobalDbSuffix.Text= "Global";
            txtCommSharedDbSuffix.Text= "SharedEnvironments";
            txtUserSuffix.Text= "_User";
            txtStorefrontHostSuffix.Text= ".storefront.com";
            txtHostSuffix.Text = ".com";
            txtHttpsString.Text = "https://";
            txtUserPassword.Text = "q5Y8tA3FRMZf3xKN!";
        }

        private void PopulateSettingsInfo()
        {
            SqlConnection connection;
            if (!dbServer.IsSettingsPresent)
            {
                LoadDefaultSettingsData();
                return;
            }
              
            using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(dbServer.Server, "SCIA_DB", dbServer.Username, dbServer.Password)))
            {
                connection.Open();
                if (CommonFunctions.DbTableExists("Settings", connection))
                {

                    SettingsData settingsData = CommonFunctions.GetSettingsData(CommonFunctions.BuildConnectionString(dbServer.Server, "SCIA_DB", dbServer.Username, dbServer.Password));
                    if (settingsData == null) { LoadDefaultSettingsData(); return; }
                       
                    txtSiteNameSuffix.Text = settingsData.SiteNameSuffix.Trim();
                    txtSitePrefixAdditional.Text = settingsData.SitePrefixString.Trim();
                    txtIdentityServerNameAdditional.Text = settingsData.IdentityServerNameString.Trim();
                    txtxConnectString.Text = settingsData.xConnectServerNameString.Trim();
                    txtCommerceEngineConnectClientId.Text = settingsData.CommerceEngineConnectClientId.Trim();
                    txtCommerceEngineConnectClientSecret.Text = settingsData.CommerceEngineConnectClientSecret.Trim();
                    txtSiteRootDir.Text = settingsData.SiteRootDir.Trim();
                    txtSitecoreDomain.Text = settingsData.SitecoreDomain.Trim();
                    txtSitecoreUserName.Text = settingsData.SitecoreUsername.Trim();
                    txtSearchIndexPrefix.Text = settingsData.SearchIndexPrefix.Trim();
                    txtRedisHost.Text = settingsData.RedisHost.Trim();
                    txtRedisPort.Text = settingsData.RedisPort.Trim();
                    txtBizFxSitePrefix.Text = settingsData.BizFxSitenamePrefix.Trim();
                    txtEnvironmentPrefix.Text = settingsData.EnvironmentsPrefix.Trim();
                    txtCommerceDbNameString.Text = settingsData.CommerceDbNameString.Trim();
                    txtUserDomain.Text = settingsData.UserDomain.Trim();
                    txtBraintreeEnvironment.Text = settingsData.BraintreeEnvironment.Trim();
                    txtBraintreePrivateKey.Text = settingsData.BraintreePrivateKey.Trim();
                    txtBraintreePublicKey.Text = settingsData.BraintreePublicKey.Trim();
                    txtBraintreeMerchantId.Text = settingsData.BraintreeMerchantId.Trim();
                    txtCoreDbSuffix.Text = settingsData.CoreDbSuffix;
                    txtCommerceGlobalDbSuffix.Text = settingsData.CommerceGlobalDbSuffix;
                    txtCommSharedDbSuffix.Text = settingsData.CommSharedDbSuffix;
                    txtUserSuffix.Text = settingsData.UserSuffix;
                    txtStorefrontHostSuffix.Text = settingsData.StorefrontHostSuffix;
                    txtHostSuffix.Text = settingsData.HostSuffix;
                    txtHttpsString.Text =settingsData.HttpsString;
                    txtUserPassword.Text = settingsData.UserPassword;
                }

            }
        }
        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection;

            if (!ValidateAll()) return;
            try
            {
                Cursor.Current = Cursors.WaitCursor;
                btnSave.Enabled = false;
                SetStatusMessage("Processing....", Color.Orange);

                if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", dbServer.Server, dbServer.Username, dbServer.Password))
                {
                    connection = CommonFunctions.CreateDatabase("SCIA_DB",txtDbServer.Text,txtSqlUser.Text,txtSqlPass.Text);
                    if (connection == null)
                    {
                        SetStatusMessage("Database Connectivity issues....", Color.Red);
                        return;
                    }
                }

                using TransactionScope scope = new TransactionScope();
                    using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                    {
                        connection.Open();
                        if (!CommonFunctions.DbTableExists("Settings", connection))
                        {
                            CommonFunctions.CreateSettingsTable(connection);
                        }

                        SaveSettingsDatatoDBSuccess(connection);
                    }

                // if all the coperations complete successfully, this would be called and commit the transaction. 
                // In case of an exception, it wont be called and transaction is rolled back
                scope.Complete();

                Cursor.Current = Cursors.Default;
                btnSave.Enabled = true;
                SetStatusMessage("Settings saved successfully... Close and re-open application to see latest changes....", Color.DarkGreen);

            }
            catch (Exception ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Error in save click of settings - " + ex.Message, EventLogEntryType.Error);
              
                Cursor.Current = Cursors.Default;
                btnSave.Enabled = true;
                SetStatusMessage("Error saving settings....", Color.Red);
            }
            
        }

        public bool SaveSettingsDatatoDBSuccess(SqlConnection sqlConn)
        {
            try
            {

                var query = "delete from  Settings; INSERT INTO Settings([SiteNameSuffix], [SitePrefixString],[IdentityServerNameString],xConnectServerNameString, CommerceEngineConnectClientId, CommerceEngineConnectClientSecret, SiteRootDir, SitecoreDomain, SitecoreUsername ,SearchIndexPrefix  ,RedisHost  ,RedisPort  ,BizFxSitenamePrefix  ,EnvironmentsPrefix  ,CommerceDbNameString  ,UserDomain , BraintreeMerchantId ,BraintreePublicKey ,BraintreePrivateKey ,BraintreeEnvironment,CoreDbSuffix,CommerceGlobalDbSuffix,CommSharedDbSuffix,UserSuffix,StorefrontHostSuffix,HostSuffix,HttpsString,UserPassword) VALUES (@siteNameSuffix, @siteNamePrefixString,@identityServerNameString,@xConnectServerNameString, @commerceEngineConnectClientId, @commerceEngineConnectClientSecret, @siteRootDir, @sitecoreDomain, @sitecoreUsername ,@searchIndexPrefix  ,@redisHost  ,@redisPort  ,@bizFxSitenamePrefix  ,@environmentsPrefix  ,@commerceDbNameString  ,@userDomain , @braintreeMerchantId ,@braintreePublicKey ,@braintreePrivateKey ,@braintreeEnvironment,@CoreDbSuffix,@CommerceGlobalDbSuffix,@CommSharedDbSuffix,@UserSuffix,@StorefrontHostSuffix,@HostSuffix,@HttpString,@UserPassword)";

                SqlCommand sqlcommand = new SqlCommand(query, sqlConn);
                sqlcommand.Parameters.AddWithValue("@siteNameSuffix", txtSiteNameSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@siteNamePrefixString", txtSitePrefixAdditional.Text);
                sqlcommand.Parameters.AddWithValue("@identityServerNameString", txtIdentityServerNameAdditional.Text);
                sqlcommand.Parameters.AddWithValue("@xConnectServerNameString", txtxConnectString.Text);
                sqlcommand.Parameters.AddWithValue("@commerceEngineConnectClientId", txtCommerceEngineConnectClientId.Text);
                sqlcommand.Parameters.AddWithValue("@commerceEngineConnectClientSecret", txtCommerceEngineConnectClientSecret.Text);
                sqlcommand.Parameters.AddWithValue("@siteRootDir", txtSiteRootDir.Text);
                sqlcommand.Parameters.AddWithValue("@sitecoreDomain", txtSitecoreDomain.Text);
                sqlcommand.Parameters.AddWithValue("@sitecoreUsername", txtSitecoreUserName.Text);
                sqlcommand.Parameters.AddWithValue("@searchIndexPrefix", txtSearchIndexPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@redisHost", txtRedisHost.Text);
                sqlcommand.Parameters.AddWithValue("@redisPort", txtRedisPort.Text);
                sqlcommand.Parameters.AddWithValue("@bizFxSitenamePrefix", txtBizFxSitePrefix.Text);
                sqlcommand.Parameters.AddWithValue("@environmentsPrefix", txtEnvironmentPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@commerceDbNameString", txtCommerceDbNameString.Text);
                sqlcommand.Parameters.AddWithValue("@userDomain", txtUserDomain.Text);
                sqlcommand.Parameters.AddWithValue("@braintreeMerchantId", txtBraintreeMerchantId.Text);
                sqlcommand.Parameters.AddWithValue("@braintreePublicKey", txtBraintreePublicKey.Text);
                sqlcommand.Parameters.AddWithValue("@braintreePrivateKey", txtBraintreePrivateKey.Text);
                sqlcommand.Parameters.AddWithValue("@braintreeEnvironment", txtBraintreeEnvironment.Text);
                sqlcommand.Parameters.AddWithValue("@CoreDbSuffix", txtCoreDbSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceGlobalDbSuffix", txtCommerceGlobalDbSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@CommSharedDbSuffix", txtCommSharedDbSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@UserSuffix", txtUserSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@StorefrontHostSuffix", txtStorefrontHostSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@HostSuffix", txtHostSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@HttpString", txtHttpsString.Text);
                sqlcommand.Parameters.AddWithValue("@UserPassword", txtUserPassword.Text);

                int numberOfInsertedRows = sqlcommand.ExecuteNonQuery();
            }
            catch(Exception ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Error saving settings data to SCIA_DB DB table " + ex.Message, EventLogEntryType.Error);
                return false;
            }
            
            return true;
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {            
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void txtBizFxPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }


        private void txtIDServerSiteName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreIdentityServerUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreIdentityServerUrl_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceEngineConnectClientSecret_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteHostHeaderName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSXAInstallDir_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtxConnectInstallDir_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceInstallRoot_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlDbPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreDbServer_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreCoreDbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreCoreDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlUser_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlPass_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreDomain_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreUsername_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreUserPassword_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSearchIndexPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrUrl_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrRoot_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrService_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtStorefrontIndexPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtRedisHost_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtRedisPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceServicesDBServer_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceGlobalDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceSvcPostFix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceServicesHostPostFix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBizFxName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserDomain_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserPassword_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txttxtBraintreeMerchantId_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreePublicKey_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreePrivateKey_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreeEnvironment_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteNameSuffix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteNamePrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            tabSiteDetails.SelectedIndex = tabIndex;
            chkStepList.SelectedIndex = tabIndex;
            chkStepList.SetItemChecked(tabIndex, true);
            switch (tabIndex)
            {
                case const_DBConn_Tab:
                    lblStepStatus.Text = "Step 1 of 11: DB Connection";
                    break;
                case const_SiteInfo_Tab:
                    ToggleButtonEnable(true);
                    lblStepStatus.Text = "Step 2 of 11: Site Info";
                    break;
                case const_General_Tab:
                    lblStepStatus.Text = "Step 3 of 11: General Info";
                    break;
                case const_Install_Details_Tab:
                    lblStepStatus.Text = "Step 4 of 11: Install Details";
                    break;
                case const_Sitecore_Tab:
                    lblStepStatus.Text = "Step 5 of 11 Sitecore Details";
                    break;
                case const_Solr_Tab:
                    lblStepStatus.Text = "Step 6 of 11: Solr Details";
                    break;
                case const_Redis_Tab:
                    lblStepStatus.Text = "Step 7 of 11: Redis Details";
                    break;                
                case const_Port_Tab:
                    lblStepStatus.Text = "Step 8 of 11: BizFx Details";
                    break;
                case const_Environments_Tab:
                    lblStepStatus.Text = "Step 9 of 11: Environment Details";
                    break;
                case const_Win_User_Tab:
                    lblStepStatus.Text = "Step 10 of 11: Win User Details";
                    break;
                case const_Braintree_User_Tab:
                    lblStepStatus.Text = "Step 11 of 11: Braintree Details";
                    break;
            }
        }


        public int TabIndexValue { get; set; }

        private void btnNext_Click(object sender, EventArgs e)
        {           

            if (TabIndexValue >= 0 && TabIndexValue <= tabSiteDetails.TabCount - 2) TabIndexValue += 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            TabIndexValue = tabSiteDetails.TabCount - 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private bool ValidateData(TextBox control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                lblStatus.Text = controlString + " needed... ";
                lblStatus.ForeColor = Color.Red;
                tabSiteDetails.SelectedIndex = tabIndex;
                Valid = false;
            }
            return Valid;
        }

        private bool ValidateAll()
        {
            if (!ValidateData(txtCommerceEngineConnectClientId, "Sitecore Commerce Connect Client Id", const_General_Tab)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientSecret, "Sitecore Commerce Connect Client Secret", const_General_Tab)) return false;

            if (!ValidateData(txtSiteRootDir, "Sitecore SXA Install Directory", const_Install_Details_Tab)) return false;

            if (!ValidateData(txtDbServer, "Db Server", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return false;

            if (!ValidateData(txtSitecoreDomain, "Sitecore Domain", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserName, "Sitecore Username", const_Sitecore_Tab)) return false;

            if (!ValidateData(txtSearchIndexPrefix, "Search Index Prefix", const_Solr_Tab)) return false;

            if (!ValidateData(txtRedisHost, "Redis Host", const_Redis_Tab)) return false;

            if (!ValidateData(txtBizFxSitePrefix, "BizFx Prefix", const_Port_Tab)) return false;

            if (!ValidateData(txtUserDomain, "Win User Domain", const_Win_User_Tab)) return false;

            if (!ValidateData(txtBraintreeMerchantId, "Braintree Merchant Id", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreePublicKey, "Braintree Public Key", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreePrivateKey, "Braintree Private Key", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreeEnvironment, "Braintree Environment", const_Braintree_User_Tab)) return false;

            if (!ValidateData(txtHttpsString,"Https String", const_SiteInfo_Tab)) return false;
            if (!ValidateData(txtCommerceGlobalDbSuffix, "Global Db Suffix",  const_Sitecore_Tab)) return false;
            if (!ValidateData(txtCommSharedDbSuffix, "Shared Db Suffix", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtCoreDbSuffix,"Core Db Suffix",  const_Sitecore_Tab)) return false;
            if (!ValidateData(txtStorefrontHostSuffix, "Storefront Host Suffix", const_Environments_Tab)) return false;
            if (!ValidateData(txtHostSuffix,"Host Suffix", const_Environments_Tab)) return false;
            if (!ValidateData(txtUserSuffix,"User Suffix", const_Win_User_Tab)) return false;
            if (!ValidateData(txtUserPassword,"User Password", const_Win_User_Tab)) return false;

            return true;
        }

        

        private void btnFirst_Click(object sender, EventArgs e)
        {
            TabIndexValue = 0;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabSiteDetails.TabCount - 1) TabIndexValue -= 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_Click(object sender, EventArgs e)
        {
            TabIndexValue = chkStepList.SelectedIndex;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < chkStepList.Items.Count; ++ix)
                if (ix != e.Index) chkStepList.SetItemChecked(ix, false);
        }

        private void chkStepList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ToggleEnableControls(bool enabled)
        {
            btnSave.Enabled = enabled;
        }

        private void btnDbConn_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtDbServer.Text, "master", txtSqlUser.Text, txtSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                TabIndexValue = const_DBConn_Tab;
                AssignStepStatus(TabIndexValue);
                ToggleEnableControls(false);
                return;
            }
            ToggleEnableControls(true);
            SetStatusMessage("Successfully established DB Connection", Color.DarkGreen);
        }

        private void btnSave_EnabledChanged(object sender, EventArgs e)
        {
                Button btn = (Button)sender;
                btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
                btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }
    }
}
