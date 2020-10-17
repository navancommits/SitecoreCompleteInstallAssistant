using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Management.Automation;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;

namespace SCIA
{
    /*# The root folder with WDP files.
    [string]$XCInstallRoot = "..",
    # The root folder of SIF.Sitecore.Commerce package.
    [string]$XCSIFInstallRoot = $PWD,
    # Specifies whether or not to bypass the installation of the default SXA Storefront. By default, the Sitecore XC installation script also deploys the SXA Storefront.
    [bool]$SkipInstallDefaultStorefront = $false,
    # Specifies whether or not to bypass the installation of the SXA Storefront packages.
    # If set to $true, $TasksToSkip parameter will be populated with the list of tasks to skip in order to bypass SXA Storefront packages installation.
    [bool]$SkipDeployStorefrontPackages = $false,

    # Path to the Master_SingleServer.json file provided in the SIF.Sitecore.Commerce package.
    [string]$Path = "$XCSIFInstallRoot\Configuration\Commerce\Master_SingleServer.json",
    # Path to the Commerce Solr schemas provided as part of the SIF.Sitecore.Commerce package.
    [string]$SolrSchemas = "$XCSIFInstallRoot\SolrSchemas",
    # Path to the SiteUtilityPages folder provided as part of the SIF.Sitecore.Commerce package.
    [string]$SiteUtilitiesSrc = "$XCSIFInstallRoot\SiteUtilityPages",
    # Path to the location where you downloaded the Microsoft.Web.XmlTransform.dll file.
    [string]$MergeToolFullPath = "$XCInstallRoot\MSBuild.Microsoft.VisualStudio.Web.targets*\tools\VSToolsPath\Web\Microsoft.Web.XmlTransform.dll",
    # Path to the Adventure Works Images.OnPrem SCWDP file
    [string]$AdventureWorksImagesWdpFullPath = "$XCInstallRoot\Adventure Works Images.OnPrem.scwdp.zip",
    # Path to the Sitecore Commerce Connect Core SCWDP file.
    [string]$CommerceConnectWdpFullPath = "$XCInstallRoot\Sitecore Commerce Connect Core*.scwdp.zip",
    # Path to the Sitecore Commerce Engine Connect OnPrem SCWDP file.
    [string]$CEConnectWdpFullPath = "$XCInstallRoot\Sitecore Commerce Engine Connect*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Accelerator SCWDP file.
    [string]$SXACommerceWdpFullPath = "$XCInstallRoot\Sitecore Commerce Experience Accelerator*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Accelerator Habitat Catalog SCWDP file.
    [string]$SXAStorefrontCatalogWdpFullPath = "$XCInstallRoot\Sitecore Commerce Experience Accelerator Habitat*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Accelerator Storefront SCWDP file.
    [string]$SXAStorefrontWdpFullPath = "$XCInstallRoot\Sitecore Commerce Experience Accelerator Storefront*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Accelerator Storefront Themes SCWDP file.
    [string]$SXAStorefrontThemeWdpFullPath = "$XCInstallRoot\Sitecore Commerce Experience Accelerator Storefront Themes*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Analytics Core SCWDP file.
    [string]$CommercexAnalyticsWdpFullPath = "$XCInstallRoot\Sitecore Commerce ExperienceAnalytics Core*.scwdp.zip",
    # Path to the Sitecore Commerce Experience Profile Core SCWDP file.
    [string]$CommercexProfilesWdpFullPath = "$XCInstallRoot\Sitecore Commerce ExperienceProfile Core*.scwdp.zip",
    # Path to the Sitecore Commerce Marketing Automation Core SCWDP file.
    [string]$CommerceMAWdpFullPath = "$XCInstallRoot\Sitecore Commerce Marketing Automation Core*.scwdp.zip",
    # Path to the Sitecore Commerce Marketing Automation for AutomationEngine zip file.
    [string]$CommerceMAForAutomationEngineZIPFullPath = "$XCInstallRoot\Sitecore Commerce Marketing Automation for AutomationEngine*.zip",
    # Path to the Sitecore Experience Accelerator zip file.
    [string]$SXAModuleZIPFullPath = "$XCInstallRoot\Sitecore Experience Accelerator*.zip",
    # Path to the Sitecore.PowerShell.Extensions zip file.
    [string]$PowerShellExtensionsModuleZIPFullPath = "$XCInstallRoot\Sitecore.PowerShell.Extensions*.zip",
    # Path to the Sitecore BizFx Server SCWDP file.
    [string]$BizFxPackage = "$XCInstallRoot\Sitecore.BizFx.OnPrem*scwdp.zip",
    # Path to the Commerce Engine Service SCWDP file.
    [string]$CommerceEngineWdpFullPath = "$XCInstallRoot\Sitecore.Commerce.Engine.OnPrem.Solr.*scwdp.zip",
    # Path to the Sitecore.Commerce.Habitat.Images.OnPrem SCWDP file.
    [string]$HabitatImagesWdpFullPath = "$XCInstallRoot\Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip",

    # The prefix that will be used on SOLR, Website and Database instances. The default value matches the Sitecore XP default.
    [string]$SiteNamePrefix = "XP0",
    # The name of the Sitecore site instance.
    [string]$SiteName = "$SiteNamePrefix.sc",
    # Identity Server site name.
    [string]$IdentityServerSiteName = "$SiteNamePrefix.IdentityServer",
    # The url of the Sitecore Identity server.
    [string]$SitecoreIdentityServerUrl = "https://$IdentityServerSiteName",
    # The Commerce Engine Connect Client Id for the Sitecore Identity Server
    [string]$CommerceEngineConnectClientId = "CommerceEngineConnect",
    # The Commerce Engine Connect Client Secret for the Sitecore Identity Server
    [string]$CommerceEngineConnectClientSecret = "",
    # The host header name for the Sitecore storefront site.
    [string]$SiteHostHeaderName = "sxa.storefront.com",

    # The path of the Sitecore XP site.
    [string]$InstallDir = "$($Env:SYSTEMDRIVE)\inetpub\wwwroot\$SiteName",
    # The path of the Sitecore XConnect site.
    [string]$XConnectInstallDir = "$($Env:SYSTEMDRIVE)\inetpub\wwwroot\$SiteNamePrefix.xconnect",
    # The path to the inetpub folder where Commerce is installed.
    [string]$CommerceInstallRoot = "$($Env:SYSTEMDRIVE)\inetpub\wwwroot\",

    # The prefix for Sitecore core and master databases.
    [string]$SqlDbPrefix = $SiteNamePrefix,
    # The location of the database server where Sitecore XP databases are hosted. In case of named SQL instance, use "SQLServerName\\SQLInstanceName"
    [string]$SitecoreDbServer = $($Env:COMPUTERNAME),
    # The name of the Sitecore core database.
    [string]$SitecoreCoreDbName = "$($SqlDbPrefix)_Core",
    # A SQL user with sysadmin privileges.
    [string]$SqlUser = "sa",
    # The password for $SQLAdminUser.
    [string]$SqlPass = "12345",

    # The name of the Sitecore domain.
    [string]$SitecoreDomain = "sitecore",
    # The name of the Sitecore user account.
    [string]$SitecoreUsername = "admin",
    # The password for the $SitecoreUsername.
    [string]$SitecoreUserPassword = "b",

    # The prefix for the Search index. Using the SiteName value for the prefix is recommended.
    [string]$SearchIndexPrefix = "",
    # The URL of the Solr Server.
    [string]$SolrUrl = "https://localhost:8995/solr",
    # The folder that Solr has been installed to.
    [string]$SolrRoot = "$($Env:SYSTEMDRIVE)\solr-8.4.0",
    # The name of the Solr Service.
    [string]$SolrService = "solr-8.4.0",
    # The prefix for the Storefront index. The default value is the SiteNamePrefix.
    [string]$StorefrontIndexPrefix = $SiteNamePrefix,

    # The host name where Redis is hosted.
    [string]$RedisHost = "localhost",
    # The port number on which Redis is running.
    [string]$RedisPort = "6379",
    # The name of the Redis instance.
    [string]$RedisInstanceName = "Redis",
    # The path to the redis-cli executable.
    [string]$RedisCliPath = "$($Env:SYSTEMDRIVE)\Program Files\Redis\redis-cli.exe",

    # The location of the database server where Commerce databases should be deployed. In case of named SQL instance, use "SQLServerName\\SQLInstanceName"
    [string]$CommerceServicesDbServer = $($Env:COMPUTERNAME),
    # The name of the shared database for the Commerce Services.
    [string]$CommerceServicesDbName = "SitecoreCommerce_SharedEnvironments",
    # The name of the global database for the Commerce Services.
    [string]$CommerceServicesGlobalDbName = "SitecoreCommerce_Global",
    # The port for the Commerce Ops Service.
    [string]$CommerceOpsServicesPort = "5015",
    # The port for the Commerce Shops Service
    [string]$CommerceShopsServicesPort = "5005",
    # The port for the Commerce Authoring Service.
    [string]$CommerceAuthoringServicesPort = "5000",
    # The port for the Commerce Minions Service.
    [string]$CommerceMinionsServicesPort = "5010",
    # The postfix appended to Commerce services folders names and sitenames.
    # The postfix allows you to host more than one Commerce installment on one server.
    [string]$CommerceServicesPostfix = "Sc",
    # The postfix used as the root domain name (two-levels) to append as the hostname for Commerce services.
    # By default, all Commerce services are configured as sub-domains of the domain identified by the postfix.
    # Postfix validation enforces the following rules:
    # 1. The first level (TopDomainName) must be 2-7 characters in length and can contain alphabetical characters (a-z, A-Z) only. Numeric and special characters are not valid.
    # 2. The second level (DomainName) can contain alpha-numeric characters (a-z, A-Z,and 0-9) and can include one hyphen (-) character.
    # Special characters (wildcard (*)), for example, are not valid.
    [string]$CommerceServicesHostPostfix = "sc.com",

    # The name of the Sitecore XC Business Tools server.
    [string]$BizFxSiteName = "SitecoreBizFx",
    # The port of the Sitecore XC Business Tools server.
    [string]$BizFxPort = "4200",

    # The prefix used in the EnvironmentName setting in the config.json file for each Commerce Engine role.
    [string]$EnvironmentsPrefix = "Habitat",
    # The list of Commerce environment names. By default, the script deploys the AdventureWorks and the Habitat environments.
    [array]$Environments = @("AdventureWorksAuthoring", "HabitatAuthoring"),
    # Commerce environments GUIDs used to clean existing Redis cache during deployment. Default parameter values correspond to the default Commerce environment GUIDS.
    [array]$EnvironmentsGuids = @("78a1ea611f3742a7ac899a3f46d60ca5", "40e77b7b4be94186b53b5bfd89a6a83b"),
    # The environments running the minions service. (This is required, for example, for running indexing minions).
    [array]$MinionEnvironments = @("AdventureWorksMinions", "HabitatMinions"),
    # whether to deploy sample data for each environment.
    [bool]$DeploySampleData = $true,

    # The domain of the local account used for the various application pools created as part of the deployment.
    [string]$UserDomain = $Env:COMPUTERNAME,
    # The user name for a local account to be set up for the various application pools that are created as part of the deployment.
    [string]$UserName = "CSFndRuntimeUser",
    # The password for the $UserName.
    [string]$UserPassword = "q5Y8tA3FRMZf3xKN!",

    # The Braintree Merchant Id.
    [string]$BraintreeMerchantId = "",
    # The Braintree Public Key.
    [string]$BraintreePublicKey = "",
    # The Braintree Private Key.
    [string]$BraintreePrivateKey = "",
    # The Braintree Environment.
    [string]$BraintreeEnvironment = "",

    # List of comma-separated task names to skip during Sitecore XC deployment.
    [string]$TasksToSkip = ""
     *
     *
     */

