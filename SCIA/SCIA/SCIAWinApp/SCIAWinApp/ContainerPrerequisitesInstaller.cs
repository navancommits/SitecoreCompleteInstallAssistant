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
    public partial class ContainerPrerequisitesInstaller : Form
    {
        SiteDetails siteDetails;
        public ContainerPrerequisitesInstaller(SiteDetails siteInfo)
        {
            InitializeComponent();
            siteDetails = siteInfo;
        }

        void WriteWorkerFile(string path)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("[CmdletBinding(SupportsShouldProcess = $true)]");
            file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"SitecorePassword\")]");
            file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"RegistryPassword\")]");
            file.WriteLine();
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $false)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecorePassword");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("$sitecoreDownloadUrl = \"https://sitecoredev.azureedge.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"" + ZipList.CommerceContainerZip + ".zip\" = \"https://sitecoredev.azureedge.net/~/media/FB50C51D304C47E89EB1C21C087B9B73.ashx\"");            
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# download packages from Sitecore");
            file.WriteLine("$packages.GetEnumerator() | ForEach-Object {");
            file.WriteLine();
            file.WriteLine("\t$filePath = Join-Path $InstallSourcePath $_.Key");
            file.WriteLine("\t$fileUrl = $_.Value");
            file.WriteLine();
            file.WriteLine("\tif (Test-Path $filePath -PathType Leaf)");
            file.WriteLine("\t{");
            file.WriteLine("\t\tWrite-Host (\"Required package found: '{0}'\" -f $filePath)");
            file.WriteLine("\t}");
            file.WriteLine("\telse");
            file.WriteLine("\t{");
            file.WriteLine("\t\tif ($PSCmdlet.ShouldProcess($fileName))");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\tWrite-Host (\"Downloading '{0}' to '{1}'...\" -f $fileUrl, $filePath)");
            file.WriteLine("\t\t\t# Download package");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t\telse");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\t# Download package");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t}");
            file.WriteLine("}");
        }
        void WriteMainFile(string path)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("[CmdletBinding(SupportsShouldProcess = $true)]");
            file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"SitecorePassword\")]");
            file.WriteLine("[System.Diagnostics.CodeAnalysis.SuppressMessageAttribute(\"PSAvoidUsingPlainTextForPassword\", \"RegistryPassword\")]");
            file.WriteLine();
            file.WriteLine("param(");
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecoreUsername,");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecorePassword");
            file.WriteLine(")");
            file.WriteLine();         

            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllContainerPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("Expand-Archive -Force -LiteralPath " + ZipList.CommerceContainerZip + ".zip -DestinationPath .");            
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        void WriteAutoFillFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$LicenseXmlPath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$InstallSourcePath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("[Parameter(Mandatory = $false)]");
            file.WriteLine("[ValidateNotNullOrEmpty()]");
            file.WriteLine("[string]$SitecoreUsername,");
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
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandExpandContainerZip.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \"" + txtUser.Text + "\" -SitecorePassword \"" + txtPass.Text + "\"");
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
            file.WriteLine("Set-DockerComposeEnvFileVariable \"TELERIK_ENCRYPTION_KEY\" -Value (Get-SitecoreRandomString 128  -DisallowSpecial)");
            file.WriteLine();
            file.WriteLine("# SITECORE_IDSECRET = random 64 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_IDSECRET\" -Value (Get-SitecoreRandomString 64 -DisallowSpecial)");
            file.WriteLine();
            file.WriteLine("# SITECORE_ID_CERTIFICATE");
            file.WriteLine("$idCertPassword = Get-SitecoreRandomString 12 -DisallowSpecial");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ID_CERTIFICATE\" -Value (Get-SitecoreCertificateAsBase64String -DnsName \"" + siteDetails.HostPostFixforContainer + "\" -Password (ConvertTo-SecureString -String $idCertPassword -Force -AsPlainText))");
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
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.SiteHostName +".crt -key-file "+ siteDetails.SiteHostName + ".key \"" + siteDetails.SiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.IdServerSiteHostName + ".crt -key-file " + siteDetails.IdServerSiteHostName + ".key \"" + siteDetails.IdServerSiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.CommerceAuthSiteHostName + ".crt -key-file " + siteDetails.CommerceAuthSiteHostName + ".key \"" + siteDetails.CommerceAuthSiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.CommerceShopsSiteHostName + ".crt -key-file " + siteDetails.CommerceShopsSiteHostName + ".key \"" + siteDetails.CommerceShopsSiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.CommerceMinionsSiteHostName + ".crt -key-file " + siteDetails.CommerceMinionsSiteHostName + ".key \"" + siteDetails.CommerceMinionsSiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.CommerceOpsSiteHostName + ".crt -key-file " + siteDetails.CommerceOpsSiteHostName + ".key \"" + siteDetails.CommerceOpsSiteHostName + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + siteDetails.BizFxSiteHostName + ".crt -key-file " + siteDetails.BizFxSiteHostName + ".key \"" + siteDetails.BizFxSiteHostName + "\"");
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
            file.WriteLine("Add-HostsEntry \"" + siteDetails.IdServerSiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.SiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.CommerceMinionsSiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.CommerceShopsSiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.CommerceOpsSiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.BizFxSiteHostName + "\"");
            file.WriteLine("Add-HostsEntry \"" + siteDetails.CommerceAuthSiteHostName + "\"");
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

            private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUser.Text) || string.IsNullOrWhiteSpace(txtPass.Text)) {
                SetStatusMessage("SDN User / Password needed", Color.Red);
                return;
            }

            WriteAutoFillFile(@".\" + SCIASettings.FilePrefixAppString  + "AutofillEnvFile.ps1");

            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "AutofillEnvFile.ps1 -BrainTreeMerchantId \"" + siteDetails.BraintreeMerchantId + "\" -BrainTreePrivateKey \"" + siteDetails.BraintreePrivateKey + "\" -BrainTreePublicKey \"" + siteDetails.BraintreePublicKey + "\" -BrainTreeEnvironment \"" + siteDetails.BraintreeEnvironment + "\" -SqlSaPassword \"" + siteDetails.SqlPass + "\" -LicenseXmlPath . -InstallSourcePath \".\"");
        }
    }
}
