using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace SCIA
{
    public partial class SiaPrerequisites : Form
    {
        bool AllChecked = false;
        ZipVersions zipVersions = null;
        public SiaPrerequisites()
        {
            InitializeComponent();
            chkSitecoreSetup.Text = ZipList.SitecoreDevSetupZip + " Folder";
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "Required Pre-requisites Available...";
            }

            switch (Version.SitecoreVersion)
            {
                default:
                    zipVersions = CommonFunctions.GetZipVersionData(Version.SitecoreVersion, "sitecoredevsetup");
                    break;
            }
        }

        private bool FolderExists(string folderPath)
        {
            if (Directory.Exists(folderPath)) return true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Missing Pre-requisite: " + folderPath + " folder";
            AllChecked = false;
            return false;
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void CheckPrerequisites()
        {
            if (!File.Exists(ZipList.SitecoreDevSetupZip + "\\setup.exe"))
            {
                SetStatusMessage("Missing Setup Exe - " + ZipList.SitecoreDevSetupZip + "\\setup.exe ", Color.Red);
                return;
            }
                
            chkSitecoreSetup.Checked = true; 
            chkSitecoreSetup.BackColor = Color.LightGreen;
            AllChecked = true;
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

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
            file.WriteLine("\"" + zipVersions.ZipName + ".zip\" = \"" + zipVersions.Url  + "\"");
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
            file.WriteLine(".\\" + SCIASettings.FilePrefixAppString +  "DownloadandSetupSiaPrereqs.ps1 -InstallSourcePath $InstallSourcePath");
            file.WriteLine("Expand-Archive -Force -LiteralPath \"" + ZipList.SitecoreDevSetupZip + ".zip\" -DestinationPath \"" + ZipList.SitecoreDevSetupZip + "\"");
            file.WriteLine(
                "Start-Process -FilePath \".\\" + ZipList.SitecoreDevSetupZip + "\\setup.exe\"");
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }



        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (File.Exists(@".\" + ZipList.SitecoreDevSetupZip + "\\setup.exe"))
            {
                System.Diagnostics.Process.Start(ZipList.SitecoreDevSetupZip + "\\setup.exe");

            }
            else
            {
                WriteWorkerFile(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupSiaPrereqs.ps1");
                WriteMainFile(".\\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSiaZip.ps1");
                CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "DownloadandExpandSiaZip.ps1 -InstallSourcePath \".\"");
            }
        }
    }
}
