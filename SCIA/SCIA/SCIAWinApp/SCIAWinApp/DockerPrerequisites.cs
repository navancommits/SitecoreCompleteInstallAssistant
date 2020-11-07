using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SCIA
{
    public partial class DockerPrerequisites : Form
    {
        bool AllChecked = true;
        string destFolder = @".";
        public DockerPrerequisites()
        {
            InitializeComponent();
            CheckPrerequisites();

            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Pre-requisites Available";
            }
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
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
            file.WriteLine("\"Sitecore.Commerce.Container.SDK.1.0.214.zip\" = \"https://dev.sitecore.net/~/media/FB50C51D304C47E89EB1C21C087B9B73.ashx\"");
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
            file.WriteLine(".\\DownloadandSetupAllContainerPrereqs.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \"" + Login.username + "\" -SitecorePassword \"" + Login.password + "\"");
            file.WriteLine("Expand-Archive -Force -LiteralPath Sitecore.Commerce.Container.SDK.1.0.214.zip -DestinationPath .\\Sitecore.Commerce.Container.SDK.1.0.214");
            file.WriteLine();
            file.WriteLine("if (-not(Test-Path -Path 'C:\\program files\\docker' -PathType Container)) {");
            file.WriteLine(
                "\tInvoke-WebRequest -Uri \"https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe\"  -OutFile \"Docker Desktop Installer.exe\" -UseBasicParsing");
            file.WriteLine(
                "\tStart-Process -FilePath \"Docker Desktop Installer.exe\"");
            file.WriteLine("}");
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //ContainerPrerequisitesInstaller containerPrerequisitesInstaller = new ContainerPrerequisitesInstaller(siteDetails);
            //containerPrerequisitesInstaller.ShowDialog();
            if (FolderExists(@".\\Sitecore.Commerce.Container.SDK.1.0.214") && FolderExists(@"C:\\program files\\docker")) return;

            if (!Login.Success)
            {
                SetStatusMessage("Login to Sdn from menubar...", Color.Red);
                return;
            }

            WriteWorkerFile(".\\DownloadandSetupAllContainerPrereqs.ps1");
            WriteMainFile(".\\DownloadandExpandContainerZip.ps1");
            CommonFunctions.LaunchPSScript(".\\DownloadandExpandContainerZip.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + Login.username + "\" -SitecorePassword \"" + Login.password + "\"");
        }

        private void CheckPrerequisites()
        {
            if (FolderExists(destFolder + "\\Sitecore.Commerce.Container.SDK.1.0.214")) { chkSitecoreCommerceContainer.Checked = true; chkSitecoreCommerceContainer.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.Commerce.Container.SDK.1.0.214\\xc0\\license.xml")) { chkLicenseFile.Checked = true; chkLicenseFile.BackColor = Color.LightGreen; }
           if(WindowsVersionOk()) { chkWindowsEdition.Checked = true; chkWindowsEdition.BackColor = Color.LightGreen; };
            if (FolderExists("c:\\program files\\docker")) { chkDocker.Checked = true; chkDocker.BackColor = Color.LightGreen; }
        }

        private bool FolderExists(string folderPath)
        {
            if (Directory.Exists(folderPath)) return true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "One or more missing Pre-requisites: " + folderPath;
            AllChecked = false;
            return false;
        }

        private bool WindowsVersionOk()
        {
            string version = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion", "ProductName", null);
            if (version == "Windows 10 Pro" || version == "Windows 10 Enterprise") { return true; }
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Windows Edition must be Pro or Enterprise Build >=14372 for Docker Windows";
            AllChecked = false;
            return false;
        }

        private bool FileExists(string filePath)
        {
            if (File.Exists(filePath)) return true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "One or more missing Pre-requisites: " + filePath;
            AllChecked = false;
            return false;
        }

        private void chkLicenseFile_CheckedChanged(object sender, System.EventArgs e)
        {

        }
    }
    }
