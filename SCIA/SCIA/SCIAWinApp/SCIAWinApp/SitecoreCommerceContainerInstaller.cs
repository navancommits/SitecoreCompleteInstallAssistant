using System;
using System.Collections.Generic;
using System.Data.Linq;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Transactions;
using System.Windows.Forms;

namespace SCIA
{

    public partial class SitecoreCommerceContainerInstaller : Form
    {
        string SystemDrive = "C:";
        string destFolder = @".\Sitecore.Commerce.WDP.2020.08-6.0.238\SIF.Sitecore.Commerce.5.0.49\";
        string StatusMessage = string.Empty;
        string DefaultStatusMessage = "Happy Sitecoring!";
        //int tabIndex = 0;
        const int const_DBConn_Tab = 0;
        const int const_SiteInfo_Tab = 1;
        const int const_General_Tab = 2;
        const int const_Install_Details_Tab = 3;
        const int const_Sitecore_Tab = 4;
        const int const_Solr_Tab = 5;
        const int const_Redis_Tab = 6;
        const int const_Sitecore_DB_Tab = 7;
        const int const_Commerce_Tab = 8;
        const int const_Port_Tab = 9;
        const int const_Environments_Tab = 10;
        const int const_Win_User_Tab = 11;
        const int const_Braintree_User_Tab = 12;
        string siteNamePrefixString ="sc";//sc
        string identityServerNameString = "identityserver";//identityserver
        string xConnectServerNameString = "xconnect";//xconnect
        string siteRootDir = "c:\\intetpub\\wwwroot";//"c:\\intetpub\\wwwroot\\
        string bizFxSitenamePrefix = "SitecoreBizFx-";//SitecoreBizFx-
        string commerceDbNameString = "_sitecore_commerce_";//"_sitecore_commerce_
        string httpsString = "https://";//"https://
        string sharedDbSuffix = "shared";
        string globalDbSuffix = "global";
        string userSuffixString = "_User";
        string storefrontHostSuffix = ".storefront.com";
        string hostSuffix = ".com";
        string coreDbSuffix = "_Core";
        bool Uninstall = false;

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        
        public SitecoreCommerceContainerInstaller()
        {
            InitializeComponent();
            SystemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
            ToggleEnableControls(false);
            AssignStepStatus(TabIndexValue);
        }

