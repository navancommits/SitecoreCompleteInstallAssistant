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
            chkSitecoreCommerceContainer.Text = ZipList.CommerceContainerZip + " Folder";
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
            file.WriteLine("if (-not(Test-Path \"" + ZipList.CommerceContainerZip + ".zip\" -PathType Leaf)) {");
            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("$sitecoreDownloadUrl = \"https://sitecoredev.azureedge.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"" + ZipList.CommerceContainerZip + ".zip\" = '" + CommonFunctions.GetUrlfromWdpVersion("commercecon", Version.SitecoreVersion) + "'");
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
            file.WriteLine("\t\t\tInvoke-WebRequest -Uri $fileUrl -OutFile $filePath  -UseBasicParsing");
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
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllContainerPrereqs.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \"" + Login.username + "\" -SitecorePassword \"" + Login.password + "\"");
            file.WriteLine("Expand-Archive -Force -LiteralPath " +  ZipList.CommerceContainerZip + ".zip -DestinationPath .\\" + ZipList.CommerceContainerZip);
            file.WriteLine();
            file.WriteLine("if (-not(Test-Path -Path 'C:\\program files\\docker' -PathType Container)) {");
            file.WriteLine("if (-not(Test-Path -Path 'Docker Desktop Installer.exe' -PathType Leaf)) {");
            file.WriteLine(
                "\tInvoke-WebRequest -Uri \"https://desktop.docker.com/win/stable/Docker%20Desktop%20Installer.exe\"  -OutFile \"Docker Desktop Installer.exe\" -UseBasicParsing");
            file.WriteLine("}");
            file.WriteLine(
                "\tStart-Process -FilePath \"Docker Desktop Installer.exe\"");
            file.WriteLine("}");
            file.WriteLine("if ((Test-Path '" + ZipList.CommerceContainerZip + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine("Copy-Item -Force -Path \"license.xml\" -Destination \".\\" + ZipList.CommerceContainerZip + "\\xc0\\license.xml\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (!CommonFunctions.FileSystemEntryExists("license.xml", null, "file"))
            {
                SetStatusMessage("License file missing in the exe location...", Color.Red);
                return;
            }
            
            if (!CommonFunctions.FileSystemEntryExists(SCIASettings.FilePrefixAppString + "DownloadandSetupAllContainerPrereqs.ps1",null))
            {
                WriteWorkerFile(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllContainerPrereqs.ps1");
            }
            if (!CommonFunctions.FileSystemEntryExists(SCIASettings.FilePrefixAppString + "DownloadandExpandContainerZip.ps1", null))
            {
                WriteMainFile(".\\" + SCIASettings.FilePrefixAppString + "DownloadandExpandContainerZip.ps1");
            }
            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "DownloadandExpandContainerZip.ps1 -InstallSourcePath \".\"");
        }

        private void CheckPrerequisites()
        {
            if (CommonFunctions.FileSystemEntryExists(ZipList.CommerceContainerZip, null, "folder", true)) { chkSitecoreCommerceContainer.Checked = true; chkSitecoreCommerceContainer.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\" + ZipList.CommerceContainerZip + "\\xc0\\license.xml")) { chkLicenseFile.Checked = true; chkLicenseFile.BackColor = Color.LightGreen; }
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
            if (version == "Windows 10 Pro" || version.StartsWith("Windows 10 Enterprise")) { return true; }
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Windows Edition must be Pro or Enterprise Build for Docker Windows";
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

        private void chkSitecoreCommerceContainer_CheckedChanged(object sender, System.EventArgs e)
        {

        }
    }
    }