    public partial class SitecoreCommerceInstaller : Form
    {
        string SystemDrive = "C:";
        string StatusMessage = string.Empty;
        string DefaultStatusMessage = "Happy Sitecoring!";

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        
        public SitecoreCommerceInstaller()
        {
            InitializeComponent();
            SystemDrive = Path.GetPathRoot(Environment.GetFolderPath(Environment.SpecialFolder.System));

            txtSiteName.Text = txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + "identityserver" + txtSiteNameSuffix.Text;
            txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + ".storefront.com";
            txtSXAInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text;
            txtSqlDbPrefix.Text = txtSiteNamePrefix.Text;
            txtSitecoreCoreDbName.Text = txtSqlDbPrefix.Text + "_Core";
            txtCommerceDbName.Text = txtSiteNamePrefix.Text + "_SitecoreCommerce_SharedEnvironments";
            txtCommerceGlobalDbName.Text = txtSiteNamePrefix.Text + "_SitecoreCommerce_Global";
            txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + "sc";
            txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + ".com";
            txtBizFxName.Text = "SitecoreBizFx-" + txtCommerceSvcPostFix.Text;
            txtUserName.Text = txtCommerceSvcPostFix.Text + "_User";
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtStorefrontIndexPrefix.Text = txtSiteName.Text;
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
        }

