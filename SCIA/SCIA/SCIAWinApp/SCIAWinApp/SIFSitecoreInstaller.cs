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
    public partial class SIFSitecoreInstaller : Form
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
        string identityServerNameString = "identityserver";//identityserver
        string xConnectServerNameString = "xconnect";//xconnect
        string siteRootDir = "c:\\inetpub\\wwwroot";//"c:\\intetpub\\wwwroot\\
        private SettingsData settingsData { get; set; }
        public int TabIndexValue { get; set; }
        public SIFSitecoreInstaller()
        {
            InitializeComponent();
            this.Text = this.Text + " for Sitecore v" + Version.SitecoreVersion;
            switch (Version.SitecoreVersion)
            {
                case "10.1.0":
                case "10.0.1":
                case "10.0":
                case "9.3":
                case "9.2":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);
                    prereqs = CommonFunctions.GetVersionPrerequisites(Version.SitecoreVersion, "sitecoredevsetup");
                    break;
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                case "9.1":
                    prereqs = CommonFunctions.GetVersionPrerequisites(Version.SitecoreVersion, "sitecoresif");
                    destFolder = ZipList.SitecoreSifZip;
                    xp0Install = true;
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

        void Write90PSFile(string path, bool uninstallscript)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("Install-Module -Name SitecoreInstallFramework -Repository SitecoreGallery -RequiredVersion 1.2.1");
            //file.WriteLine("Remove-Module -Name SitecoreInstallFramework");
            file.WriteLine("Import-Module -Name SitecoreInstallFramework -RequiredVersion 1.2.1");
            file.WriteLine("Import-Module SitecoreFundamentals");

            file.WriteLine("#define parameters");
            file.WriteLine("$prefix = \"" + txtSiteNamePrefix.Text + "\"");
            file.WriteLine("$PSScriptRoot = \".\"");
            file.WriteLine("$XConnectCollectionService = \"" + txtxConnectCollectionSvc.Text + "\"");
            file.WriteLine("$sitecoreSiteName = \"" + txtSiteName.Text + "\"");
            file.WriteLine("$SolrUrl = \"" + txtSolrUrl.Text + "\"");
            file.WriteLine("$SolrRoot = \"" + txtSolrRoot.Text + "\"");
            file.WriteLine("$SolrService = \"" + txtSolrService.Text + "\"");
            file.WriteLine("$SqlServer = \"" + txtSqlDbServer.Text + "\"");
            file.WriteLine("$SqlAdminUser = \"" + txtSqlUser.Text + "\"");
            file.WriteLine("$SqlAdminPassword=\"" + txtSqlPass.Text + "\"");
            file.WriteLine("#install client certificate for xconnect");
            file.WriteLine("$certParams = @{");
            //file.WriteLine(" Path = \"$PSScriptRoot\\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_xconnect-createcert.json" + "\"");
            file.WriteLine(" Path = \"$PSScriptRoot\\xconnect-createcert.json\"");
            file.WriteLine(" CertificateName = \"$prefix.xconnect_client\"");
            file.WriteLine(" RootCertFileName = $prefix");
            file.WriteLine("}");
            if (!uninstallscript)
            {
                file.WriteLine("Install-SitecoreConfiguration @certParams -Verbose");
            }
            else
            {
                file.WriteLine("UnInstall-SitecoreConfiguration @certParams -Verbose");
            }
            file.WriteLine("#install solr cores for xdb");
            file.WriteLine("$solrParams = @{");
            file.WriteLine(" Path = \"$PSScriptRoot\\xconnect-solr.json\"");
            file.WriteLine(" SolrUrl = $SolrUrl");
            file.WriteLine(" SolrRoot = $SolrRoot");
            file.WriteLine(" SolrService = $SolrService");
            file.WriteLine(" CorePrefix = $prefix");
            file.WriteLine("}");
            if (!uninstallscript)
            {
                file.WriteLine("Install-SitecoreConfiguration @solrParams");
            }
            else
            {
                file.WriteLine("UnInstall-SitecoreConfiguration @solrParams");
            }
            file.WriteLine("#deploy xconnect instance");
            file.WriteLine("$xconnectParams = @{");
            file.WriteLine(" Path = \"$PSScriptRoot\\xconnect-xp0.json\"");
            file.WriteLine(" Package = \"$PSScriptRoot\\Sitecore * (OnPrem)_xp0xconnect.scwdp.zip\"");

            file.WriteLine(" LicenseFile = \"$PSScriptRoot\\license.xml\"");
            file.WriteLine(" Sitename = \"" + txtxConnectSiteName.Text + "\"");
            file.WriteLine(" XConnectCert = $certParams.CertificateName");
            file.WriteLine(" SqlDbPrefix = $prefix");
            file.WriteLine("SqlServer = $SqlServer");
            file.WriteLine("SqlAdminUser = $SqlAdminUser");
            file.WriteLine(" SqlAdminPassword = $SqlAdminPassword");
            file.WriteLine(" SolrCorePrefix = $prefix");
            file.WriteLine(" SolrURL = $SolrUrl");
            file.WriteLine("}");
            if (!uninstallscript)
            {
                file.WriteLine("Install-SitecoreConfiguration @xconnectParams");
            }
            else
            {
                file.WriteLine("UnInstall-SitecoreConfiguration @xconnectParams");
            }
            file.WriteLine("#install solr cores for sitecore");
            file.WriteLine("#$solrParams = @{");
            file.WriteLine("# Path = \"$PSScriptRoot\\sitecore-solr.json\"");
            file.WriteLine("# SolrUrl = $SolrUrl");
            file.WriteLine("# SolrRoot = $SolrRoot");
            file.WriteLine("# SolrService = $SolrService");
            file.WriteLine("# CorePrefix = $prefix");
            file.WriteLine("#}");
            if (!uninstallscript)
            {
                file.WriteLine("#Install-SitecoreConfiguration @solrParams");
            }
            else
            {
                file.WriteLine("#UnInstall-SitecoreConfiguration @solrParams");
            }
            file.WriteLine("#install sitecore instance");
            file.WriteLine("$xconnectHostName = $XConnectCollectionService");
            file.WriteLine("$sitecoreParams = @{");
            file.WriteLine(" Path = \"$PSScriptRoot\\sitecore-XP0.json\"");

            file.WriteLine(" Package = \"$PSScriptRoot\\Sitecore * (OnPrem)_single.scwdp.zip\"");

            file.WriteLine(" LicenseFile = \"$PSScriptRoot\\license.xml\"");
            file.WriteLine(" SqlDbPrefix = $prefix");
            file.WriteLine("SqlServer = $SqlServer");
            file.WriteLine("SqlAdminUser = $SqlAdminUser");
            file.WriteLine(" SqlAdminPassword = $SqlAdminPassword");
            file.WriteLine(" SolrCorePrefix = $prefix");
            file.WriteLine("SolrUrl = $SolrUrl");
            file.WriteLine(" XConnectCert = $certParams.CertificateName");
            file.WriteLine(" Sitename = $sitecoreSiteName");
            file.WriteLine(" XConnectCollectionService = \"$XConnectCollectionService\"");
            file.WriteLine("}");
            if (!uninstallscript)
            {
                file.WriteLine("Install-SitecoreConfiguration @sitecoreParams");
            }
            else
            {
                file.WriteLine("UnInstall-SitecoreConfiguration @sitecoreParams");
            }

            file.Dispose();
        }

            void WriteSingleDeveloperJsonFile(string path)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"XConnectCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the xconnect server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the identity server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the identity server to be created.\",");
            file.WriteLine("            \"DefaultValue\": \"IdentityServer\"");
            file.WriteLine("        },");
            file.WriteLine("        \"LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore license file.\",");
            file.WriteLine("            \"DefaultValue\": \".\\\\License.xml\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Prefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSiteNamePrefix.Text + "\",");
            file.WriteLine("            \"Description\": \"The prefix for uniquely identifying instances.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSitecoreUserPassword.Text + "\",");
            file.WriteLine("            \"Description\": \"The admin password for the Sitecore instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"sa\",");
            file.WriteLine("            \"Description\": \"The Sql admin user account to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSqlPass.Text + "\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"" + serverName + "\",");
            file.WriteLine("            \"Description\": \"The Sql Server where databases will be installed.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCollectionPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingPoolsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Pools connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReferenceDataPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reference Data connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMarketingAutomationPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Marketing Automation connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMessagingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Messaging connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingEnginePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Engine Tasks and Storage database connection strings in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReportingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reporting connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCorePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Core connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlSecurityPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Security connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlWebPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Web connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingTasksPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Tasks connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlFormsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Experience Forms connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlExmMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the EXM Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"https://localhost:8983/solr\",");
            file.WriteLine("            \"Description\": \"The Solr instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"C:\\\\solr-7.2.1\",");
            file.WriteLine("            \"Description\": \"The file path to the Solr instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Solr-7.2.1\",");
            file.WriteLine("            \"Description\": \"The name of the Solr service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the XConnect package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"XConnect\",");
            file.WriteLine("            \"Description\": \"The name of the XConnect site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Sitecore\",");
            file.WriteLine("            \"Description\": \"The name of the Sitecore site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Password recovery Url (Host name of CM instance).\",");
            file.WriteLine("            \"DefaultValue\": \"http:\\\\\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecorePackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Identity Server package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://XConnect\",");
            file.WriteLine("            \"Description\": \"The url for the XConnect Collection Service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"Sitecore IdentityServer clients configuration\"");
            file.WriteLine("        },");
            file.WriteLine("        \"AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://SitecoreIdentityServerHost\",");
            file.WriteLine("            \"Description\": \"IdentityServer provider URI.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientSecret\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Client secret of PasswordClient section. It's a random string between 1 and 100 symbols long.\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Microsoft Machine Learning Server instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to certificate of certification authority that issued certificates for Machine Learning Server Blob and Table storage endpoints.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectPackage value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecorePackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecorePackage value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerPackage value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectSiteName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreSiteName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PasswordRecoveryUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass PasswordRecoveryUrl value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreIdentityAuthority\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreIdentityAuthority value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCollectionService\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCollectionService value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"ClientsConfiguration\",");
            file.WriteLine("            \"Description\": \"Override to pass ClientsConfiguration value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrURL\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCertificateName value to XConnectCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServerCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerSiteName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SitecoreIdentityCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"AllowedCorsOrigins\",");
            file.WriteLine("            \"Description\": \"Override to pass AllowedCorsOrigins value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerUrl value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerEndpointCertificationAuthorityCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Variables\": {");
            file.WriteLine("        \"SqlProcessingPools.Password\": \"[if(variable('Test.SqlProcessingPools.Password'),variable('Generate.SqlProcessingPools.Password'),parameter('SqlProcessingPoolsPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingPools.Password\": \"[equal(parameter('SqlProcessingPoolsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingPools.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCollection.Password\": \"[if(variable('Test.SqlCollection.Password'),variable('Generate.SqlCollection.Password'),parameter('SqlCollectionPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlCollection.Password\": \"[variable('SqlCollection.Password')]\",");
            file.WriteLine("        \"Test.SqlCollection.Password\": \"[equal(parameter('SqlCollectionPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCollection.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReferenceData.Password\": \"[if(variable('Test.SqlReferenceData.Password'),variable('Generate.SqlReferenceData.Password'),parameter('SqlReferenceDataPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"Test.SqlReferenceData.Password\": \"[equal(parameter('SqlReferenceDataPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReferenceData.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMarketingAutomation.Password\": \"[if(variable('Test.SqlMarketingAutomation.Password'),variable('Generate.SqlMarketingAutomation.Password'),parameter('SqlMarketingAutomationPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"Test.SqlMarketingAutomation.Password\": \"[equal(parameter('SqlMarketingAutomationPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMarketingAutomation.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMessaging.Password\": \"[if(variable('Test.SqlMessaging.Password'),variable('Generate.SqlMessaging.Password'),parameter('SqlMessagingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"Test.SqlMessaging.Password\": \"[equal(parameter('SqlMessagingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMessaging.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingEngine.Password\": \"[if(variable('Test.SqlProcessingEngine.Password'),variable('Generate.SqlProcessingEngine.Password'),parameter('SqlProcessingEnginePassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingEngine.Password\": \"[variable('SqlProcessingEngine.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingEngine.Password\": \"[equal(parameter('SqlProcessingEnginePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingEngine.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReporting.Password\": \"[if(variable('Test.SqlReporting.Password'),variable('Generate.SqlReporting.Password'),parameter('SqlReportingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"Test.SqlReporting.Password\": \"[equal(parameter('SqlReportingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReporting.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCore.Password\": \"[if(variable('Test.SqlCore.Password'),variable('Generate.SqlCore.Password'),parameter('SqlCorePassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlCore.Password\": \"[variable('SqlCore.Password')]\",");
            file.WriteLine("        \"IdentityServer:SqlCore.Password\": \"[variable('SqlCore.Password')]\",");
            file.WriteLine("        \"Test.SqlCore.Password\": \"[equal(parameter('SqlCorePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCore.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlSecurity.Password\": \"[if(variable('Test.SqlSecurity.Password'),variable('Generate.SqlSecurity.Password'),parameter('SqlSecurityPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlSecurity.Password\": \"[variable('SqlSecurity.Password')]\",");
            file.WriteLine("        \"Test.SqlSecurity.Password\": \"[equal(parameter('SqlSecurityPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlSecurity.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMaster.Password\": \"[if(variable('Test.SqlMaster.Password'),variable('Generate.SqlMaster.Password'),parameter('SqlMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMaster.Password\": \"[variable('SqlMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlMaster.Password\": \"[equal(parameter('SqlMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlWeb.Password\": \"[if(variable('Test.SqlWeb.Password'),variable('Generate.SqlWeb.Password'),parameter('SqlWebPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlWeb.Password\": \"[variable('SqlWeb.Password')]\",");
            file.WriteLine("        \"Test.SqlWeb.Password\": \"[equal(parameter('SqlWebPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlWeb.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingTasks.Password\": \"[if(variable('Test.SqlProcessingTasks.Password'),variable('Generate.SqlProcessingTasks.Password'),parameter('SqlProcessingTasksPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingTasks.Password\": \"[variable('SqlProcessingTasks.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingTasks.Password\": \"[equal(parameter('SqlProcessingTasksPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingTasks.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlForms.Password\": \"[if(variable('Test.SqlForms.Password'),variable('Generate.SqlForms.Password'),parameter('SqlFormsPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlForms.Password\": \"[variable('SqlForms.Password')]\",");
            file.WriteLine("        \"Test.SqlForms.Password\": \"[equal(parameter('SqlFormsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlForms.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlExmMaster.Password\": \"[if(variable('Test.SqlExmMaster.Password'),variable('Generate.SqlExmMaster.Password'),parameter('SqlExmMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlExmMaster.Password\": \"[variable('SqlExmMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlExmMaster.Password\": \"[equal(parameter('SqlExmMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlExmMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"Sitecore.Admin.Password\": \"[if(variable('Test.Admin.Password'),variable('Generate.Admin.Password'),parameter('SitecoreAdminPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:Sitecore.Admin.Password\": \"[variable('Sitecore.Admin.Password')]\",");
            file.WriteLine("        \"Test.Admin.Password\": \"[equal(parameter('SitecoreAdminPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.Admin.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"Client.Secret\" : \"[if(variable('Test.Client.Secret'),variable('Generate.Client.Secret'),parameter('ClientSecret'))]\",");
            file.WriteLine("        \"IdentityServer:Client.Secret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"SitecoreXP0:Sitecore.IdentitySecret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"Test.Client.Secret\": \"[equal(parameter('ClientSecret'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.Client.Secret\": \"[randomstring(Length:100,DisAllowSpecial:True)]\"");
            file.WriteLine("    },");
            file.WriteLine("    \"Includes\": {");
            file.WriteLine("        \"IdentityServerCertificates\":{");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer\": {");
            file.WriteLine("            \"Source\": \".\\\\identityServer.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates\": {");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-xp0.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-XP0.json\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Register\": {");
            file.WriteLine("        \"Tasks\": {");
            file.WriteLine("            \"SetVariable\": \"Set-Variable\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Tasks\": {");
            file.WriteLine("        \"GeneratePasswords\": {");
            file.WriteLine("            \"Description\": \"Generates all shared passwords and secrets.\",");
            file.WriteLine("            \"Type\": \"SetVariable\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Name\": \"XP0Passwords\",");
            file.WriteLine("                \"Scope\": \"Global\",");
            file.WriteLine("                \"Value\": [");
            file.WriteLine("                    {\"SqlProcessingPoolsPassword\":     \"[variable('SqlProcessingPools.Password')]\"},");
            file.WriteLine("                    {\"SqlCollectionPassword\":          \"[variable('SqlCollection.Password')]\"},");
            file.WriteLine("                    {\"SqlReferenceDataPassword\":       \"[variable('SqlReferenceData.Password')]\"},");
            file.WriteLine("                    {\"SqlMarketingAutomationPassword\": \"[variable('SqlMarketingAutomation.Password')]\"},");
            file.WriteLine("                    {\"SqlMessagingPassword\":           \"[variable('SqlMessaging.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingEnginePassword\":    \"[variable('SqlProcessingEngine.Password')]\"},");
            file.WriteLine("                    {\"SqlReportingPassword\":           \"[variable('SqlReporting.Password')]\"},");
            file.WriteLine("                    {\"SqlCorePassword\":                \"[variable('SqlCore.Password')]\"},");
            file.WriteLine("                    {\"SqlSecurityPassword\":            \"[variable('SqlSecurity.Password')]\"},");
            file.WriteLine("                    {\"SqlMasterPassword\":              \"[variable('SqlMaster.Password')]\"},");
            file.WriteLine("                    {\"SqlWebPassword\":                 \"[variable('SqlWeb.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingTasksPassword\":     \"[variable('SqlProcessingTasks.Password')]\"},");
            file.WriteLine("                    {\"SqlFormsPassword\":               \"[variable('SqlForms.Password')]\"},");
            file.WriteLine("                    {\"SqlExmMasterPassword\":           \"[variable('SqlExmMaster.Password')]\"},");
            file.WriteLine("                    {\"SitecoreAdminPassword\":          \"[variable('Sitecore.Admin.Password')]\"},");
            file.WriteLine("                    {\"ClientSecret\":                   \"[variable('Client.Secret')]\"}");
            file.WriteLine("                ]");
            file.WriteLine("            }");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");

            file.WriteLine();
            file.Dispose();

        }

        void WriteCreateCertJsonFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"CertificateName\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"CertPath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"The physical path on disk where certificates will be stored.\",");
            file.WriteLine("            \"DefaultValue\": \"c:\\\\certificates\"");
            file.WriteLine("        },");
            file.WriteLine("        \"RootCertFileName\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"The file name of the root certificate to be created.\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSiteName.Text + "\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Variables\": {");
            file.WriteLine("        // The name dns name of the root certificate.");
            file.WriteLine("        \"Root.Cert.DnsName\": \"[concat('DO_NOT_TRUST_', parameter('RootCertFileName'))]\",");
            file.WriteLine("        // The certificate store for the root certificate.");
            file.WriteLine("        \"Root.Cert.Store\": \"cert:\\\\LocalMachine\\\\Root\",");
            file.WriteLine("        // The certificate store for the client certificate.");
            file.WriteLine("        \"Client.Cert.Store\": \"cert:\\\\LocalMachine\\\\My\"");
            file.WriteLine("    },");
            file.WriteLine("    \"Tasks\": {");
            file.WriteLine("        \"CreatePaths\": {");
            file.WriteLine("            // Create the physical disk path.");
            file.WriteLine("            \"Type\": \"EnsurePath\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Exists\": [");
            file.WriteLine("                    \"[parameter('CertPath')]\"");
            file.WriteLine("                ]");
            file.WriteLine("            }");
            file.WriteLine("        },");
            file.WriteLine("        \"CreateRootCert\": {");
            file.WriteLine("            // Create the root certificate.");
            file.WriteLine("            \"Type\": \"NewRootCertificate\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Path\": \"[parameter('CertPath')]\",");
            file.WriteLine("                \"Name\": \"[parameter('RootCertFileName')]\",");
            file.WriteLine("                \"StoreLocation\": \"CurrentUser\",");
            file.WriteLine("                \"DnsName\": \"[variable('Root.Cert.DnsName')]\"");
            file.WriteLine("            }");
            file.WriteLine("        },");
            file.WriteLine("        \"ImportRootCertificate\": {");
            file.WriteLine("            // Import the root certificate.");
            file.WriteLine("            \"Type\": \"ImportCertificate\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"CertStoreLocation\": \"[variable('Root.Cert.Store')]\",");
            file.WriteLine("                \"FilePath\": \"[concat(joinpath(parameter('CertPath'), parameter('RootCertFileName')), '.crt')]\"");
            file.WriteLine("            }");
            file.WriteLine("        },");
            file.WriteLine("        \"CreateSignedCert\": {");
            file.WriteLine("            // Create a certificate signed by the root authority.");
            file.WriteLine("            \"Type\": \"NewSignedCertificate\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Signer\": \"[GetCertificate(variable('Root.Cert.DnsName'), variable('Root.Cert.Store'))]\",");
            file.WriteLine("                \"Path\": \"[parameter('CertPath')]\",");
            file.WriteLine("                \"CertStoreLocation\": \"[variable('Client.Cert.Store')]\",");
            file.WriteLine("                \"Name\": \"[parameter('CertificateName')]\",");
            file.WriteLine("                \"DnsName\": \"[parameter('CertificateName')]\"");
            file.WriteLine("            }");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");
            file.Dispose();

        }

        void Write92JsonFile(string path)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"XConnectCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the xconnect server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the identity server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the identity server to be created.\",");
            file.WriteLine("            \"DefaultValue\": \"IdentityServer\"");
            file.WriteLine("        },");
            file.WriteLine("        \"LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore license file.\",");
            file.WriteLine("            \"DefaultValue\": \".\\\\License.xml\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Prefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"SC910\",");
            file.WriteLine("            \"Description\": \"The prefix for uniquely identifying instances.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"" + txtSitecoreUserPassword.Text + "\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSqlUser.Text + "\",");
            file.WriteLine("            \"Description\": \"The Sql admin user account to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSqlPass.Text + "\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"" + serverName + "\",");
            file.WriteLine("            \"Description\": \"The Sql Server where databases will be installed.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCollectionPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingPoolsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Pools connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReferenceDataPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reference Data connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMarketingAutomationPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Marketing Automation connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMessagingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Messaging connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingEnginePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Engine Tasks and Storage database connection strings in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReportingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reporting connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCorePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Core connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlSecurityPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Security connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlWebPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Web connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingTasksPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Tasks connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlFormsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Experience Forms connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlExmMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the EXM Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"https://localhost:8983/solr\",");
            file.WriteLine("            \"Description\": \"The Solr instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"C:\\\\solr-8.4.0\",");
            file.WriteLine("            \"Description\": \"The file path to the Solr instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Solr-8.4.0\",");
            file.WriteLine("            \"Description\": \"The name of the Solr service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the XConnect package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"XConnect\",");
            file.WriteLine("            \"Description\": \"The name of the XConnect site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Sitecore\",");
            file.WriteLine("            \"Description\": \"The name of the Sitecore site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Password recovery Url (Host name of CM instance).\",");
            file.WriteLine("            \"DefaultValue\": \"http:\\\\\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecorePackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Identity Server package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://XConnect\",");
            file.WriteLine("            \"Description\": \"The url for the XConnect Collection Service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"Sitecore IdentityServer clients configuration\"");
            file.WriteLine("        },");
            file.WriteLine("        \"AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://SitecoreIdentityServerHost\",");
            file.WriteLine("            \"Description\": \"IdentityServer provider URI.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientSecret\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Client secret of PasswordClient section. It's a random string between 1 and 100 symbols long.\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Microsoft Machine Learning Server instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to certificate of certification authority that issued certificates for Machine Learning Server Blob and Table storage endpoints.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectPackage value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecorePackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecorePackage value to SitecoreXP0 config.\"");
            file.WriteLine("        },	        ");
            file.WriteLine("        \"IdentityServer:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerPackage value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectSiteName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreSiteName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PasswordRecoveryUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass PasswordRecoveryUrl value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreIdentityAuthority\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreIdentityAuthority value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCollectionService\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCollectionService value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"ClientsConfiguration\",");
            file.WriteLine("            \"Description\": \"Override to pass ClientsConfiguration value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrURL\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCertificateName value to XConnectCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServerCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerSiteName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SitecoreIdentityCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"AllowedCorsOrigins\",");
            file.WriteLine("            \"Description\": \"Override to pass AllowedCorsOrigins value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerUrl value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerEndpointCertificationAuthorityCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Variables\": {");
            file.WriteLine("        \"SqlProcessingPools.Password\": \"[if(variable('Test.SqlProcessingPools.Password'),variable('Generate.SqlProcessingPools.Password'),parameter('SqlProcessingPoolsPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingPools.Password\": \"[equal(parameter('SqlProcessingPoolsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingPools.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCollection.Password\": \"[if(variable('Test.SqlCollection.Password'),variable('Generate.SqlCollection.Password'),parameter('SqlCollectionPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlCollection.Password\": \"[variable('SqlCollection.Password')]\",");
            file.WriteLine("        \"Test.SqlCollection.Password\": \"[equal(parameter('SqlCollectionPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCollection.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReferenceData.Password\": \"[if(variable('Test.SqlReferenceData.Password'),variable('Generate.SqlReferenceData.Password'),parameter('SqlReferenceDataPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"Test.SqlReferenceData.Password\": \"[equal(parameter('SqlReferenceDataPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReferenceData.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMarketingAutomation.Password\": \"[if(variable('Test.SqlMarketingAutomation.Password'),variable('Generate.SqlMarketingAutomation.Password'),parameter('SqlMarketingAutomationPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"Test.SqlMarketingAutomation.Password\": \"[equal(parameter('SqlMarketingAutomationPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMarketingAutomation.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMessaging.Password\": \"[if(variable('Test.SqlMessaging.Password'),variable('Generate.SqlMessaging.Password'),parameter('SqlMessagingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"Test.SqlMessaging.Password\": \"[equal(parameter('SqlMessagingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMessaging.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingEngine.Password\": \"[if(variable('Test.SqlProcessingEngine.Password'),variable('Generate.SqlProcessingEngine.Password'),parameter('SqlProcessingEnginePassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingEngine.Password\": \"[variable('SqlProcessingEngine.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingEngine.Password\": \"[equal(parameter('SqlProcessingEnginePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingEngine.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReporting.Password\": \"[if(variable('Test.SqlReporting.Password'),variable('Generate.SqlReporting.Password'),parameter('SqlReportingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"Test.SqlReporting.Password\": \"[equal(parameter('SqlReportingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReporting.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCore.Password\": \"[if(variable('Test.SqlCore.Password'),variable('Generate.SqlCore.Password'),parameter('SqlCorePassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlCore.Password\": \"[variable('SqlCore.Password')]\",");
            file.WriteLine("        \"Test.SqlCore.Password\": \"[equal(parameter('SqlCorePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCore.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlSecurity.Password\": \"[if(variable('Test.SqlSecurity.Password'),variable('Generate.SqlSecurity.Password'),parameter('SqlSecurityPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlSecurity.Password\": \"[variable('SqlSecurity.Password')]\",");
            file.WriteLine("        \"IdentityServer:Sql.Database.Password\": \"[variable('SqlSecurity.Password')]\",");
            file.WriteLine("        \"Test.SqlSecurity.Password\": \"[equal(parameter('SqlSecurityPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlSecurity.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMaster.Password\": \"[if(variable('Test.SqlMaster.Password'),variable('Generate.SqlMaster.Password'),parameter('SqlMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMaster.Password\": \"[variable('SqlMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlMaster.Password\": \"[equal(parameter('SqlMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlWeb.Password\": \"[if(variable('Test.SqlWeb.Password'),variable('Generate.SqlWeb.Password'),parameter('SqlWebPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlWeb.Password\": \"[variable('SqlWeb.Password')]\",");
            file.WriteLine("        \"Test.SqlWeb.Password\": \"[equal(parameter('SqlWebPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlWeb.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingTasks.Password\": \"[if(variable('Test.SqlProcessingTasks.Password'),variable('Generate.SqlProcessingTasks.Password'),parameter('SqlProcessingTasksPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingTasks.Password\": \"[variable('SqlProcessingTasks.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingTasks.Password\": \"[equal(parameter('SqlProcessingTasksPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingTasks.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlForms.Password\": \"[if(variable('Test.SqlForms.Password'),variable('Generate.SqlForms.Password'),parameter('SqlFormsPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlForms.Password\": \"[variable('SqlForms.Password')]\",");
            file.WriteLine("        \"Test.SqlForms.Password\": \"[equal(parameter('SqlFormsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlForms.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlExmMaster.Password\": \"[if(variable('Test.SqlExmMaster.Password'),variable('Generate.SqlExmMaster.Password'),parameter('SqlExmMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlExmMaster.Password\": \"[variable('SqlExmMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlExmMaster.Password\": \"[equal(parameter('SqlExmMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlExmMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("		\"Sitecore.Admin.Password\": \"[if(variable('Test.Admin.Password'),variable('Generate.Admin.Password'),parameter('SitecoreAdminPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:Sitecore.Admin.Password\": \"[variable('Sitecore.Admin.Password')]\",");
            file.WriteLine("        \"Test.Admin.Password\": \"[equal(parameter('SitecoreAdminPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.Admin.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"Client.Secret\" : \"[if(variable('Test.Client.Secret'),variable('Generate.Client.Secret'),parameter('ClientSecret'))]\",");
            file.WriteLine("        \"IdentityServer:Client.Secret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"SitecoreXP0:Sitecore.IdentitySecret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"Test.Client.Secret\": \"[equal(parameter('ClientSecret'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.Client.Secret\": \"[randomstring(Length:100,DisAllowSpecial:True)]\"");
            file.WriteLine("    },");
            file.WriteLine("    \"Includes\": {        ");
            file.WriteLine("		\"IdentityServerCertificates\":{");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer\": {");
            file.WriteLine("            \"Source\": \".\\\\identityServer.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates\": {");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-xp0.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-XP0.json\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Register\": {");
            file.WriteLine("        \"Tasks\": {");
            file.WriteLine("            \"SetVariable\": \"Set-Variable\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Tasks\": {");
            file.WriteLine("        \"GeneratePasswords\": {");
            file.WriteLine("            \"Description\": \"Generates all shared passwords and secrets.\",");
            file.WriteLine("            \"Type\": \"SetVariable\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Name\": \"XP0Passwords\",");
            file.WriteLine("                \"Scope\": \"Global\",");
            file.WriteLine("                \"Value\": [");
            file.WriteLine("                    {\"SqlProcessingPoolsPassword\":     \"[variable('SqlProcessingPools.Password')]\"},");
            file.WriteLine("                    {\"SqlCollectionPassword\":          \"[variable('SqlCollection.Password')]\"},");
            file.WriteLine("                    {\"SqlReferenceDataPassword\":       \"[variable('SqlReferenceData.Password')]\"},");
            file.WriteLine("                    {\"SqlMarketingAutomationPassword\": \"[variable('SqlMarketingAutomation.Password')]\"},");
            file.WriteLine("                    {\"SqlMessagingPassword\":           \"[variable('SqlMessaging.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingEnginePassword\":    \"[variable('SqlProcessingEngine.Password')]\"},");
            file.WriteLine("                    {\"SqlReportingPassword\":           \"[variable('SqlReporting.Password')]\"},");
            file.WriteLine("                    {\"SqlCorePassword\":                \"[variable('SqlCore.Password')]\"},");
            file.WriteLine("                    {\"SqlSecurityPassword\":            \"[variable('SqlSecurity.Password')]\"},");
            file.WriteLine("                    {\"SqlMasterPassword\":              \"[variable('SqlMaster.Password')]\"},");
            file.WriteLine("                    {\"SqlWebPassword\":                 \"[variable('SqlWeb.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingTasksPassword\":     \"[variable('SqlProcessingTasks.Password')]\"},");
            file.WriteLine("                    {\"SqlFormsPassword\":               \"[variable('SqlForms.Password')]\"},");
            file.WriteLine("                    {\"SqlExmMasterPassword\":           \"[variable('SqlExmMaster.Password')]\"},");
            file.WriteLine("                    {\"ClientSecret\":                   \"[variable('Client.Secret')]\"}");
            file.WriteLine("                ]");
            file.WriteLine("            }");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");
            file.WriteLine();
            file.Dispose();

        }


        void WriteJsonFile(string path)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"XConnectCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the xconnect server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the certificate to be created for the identity server.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The name of the identity server to be created.\",");
            file.WriteLine("            \"DefaultValue\": \"IdentityServer\"");
            file.WriteLine("        },");
            file.WriteLine("        \"LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore license file.\",");
            file.WriteLine("            \"DefaultValue\": \".\\\\License.xml\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Prefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"SC910\",");
            file.WriteLine("            \"Description\": \"The prefix for uniquely identifying instances.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The admin password for the Sitecore instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitePhysicalRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Root folder to install the site to. If left on the default [systemdrive]:\\\\inetpub\\\\wwwroot will be used.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSqlUser.Text + "\",");
            file.WriteLine("            \"Description\": \"The Sql admin user account to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"" + txtSqlPass.Text + "\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"" + serverName + "\",");
            file.WriteLine("            \"Description\": \"The Sql Server where databases will be installed.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCollectionPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingPoolsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Pools connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReferenceDataPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reference Data connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMarketingAutomationPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Marketing Automation connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMessagingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Messaging connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingEnginePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Engine Tasks and Storage database connection strings in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlReportingPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Reporting connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlCorePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Core connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlSecurityPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Security connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlWebPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Web connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlProcessingTasksPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Processing Tasks connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlFormsPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the Experience Forms connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlExmMasterPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The Sql password for the EXM Master connection string in Sitecore.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"https://localhost:8983/solr\",");
            file.WriteLine("            \"Description\": \"The Solr instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"C:\\\\solr-8.4.0\",");
            file.WriteLine("            \"Description\": \"The file path to the Solr instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Solr-8.4.0\",");
            file.WriteLine("            \"Description\": \"The name of the Solr service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the XConnect package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"XConnect\",");
            file.WriteLine("            \"Description\": \"The name of the XConnect site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Sitecore\",");
            file.WriteLine("            \"Description\": \"The name of the Sitecore site.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Password recovery Url (Host name of CM instance).\",");
            file.WriteLine("            \"DefaultValue\": \"http:\\\\\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecorePackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Identity Server package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://XConnect\",");
            file.WriteLine("            \"Description\": \"The url for the XConnect Collection Service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"Sitecore IdentityServer clients configuration\"");
            file.WriteLine("        },");
            file.WriteLine("        \"AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"https://SitecoreIdentityServerHost\",");
            file.WriteLine("            \"Description\": \"IdentityServer provider URI.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"ClientSecret\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"Description\": \"Client secret of PasswordClient section. It's a random string between 1 and 100 symbols long.\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Microsoft Machine Learning Server instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Blob Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Path.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The Machine Learning Server Table Storage WebApi Endpoint Certificate Password.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to certificate of certification authority that issued certificates for Machine Learning Server Blob and Table storage endpoints.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Alternative location to save WDP packages. If left on the default $Env:Temp will be used.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"File with URI and SHA256 hashes of dynamically downloadable WDPs.\",");
            file.WriteLine("            \"DefaultValue\": \".\\\\downloads.json\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXAPackage\": {");
            file.WriteLine("            \"Type\": \"String\",            ");
            file.WriteLine("            \"Description\": \"Override to pass SXAPackage  value.\",");
            file.WriteLine("			\"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPEPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"Override to pass SPEPackage  value.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectPackage value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecorePackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecorePackage value to SitecoreXP0 config.\"");
            file.WriteLine("        },	        ");
            file.WriteLine("        \"IdentityServer:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerPackage value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectSiteName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreSiteName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SitePhysicalRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitePhysicalRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SitePhysicalRoot value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SitePhysicalRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitePhysicalRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SitePhysicalRoot value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SitePhysicalRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitePhysicalRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SitePhysicalRoot value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:PasswordRecoveryUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PasswordRecoveryUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass PasswordRecoveryUrl value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SitecoreIdentityAuthority\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreIdentityAuthority\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreIdentityAuthority value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCollectionService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCollectionService\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCollectionService value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:ClientsConfiguration\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"ClientsConfiguration\",");
            file.WriteLine("            \"Description\": \"Override to pass ClientsConfiguration value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlServer value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrURL\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrURL value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreSolr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass XConnectCertificateName value to XConnectCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServerCertificates:CertificateName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServerCertificates config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerSiteName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:SitecoreIdentityCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"IdentityServerCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass IdentityServerCertificateName value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:XConnectCert\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"XConnectCertificateName\",");
            file.WriteLine("            \"Description\": \"Override to pass CertificateName value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:LicenseFile\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"LicenseFile\",");
            file.WriteLine("            \"Description\": \"Override to pass LicenseFile value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:AllowedCorsOrigins\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"AllowedCorsOrigins\",");
            file.WriteLine("            \"Description\": \"Override to pass AllowedCorsOrigins value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerUrl value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerBlobEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerBlobEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerBlobEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerTableEndpointCertificatePassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerTableEndpointCertificatePassword\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerTableEndpointCertificatePassword value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:MachineLearningServerEndpointCertificationAuthorityCertificatePath\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"MachineLearningServerEndpointCertificationAuthorityCertificatePath\",");
            file.WriteLine("            \"Description\": \"Override to pass MachineLearningServerEndpointCertificationAuthorityCertificatePath value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreAdminPassword value to SitecoreXPO config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PackagesTempLocation\",");
            file.WriteLine("            \"Description\": \"Override to pass PackagesTempLocation value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PackagesTempLocation\",");
            file.WriteLine("            \"Description\": \"Override to pass PackagesTempLocation name value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PackagesTempLocation\",");
            file.WriteLine("            \"Description\": \"Override to pass PackagesTempLocation name value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer:DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"DownloadLocations\",");
            file.WriteLine("            \"Description\": \"Override to pass DownloadLocations value to IdentityServer config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0:DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"DownloadLocations\",");
            file.WriteLine("            \"Description\": \"Override to pass DownloadLocations name value to SitecoreXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0:DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"DownloadLocations\",");
            file.WriteLine("            \"Description\": \"Override to pass DownloadLocations name value to XConnectXP0 config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:Sitename\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreSiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreSiteName value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:SXAPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SXAPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SXAPackage value to SXASingleDeveloper config.\"");
            file.WriteLine("        },	");
            file.WriteLine("		\"SXASingleDeveloper:SPEPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SPEPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SPEPackage value to SXASingleDeveloper config.\"");
            file.WriteLine("        },	");
            file.WriteLine("		\"SXASingleDeveloper:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreAdminPassword value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:Prefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrUrl value to SXASingleDeveloper config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXA:DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"DownloadLocations\",");
            file.WriteLine("            \"Description\": \"Override to pass DownloadLocations value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PackagesTempLocation\",");
            file.WriteLine("            \"Description\": \"Override to pass PackagesTempLocation name value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SPE:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SPEPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreSiteName value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SPE:DownloadLocations\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"DownloadLocations\",");
            file.WriteLine("            \"Description\": \"Override to pass DownloadLocations value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:PackagesTempLocation\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"PackagesTempLocation\",");
            file.WriteLine("            \"Description\": \"Override to pass PackagesTempLocation name value to SPE config.\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Variables\": {");
            file.WriteLine("        \"SqlProcessingPools.Password\": \"[if(variable('Test.SqlProcessingPools.Password'),variable('Generate.SqlProcessingPools.Password'),parameter('SqlProcessingPoolsPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingPools.Password\": \"[variable('SqlProcessingPools.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingPools.Password\": \"[equal(parameter('SqlProcessingPoolsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingPools.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCollection.Password\": \"[if(variable('Test.SqlCollection.Password'),variable('Generate.SqlCollection.Password'),parameter('SqlCollectionPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlCollection.Password\": \"[variable('SqlCollection.Password')]\",");
            file.WriteLine("        \"Test.SqlCollection.Password\": \"[equal(parameter('SqlCollectionPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCollection.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReferenceData.Password\": \"[if(variable('Test.SqlReferenceData.Password'),variable('Generate.SqlReferenceData.Password'),parameter('SqlReferenceDataPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReferenceData.Password\": \"[variable('SqlReferenceData.Password')]\",");
            file.WriteLine("        \"Test.SqlReferenceData.Password\": \"[equal(parameter('SqlReferenceDataPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReferenceData.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMarketingAutomation.Password\": \"[if(variable('Test.SqlMarketingAutomation.Password'),variable('Generate.SqlMarketingAutomation.Password'),parameter('SqlMarketingAutomationPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMarketingAutomation.Password\": \"[variable('SqlMarketingAutomation.Password')]\",");
            file.WriteLine("        \"Test.SqlMarketingAutomation.Password\": \"[equal(parameter('SqlMarketingAutomationPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMarketingAutomation.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMessaging.Password\": \"[if(variable('Test.SqlMessaging.Password'),variable('Generate.SqlMessaging.Password'),parameter('SqlMessagingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMessaging.Password\": \"[variable('SqlMessaging.Password')]\",");
            file.WriteLine("        \"Test.SqlMessaging.Password\": \"[equal(parameter('SqlMessagingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMessaging.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingEngine.Password\": \"[if(variable('Test.SqlProcessingEngine.Password'),variable('Generate.SqlProcessingEngine.Password'),parameter('SqlProcessingEnginePassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlProcessingEngine.Password\": \"[variable('SqlProcessingEngine.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingEngine.Password\": \"[equal(parameter('SqlProcessingEnginePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingEngine.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlReporting.Password\": \"[if(variable('Test.SqlReporting.Password'),variable('Generate.SqlReporting.Password'),parameter('SqlReportingPassword'))]\",");
            file.WriteLine("        \"XConnectXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"SitecoreXP0:SqlReporting.Password\": \"[variable('SqlReporting.Password')]\",");
            file.WriteLine("        \"Test.SqlReporting.Password\": \"[equal(parameter('SqlReportingPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlReporting.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlCore.Password\": \"[if(variable('Test.SqlCore.Password'),variable('Generate.SqlCore.Password'),parameter('SqlCorePassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlCore.Password\": \"[variable('SqlCore.Password')]\",");
            file.WriteLine("        \"Test.SqlCore.Password\": \"[equal(parameter('SqlCorePassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlCore.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlSecurity.Password\": \"[if(variable('Test.SqlSecurity.Password'),variable('Generate.SqlSecurity.Password'),parameter('SqlSecurityPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlSecurity.Password\": \"[variable('SqlSecurity.Password')]\",");
            file.WriteLine("        \"IdentityServer:Sql.Database.Password\": \"[variable('SqlSecurity.Password')]\",");
            file.WriteLine("        \"Test.SqlSecurity.Password\": \"[equal(parameter('SqlSecurityPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlSecurity.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlMaster.Password\": \"[if(variable('Test.SqlMaster.Password'),variable('Generate.SqlMaster.Password'),parameter('SqlMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlMaster.Password\": \"[variable('SqlMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlMaster.Password\": \"[equal(parameter('SqlMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlWeb.Password\": \"[if(variable('Test.SqlWeb.Password'),variable('Generate.SqlWeb.Password'),parameter('SqlWebPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlWeb.Password\": \"[variable('SqlWeb.Password')]\",");
            file.WriteLine("        \"Test.SqlWeb.Password\": \"[equal(parameter('SqlWebPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlWeb.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlProcessingTasks.Password\": \"[if(variable('Test.SqlProcessingTasks.Password'),variable('Generate.SqlProcessingTasks.Password'),parameter('SqlProcessingTasksPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlProcessingTasks.Password\": \"[variable('SqlProcessingTasks.Password')]\",");
            file.WriteLine("        \"Test.SqlProcessingTasks.Password\": \"[equal(parameter('SqlProcessingTasksPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlProcessingTasks.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlForms.Password\": \"[if(variable('Test.SqlForms.Password'),variable('Generate.SqlForms.Password'),parameter('SqlFormsPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlForms.Password\": \"[variable('SqlForms.Password')]\",");
            file.WriteLine("        \"Test.SqlForms.Password\": \"[equal(parameter('SqlFormsPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlForms.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"SqlExmMaster.Password\": \"[if(variable('Test.SqlExmMaster.Password'),variable('Generate.SqlExmMaster.Password'),parameter('SqlExmMasterPassword'))]\",");
            file.WriteLine("        \"SitecoreXP0:SqlExmMaster.Password\": \"[variable('SqlExmMaster.Password')]\",");
            file.WriteLine("        \"Test.SqlExmMaster.Password\": \"[equal(parameter('SqlExmMasterPassword'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.SqlExmMaster.Password\": \"[randomstring(Length:20,DisAllowSpecial:True,EnforceComplexity:True)]\",");
            file.WriteLine("        \"Client.Secret\" : \"[if(variable('Test.Client.Secret'),variable('Generate.Client.Secret'),parameter('ClientSecret'))]\",");
            file.WriteLine("        \"IdentityServer:Client.Secret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"SitecoreXP0:Sitecore.IdentitySecret\": \"[variable('Client.Secret')]\",");
            file.WriteLine("        \"Test.Client.Secret\": \"[equal(parameter('ClientSecret'),'SIF-Default')]\",");
            file.WriteLine("        \"Generate.Client.Secret\": \"[randomstring(Length:100,DisAllowSpecial:True)]\"");
            file.WriteLine("    },");
            file.WriteLine("    \"Includes\": {        ");
            file.WriteLine("		\"IdentityServerCertificates\":{");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"IdentityServer\": {");
            file.WriteLine("            \"Source\": \".\\\\identityServer.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectCertificates\": {");
            file.WriteLine("            \"Source\": \".\\\\createcert.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"XConnectXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\xconnect-xp0.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreSolr\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreXP0\": {");
            file.WriteLine("            \"Source\": \".\\\\Sitecore-XP0.json\"");
            file.WriteLine("        },");
            file.WriteLine("		\"SXASingleDeveloper\": {");
            file.WriteLine("            \"Source\": \".\\\\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SXA-SingleDeveloper" + ".json\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Register\": {");
            file.WriteLine("        \"Tasks\": {");
            file.WriteLine("            \"SetVariable\": \"Set-Variable\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Tasks\": {");
            file.WriteLine("        \"GeneratePasswords\": {");
            file.WriteLine("            \"Description\": \"Generates all shared passwords and secrets.\",");
            file.WriteLine("            \"Type\": \"SetVariable\",");
            file.WriteLine("            \"Params\": {");
            file.WriteLine("                \"Name\": \"XP0Passwords\",");
            file.WriteLine("                \"Scope\": \"Global\",");
            file.WriteLine("                \"Value\": [");
            file.WriteLine("                    {\"SqlProcessingPoolsPassword\":     \"[variable('SqlProcessingPools.Password')]\"},");
            file.WriteLine("                    {\"SqlCollectionPassword\":          \"[variable('SqlCollection.Password')]\"},");
            file.WriteLine("                    {\"SqlReferenceDataPassword\":       \"[variable('SqlReferenceData.Password')]\"},");
            file.WriteLine("                    {\"SqlMarketingAutomationPassword\": \"[variable('SqlMarketingAutomation.Password')]\"},");
            file.WriteLine("                    {\"SqlMessagingPassword\":           \"[variable('SqlMessaging.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingEnginePassword\":    \"[variable('SqlProcessingEngine.Password')]\"},");
            file.WriteLine("                    {\"SqlReportingPassword\":           \"[variable('SqlReporting.Password')]\"},");
            file.WriteLine("                    {\"SqlCorePassword\":                \"[variable('SqlCore.Password')]\"},");
            file.WriteLine("                    {\"SqlSecurityPassword\":            \"[variable('SqlSecurity.Password')]\"},");
            file.WriteLine("                    {\"SqlMasterPassword\":              \"[variable('SqlMaster.Password')]\"},");
            file.WriteLine("                    {\"SqlWebPassword\":                 \"[variable('SqlWeb.Password')]\"},");
            file.WriteLine("                    {\"SqlProcessingTasksPassword\":     \"[variable('SqlProcessingTasks.Password')]\"},");
            file.WriteLine("                    {\"SqlFormsPassword\":               \"[variable('SqlForms.Password')]\"},");
            file.WriteLine("                    {\"SqlExmMasterPassword\":           \"[variable('SqlExmMaster.Password')]\"},");
            file.WriteLine("                    {\"ClientSecret\":                   \"[variable('Client.Secret')]\"}");
            file.WriteLine("                ]");
            file.WriteLine("            }");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");
            file.WriteLine();
            file.Dispose();

        }

        void WriteSingleDeveloperPSFile(string path, bool uninstallscript)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("# The Prefix that will be used on SOLR, Website and Database instances.");
            file.WriteLine("$Prefix = \"" + txtSiteNamePrefix.Text + "\"");
            file.WriteLine("# The Password for the Sitecore Admin User. This will be regenerated if left on the default.");
            file.WriteLine("$SitecoreAdminPassword = \"" + txtSitecoreUserPassword.Text + "\"");
            file.WriteLine("# The root folder with the license file and WDP files.");
            file.WriteLine("$SCInstallRoot = \".\"");
            file.WriteLine("# The name for the XConnect service.");
            file.WriteLine("$XConnectSiteName = \"$prefix.xconnect\"");
            file.WriteLine("# The Sitecore site instance name.");
            file.WriteLine("$SitecoreSiteName = \"" + txtSiteName.Text + "\"");
            file.WriteLine("# Identity Server site name");
            file.WriteLine("$IdentityServerSiteName = \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("# The Path to the license file");
            file.WriteLine("$LicenseFile = \"$SCInstallRoot\\license.xml\"");
            file.WriteLine("# The URL of the Solr Server");
            file.WriteLine("$SolrUrl = \"" + txtSolrUrl.Text + "\"");
            file.WriteLine("# The Folder that Solr has been installed to.");
            file.WriteLine("$SolrRoot = \"" + txtSolrRoot.Text + "\"");
            file.WriteLine("# The Name of the Solr Service.");
            file.WriteLine("$SolrService = \"" + txtSolrService.Text + "\"");
            file.WriteLine("# The DNS name or IP of the SQL Instance.");
            file.WriteLine("$SqlServer = \"" + serverName + "\"");
            file.WriteLine("# A SQL user with sysadmin privileges.");
            file.WriteLine("$SqlAdminUser = \"" + txtSqlUser.Text + "\"");
            file.WriteLine("# The password for $SQLAdminUser.");
            file.WriteLine("$SqlAdminPassword = \"" + txtSqlPass.Text + "\"");
            file.WriteLine("# The path to the XConnect Package to Deploy.");
            file.WriteLine("$XConnectPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore 9* rev. * (OnPrem)_xp0xconnect.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Sitecore Package to Deploy.");
            file.WriteLine("$SitecorePackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore 9* rev. * (OnPrem)_single.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Identity Server Package to Deploy.");
            file.WriteLine("$IdentityServerPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore.IdentityServer * rev. * (OnPrem)_identityserver.scwdp.zip\").FullName");
            file.WriteLine("# The Identity Server password recovery URL, this should be the URL of the CM Instance");
            file.WriteLine("$PasswordRecoveryUrl = \"http://$SitecoreSiteName\"");
            file.WriteLine("# The URL of the Identity Server");
            file.WriteLine("$SitecoreIdentityAuthority = \"https://$IdentityServerSiteName\"");
            file.WriteLine("# The URL of the XconnectService");
            file.WriteLine("$XConnectCollectionService = \"https://$XConnectSiteName\"");
            file.WriteLine("# The random string key used for establishing connection with IdentityService. This will be regenerated if left on the default.");
            file.WriteLine("$ClientSecret = \"" + txtClientSecret.Text + "\"");
            file.WriteLine("# Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.");
            file.WriteLine("$AllowedCorsOrigins = \"http://$SitecoreSiteName\"");
            file.WriteLine("# Install XP0 via combined partials file.");
            file.WriteLine("$singleDeveloperParams = @{");
            file.WriteLine("    Path = \"$SCInstallRoot\\" + SCIASettings.FilePrefixAppString  + txtSiteName.Text + "-SingleDeveloper.json\"");
            file.WriteLine("    SqlServer = $SqlServer");
            file.WriteLine("    SqlAdminUser = $SqlAdminUser");
            file.WriteLine("    SqlAdminPassword = $SqlAdminPassword");
            file.WriteLine("    SitecoreAdminPassword = $SitecoreAdminPassword");
            file.WriteLine("    SolrUrl = $SolrUrl");
            file.WriteLine("    SolrRoot = $SolrRoot");
            file.WriteLine("    SolrService = $SolrService");
            file.WriteLine("    Prefix = $Prefix");
            file.WriteLine("    XConnectCertificateName = $XConnectSiteName");
            file.WriteLine("    IdentityServerCertificateName = $IdentityServerSiteName");
            file.WriteLine("    IdentityServerSiteName = $IdentityServerSiteName");
            file.WriteLine("    LicenseFile = $LicenseFile");
            file.WriteLine("    XConnectPackage = $XConnectPackage");
            file.WriteLine("    SitecorePackage = $SitecorePackage");
            file.WriteLine("    IdentityServerPackage = $IdentityServerPackage");
            file.WriteLine("    XConnectSiteName = $XConnectSiteName");
            file.WriteLine("    SitecoreSitename = $SitecoreSiteName");
            file.WriteLine("    PasswordRecoveryUrl = $PasswordRecoveryUrl");
            file.WriteLine("    SitecoreIdentityAuthority = $SitecoreIdentityAuthority");
            file.WriteLine("    XConnectCollectionService = $XConnectCollectionService");
            file.WriteLine("    ClientSecret = $ClientSecret");
            file.WriteLine("    AllowedCorsOrigins = $AllowedCorsOrigins");
            file.WriteLine("}");
            file.WriteLine("Push-Location $SCInstallRoot");
            if (!uninstallscript)
            {
                file.WriteLine("Install-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper.log");
            }
            else
            {
                file.WriteLine("Uninstall-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper-Uninstall.log");
            }
            file.WriteLine("Pop-Location");
            file.WriteLine("# SIG # Begin signature block");
            file.WriteLine("# MIIXwQYJKoZIhvcNAQcCoIIXsjCCF64CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB");
            file.WriteLine("# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR");
            file.WriteLine("# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUScLKS+boG7s0kPExqeNY6Fzk");
            file.WriteLine("# 3EigghL8MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B");
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
            file.WriteLine("# MAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFBbZmYns3wAqXpyM6zahBmwG");
            file.WriteLine("# RnJCMA0GCSqGSIb3DQEBAQUABIIBAFqYc0n7BW8bMITJpldggZepJh2J2nMFbyIv");
            file.WriteLine("# fwW0cbEZZuXnrcdNVDTfZSN4C7N0RodSthbbE4iU55ZMwW4FUfdCVgDrcM5AoFe1");
            file.WriteLine("# ROKYhvtYJ6iEzN902gGEISv3vS17gw5VtpVmRgLkeFWB49HXTGN549LedK+tBq2K");
            file.WriteLine("# KHhQhX1LpYtxv2zeTo6p1+nGVjcq7RKX6W1s7iSuwWOBsoonDYuVa+/3LFG+jEDD");
            file.WriteLine("# OtYCXvo0z+3aPvtJf7mSBBRKbdImYqOzd6vyPthCIRPn8OpGM1YCfipW34+8Npeq");
            file.WriteLine("# Q+nLVSXNo+IuhncTLdx4/FeFNwbOTDTfJFcvesVcpO+XHNvjTOOhggILMIICBwYJ");
            file.WriteLine("# KoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQGEwJVUzEdMBsGA1UEChMU");
            file.WriteLine("# U3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFudGVjIFRpbWUgU3Rh");
            file.WriteLine("# bXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVuBNhqmBsaUDAJBgUrDgMC");
            file.WriteLine("# GgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkqhkiG9w0BCQUxDxcN");
            file.WriteLine("# MTkwMzIxMTQwNjQzWjAjBgkqhkiG9w0BCQQxFgQUhjBEJ3GAEFm272HQco0GuWUH");
            file.WriteLine("# 3l8wDQYJKoZIhvcNAQEBBQAEggEAO4BBRwat/GZ31DaXAOP+/YVdHuwdXhszcdLn");
            file.WriteLine("# nxAiC24pm3DoHuSIr8pRThzuDgcld6XUF6UDOPlkVBNWMy5FgvlltXts2lJsrvg7");
            file.WriteLine("# 8aBkGUCYvDFGZeWvhgEua/d1zQVMWscQWRdcxy/7bZ19jIr0gV/5zZCAUXBSJZxC");
            file.WriteLine("# j6mcEP0HypUNCYlWAVImC+xwsikiSbjFRcKBxQCHueB5akLkjnaBQtElow4MymH5");
            file.WriteLine("# Wx3ZZZuvUL705HGGxQ1iocNLzLSS9gs7BKXm9f0U7v/lXgW7M3r72CFEhcCH+soa");
            file.WriteLine("# 3Qg3sHqSKEdG0xf1sqRwRE7k6hdkcpF0csEN3EZ35pEj66hehw==");
            file.WriteLine("# SIG # End signature block");
            file.Dispose();
        }

        void WriteFile(string path, bool uninstallscript)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("# The Prefix that will be used on SOLR, Website and Database instances.");
            file.WriteLine("$Prefix = \"" + txtSiteNamePrefix.Text + "\"");
            file.WriteLine("# The Password for the Sitecore Admin User. This will be regenerated if left on the default.");
            file.WriteLine("$SitecoreAdminPassword = \"" + txtSitecoreUserPassword.Text + "\"");
            file.WriteLine("# The root folder with the license file and WDP files.");
            file.WriteLine("$SCInstallRoot = \".\"");
            file.WriteLine("# Root folder to install the site to. If left on the default [systemdrive]:\\inetpub\\wwwroot will be used");
            file.WriteLine("$SitePhysicalRoot = \"" + txtSXAInstallDir.Text + "\"");
            file.WriteLine("# The name for the XConnect service.");
            file.WriteLine("$XConnectSiteName = \"" +  txtxConnectSiteName.Text + "\"");
            file.WriteLine("# The Sitecore site instance name.");
            file.WriteLine("$SitecoreSiteName = \"" + txtSiteName.Text + "\"");
            file.WriteLine("# Identity Server site name");
            file.WriteLine("$IdentityServerSiteName = \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("# The Path to the license file");
            file.WriteLine("$LicenseFile = \"$SCInstallRoot\\license.xml\"");
            file.WriteLine("# The URL of the Solr Server");
            file.WriteLine("$SolrUrl = \"" + txtSolrUrl.Text  + "\"");
            file.WriteLine("# The Folder that Solr has been installed to.");
            file.WriteLine("$SolrRoot = \"" + txtSolrRoot.Text  + "\"");
            file.WriteLine("# The Name of the Solr Service.");
            file.WriteLine("$SolrService = \"" + txtSolrService.Text + "\"");
            file.WriteLine("# The DNS name or IP of the SQL Instance.");
            file.WriteLine("$SqlServer = \"" + txtSqlDbServer.Text + "\"");
            file.WriteLine("# A SQL user with sysadmin privileges.");
            file.WriteLine("$SqlAdminUser = \"" + txtSqlUser.Text + "\"");
            file.WriteLine("# The password for $SQLAdminUser.");
            file.WriteLine("$SqlAdminPassword = \"" + txtSqlPass.Text + "\"");
            file.WriteLine("# The path to the XConnect Package to Deploy.");
            file.WriteLine("$XConnectPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore * rev. * (OnPrem)_xp0xconnect.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Sitecore Package to Deploy.");
            file.WriteLine("$SitecorePackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore * rev. * (OnPrem)_single.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Identity Server Package to Deploy.");
            file.WriteLine("$IdentityServerPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore.IdentityServer * rev. * (OnPrem)_identityserver.scwdp.zip\").FullName");
            file.WriteLine("# The Identity Server password recovery URL, this should be the URL of the CM Instance");
            file.WriteLine("$PasswordRecoveryUrl = \"https://$SitecoreSiteName\"");
            file.WriteLine("# The URL of the Identity Server");
            file.WriteLine("$SitecoreIdentityAuthority = \"https://$IdentityServerSiteName\"");
            file.WriteLine("# The URL of the XconnectService");
            file.WriteLine("$XConnectCollectionService = \"https://$XConnectSiteName\"");
            file.WriteLine("# The random string key used for establishing connection with IdentityService. This will be regenerated if left on the default.");
            file.WriteLine("$ClientSecret = \"" + txtClientSecret.Text + "\"");
            file.WriteLine("# Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.");
            file.WriteLine("$AllowedCorsOrigins = \"https://$SitecoreSiteName\"");
            file.WriteLine("$SXAPackage =  (Get-ChildItem \"$SCInstallRoot\\Sitecore Experience Accelerator*.scwdp.zip\").FullName");
            file.WriteLine("$SPEPackage =  (Get-ChildItem \"$SCInstallRoot\\Sitecore.PowerShell.Extensions*.scwdp.zip\").FullName");
            file.WriteLine("# Install XP0 via combined partials file.");
            file.WriteLine("$singleDeveloperParams = @{");
            file.WriteLine("    Path = \"$SCInstallRoot\\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SingleDeveloperwithSXA.json\"");
            file.WriteLine("    SqlServer = $SqlServer");
            file.WriteLine("    SqlAdminUser = $SqlAdminUser");
            file.WriteLine("    SqlAdminPassword = $SqlAdminPassword");
            file.WriteLine("    SitecoreAdminPassword = $SitecoreAdminPassword");
            file.WriteLine("    SolrUrl = $SolrUrl");
            file.WriteLine("    SolrRoot = $SolrRoot");
            file.WriteLine("    SolrService = $SolrService");
            file.WriteLine("    Prefix = $Prefix");
            file.WriteLine("    XConnectCertificateName = $XConnectSiteName");
            file.WriteLine("    IdentityServerCertificateName = $IdentityServerSiteName");
            file.WriteLine("    IdentityServerSiteName = $IdentityServerSiteName");
            file.WriteLine("    LicenseFile = $LicenseFile");
            file.WriteLine("    XConnectPackage = $XConnectPackage");
            file.WriteLine("    SitecorePackage = $SitecorePackage");
            file.WriteLine("    IdentityServerPackage = $IdentityServerPackage");
            file.WriteLine("    XConnectSiteName = $XConnectSiteName");
            file.WriteLine("    SitecoreSitename = $SitecoreSiteName");
            file.WriteLine("    PasswordRecoveryUrl = $PasswordRecoveryUrl");
            file.WriteLine("    SitecoreIdentityAuthority = $SitecoreIdentityAuthority");
            file.WriteLine("    XConnectCollectionService = $XConnectCollectionService");
            file.WriteLine("    ClientSecret = $ClientSecret");
            file.WriteLine("    AllowedCorsOrigins = $AllowedCorsOrigins");
            file.WriteLine("    SitePhysicalRoot = $SitePhysicalRoot");
            file.WriteLine("    SXAPackage = $SXAPackage");
            file.WriteLine("	SPEPackage = $SPEPackage");
            file.WriteLine("}");
            file.WriteLine("Push-Location $SCInstallRoot");
            if (!uninstallscript)
            {
                file.WriteLine(" Install-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper.log");
            }
            if (uninstallscript)
            {
                file.WriteLine(" Uninstall-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper-Uninstall.log");
            }

            file.WriteLine("Pop-Location");
            file.WriteLine("# SIG # Begin signature block");
            file.WriteLine("# MIIXwQYJKoZIhvcNAQcCoIIXsjCCF64CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB");
            file.WriteLine("# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR");
            file.WriteLine("# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUeGAz+NczayJIZTbPRKZxRFAQ");
            file.WriteLine("# Y8GgghL8MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B");
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
            file.WriteLine("# MAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFIuO9OxnfSZtMu1Iow+a63pL");
            file.WriteLine("# 14sSMA0GCSqGSIb3DQEBAQUABIIBAKiCspaxn7bSZG+YXo2jwHVVBXA08m/8Obhs");
            file.WriteLine("# TjBKVAgNgdZYQ9/tadnpabiXhf/ul35RyRRI1SaiWr+DBONxND7sW5Czn7JiPt5z");
            file.WriteLine("# ay+XUHsNSu6qFx+H3L0yAgFGfl/wlam5AkeitR6gwxBplIP/dH15rniJb3CaBHaN");
            file.WriteLine("# kxfs8MIGmzUCugc45RvRaXxkmWX8vCfpt1X5YWNu1vHQG0LBK6xP4kpNWR+5UVh/");
            file.WriteLine("# HbfxisjgMrNHLlYNMJgSKNftOpF90fgK14zINHYOZHFWgewAtf5h9fC/ExEQjXq1");
            file.WriteLine("# KeyuM0kW7oHzlvrvqt+HB9IVwANI+dub/eS3vHj/JV1GR2BjxnihggILMIICBwYJ");
            file.WriteLine("# KoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQGEwJVUzEdMBsGA1UEChMU");
            file.WriteLine("# U3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFudGVjIFRpbWUgU3Rh");
            file.WriteLine("# bXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVuBNhqmBsaUDAJBgUrDgMC");
            file.WriteLine("# GgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkqhkiG9w0BCQUxDxcN");
            file.WriteLine("# MjAwNTIwMTM0MzE2WjAjBgkqhkiG9w0BCQQxFgQUNHljh0VLrrH4wz85kVzfGbww");
            file.WriteLine("# uXIwDQYJKoZIhvcNAQEBBQAEggEAjDClMBinMY2uBcca2tUcb2+Pe4QQyDq/A6Te");
            file.WriteLine("# Hsv7VqLxrbY7Ax+Nm3UGkFn44rRgPxF53Zueujcbf9dgzWpWLwrW1aedybLvkywy");
            file.WriteLine("# Rvi4u7qCNWhjmZuqolklTN6b186G8Mpfjqcf2LmyvD8/qEYyUdLus3I9K9KrOttb");
            file.WriteLine("# cPJOJiD/T/RGTIBUa/TSi2xgCI7QeV6TABgUUFhU70IYWq9BYRvqeynQ70nt/xDU");
            file.WriteLine("# BwXcJgYce7RKNXuSRAU3SKLDfk6mRFzVmV7YTAYr1k3+SeivQ4avAEp429X582ks");
            file.WriteLine("# ILaKCLi1dRTARndiAs0Sy1n3AiL1/Lr+WU5z/19aoWGZThacKg==");
            file.WriteLine("# SIG # End signature block");

            file.WriteLine();
            file.Dispose();
        }

        void Write92File(string path, bool uninstallscript)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("# The Prefix that will be used on SOLR, Website and Database instances.");
            file.WriteLine("$Prefix = \"" + txtSiteNamePrefix.Text + "\"");
            file.WriteLine("# The Password for the Sitecore Admin User. This will be regenerated if left on the default.");
            file.WriteLine("$SitecoreAdminPassword = \"" + txtSitecoreUserPassword.Text + "\"");
            file.WriteLine("# The root folder with the license file and WDP files.");
            file.WriteLine("$SCInstallRoot = \".\"");
            file.WriteLine("# Root folder to install the site to. If left on the default [systemdrive]:\\inetpub\\wwwroot will be used");
            file.WriteLine("$SitePhysicalRoot = \"" + txtSXAInstallDir.Text + "\"");
            file.WriteLine("# The name for the XConnect service.");
            file.WriteLine("$XConnectSiteName = \"" + txtxConnectSiteName.Text + "\"");
            file.WriteLine("# The Sitecore site instance name.");
            file.WriteLine("$SitecoreSiteName = \"" + txtSiteName.Text + "\"");
            file.WriteLine("# Identity Server site name");
            file.WriteLine("$IdentityServerSiteName = \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("# The Path to the license file");
            file.WriteLine("$LicenseFile = \"$SCInstallRoot\\license.xml\"");
            file.WriteLine("# The URL of the Solr Server");
            file.WriteLine("$SolrUrl = \"" + txtSolrUrl.Text + "\"");
            file.WriteLine("# The Folder that Solr has been installed to.");
            file.WriteLine("$SolrRoot = \"" + txtSolrRoot.Text + "\"");
            file.WriteLine("# The Name of the Solr Service.");
            file.WriteLine("$SolrService = \"" + txtSolrService.Text + "\"");
            file.WriteLine("# The DNS name or IP of the SQL Instance.");
            file.WriteLine("$SqlServer = \"" + txtSqlDbServer.Text + "\"");
            file.WriteLine("# A SQL user with sysadmin privileges.");
            file.WriteLine("$SqlAdminUser = \"" + txtSqlUser.Text + "\"");
            file.WriteLine("# The password for $SQLAdminUser.");
            file.WriteLine("$SqlAdminPassword = \"" + txtSqlPass.Text + "\"");
            file.WriteLine("# The path to the XConnect Package to Deploy.");
            file.WriteLine("$XConnectPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore * rev. * (OnPrem)_xp0xconnect.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Sitecore Package to Deploy.");
            file.WriteLine("$SitecorePackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore * rev. * (OnPrem)_single.scwdp.zip\").FullName");
            file.WriteLine("# The path to the Identity Server Package to Deploy.");
            file.WriteLine("$IdentityServerPackage = (Get-ChildItem \"$SCInstallRoot\\Sitecore.IdentityServer * rev. * (OnPrem)_identityserver.scwdp.zip\").FullName");
            file.WriteLine("# The Identity Server password recovery URL, this should be the URL of the CM Instance");
            file.WriteLine("$PasswordRecoveryUrl = \"https://$SitecoreSiteName\"");
            file.WriteLine("# The URL of the Identity Server");
            file.WriteLine("$SitecoreIdentityAuthority = \"https://$IdentityServerSiteName\"");
            file.WriteLine("# The URL of the XconnectService");
            file.WriteLine("$XConnectCollectionService = \"https://$XConnectSiteName\"");
            file.WriteLine("# The random string key used for establishing connection with IdentityService. This will be regenerated if left on the default.");
            file.WriteLine("$ClientSecret = \"" + txtClientSecret.Text + "\"");
            file.WriteLine("# Pipe-separated list of instances (URIs) that are allowed to login via Sitecore Identity.");
            file.WriteLine("$AllowedCorsOrigins = \"https://$SitecoreSiteName\"");
            file.WriteLine("# Install XP0 via combined partials file.");
            file.WriteLine("$singleDeveloperParams = @{");
            file.WriteLine("    Path = \"$SCInstallRoot\\" + txtSiteNamePrefix.Text + "-SingleDeveloper.json\"");
            file.WriteLine("    SqlServer = $SqlServer");
            file.WriteLine("    SqlAdminUser = $SqlAdminUser");
            file.WriteLine("    SqlAdminPassword = $SqlAdminPassword");
            file.WriteLine("    SitecoreAdminPassword = $SitecoreAdminPassword");
            file.WriteLine("    SolrUrl = $SolrUrl");
            file.WriteLine("    SolrRoot = $SolrRoot");
            file.WriteLine("    SolrService = $SolrService");
            file.WriteLine("    Prefix = $Prefix");
            file.WriteLine("    XConnectCertificateName = $XConnectSiteName");
            file.WriteLine("    IdentityServerCertificateName = $IdentityServerSiteName");
            file.WriteLine("    IdentityServerSiteName = $IdentityServerSiteName");
            file.WriteLine("    LicenseFile = $LicenseFile");
            file.WriteLine("    XConnectPackage = $XConnectPackage");
            file.WriteLine("    SitecorePackage = $SitecorePackage");
            file.WriteLine("    IdentityServerPackage = $IdentityServerPackage");
            file.WriteLine("    XConnectSiteName = $XConnectSiteName");
            file.WriteLine("    SitecoreSitename = $SitecoreSiteName");
            file.WriteLine("    PasswordRecoveryUrl = $PasswordRecoveryUrl");
            file.WriteLine("    SitecoreIdentityAuthority = $SitecoreIdentityAuthority");
            file.WriteLine("    XConnectCollectionService = $XConnectCollectionService");
            file.WriteLine("    ClientSecret = $ClientSecret");
            file.WriteLine("    AllowedCorsOrigins = $AllowedCorsOrigins");
            file.WriteLine("}");
            file.WriteLine("Push-Location $SCInstallRoot");
            if (!uninstallscript)
            {
                file.WriteLine(" Install-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper.log");
            }
            if (uninstallscript)
            {
                file.WriteLine(" Uninstall-SitecoreConfiguration @singleDeveloperParams *>&1 | Tee-Object XP0-SingleDeveloper-Uninstall.log");
            }

            file.WriteLine("Pop-Location");
            file.WriteLine("# SIG # Begin signature block");
            file.WriteLine("# MIIXwQYJKoZIhvcNAQcCoIIXsjCCF64CAQExCzAJBgUrDgMCGgUAMGkGCisGAQQB");
            file.WriteLine("# gjcCAQSgWzBZMDQGCisGAQQBgjcCAR4wJgIDAQAABBAfzDtgWUsITrck0sYpfvNR");
            file.WriteLine("# AgEAAgEAAgEAAgEAAgEAMCEwCQYFKw4DAhoFAAQUeGAz+NczayJIZTbPRKZxRFAQ");
            file.WriteLine("# Y8GgghL8MIID7jCCA1egAwIBAgIQfpPr+3zGTlnqS5p31Ab8OzANBgkqhkiG9w0B");
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
            file.WriteLine("# MAwGCisGAQQBgjcCARUwIwYJKoZIhvcNAQkEMRYEFIuO9OxnfSZtMu1Iow+a63pL");
            file.WriteLine("# 14sSMA0GCSqGSIb3DQEBAQUABIIBAKiCspaxn7bSZG+YXo2jwHVVBXA08m/8Obhs");
            file.WriteLine("# TjBKVAgNgdZYQ9/tadnpabiXhf/ul35RyRRI1SaiWr+DBONxND7sW5Czn7JiPt5z");
            file.WriteLine("# ay+XUHsNSu6qFx+H3L0yAgFGfl/wlam5AkeitR6gwxBplIP/dH15rniJb3CaBHaN");
            file.WriteLine("# kxfs8MIGmzUCugc45RvRaXxkmWX8vCfpt1X5YWNu1vHQG0LBK6xP4kpNWR+5UVh/");
            file.WriteLine("# HbfxisjgMrNHLlYNMJgSKNftOpF90fgK14zINHYOZHFWgewAtf5h9fC/ExEQjXq1");
            file.WriteLine("# KeyuM0kW7oHzlvrvqt+HB9IVwANI+dub/eS3vHj/JV1GR2BjxnihggILMIICBwYJ");
            file.WriteLine("# KoZIhvcNAQkGMYIB+DCCAfQCAQEwcjBeMQswCQYDVQQGEwJVUzEdMBsGA1UEChMU");
            file.WriteLine("# U3ltYW50ZWMgQ29ycG9yYXRpb24xMDAuBgNVBAMTJ1N5bWFudGVjIFRpbWUgU3Rh");
            file.WriteLine("# bXBpbmcgU2VydmljZXMgQ0EgLSBHMgIQDs/0OMj+vzVuBNhqmBsaUDAJBgUrDgMC");
            file.WriteLine("# GgUAoF0wGAYJKoZIhvcNAQkDMQsGCSqGSIb3DQEHATAcBgkqhkiG9w0BCQUxDxcN");
            file.WriteLine("# MjAwNTIwMTM0MzE2WjAjBgkqhkiG9w0BCQQxFgQUNHljh0VLrrH4wz85kVzfGbww");
            file.WriteLine("# uXIwDQYJKoZIhvcNAQEBBQAEggEAjDClMBinMY2uBcca2tUcb2+Pe4QQyDq/A6Te");
            file.WriteLine("# Hsv7VqLxrbY7Ax+Nm3UGkFn44rRgPxF53Zueujcbf9dgzWpWLwrW1aedybLvkywy");
            file.WriteLine("# Rvi4u7qCNWhjmZuqolklTN6b186G8Mpfjqcf2LmyvD8/qEYyUdLus3I9K9KrOttb");
            file.WriteLine("# cPJOJiD/T/RGTIBUa/TSi2xgCI7QeV6TABgUUFhU70IYWq9BYRvqeynQ70nt/xDU");
            file.WriteLine("# BwXcJgYce7RKNXuSRAU3SKLDfk6mRFzVmV7YTAYr1k3+SeivQ4avAEp429X582ks");
            file.WriteLine("# ILaKCLi1dRTARndiAs0Sy1n3AiL1/Lr+WU5z/19aoWGZThacKg==");
            file.WriteLine("# SIG # End signature block");

            file.WriteLine();
            file.Dispose();
        }

        private void WriteSiteSingleDeveloperJsonFile(string path)
        {
            using var file = new StreamWriter(path);
            string serverName = txtSqlDbServer.Text.Replace("\\", "\\\\");
            file.WriteLine("{");
            file.WriteLine("    \"Parameters\": {");
            file.WriteLine("        \"SiteName\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"Sitecore\",");
            file.WriteLine("            \"Description\": \"The name of the site to be deployed to.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Prefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Description\": \"The prefix for the Solr, SQL and Sitecore instances.\",");
            file.WriteLine("            \"DefaultValue\": \"\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"SIF-Default\",");
            file.WriteLine("            \"Description\": \"The admin password for the Sitecore instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"sa\",");
            file.WriteLine("            \"Description\": \"The Sql admin user account to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"string\",");
            file.WriteLine("            \"DefaultValue\": \"12345\",");
            file.WriteLine("            \"Description\": \"The Sql admin password to use when installing databases.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"" + serverName + "\",");
            file.WriteLine("            \"Description\": \"The Sql Server where databases will be installed.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"https://localhost:8983/solr\",");
            file.WriteLine("            \"Description\": \"The Solr instance to use.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"C:\\\\solr-7.5.0\",");
            file.WriteLine("            \"Description\": \"The file path to the Solr instance.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"Solr-7.5.0\",");
            file.WriteLine("            \"Description\": \"The name of the Solr service.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXAPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore Experience Accelerator package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPEPackage\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"DefaultValue\": \"\",");
            file.WriteLine("            \"Description\": \"The path to the Sitecore Powershell Extensions package to deploy.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SiteName value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:SiteName\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SiteName\",");
            file.WriteLine("            \"Description\": \"Override to pass SiteName value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SXAPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SXAPackage value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:Package\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SPEPackage\",");
            file.WriteLine("            \"Description\": \"Override to pass SPEPackage value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SQLServer value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SQLServer\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SQLServer\",");
            file.WriteLine("            \"Description\": \"Override to pass SQLServer value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:SqlAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminPassword value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:SqlAdminUser\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SqlAdminUser\",");
            file.WriteLine("            \"Description\": \"Override to pass SqlAdminUser value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SPE:SqlDbPrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SPE config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SolrCorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA:SitecoreAdminPassword\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SitecoreAdminPassword\",");
            file.WriteLine("            \"Description\": \"Override to pass SitecoreAdminPassword value to SXA config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Solr:CorePrefix\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"Prefix\",");
            file.WriteLine("            \"Description\": \"Override to pass Prefix value to Solr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Solr:SolrUrl\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrUrl\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrUrl to Solr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Solr:SolrService\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrService\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrService to Solr config.\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Solr:SolrRoot\": {");
            file.WriteLine("            \"Type\": \"String\",");
            file.WriteLine("            \"Reference\": \"SolrRoot\",");
            file.WriteLine("            \"Description\": \"Override to pass SolrRoot to Solr config.\"");
            file.WriteLine("        }");
            file.WriteLine("    },");
            file.WriteLine("    \"Includes\": {");
            file.WriteLine("        \"SPE\":{");
            file.WriteLine("            \"Source\": \".\\\\spe.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"Solr\": {");
            file.WriteLine("            \"Source\": \".\\\\sxa-solr.json\"");
            file.WriteLine("        },");
            file.WriteLine("        \"SXA\": {");
            file.WriteLine("            \"Source\": \".\\\\sxa-XP0.json\"");
            file.WriteLine("        }");
            file.WriteLine("    }");
            file.WriteLine("}");

            file.WriteLine();
            file.Dispose();
        }


        private void btnGenerate_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            switch (Version.SitecoreVersion)
            {
                case "10.0.1":
                case "10.1.0":
                case "10.0":
                case "9.3":
                    WriteSiteSingleDeveloperJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SXA-SingleDeveloper.json");
                    WriteJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SingleDeveloperwithSXA.json");
                    WriteFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    WriteFile(@".\"  + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", true);
                    break;
                case "9.2":
                    Write92JsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + txtSiteNamePrefix.Text + "-SingleDeveloper.json");
                    Write92File(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    Write92File(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", true);
                    break;
                case "9.1":
                    WriteSingleDeveloperJsonFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "-SingleDeveloper.json");
                    WriteSingleDeveloperPSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    WriteSingleDeveloperPSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", false);
                    break;
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    WriteCreateCertJsonFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_xconnect-createcert.json");
                    Write90PSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    DeleteScript(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Delete_Script.ps1"); break;
                default:
                    break;
            }

            lblStatus.Text = "Scripts generated successfully....";
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
            btnAppSettings.Enabled = enabled;
            chkStepsList.Enabled = enabled;
            btnNext.Enabled = enabled;
            btnValidateAll.Enabled = enabled;
            btnPrerequisites.Enabled = enabled;
        }

        private void ToggleButtonControls(bool enabled)
        {
            btnInstall.Enabled = enabled;
            btnUninstall.Enabled = enabled;
            btnGenerate.Enabled = enabled;
        }

        private bool CheckSitecoreInstallDir()
        {
            if (Directory.Exists(txtSXAInstallDir.Text)) return true;

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
                    btnAppSettings.Enabled = true;
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
            if (!ValidateData(txtClientSecret, "Sitecore Client Secret", const_General_Tab)) return false;

            if (!ValidateData(txtSXAInstallDir, "Sitecore SXA Install Directory", const_Install_Details_Tab)) return false;
            if (!ValidateData(txtxConnectInstallDir, "Sitecore xConnect Install Directory", const_Install_Details_Tab)) return false;

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
            if (uninstall) { btnInstall.Enabled = false; } else { btnUninstall.Enabled = false; }

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
                sqlcommand.Parameters.AddWithValue("@XConnectInstallDir", txtxConnectInstallDir.Text);
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

        private bool SaveSIFDatatoDBSuccess(SqlConnection sqlConn)
        {
            try
            {

                var query = "delete from  SIF where SiteName='" + txtSiteName.Text + "'; INSERT INTO SIF(SiteNameSuffix ,[SiteNamePrefix],SiteName ,[IDServerSiteName], [SitecoreIdentityServerUrl] ,[SXAInstallDir] ,XConnectInstallDir ,ClientSecret  , SitecoreUsername ,SitecoreUserPassword   ,SolrUrl , SolrRoot , SolrService ,SolrVersion,PasswordRecoveryUrl,xConnectCollectionSvc, SitecoreIdentityAuthority) VALUES (@SiteNameSuffix, @SitePrefix,@SiteName, @IdentityServerSiteName, @SitecoreIdServerUrl, @SXASiteInstallDir, @XConnectInstallDir, @ClientSecret, @SitecoreUsername, @SitecoreUserPassword, @SolrUrl, @SolrRoot, @SolrService, @SolrVersion,@PasswordRecoveryUrl,@xConnectCollectionSvc, @SitecoreIdentityAuthority)";

                SqlCommand sqlcommand = new SqlCommand(query, sqlConn);
                sqlcommand.Parameters.AddWithValue("@SiteNameSuffix", txtSiteNameSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@SitePrefix", txtSiteNamePrefix.Text);
                sqlcommand.Parameters.AddWithValue("@SiteName", txtSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@IdentityServerSiteName", txtIDServerSiteName.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreIdServerUrl", txtSitecoreIdentityServerUrl.Text);
                sqlcommand.Parameters.AddWithValue("@SXASiteInstallDir", txtSXAInstallDir.Text);
                sqlcommand.Parameters.AddWithValue("@XConnectInstallDir", txtxConnectInstallDir.Text);
                sqlcommand.Parameters.AddWithValue("@ClientSecret", txtClientSecret.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUsername", txtSitecoreUsername.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreUserPassword", txtSitecoreUserPassword.Text);
                sqlcommand.Parameters.AddWithValue("@SolrUrl", txtSolrUrl.Text);
                sqlcommand.Parameters.AddWithValue("@SolrRoot", txtSolrRoot.Text);
                sqlcommand.Parameters.AddWithValue("@SolrService", txtSolrService.Text);
                sqlcommand.Parameters.AddWithValue("@SolrVersion", txtSolrVersion.Text);
                sqlcommand.Parameters.AddWithValue("@PasswordRecoveryUrl", txtPasswordRecoveryUrl.Text);
                sqlcommand.Parameters.AddWithValue("@xConnectCollectionSvc", txtxConnectCollectionSvc.Text);
                sqlcommand.Parameters.AddWithValue("@SitecoreIdentityAuthority", txtSitecoreIdentityAuthority.Text);

                int numberOfInsertedRows = sqlcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                CommonFunctions.WritetoEventLog("SCIA - Error saving SIF data to SCIA DB " + ex.Message, EventLogEntryType.Error);
                return false;
            }

            return true;
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

                    if (!CommonFunctions.DbTableExists("SIF", connection))
                    {
                        CommonFunctions.CreateSIFTable(connection);
                    }

                    SaveSIFDatatoDBSuccess(connection);
                }

                // if all the coperations complete successfully, this would be called and commit the transaction.
                // In case of an exception, it wont be called and transaction is rolled back
                scope.Complete();
            }
            catch (Exception ex)
            {
                CommonFunctions.WritetoEventLog("Error saving SIF SCIA Data - " + ex.Message, EventLogEntryType.Error);
            }
            Cursor.Current = Cursors.Default;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            switch (Version.SitecoreVersion)
            {
                case "10.0.1":
                case "10.1.0":
                case "10.0":
                case "9.3":
                    WriteSiteSingleDeveloperJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SXA-SingleDeveloper.json");
                    WriteJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SingleDeveloperwithSXA.json");
                    WriteFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    CommonFunctions.LaunchPSScript(@".\'"  + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1'", destFolder);
                    break;
                case "9.2":
                    Write92JsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + txtSiteNamePrefix.Text + "-SingleDeveloper.json");
                    Write92File(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    CommonFunctions.LaunchPSScript(@".\'" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1'", destFolder);
                    break;
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    WriteCreateCertJsonFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_xconnect-createcert.json");
                    Write90PSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    CommonFunctions.LaunchPSScript(@".\'" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1'", destFolder);
                    break;
                case "9.1":
                case "9.1.1":
                    WriteSingleDeveloperJsonFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "-SingleDeveloper.json");
                    WriteSingleDeveloperPSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1", false);
                    CommonFunctions.LaunchPSScript(@".\'" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Install_Script.ps1'", destFolder);
                    break;
                default:
                    break;
            }

            lblStatus.Text = "Installation successfully launched through Powershell....";
            SaveSCIAData();
            lblStatus.ForeColor = Color.DarkGreen;
            ToggleEnableControls(false);
        }

        private void DeleteScript(string path)
        {
            var appcmdExe = "C:\\windows\\system32\\inetsrv\\appcmd.exe";
            var stoppedStatus = "Stopped";
            var sitePath = "IIS:\\Sites\\Default Web Site\\$SiteName";
            var xConnectSitePath = "IIS:\\Sites\\Default Web Site\\$SitecorexConnectSiteName";

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
            var collectionShard0DBSuffix = "_Xdb.Collection.Shard0";
            var collectionShard1DBSuffix = "_Xdb.Collection.Shard1";
            var collectionShardMapManagerDBSuffix = "_Xdb.Collection.ShardMapManager";
            var messagingDBSuffix = "_Messaging";

            var webRootPath = "c:\\inetpub\\wwwroot\\";
            var usersFolderPath = "c:\\users\\";
            var certificatesFolderPath = "c:\\certificates\\";

            using var file = new StreamWriter(path);
            file.WriteLine("Param(");
            file.WriteLine("\t[string]$Prefix = \"" + txtSiteNamePrefix.Text + "\",");
            file.WriteLine("\t[string]$UserFolder = \"" + txtSiteNamePrefix.Text + "sc_User" + "\",");
            file.WriteLine("\t[string]$CommDbPrefix = \"" + txtSiteNamePrefix.Text + "_SitecoreCommerce" + "\",");
            file.WriteLine("\t[string]$SiteName = \"" + txtSiteName.Text + "\",");
            file.WriteLine("\t[string]$SolrService = \"" + txtSolrService.Text + "\",");
            file.WriteLine("\t[string]$PathToSolr = \"" + txtSolrRoot.Text + "\",");
            file.WriteLine("\t[string]$SqlServer = \"" + txtSqlDbServer.Text + "\",");
            file.WriteLine("\t[string]$SqlAccount = \"" + txtSqlUser.Text + "\",");
            file.WriteLine("\t[string]$SqlPassword = \"" + txtSqlPass.Text + "\",");
            file.WriteLine("\t[string]$SitecorexConnectSiteName = \"" + txtSiteNamePrefix.Text + "xconnect" + txtSiteNameSuffix.Text + "\",");
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

            file.WriteLine("if (Test-Path \"" + sitePath + "\") { Stop-Website -Name $SiteName }");
            file.WriteLine("if (Test-Path \"" + xConnectSitePath + "\") { Stop-Website -Name $SitecorexConnectSiteName }");
            file.WriteLine();

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
            file.WriteLine("Remove-AppPool -appPoolName $SiteName");
            file.WriteLine("Remove-AppPool -appPoolName $SitecorexConnectSiteName");
            file.WriteLine("Write-Host \"App Pools deleted successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting Websites from IIS\"");
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
            file.WriteLine("rm " + webRootPath + "$SiteName -force -recurse -ea ig");
            file.WriteLine("rm " + webRootPath + "$SitecorexConnectSiteName -force -recurse -ea ig");
            file.WriteLine("Write-Host \"Websites removed from wwwroot\"");

            file.WriteLine("Write-Host \"Deleting Windows Users from c:\\users\"");
            file.WriteLine("rm " + usersFolderPath + "$SitecorexConnectSiteName -force -recurse -ea ig");
            file.WriteLine("rm " + usersFolderPath + "$SiteName -force -recurse -ea ig");
            file.WriteLine("rm " + usersFolderPath + "$UserFolder -force -recurse -ea ig");
            file.WriteLine("Write-Host \"Website Folders removed from c:\\Users folder\"");

            file.WriteLine("rm " + certificatesFolderPath + "$Prefix"   + ".crt -force -recurse -ea ig");
            file.WriteLine("rm " + certificatesFolderPath + "$SitecorexConnectSiteName" + ".pfx -force -recurse -ea ig");
            file.WriteLine("pop-location");
        }


        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            switch (Version.SitecoreVersion)
            {
                case "10.0.1":
                case "10.1.0":
                case "10.0":
                case "9.3":
                    WriteSiteSingleDeveloperJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SXA-SingleDeveloper.json");
                    WriteJsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "SingleDeveloperwithSXA.json");
                    WriteFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", true);
                    CommonFunctions.LaunchPSScript(@".\'"  + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1'", destFolder);
                    break;
                case "9.2":
                    Write92JsonFile(@".\" + ZipList.SitecoreDevSetupZip + @"\" + txtSiteNamePrefix.Text + "-SingleDeveloper.json");
                    Write92File(@".\" + ZipList.SitecoreDevSetupZip + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", true);
                    CommonFunctions.LaunchPSScript(@".\'" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1'", destFolder);
                    break;
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    DeleteScript(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Delete_Script.ps1");
                    CommonFunctions.LaunchPSScript(@".\"  + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Delete_Script.ps1", destFolder);
                    break;
                case "9.1":
                    WriteSingleDeveloperJsonFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "-SingleDeveloper.json");
                    WriteSingleDeveloperPSFile(@".\" + destFolder + @"\" + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1", true);
                    CommonFunctions.LaunchPSScript(@".\'"  + SCIASettings.FilePrefixAppString + txtSiteName.Text + "_UnInstall_Script.ps1'", destFolder);
                    break;
                default:
                    break;
            }

            lblStatus.Text = "Uninstallation successfully launched through Powershell....";
            //SaveSCIAData();
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
                    xConnectServerNameString = settingsData.xConnectServerNameString.Trim();
                    txtClientSecret.Text = settingsData.CommerceEngineConnectClientSecret.Trim();
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
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtSXAInstallDir.Text = siteRootDir;
            if (xp0Install)
            {
                txtSXAInstallDir.Text = siteRootDir + txtSiteName.Text;
            }
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtSitecoreIdentityAuthority.Text = "https://" + txtIDServerSiteName.Text;
            txtPasswordRecoveryUrl.Text = "https://" + txtSiteName.Text;
            txtxConnectSiteName.Text = txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtxConnectCollectionSvc.Text = "https://" + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;

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
                txtClientSecret.Text = siteData.ClientSecret.Trim();
                txtSitecoreUsername.Text = siteData.SitecoreUsername.Trim();
                txtSolrRoot.Text = siteData.SolrRoot;
                txtSolrUrl.Text = siteData.SolrUrl;
                txtSolrService.Text = siteData.SolrService;
                txtSolrVersion.Text = siteData.SolrVersion;
                txtPasswordRecoveryUrl.Text=siteData.PasswordRecoveryUrl;
                txtClientSecret.Text=siteData.ClientSecret;
                txtxConnectCollectionSvc.Text=siteData.xConnectCollectionSvc;
                txtSitecoreIdentityAuthority.Text=siteData.SitecoreIdentityAuthority;
                //Uninstall = true;
            }
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TabIndexValue == const_DBConn_Tab) if (!DbConnTabValidations() || settingsData == null) return;

            //Get data from SCIA table
            if (TabIndexValue == const_SiteInfo_Tab)
            {
                if (!SiteInfoTabValidations()) return;
                if (!CommonFunctions.FileSystemEntryExists(txtSXAInstallDir.Text,null,"folder",true))
                {
                    PopulateSIFData();
                }
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
                SifPrerequisites formInstance = new SifPrerequisites(dbDetails);
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
            txtxConnectInstallDir.Text = siteRootDir + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtSitecoreIdentityServerUrl.Text = "https://" + txtIDServerSiteName.Text;
            txtSitecoreIdentityAuthority.Text = "https://" + txtIDServerSiteName.Text;
            txtPasswordRecoveryUrl.Text = "https://" + txtSiteName.Text;
            txtxConnectSiteName.Text = txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;
            txtxConnectCollectionSvc.Text = "https://" + txtSiteNamePrefix.Text + xConnectServerNameString + txtSiteNameSuffix.Text;

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
