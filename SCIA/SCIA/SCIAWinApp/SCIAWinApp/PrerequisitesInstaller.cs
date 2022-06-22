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
    public partial class PrerequisitesInstaller : Form
    {
        public PrerequisitesInstaller()
        {
            InitializeComponent();
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
            file.WriteLine("$sitecoreDownloadUrl = \"https://dev.sitecore.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"Sitecore.Commerce.WDP.2020.08-6.0.238.zip\" = \"https://dev.sitecore.net/~/media/7ED76B7A45D04746A3862726ADB59583.ashx\"");
            file.WriteLine("\"Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip\" = \"https://dev.sitecore.net/~/media/42992D85CC134384A0660F6C41479C16.ashx\"");
            file.WriteLine("\"Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip\" = \"https://dev.sitecore.net/~/media/E820B0DA62464072891DA92470F93954.ashx\"");
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
            file.WriteLine();
            file.WriteLine("\t\t\tif ($fileUrl.StartsWith($sitecoreDownloadUrl))");
            file.WriteLine("\t\t\t{");
            file.WriteLine("\t\t\t\t# Login to dev.sitecore.net and save session for re-use");
            file.WriteLine("\t\t\t\tif ($null -eq $sitecoreDownloadSession)");
            file.WriteLine("\t\t\t\t{");
            file.WriteLine("\t\t\t\t\tWrite-Verbose(\"Logging in to '{0}'...\" -f $sitecoreDownloadUrl)");
            file.WriteLine();
            file.WriteLine("\t\t\t\t\t$loginResponse = Invoke-WebRequest \"https://dev.sitecore.net/api/authorization\" -Method Post -Body @{");
            file.WriteLine("\t\t\t\t\t\tusername   = $SitecoreUsername");
            file.WriteLine("\t\t\t\t\t\tpassword   = $SitecorePassword");
            file.WriteLine("\t\t\t\t\t\trememberMe = $true");
            file.WriteLine("\t\t\t\t\t} -SessionVariable \"sitecoreDownloadSession\" -UseBasicParsing");
            file.WriteLine();
            file.WriteLine("\t\t\t\tif ($null -eq $loginResponse -or $loginResponse.StatusCode -ne 200 -or $loginResponse.Content -eq \"false\")");
            file.WriteLine("\t\t\t\t{");
            file.WriteLine("\t\t\t\t\tthrow (\"Unable to login to '{0}' with the supplied credentials.\" -f $sitecoreDownloadUrl)");
            file.WriteLine("\t\t\t\t}");
            file.WriteLine();
            file.WriteLine("\t\t\t\tWrite-Verbose (\"Logged in to '{0}'.\" -f $sitecoreDownloadUrl)");
            file.WriteLine("\t\t\t}");
            file.WriteLine();
            file.WriteLine("\t\t\t# Download package using saved session");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath -WebSession $sitecoreDownloadSession -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t\telse");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\t# Download package");
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath -UseBasicParsing");
            file.WriteLine("\t\t}");
            file.WriteLine("\t}");
            file.WriteLine("}");
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
            file.WriteLine(".\\DownloadandSetupAllPrereqs.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \""+ txtUser.Text + "\" -SitecorePassword \"" + txtPass.Text + "\"");
            file.WriteLine("Expand-Archive -Force -LiteralPath Sitecore.Commerce.WDP.2020.08-6.0.238.zip -DestinationPath .");
            file.WriteLine("Expand-Archive -Force -LiteralPath Sitecore.Commerce.WDP.2020.08-6.0.238\\SIF.Sitecore.Commerce.5.0.49.zip -DestinationPath .\\Sitecore.Commerce.WDP.2020.08-6.0.238\\SIF.Sitecore.Commerce.5.0.49");
            file.WriteLine("Invoke-WebRequest -Uri \"https://www.nuget.org/api/v2/package/MSBuild.Microsoft.VisualStudio.Web.targets/14.0.0.3\" -OutFile \".\\Sitecore.Commerce.WDP.2020.08-6.0.238\\msbuild.microsoft.visualstudio.web.targets.14.0.0.3.zip\"");
            file.WriteLine("Copy-Item -Force -Path \"Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip\" -Destination \".\\Sitecore.Commerce.WDP.2020.08-6.0.238\\Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip\"");
            file.WriteLine(
                "Copy-Item -Force -Path \"Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip\" -Destination \".\\Sitecore.Commerce.WDP.2020.08-6.0.238\\Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip\"");
            file.WriteLine(
                "Expand-Archive -Force -LiteralPath Sitecore.Commerce.WDP.2020.08-6.0.238\\msbuild.microsoft.visualstudio.web.targets.14.0.0.3.zip -DestinationPath .\\Sitecore.Commerce.WDP.2020.08-6.0.238\\msbuild");
            file.WriteLine(
                "Invoke-WebRequest -Uri \"https://download.visualstudio.microsoft.com/download/pr/854cbd11-4b96-4a44-9664-b95991c0c4f7/8ec4944a5bd770faba2f769e647b1e6e/dotnet-hosting-3.1.8-win.exe\"  -OutFile dotnet-hosting-3.1.8-win.exe -UseBasicParsing");
            file.WriteLine(
                "Start-Process -FilePath dotnet-hosting-3.1.8-win.exe");            
            file.WriteLine("msiexec /i \"https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi\"");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

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


            WriteWorkerFile(".\\DownloadandSetupAllPrereqs.ps1");
            WriteMainFile(".\\DownloadandExpandZip.ps1");
            CommonFunctions.LaunchPSScript(".\\DownloadandExpandZip.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + txtUser.Text + "\" -SitecorePassword \"" + txtPass.Text + "\"");
        }
    }
}