        void LaunchPSScript(string scriptname)
        {
            var script = @".\" + scriptname;
            var startInfo = new ProcessStartInfo()
            {
                FileName = "powershell.exe",
                Arguments = $"-NoProfile -noexit -ExecutionPolicy unrestricted \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }

        void WriteFile(string path, bool habitatflag, bool uninstallscript)
        {
            //File.Create(path);
            // Example #4: Append new text to an existing file.
            // The using statement automatically flushes AND CLOSES the stream and calls
            // IDisposable.Dispose on the stream object.
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
            file.WriteLine("\t[string]$SqlUser = \"" + txtSqlUser.Text + "\",");
            file.WriteLine("\t# The password for $SQLAdminUser.");
            file.WriteLine("\t[string]$SqlPass = \"" + txtSqlPass.Text + "\",");
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
            if (habitatflag)
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

        private void  ResetIIS()
        {
            Process elevated = new Process();
            elevated.StartInfo.Verb = "runas";
            elevated.StartInfo.FileName = "iisreset.exe";
            elevated.Start();
        }

        
        private void btnInstall_Click(object sender, EventArgs e)
        {
            string portString = string.Empty;
            if (!ValidateAll()) return;
            if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! provide unique port numbers...."; return; }
            if (!Directory.Exists(txtSXAInstallDir.Text))
            {
                lblStatus.Text = "Missing Directory! Install SXA-site at - " + txtSXAInstallDir.Text;
                lblStatus.ForeColor = Color.Red;
                return;
            }
            if (!Directory.Exists(txtSXAInstallDir.Text + "\\App_Config\\Modules\\SXA"))
            {
                lblStatus.Text = txtSiteName.Text + " is not an SXA-enabled site";
                lblStatus.ForeColor = Color.Red;
                return;
            }
            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red; }
            bool habitatExists = HabitatExists(BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSqlUser.Text, txtSqlPass.Text));

            WriteFile(txtSiteName.Text + "_Install_Script.ps1", habitatExists, false);
            //ResetIIS();
            LaunchPSScript(txtSiteName.Text + "_Install_Script.ps1");
            lblStatus.Text = "Installation successfully launched through Powershell....";
            lblStatus.ForeColor = Color.DarkGreen;             
        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {
            txtSXAInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
            txtSearchIndexPrefix.Text = txtSiteName.Text;
        }

        private bool ValidateAll()
        {
            if (!ValidateData(txtSiteName, "Site Name",0)) return false;
            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name",0)) return false;
            if (!ValidateData(txtSitecoreIdentityServerUrl, "Sitecore Id Server Url",0)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientId, "Sitecore Commerce Connect Client Id",0)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientSecret, "Sitecore Commerce Connect Client Secret",0)) return false;
            if (!ValidateData(txtSiteHostHeaderName, "Site Host Header Name",0)) return false;

            if (!ValidateData(txtSXAInstallDir, "Sitecore SXA Install Directory",1)) return false;
            if (!ValidateData(txtxConnectInstallDir, "Sitecore xConnect Install Directory",1)) return false;
            if (!ValidateData(txtCommerceInstallRoot, "Commerce Install Root",1)) return false;

            if (!ValidateData(txtSqlDbPrefix, "Sql Db Prefix",2)) return false;
            if (!ValidateData(txtSitecoreDbServer, "Sitecore Db Server",2)) return false;
            if (!ValidateData(txtSitecoreCoreDbName, "Sitecore Core Db Name",2)) return false;
            if (!ValidateData(txtSqlUser, "Sql User",2)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password",2)) return false;

            if (!ValidateData(txtSitecoreDomain, "Sitecore Domain",3)) return false;
            if (!ValidateData(txtSitecoreUsername, "Sitecore Username",3)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password",3)) return false;

            if (!ValidateData(txtSearchIndexPrefix, "Search Index Prefix",4)) return false;
            if (!ValidateData(txtSolrUrl, "Solr Url",4)) return false;
            if (!ValidateData(txtSolrRoot, "Solr Root Path",4)) return false;
            if (!ValidateData(txtSolrService, "Solr Service Name",4)) return false;
            if (!ValidateData(txtStorefrontIndexPrefix, "Storefront Index Prefix",4)) return false;

            if (!ValidateData(txtRedisHost, "Redis Host",5)) return false;
            if (!ValidatePortNumber(txtRedisPort, "Redis Port",5)) return false;

            if (!ValidateData(txtCommerceServicesDBServer, "Commerce DB Server",6)) return false;
            if (!ValidateData(txtCommerceDbName, "Commerce DB Name",6)) return false;
            if (!ValidateData(txtCommerceGlobalDbName, "Sitecore Commerce Global Db Name",6)) return false;
            if (!ValidateData(txtCommerceSvcPostFix, "Sitecore Commerce Svc Post Fix",6)) return false;
            if (!ValidateData(txtCommerceServicesHostPostFix, "Sitecore Commerce Svc Host Post Fix",6)) return false;

            if (!ValidatePortNumber(txtCommerceOpsSvcPort, "Commerce Ops Svc Port",7)) return false;
            if (!ValidatePortNumber(txtCommerceShopsServicesPort, "Commerce Shops Svc Port",7)) return false;
            if (!ValidatePortNumber(txtCommerceAuthSvcPort, "Commerce Auth Svc Port",7)) return false;
            if (!ValidatePortNumber(txtCommerceMinionsSvcPort, "Commerce Minions Svc Port",7)) return false;
            if (!ValidatePortNumber(txtBizFxPort, "BizFx Port Number",7)) return false;
            if (!IsPortNotinUse(txtCommerceOpsSvcPort,7)) return false;
            if (!IsPortNotinUse(txtCommerceShopsServicesPort,7)) return false;
            if (!IsPortNotinUse(txtCommerceAuthSvcPort,7)) return false;
            if (!IsPortNotinUse(txtCommerceMinionsSvcPort,7)) return false;
            if (!IsPortNotinUse(txtBizFxPort,7)) return false;
            if (!ValidateData(txtBizFxName, "BizFx Name",7)) return false;

            if (!ValidateData(txtUserDomain, "Win User Domain",9)) return false;
            if (!ValidateData(txtUserName, "Win User Name",9)) return false;
            if (!ValidateData(txtUserPassword, "Win User Password",9)) return false;

            if (!ValidateData(txttxtBraintreeMerchantId, "Braintree Merchant Id", 10)) return false;
            if (!ValidateData(txtBraintreePublicKey, "Braintree Public Key", 10)) return false;
            if (!ValidateData(txtBraintreePrivateKey, "Braintree Private Key", 10)) return false;
            if (!ValidateData(txtBraintreeEnvironment, "Braintree Environment", 10)) return false;
            
            return true;
        }

        private bool ValidateData(TextBox control, string controlString,int tabIndex)
        {
            bool Valid = true;
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                lblStatus.Text = controlString + " needed... ";
                lblStatus.ForeColor = Color.Red;
                tabDetails.SelectedIndex = tabIndex;
                Valid = false;
            }
            return Valid;
        }

        private bool ValidatePortNumber(NumericUpDown control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (control.Value<1024)
            {
                lblStatus.Text = controlString + " must be between 1024 to 49151... ";
                lblStatus.ForeColor = Color.Red;
                control.Focus();
                tabDetails.SelectedIndex = tabIndex;
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

        private string BuildConnectionString(string datasource, string dbname, string uid, string pwd)
        {
            
            return "Data Source=" + datasource + "; Initial Catalog=" + dbname + "; User ID=" + uid + "; Password=" + pwd;
            
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string portString = string.Empty;
            if (!ValidateAll()) return;
            if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! Provide unique port numbers...."; return; }
            if (!Directory.Exists(txtSXAInstallDir.Text))
            {
                lblStatus.Text = "Missing Directory! Install SXA-site at - " + txtSXAInstallDir.Text;
                lblStatus.ForeColor = Color.Red;
                return;
            }
            if (!Directory.Exists(txtSXAInstallDir.Text + "\\App_Config\\Modules\\SXA"))
            {
                lblStatus.Text = txtSiteName.Text + " is not an SXA-enabled site";
                lblStatus.ForeColor = Color.Red;
                return;
            }
            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red; }
            bool habitatExists = HabitatExists(BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSqlUser.Text, txtSqlPass.Text));

            WriteFile(txtSiteName.Text + "_Uninstall_Script.ps1", habitatExists, true);
            WriteFile(txtSiteName.Text + "_Install_Script.ps1", habitatExists, false);
            lblStatus.Text = "Scripts generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;

        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text= txtSiteNamePrefix.Text + "identityserver" + txtSiteNameSuffix.Text;
            txtSiteHostHeaderName.Text = txtSiteNamePrefix.Text + ".storefront.com";
            txtSXAInstallDir.Text= txtSXAInstallDir.Text + "\\" + txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = txtxConnectInstallDir.Text + "\\" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text;
            txtSqlDbPrefix.Text = txtSiteNamePrefix.Text;
            txtSitecoreCoreDbName.Text= txtSqlDbPrefix.Text + "_Core";
            txtCommerceDbName.Text = txtSiteNamePrefix.Text + "_SitecoreCommerce_SharedEnvironments";
            txtCommerceGlobalDbName.Text= txtSiteNamePrefix.Text + "_SitecoreCommerce_Global";
            txtCommerceSvcPostFix.Text = txtSiteNamePrefix.Text + "sc";
            txtCommerceServicesHostPostFix.Text = txtCommerceSvcPostFix.Text + ".com";
            txtBizFxName.Text = "SitecoreBizFx-" + txtCommerceSvcPostFix.Text;
            txtUserName.Text = txtCommerceSvcPostFix.Text + "_User";
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtSXAInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "sc" + txtSiteNameSuffix.Text;
            txtxConnectInstallDir.Text = "c:\\inetpub\\wwwroot\\" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text;
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
            string portString = string.Empty;
            if (!ValidateAll()) return;
            if (IsPortDuplicated(AddPortstoArray())) { lblStatus.Text = "Duplicate port numbers detected! Provide unique port numbers...."; return; }
            if (!Directory.Exists(txtSXAInstallDir.Text))
            {
                lblStatus.Text = "Missing Directory! Install SXA-site at - " + txtSXAInstallDir.Text;
                lblStatus.ForeColor = Color.Red;
                return;
            }
            if (!Directory.Exists(txtSXAInstallDir.Text + "\\App_Config\\Modules\\SXA"))
            {
                lblStatus.Text = txtSiteName.Text + " is not an SXA-enabled site";
                lblStatus.ForeColor = Color.Red;
                return;
            }
            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red; }
            bool habitatExists = HabitatExists(BuildConnectionString(txtSitecoreDbServer.Text, txtSqlDbPrefix.Text + "_master", txtSqlUser.Text, txtSqlPass.Text));

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
                tabDetails.SelectedIndex = tabIndex;
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

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            Prerequisites prereq = new Prerequisites();
            prereq.ShowDialog();
        }

        private void btnProceed_Click(object sender, EventArgs e)
        {
            if (!ValidateData(txtSiteName, "Site Name", 0)) return;
            tabDetails.SelectedTab = tabDetails.TabPages[1];
        }

        private void roundButton3_Click(object sender, EventArgs e)
        {

        }

        private void btnGenerate_MouseHover(object sender, EventArgs e)
        {
            
        }
    }

    public class RoundButton : Button
    {
        protected override void OnPaint(System.Windows.Forms.PaintEventArgs e)
        {
            GraphicsPath grPath = new GraphicsPath();
            grPath.AddEllipse(0, 0, ClientSize.Width, ClientSize.Height);
            this.Region = new System.Drawing.Region(grPath);
            base.OnPaint(e);
        }
    }

}
