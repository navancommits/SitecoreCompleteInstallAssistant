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
    public partial class SifPrerequisites : Form
    {
        bool AllChecked = true;
        List<VersionPrerequisites> prereqs;
        string destFolder = string.Empty;
        DBServerDetails dbServer;
        const string zipType = "sitecoredevsetup";
        string version = "10.0";
        bool xpoFile = true;
        ZipVersions zipVersions = null;
        public SifPrerequisites(DBServerDetails dbServerDetails)
        {
            InitializeComponent();
            dbServer = dbServerDetails;
            CommonFunctions.ConnectionString = CommonFunctions.BuildConnectionString(dbServer.Server, "SCIA_DB", dbServer.Username, dbServer.Password);
            version = Version.SitecoreVersion;
            this.Text = this.Text + " for Sitecore v" + version;
            

            switch (Version.SitecoreVersion)
            {
                case "10.0":
                case "10.0.1":
                case "9.3":
                case "9.2":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);
                    zipVersions = CommonFunctions.GetZipVersionData(Version.SitecoreVersion, "sitecoredevsetup");
                    prereqs = CommonFunctions.GetVersionPrerequisites(version, "sitecoredevsetup");
                    xpoFile = false;
                    break;
                case "9.1":
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    destFolder = CommonFunctions.GetZipNamefromWdpVersion("sitecoresif", Version.SitecoreVersion);
                    zipVersions = CommonFunctions.GetZipVersionData(Version.SitecoreVersion, "sitecoresif");
                    prereqs = CommonFunctions.GetVersionPrerequisites(version, "sitecoresif");
                    break;
                default:
                    break;
            }

            chkSitecoreSetup.Text = destFolder + " Folder";
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Prerequisites Available";
            }
            else
            {
                lblStatus.ForeColor = Color.Red;
                lblStatus.Text = "One or more missing Prerequisites....";
            }
        }

        private void CheckPrerequisites()
        {

            if (!CommonFunctions.FileSystemEntryExists(destFolder, null, "folder", false)) { AllChecked = false; return; }
            if (!CommonFunctions.FileSystemEntryExists("license.xml",null, "file")) { AllChecked = false; return; }
            chkSitecoreSetup.Checked = true;
            chkSitecoreSetup.BackColor = Color.LightGreen;
            chkLicense.Checked = true;
            chkLicense.BackColor = Color.LightGreen;
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
            file.WriteLine("if (-not(Test-Path \"" + ZipList.SitecoreDevSetupZip + ".zip\" -PathType Leaf)) {");
            file.WriteLine("$preference = $ProgressPreference");
            file.WriteLine("$ProgressPreference = \"SilentlyContinue\"");
            file.WriteLine("$sitecoreDownloadUrl = \"https://sitecoredev.azureedge.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"" + zipVersions.ZipName + ".zip\" = \"" + zipVersions.Url + "\"");            
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
            file.WriteLine("if (-not(Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("}");
            file.WriteLine("Expand-Archive -Force -LiteralPath '" + zipVersions.ZipName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine("Copy-Item -Force -Path \"license.xml\" -Destination \".\\" + zipVersions.ZipName + "\\license.xml\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        void WriteXP0MainFile(string path)
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
            file.WriteLine("if (-not(Test-Path '" + zipVersions.ZipName + ".zip' -PathType Leaf))");
            file.WriteLine("{");
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllXP0SIFPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("}");
            file.WriteLine("Expand-Archive -Force -LiteralPath '" + zipVersions.ZipName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + "' -PathType Container))");
            file.WriteLine("\t{");
            file.WriteLine("\tExpand-Archive -Force -LiteralPath '.\\" + destFolder + "\\" + prereqs.Where(p => p.PrerequisiteKey == "XP0").ToList().FirstOrDefault().PrerequisiteName + ".zip' -DestinationPath \".\\" + zipVersions.ZipName + "\"");
            file.WriteLine("}");
            file.WriteLine("if ((Test-Path '" + zipVersions.ZipName + "' -PathType Container))");
            file.WriteLine("{");
            file.WriteLine("Copy-Item -Force -Path \"license.xml\" -Destination \".\\" + zipVersions.ZipName + "\\"  + "\\license.xml\"");
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
            if (CommonFunctions.FileSystemEntryExists(destFolder, null,"folder")) return;
            if (!CommonFunctions.FileSystemEntryExists("license.xml", null, "file")) { 
                SetStatusMessage("License file missing in the exe location...", Color.Red); 
                return; 
            }
           
            switch (Version.SitecoreVersion)
            {
                case "10.0.1":
                case "10.0":
                case "9.3":
                case "9.2":
                    WriteWorkerFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllPrereqs.ps1");
                    WriteMainFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSifZip.ps1");

                    CommonFunctions.LaunchPSScript(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSifZip -InstallSourcePath \".\"");
                    break;
                case "9.1":
                case "9.0":
                case "9.0.1":
                case "9.0.2":
                    WriteWorkerFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllXP0SIFPrereqs.ps1");
                    WriteXP0MainFile(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandXP0SifZip.ps1");

                    CommonFunctions.LaunchPSScript(@".\" + SCIASettings.FilePrefixAppString + "DownloadandExpandXP0SifZip -InstallSourcePath \".\"");
                    break;
                default:
                    break;
            }
            

            
        }

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            if (CommonFunctions.FileSystemEntryExists(destFolder, null, "folder"))
            {
                CommonFunctions.LaunchPSScript(@"Install-SitecoreConfiguration -Path .\Prerequisites.json", destFolder);
            }
        }

    }
}
