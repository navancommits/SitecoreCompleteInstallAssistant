using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCIA
{
    public partial class Prerequisites : Form
    {
        bool AllChecked = true;
        string destFolder = string.Empty;
        DBServerDetails dbServer;
        const string zipType = "commerce";
        const string commerceZipKey= "commercezip";
        const string commerceEngineZipKey = "commerceengine";
        const string commerceEngineSdkZipKey = "commerceenginesdk";
        const string BizFxZipKey = "bizfx";
        const string msBuildZipKey = "msbuild";
        const string psExtensionZipKey = "psextension";
        const string dotnethostKey = "dotnethost";
        const string sxaZipKey = "sxa";
        const string redisMsiKey = "redis";
        const string latestPSKey = "powershell";
        const string commerceSifZipKey = "sif";
        string version="10.0";
        List<VersionPrerequisites> prereqs;
        public Prerequisites(DBServerDetails dbServerDetails)
        {
            InitializeComponent();
            this.Width = 890;
            this.Height = 580;
            dbServer = dbServerDetails;
            CommonFunctions.ConnectionString = CommonFunctions.BuildConnectionString(dbServer.Server, "SCIA_DB", dbServer.Username, dbServer.Password);
            version= Version.SitecoreVersion;
            this.Text = this.Text + " for Sitecore v" + version;
            destFolder = CommonFunctions.GetZipNamefromWdpVersion(zipType,version);
            prereqs = CommonFunctions.GetVersionPrerequisites(version, zipType);
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

            if (!CommonFunctions.FileSystemEntryExists(destFolder, null, "folder",true)) { AllChecked = false;  return; }
            if (CommonFunctions.FileSystemEntryExists(destFolder + "\\" + msBuildZipKey, null,"folder",true)) { chkMsBuild.Checked = true; chkMsBuild.BackColor = Color.LightGreen;  }
            if (CommonFunctions.FileSystemEntryExists(destFolder,"SIF.Sitecore.Commerce.*","folder",true)) { chkSIF.Checked = true; chkSIF.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder + "\\Adventure Works Images.OnPrem.scwdp.zip")) { chkAdvworksImages.Checked = true; chkAdvworksImages.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Connect Core OnPrem *.zip")) { chkCommerceConnectCore.Checked = true; chkCommerceConnectCore.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Engine Connect OnPrem *.zip")) { chkCommerceEngConnect.Checked = true; chkCommerceEngConnect.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator *.zip")) { chkCommerceExperienceAccelerator.Checked = true; chkCommerceExperienceAccelerator.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Habitat Catalog *.zip")) { chkExperienceAcceleratorHabitat.Checked = true; chkExperienceAcceleratorHabitat.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Storefront *.zip")) { chkCommerceExperienceAcceleratorStorefront.Checked = true; chkCommerceExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Storefront Themes *.zip")) { chkExperienceAcceleratorStorefront.Checked = true; chkExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce ExperienceAnalytics Core OnPrem *.zip")) { chkCommerceXACore.Checked = true; chkCommerceXACore.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce ExperienceProfile Core OnPrem *.zip")) { chkExperienceProfile.Checked = true; chkExperienceProfile.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Marketing Automation Core OnPrem *.zip")) { chkCommerceMarketingAutomationCore.Checked = true; chkCommerceMarketingAutomationCore.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Marketing Automation for AutomationEngine *.zip")) { chkMarketingAutomationAutomationEngine.Checked = true; chkMarketingAutomationAutomationEngine.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, prereqs.Where(p => p.PrerequisiteKey == sxaZipKey).ToList().FirstOrDefault().PrerequisiteName)) { chkSitecoreExperienceAccelerator.Checked = true; chkSitecoreExperienceAccelerator.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.BizFx.OnPrem.*.zip")) { chkBizFxOnPrem.Checked = true; chkBizFxOnPrem.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.BizFX.SDK.*.zip")) { chkBizFxSdk.Checked = true; chkBizFxSdk.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.Commerce.Engine.OnPrem.Solr.*.zip")) { chkCommerceEngineSolr.Checked = true; chkCommerceEngineSolr.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder,"Sitecore.Commerce.Habitat.Images.*.zip")) { chkHabitatImages.Checked = true; chkHabitatImages.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists(destFolder, prereqs.Where(p => p.PrerequisiteKey == psExtensionZipKey).ToList().FirstOrDefault().PrerequisiteName)) { chkPowershellExtensions.Checked = true; chkPowershellExtensions.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.8",null,"folder") || CommonFunctions.FileSystemEntryExists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.7", null, "folder")) { chkCoreRuntime.Checked = true; chkCoreRuntime.BackColor = Color.LightGreen; }
            if (CommonFunctions.FileSystemEntryExists("C:\\Program Files\\Redis", null, "folder")) { chkRedis.Checked = true; chkRedis.BackColor = Color.LightGreen; }
        }


        private bool FolderExists(string filePath)
        {
            if (Directory.Exists(filePath)) return true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "One or more missing Pre-requisites: " + filePath;
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


        private void btnInstaller_Click(object sender, EventArgs e)
        {
            Process.Start(prereqs.Where(p => p.PrerequisiteKey == dotnethostKey).ToList().FirstOrDefault().PrerequisiteUrl);
        }

        private void linkLabelSitecoreCommerce_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            linkLabelSitecoreCommerce.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://sitecoredev.azureedge.net/Downloads/Sitecore_Commerce/100/Sitecore_Experience_Commerce_100.aspx");
        }

        private void linkLabelMsBuild_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelMsBuild.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            //System.Diagnostics.Process.Start("https://www.nuget.org/packages/MSBuild.Microsoft.VisualStudio.Web.targets/");

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start(prereqs.Where(p => p.PrerequisiteKey == redisMsiKey).ToList().FirstOrDefault().PrerequisiteUrl);
        }

        private void linklabelCoreRuntime_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linklabelCoreRuntime.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://dotnet.microsoft.com/download/dotnet-core/thank-you/runtime-aspnetcore-3.1.8-windows-hosting-bundle-installer");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel2.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://sitecoredev.azureedge.net/Downloads/Sitecore_Experience_Accelerator/10x/Sitecore_Experience_Accelerator_1000.aspx");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://sitecoredev.azureedge.net/Downloads/Sitecore_Experience_Accelerator/10x/Sitecore_Experience_Accelerator_1000.aspx");
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel4.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            Process.Start("https://www.braintreepayments.com/sandbox");

        }

        private void linkLabel5_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WritePSFile(".\\" + SCIASettings.FilePrefixAppString + "PSVersion7.ps1");
            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "PSVersion7.ps1");
        }

        void WritePSFile(string path)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("if (($PSVersionTable.PSVersion.Major -lt 6)) {");
            file.WriteLine("iex \"& { $(irm " + prereqs.Where(p => p.PrerequisiteKey == latestPSKey).ToList().FirstOrDefault().PrerequisiteUrl + ") } -UseMSI\"");
            file.WriteLine("}");
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
            file.WriteLine("$sitecoreDownloadUrl = \"https://sitecoredev.azureedge.net\"");
            file.WriteLine("$packages = @{");
            file.WriteLine("\"" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip\" = \""+ prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteUrl + "\"");
            file.WriteLine("\"" + prereqs.Where(p => p.PrerequisiteKey == sxaZipKey).ToList().FirstOrDefault().PrerequisiteName + "\" = \"" + prereqs.Where(p => p.PrerequisiteKey == sxaZipKey).ToList().FirstOrDefault().PrerequisiteUrl + "\"");
            file.WriteLine("\"" + prereqs.Where(p => p.PrerequisiteKey == psExtensionZipKey).ToList().FirstOrDefault().PrerequisiteName + "\" = \"" + prereqs.Where(p => p.PrerequisiteKey == psExtensionZipKey).ToList().FirstOrDefault().PrerequisiteUrl + "\"");
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
            if (Version.SitecoreVersion == "10.0")
            {
                file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .");
            }
            else
            {
                file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .\\" + ZipList.CommerceZip);
            }            
            file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceSifZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .\\" + ZipList.CommerceZip + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceSifZipKey).ToList().FirstOrDefault().PrerequisiteName);
            file.WriteLine("Invoke-WebRequest -Uri \"" + prereqs.Where(p => p.PrerequisiteKey == msBuildZipKey).ToList().FirstOrDefault().PrerequisiteUrl + "\" -OutFile \".\\" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == msBuildZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip\"");
            file.WriteLine("Copy-Item -Force -Path \"" + prereqs.Where(p => p.PrerequisiteKey == sxaZipKey).ToList().FirstOrDefault().PrerequisiteName + "\" -Destination \".\\" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == sxaZipKey).ToList().FirstOrDefault().PrerequisiteName + "\"");
            file.WriteLine(
                "Copy-Item -Force -Path \"" + prereqs.Where(p => p.PrerequisiteKey == psExtensionZipKey).ToList().FirstOrDefault().PrerequisiteName + "\" -Destination \".\\" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == psExtensionZipKey).ToList().FirstOrDefault().PrerequisiteName + "\"");
            file.WriteLine(
                "Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == msBuildZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath '.\\" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + msBuildZipKey  + "'");
            if (Version.SitecoreVersion == "9.1.1")
            {
                file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceEngineZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .\\" + ZipList.CommerceZip + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceEngineZipKey).ToList().FirstOrDefault().PrerequisiteName);

                file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceEngineSdkZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .\\" + ZipList.CommerceZip + "\\" + prereqs.Where(p => p.PrerequisiteKey == commerceEngineSdkZipKey).ToList().FirstOrDefault().PrerequisiteName);

                file.WriteLine("Expand-Archive -Force -LiteralPath " + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + "\\" + prereqs.Where(p => p.PrerequisiteKey == BizFxZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip -DestinationPath .\\" + ZipList.CommerceZip + "\\" + prereqs.Where(p => p.PrerequisiteKey == BizFxZipKey).ToList().FirstOrDefault().PrerequisiteName);
            }
            file.WriteLine(
                "Invoke-WebRequest -Uri \"" + prereqs.Where(p => p.PrerequisiteKey == dotnethostKey).ToList().FirstOrDefault().PrerequisiteUrl + "\"  -OutFile " + prereqs.Where(p => p.PrerequisiteKey == dotnethostKey).ToList().FirstOrDefault().PrerequisiteName + " -UseBasicParsing");
            file.WriteLine(
                "Start-Process -FilePath " + prereqs.Where(p => p.PrerequisiteKey == dotnethostKey).ToList().FirstOrDefault().PrerequisiteName);
            if (Version.SitecoreVersion != "9.1.1")
            {
                file.WriteLine("msiexec /i \"" + prereqs.Where(p => p.PrerequisiteKey == redisMsiKey).ToList().FirstOrDefault().PrerequisiteUrl + "\"");
            }
            file.WriteLine();
            file.WriteLine(
                "$ProgressPreference = $preference");
            file.WriteLine(
                "Write-Host \"DONE\"");

            file.Dispose();
        }

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (CommonFunctions.FileSystemEntryExists(@".\\" + prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName,null,"folder")) return;
            var commerceZipName = prereqs.Where(p => p.PrerequisiteKey == commerceZipKey).ToList().FirstOrDefault().PrerequisiteName + ".zip";

            WriteWorkerFile(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllCommercePrereqs.ps1");
            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "DownloadandSetupAllCommercePrereqs.ps1 -InstallSourcePath \".\"");
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            WriteMainFile(".\\" + SCIASettings.FilePrefixAppString + "SetupAllCommercePrereqs.ps1");
            CommonFunctions.LaunchPSScript(".\\" + SCIASettings.FilePrefixAppString + "SetupAllCommercePrereqs.ps1 -InstallSourcePath \".\"");
        }
    }
}
