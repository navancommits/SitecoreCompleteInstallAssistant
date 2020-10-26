﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace SCIA
{

    public partial class SitecoreCommerceInstaller : Form
    {
        string SystemDrive = "C:";
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
        string siteNamePrefixString = string.Empty;//sc
        string identityServerNameString = string.Empty;//identityserver
        string xConnectServerNameString = string.Empty;//xconnect
        string siteRootDir = string.Empty;//"c:\\intetpub\\wwwroot\\
        string bizFxSitenamePrefix = string.Empty;//SitecoreBizFx-
        string commerceDbNameString = string.Empty;//"_sitecore_commerce_

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        
        public SitecoreCommerceInstaller()
        {
            InitializeComponent();
            SystemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));
            //txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            //txtSearchIndexPrefix.Text = txtSiteName.Text;
            //txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + txtSiteNameSuffix.Text;
            //txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + ".storefront.com";
            //txtSXAInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            //txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            //txtSqlDbPrefix.Text = txtSiteNamePrefix.Text;
            //txtSitecoreCoreDbName.Text = txtSqlDbPrefix.Text + "_Core";
            //txtCommerceDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "SharedEnvironments";
            //txtCommerceGlobalDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "Global";
            //txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + siteNamePrefixString;
            //txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + ".com";
            //txtBizFxName.Text = bizFxSitenamePrefix + txtCommerceSvcPostFix.Text;
            //txtUserName.Text = txtCommerceSvcPostFix.Text + "_User";
            //txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            //txtStorefrontIndexPrefix.Text = txtSiteName.Text;
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
            AssignStepStatus(TabIndexValue);
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
                                txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + ".storefront.com";
                                txtSitecoreCoreDbName.Text = txtSqlDbPrefix.Text + "_Core";
                                txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + siteNamePrefixString;
                                txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + ".com";
                                txtBizFxName.Text = bizFxSitenamePrefix + txtCommerceSvcPostFix.Text;
                                txtUserName.Text = txtCommerceSvcPostFix.Text + "_User";
                                txtCommerceInstallRoot.Text = siteRootDir;
                                txtCommerceDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "SharedEnvironments";
                                txtCommerceGlobalDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "Global";

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
                        SetFieldValues();
                    }

                }
            }
        }


        void LaunchPSScript(string scriptname)
        {
            var script = @".\" + scriptname;
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -noexit -ExecutionPolicy Bypass \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }

        void WriteFile(string path, bool habitatflag, bool uninstallscript)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("#Requires -Version 3");
            file.WriteLine("param(");
            file.WriteLine("\t# The root folder with WDP files.");
            file.WriteLine("\t[string]$XCInstallRoot = \"..\",");
            file.WriteLine("\t# The root folder of SIF.Sitecore.Commerce package.");
            file.WriteLine("\t[string]$XCSIFInstallRoot = $PWD,");
            file.WriteLine(
                "\t# Specifies whether or not to bypass the installation of the default SXA Storefront. By default, the Sitecore XC installation script also deploys the SXA Storefront.");
            file.WriteLine("\t[bool]$SkipInstallDefaultStorefront = $false,");
            file.WriteLine("\t# Specifies whether or not to bypass the installation of the SXA Storefront packages.");
            file.WriteLine(
                "\t# If set to $true, $TasksToSkip parameter will be populated with the list of tasks to skip in order to bypass SXA Storefront packages installation.");
            file.WriteLine("\t[bool]$SkipDeployStorefrontPackages = $false,");
            file.WriteLine();
            file.WriteLine(
                "\t# Path to the Master_SingleServer.json file provided in the SIF.Sitecore.Commerce package.");
            file.WriteLine("\t[string]$Path = \"$XCSIFInstallRoot\\Configuration\\Commerce\\Master_SingleServer.json\",");
            file.WriteLine("\t# Path to the Commerce Solr schemas provided as part of the SIF.Sitecore.Commerce package.");
            file.WriteLine("\t[string]$SolrSchemas = \"$XCSIFInstallRoot\\SolrSchemas\",");
            file.WriteLine("\t# Path to the SiteUtilityPages folder provided as part of the SIF.Sitecore.Commerce package.");
            file.WriteLine("\t[string]$SiteUtilitiesSrc = \"$XCSIFInstallRoot\\SiteUtilityPages\",");
            file.WriteLine("\t# Path to the location where you downloaded the Microsoft.Web.XmlTransform.dll file.");
            file.WriteLine(
                "\t[string]$MergeToolFullPath = \"$XCInstallRoot\\MSBuild.Microsoft.VisualStudio.Web.targets*\\tools\\VSToolsPath\\Web\\Microsoft.Web.XmlTransform.dll\",");
            file.WriteLine("\t# Path to the Adventure Works Images.OnPrem SCWDP file");
            file.WriteLine("\t[string]$AdventureWorksImagesWdpFullPath = \"$XCInstallRoot\\Adventure Works Images.OnPrem.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Connect Core SCWDP file.");
            file.WriteLine("\t[string]$CommerceConnectWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Connect Core*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Engine Connect OnPrem SCWDP file.");
            file.WriteLine(
                "\t[string]$CEConnectWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Engine Connect*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Experience Accelerator SCWDP file.");
            file.WriteLine(
                "\t[string]$SXACommerceWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Experience Accelerator*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Experience Accelerator Habitat Catalog SCWDP file.");
            file.WriteLine("\t[string]$SXAStorefrontCatalogWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Experience Accelerator Habitat*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Experience Accelerator Storefront SCWDP file.");
            file.WriteLine("\t[string]$SXAStorefrontWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Experience Accelerator Storefront*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Experience Accelerator Storefront Themes SCWDP file.");
            file.WriteLine("\t[string]$SXAStorefrontThemeWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Experience Accelerator Storefront Themes*.scwdp.zip\",");
            file.WriteLine("\t# Path to the Sitecore Commerce Experience Analytics Core SCWDP file.");
            file.WriteLine(
                "\t[string]$CommercexAnalyticsWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce ExperienceAnalytics Core*.scwdp.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore Commerce Experience Profile Core SCWDP file.");
            file.WriteLine(
                "\t[string]$CommercexProfilesWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce ExperienceProfile Core*.scwdp.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore Commerce Marketing Automation Core SCWDP file.");
            file.WriteLine(
                "\t[string]$CommerceMAWdpFullPath = \"$XCInstallRoot\\Sitecore Commerce Marketing Automation Core*.scwdp.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore Commerce Marketing Automation for AutomationEngine zip file.");
            file.WriteLine(
                "\t[string]$CommerceMAForAutomationEngineZIPFullPath = \"$XCInstallRoot\\Sitecore Commerce Marketing Automation for AutomationEngine*.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore Experience Accelerator zip file.");
            file.WriteLine(
                "\t[string]$SXAModuleZIPFullPath = \"$XCInstallRoot\\Sitecore Experience Accelerator*.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore.PowerShell.Extensions zip file.");
            file.WriteLine(
                "\t[string]$PowerShellExtensionsModuleZIPFullPath = \"$XCInstallRoot\\Sitecore.PowerShell.Extensions*.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore BizFx Server SCWDP file.");
            file.WriteLine(
                "\t[string]$BizFxPackage = \"$XCInstallRoot\\Sitecore.BizFx.OnPrem*scwdp.zip\",");
            file.WriteLine(
                "\t# Path to the Commerce Engine Service SCWDP file.");
            file.WriteLine(
                "\t[string]$CommerceEngineWdpFullPath = \"$XCInstallRoot\\Sitecore.Commerce.Engine.OnPrem.Solr.*scwdp.zip\",");
            file.WriteLine(
                "\t# Path to the Sitecore.Commerce.Habitat.Images.OnPrem SCWDP file.");
            file.WriteLine(
                "\t[string]$HabitatImagesWdpFullPath = \"$XCInstallRoot\\Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip\",");
            file.WriteLine();
            file.WriteLine(
                "\t# The prefix that will be used on SOLR, Website and Database instances. The default value matches the Sitecore XP default.");
            file.WriteLine(
                "\t[string]$SiteNamePrefix = \"" + txtSiteNamePrefix.Text + "\",");

            file.WriteLine("\t# The name of the Sitecore site instance.");
            file.WriteLine("\t[string]$SiteName = \"" + txtSiteName.Text + "\",");
            file.WriteLine("\t# Identity Server site name.");
            file.WriteLine("\t[string]$IdentityServerSiteName = \"" + txtIDServerSiteName.Text + "\",");
            file.WriteLine("\t# The url of the Sitecore Identity server.");
            file.WriteLine("\t[string]$SitecoreIdentityServerUrl = \"" + txtSitecoreIdentityServerUrl.Text + "\",");
            file.WriteLine("\t# The Commerce Engine Connect Client Id for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientId = \"" + txtCommerceEngineConnectClientId.Text + "\",");

            file.WriteLine("\t# The Commerce Engine Connect Client Secret for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientSecret = \"" + txtCommerceEngineConnectClientSecret.Text + "\",");
            file.WriteLine("\t# The host header name for the Sitecore storefront site.");
            file.WriteLine("\t[string]$SiteHostHeaderName = \"" + txtSiteHostHeaderName.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The path of the Sitecore XP site.");
            file.WriteLine("\t[string]$InstallDir = \"" + txtSXAInstallDir.Text + "\",");
            file.WriteLine("\t# The path of the Sitecore XConnect site.");
            file.WriteLine("\t[string]$XConnectInstallDir = \"" + txtxConnectInstallDir.Text + "\",");
            file.WriteLine("\t# The path to the inetpub folder where Commerce is installed.");
            file.WriteLine("\t[string]$CommerceInstallRoot = \"" + txtCommerceInstallRoot.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for Sitecore core and master databases.");
            file.WriteLine("\t[string]$SqlDbPrefix = $SiteNamePrefix,");
            file.WriteLine("\t# The location of the database server where Sitecore XP databases are hosted. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\".");

            file.WriteLine("\t[string]$SitecoreDbServer = \"" + txtSitecoreDbServer.Text + "\",");
            file.WriteLine("\t# The name of the Sitecore core database.");
            file.WriteLine("\t[string]$SitecoreCoreDbName = \"$($SqlDbPrefix)_Core\",");
            file.WriteLine("\t# A SQL user with sysadmin privileges.");
            file.WriteLine("\t[string]$SqlUser = \"" + txtSitecoreSqlUser.Text + "\",");
            file.WriteLine("\t# The password for $SQLAdminUser.");
            file.WriteLine("\t[string]$SqlPass = \"" + txtSitecoreSqlPass.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore domain.");
            file.WriteLine("\t[string]$SitecoreDomain = \"" + txtSitecoreDomain.Text + "\",");
            file.WriteLine("\t# The name of the Sitecore user account.");
            file.WriteLine("\t[string]$SitecoreUsername = \"" + txtSitecoreUsername.Text + "\",");
            file.WriteLine("\t# The password for the $SitecoreUsername.");
            file.WriteLine("\t[string]$SitecoreUserPassword = \"" + txtSitecoreUserPassword.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for the Search index. Using the SiteName value for the prefix is recommended.");
            file.WriteLine("\t[string]$SearchIndexPrefix = \"" + txtSearchIndexPrefix.Text + "\",");
            file.WriteLine("\t# The URL of the Solr Server.");
            file.WriteLine("\t[string]$SolrUrl =  \"" + txtSolrUrl.Text + "\",");
            file.WriteLine("\t# The folder that Solr has been installed to.");
            file.WriteLine("\t[string]$SolrRoot =  \"" + txtSolrRoot.Text + "\",");
            file.WriteLine("\t# The name of the Solr Service.");
            file.WriteLine("\t[string]$SolrService =  \"" + txtSolrService.Text + "\",");
            file.WriteLine("\t# The prefix for the Storefront index. The default value is the SiteNamePrefix.");
            file.WriteLine("\t[string]$StorefrontIndexPrefix = $SiteNamePrefix,");
            file.WriteLine();
            file.WriteLine("\t# The host name where Redis is hosted.");
            file.WriteLine("\t[string]$RedisHost =  \"" + txtRedisHost.Text + "\",");
            file.WriteLine("\t# The port number on which Redis is running.");
            file.WriteLine("\t[string]$RedisPort = \"" + txtRedisPort.Text + "\",");
            file.WriteLine("\t# The name of the Redis instance.");
            file.WriteLine("\t[string]$RedisInstanceName = \"Redis\",");
            file.WriteLine("\t# The path to the redis-cli executable.");
            file.WriteLine("\t[string]$RedisCliPath = \"$($Env:SYSTEMDRIVE)\\Program Files\\Redis\\redis-cli.exe\",");
            file.WriteLine();
            file.WriteLine("\t# The location of the database server where Commerce databases should be deployed. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\"");
            file.WriteLine("\t[string]$CommerceServicesDbServer = \"" + txtCommerceServicesDBServer.Text + "\",");
            file.WriteLine("\t# The name of the shared database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesDbName = \"" + txtCommerceDbName.Text + "\",");
            file.WriteLine("\t# The name of the global database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesGlobalDbName =  \"" + txtCommerceGlobalDbName.Text + "\",");
            file.WriteLine("\t# The port for the Commerce Ops Service.");
            file.WriteLine("\t[string]$CommerceOpsServicesPort = \"" + txtCommerceOpsSvcPort.Value.ToString() + "\",");
            file.WriteLine("\t# The port for the Commerce Shops Service");
            file.WriteLine("\t[string]$CommerceShopsServicesPort = \"" + txtCommerceShopsServicesPort.Value.ToString() + "\",");
            file.WriteLine("\t# The port for the Commerce Authoring Service.");
            file.WriteLine("\t[string]$CommerceAuthoringServicesPort = \"" + txtCommerceAuthSvcPort.Value.ToString() + "\",");
            file.WriteLine("\t# The port for the Commerce Minions Service.");
            file.WriteLine("\t[string]$CommerceMinionsServicesPort = \"" + txtCommerceMinionsSvcPort.Value.ToString() + "\",");
            file.WriteLine("\t# The postfix appended to Commerce services folders names and sitenames.");
            file.WriteLine("\t# The postfix allows you to host more than one Commerce installment on one server.");
            file.WriteLine("\t[string]$CommerceServicesPostfix = \"" + txtCommerceSvcPostFix.Text + "\",");
            file.WriteLine("\t# The postfix used as the root domain name (two-levels) to append as the hostname for Commerce services.");
            file.WriteLine("\t# By default, all Commerce services are configured as sub-domains of the domain identified by the postfix.");
            file.WriteLine("\t# Postfix validation enforces the following rules:");
            file.WriteLine("\t# 1. The first level (TopDomainName) must be 2-7 characters in length and can contain alphabetical characters (a-z, A-Z) only. Numeric and special characters are not valid.");
            file.WriteLine("\t# 2. The second level (DomainName) can contain alpha-numeric characters (a-z, A-Z,and 0-9) and can include one hyphen (-) character.");
            file.WriteLine("\t# Special characters (wildcard (*)), for example, are not valid.");
            file.WriteLine("\t[string]$CommerceServicesHostPostfix = \"" + txtCommerceServicesHostPostFix.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxSiteName = \"" + txtBizFxName.Text + "\",");
            file.WriteLine("\t# The port of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxPort = \"" + txtBizFxPort.Value.ToString() + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix used in the EnvironmentName setting in the config.json file for each Commerce Engine role.");
            file.WriteLine("\t[string]$EnvironmentsPrefix = \"Habitat\",");
            file.WriteLine("\t# The list of Commerce environment names. By default, the script deploys the AdventureWorks and the Habitat environments.");
            file.WriteLine("\t[array]$Environments = @(\"AdventureWorksAuthoring\", \"HabitatAuthoring\"),");

            file.WriteLine("\t# Commerce environments GUIDs used to clean existing Redis cache during deployment. Default parameter values correspond to the default Commerce environment GUIDS.");
            file.WriteLine("\t[array]$EnvironmentsGuids = @(\"78a1ea611f3742a7ac899a3f46d60ca5\", \"40e77b7b4be94186b53b5bfd89a6a83b\"),");
            file.WriteLine("\t# The environments running the minions service. (This is required, for example, for running indexing minions).");
            file.WriteLine("\t[array]$MinionEnvironments = @(\"AdventureWorksMinions\", \"HabitatMinions\"),");
            file.WriteLine("\t# whether to deploy sample data for each environment.");
            if (chkDeploySampleData.Checked)
            {
                file.WriteLine("\t[bool]$DeploySampleData = $true,");
            }
            else
            {
                file.WriteLine("\t[bool]$DeploySampleData = $false,");
            }
            file.WriteLine();
            file.WriteLine("\t# The domain of the local account used for the various application pools created as part of the deployment.");
            file.WriteLine("\t[string]$UserDomain = $Env:COMPUTERNAME,");
            file.WriteLine("\t# The user name for a local account to be set up for the various application pools that are created as part of the deployment.");
            file.WriteLine("\t[string]$UserName = \"" + txtUserName.Text + "\",");
            file.WriteLine("\t# The password for the $UserName.");
            file.WriteLine("\t[string]$UserPassword = \"" + txtUserPassword.Text + "\",");
            file.WriteLine();
            file.WriteLine("\t# The Braintree Merchant Id.");
            file.WriteLine("\t[string]$BraintreeMerchantId = \"" + txttxtBraintreeMerchantId.Text + "\",");
            file.WriteLine("\t# The Braintree Public Key.");
            file.WriteLine("\t[string]$BraintreePublicKey = \"" + txtBraintreePublicKey.Text + "\",");
            file.WriteLine("\t# The Braintree Private Key.");
            file.WriteLine("\t[string]$BraintreePrivateKey = \"" + txtBraintreePrivateKey.Text + "\",");
            file.WriteLine("\t# The Braintree Environment.");
            file.WriteLine("\t[string]$BraintreeEnvironment = \"sandbox\",");
            file.WriteLine();
            file.WriteLine("\t# List of comma-separated task names to skip during Sitecore XC deployment.");
            if (habitatflag && !uninstallscript)
            {
                file.WriteLine("\t[string]$TasksToSkip = \"Module-HabitatImages_InstallWDPModuleMasterCore,Module-HabitatImages_InstallWDPModuleMaster,Module-HabitatImages_InstallWDPModuleCore,Module-AdventureWorksImages_InstallWDPModuleMasterCore,Module-AdventureWorksImages_InstallWDPModuleMaster,Module-AdventureWorksImages_InstallWDPModuleCore,RebuildIndexes_RebuildIndex-Master,RebuildIndexes_RebuildIndex-Web\"");
            }
            else
            {
                file.WriteLine("\t[string]$TasksToSkip = \"\"");
            }

            file.WriteLine(")");
            file.WriteLine();
            file.WriteLine("Function Resolve-ItemPath {");
            file.WriteLine("\tparam (");
            file.WriteLine("\t\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t\t[ValidateNotNullorEmpty()]");
            file.WriteLine("\t\t[string] $Path");
            file.WriteLine("\t)");
            file.WriteLine("\tprocess {");
            file.WriteLine("\t\tif ([string]::IsNullOrWhiteSpace($Path)) {");
            file.WriteLine("\t\t\tthrow \"Parameter could not be validated because it contains only whitespace. Please check script parameters.\"");
            file.WriteLine("\t\t}");
            file.WriteLine("\t\t$itemPath = Resolve-Path -Path $Path -ErrorAction SilentlyContinue | Select-Object -First 1");
            file.WriteLine("\t\tif ([string]::IsNullOrEmpty($itemPath) -or (-not (Test-Path $itemPath))) {");
            file.WriteLine("\t\t\tthrow \"Path[$Path] could not be resolved.Please check script parameters.\"");
            file.WriteLine("\t\t}");
            file.WriteLine();
            file.WriteLine("\t\tWrite-Host \"Found [$itemPath].\"");
            file.WriteLine("\t\treturn $itemPath");
            file.WriteLine("\t}");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("if (($SkipDeployStorefrontPackages -eq $true) -and ($SkipInstallDefaultStorefront -eq $false)) {");
            file.WriteLine("\tthrow \"You cannot install the SXA Storefront without deploying necessary packages. If you want to install the SXA Storefront, set [SkipDeployStorefrontPackages] parameter to [false].\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("if (($DeploySampleData -eq $false) -and ($SkipInstallDefaultStorefront -eq $false)) {");
            file.WriteLine("\tthrow \"You cannot install the SXA Storefront without deploying sample data. If you want to install the SXA Storefront, set [DeploySampleData] parameter to [true].\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("[string[]] $Skip = @()");
            file.WriteLine("if (-not ([string]::IsNullOrWhiteSpace($TasksToSkip))) {");
            file.WriteLine("\t$TasksToSkip.Split(',') | ForEach-Object { $Skip += $_.Trim() }");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("Push-Location $PSScriptRoot");
            file.WriteLine();
            file.WriteLine("$modulesPath = ( Join-Path -Path $PWD -ChildPath \"Modules\" )");
            file.WriteLine("if ($env:PSModulePath -notlike \"*$modulesPath*\") {");
            file.WriteLine("\t[Environment]::SetEnvironmentVariable(\"PSModulePath\", \"$env:PSModulePath;$modulesPath\")");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("$deployCommerceParams = @{");
            file.WriteLine("\tPath                                     = Resolve-ItemPath -Path $Path");
            file.WriteLine("\tSolrSchemas              				  = Resolve-ItemPath -Path $SolrSchemas");
            file.WriteLine("\tSiteUtilitiesSrc                         = Resolve-ItemPath -Path $SiteUtilitiesSrc");
            file.WriteLine("\tMergeToolFullPath                        = Resolve-ItemPath -Path $MergeToolFullPath");
            file.WriteLine("\tAdventureWorksImagesWdpFullPath          = Resolve-ItemPath -Path $AdventureWorksImagesWdpFullPath");
            file.WriteLine("\tCommerceConnectWdpFullPath               = Resolve-ItemPath -Path $CommerceConnectWdpFullPath");
            file.WriteLine("\tCEConnectWdpFullPath                     = Resolve-ItemPath -Path $CEConnectWdpFullPath");
            file.WriteLine("\tSXACommerceWdpFullPath                   = Resolve-ItemPath -Path $SXACommerceWdpFullPath");
            file.WriteLine("\tSXAStorefrontCatalogWdpFullPath          = Resolve-ItemPath -Path $SXAStorefrontCatalogWdpFullPath");
            file.WriteLine("\tSXAStorefrontWdpFullPath                 = Resolve-ItemPath -Path $SXAStorefrontWdpFullPath");
            file.WriteLine("\tSXAStorefrontThemeWdpFullPath            = Resolve-ItemPath -Path $SXAStorefrontThemeWdpFullPath");
            file.WriteLine("\tCommercexAnalyticsWdpFullPath            = Resolve-ItemPath -Path $CommercexAnalyticsWdpFullPath");
            file.WriteLine("\tCommercexProfilesWdpFullPath             = Resolve-ItemPath -Path $CommercexProfilesWdpFullPath");
            file.WriteLine("\tCommerceMAWdpFullPath                    = Resolve-ItemPath -Path $CommerceMAWdpFullPath");
            file.WriteLine("\tCommerceMAForAutomationEngineZIPFullPath = Resolve-ItemPath -Path $CommerceMAForAutomationEngineZIPFullPath");
            file.WriteLine("\tSXAModuleZIPFullPath                     = Resolve-ItemPath -Path $SXAModuleZIPFullPath");
            file.WriteLine("\tPowerShellExtensionsModuleZIPFullPath    = Resolve-ItemPath -Path $PowerShellExtensionsModuleZIPFullPath");
            file.WriteLine("\tBizFxPackage                             = Resolve-ItemPath -Path $BizFxPackage");
            file.WriteLine("\tCommerceEngineWdpFullPath                = Resolve-ItemPath -Path $CommerceEngineWdpFullPath");
            file.WriteLine("\tHabitatImagesWdpFullPath                 = Resolve-ItemPath -Path $HabitatImagesWdpFullPath");
            file.WriteLine("\tSiteName                                 = $SiteName");
            file.WriteLine("\tSiteHostHeaderName                       = $SiteHostHeaderName");
            file.WriteLine("\tInstallDir                               = Resolve-ItemPath -Path $InstallDir");
            file.WriteLine("\tXConnectInstallDir                       = Resolve-ItemPath -Path $XConnectInstallDir");
            file.WriteLine("\tCommerceInstallRoot                      = Resolve-ItemPath -Path $CommerceInstallRoot");
            file.WriteLine("\tCommerceServicesDbServer                 = $CommerceServicesDbServer");
            file.WriteLine("\tCommerceServicesDbName                   = $CommerceServicesDbName");
            file.WriteLine("\tCommerceServicesGlobalDbName             = $CommerceServicesGlobalDbName");
            file.WriteLine("\tSitecoreDbServer                         = $SitecoreDbServer");
            file.WriteLine("\tSitecoreCoreDbName                       = $SitecoreCoreDbName");
            file.WriteLine("\tSqlDbPrefix                              = $SqlDbPrefix");
            file.WriteLine("\tSqlAdminUser                             = $SqlUser");
            file.WriteLine("\tSqlAdminPassword                         = $SqlPass");
            file.WriteLine("\tSolrUrl                                  = $SolrUrl");
            file.WriteLine("\tSolrRoot                                 = Resolve-ItemPath -Path $SolrRoot");
            file.WriteLine("\tSolrService                              = $SolrService");
            file.WriteLine("\tSearchIndexPrefix                        = $SearchIndexPrefix");
            file.WriteLine("\tStorefrontIndexPrefix                    = $StorefrontIndexPrefix");
            file.WriteLine("\tCommerceServicesPostfix                  = $CommerceServicesPostfix");
            file.WriteLine("\tCommerceServicesHostPostfix              = $CommerceServicesHostPostfix");
            file.WriteLine("\tEnvironmentsPrefix                       = $EnvironmentsPrefix");
            file.WriteLine("\tEnvironments                             = $Environments");
            file.WriteLine("\tEnvironmentsGuids                        = $EnvironmentsGuids");
            file.WriteLine("\tMinionEnvironments                       = $MinionEnvironments");
            file.WriteLine("\tCommerceOpsServicesPort                  = $CommerceOpsServicesPort");
            file.WriteLine("\tCommerceShopsServicesPort                = $CommerceShopsServicesPort");
            file.WriteLine("\tCommerceAuthoringServicesPort            = $CommerceAuthoringServicesPort");
            file.WriteLine("\tCommerceMinionsServicesPort              = $CommerceMinionsServicesPort");
            file.WriteLine("\tRedisInstanceName                        = $RedisInstanceName");
            file.WriteLine("\tRedisCliPath                             = $RedisCliPath");
            file.WriteLine("\tRedisHost                                = $RedisHost");
            file.WriteLine("\tRedisPort                                = $RedisPort");
            file.WriteLine("\tUserDomain                               = $UserDomain");
            file.WriteLine("\tUserName                                 = $UserName");
            file.WriteLine("\tUserPassword                             = $UserPassword");
            file.WriteLine("\tBraintreeMerchantId                      = $BraintreeMerchantId");
            file.WriteLine("\tBraintreePublicKey                       = $BraintreePublicKey");
            file.WriteLine("\tBraintreePrivateKey                      = $BraintreePrivateKey");
            file.WriteLine("\tBraintreeEnvironment                     = $BraintreeEnvironment");
            file.WriteLine("\tSitecoreDomain                           = $SitecoreDomain");
            file.WriteLine("\tSitecoreUsername                         = $SitecoreUsername");
            file.WriteLine("\tSitecoreUserPassword                     = $SitecoreUserPassword");
            file.WriteLine("\tBizFxSiteName                            = $BizFxSiteName");
            file.WriteLine("\tBizFxPort                                = $BizFxPort");
            file.WriteLine("\tSitecoreIdentityServerApplicationName    = $IdentityServerSiteName");
            file.WriteLine("\tSitecoreIdentityServerUrl                = $SitecoreIdentityServerUrl");
            file.WriteLine("\tSkipInstallDefaultStorefront             = $SkipInstallDefaultStorefront");
            file.WriteLine("\tSkipDeployStorefrontPackages             = $SkipDeployStorefrontPackages");
            file.WriteLine("\tCommerceEngineConnectClientId            = $CommerceEngineConnectClientId");
            file.WriteLine("\tCommerceEngineConnectClientSecret        = $CommerceEngineConnectClientSecret");
            file.WriteLine("\tDeploySampleData                         = $DeploySampleData");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("if ($Skip.Count -eq 0) {");
            if (uninstallscript)
            {
                file.WriteLine("\tUnInstall-SitecoreConfiguration @deployCommerceParams -Verbose *>&1 | Tee-Object \"$XCSIFInstallRoot\\XC-Install.log\"");
            }
            else
            {
                file.WriteLine("\tInstall-SitecoreConfiguration @deployCommerceParams -Verbose *>&1 | Tee-Object \"$XCSIFInstallRoot\\XC-Install.log\"");
            }
            file.WriteLine("}");
            file.WriteLine("else {");
            if (!uninstallscript)
            {
                file.WriteLine("\tInstall-SitecoreConfiguration @deployCommerceParams -Skip $Skip -Verbose *>&1 | Tee-Object \"$XCSIFInstallRoot\\XC-Install.log\"");
            }
            else
            {
                file.WriteLine("\tUnInstall-SitecoreConfiguration @deployCommerceParams -Skip $Skip -Verbose *>&1 | Tee-Object \"$XCSIFInstallRoot\\XC-Install.log\"");
            }
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# SIG # Begin signature block");
            file.WriteLine("# MIIXwQYJKoZIhvcNAQcCoIIXsjCCF64CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB");
            file.WriteLine("# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR");
            file.WriteLine("# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUhy349aqDO4jjrB5wZPv+ZL41");
            file.WriteLine("# 6RygghL8MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B");
            file.WriteLine("# AQUFADCBizELMAkGA1UEBhMCWkExFTATBgNVBAgTDFdlc3Rlcm4gQ2FwZTEUMBIG");
            file.WriteLine("# A1UEBxMLRHVyYmFudmlsbGUxDzANBgNVBAoTBlRoYXd0ZTEdMBsGA1UECxMUVGhh");
            file.WriteLine("# d3RlIENlcnRpZmljYXRpb24xHzAdBgNVBAMTFlRoYXd0ZSBUaW1lc3RhbXBpbmcg");
            file.WriteLine("# Q0EwHhcNMTIxMjIxMDAwMDAwWhcNMjAxMjMwMjM1OTU5WjBeMQswCQYDVQQGEwJV");
            file.WriteLine("# UzEdMBsGA1UEChMUU3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFu");
            file.WriteLine("# dGVjIFRpbWUgU3RhbXBpbmcgU2VydmljZXMgQ0EgLSBHMjCCASIwDQYJKoZIhvcN");
            file.WriteLine("# AQEBBQADggEPADCCAQoCggEBALGss0lUS5ccEgrYJXmRIlcqb9y4JsRDc2vCvy5Q");
            file.WriteLine("# WvsUwnaOQwElQ7Sh4kX06Ld7w3TMIte0lAAC903tv7S3RCRrzV9FO9FEzkMScxeC");
            file.WriteLine("# i2m0K8uZHqxyGyZNcR+xMd37UWECU6aq9UksBXhFpS+JzueZ5/6M4lc/PcaS3Er4");
            file.WriteLine("# ezPkeQr78HWIQZz/xQNRmarXbJ+TaYdlKYOFwmAUxMjJOxTawIHwHw103pIiq8r3");
            file.WriteLine("# +3R8J+b3Sht/p8OeLa6K6qbmqicWfWH3mHERvOJQoUvlXfrlDqcsn6plINPYlujI");
            file.WriteLine("# fKVOSET/GeJEB5IL12iEgF1qeGRFzWBGflTBE3zFefHJwXECAwEAAaOB+jCB9zAd");
            file.WriteLine("# BgNVHQ4EFgQUX5r1blzMzHSa1N197z/b7EyALt0wMgYIKwYBBQUHAQEEJjAkMCIG");
            file.WriteLine("# CCsGAQUFBzABhhZodHRwOi8vb2NzcC50aGF3dGUuY29tMBIGA1UdEwEB/wQIMAYB");
            file.WriteLine("# Af8CAQAwPwYDVR0fBDgwNjA0oDKgMIYuaHR0cDovL2NybC50aGF3dGUuY29tL1Ro");
            file.WriteLine("# YXd0ZVRpbWVzdGFtcGluZ0NBLmNybDATBgNVHSUEDDAKBggrBgEFBQcDCDAOBgNV");
            file.WriteLine("# HQ8BAf8EBAMCAQYwKAYDVR0RBCEwH6QdMBsxGTAXBgNVBAMTEFRpbWVTdGFtcC0y");
            file.WriteLine("# MDQ4LTEwDQYJKoZIhvcNAQEFBQADgYEAAwmbj3nvf1kwqu9otfrjCR27T4IGXTdf");
            file.WriteLine("# plKfFo3qHJIJRG71betYfDDo+WmNI3MLEm9Hqa45EfgqsZuwGsOO61mWAK3ODE2y");
            file.WriteLine("# 0DGmCFwqevzieh1XTKhlGOl5QGIllm7HxzdqgyEIjkHq3dlXPx13SYcqFgZepjhq");
            file.WriteLine("# IhKjURmDfrYwggSjMIIDi6ADAgECAhAOz/Q4yP6/NW4E2GqYGxpQMA0GCSqGSIb3");
            file.WriteLine("# DQEBBQUAMF4xCzAJBgNVBAYTAlVTMR0wGwYDVQQKExRTeW1hbnRlYyBDb3Jwb3Jh");
            file.WriteLine("# dGlvbjEwMC4GA1UEAxMnU3ltYW50ZWMgVGltZSBTdGFtcGluZyBTZXJ2aWNlcyBD");
            file.WriteLine("# QSAtIEcyMB4XDTEyMTAxODAwMDAwMFoXDTIwMTIyOTIzNTk1OVowYjELMAkGA1UE");
            file.WriteLine("# BhMCVVMxHTAbBgNVBAoTFFN5bWFudGVjIENvcnBvcmF0aW9uMTQwMgYDVQQDEytT");
            file.WriteLine("# eW1hbnRlYyBUaW1lIFN0YW1waW5nIFNlcnZpY2VzIFNpZ25lciAtIEc0MIIBIjAN");
            file.WriteLine("# BgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAomMLOUS4uyOnREm7Dv+h8GEKU5Ow");
            file.WriteLine("# mNutLA9KxW7/hjxTVQ8VzgQ/K/2plpbZvmF5C1vJTIZ25eBDSyKV7sIrQ8Gf2Gi0");
            file.WriteLine("# jkBP7oU4uRHFI/JkWPAVMm9OV6GuiKQC1yoezUvh3WPVF4kyW7BemVqonShQDhfu");
            file.WriteLine("# ltthO0VRHc8SVguSR/yrrvZmPUescHLnkudfzRC5xINklBm9JYDh6NIipdC6Anqh");
            file.WriteLine("# d5NbZcPuF3S8QYYq3AhMjJKMkS2ed0QfaNaodHfbDlsyi1aLM73ZY8hJnTrFxeoz");
            file.WriteLine("# C9Lxoxv0i77Zs1eLO94Ep3oisiSuLsdwxb5OgyYI+wu9qU+ZCOEQKHKqzQIDAQAB");
            file.WriteLine("# o4IBVzCCAVMwDAYDVR0TAQH/BAIwADAWBgNVHSUBAf8EDDAKBggrBgEFBQcDCDAO");
            file.WriteLine("# BgNVHQ8BAf8EBAMCB4AwcwYIKwYBBQUHAQEEZzBlMCoGCCsGAQUFBzABhh5odHRw");
            file.WriteLine("# Oi8vdHMtb2NzcC53cy5zeW1hbnRlYy5jb20wNwYIKwYBBQUHMAKGK2h0dHA6Ly90");
            file.WriteLine("# cy1haWEud3Muc3ltYW50ZWMuY29tL3Rzcy1jYS1nMi5jZXIwPAYDVR0fBDUwMzAx");
            file.WriteLine("# oC+gLYYraHR0cDovL3RzLWNybC53cy5zeW1hbnRlYy5jb20vdHNzLWNhLWcyLmNy");
            file.WriteLine("# bDAoBgNVHREEITAfpB0wGzEZMBcGA1UEAxMQVGltZVN0YW1wLTIwNDgtMjAdBgNV");
            file.WriteLine("# HQ4EFgQURsZpow5KFB7VTNpSYxc/Xja8DeYwHwYDVR0jBBgwFoAUX5r1blzMzHSa");
            file.WriteLine("# 1N197z/b7EyALt0wDQYJKoZIhvcNAQEFBQADggEBAHg7tJEqAEzwj2IwN3ijhCcH");
            file.WriteLine("# bxiy3iXcoNSUA6qGTiWfmkADHN3O43nLIWgG2rYytG2/9CwmYzPkSWRtDebDZw73");
            file.WriteLine("# BaQ1bHyJFsbpst+y6d0gxnEPzZV03LZc3r03H0N45ni1zSgEIKOq8UvEiCmRDoDR");
            file.WriteLine("# EfzdXHZuT14ORUZBbg2w6jiasTraCXEQ/Bx5tIB7rGn0/Zy2DBYr8X9bCT2bW+IW");
            file.WriteLine("# yhOBbQAuOA2oKY8s4bL0WqkBrxWcLC9JG9siu8P+eJRRw4axgohd8D20UaF5Mysu");
            file.WriteLine("# e7ncIAkTcetqGVvP6KUwVyyJST+5z3/Jvz4iaGNTmr1pdKzFHTx/kuDDvBzYBHUw");
            file.WriteLine("# ggUrMIIEE6ADAgECAhAHplztCw0v0TJNgwJhke9VMA0GCSqGSIb3DQEBCwUAMHIx");
            file.WriteLine("# CzAJBgNVBAYTAlVTMRUwEwYDVQQKEwxEaWdpQ2VydCBJbmMxGTAXBgNVBAsTEHd3");
            file.WriteLine("# dy5kaWdpY2VydC5jb20xMTAvBgNVBAMTKERpZ2lDZXJ0IFNIQTIgQXNzdXJlZCBJ");
            file.WriteLine("# RCBDb2RlIFNpZ25pbmcgQ0EwHhcNMTcwODIzMDAwMDAwWhcNMjAwOTMwMTIwMDAw");
            file.WriteLine("# WjBoMQswCQYDVQQGEwJVUzELMAkGA1UECBMCY2ExEjAQBgNVBAcTCVNhdXNhbGl0");
            file.WriteLine("# bzEbMBkGA1UEChMSU2l0ZWNvcmUgVVNBLCBJbmMuMRswGQYDVQQDExJTaXRlY29y");
            file.WriteLine("# ZSBVU0EsIEluYy4wggEiMA0GCSqGSIb3DQEBAQUAA4IBDwAwggEKAoIBAQC7PZ/g");
            file.WriteLine("# huhrQ/p/0Cg7BRrYjw7ZMx8HNBamEm0El+sedPWYeAAFrjDSpECxYjvK8/NOS9dk");
            file.WriteLine("# tC35XL2TREMOJk746mZqia+g+NQDPEaDjNPG/iT0gWsOeCa9dUcIUtnBQ0hBKsuR");
            file.WriteLine("# bau3n7w1uIgr3zf29vc9NhCoz1m2uBNIuLBlkKguXwgPt4rzj66+18JV3xyLQJoS");
            file.WriteLine("# 3ZAA8k6FnZltNB+4HB0LKpPmF8PmAm5fhwGz6JFTKe+HCBRtuwOEERSd1EN7TGKi");
            file.WriteLine("# xczSX8FJMz84dcOfALxjTj6RUF5TNSQLD2pACgYWl8MM0lEtD/1eif7TKMHqaA+s");
            file.WriteLine("# m/yJrlKEtOr836BvAgMBAAGjggHFMIIBwTAfBgNVHSMEGDAWgBRaxLl7Kgqjpepx");
            file.WriteLine("# A8Bg+S32ZXUOWDAdBgNVHQ4EFgQULh60SWOBOnU9TSFq0c2sWmMdu7EwDgYDVR0P");
            file.WriteLine("# AQH/BAQDAgeAMBMGA1UdJQQMMAoGCCsGAQUFBwMDMHcGA1UdHwRwMG4wNaAzoDGG");
            file.WriteLine("# L2h0dHA6Ly9jcmwzLmRpZ2ljZXJ0LmNvbS9zaGEyLWFzc3VyZWQtY3MtZzEuY3Js");
            file.WriteLine("# MDWgM6Axhi9odHRwOi8vY3JsNC5kaWdpY2VydC5jb20vc2hhMi1hc3N1cmVkLWNz");
            file.WriteLine("# LWcxLmNybDBMBgNVHSAERTBDMDcGCWCGSAGG/WwDATAqMCgGCCsGAQUFBwIBFhxo");
            file.WriteLine("# dHRwczovL3d3dy5kaWdpY2VydC5jb20vQ1BTMAgGBmeBDAEEATCBhAYIKwYBBQUH");
            file.WriteLine("# AQEEeDB2MCQGCCsGAQUFBzABhhhodHRwOi8vb2NzcC5kaWdpY2VydC5jb20wTgYI");
            file.WriteLine("# KwYBBQUHMAKGQmh0dHA6Ly9jYWNlcnRzLmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydFNI");
            file.WriteLine("# QTJBc3N1cmVkSURDb2RlU2lnbmluZ0NBLmNydDAMBgNVHRMBAf8EAjAAMA0GCSqG");
            file.WriteLine("# SIb3DQEBCwUAA4IBAQBozpJhBdsaz19E9faa/wtrnssUreKxZVkYQ+NViWeyImc5");
            file.WriteLine("# qEZcDPy3Qgf731kVPnYuwi5S0U+qyg5p1CNn/WsvnJsdw8aO0lseadu8PECuHj1Z");
            file.WriteLine("# 5w4mi5rGNq+QVYSBB2vBh5Ps5rXuifBFF8YnUyBc2KuWBOCq6MTRN1H2sU5LtOUc");
            file.WriteLine("# Qkacv8hyom8DHERbd3mIBkV8fmtAmvwFYOCsXdBHOSwQUvfs53GySrnIYiWT0y56");
            file.WriteLine("# mVYPwDj7h/PdWO5hIuZm6n5ohInLig1weiVDJ254r+2pfyyRT+02JVVxyHFMCLwC");
            file.WriteLine("# ASs4vgbiZzMDltmoTDHz9gULxu/CfBGM0waMDu3cMIIFMDCCBBigAwIBAgIQBAkY");
            file.WriteLine("# G1/Vu2Z1U0O1b5VQCDANBgkqhkiG9w0BAQsFADBlMQswCQYDVQQGEwJVUzEVMBMG");
            file.WriteLine("# A1UEChMMRGlnaUNlcnQgSW5jMRkwFwYDVQQLExB3d3cuZGlnaWNlcnQuY29tMSQw");
            file.WriteLine("# IgYDVQQDExtEaWdpQ2VydCBBc3N1cmVkIElEIFJvb3QgQ0EwHhcNMTMxMDIyMTIw");
            file.WriteLine("# MDAwWhcNMjgxMDIyMTIwMDAwWjByMQswCQYDVQQGEwJVUzEVMBMGA1UEChMMRGln");
            file.WriteLine("# aUNlcnQgSW5jMRkwFwYDVQQLExB3d3cuZGlnaWNlcnQuY29tMTEwLwYDVQQDEyhE");
            file.WriteLine("# aWdpQ2VydCBTSEEyIEFzc3VyZWQgSUQgQ29kZSBTaWduaW5nIENBMIIBIjANBgkq");
            file.WriteLine("# hkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEA+NOzHH8OEa9ndwfTCzFJGc/Q+0WZsTrb");
            file.WriteLine("# RPV/5aid2zLXcep2nQUut4/6kkPApfmJ1DcZ17aq8JyGpdglrA55KDp+6dFn08b7");
            file.WriteLine("# KSfH03sjlOSRI5aQd4L5oYQjZhJUM1B0sSgmuyRpwsJS8hRniolF1C2ho+mILCCV");
            file.WriteLine("# rhxKhwjfDPXiTWAYvqrEsq5wMWYzcT6scKKrzn/pfMuSoeU7MRzP6vIK5Fe7SrXp");
            file.WriteLine("# dOYr/mzLfnQ5Ng2Q7+S1TqSp6moKq4TzrGdOtcT3jNEgJSPrCGQ+UpbB8g8S9MWO");
            file.WriteLine("# D8Gi6CxR93O8vYWxYoNzQYIH5DiLanMg0A9kczyen6Yzqf0Z3yWT0QIDAQABo4IB");
            file.WriteLine("# zTCCAckwEgYDVR0TAQH/BAgwBgEB/wIBADAOBgNVHQ8BAf8EBAMCAYYwEwYDVR0l");
            file.WriteLine("# BAwwCgYIKwYBBQUHAwMweQYIKwYBBQUHAQEEbTBrMCQGCCsGAQUFBzABhhhodHRw");
            file.WriteLine("# Oi8vb2NzcC5kaWdpY2VydC5jb20wQwYIKwYBBQUHMAKGN2h0dHA6Ly9jYWNlcnRz");
            file.WriteLine("# LmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydEFzc3VyZWRJRFJvb3RDQS5jcnQwgYEGA1Ud");
            file.WriteLine("# HwR6MHgwOqA4oDaGNGh0dHA6Ly9jcmw0LmRpZ2ljZXJ0LmNvbS9EaWdpQ2VydEFz");
            file.WriteLine("# c3VyZWRJRFJvb3RDQS5jcmwwOqA4oDaGNGh0dHA6Ly9jcmwzLmRpZ2ljZXJ0LmNv");
            file.WriteLine("# bS9EaWdpQ2VydEFzc3VyZWRJRFJvb3RDQS5jcmwwTwYDVR0gBEgwRjA4BgpghkgB");
            file.WriteLine("# hv1sAAIEMCowKAYIKwYBBQUHAgEWHGh0dHBzOi8vd3d3LmRpZ2ljZXJ0LmNvbS9D");
            file.WriteLine("# UFMwCgYIYIZIAYb9bAMwHQYDVR0OBBYEFFrEuXsqCqOl6nEDwGD5LfZldQ5YMB8G");
            file.WriteLine("# A1UdIwQYMBaAFEXroq/0ksuCMS1Ri6enIZ3zbcgPMA0GCSqGSIb3DQEBCwUAA4IB");
            file.WriteLine("# AQA+7A1aJLPzItEVyCx8JSl2qB1dHC06GsTvMGHXfgtg/cM9D8Svi/3vKt8gVTew");
            file.WriteLine("# 4fbRknUPUbRupY5a4l4kgU4QpO4/cY5jDhNLrddfRHnzNhQGivecRk5c/5CxGwcO");
            file.WriteLine("# kRX7uq+1UcKNJK4kxscnKqEpKBo6cSgCPC6Ro8AlEeKcFEehemhor5unXCBc2XGx");
            file.WriteLine("# DI+7qPjFEmifz0DLQESlE/DmZAwlCEIysjaKJAL+L3J+HNdJRZboWR3p+nRka7Lr");
            file.WriteLine("# ZkPas7CM1ekN3fYBIM6ZMWM9CBoYs4GbT8aTEAb8B4H6i9r5gkn3Ym6hU/oSlBiF");
            file.WriteLine("# LpKR6mhsRDKyZqHnGKSaZFHvMYIELzCCBCsCAQEwgYYwcjELMAkGA1UEBhMCVVMx");
            file.WriteLine("# FTATBgNVBAoTDERpZ2lDZXJ0IEluYzEZMBcGA1UECxMQd3d3LmRpZ2ljZXJ0LmNv");
            file.WriteLine("# bTExMC8GA1UEAxMoRGlnaUNlcnQgU0hBMiBBc3N1cmVkIElEIENvZGUgU2lnbmlu");
            file.WriteLine("# ZyBDQQIQB6Zc7QsNL9EyTYMCYZHvVTAJBgUrDgMCGgUAoHAwEAYKKwYBBAGCNwIB");
            file.WriteLine("# DDECMAAwGQYJKoZIhvcNAQkDMQwGCisGAQQBgjcCAQQwHAYKKwYBBAGCNwIBCzEO");
            file.WriteLine("# MAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFHwhuiBSz5zCUVArLB0+ZBdk");
            file.WriteLine("# kxcrMA0GCSqGSIb3DQEBAQUABIIBAC+k4sN12pJTowhwtnxEmKjjqdrpsTDQtziI");
            file.WriteLine("# Iw7HhJlRBzqEbxfRc3sTYVvHKuODBZW1Fj9mtB1rYFz5zhgLkQGv+1jlfdtAeUff");
            file.WriteLine("# mrZe8LAKk/Gs3n32uDptZcrGcXUHl5oyyQicFMRlmeU0yPp3KhlYz+cdQDewPsKA");
            file.WriteLine("# eXuBwDRfTZ6ounInkxlBFcdbTZqwsChUYWC4IaBFb/J4GkAWBlxGPlw3ty3FuT1p");
            file.WriteLine("# uwRbNyrOT71/hwFIEdc36Y8M8Q9dEF7sOWKxvEtZ4aCXHtwZhbpT19l8VvDnjtEi");
            file.WriteLine("# jtyVS8PoYuISCsnzPr8Vmajn+d/B8XZT+0NnTlLxBeKBhKzZmj+hggILMIICBwYJ");
            file.WriteLine("# KoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQGEwJVUzEdMBsGA1UEChMU");
            file.WriteLine("# U3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFudGVjIFRpbWUgU3Rh");
            file.WriteLine("# bXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVuBNhqmBsaUDAJBgUrDgMC");
            file.WriteLine("# GgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkqhkiG9w0BCQUxDxcN");
            file.WriteLine("# MjAwNzI5MTQzMjM3WjAjBgkqhkiG9w0BCQQxFgQU1U2FxtyhU0AStBRmZXCxXzYP");
            file.WriteLine("# 1uUwDQYJKoZIhvcNAQEBBQAEggEAhtpgsIBkdxtX5EXVK1ZyRm3F+9qh38OCldqF");
            file.WriteLine("# Kwf7V9+vjy8lPaJlZ63gM9fpr3OprF0wcwkew8nm4FxlmGCns8tgA1KewkZWpqFu");
            file.WriteLine("# pDJKYRyycEySeq8HxTqm/xphUe1YrfOgaACNH+iykhMJDVLsFXyyIcZeHRSJIKjs");
            file.WriteLine("# BErKc4xfYZOMr0hjeN988B3jrPkmQHq08p1BmLjQ2DkuORDk16CMXg+yzufifWBv");
            file.WriteLine("# 1ssuVldvH25k4aPIg6Q1fcAxUjNL4ST8Zb9e9ximjL7DxIe+VAyhXEfBLYCB53p2");
            file.WriteLine("# wUZQ7JEOQJA+c1KEpo8R2cRfVzvkH3kgF9AnMuKnzUM8NnfaTg==");
            file.WriteLine("# SIG # End signature block");
            file.WriteLine();
            file.Dispose();
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
                
        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!ValidateAll(false)) return;
            bool habitatExists = HabitatExists(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text));

            WriteFile(txtSiteName.Text + "_Install_Script.ps1", habitatExists, false);
            LaunchPSScript(txtSiteName.Text + "_Install_Script.ps1");
            lblStatus.Text = "Installation successfully launched through Powershell....";
            lblStatus.ForeColor = Color.DarkGreen;             
        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {
            txtSXAInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
        }

        private bool ValidateAll(bool unInstall=false)
        {
            if (!ValidateData(txtSiteName, "Site Name",const_SiteInfo_Tab)) return false;
            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_General_Tab)) return false;
            if (!ValidateData(txtSitecoreIdentityServerUrl, "Sitecore Id Server Url", const_General_Tab)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientId, "Sitecore Commerce Connect Client Id", const_General_Tab)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientSecret, "Sitecore Commerce Connect Client Secret", const_General_Tab)) return false;
            if (!ValidateData(txtSiteHostHeaderName, "Site Host Header Name",const_General_Tab)) return false;

            if (!ValidateData(txtSXAInstallDir, "Sitecore SXA Install Directory", const_Install_Details_Tab)) return false;
            if (!ValidateData(txtxConnectInstallDir, "Sitecore xConnect Install Directory", const_Install_Details_Tab)) return false;
            if (!ValidateData(txtCommerceInstallRoot, "Commerce Install Root",const_Install_Details_Tab)) return false;

            if (!ValidateData(txtSqlDbPrefix, "Sql Db Prefix",const_Sitecore_DB_Tab)) return false;
            if (!ValidateData(txtSqlDbServer, "Sitecore Db Server", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSitecoreCoreDbName, "Sitecore Core Db Name",const_Sitecore_DB_Tab)) return false;
            if (!ValidateData(txtSqlUser, "Sql User",const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return false;

            if (!ValidateData(txtSitecoreDomain, "Sitecore Domain",const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUsername, "Sitecore Username", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", const_Sitecore_Tab)) return false;

            if (!ValidateData(txtSearchIndexPrefix, "Search Index Prefix",const_Solr_Tab)) return false;
            if (!ValidateData(txtSolrUrl, "Solr Url", const_Solr_Tab)) return false;
            if (!ValidateData(txtSolrRoot, "Solr Root Path", const_Solr_Tab)) return false;
            if (!ValidateData(txtSolrService, "Solr Service Name", const_Solr_Tab)) return false;
            if (!ValidateData(txtStorefrontIndexPrefix, "Storefront Index Prefix", const_Solr_Tab)) return false;

            if (!ValidateData(txtRedisHost, "Redis Host",const_Redis_Tab)) return false;
            //if (!ValidatePortNumber(txtRedisPort, "Redis Port",5)) return false;

            if (!ValidateData(txtCommerceServicesDBServer, "Commerce DB Server",const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceDbName, "Commerce DB Name", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceGlobalDbName, "Sitecore Commerce Global Db Name", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceSvcPostFix, "Sitecore Commerce Svc Post Fix", const_Commerce_Tab)) return false;
            if (!ValidateData(txtCommerceServicesHostPostFix, "Sitecore Commerce Svc Host Post Fix", const_Commerce_Tab)) return false;

            //if (!ValidatePortNumber(txtCommerceOpsSvcPort, "Commerce Ops Svc Port",7)) return false;
            //if (!ValidatePortNumber(txtCommerceShopsServicesPort, "Commerce Shops Svc Port",7)) return false;
            //if (!ValidatePortNumber(txtCommerceAuthSvcPort, "Commerce Auth Svc Port",7)) return false;
            //if (!ValidatePortNumber(txtCommerceMinionsSvcPort, "Commerce Minions Svc Port",7)) return false;
            //if (!ValidatePortNumber(txtBizFxPort, "BizFx Port Number",7)) return false;
            //if (!IsPortNotinUse(txtCommerceOpsSvcPort,7)) return false;
            //if (!IsPortNotinUse(txtCommerceShopsServicesPort,7)) return false;
            //if (!IsPortNotinUse(txtCommerceAuthSvcPort,7)) return false;
            //if (!IsPortNotinUse(txtCommerceMinionsSvcPort,7)) return false;
            //if (!IsPortNotinUse(txtBizFxPort,7)) return false;
            if (!PerformPortValidations(unInstall)) return false;
            if (!ValidateData(txtBizFxName, "BizFx Name",const_Redis_Tab)) return false;

            if (!ValidateData(txtUserDomain, "Win User Domain",const_Win_User_Tab)) return false;
            if (!ValidateData(txtUserName, "Win User Name", const_Win_User_Tab)) return false;
            if (!ValidateData(txtUserPassword, "Win User Password", const_Win_User_Tab)) return false;

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
            if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! provide unique port numbers...."; return false; }

            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red; }
            
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

        
        private bool CheckAllValidations(bool uninstall=false)
        {
            string portString = string.Empty;
            ToggleEnableControls(false);
            if (!ValidateAll(uninstall)) return false;
            if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! Provide unique port numbers...."; return false; }
            if (!Directory.Exists(txtSXAInstallDir.Text))
            {
                lblStatus.Text = "Missing Directory! Install SXA-site at - " + txtSXAInstallDir.Text;
                TabIndexValue = const_SiteInfo_Tab;
                AssignStepStatus(TabIndexValue);
                lblStatus.ForeColor = Color.Red;
                return false;
            }
            if (!Directory.Exists(txtSXAInstallDir.Text + "\\App_Config\\Modules\\SXA"))
            {
                lblStatus.Text = txtSiteName.Text + " is not an SXA-enabled site";
                TabIndexValue = const_SiteInfo_Tab;
                AssignStepStatus(TabIndexValue);
                lblStatus.ForeColor = Color.Red;
                return false;
            }
            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red;
                TabIndexValue = const_Port_Tab;
                AssignStepStatus(TabIndexValue);
                return false; 
            }
            ToggleEnableControls(true);
            return true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!ValidateAll(false)) return;
            bool habitatExists = HabitatExists(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text));

            WriteFile(txtSiteName.Text + "_Uninstall_Script.ps1", habitatExists, true);
            WriteFile(txtSiteName.Text + "_Install_Script.ps1", habitatExists, false);
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
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + txtSiteNameSuffix.Text;
            txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + ".storefront.com";
            txtSXAInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtSqlDbPrefix.Text = txtSiteNamePrefix.Text;
            txtSitecoreCoreDbName.Text = txtSqlDbPrefix.Text + "_Core";
            txtCommerceDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "SharedEnvironments";
            txtCommerceGlobalDbName.Text = txtSiteNamePrefix.Text + commerceDbNameString + "Global";
            txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + siteNamePrefixString;
            txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + ".com";
            txtBizFxName.Text = bizFxSitenamePrefix + txtCommerceSvcPostFix.Text;
            txtUserName.Text = txtCommerceSvcPostFix.Text + "_User";
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtSXAInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + siteNamePrefixString + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtStorefrontIndexPrefix.Text = txtSiteName.Text;
        }

        private string StatusMessageBuilder(string msg)
        {
            string portString = string.Empty;
            if (PortInUse(Convert.ToInt32(txtCommerceShopsServicesPort.Value))) { portString = BuildPortString(txtCommerceShopsServicesPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceOpsSvcPort.Value))) { portString = BuildPortString(txtCommerceOpsSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtBizFxPort.Value))) { portString = BuildPortString(txtBizFxPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceAuthSvcPort.Value))) { portString = BuildPortString(txtCommerceAuthSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceMinionsSvcPort.Value))) { portString = BuildPortString(txtCommerceMinionsSvcPort.Value.ToString(), portString); }
            return  portString;
        }

        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + "identityserver" + txtSiteNameSuffix.Text;
            txtSXAInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text;
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtStorefrontIndexPrefix.Text = txtSiteName.Text;
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (!ValidateAll()) return;
            bool habitatExists = HabitatExists(CommonFunctions.BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSitecoreSqlUser.Text, txtSitecoreSqlPass.Text));

            WriteFile(txtSiteName.Text + "_UnInstall_Script.ps1", habitatExists, true);
            LaunchPSScript(txtSiteName.Text + "_UnInstall_Script.ps1");
            lblStatus.Text = "Uninstallation successfully launched through Powershell....";
            lblStatus.ForeColor = Color.DarkGreen;
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

        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            chkStepsList.SelectedIndex = tabIndex;
            tabDetails.SelectedIndex = tabIndex;
            chkStepsList.SetItemChecked(tabIndex, true);
            switch (tabIndex)
            {
                case 0:
                    lblStepInfo.Text= "Step 1 of 13: DB Connection";
                    break;
                case 1:
                    PopulateSettingsInfo();
                    lblStepInfo.Text = "Step 2 of 13: Site Info";
                    break;
                case 2:
                    lblStepInfo.Text = "Step 3 of 13: General Info";
                    break;
                case 3:
                    lblStepInfo.Text = "Step 4 of 13: Install Details"; 
                    break;
                case 4:
                    lblStepInfo.Text = "Step 5 of 13: Sitecore Details"; 
                    break;
                case 5:
                    lblStepInfo.Text = "Step 6 of 13: Solr Details"; 
                    break;
                case 6:
                    lblStepInfo.Text = "Step 7 of 13: Redis Details";
                    break;
                case 7:
                    lblStepInfo.Text = "Step 8 of 13: Sitecore DB Details";
                    break;
                case 8:
                    lblStepInfo.Text = "Step 9 of 13: Commerce Details";
                    break;
                case 9:
                    lblStepInfo.Text = "Step 10 of 13: Port Details";
                    break;
                case 10:
                    lblStepInfo.Text = "Step 11 of 13: Environment Details";
                    break;
                case 11:
                    lblStepInfo.Text = "Step 12 of 13: Win User Details";
                    break;
                case 12:
                    lblStepInfo.Text = "Step 13 of 13: Braintree Details";
                    break;
            }
        }

        public int TabIndexValue { get; set; }

        private void btnNext_Click(object sender, EventArgs e)
        {
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
            ToggleEnableControls(true);
            SetStatusMessage("Successfully established DB Connection", Color.DarkGreen);
        }


        private void ToggleEnableControls(bool enabled)
        {
            btnInstall.Enabled = enabled;
            btnUninstall.Enabled = enabled;
            btnGenerate.Enabled = enabled;
            btnDelete.Enabled = enabled;
        }


        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            Prerequisites prerequisites = new Prerequisites();
            prerequisites.ShowDialog();
        }

        private void btnAppSettings_Click(object sender, EventArgs e)
        {
            DBServerDetails dBServerDetails = new DBServerDetails
            {
                Server = txtSqlDbServer.Text,
                Username = txtSqlUser.Text,
                Password = txtSqlPass.Text
            };

            Settings settings = new Settings(dBServerDetails);
            settings.ShowDialog();
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
            if (!ValidateData(txtSiteName, "Site Name", const_SiteInfo_Tab)) return;
            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_General_Tab)) return;
            if (!ValidateData(txtSitecoreIdentityServerUrl, "Sitecore Id Server Url", const_General_Tab)) return;
           
            if (!ValidateData(txtSXAInstallDir, "Sitecore SXA Install Directory", const_Install_Details_Tab)) return;
            if (!ValidateData(txtxConnectInstallDir, "Sitecore xConnect Install Directory", const_Install_Details_Tab)) return;
            if (!ValidateData(txtCommerceInstallRoot, "Commerce Install Root", const_Install_Details_Tab)) return ;

            if (!ValidateData(txtSqlDbServer, "Sitecore Db Server", const_DBConn_Tab)) return;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return;

            if (!ValidateData(txtSolrUrl, "Solr Url", const_Solr_Tab)) return;
            if (!ValidateData(txtSolrRoot, "Solr Root Path", const_Solr_Tab)) return;
            if (!ValidateData(txtSolrService, "Solr Service Name", const_Solr_Tab)) return;

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
            file.WriteLine("\t[string]$SolrService = \"" + txtCommerceSvcPostFix.Text + "\",");
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

        private void btnSolr_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;
            btnSolr.Enabled = false;
            SetStatusMessage("Processing....", Color.Orange);
            if (!ValidSolrUrl(txtSolrUrl.Text))
            {
                TabIndexValue = const_Solr_Tab;
                AssignStepStatus(TabIndexValue);
                ToggleEnableControls(false);
                btnSolr.Enabled = true;
                SetStatusMessage("Wrong Solr Url....", Color.Red);
                return;
            }
           
            SolrInfo info = CommonFunctions.GetSolrInformation(txtSolrUrl.Text);
            txtSolrRoot.Text = info.solr_home.Replace("\\server\\solr",string.Empty);
            //txtSolrService.Text = info.system.name;
            if (info.lucene.SolrSpecVersion!="8.4.0")
            {
                TabIndexValue = const_Solr_Tab;
                AssignStepStatus(TabIndexValue);
                ToggleEnableControls(false);
                btnSolr.Enabled = true;
                SetStatusMessage("That Solr Url doesn't run on Solr 8.4.0....", Color.Red);
                return;
            }

            ToggleEnableControls(true);
            SetStatusMessage("All seems fine with Solr Url....", Color.DarkGreen);
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
            if(CheckAllValidations()) SetStatusMessage("Congrats! Passed all Validations!",Color.DarkGreen);
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
               SXAInstallDir = txtSXAInstallDir.Text,
               xConnectInstallDir = txtxConnectInstallDir.Text,
                        CommerceInstallRoot = txtCommerceInstallRoot.Text,
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
                 RedisPort =txtRedisPort.Value.ToString(),
                 CommerceServicesDBServer =txtCommerceServicesDBServer.Text,
                 CommerceDbName =txtCommerceDbName.Text,
                 CommerceGlobalDbName = txtCommerceGlobalDbName.Text,
                        CommerceOpsSvcPort = txtCommerceOpsSvcPort.Value.ToString(),
                 CommerceShopsServicesPort = txtCommerceShopsServicesPort.Value.ToString(),
                 CommerceAuthSvcPort =txtCommerceAuthSvcPort.Value.ToString(),
                 CommerceMinionsSvcPort =txtCommerceMinionsSvcPort.Value.ToString(),
                 CommerceSvcPostFix =txtCommerceSvcPostFix.Text,
                 CommerceServicesHostPostFix =txtCommerceServicesHostPostFix.Text,
                 BizFxName =txtBizFxName.Text,
                 BizFxPort =txtBizFxPort.Value.ToString(),
                 EnvironmentsPrefix =txtEnvironmentsPrefix.Text,
                DeploySampleData =chkDeploySampleData.Checked,
                 UserName =txtUserName.Text,
                 UserPassword =txtUserPassword.Text,
                 BraintreeMerchantId =txttxtBraintreeMerchantId.Text,
                 BraintreePublicKey =txtBraintreePublicKey.Text,
                 BraintreePrivateKey =txtBraintreePrivateKey.Text,
                 BraintreeEnvironment =txtBraintreeEnvironment.Text
            };
            ScriptPreview preview = new ScriptPreview(siteDetails: siteDetails);
            preview.ShowDialog();
        }
    }

}
