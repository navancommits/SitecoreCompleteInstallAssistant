using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SCIA
{
    public partial class ScriptPreview : Form
    {
        SiteDetails sitedetails;
       
        public ScriptPreview(SiteDetails siteDetails)
        {

            InitializeComponent();
            sitedetails = siteDetails;
            this.Width = 775;
            this.Height = 900;
            WritePreviewFile(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Install_Script_Preview.ps1", sitedetails.HabitatExists, false);
            string text = System.IO.File.ReadAllText(@".\" + SCIASettings.FilePrefixAppString  + sitedetails.SiteName + "_Install_Script_Preview.ps1");
            lblPreview.Text = text;
        }


        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void rbInstallScript_CheckedChanged(object sender, EventArgs e)
        {
            switch (Version.SitecoreVersion)
            {
                case "9.3":
                    Write93PreviewFile(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Install_Script_Preview.ps1", sitedetails.HabitatExists, false);
                    break;
                case "10.0":
                case "10.0.1":
                case "10.1.0":
                case "10.1.1":
                case "10.2.0":
                case "10.3.0":
                    WritePreviewFile(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Install_Script_Preview.ps1", sitedetails.HabitatExists, false);
                    break;
                default:
                    break;
            }

            string text = System.IO.File.ReadAllText(@".\" + SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Install_Script_Preview.ps1");
            SetStatusMessage("Happy Sitecoring!", Color.DarkGreen);
            lblPreview.Text = text;
        }

        void Write93PreviewFile(string path, bool habitatflag, bool uninstallscript)
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
                "\t[string]$MergeToolFullPath = \"$XCInstallRoot\\MSBuild\\tools\\VSToolsPath\\Web\\Microsoft.Web.XmlTransform.dll\",");
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
                "\t[string]$SiteNamePrefix = \"" + sitedetails.SiteNamePrefix + "\",");

            file.WriteLine("\t# The name of the Sitecore site instance.");
            file.WriteLine("\t[string]$SiteName = \"" + sitedetails.SiteName + "\",");
            file.WriteLine("\t# Identity Server site name.");
            file.WriteLine("\t[string]$IdentityServerSiteName = \"" + sitedetails.IDServerSiteName + "\",");
            file.WriteLine("\t# The url of the Sitecore Identity server.");
            file.WriteLine("\t[string]$SitecoreIdentityServerUrl = \"" + sitedetails.SitecoreIdentityServerUrl + "\",");
            file.WriteLine("\t# The Commerce Engine Connect Client Id for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientId = \"" + sitedetails.CommerceEngineConnectClientId + "\",");

            file.WriteLine("\t# The Commerce Engine Connect Client Secret for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientSecret = \"" + sitedetails.CommerceEngineConnectClientSecret + "\",");
            file.WriteLine("\t# The host header name for the Sitecore storefront site.");
            file.WriteLine("\t[string]$SiteHostHeaderName = \"" + sitedetails.SiteHostHeaderName + "\",");
            file.WriteLine();
            file.WriteLine("\t# The path of the Sitecore XP site.");
            file.WriteLine("\t[string]$InstallDir = \"" + sitedetails.SXAInstallDir + "\",");
            file.WriteLine("\t# The path of the Sitecore XConnect site.");
            file.WriteLine("\t[string]$XConnectInstallDir = \"" + sitedetails.xConnectInstallDir + "\",");
            file.WriteLine("\t# The path to the inetpub folder where Commerce is installed.");
            file.WriteLine("\t[string]$CommerceInstallRoot = \"" + sitedetails.CommerceInstallRoot + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for Sitecore core and master databases.");
            file.WriteLine("\t[string]$SqlDbPrefix = $SiteNamePrefix,");
            file.WriteLine("\t# The location of the database server where Sitecore XP databases are hosted. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\".");

            file.WriteLine("\t[string]$SitecoreDbServer = \"" + sitedetails.SitecoreDbServer + "\",");
            file.WriteLine("\t# The name of the Sitecore core database.");
            file.WriteLine("\t[string]$SitecoreCoreDbName = \"$($SqlDbPrefix)_Core\",");
            file.WriteLine("\t# A SQL user with sysadmin privileges.");
            file.WriteLine("\t[string]$SqlUser = \"" + sitedetails.SitecoreSqlUser + "\",");
            file.WriteLine("\t# The password for $SQLAdminUser.");
            file.WriteLine("\t[string]$SqlPass = \"" + sitedetails.SitecoreSqlPass + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore domain.");
            file.WriteLine("\t[string]$SitecoreDomain = \"" + sitedetails.SitecoreDomain + "\",");
            file.WriteLine("\t# The name of the Sitecore user account.");
            file.WriteLine("\t[string]$SitecoreUsername = \"" + sitedetails.SitecoreUsername + "\",");
            file.WriteLine("\t# The password for the $SitecoreUsername.");
            file.WriteLine("\t[string]$SitecoreUserPassword = \"" + sitedetails.SitecoreUserPassword + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for the Search index. Using the SiteName value for the prefix is recommended.");
            file.WriteLine("\t[string]$SearchIndexPrefix = \"" + sitedetails.SearchIndexPrefix + "\",");
            file.WriteLine("\t# The URL of the Solr Server.");
            file.WriteLine("\t[string]$SolrUrl =  \"" + sitedetails.SolrUrl + "\",");
            file.WriteLine("\t# The folder that Solr has been installed to.");
            file.WriteLine("\t[string]$SolrRoot =  \"" + sitedetails.SolrRoot + "\",");
            file.WriteLine("\t# The name of the Solr Service.");
            file.WriteLine("\t[string]$SolrService =  \"" + sitedetails.SolrService + "\",");
            file.WriteLine("\t# The prefix for the Storefront index. The default value is the SiteNamePrefix.");
            file.WriteLine("\t[string]$StorefrontIndexPrefix = $SiteNamePrefix,");
            file.WriteLine();
            file.WriteLine("\t# The URL of the Redis service.");
            file.WriteLine("\t[string]$RedisConfiguration =  \"" + sitedetails.RedisHost + "\",");
            file.WriteLine("\t# The name of the Redis instance.");
            file.WriteLine("\t[string]$RedisInstanceName = \"Redis\",");
            file.WriteLine("\t# The path to the Redis installation.");
            //[string]$RedisInstallationPath = "$($Env:SYSTEMDRIVE)\Program Files\Redis",
            file.WriteLine("\t[string]$RedisInstallationPath = \"$($Env:SYSTEMDRIVE)\\Program Files\\Redis\",");
            file.WriteLine();
            file.WriteLine("\t# The location of the database server where Commerce databases should be deployed. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\"");
            file.WriteLine("\t[string]$CommerceServicesDbServer = \"" + sitedetails.CommerceServicesDBServer + "\",");
            file.WriteLine("\t# The name of the shared database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesDbName = \"" + sitedetails.CommerceDbName + "\",");
            file.WriteLine("\t# The name of the global database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesGlobalDbName =  \"" + sitedetails.CommerceGlobalDbName + "\",");
            file.WriteLine("\t# The port for the Commerce Ops Service.");
            file.WriteLine("\t[string]$CommerceOpsServicesPort = \"" + sitedetails.CommerceOpsSvcPort + "\",");
            file.WriteLine("\t# The port for the Commerce Shops Service");
            file.WriteLine("\t[string]$CommerceShopsServicesPort = \"" + sitedetails.CommerceShopsServicesPort + "\",");
            file.WriteLine("\t# The port for the Commerce Authoring Service.");
            file.WriteLine("\t[string]$CommerceAuthoringServicesPort = \"" + sitedetails.CommerceAuthSvcPort + "\",");
            file.WriteLine("\t# The port for the Commerce Minions Service.");
            file.WriteLine("\t[string]$CommerceMinionsServicesPort = \"" + sitedetails.CommerceMinionsSvcPort + "\",");
            file.WriteLine("\t# The postfix appended to Commerce services folders names and sitenames.");
            file.WriteLine("\t# The postfix allows you to host more than one Commerce installment on one server.");
            file.WriteLine("\t[string]$CommerceServicesPostfix = \"" + sitedetails.CommerceSvcPostFix + "\",");
            file.WriteLine("\t# The postfix used as the root domain name (two-levels) to append as the hostname for Commerce services.");
            file.WriteLine("\t# By default, all Commerce services are configured as sub-domains of the domain identified by the postfix.");
            file.WriteLine("\t# Postfix validation enforces the following rules:");
            file.WriteLine("\t# 1. The first level (TopDomainName) must be 2-7 characters in length and can contain alphabetical characters (a-z, A-Z) only. Numeric and special characters are not valid.");
            file.WriteLine("\t# 2. The second level (DomainName) can contain alpha-numeric characters (a-z, A-Z,and 0-9) and can include one hyphen (-) character.");
            file.WriteLine("\t# Special characters (wildcard (*)), for example, are not valid.");
            file.WriteLine("\t[string]$CommerceServicesHostPostfix = \"" + sitedetails.CommerceServicesHostPostFix + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxSiteName = \"" + sitedetails.BizFxName + "\",");
            file.WriteLine("\t# The port of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxPort = \"" + sitedetails.BizFxPort + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix used in the EnvironmentName setting in the config.json file for each Commerce Engine role.");
            file.WriteLine("\t[string]$EnvironmentsPrefix = \"Habitat\",");
            file.WriteLine("\t# The list of Commerce environment names. By default, the script deploys the AdventureWorks and the Habitat environments.");
            file.WriteLine("\t[array]$Environments = @(\"AdventureWorksAuthoring\", \"HabitatAuthoring\"),");

            file.WriteLine("\t# Commerce environments GUIDs used to clean existing Redis cache during deployment. Default parameter values correspond to the default Commerce environment GUIDS.");
            file.WriteLine("\t[array]$EnvironmentsGuids = @(\"78a1ea611f3742a7ac899a3f46d60ca5\", \"40e77b7b4be94186b53b5bfd89a6a83b\"),");
            file.WriteLine("\t# The environments running the minions service. (This is required, for example, for running indexing minions).");
            file.WriteLine("\t[array]$MinionEnvironments = @(\"AdventureWorksMinions\", \"HabitatMinions\"),");
            file.WriteLine();
            file.WriteLine("\t# The domain of the local account used for the various application pools created as part of the deployment.");
            file.WriteLine("\t[string]$UserDomain = $Env:COMPUTERNAME,");
            file.WriteLine("\t# The user name for a local account to be set up for the various application pools that are created as part of the deployment.");
            file.WriteLine("\t[string]$UserName = \"" + sitedetails.UserName + "\",");
            file.WriteLine("\t# The password for the $UserName.");
            file.WriteLine("\t[string]$UserPassword = \"" + sitedetails.UserPassword + "\",");
            file.WriteLine();
            file.WriteLine("\t# The Braintree Merchant Id.");
            file.WriteLine("\t[string]$BraintreeMerchantId = \"" + sitedetails.BraintreeMerchantId + "\",");
            file.WriteLine("\t# The Braintree Public Key.");
            file.WriteLine("\t[string]$BraintreePublicKey = \"" + sitedetails.BraintreePublicKey + "\",");
            file.WriteLine("\t# The Braintree Private Key.");
            file.WriteLine("\t[string]$BraintreePrivateKey = \"" + sitedetails.BraintreePrivateKey + "\",");
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
            file.WriteLine("[string[]] $Skip = @()");
            file.WriteLine("if (-not ([string]::IsNullOrWhiteSpace($TasksToSkip))) {");
            file.WriteLine("\t$TasksToSkip.Split(',') | ForEach-Object { $Skip += $_.Trim() }");
            file.WriteLine("}");
            file.WriteLine("if ($SkipDeployStorefrontPackages) {");
            file.WriteLine("\t\"Module-PowershellExtensions_CheckPaths\",");
            file.WriteLine("\t\"Module-PowershellExtensions_InstallModule\",");
            file.WriteLine("\t\"Module-SXAFramework_CheckPaths\",");
            file.WriteLine("\t\"Module-SXAFramework_InstallModule\",");
            file.WriteLine("\t\"Publish-Extensions_PublishToWeb\",");
            file.WriteLine("\t\"SXAStorefrontWdpsInstall_InstallCXAWDP\",");
            file.WriteLine("\t\"SXAStorefrontWdpsInstall_InstallSXAStorefrontWDP\",");
            file.WriteLine("\t\"SXAStorefrontWdpsInstall_InstallStorefrontThemesWDP\",");
            file.WriteLine("\t\"SXAStorefrontWdpsInstall_InstallStorefrontCatalogWDP\",");
            file.WriteLine("\t\"SXAStorefrontPostInstallationSteps_MergeWebConfig\" | ForEach-Object { $Skip += $_.Trim() }");
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
            file.WriteLine("\tRedisConfiguration                       = $RedisConfiguration");
            file.WriteLine("\tRedisInstanceName                        = $RedisInstanceName");
            file.WriteLine("\tRedisInstallationPath                    = Resolve-ItemPath -Path $RedisInstallationPath");
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
            file.WriteLine("\tCommerceEngineConnectClientId            = $CommerceEngineConnectClientId");
            file.WriteLine("\tCommerceEngineConnectClientSecret        = $CommerceEngineConnectClientSecret");
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

        void WritePreviewFile(string path, bool habitatflag, bool uninstallscript)
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
            Color foreColor = new Color();

            file.WriteLine(
                "\t[string]$SiteNamePrefix = \"" + sitedetails.SiteNamePrefix + "\",", foreColor.R);

            file.WriteLine("\t# The name of the Sitecore site instance.");
            file.WriteLine("\t[string]$SiteName = \"" + sitedetails.SiteName + "\",");
            file.WriteLine("\t# Identity Server site name.");
            file.WriteLine("\t[string]$IdentityServerSiteName = \"" + sitedetails.IDServerSiteName + "\",");
            file.WriteLine("\t# The url of the Sitecore Identity server.");
            file.WriteLine("\t[string]$SitecoreIdentityServerUrl = \"" + sitedetails.SitecoreIdentityServerUrl + "\",");
            file.WriteLine("\t# The Commerce Engine Connect Client Id for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientId = \"" + sitedetails.CommerceEngineConnectClientId + "\",");

            file.WriteLine("\t# The Commerce Engine Connect Client Secret for the Sitecore Identity Server");
            file.WriteLine("\t[string]$CommerceEngineConnectClientSecret = \"" + sitedetails.CommerceEngineConnectClientSecret + "\",");
            file.WriteLine("\t# The host header name for the Sitecore storefront site.");
            file.WriteLine("\t[string]$SiteHostHeaderName = \"" + sitedetails.SiteHostHeaderName + "\",");
            file.WriteLine();
            file.WriteLine("\t# The path of the Sitecore XP site.");
            file.WriteLine("\t[string]$InstallDir = \"" + sitedetails.SXAInstallDir + "\",");
            file.WriteLine("\t# The path of the Sitecore XConnect site.");
            file.WriteLine("\t[string]$XConnectInstallDir = \"" + sitedetails.xConnectInstallDir + "\",");
            file.WriteLine("\t# The path to the inetpub folder where Commerce is installed.");
            file.WriteLine("\t[string]$CommerceInstallRoot = \"" + sitedetails.CommerceInstallRoot + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for Sitecore core and master databases.");
            file.WriteLine("\t[string]$SqlDbPrefix = $SiteNamePrefix,");
            file.WriteLine("\t# The location of the database server where Sitecore XP databases are hosted. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\".");

            file.WriteLine("\t[string]$SitecoreDbServer = \"" + sitedetails.SitecoreDbServer + "\",");
            file.WriteLine("\t# The name of the Sitecore core database.");
            file.WriteLine("\t[string]$SitecoreCoreDbName = \"$($SqlDbPrefix)_Core\",");
            file.WriteLine("\t# A SQL user with sysadmin privileges.");
            file.WriteLine("\t[string]$SqlUser = \"" + sitedetails.SitecoreSqlUser + "\",");
            file.WriteLine("\t# The password for $SQLAdminUser.");
            file.WriteLine("\t[string]$SqlPass = \"" + sitedetails.SitecoreSqlPass + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore domain.");
            file.WriteLine("\t[string]$SitecoreDomain = \"" + sitedetails.SitecoreDomain + "\",");
            file.WriteLine("\t# The name of the Sitecore user account.");
            file.WriteLine("\t[string]$SitecoreUsername = \"" + sitedetails.SitecoreUsername + "\",");
            file.WriteLine("\t# The password for the $SitecoreUsername.");
            file.WriteLine("\t[string]$SitecoreUserPassword = \"" + sitedetails.SitecoreUserPassword + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix for the Search index. Using the SiteName value for the prefix is recommended.");
            file.WriteLine("\t[string]$SearchIndexPrefix = \"" + sitedetails.SearchIndexPrefix + "\",");
            file.WriteLine("\t# The URL of the Solr Server.");
            file.WriteLine("\t[string]$SolrUrl =  \"" + sitedetails.SolrUrl + "\",");
            file.WriteLine("\t# The folder that Solr has been installed to.");
            file.WriteLine("\t[string]$SolrRoot =  \"" + sitedetails.SolrRoot + "\",");
            file.WriteLine("\t# The name of the Solr Service.");
            file.WriteLine("\t[string]$SolrService =  \"" + sitedetails.SolrService + "\",");
            file.WriteLine("\t# The prefix for the Storefront index. The default value is the SiteNamePrefix.");
            file.WriteLine("\t[string]$StorefrontIndexPrefix = $SiteNamePrefix,");
            file.WriteLine();
            file.WriteLine("\t# The host name where Redis is hosted.");
            file.WriteLine("\t[string]$RedisHost =  \"" + sitedetails.RedisHost + "\",");
            file.WriteLine("\t# The port number on which Redis is running.");
            file.WriteLine("\t[string]$RedisPort = \"" + sitedetails.RedisPort + "\",");
            file.WriteLine("\t# The name of the Redis instance.");
            file.WriteLine("\t[string]$RedisInstanceName = \"Redis\",");
            file.WriteLine("\t# The path to the redis-cli executable.");
            file.WriteLine("\t[string]$RedisCliPath = \"$($Env:SYSTEMDRIVE)\\Program Files\\Redis\\redis-cli.exe\",");
            file.WriteLine();
            file.WriteLine("\t# The location of the database server where Commerce databases should be deployed. In case of named SQL instance, use \"SQLServerName\\SQLInstanceName\"");
            file.WriteLine("\t[string]$CommerceServicesDbServer = \"" + sitedetails.CommerceServicesDBServer + "\",");
            file.WriteLine("\t# The name of the shared database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesDbName = \"" + sitedetails.CommerceDbName + "\",");
            file.WriteLine("\t# The name of the global database for the Commerce Services.");
            file.WriteLine("\t[string]$CommerceServicesGlobalDbName =  \"" + sitedetails.CommerceGlobalDbName + "\",");
            file.WriteLine("\t# The port for the Commerce Ops Service.");
            file.WriteLine("\t[string]$CommerceOpsServicesPort = \"" + sitedetails.CommerceOpsSvcPort + "\",");
            file.WriteLine("\t# The port for the Commerce Shops Service");
            file.WriteLine("\t[string]$CommerceShopsServicesPort = \"" + sitedetails.CommerceShopsServicesPort + "\",");
            file.WriteLine("\t# The port for the Commerce Authoring Service.");
            file.WriteLine("\t[string]$CommerceAuthoringServicesPort = \"" + sitedetails.CommerceAuthSvcPort + "\",");
            file.WriteLine("\t# The port for the Commerce Minions Service.");
            file.WriteLine("\t[string]$CommerceMinionsServicesPort = \"" + sitedetails.CommerceMinionsSvcPort + "\",");
            file.WriteLine("\t# The postfix appended to Commerce services folders names and sitenames.");
            file.WriteLine("\t# The postfix allows you to host more than one Commerce installment on one server.");
            file.WriteLine("\t[string]$CommerceServicesPostfix = \"" + sitedetails.CommerceSvcPostFix + "\",");
            file.WriteLine("\t# The postfix used as the root domain name (two-levels) to append as the hostname for Commerce services.");
            file.WriteLine("\t# By default, all Commerce services are configured as sub-domains of the domain identified by the postfix.");
            file.WriteLine("\t# Postfix validation enforces the following rules:");
            file.WriteLine("\t# 1. The first level (TopDomainName) must be 2-7 characters in length and can contain alphabetical characters (a-z, A-Z) only. Numeric and special characters are not valid.");
            file.WriteLine("\t# 2. The second level (DomainName) can contain alpha-numeric characters (a-z, A-Z,and 0-9) and can include one hyphen (-) character.");
            file.WriteLine("\t# Special characters (wildcard (*)), for example, are not valid.");
            file.WriteLine("\t[string]$CommerceServicesHostPostfix = \"" + sitedetails.CommerceServicesHostPostFix + "\",");
            file.WriteLine();
            file.WriteLine("\t# The name of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxSiteName = \"" + sitedetails.BizFxName + "\",");
            file.WriteLine("\t# The port of the Sitecore XC Business Tools server.");
            file.WriteLine("\t[string]$BizFxPort = \"" + sitedetails.BizFxPort + "\",");
            file.WriteLine();
            file.WriteLine("\t# The prefix used in the EnvironmentName setting in the config.json file for each Commerce Engine role.");
            file.WriteLine("\t[string]$EnvironmentsPrefix = \"" + sitedetails.EnvironmentsPrefix + "\",");
            file.WriteLine("\t# The list of Commerce environment names. By default, the script deploys the AdventureWorks and the Habitat environments.");
            file.WriteLine("\t[array]$Environments = @(\"AdventureWorksAuthoring\", \"HabitatAuthoring\"),");

            file.WriteLine("\t# Commerce environments GUIDs used to clean existing Redis cache during deployment. Default parameter values correspond to the default Commerce environment GUIDS.");
            file.WriteLine("\t[array]$EnvironmentsGuids = @(\"78a1ea611f3742a7ac899a3f46d60ca5\", \"40e77b7b4be94186b53b5bfd89a6a83b\"),");
            file.WriteLine("\t# The environments running the minions service. (This is required, for example, for running indexing minions).");
            file.WriteLine("\t[array]$MinionEnvironments = @(\"AdventureWorksMinions\", \"HabitatMinions\"),");
            file.WriteLine("\t# whether to deploy sample data for each environment.");
            if (sitedetails.DeploySampleData=="Y")
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
            file.WriteLine("\t[string]$UserName = \"" + sitedetails.UserName + "\",");
            file.WriteLine("\t# The password for the $UserName.");
            file.WriteLine("\t[string]$UserPassword = \"" + sitedetails.UserPassword + "\",");
            file.WriteLine();
            file.WriteLine("\t# The Braintree Merchant Id.");
            file.WriteLine("\t[string]$BraintreeMerchantId = \"" + sitedetails.BraintreeMerchantId + "\",");
            file.WriteLine("\t# The Braintree Public Key.");
            file.WriteLine("\t[string]$BraintreePublicKey = \"" + sitedetails.BraintreePublicKey + "\",");
            file.WriteLine("\t# The Braintree Private Key.");
            file.WriteLine("\t[string]$BraintreePrivateKey = \"" + sitedetails.BraintreePrivateKey + "\",");
            file.WriteLine("\t# The Braintree Environment.");
            file.WriteLine("\t[string]$BraintreeEnvironment = \"" + sitedetails.BraintreeEnvironment + "\",");
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

        private void DeleteScript(string path)
        {
            var appcmdExe = "C:\\windows\\system32\\inetsrv\\appcmd.exe";
            var stoppedStatus = "Stopped";
            var commerceOpsSitePath = "IIS:\\Sites\\Default Web Site\\$CommerceOpsSiteName";
            var commerceShopsSitePath = "IIS:\\Sites\\Default Web Site\\$CommerceShopsSiteName";
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
            file.WriteLine("\t[string]$Prefix = \"" + sitedetails.SiteNamePrefix + "\",");
            file.WriteLine("\t[string]$UserFolder = \"" + sitedetails.SiteNamePrefix + "sc_User" + "\",");
            file.WriteLine("\t[string]$CommDbPrefix = \"" + sitedetails.SiteNamePrefix + "_SitecoreCommerce" + "\",");
            file.WriteLine("\t[string]$SiteName = \"" + sitedetails.SiteName + "\",");
            file.WriteLine("\t[string]$SitecoreBizFxSiteName = \"" + sitedetails.BizFxName + "\",");
            file.WriteLine("\t[string]$CommerceServicesPostfix = \"" + sitedetails.CommerceSvcPostFix + "\",");
            file.WriteLine("\t[string]$SolrService = \"" + sitedetails.CommerceSvcPostFix + "\",");
            file.WriteLine("\t[string]$PathToSolr = \"" + sitedetails.SolrRoot + "\",");
            file.WriteLine("\t[string]$SqlServer = \"" + sitedetails.SitecoreDbServer + "\",");
            file.WriteLine("\t[string]$SqlAccount = \"" + sitedetails.SitecoreSqlUser + "\",");
            file.WriteLine("\t[string]$SqlPassword = \"" + sitedetails.SitecoreSqlPass + "\",");
            file.WriteLine("\t[string]$SitecorexConnectSiteName = \"" + sitedetails.SiteNamePrefix + "xconnect" + sitedetails.SiteNameSuffix + "\",");
            file.WriteLine("\t[string]$SitecoreIdentityServerSiteName = \"" + sitedetails.IDServerSiteName + "\",");
            file.WriteLine("\t[string]$CommerceServicesHostPostfix = \"" + sitedetails.CommerceServicesHostPostFix + "\",");
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

            file.WriteLine("rm " + certificatesFolderPath + "$Prefix" + storefrontCertExt + " -force -recurse -ea ig");
            file.WriteLine("rm " + certificatesFolderPath + "$SitecoreIdentityServerSiteName" + ".pfx  -force -recurse -ea ig");
            file.WriteLine("rm " + certificatesFolderPath + "$SitecorexConnectSiteName" + ".pfx -force -recurse -ea ig");
            file.WriteLine("pop-location");
        }

        private void rbUninstallScript_CheckedChanged(object sender, EventArgs e)
        {
            switch (Version.SitecoreVersion)
            {
                case "9.3":
                    Write93PreviewFile(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_UnInstall_Script_Preview.ps1", sitedetails.HabitatExists, true);
                    break;
                case "10.0":
                case "10.0.1":
                case "10.1.0":
                    WritePreviewFile(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_UnInstall_Script_Preview.ps1", sitedetails.HabitatExists, true);
                    break;
                default:
                    break;
            }
            string text = System.IO.File.ReadAllText(@".\" + SCIASettings.FilePrefixAppString +  sitedetails.SiteName + "_UnInstall_Script_Preview.ps1");
            SetStatusMessage("Happy Sitecoring!", Color.DarkGreen);
            lblPreview.Text = text;
        }

        private void rbDeleteScript_CheckedChanged(object sender, EventArgs e)
        {
            DeleteScript(SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Delete_Script_Preview.ps1");
            string text = System.IO.File.ReadAllText(@".\" + SCIASettings.FilePrefixAppString + sitedetails.SiteName + "_Delete_Script_Preview.ps1");
            SetStatusMessage("Happy Sitecoring!", Color.DarkGreen);
            lblPreview.Text = text;
        }

        private void rbInstallScript_CheckedChanged_1(object sender, EventArgs e)
        {

        }
    }
}