        private void SetDefaultData()
        {
            txtSiteNameSuffix.Text = ".dev.local";
            siteNamePrefixString = "sc";
            identityServerNameString = "identityserver";
            xConnectServerNameString = "xconnect";
            txtCommerceEngineConnectClientId.Text = "CommerceEngineConnect";
            txtCommerceEngineConnectClientSecret.Text = "fe6g2c5+YBGh5180qjB6N91nKGNn+gvgS0n51ixHnNY=";
            siteRootDir = "c:\\inetpub\\wwwroot";
            txtSitecoreDomain.Text = "sitecore";
            txtSitecoreUsername.Text = "admin";
            txtSearchIndexPrefix.Text = "sitecore";
            txtRedisHost.Text = "localhost";
            txtRedisPort.Text = "6379";
            bizFxSitenamePrefix = "SitecoreBizFx-";
            txtEnvironmentsPrefix.Text = "Habitat";
            commerceDbNameString = "_SitecoreCommerce_";
            txtUserDomain.Text = "sitecore";
            txtBraintreeEnvironment.Text = "sandbox";
            txtBraintreePrivateKey.Text = string.Empty;
            txtBraintreePublicKey.Text = string.Empty;
            txttxtBraintreeMerchantId.Text = string.Empty;
            
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
                    xConnectServerNameString = settingsData.xConnectServerNameString.Trim();
                    txtCommerceEngineConnectClientId.Text = settingsData.CommerceEngineConnectClientId.Trim();
                    txtCommerceEngineConnectClientSecret.Text = settingsData.CommerceEngineConnectClientSecret.Trim();
                    siteRootDir = settingsData.SiteRootDir.Trim();
                    txtSitecoreDomain.Text = settingsData.SitecoreDomain.Trim();
                    txtSitecoreUsername.Text = settingsData.SitecoreUsername.Trim();
                    txtSearchIndexPrefix.Text = settingsData.SearchIndexPrefix.Trim();
                    txtRedisHost.Text = settingsData.RedisHost.Trim();
                    txtRedisPort.Text = settingsData.RedisPort.Trim();
                    bizFxSitenamePrefix = settingsData.BizFxSitenamePrefix.Trim();
                    txtEnvironmentsPrefix.Text = settingsData.EnvironmentsPrefix.Trim();
                    commerceDbNameString = settingsData.CommerceDbNameString.Trim();
                    txtUserDomain.Text = settingsData.UserDomain.Trim();
                    txtBraintreeEnvironment.Text = settingsData.BraintreeEnvironment.Trim();
                    txtBraintreePrivateKey.Text = settingsData.BraintreePrivateKey.Trim();
                    txtBraintreePublicKey.Text = settingsData.BraintreePublicKey.Trim();
                    txttxtBraintreeMerchantId.Text = settingsData.BraintreeMerchantId.Trim();
                    sharedDbSuffix = settingsData.CommSharedDbSuffix;
                    globalDbSuffix = settingsData.CommerceGlobalDbSuffix;
                    coreDbSuffix = settingsData.CoreDbSuffix;
                    userSuffixString = settingsData.UserSuffix;
                    httpsString = settingsData.HttpsString;
                    storefrontHostSuffix = settingsData.StorefrontHostSuffix;
                    hostSuffix = settingsData.HostSuffix;
                    txtUserPassword.Text = settingsData.UserPassword;
                    
                    SetFieldValues();
                  
                }
            }
            return true;
        }

        private void PopulateSCIAData()
        {

            SqlConnection connection;
                using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                {
                    connection.Open();               

                    SiteDetails siteData = CommonFunctions.GetSCIAData(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text), txtSiteName.Text);
                    if (siteData == null) return;
                    txtSiteNameSuffix.Text = siteData.SiteNameSuffix.Trim();
                    txtSiteNameSuffix.Text = siteData.SiteNameSuffix.Trim();
                    txtIDServerSiteName.Text = siteData.IDServerSiteName.Trim();
                    txtSitecoreIdentityServerUrl.Text = siteData.SitecoreIdentityServerUrl.Trim();
                    txtCommerceEngineConnectClientId.Text = siteData.CommerceEngineConnectClientId.Trim();
                    txtCommerceEngineConnectClientSecret.Text = siteData.CommerceEngineConnectClientSecret.Trim();
                    txtCommerceAuthSvcPort.Value = siteData.CommerceAuthSvcPort;
                    txtCommerceOpsSvcPort.Value = siteData.CommerceOpsSvcPort;
                    txtCommerceShopsServicesPort.Value = siteData.CommerceShopsServicesPort;
                    txtBizFxPort.Value = siteData.BizFxPort;
                    txtCommerceMinionsSvcPort.Value = siteData.CommerceMinionsSvcPort;
                    txtSitecoreDomain.Text = siteData.SitecoreDomain.Trim();
                    txtSitecoreUsername.Text = siteData.SitecoreUsername.Trim();
                    txtSearchIndexPrefix.Text = siteData.SearchIndexPrefix.Trim();
                    txtRedisHost.Text = siteData.RedisHost.Trim();
                    txtRedisPort.Value = siteData.RedisPort;
                    txtBizFxName.Text = siteData.BizFxName.Trim();
                    txtEnvironmentsPrefix.Text = siteData.EnvironmentsPrefix.Trim();
                    txtUserPassword.Text = siteData.UserPassword.Trim();
                    txtUserDomain.Text = siteData.UserDomain.Trim();
                    txtBraintreeEnvironment.Text = siteData.BraintreeEnvironment.Trim();
                    txtBraintreePrivateKey.Text = settingsData.BraintreePrivateKey.Trim();
                    txtBraintreePublicKey.Text = settingsData.BraintreePublicKey.Trim();
                    txttxtBraintreeMerchantId.Text = settingsData.BraintreeMerchantId.Trim();
                    chkDeploySampleData.Checked = siteData.DeploySampleData=="Y"?true:false;
                    txtSitecoreCoreDbName.Text = siteData.SitecoreCoreDbName;
                    txtCommerceDbName.Text = siteData.CommerceDbName;
                    txtCommerceGlobalDbName.Text = siteData.CommerceGlobalDbName;
                    txtCommerceSvcPostFix.Text = siteData.CommerceSvcPostFix;
                    txtCommerceServicesHostPostFix.Text = siteData.CommerceServicesHostPostFix;
                    txtSolrRoot.Text = siteData.SolrRoot;
                    txtSolrUrl.Text = siteData.SolrUrl;
                    txtSolrService.Text = siteData.SolrService;

                    //Uninstall = true;
                }
        }


        private void PopulateSettingsInfo()
        {
            SqlConnection connection;
            if (CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                {
                    connection.Open();
                    if (CommonFunctions.DbTableExists("Settings", connection))
                    {

                        SqlDataReader reader = CommonFunctions.GetSettingsData(connection);

                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                txtSiteNameSuffix.Text = reader["SiteNameSuffix"].ToString().Trim();
                                siteNamePrefixString = reader["SitePrefixString"].ToString().Trim();
                                identityServerNameString = reader["IdentityServerNameString"].ToString().Trim();
                                xConnectServerNameString = reader["xConnectServerNameString"].ToString().Trim();
                                txtCommerceEngineConnectClientId.Text = reader["CommerceEngineConnectClientId"].ToString().Trim();
                                txtCommerceEngineConnectClientSecret.Text = reader["CommerceEngineConnectClientSecret"].ToString().Trim();
                                siteRootDir = reader["SiteRootDir"].ToString().Trim();
                                txtSitecoreDomain.Text = reader["SitecoreDomain"].ToString().Trim();
                                txtSitecoreUsername.Text = reader["SitecoreUsername"].ToString().Trim();
                                txtSearchIndexPrefix.Text = reader["SearchIndexPrefix"].ToString().Trim();
                                txtRedisHost.Text = reader["RedisHost"].ToString().Trim();
                                txtRedisPort.Text = reader["RedisPort"].ToString().Trim();
                                bizFxSitenamePrefix = reader["BizFxSitenamePrefix"].ToString().Trim();
                                txtEnvironmentsPrefix.Text = reader["EnvironmentsPrefix"].ToString().Trim();
                                commerceDbNameString = reader["CommerceDbNameString"].ToString().Trim();
                                txtUserDomain.Text = reader["UserDomain"].ToString().Trim();                               
                                txtBraintreeEnvironment.Text = reader["BraintreeEnvironment"].ToString().Trim();
                                txtBraintreePrivateKey.Text = reader["BraintreePrivateKey"].ToString().Trim();
                                txtBraintreePublicKey.Text = reader["BraintreePublicKey"].ToString().Trim();
                                txttxtBraintreeMerchantId.Text = reader["BraintreeMerchantId"].ToString().Trim();

                            }

                        }
                        else
                        {
                            txtSiteNameSuffix.Text = ".dev.local";
                            siteNamePrefixString = "sc";
                            identityServerNameString = "identityserver";
                            xConnectServerNameString = "xconnect";
                            txtCommerceEngineConnectClientId.Text = "CommerceEngineConnect";
                            txtCommerceEngineConnectClientSecret.Text = "fe6g2c5+YBGh5180qjB6N91nKGNn+gvgS0n51ixHnNY=";
                            txtSitecoreDomain.Text = "sitecore";
                            txtSitecoreUsername.Text = "admin";
                            txtSearchIndexPrefix.Text = "sitecore";
                            txtRedisHost.Text = "localhost";
                            txtRedisPort.Text = "6379";
                            bizFxSitenamePrefix = "SitecoreBizFx-";
                            txtEnvironmentsPrefix.Text = "Habitat";
                            commerceDbNameString = "_SitecoreCommerce_";
                            txtUserDomain.Text = "sitecore";
                            txtBraintreeEnvironment.Text = "sandbox";
                            txtBraintreePrivateKey.Text = string.Empty;
                            txtBraintreePublicKey.Text = string.Empty;
                            txttxtBraintreeMerchantId.Text = string.Empty;
                        }
                        SetFieldValues();
                    }

                }
            }
        }        

        private void SaveSCIAData()
        {
            Cursor.Current = Cursors.WaitCursor;
            btnInstall.Enabled = false;
            try
            {
                using TransactionScope scope = new TransactionScope();

                using (SqlConnection connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                {
                    connection.Open();

                    if (!CommonFunctions.DbTableExists("SCIA", connection))
                    {
                        CommonFunctions.CreateSCIATable(connection);
                    }

                    SaveSCIADatatoDBSuccess(connection);
                }

                // if all the coperations complete successfully, this would be called and commit the transaction. 
                // In case of an exception, it wont be called and transaction is rolled back
                scope.Complete();
            }
            catch(Exception ex)
            {
                CommonFunctions.WritetoEventLog("Error saving SCIA Data - " + ex.Message, EventLogEntryType.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private bool SaveSCIADatatoDBSuccess(SqlConnection sqlConn)
        {
            try
            {

                var query = "delete from  SCIA where SiteName='" + txtSiteName.Text + "'; INSERT INTO SCIA(SiteNameSuffix ,[SiteNamePrefix],SiteName ,[IDServerSiteName], [SitecoreIdentityServerUrl] ,[SXAInstallDir] ,XConnectInstallDir ,CommerceInstallRoot  , CommerceEngineConnectClientId , CommerceEngineConnectClientSecret , SiteHostHeaderName , SitecoreDomain , SitecoreUsername ,SitecoreUserPassword ,SearchIndexPrefix  ,SolrUrl , SolrRoot , SolrService ,StorefrontIndexPrefix ,RedisHost , RedisPort  ,SqlDbPrefix  ,SitecoreDbServer ,SitecoreCoreDbName ,[CommerceServicesDBServer], CommerceDbName,CommerceGlobalDbName ,[CommerceSvcPostFix],CommerceServicesHostPostFix,CommerceOpsSvcPort ,[CommerceShopsServicesPort] ,CommerceAuthSvcPort , CommerceMinionsSvcPort ,BizFxPort , [BizFxName]  ,EnvironmentsPrefix  ,DeploySampleData ,UserDomain,UserName , UserPassword, BraintreeMerchantId ,BraintreePublicKey ,BraintreePrivateKey ,BraintreeEnvironment) VALUES (@SiteNameSuffix, @SitePrefix,@SiteName, @IdentityServerSiteName, @SitecoreIdServerUrl, @SXASiteInstallDir, @XConnectInstallDir, @CommerceInstallRoot, @CommerceEngineConnectClientId, @CommerceEngineConnectClientSecret, @SiteHostHeaderName, @SitecoreDomain, @SitecoreUsername, @SitecoreUserPassword, @SearchIndexPrefix, @SolrUrl, @SolrRoot, @SolrService, @StorefrontIndexPrefix, @RedisHost, @RedisPort, @SqlDbPrefix, @SitecoreDbServer, @SitecoreCoreDbName, @CommerceDbServer, @CommerceDbName, @CommerceGlobalDbName, @CommerceServicesPostFix, @CommerceServicesHostPostFix, @CommerceOpsSvcPort, @CommerceShopsSvcPort, @CommerceAuthSvcPort, @CommerceMinionsSvcPort, @BizFxPort, @BizFxSitename, @EnvironmentsPrefix, @DeploySampleData, @UserDomain, @UserName, @UserPassword, @BraintreeMerchantId, @BraintreePublicKey, @BraintreePrivateKey, @BraintreeEnvironment)";

                SqlCommand sqlcommand = new SqlCommand(query, sqlConn);
                sqlcommand.Parameters.AddWithValue("@SiteNameSuffix", txtSiteNameSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@SitePrefix", txtSiteNamePrefix.Text);
                sqlcommand.Parameters.AddWithValue("@SiteName", txtSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@IdentityServerSiteName", txtIDServerSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreIdServerUrl", txtSitecoreIdentityServerUrl.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceEngineConnectClientId", txtCommerceEngineConnectClientId.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceEngineConnectClientSecret", txtCommerceEngineConnectClientSecret.Text);
                sqlcommand.Parameters.AddWithValue("@SiteHostHeaderName", txtSiteHostHeaderName.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreDomain", txtSitecoreDomain.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUsername", txtSitecoreUsername.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUserPassword", txtUserPassword.Text);
                sqlcommand.Parameters.AddWithValue("@SearchIndexPrefix", txtSearchIndexPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@SolrUrl", txtSolrUrl.Text);
                sqlcommand.Parameters.AddWithValue("@SolrRoot", txtSolrRoot.Text);
                sqlcommand.Parameters.AddWithValue("@SolrService", txtSolrService.Text);
                sqlcommand.Parameters.AddWithValue("@StorefrontIndexPrefix", txtStorefrontIndexPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@RedisHost", txtRedisHost.Text);
                sqlcommand.Parameters.AddWithValue("@RedisPort", txtRedisPort.Text);
                sqlcommand.Parameters.AddWithValue("@SqlDbPrefix", txtSqlDbPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreDbServer", txtSitecoreDbServer.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreCoreDbName", txtSitecoreCoreDbName.Text);
                    sqlcommand.Parameters.AddWithValue("@CommerceDbServer", txtCommerceServicesDBServer.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceDbName", txtCommerceDbName.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceGlobalDbName", txtCommerceGlobalDbName.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceServicesPostFix", txtCommerceSvcPostFix.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceServicesHostPostFix", txtCommerceServicesHostPostFix.Text);
                sqlcommand.Parameters.AddWithValue("@CommerceOpsSvcPort", txtCommerceOpsSvcPort.Value);
                sqlcommand.Parameters.AddWithValue("@CommerceShopsSvcPort", txtCommerceShopsServicesPort.Value);
                sqlcommand.Parameters.AddWithValue("@CommerceAuthSvcPort", txtCommerceAuthSvcPort.Value);
                sqlcommand.Parameters.AddWithValue("@CommerceMinionsSvcPort", txtCommerceMinionsSvcPort.Value);
                sqlcommand.Parameters.AddWithValue("@BizFxPort", txtBizFxPort.Value);
                sqlcommand.Parameters.AddWithValue("@BizFxSitename", txtBizFxName.Text);
                sqlcommand.Parameters.AddWithValue("@EnvironmentsPrefix", txtEnvironmentsPrefix.Text);
                sqlcommand.Parameters.AddWithValue("@DeploySampleData", (chkDeploySampleData.Checked==true)?"Y":"N");
                sqlcommand.Parameters.AddWithValue("@UserDomain", txtUserDomain.Text);
                sqlcommand.Parameters.AddWithValue("@UserName", txtUserName.Text);
                sqlcommand.Parameters.AddWithValue("@UserPassword", txtUserPassword.Text);
                sqlcommand.Parameters.AddWithValue("@braintreeMerchantId", txttxtBraintreeMerchantId.Text);
                sqlcommand.Parameters.AddWithValue("@braintreePublicKey", txtBraintreePublicKey.Text);
                sqlcommand.Parameters.AddWithValue("@braintreePrivateKey", txtBraintreePrivateKey.Text);
                sqlcommand.Parameters.AddWithValue("@braintreeEnvironment", txtBraintreeEnvironment.Text);

                int numberOfInsertedRows = sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Error saving settings data to SCIA_DB DB table " + ex.Message, EventLogEntryType.Error);
                return false;
            }

            return true;
        }


        private void btnInstall_Click(object sender, EventArgs e)
        {
            //if (!CheckAllValidations()) return;

            //WriteFile(destFolder + txtSiteName.Text + "_Install_Script.ps1", habitatExists, false);
            //CommonFunctions.LaunchPSScript(txtSiteName.Text + "_Install_Script.ps1");
            //SaveSCIAData();
            CommonFunctions.LaunchCmdScript("docker-compose up -d", ".\\Sitecore.Commerce.Container.SDK.1.0.214\\xc0");
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Installation successfully launched through Powershell....";
            ToggleEnableControls(false);
        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {
            txtSearchIndexPrefix.Text = txtSiteName.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
        }

        private bool DbConnTabValidations()
        {
            if (!ValidateData(txtSqlDbServer, "Db Server", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return false;

            return true;
        }

        private bool SiteInfoTabValidations()
        {
            if (!ValidateData(txtSiteNamePrefix, "Site Prefix", const_SiteInfo_Tab)) return false;
            return true;
        }


        private bool ValidateAll(bool unInstall=false)
        {
            if (!DbConnTabValidations()) return false;
            if (!SiteInfoTabValidations()) return false;

            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_General_Tab)) return false;
            if (!ValidateData(txtSitecoreIdentityServerUrl, "Sitecore Id Server Url", const_General_Tab)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientId, "Sitecore Commerce Connect Client Id", const_General_Tab)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientSecret, "Sitecore Commerce Connect Client Secret", const_General_Tab)) return false;
            if (!ValidateData(txtSiteHostHeaderName, "Site Host Header Name",const_General_Tab)) return false;

            if (!ValidateData(txtSqlDbPrefix, "Sql Db Prefix",const_Sitecore_DB_Tab)) return false;
            if (!ValidateData(txtSitecoreCoreDbName, "Sitecore Core Db Name",const_Sitecore_DB_Tab)) return false;
           
            if (!ValidateData(txtSitecoreDomain, "Sitecore Domain",const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUsername, "Sitecore Username", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", const_Sitecore_Tab)) return false;

            if (!ValidateData(txtRedisHost, "Redis Host",const_Redis_Tab)) return false;

            if (!ValidateData(txtCommerceServicesDBServer, "Commerce DB Server",const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceDbName, "Commerce DB Name", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceGlobalDbName, "Sitecore Commerce Global Db Name", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceSvcPostFix, "Sitecore Commerce Svc Post Fix", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceServicesHostPostFix, "Sitecore Commerce Svc Host Post Fix", const_Commerce_Tab)) return false;

            if (!PerformPortValidations(unInstall)) return false;
            if (!ValidateData(txtBizFxName, "BizFx Name",const_Redis_Tab)) return false;

            if (unInstall) return true;
            if (!ValidateData(txttxtBraintreeMerchantId, "Braintree Merchant Id", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreePublicKey, "Braintree Public Key", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreePrivateKey, "Braintree Private Key", const_Braintree_User_Tab)) return false;
            if (!ValidateData(txtBraintreeEnvironment, "Braintree Environment", const_Braintree_User_Tab)) return false;
            
            return true;
        }

        private bool PerformPortValidations(bool unInstall=false)
        {
            string portString = string.Empty;
            
            if (!ValidatePortNumber(txtRedisPort, "Redis Port", const_Redis_Tab)) return false;

            if (!ValidatePortNumber(txtCommerceOpsSvcPort, "Commerce Ops Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceShopsServicesPort, "Commerce Shops Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceAuthSvcPort, "Commerce Auth Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceMinionsSvcPort, "Commerce Minions Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtBizFxPort, "BizFx Port Number", const_Port_Tab)) return false;
            if (!unInstall)
            {
                if (!IsPortNotinUse(txtCommerceOpsSvcPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtCommerceShopsServicesPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtCommerceAuthSvcPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtCommerceMinionsSvcPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtBizFxPort, const_Port_Tab)) return false;
            }
                       
            return true;
        }

        private bool ValidatePortNumber(NumericUpDown control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (control.Value < 1024)
            {
                lblStatus.Text = controlString + " must be between 1024 to 49151... ";
                lblStatus.ForeColor = Color.Red;
                control.Focus();
                AssignStepStatus(tabIndex);
                Valid = false;
            }
            return Valid;
        }

        private bool ValidateData(TextBox control, string controlString,int tabIndex)
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

       

        private bool IsPortDuplicated(List<int> ports)
        {
            bool portDuplicated=false;
            for(int index=0;index<ports.Count;index++)
            {
                for (int loopIndex = 0; loopIndex < ports.Count; loopIndex++)
                {
                    if (loopIndex!=index)
                    {
                        if (ports[loopIndex]==ports[index]) { 
                            portDuplicated = true; 
                            break;
                        }
                    }
                    
                }
            }
            return portDuplicated;
        }

        private List<int> AddPortstoArray()
        {
            List<int> ports = new List<int>
            {
                Convert.ToInt32(txtBizFxPort.Value),
                Convert.ToInt32(txtCommerceAuthSvcPort.Value),
                Convert.ToInt32(txtCommerceMinionsSvcPort.Value),
                Convert.ToInt32(txtCommerceOpsSvcPort.Value),
                Convert.ToInt32(txtCommerceShopsServicesPort.Value),
                Convert.ToInt32(txtRedisPort.Value)
            };
            return ports;
        }

        private bool HabitatExists(string connString)
        {
            try
            {
                SqlConnection sqlConn;
                using (sqlConn = new SqlConnection(connString))
                {
                    sqlConn.Open();
                    using SqlCommand cmd = new SqlCommand(@"SELECT count(id) FROM Items where name like '%storefront%'", sqlConn);
                    int recCount = (int)cmd.ExecuteScalar();
                    sqlConn.Close();
                    if (recCount <= 0) return false;
                }
            }
            catch
            {
                lblStatus.Text = "Connectivity Issues....";
            }
            return true;
        }


        private bool CheckPrerequisites()
        {
            if (!Directory.Exists(".\\Sitecore.Commerce.Container.SDK.1.0.214")) { return false; }
            if (!File.Exists(".\\Sitecore.Commerce.Container.SDK.1.0.214\\xc0\\license.xml")) { return false; }
            if (!Directory.Exists("c:\\program files\\docker")) { return false; }

            return true;
        }

        private string StatusMessageBuilder(string msg)
        {
            string portString = string.Empty;
            if (PortInUse(Convert.ToInt32(txtCommerceShopsServicesPort.Value))) { portString = BuildPortString(txtCommerceShopsServicesPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceOpsSvcPort.Value))) { portString = BuildPortString(txtCommerceOpsSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtBizFxPort.Value))) { portString = BuildPortString(txtBizFxPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceAuthSvcPort.Value))) { portString = BuildPortString(txtCommerceAuthSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceMinionsSvcPort.Value))) { portString = BuildPortString(txtCommerceMinionsSvcPort.Value.ToString(), portString); }
            return portString;
        }

        private string BuildPortString(string port, string buildMsgString)
        {
            string portMessageString = string.Empty;
            if (string.IsNullOrWhiteSpace(buildMsgString))
            {
                portMessageString = port;
            }
            else
            {
                portMessageString = buildMsgString + ", " + port;
            }

            return portMessageString;
        }

        private bool CheckAllValidations(bool uninstall=false,bool generatescript=false)
        {
            string portString = string.Empty;
            ToggleButtonControls(false);


            if (!CheckPrerequisites())
            {
                SetStatusMessage("One or more pre-requisites missing.... Click Pre-requisites button to check...", Color.Red);
                return false;
            }
            
            if (!ValidateAll(uninstall)) return false;
            if(!SiteInfoTabValidations()) return false;
            if (!uninstall)
            {
                if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! Provide unique port numbers...."; return false; }

                portString = StatusMessageBuilder(portString);
                if (!string.IsNullOrWhiteSpace(portString))
                {
                    lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red;
                    TabIndexValue = const_Port_Tab;
                    AssignStepStatus(TabIndexValue);
                    return false;
                }
            }
           
            ToggleEnableControls(true);
            if (uninstall) { btnInstall.Enabled = false; } else { btnUninstall.Enabled = false; }

            return true;
        }

        void WriteAutoFillFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$LicenseXmlPath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreAdminPassword = \"Password12345\",");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SqlSaPassword = \"Password12345\",");
            file.WriteLine();
            file.WriteLine("\t[string]$BrainTreeEnvironment = \"sandbox\",");
            file.WriteLine();
            file.WriteLine("\t[string]$BrainTreePublicKey,");
            file.WriteLine();
            file.WriteLine("\t[string]$BrainTreePrivateKey,");
            file.WriteLine();
            file.WriteLine("\t[string]$BrainTreeMerchantId");
            file.WriteLine(")");
            file.WriteLine();
            file.WriteLine("$ErrorActionPreference = \"Stop\";");
            file.WriteLine();            
            file.WriteLine("if (-not (Test-Path $LicenseXmlPath)) {");
            file.WriteLine("\tthrow \"Did not find $LicenseXmlPath\"");
            file.WriteLine("}");
            file.WriteLine("if (-not (Test-Path $LicenseXmlPath -PathType Leaf)) {");
            file.WriteLine("\tthrow \"$LicenseXmlPath is not a file\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# Check for Sitecore Gallery");
            file.WriteLine("Import-Module PowerShellGet");
            file.WriteLine("$SitecoreGallery = Get-PSRepository | Where-Object { $_.SourceLocation -eq \"https://sitecore.myget.org/F/sc-powershell/api/v2\" }");
            file.WriteLine("if (-not $SitecoreGallery) {");
            file.WriteLine("\tWrite-Host \"Adding Sitecore PowerShell Gallery...\" -ForegroundColor Green ");
            file.WriteLine("\tRegister-PSRepository -Name SitecoreGallery -SourceLocation https://sitecore.myget.org/F/sc-powershell/api/v2 -InstallationPolicy Trusted");
            file.WriteLine("\t$SitecoreGallery = Get-PSRepository -Name SitecoreGallery");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# Install and Import SitecoreDockerTools ");
            file.WriteLine("$dockerToolsVersion = \"10.0.5\"");
            file.WriteLine("Remove-Module SitecoreDockerTools -ErrorAction SilentlyContinue");
            file.WriteLine("if (-not (Get-InstalledModule -Name SitecoreDockerTools -RequiredVersion $dockerToolsVersion -AllowPrerelease -ErrorAction SilentlyContinue)) {");
            file.WriteLine("\tWrite-Host \"Installing SitecoreDockerTools...\" -ForegroundColor Green");
            file.WriteLine("\tInstall-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion -AllowPrerelease -Scope CurrentUser -Repository $SitecoreGallery.Name");
            file.WriteLine("}");
            file.WriteLine("Write-Host \"Importing SitecoreDockerTools...\" -ForegroundColor Green");
            file.WriteLine("Import-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion");
            file.WriteLine();
            file.WriteLine("###############################");
            file.WriteLine("# Populate the environment file");
            file.WriteLine("###############################");
            file.WriteLine();
            file.WriteLine("Write-Host \"Populating required.env file variables...\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("# SITECORE_ADMIN_PASSWORD");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ADMIN_PASSWORD\" -Value $SitecoreAdminPassword");
            file.WriteLine();
            file.WriteLine("# SQL_SA_PASSWORD");
            file.WriteLine();
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SQL_SA_PASSWORD\" -Value $SqlSaPassword");
            file.WriteLine();
            file.WriteLine("# TELERIK_ENCRYPTION_KEY = random 64-128 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"TELERIK_ENCRYPTION_KEY\" -Value (Get-SitecoreRandomString 128)");
            file.WriteLine();
            file.WriteLine("# SITECORE_IDSECRET = random 64 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_IDSECRET\" -Value (Get-SitecoreRandomString 64 -DisallowSpecial)");
            file.WriteLine();
            file.WriteLine("# SITECORE_ID_CERTIFICATE");
            file.WriteLine("$idCertPassword = Get-SitecoreRandomString 12 -DisallowSpecial");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ID_CERTIFICATE\" -Value (Get-SitecoreCertificateAsBase64String -DnsName \"" + txtSiteNameSuffix.Text + "\" -Password (ConvertTo-SecureString -String $idCertPassword -Force -AsPlainText))");
            file.WriteLine();
            file.WriteLine("# SITECORE_ID_CERTIFICATE_PASSWORD");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ID_CERTIFICATE_PASSWORD\" -Value $idCertPassword");
            file.WriteLine();
            file.WriteLine("# SITECORE_LICENSE");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_LICENSE\" -Value (ConvertTo-CompressedBase64String -Path $LicenseXmlPath)");
            file.WriteLine();
            file.WriteLine("# XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1=random 64 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"XC_IDENTITY_COMMERCEENGINECONNECTCLIENT_CLIENTSECRET1\" -Value (Get-SitecoreRandomString 64 -DisallowSpecial)");
            file.WriteLine();
            file.WriteLine("# REPORTING_API_KEY=random 32 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"REPORTING_API_KEY\" -Value (Get-SitecoreRandomString 32)");
            file.WriteLine();
            file.WriteLine("# XC_ENGINE_BRAINTREEENVIRONMENT");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"XC_ENGINE_BRAINTREEENVIRONMENT\" -Value $BrainTreeEnvironment");
            file.WriteLine();
            file.WriteLine("# XC_ENGINE_BRAINTREEMERCHANTID");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"XC_ENGINE_BRAINTREEMERCHANTID\" -Value $BrainTreeMerchantId");
            file.WriteLine();
            file.WriteLine("# CM_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"CM_HOST\" -Value \"" + txtSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# ID_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"ID_HOST\" -Value \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# AUTHORING_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"AUTHORING_HOST\" -Value \"" + txtAuthSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# SHOPS_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SHOPS_HOST\" -Value \"" + txtShopsSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# OPS_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"OPS_HOST\" -Value \"" + txtOpsSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# MINIONS_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"MINIONS_HOST\" -Value \"" + txtMinionsSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# BIZFX_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"BIZFX_HOST\" -Value \"" + txtBizFxName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# XC_ENGINE_BRAINTREEPUBLICKEY");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"XC_ENGINE_BRAINTREEPUBLICKEY\" -Value $BrainTreePublicKey");
            file.WriteLine();
            file.WriteLine("# XC_ENGINE_BRAINTREEPRIVATEKEY");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"XC_ENGINE_BRAINTREEPRIVATEKEY\" -Value $BrainTreePrivateKey");           
            file.WriteLine();
            file.WriteLine("##################################");
            file.WriteLine("# Configure TLS/HTTPS certificates");
            file.WriteLine("##################################");
            file.WriteLine();
            file.WriteLine("Push-Location traefik\\certs");
            file.WriteLine("try {");
            file.WriteLine("\t$mkcert = \".\\mkcert.exe\"");
            file.WriteLine("\tif ($null -ne (Get-Command mkcert.exe -ErrorAction SilentlyContinue)) {");
            file.WriteLine("\t\t# mkcert installed in PATH");
            file.WriteLine("\t\t$mkcert = \"mkcert\"");
            file.WriteLine("\t} elseif (-not (Test-Path $mkcert)) {");
            file.WriteLine("\t\tWrite-Host \"Downloading and installing mkcert certificate tool...\" -ForegroundColor Green ");
            file.WriteLine("\t\tInvoke-WebRequest \"https://github.com/FiloSottile/mkcert/releases/download/v1.4.1/mkcert-v1.4.1-windows-amd64.exe\" -UseBasicParsing -OutFile mkcert.exe ");
            file.WriteLine("\t\tif ((Get-FileHash mkcert.exe).Hash -ne \"1BE92F598145F61CA67DD9F5C687DFEC17953548D013715FF54067B34D7C3246\") {");
            file.WriteLine("\t\t\tRemove-Item mkcert.exe -Force");
            file.WriteLine("\t\t\tthrow \"Invalid mkcert.exe file\"");
            file.WriteLine("\t\t}");
            file.WriteLine("\t}");
            file.WriteLine("\tWrite-Host \"Generating Traefik TLS certificates...\" -ForegroundColor Green");
            file.WriteLine("\t& $mkcert -install");
            file.WriteLine("\t& $mkcert -cert-file " + txtSiteName.Text + ".crt -key-file " + txtSiteName.Text + ".key \"" + txtSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtIDServerSiteName.Text + ".crt -key-file " + txtIDServerSiteName.Text + ".key \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtAuthSiteName.Text + ".crt -key-file " + txtAuthSiteName.Text + ".key \"" + txtAuthSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtShopsSiteName.Text + ".crt -key-file " + txtShopsSiteName.Text + ".key \"" + txtShopsSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtMinionsSiteName.Text + ".crt -key-file " + txtMinionsSiteName.Text + ".key \"" + txtMinionsSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtOpsSiteName.Text + ".crt -key-file " + txtOpsSiteName.Text + ".key \"" + txtOpsSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtBizFxName.Text + ".crt -key-file " + txtBizFxName.Text + ".key \"" + txtBizFxName.Text + "\"");
            file.WriteLine("}");
            file.WriteLine("catch {");
            file.WriteLine("\tWrite-Host \"An error occurred while attempting to generate TLS certificates: $_\" -ForegroundColor Red");
            file.WriteLine("}");
            file.WriteLine("finally {");
            file.WriteLine("\tPop-Location");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("################################");
            file.WriteLine("# Add Windows hosts file entries");
            file.WriteLine("################################");
            file.WriteLine();
            file.WriteLine("Write-Host \"Adding Windows hosts file entries...\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("Add-HostsEntry \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtMinionsSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtShopsSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtOpsSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtBizFxName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtAuthSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("Write-Host \"Done!\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("###############################################");
            file.WriteLine("# Create containers folder and related volumes");
            file.WriteLine("###############################################");
            file.WriteLine();
            file.WriteLine("Write-Host \"Create containers folder...\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("..\\scripts\\CreateVolumeFolders.ps1");
            file.WriteLine();
            file.WriteLine("Write-Host \"Done!\" -ForegroundColor Green");
            file.Dispose();
        }

        private void CreateVolumeFoldersScript(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("Import-Module(Join-Path $PSScriptRoot \"ScriptSupport\") -DisableNameChecking -Global");
            file.WriteLine("#Creates volumes folders in the host");
            file.WriteLine("Confirm-VolumeFoldersExist -Path \"C:\\containers\"");
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            CreateVolumeFoldersScript(".\\Sitecore.Commerce.Container.SDK.1.0.214\\scripts\\CreateVolumeFolders.ps1");
            WriteAutoFillFile(".\\Sitecore.Commerce.Container.SDK.1.0.214\\xc0\\init-setup.ps1");

            CommonFunctions.LaunchPSScript(".\\init-setup.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + txtSitecoreUsername.Text + "\" -SitecoreAdminPassword \"" + txtSitecoreUserPassword.Text + "\" -SqlSaPassword \"" + txtSqlPass.Text + "\" -BrainTreeEnvironment \"" + txtBraintreeEnvironment.Text + "\" -BrainTreePublicKey \"" + txtBraintreePublicKey.Text + "\" -BrainTreePrivateKey \"" + txtBraintreePrivateKey.Text + "\" -BrainTreeMerchantId \"" + txttxtBraintreeMerchantId.Text + "\" -LicenseXmlPath \"license.xml\"", ".\\Sitecore.Commerce.Container.SDK.1.0.214\\xc0");

            lblStatus.Text = "Scripts generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void SetFieldValues()
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtAuthSiteName.Text = txtSiteNamePrefix.Text + "auth." + txtSiteNameSuffix.Text;
            txtBizFxName.Text = txtSiteNamePrefix.Text + "bizfx." + txtSiteNameSuffix.Text;
            txtMinionsSiteName.Text = txtSiteNamePrefix.Text + "minions." + txtSiteNameSuffix.Text;
            txtShopsSiteName.Text = txtSiteNamePrefix.Text + "shops." + txtSiteNameSuffix.Text;
            txtOpsSiteName.Text = txtSiteNamePrefix.Text + "ops." + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + "."  + txtSiteNameSuffix.Text;
            txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + storefrontHostSuffix;
            txtSqlDbPrefix.Text = txtSiteNamePrefix.Text;
            txtSitecoreCoreDbName.Text = txtSqlDbPrefix.Text + coreDbSuffix;
            txtCommerceDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + sharedDbSuffix;
            txtCommerceGlobalDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + globalDbSuffix;
            txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + siteNamePrefixString;
            txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + hostSuffix;
            txtBizFxName.Text = bizFxSitenamePrefix + txtCommerceSvcPostFix.Text;
            txtUserName.Text = txtCommerceSvcPostFix.Text + userSuffixString;
            txtSitecoreIdentityServerUrl.Text = httpsString + txtIDServerSiteName.Text;
            txtStorefrontIndexPrefix.Text = txtSiteName.Text;
            
        }


        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations(true)) return;
            bool habitatExists = HabitatExists(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text));

            lblStatus.Text = "Docker Compose Down successfully launched through Cmd prompt....";
            lblStatus.ForeColor = Color.DarkGreen;
            ToggleEnableControls(false);
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

        private void btnCommerceOpsSvcPort_Click(object sender, EventArgs e)
        {
            IsPortNotinUse(txtCommerceOpsSvcPort,7);
        }

        private bool IsPortNotinUse(NumericUpDown control,int tabIndex)
        {
            if (PortInUse(Convert.ToInt32(control.Value)))
            {
                lblStatus.Text = control.Value + " port in use... provide a different number...";
                lblStatus.ForeColor = Color.Red;
                AssignStepStatus(const_Port_Tab);
                return false;                
            }
            return true;
        }

        private void btnCommerceShopsSvcPort_Click(object sender, EventArgs e)
        {
            IsPortNotinUse(txtCommerceShopsServicesPort,7);
        }

        private void btnCommerceAuthSvcPort_Click(object sender, EventArgs e)
        {
            IsPortNotinUse(txtCommerceAuthSvcPort,7);
        }

        private void btnCommerceMinionsSvcPort_Click(object sender, EventArgs e)
        {
            IsPortNotinUse(txtCommerceMinionsSvcPort,7);
        }

        private void btnBizFxPort_Click(object sender, EventArgs e)
        {
            IsPortNotinUse(txtBizFxPort,7);
        }

        private void txtCommerceMinionsSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceAuthSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceShopsServicesPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceOpsSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBizFxPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtIDServerSiteName_TextChanged(object sender, EventArgs e)
        {
            txtSitecoreIdentityServerUrl.Text= "https://" + txtIDServerSiteName.Text;
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

        private SettingsData settingsData { get; set; }

        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            chkStepsList.SelectedIndex = tabIndex;
            tabDetails.SelectedIndex = tabIndex;
            chkStepsList.SetItemChecked(tabIndex, true);
            switch (tabIndex)
            {
                case const_DBConn_Tab:
                    lblStepInfo.Text= "Step 1 of 13: DB Connection";
                    break;
                case const_SiteInfo_Tab:
                    ToggleEnableControls(false);
                    btnNext.Enabled = true;
                    btnDelete.Enabled=true;
                    btnAppSettings.Enabled = true;
                    lblStepInfo.Text = "Step 2 of 13: Site Info";
                    break;
                case const_General_Tab:
                    MenubarControls(true);
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 3 of 13: General Info";
                    break;
                case const_Install_Details_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 4 of 13: Install Details"; 
                    break;
                case const_Sitecore_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 5 of 13: Sitecore Details"; 
                    break;
                case const_Solr_Tab:
                    btnSolr.Enabled = true;
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 6 of 13: Solr Details"; 
                    break;
                case const_Redis_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 7 of 13: Redis Details";
                    break;
                case const_Sitecore_DB_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 8 of 13: Sitecore DB Details";
                    break;
                case const_Commerce_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 9 of 13: Commerce Details";
                    break;
                case const_Port_Tab:
                    btnPortCheck.Enabled = true;
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 10 of 13: Port Details";
                    break;
                case const_Environments_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 11 of 13: Environment Details";
                    break;
                case const_Win_User_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 12 of 13: Win User Details";
                    break;
                case const_Braintree_User_Tab:
                    btnValidateAll.Enabled = true;
                    lblStepInfo.Text = "Step 13 of 13: Braintree Details";
                    break;
            }
           
        }                       

        public int TabIndexValue { get; set; }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if(TabIndexValue == const_DBConn_Tab) if (!DbConnTabValidations() || settingsData==null) return;
                            
            //Get data from SCIA table
            if (TabIndexValue == const_SiteInfo_Tab)
            {
                if (!SiteInfoTabValidations()) return;
                //PopulateSCIAData();                
            }
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 2) TabIndexValue += 1;
            AssignStepStatus(TabIndexValue);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            AssignStepStatus(const_Braintree_User_Tab);
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {                     
            AssignStepStatus(const_DBConn_Tab);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabDetails.TabCount-1) TabIndexValue -= 1;
            AssignStepStatus(TabIndexValue);
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (!ValidateData(txtSiteName, "Site Name", const_SiteInfo_Tab)) return;
            tabDetails.SelectedTab = tabDetails.TabPages[1];
        }

        private void btnGenerate_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void btnPortCheck_Click(object sender, EventArgs e)
        {
            if (PerformPortValidations())
            {
                lblStatus.Text = "Ports are unique and fine...";
                lblStatus.ForeColor = Color.DarkGreen;
            }
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
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

        private void btnDbConn_Click(object sender, EventArgs e)
        {
            
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, "master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                TabIndexValue = const_DBConn_Tab;
                AssignStepStatus(TabIndexValue);
                ToggleEnableControls(false);
                return;
            }
            if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSitecoreDbServer.Text,  txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text))
            {
                LoadSettingsFormRoutine(false);
                return;
                /* if no records in settings table, just enable settings button
                * else load next tab
                * */

            }
                                
            if (!PopulateSettingsData()) return;
            SetStatusMessage("Successfully established DB Connection", Color.DarkGreen);

            ToggleEnableDbControls(false);
            btnDbConn.Enabled = false;
            AssignStepStatus(const_SiteInfo_Tab); 
            
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
            btnDelete.Enabled = enabled;
            btnLast.Enabled = enabled;
            btnFirst.Enabled = enabled;
            btnPrevious.Enabled = enabled;
            btnPortCheck.Enabled = enabled;
            btnSolr.Enabled = enabled;
            btnAppSettings.Enabled = enabled;
            chkStepsList.Enabled = enabled;
            btnNext.Enabled = enabled;
            btnValidateAll.Enabled = enabled;
        }

        private void ToggleButtonControls(bool enabled)
        {
            
            btnInstall.Enabled = enabled;
            btnInstall.Enabled = enabled;
            btnUninstall.Enabled = enabled;
            btnGenerate.Enabled = enabled;
        }


        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            DockerPrerequisites prerequisites = new DockerPrerequisites();
            prerequisites.ShowDialog();
        }

        private void btnAppSettings_Click(object sender, EventArgs e)
        {
            DisplaySettingsDialog(true);
        }

        private void txtSqlUser_TextChanged(object sender, EventArgs e)
        {
            txtSitecoreSqlUser.Text = txtSqlUser.Text;
        }

        private void txtSqlPass_TextChanged(object sender, EventArgs e)
        {
            txtSitecoreSqlPass.Text = txtSqlPass.Text;
        }

        private void txtSqlDbServer_TextChanged(object sender, EventArgs e)
        {
            txtSitecoreDbServer.Text = txtSqlDbServer.Text;
            txtCommerceServicesDBServer.Text = txtSqlDbServer.Text;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!ValidateData(txtSqlDbServer, "Sitecore Db Server", const_DBConn_Tab)) return;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return;

            if (!ValidateData(txtSiteNamePrefix, "Site Prefix", const_SiteInfo_Tab)) return;
           
            DeleteScript(txtSiteName.Text + "_Delete_Script.ps1");
            DeleteAll deleteAll = new DeleteAll(txtSiteName.Text + "_Delete_Script.ps1");
            deleteAll.ShowDialog();
        }

        private void DeleteScript(string path)
        {
            var appcmdExe = "C:\\windows\\system32\\inetsrv\\appcmd.exe";
            var stoppedStatus = "Stopped";
            var commerceOpsSitePath = "IIS:\\Sites\\Default Web Site\\$CommerceOpsSiteName";
            var commerceShopsSitePath= "IIS:\\Sites\\Default Web Site\\$CommerceShopsSiteName";
            var commerceAuthSitePath = "IIS:\\Sites\\Default Web Site\\$CommerceAuthoringSiteName";
            var commerceMinionsSitePath = "IIS:\\Sites\\Default Web Site\\$CommerceMinionsSiteName";
            var commerceBizFxSitePath = "IIS:\\Sites\\Default Web Site\\$SitecoreBizFxSiteName";
            var idServerSitePath = "IIS:\\Sites\\Default Web Site\\$SitecoreIdentityServerSiteName";
            var sitePath = "IIS:\\Sites\\Default Web Site\\$SiteName";
            var xConnectSitePath = "IIS:\\Sites\\Default Web Site\\$SitecorexConnectSiteName";

            var commerceOpsAppPool = "IIS:\\AppPools\\$CommerceOpsSiteName";
            var commerceShopsAppPool = "IIS:\\AppPools\\Default Web Site\\$CommerceShopsSiteName";
            var commerceAuthAppPool = "IIS:\\AppPools\\Default Web Site\\$CommerceAuthoringSiteName";
            var commerceMinionsAppPool = "IIS:\\AppPools\\Default Web Site\\$CommerceMinionsSiteName";
            var commerceBizFxAppPool = "IIS:\\AppPools\\Default Web Site\\$SitecoreBizFxSiteName";
            var idServerAppPool = "IIS:\\AppPools\\$SitecoreIdentityServerSiteName";
            var siteAppPool = "IIS:\\AppPools\\$SiteName";
            var xConnectAppPool = "IIS:\\AppPools\\$SitecorexConnectSiteName";

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

            var webRootPath = "c:\\inetpub\\wwwroot\\";
            var usersFolderPath = "c:\\users\\";
            var certificatesFolderPath = "c:\\certificates\\";
            var storefrontCertExt = ".storefront.com.crt";

            using var file = new StreamWriter(path);
            file.WriteLine("Param(");
            file.WriteLine("\t[string]$Prefix = \"" + txtSiteNamePrefix.Text + "\",");
            file.WriteLine("\t[string]$UserFolder = \"" + txtSiteNamePrefix.Text + "sc_User" + "\",");
            file.WriteLine("\t[string]$CommDbPrefix = \"" + txtSiteNamePrefix.Text + "_SitecoreCommerce" + "\",");
            file.WriteLine("\t[string]$SiteName = \"" + txtSiteName.Text + "\",");
            file.WriteLine("\t[string]$SitecoreBizFxSiteName = \"" + txtBizFxName.Text + "\",");
            file.WriteLine("\t[string]$CommerceServicesPostfix = \"" + txtCommerceSvcPostFix.Text + "\",");
            file.WriteLine("\t[string]$SolrService = \"" + txtSolrService.Text + "\",");
            file.WriteLine("\t[string]$PathToSolr = \"" + txtSolrRoot.Text + "\",");
            file.WriteLine("\t[string]$SqlServer = \"" + txtSitecoreDbServer.Text + "\",");
            file.WriteLine("\t[string]$SqlAccount = \"" + txtSitecoreSqlUser.Text + "\",");
            file.WriteLine("\t[string]$SqlPassword = \"" + txtSitecoreSqlPass.Text + "\",");
            file.WriteLine("\t[string]$SitecorexConnectSiteName = \"" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text + "\",");
            file.WriteLine("\t[string]$SitecoreIdentityServerSiteName = \"" + txtIDServerSiteName.Text + "\",");
            file.WriteLine("\t[string]$CommerceServicesHostPostfix = \"" + txtCommerceServicesHostPostFix.Text + "\",");
            file.WriteLine("\t[string]$CommerceOpsSiteName = \"CommerceOps_$CommerceServicesPostfix\", ");           
            file.WriteLine("\t[string]$CommerceShopsSiteName = \"CommerceShops_$CommerceServicesPostfix\", ");
            file.WriteLine("\t[string]$CommerceAuthoringSiteName = \"CommerceAuthoring_$CommerceServicesPostfix\", ");
            file.WriteLine("\t[string]$CommerceMinionsSiteName = \"CommerceMinions_$CommerceServicesPostfix\", ");
            file.WriteLine("\t[string]$SitecoreMarketingAutomationService=\"$SitecorexConnectSiteName-MarketingAutomationService\", ");
            file.WriteLine("\t[string]$SitecoreProcessingEngineService=\"$SitecorexConnectSiteName-ProcessingEngineService\", ");
            file.WriteLine("\t[string]$SitecoreIndexWorkerService=\"$SitecorexConnectSiteName-IndexWorker\"");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("Function Remove-Service{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$serviceName");
            file.WriteLine("\t)");
            file.WriteLine("\tif(Get-Service $serviceName -ErrorAction SilentlyContinue){");
            file.WriteLine("\t\tsc.exe delete $serviceName -Force");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Stop-Service{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$serviceName");
            file.WriteLine("\t)");
            file.WriteLine("\tif(Get-Service $serviceName -ErrorAction SilentlyContinue){");
            file.WriteLine("\t\tsc.exe stop $serviceName -Force");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Remove-Website{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$siteName");
            file.WriteLine("\t)");
            file.WriteLine("\t$appCmd=\"" + appcmdExe + "\"");
            file.WriteLine("\t& $appCmd delete site $siteName");            
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Stop-WebAppPool{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$appPoolName");
            file.WriteLine("\t)");
            file.WriteLine("\t\t$ApplicationPoolStatus = Get-WebAppPoolState $appPoolName");
            file.WriteLine("\t\t$ApplicationPoolStatusValue = $ApplicationPoolStatus.Value");
            file.WriteLine("\t\t#Write-Host \"$appPoolName-> $ApplicationPoolStatusValue\"");

            file.WriteLine("\t\tif ($ApplicationPoolStatus.Value -ne \"" + stoppedStatus + "\")");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\tStop-WebAppPool -Name $appPoolName");            
            file.WriteLine("\t\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Remove-AppPool{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$appPoolName");
            file.WriteLine("\t)");
            file.WriteLine("\t$appCmd=\"" + appcmdExe + "\"");
            file.WriteLine("\t& $appCmd delete apppool $appPoolName");
            file.WriteLine("}");

            file.WriteLine("if (Test-Path \"" + commerceShopsSitePath + "\") { Stop-Website -Name $CommerceShopsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceOpsSitePath + "\") { Stop-Website -Name $CommerceOpsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceAuthSitePath + "\") { Stop-Website -Name $CommerceAuthoringSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceMinionsSitePath + "\") { Stop-Website -Name $CommerceMinionsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceBizFxSitePath + "\") { Stop-Website -Name $SitecoreBizFxSiteName }");
            file.WriteLine("if (Test-Path \"" + idServerSitePath + "\") { Stop-Website -Name $SitecoreIdentityServerSiteName }");
            file.WriteLine("if (Test-Path \"" + sitePath + "\") { Stop-Website -Name $SiteName }");
            file.WriteLine("if (Test-Path \"" + xConnectSitePath + "\") { Stop-Website -Name $SitecorexConnectSiteName }");
            file.WriteLine();

            file.WriteLine("if (Test-Path \"" + commerceShopsAppPool + "\") { Stop-WebAppPool -appPoolName $CommerceShopsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceOpsAppPool + "\") { Stop-WebAppPool -appPoolName $CommerceOpsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceAuthAppPool + "\") { Stop-WebAppPool -appPoolName $CommerceAuthoringSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceMinionsAppPool + "\") { Stop-WebAppPool -appPoolName $CommerceMinionsSiteName }");
            file.WriteLine("if (Test-Path \"" + commerceBizFxAppPool + "\") { Stop-WebAppPool -appPoolName $SitecoreBizFxSiteName }");
            file.WriteLine("if (Test-Path \"" + idServerAppPool + "\") { Stop-WebAppPool -appPoolName $SitecoreIdentityServerSiteName }");
            file.WriteLine("if (Test-Path \"" + siteAppPool + "\") { Stop-WebAppPool -appPoolName $SiteName }");
            file.WriteLine("if (Test-Path \"" + xConnectAppPool + "\") { Stop-WebAppPool -appPoolName $SitecorexConnectSiteName }");
            file.WriteLine();

            file.WriteLine("Write-Host \"Stopping solr service\"");
            file.WriteLine("Stop-Service $SolrService");
            file.WriteLine("Write-Host \"Solr service stopped successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Stopping Marketing Automation service\"");
            file.WriteLine("Stop-Service $SitecoreMarketingAutomationService ");
            file.WriteLine("Write-Host \"Marketing Automation service stopped successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Stopping Processing Engine service\"");
            file.WriteLine("Stop-Service $SitecoreProcessingEngineService ");
            file.WriteLine("Write-Host \"Processing Engine service stopped successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Stopping Index Worker service\"");
            file.WriteLine("Stop-Service $SitecoreIndexWorkerService ");
            file.WriteLine("Write-Host \"Index Worker service stopped successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting App Pools\"");
            file.WriteLine("Remove-AppPool -appPoolName $CommerceOpsSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $CommerceShopsSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $CommerceAuthoringSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $CommerceMinionsSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $SitecoreBizFxSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $SitecoreIdentityServerSiteName");
            file.WriteLine("Remove-AppPool -appPoolName $SiteName");
            file.WriteLine("Remove-AppPool -appPoolName $SitecorexConnectSiteName");
            file.WriteLine("Write-Host \"App Pools deleted successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting Websites from IIS\"");
            file.WriteLine("Remove-Website -siteName $CommerceOpsSiteName");
            file.WriteLine("Remove-Website -siteName $CommerceShopsSiteName");
            file.WriteLine("Remove-Website -siteName $CommerceAuthoringSiteName");
            file.WriteLine("Remove-Website -siteName $CommerceMinionsSiteName");
            file.WriteLine("Remove-Website -siteName $SitecoreBizFxSiteName");
            file.WriteLine("Remove-Website -siteName $SitecoreIdentityServerSiteName");
            file.WriteLine("Remove-Website -siteName $SiteName");
            file.WriteLine("Remove-Website -siteName $SitecorexConnectSiteName");
            file.WriteLine("Write-Host \"IIS Websites deleted successfully\"");
            file.WriteLine();

            file.WriteLine("#Drop databases from SQL");
            file.WriteLine("Write-Host \"Dropping databases from SQL server\"");
            file.WriteLine("push-location");
            file.WriteLine("import-module sqlps");
            //alter database YourDb set single_user with rollback immediate
            file.WriteLine("Write-Host \"Dropping databases from SQL server\"");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + coreDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + masterDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + webDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + exmMasterDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + refDataDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + reportingDBSuffix + "]\"");
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
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + sharedenvDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");
            file.WriteLine("$sitecoreDbPrefix = \"" + dropStmtstring + "\"+ $Prefix +\"" + globalDBSuffix + "]\"");
            file.WriteLine("invoke-sqlcmd -ServerInstance $SqlServer -U $SqlAccount -P $SqlPassword -Query $sitecoreDbPrefix");

            file.WriteLine("Write-Host \"Databases dropped successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Removing Services\"");
            file.WriteLine("Remove-Service $SolrService");
            file.WriteLine("Remove-Service $SitecoreMarketingAutomationService");
            file.WriteLine("Remove-Service $SitecoreProcessingEngineService");
            file.WriteLine("Remove-Service $SitecoreIndexWorkerService");
            file.WriteLine("Write-Host \"Solr, Marketing Automation, Processing Engine, Index Worker services removed\"");
            file.WriteLine();

            file.WriteLine("# Delete solr cores");
            file.WriteLine("Write-Host \"Deleting Solr directory\"");
            file.WriteLine("$pathToCores = $PathToSolr");
            file.WriteLine("rm $PathToSolr -recurse -force -ea ig");
            file.WriteLine("Write-Host \"Solr folder deleted successfully\"");

            file.WriteLine("Write-Host \"Deleting Websites from wwwroot\"");
            file.WriteLine("rm " + webRootPath + "$CommerceOpsSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$CommerceShopsSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$CommerceAuthoringSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$CommerceMinionsSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$SitecoreBizFxSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$SitecoreIdentityServerSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$SiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$SitecorexConnectSiteName -force -recurse -ea ig");
            file.WriteLine("Write-Host \"Websites removed from wwwroot\"");

            file.WriteLine("Write-Host \"Deleting Windows Users from c:\\users\"");
            file.WriteLine("rm " + usersFolderPath + "$SitecorexConnectSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + usersFolderPath + "$SitecoreIdentityServerSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + usersFolderPath + "$SiteName -force -recurse -ea ig");
            file.WriteLine("rm " + usersFolderPath + "$UserFolder -force -recurse -ea ig");            
            file.WriteLine("Write-Host \"Website Folders removed from c:\\Users folder\"");

            file.WriteLine("rm " + certificatesFolderPath +  "$Prefix" + storefrontCertExt + " -force -recurse -ea ig");
            file.WriteLine("rm " + certificatesFolderPath + "$SitecoreIdentityServerSiteName" + ".pfx  -force -recurse -ea ig");
            file.WriteLine("rm " + certificatesFolderPath + "$SitecorexConnectSiteName" + ".pfx -force -recurse -ea ig");
            file.WriteLine("pop-location");
        }

        private bool FillSolrDetails()
        {
            SolrInfo info = CommonFunctions.GetSolrInformation(txtSolrUrl.Text);
            if (info==null) {
                SetStatusMessage("Check if Solr Service is running - " + txtSolrUrl.Text, Color.Red);             
                return false;
            }
            txtSolrRoot.Text = info.solr_home.Replace("\\server\\solr", string.Empty);
            int lastIndexofSlash = txtSolrRoot.Text.LastIndexOf("\\");
            txtSolrService.Text = StringRight(txtSolrRoot.Text, txtSolrRoot.Text.Length - lastIndexofSlash - 1);

            return true;
        }

        private string StringRight(string str, int length)
        {
            return str.Substring(str.Length - length, length);
        }

        private void btnSolr_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnSolr.Enabled = false;
            SetStatusMessage("Processing....", Color.Orange);

            
            SolrInfo info = CommonFunctions.GetSolrInformation(txtSolrUrl.Text);

            if (string.IsNullOrWhiteSpace(txtSolrUrl.Text) || info == null || info?.lucene?.SolrSpecVersion != "8.4.0")
            {
                TabIndexValue = const_Solr_Tab;
                ToggleButtonControls(false);
                MenubarControls(true);
                AssignStepStatus(TabIndexValue);
                btnSolr.Enabled = true;
            }


            if (info==null)
            {
                SetStatusMessage("Missing Solr Url Info... check if Solr is hosted and running...", Color.Red);
                return;
            }

            if (info.lucene.SolrSpecVersion != "8.4.0")
            {
                SetStatusMessage("That Solr Url doesn't run on Solr 8.4.0....", Color.Red);
                return;
            }

            txtSolrRoot.Text = info.solr_home.Replace("\\server\\solr", string.Empty);
            FillSolrDetails();
              
            ToggleEnableControls(true);
            SetStatusMessage("All seems fine with Solr....", Color.DarkGreen);
            btnSolr.Enabled = true;            
            Cursor.Current = Cursors.Default;
        }

        private bool ValidSolrUrl(string url)
        {
            try
            {
                using (WebClientResponse client = new WebClientResponse())
                {
                    client.HeadOnly = true;
                    string downloadString = client.DownloadString(url);
                }
            }
            catch(Exception ex)
            {
                return false;
            }
            return true;
        }

       
        private void btnValidateAll_Click(object sender, EventArgs e)
        {
            if (CheckAllValidations()) SetStatusMessage("Congrats! Passed all Validations!",Color.DarkGreen);
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

        private void btnScriptPreview_Click(object sender, EventArgs e)
        {
            int i = 1;
            SiteDetails siteDetails = new SiteDetails
            {
                 SiteNamePrefix=txtSiteNamePrefix.Text,
                SiteNameSuffix = txtSiteNameSuffix.Text,
                SiteName = txtSiteName.Text,
                IDServerSiteName = txtIDServerSiteName.Text,
                SitecoreIdentityServerUrl = txtSitecoreIdentityServerUrl.Text,
                CommerceEngineConnectClientId = txtCommerceEngineConnectClientId.Text,
                CommerceEngineConnectClientSecret = txtCommerceEngineConnectClientSecret.Text,
                SiteHostHeaderName = txtSiteHostHeaderName.Text,
                SitecoreDbServer = txtSitecoreDbServer.Text,
                SitecoreSqlUser = txtSitecoreSqlUser.Text,
               SitecoreSqlPass = txtSitecoreSqlPass.Text,
                SitecoreDomain = txtSitecoreDomain.Text,
                        SitecoreUsername = txtSitecoreUsername.Text,
                SitecoreUserPassword = txtSitecoreUserPassword.Text,
                 SearchIndexPrefix = txtSearchIndexPrefix.Text,
                 SolrUrl =txtSolrUrl.Text,
                 SolrRoot =txtSolrRoot.Text,
                 SolrService =txtSolrService.Text,
                 RedisHost =txtRedisHost.Text,
                 RedisPort =Convert.ToInt16(txtRedisPort.Value),
                 CommerceServicesDBServer =txtCommerceServicesDBServer.Text,
                 CommerceDbName =txtCommerceDbName.Text,
                 CommerceGlobalDbName = txtCommerceGlobalDbName.Text,
                        CommerceOpsSvcPort = Convert.ToInt16(txtCommerceOpsSvcPort.Value),
                 CommerceShopsServicesPort = Convert.ToInt16(txtCommerceShopsServicesPort.Value),
                 CommerceAuthSvcPort = Convert.ToInt16(txtCommerceAuthSvcPort.Value),
                 CommerceMinionsSvcPort = Convert.ToInt16(txtCommerceMinionsSvcPort.Value),
                 CommerceSvcPostFix =txtCommerceSvcPostFix.Text,
                 CommerceServicesHostPostFix =txtCommerceServicesHostPostFix.Text,
                 BizFxName =txtBizFxName.Text,
                 BizFxPort = Convert.ToInt16(txtBizFxPort.Value),
                 EnvironmentsPrefix =txtEnvironmentsPrefix.Text,
                DeploySampleData=(chkDeploySampleData.Checked==true)?"Y":"N",
                 UserName =txtUserName.Text,
                 UserPassword =txtUserPassword.Text,
                 BraintreeMerchantId =txttxtBraintreeMerchantId.Text,
                 BraintreePublicKey =txtBraintreePublicKey.Text,
                 BraintreePrivateKey =txtBraintreePrivateKey.Text,
                 BraintreeEnvironment =txtBraintreeEnvironment.Text,
                 HabitatExists = HabitatExists(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text))
        };
            ScriptPreview preview = new ScriptPreview(siteDetails: siteDetails);
            preview.ShowDialog();
        }

        private void btnAppSettings_EnabledChanged(object sender, EventArgs e)
        {
        }

    }

}
