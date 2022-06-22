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
    public partial class SiaPrerequisitesInstaller : Form
    {
        public SiaPrerequisitesInstaller()
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
            file.WriteLine("\"Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64).zip\" = \"https://dev.sitecore.net/~/media/A74E47524738460B83332BAE82F123D1.ashx\"");
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
            file.WriteLine(".\\DownloadandSetupSiaPrereqs.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \"" + txtUser.Text + "\" -SitecorePassword \"" + txtPass.Text + "\"");
            file.WriteLine("Expand-Archive -Force -LiteralPath \"Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64).zip\" -DestinationPath .");
            file.WriteLine(
                "Start-Process -FilePath \".\\Sitecore 10.0.0 rev. 004346 Setup\\setup.exe\"");
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

            if (File.Exists(@".\\Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe"))
            {
                System.Diagnostics.Process.Start(@"Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)\setup.exe");
                
            }
            else
            {
                WriteWorkerFile(".\\DownloadandSetupSiaPrereqs.ps1");
                WriteMainFile(".\\DownloadandExpandSiaZip.ps1");
                CommonFunctions.LaunchPSScript(".\\DownloadandExpandSiaZip.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + txtUser.Text + "\" -SitecorePassword \"" + txtPass.Text + "\"");
            }
        }
    }
}
