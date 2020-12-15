using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SCIA
{
    public partial class SitecoreContainerInstaller : Form
    {
        const int const_DBConn_Tab = 0;
        const int const_SiteInfo_Tab = 1;
        const int const_Sitecore_Tab = 2;
        const int const_Port_Tab = 3;
        string xp0Path = "\\compose\\ltsc2019\\xp0\\";
        string siteNamePrefixString = "sc";//sc
        string identityServerNameString = "identityserver";//identityserver
        public SitecoreContainerInstaller()
        {
            InitializeComponent();
            this.Text = this.Text + " for Sitecore v" + Version.SitecoreVersion;
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
            ToggleEnableControls(false);
            AssignStepStatus(const_DBConn_Tab);
            txtSqlDbServer.Text = DBDetails.DbServer;
            txtSqlPass.Text = DBDetails.SqlPass;
            txtSqlUser.Text = DBDetails.SqlUser;
        }

        private void txtSqlPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlPass_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlDbServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreDbServer_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void txtSiteNameSuffix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void txtSiteNamePrefix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUserPassword_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUsername_Leave(object sender, EventArgs e)
        {

        }

        void WriteCleanUpFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("# Clean data folders");
            file.WriteLine("Get-ChildItem -Path(Join-Path $PSScriptRoot \"\\mssql-data\") -Exclude \".gitkeep\" -Recurse | Remove-Item -Force -Recurse -Verbose");
            file.WriteLine("Get-ChildItem -Path(Join-Path $PSScriptRoot \"\\solr-data\") -Exclude \".gitkeep\" -Recurse | Remove-Item -Force -Recurse -Verbose");
            file.Dispose();
        }

        //\traefik\config\dynamic\
        void WriteConfigFile(string path)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("tls:");
            file.WriteLine("certificates:");
            file.WriteLine("-certFile: C:\\etc\\traefik\\certs\\" + txtSiteName.Text + ".crt");
            file.WriteLine("keyFile: C:\\etc\\traefik\\certs\\" + txtSiteName.Text + ".key");
            file.WriteLine("-certFile: C:\\etc\\traefik\\certs\\" + txtIDServerSiteName.Text + ".crt");
            file.WriteLine("keyFile: C:\\etc\\traefik\\certs\\" + txtIDServerSiteName.Text + ".key");
            file.Dispose();
        }

        void WriteComposeFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("version: \"2.4\"");
            file.WriteLine("services:");
            file.WriteLine("  traefik:");
            file.WriteLine("    isolation: ${TRAEFIK_ISOLATION}");
            file.WriteLine("    image: ${TRAEFIK_IMAGE}");
            file.WriteLine("    command:");
            file.WriteLine("      - \"--ping\"");
            file.WriteLine("      - \"--api.insecure=true\"");
            file.WriteLine("      - \"--providers.docker.endpoint=npipe:////./pipe/docker_engine\"");
            file.WriteLine("      - \"--providers.docker.exposedByDefault=false\"");
            file.WriteLine("      - \"--providers.file.directory=C:/etc/traefik/config/dynamic\"");
            file.WriteLine("      - \"--entryPoints.websecure.address=:443\"");
            file.WriteLine("    ports:");
            file.WriteLine("      - \"" + txtTraefikPort1.Text + ":443\"");
            file.WriteLine("      - \"" + txtTraefikPort2.Text  + ":8080\"");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"traefik\", \"healthcheck\", \"--ping\"]");
            file.WriteLine("    volumes:");
            file.WriteLine("      - source: \\\\.\\pipe\\docker_engine");
            file.WriteLine("        target: \\\\.\\pipe\\docker_engine");
            file.WriteLine("        type: npipe");
            file.WriteLine("      - ./traefik:C:/etc/traefik");
            file.WriteLine("    depends_on:");
            file.WriteLine("      cm:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("      id:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("  mssql:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-mssql:${SITECORE_VERSION}");
            file.WriteLine("    environment:");
            file.WriteLine("      SA_PASSWORD: ${SQL_SA_PASSWORD}");
            file.WriteLine("      SITECORE_ADMIN_PASSWORD: ${SITECORE_ADMIN_PASSWORD}");
            file.WriteLine("      ACCEPT_EULA: \"Y\"");
            file.WriteLine("      SQL_SERVER: mssql");
            file.WriteLine("    ports:");
            file.WriteLine("      - \"" + txtMsSqlPort.Text + ":1433\"");
            file.WriteLine("    volumes:");
            file.WriteLine("      - type: bind");
            file.WriteLine("        source: .\\mssql-data");
            file.WriteLine("        target: c:\\data");
            file.WriteLine("  solr:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    ports:");
            file.WriteLine("      - \"" + txtSolrPort.Text + ":8983\"");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-solr:${SITECORE_VERSION}");
            file.WriteLine("    volumes:");
            file.WriteLine("      - type: bind");
            file.WriteLine("        source: .\\solr-data");
            file.WriteLine("        target: c:\\data");
            file.WriteLine("  id:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-id:${SITECORE_VERSION}");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__SitecoreMemberShipOptions__ConnectionString: Data Source=mssql;Initial Catalog=Sitecore.Core;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__AccountOptions__PasswordRecoveryUrl: https://${CM_HOST}/sitecore/login?rc=1");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__Clients__PasswordClient__ClientSecrets__ClientSecret1: ${SITECORE_IDSECRET}");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__Clients__DefaultClient__AllowedCorsOrigins__AllowedCorsOriginsGroup1: https://${CM_HOST}");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__CertificateRawData: ${SITECORE_ID_CERTIFICATE}");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__PublicOrigin: https://${ID_HOST}");
            file.WriteLine("      Sitecore_Sitecore__IdentityServer__CertificateRawDataPassword: ${SITECORE_ID_CERTIFICATE_PASSWORD}");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1\"]");
            file.WriteLine("      timeout: 300s");
            file.WriteLine("    depends_on:");
            file.WriteLine("      mssql:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("    labels:");
            file.WriteLine("      - \"traefik.enable=true\"");
            file.WriteLine("      - \"traefik.http.routers.id-secure.entrypoints=websecure\"");
            file.WriteLine("      - \"traefik.http.routers.id-secure.rule=Host(`${ID_HOST}`)\"");
            file.WriteLine("      - \"traefik.http.routers.id-secure.tls=true\"");
            file.WriteLine("  cm:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-cm:${SITECORE_VERSION}");
            file.WriteLine("    depends_on:");
            file.WriteLine("      id:");
            file.WriteLine("        condition: service_started");
            file.WriteLine("      xconnect:");
            file.WriteLine("        condition: service_started");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_ConnectionStrings_Core: Data Source=mssql;Initial Catalog=Sitecore.Core;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Security: Data Source=mssql;Initial Catalog=Sitecore.Core;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Master: Data Source=mssql;Initial Catalog=Sitecore.Master;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Web: Data Source=mssql;Initial Catalog=Sitecore.Web;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Messaging: Data Source=mssql;Initial Catalog=Sitecore.Messaging;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Processing.Pools: Data Source=mssql;Initial Catalog=Sitecore.Processing.pools;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Referencedata: Data Source=mssql;Initial Catalog=Sitecore.Referencedata;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Processing.Tasks: Data Source=mssql;Initial Catalog=Sitecore.Processing.tasks;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_ExperienceForms: Data Source=mssql;Initial Catalog=Sitecore.ExperienceForms;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Exm.Master: Data Source=mssql;Initial Catalog=Sitecore.Exm.master;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Reporting: Data Source=mssql;Initial Catalog=Sitecore.Reporting;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Sitecore.Reporting.Client: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Solr.Search: http://solr:8983/solr");
            file.WriteLine("      Sitecore_ConnectionStrings_SitecoreIdentity.Secret:  ${SITECORE_IDSECRET}");
            file.WriteLine("      Sitecore_ConnectionStrings_XConnect.Collection: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.MarketingAutomation.Operations.Client: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.MarketingAutomation.Reporting.Client: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.ReferenceData.Client: http://xconnect");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("      Sitecore_Identity_Server_Authority: https://${ID_HOST}");
            file.WriteLine("      Sitecore_Identity_Server_InternalAuthority: http://id");
            file.WriteLine("      Sitecore_Identity_Server_CallbackAuthority: https://${CM_HOST}");
            file.WriteLine("      Sitecore_Identity_Server_Require_Https: \"false\"");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1\"]");
            file.WriteLine("      timeout: 300s");
            file.WriteLine("    labels:");
            file.WriteLine("      - \"traefik.enable=true\"");
            file.WriteLine("      - \"traefik.http.middlewares.force-STS-Header.headers.forceSTSHeader=true\"");
            file.WriteLine("      - \"traefik.http.middlewares.force-STS-Header.headers.stsSeconds=31536000\"");
            file.WriteLine("      - \"traefik.http.routers.cm-secure.entrypoints=websecure\"");
            file.WriteLine("      - \"traefik.http.routers.cm-secure.rule=Host(`${CM_HOST}`)\"");
            file.WriteLine("      - \"traefik.http.routers.cm-secure.tls=true\"");
            file.WriteLine("      - \"traefik.http.routers.cm-secure.middlewares=force-STS-Header\"");
            file.WriteLine("  xconnect:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-xconnect:${SITECORE_VERSION}");
            file.WriteLine("    ports:");
            file.WriteLine("      - \"" + txtxConnectPort.Text+ ":80\"");
            file.WriteLine("    depends_on:");
            file.WriteLine("      mssql:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("      solr:");
            file.WriteLine("        condition: service_started");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("      Sitecore_ConnectionStrings_Messaging: Data Source=mssql;Initial Catalog=Sitecore.Messaging;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Processing.Engine.Storage: Data Source=mssql;Initial Catalog=Sitecore.Processing.Engine.Storage;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Reporting: Data Source=mssql;Initial Catalog=Sitecore.Reporting;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Marketingautomation: Data Source=mssql;Initial Catalog=Sitecore.Marketingautomation;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Processing.Pools: Data Source=mssql;Initial Catalog=Sitecore.Processing.pools;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Referencedata: Data Source=mssql;Initial Catalog=Sitecore.Referencedata;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Collection: Data Source=mssql;Initial Catalog=Sitecore.Xdb.Collection.ShardMapManager;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_SolrCore: http://solr:8983/solr/sitecore_xdb");
            file.WriteLine("      Sitecore_Sitecore:XConnect:CollectionSearch:Services:Solr.SolrReaderSettings:Options:RequireHttps: 'false'");
            file.WriteLine("      Sitecore_Sitecore:XConnect:CollectionSearch:Services:XConnectSolrHealthCheckServicesConfiguration:Options:RequireHttps: 'false'");
            file.WriteLine("      Sitecore_Sitecore:XConnect:SearchIndexer:Services:Solr.SolrReaderSettings:Options:RequireHttps: 'false'");
            file.WriteLine("      Sitecore_Sitecore:XConnect:SearchIndexer:Services:Solr.SolrWriterSettings:Options:RequireHttps: 'false'");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1\"]");
            file.WriteLine("      timeout: 300s");
            file.WriteLine("  xdbsearchworker:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-xdbsearchworker:${SITECORE_VERSION}");
            file.WriteLine("    depends_on:");
            file.WriteLine("      xconnect:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("    restart: unless-stopped");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_ConnectionStrings_Collection: Data Source=mssql;Initial Catalog=Sitecore.Xdb.Collection.ShardMapManager;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_SolrCore: http://solr:8983/solr/sitecore_xdb");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("      Sitecore_Sitecore:XConnect:SearchIndexer:Services:Solr.SolrReaderSettings:Options:RequireHttps: 'false'");
            file.WriteLine("      Sitecore_Sitecore:XConnect:SearchIndexer:Services:Solr.SolrWriterSettings:Options:RequireHttps: 'false'");
            file.WriteLine("      Sitecore_Sitecore:XConnect:CollectionSearch:Services:XConnectSolrHealthCheckServicesConfiguration:Options:RequireHttps: 'false'");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1 -Port 8080\"]");
            file.WriteLine("      timeout: 300s");
            file.WriteLine("  xdbautomationworker:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-xdbautomationworker:${SITECORE_VERSION}");
            file.WriteLine("    depends_on:");
            file.WriteLine("      xconnect:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("    restart: unless-stopped");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_ConnectionStrings_XConnect.Collection: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Marketingautomation: Data Source=mssql;Initial Catalog=Sitecore.Marketingautomation;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Xdb.Referencedata: Data Source=mssql;Initial Catalog=Sitecore.Referencedata;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Messaging: Data Source=mssql;Initial Catalog=Sitecore.Messaging;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1 -Port 8080\"]");
            file.WriteLine("      timeout: 300s");
            file.WriteLine("  cortexprocessingworker:");
            file.WriteLine("    isolation: ${ISOLATION}");
            file.WriteLine("    image: ${SITECORE_DOCKER_REGISTRY}sitecore-xp0-cortexprocessingworker:${SITECORE_VERSION}");
            file.WriteLine("    depends_on:");
            file.WriteLine("      xconnect:");
            file.WriteLine("        condition: service_healthy");
            file.WriteLine("    restart: unless-stopped");
            file.WriteLine("    environment:");
            file.WriteLine("      Sitecore_ConnectionStrings_Processing.Engine.Storage: Data Source=mssql;Initial Catalog=Sitecore.Processing.Engine.Storage;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Processing.Engine.Tasks: Data Source=mssql;Initial Catalog=Sitecore.Processing.Engine.Tasks;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Processing.Webapi.Blob: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Processing.Webapi.Table: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_XConnect.Collection: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_XConnect.Configuration: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_XConnect.Search: http://xconnect");
            file.WriteLine("      Sitecore_ConnectionStrings_Messaging: Data Source=mssql;Initial Catalog=Sitecore.Messaging;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_ConnectionStrings_Reporting: Data Source=mssql;Initial Catalog=Sitecore.Reporting;User ID=sa;Password=${SQL_SA_PASSWORD}");
            file.WriteLine("      Sitecore_License: ${SITECORE_LICENSE}");
            file.WriteLine("    healthcheck:");
            file.WriteLine("      test: [\"CMD\", \"powershell\", \"-command\", \"C:/Healthchecks/Healthcheck.ps1 -Port 8080\"]");
            file.WriteLine("      timeout: 300s");

        }

        void WriteAutoFillFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$LicenseXmlPath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreAdminPassword = \"Password12345\",");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SqlSaPassword = \"Password12345\"");
            file.WriteLine();
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
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SQL_SA_PASSWORD\" -Value \"" + txtsaPassword.Text + "\"");
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
            file.WriteLine("# CM_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"CM_HOST\" -Value \"" + txtSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("# ID_HOST");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"ID_HOST\" -Value \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine();
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
            file.WriteLine();
            file.WriteLine("Write-Host \"Done!\" -ForegroundColor Green");
            file.WriteLine();
            file.Dispose();
        }

        private void ToggleEnableControls(bool enabled)
        {
            ToggleButtonControls(enabled);
            MenubarControls(enabled);
        }

        private void MenubarControls(bool enabled)
        {
            btnLast.Enabled = enabled;
            btnAppSettings.Enabled = enabled;
            btnFirst.Enabled = enabled;
            btnPrevious.Enabled = enabled;
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

        private bool CheckPrerequisites()
        {
            if (!Directory.Exists(".\\" + ZipList.SitecoreContainerZip)) { return false; }
            if (!File.Exists(".\\" + ZipList.SitecoreContainerZip + xp0Path + "license.xml")) { return false; }
            if (!WindowsVersionOk()) { return false; };
            if (!Directory.Exists("c:\\program files\\docker")) { return false; }

            return true;
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
            if (!ValidateData(txtSiteNameSuffix, "Site Suffix", const_SiteInfo_Tab)) return false;
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

        
        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            chkStepsList.SelectedIndex = tabIndex;
            tabDetails.SelectedIndex = tabIndex;
            chkStepsList.SetItemChecked(tabIndex, true);
            int tabCount = tabDetails.TabCount;
            switch (tabIndex)
            {
                case const_DBConn_Tab:
                    lblStepInfo.Text = "Step 1 of " + tabCount + ": DB Connection";
                    break;
                case const_SiteInfo_Tab:
                    ToggleEnableControls(false);
                    btnAppSettings.Enabled = true;
                    btnNext.Enabled = true;
                    lblStepInfo.Text = "Step 2 of " + tabCount + ": Site Info";
                    break;
                case const_Sitecore_Tab:
                    ToggleEnableControls(true); 
                    lblStepInfo.Text = "Step 3 of " + tabCount + ": Sitecore Details";
                    break;
            }

        }

        public int TabIndexValue { get; set; }

        private bool WindowsVersionOk()
        {
            string version = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion", "ProductName", null);
            if (version == "Windows 10 Pro" || version == "Windows 10 Enterprise") { return true; }
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Windows Edition must be Pro or Enterprise Build for Docker Windows";
            return false;
        }
        private void SetStatusMessage(string statusmsg, Color color)
        {

            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private bool ValidateAll(bool unInstall = false)
        {
            if (!DbConnTabValidations()) return false;
            if (!SiteInfoTabValidations()) return false;

            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_SiteInfo_Tab)) return false;
            if (!ValidateData(txtSitecoreUsername, "Sitecore Username", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", const_Sitecore_Tab)) return false;


            if (unInstall) return true;

            return true;
        }


        private bool CheckAllValidations(bool uninstall = false, bool generatescript = false)
        {
            ToggleButtonControls(false);

            if (!CheckPrerequisites())
            {
                SetStatusMessage("One or more pre-requisites missing.... Click Pre-requisites button to check...", Color.Red);
                return false;
            }

            if (!ValidateAll(uninstall)) return false;
            if (!SiteInfoTabValidations()) return false;
            if (!PerformPortValidations(uninstall)) return false;

            ToggleEnableControls(true);
            //if (uninstall) { btnInstall.Enabled = false; } else { btnUninstall.Enabled = false; }

            return true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string initPS1 = "SCIA-" + txtSiteName.Text + "-init.ps1";
            if (!CheckAllValidations()) return;
            WriteAutoFillFile(".\\" + ZipList.SitecoreContainerZip + xp0Path + initPS1);
            WriteConfigFile(".\\" + ZipList.SitecoreContainerZip + xp0Path + "\\traefik\\config\\dynamic\\certs_config.yaml");
            WriteComposeFile(".\\" + ZipList.SitecoreContainerZip + xp0Path + "docker-compose.yml");

            CommonFunctions.LaunchPSScript(".\\" + initPS1 + "  -SitecoreAdminPassword \"" + txtSitecoreUserPassword.Text + "\" -SqlSaPassword \"" + txtSqlPass.Text + "\" -LicenseXmlPath \"license.xml\"", ".\\" + ZipList.SitecoreContainerZip + "\\compose\\ltsc2019\\xp0");

            lblStatus.Text = ".env file generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var destFolder = ZipList.SitecoreContainerZip + xp0Path;
            WriteCleanUpFile(destFolder + "SCIA-Cleanup.ps1");
            CommonFunctions.LaunchPSScript(@".\SCIA-Cleanup.ps1", destFolder);

        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            CommonFunctions.LaunchCmdScript("docker-compose up -d", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker-Compose Up successfully launched....";
            ToggleEnableControls(false);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            CommonFunctions.LaunchCmdScript("docker-compose down", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker-Compose Down successfully launched....";
            ToggleEnableControls(false);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            AssignStepStatus(tabDetails.TabCount-1);

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            AssignStepStatus(0);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabDetails.TabCount - 1) TabIndexValue -= 1;
            AssignStepStatus(TabIndexValue);
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


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TabIndexValue == const_DBConn_Tab) if (!DbConnTabValidations() || settingsData == null) return;

            //Get data from SCIA table
            if (TabIndexValue == const_SiteInfo_Tab)
            {
                if (!SiteInfoTabValidations()) return;
                //PopulateSCIAData();                
            }
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 2) TabIndexValue += 1;
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 1) AssignStepStatus(TabIndexValue);
        }
        private void DisplaySettingsDialog(bool settingsDataPresent)
        {
            DBServerDetails dBServerDetails = new DBServerDetails
            {
                Username = txtSqlUser.Text,
                Password = txtSqlPass.Text,
                Server = txtSqlDbServer.Text,
                IsSettingsPresent = settingsDataPresent
            };
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
        private void ToggleEnableDbControls(bool enabled)
        {
            txtSqlPass.Enabled = enabled;
            txtSqlUser.Enabled = enabled;
            txtSqlDbServer.Enabled = enabled;
            btnDbConn.Enabled = enabled;
        }

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            SitecoreContainerPrerequisites prerequisites = new SitecoreContainerPrerequisites();
            prerequisites.ShowDialog();
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
                    txtSitecoreUsername.Text = settingsData.SitecoreUsername.Trim();
                    txtSiteNameSuffix.Text = "dev.local";

                    SetFieldValues();

                }
            }
            return true;
        }

        private void SetFieldValues()
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + "." + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + "." + txtSiteNameSuffix.Text;
        }
        private SettingsData settingsData { get; set; }


        private void btnDbConn_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "master", txtSqlUser.Text, txtSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                TabIndexValue = const_DBConn_Tab;
                AssignStepStatus(TabIndexValue);
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

            ToggleEnableDbControls(false);
            btnDbConn.Enabled = false;
            AssignStepStatus(const_SiteInfo_Tab);

        }

        private void btnValidateAll_Click(object sender, EventArgs e)
        {
            if (CheckAllValidations()) SetStatusMessage("Congrats! Passed all Validations!", Color.DarkGreen);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            CommonFunctions.LaunchCmdScript("docker ps", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker ps launched....";
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
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

        private bool PortInRange(string input, string portString, int tabIndex)
        {
            var regex = @"^([1-9][0-9]{0,3}|[1-5][0-9]{4}|6[0-4][0-9]{3}|65[0-4][0-9]{2}|655[0-2][0-9]|6553[0-5])$";
            var match = Regex.Match(input, regex, RegexOptions.IgnoreCase);

            if (!match.Success)
            {
                lblStatus.Text = portString + " range must be between 1 and 65535... ";
                lblStatus.ForeColor = Color.Red;
                AssignStepStatus(tabIndex);
                return false;
            }

            return true;
        }

        private bool IsPortDuplicated(List<int> ports)
        {
            bool portDuplicated = false;
            for (int index = 0; index < ports.Count; index++)
            {
                for (int loopIndex = 0; loopIndex < ports.Count; loopIndex++)
                {
                    if (loopIndex != index)
                    {
                        if (ports[loopIndex] == ports[index])
                        {
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
                Convert.ToInt32(txtTraefikPort1.Value),
                Convert.ToInt32(txtTraefikPort2.Value),
                Convert.ToInt32(txtSolrPort.Value),
                Convert.ToInt32(txtMsSqlPort.Value),
                Convert.ToInt32(txtxConnectPort.Value),
            };
            return ports;
        }

        private bool IsPortNotinUse(NumericUpDown control, int tabIndex)
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

        private bool PerformPortValidations(bool unInstall = false)
        {
            string portString = string.Empty;

            if (!PortInRange(txtTraefikPort1.Text, "Traefik Port 1", const_Port_Tab)) return false;

            if (!PortInRange(txtTraefikPort2.Text, "Traefik Port 2", const_Port_Tab)) return false;
            if (!PortInRange(txtMsSqlPort.Text, "MSSQL Port", const_Port_Tab)) return false;
            if (!PortInRange(txtSolrPort.Text, "Solr Port", const_Port_Tab)) return false;
            if (!PortInRange(txtxConnectPort.Text, "xConnect Port", const_Port_Tab)) return false;

            if (!unInstall)
            {
                if (!IsPortNotinUse(txtTraefikPort2, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtMsSqlPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtSolrPort, const_Port_Tab)) return false;
                if (!IsPortNotinUse(txtxConnectPort, const_Port_Tab)) return false;
            }
            if (IsPortDuplicated(AddPortstoArray())) { SetStatusMessage("Duplicate port numbers detected! provide unique port numbers....", Color.Red); return false; }

            //portString = StatusMessageBuilder(portString);
            //if (!string.IsNullOrWhiteSpace(portString))
            //{ lblStatus.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatus.ForeColor = Color.Red; }

            return true;
        }

        private void btnPrune_Click(object sender, EventArgs e)
        {
            CommonFunctions.LaunchCmdScript("docker image prune -a", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker prune launched....";
        }

        private void btnScriptPreview_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {
            CommonFunctions.LaunchCmdScript("docker-compose kill", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker kill launched....";
        }

        private void SitecoreContainerInstaller_Load(object sender, EventArgs e)
        {

        }

        //private string StatusMessageBuilder(string msg)
        //{
        //    string portString = string.Empty;
        //    if (PortInUse(Convert.ToInt32(txtTraefikPort1.Value))) { portString = BuildPortString(txtCommerceShopsServicesPort.Value.ToString(), portString); }
        //    if (PortInUse(Convert.ToInt32(txtTraefikPort2.Value))) { portString = BuildPortString(txtCommerceOpsSvcPort.Value.ToString(), portString); }
        //    if (PortInUse(Convert.ToInt32(txtBizFxPort.Value))) { portString = BuildPortString(txtBizFxPort.Value.ToString(), portString); }
        //    if (PortInUse(Convert.ToInt32(txtCommerceAuthSvcPort.Value))) { portString = BuildPortString(txtCommerceAuthSvcPort.Value.ToString(), portString); }
        //    if (PortInUse(Convert.ToInt32(txtCommerceMinionsSvcPort.Value))) { portString = BuildPortString(txtCommerceMinionsSvcPort.Value.ToString(), portString); }
        //    return portString;
        //}

    }
}
