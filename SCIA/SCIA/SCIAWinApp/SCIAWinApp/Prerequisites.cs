using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace SCIA
{
    public partial class Prerequisites : Form
    {
        bool AllChecked = true;
        string destFolder = @".\Sitecore.Commerce.WDP.2020.08-6.0.238\";
        public Prerequisites()
        {
            InitializeComponent();
            this.Width = 890;
            this.Height = 580;
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Pre-requisites Available";
            }
        }

        private void CheckPrerequisites()
        {
            if (FolderExists(destFolder + "\\msbuild")) { chkMsBuild.Checked = true; chkMsBuild.BackColor = Color.LightGreen; }
            if (FolderExists(destFolder + "\\SIF.Sitecore.Commerce.5.0.49")) { chkSIF.Checked = true; chkSIF.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Adventure Works Images.OnPrem.scwdp.zip")) { chkAdvworksImages.Checked = true; chkAdvworksImages.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Connect Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceConnectCore.Checked = true; chkCommerceConnectCore.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Engine Connect OnPrem 6.0.77.scwdp.zip")) { chkCommerceEngConnect.Checked = true; chkCommerceEngConnect.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Experience Accelerator 5.0.106.scwdp.zip")) { chkCommerceExperienceAccelerator.Checked = true; chkCommerceExperienceAccelerator.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Experience Accelerator Habitat Catalog 5.0.106.scwdp.zip")) { chkExperienceAcceleratorHabitat.Checked = true; chkExperienceAcceleratorHabitat.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Experience Accelerator Storefront 5.0.106.scwdp.zip")) { chkCommerceExperienceAcceleratorStorefront.Checked = true; chkCommerceExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Experience Accelerator Storefront Themes 5.0.106.scwdp.zip")) { chkExperienceAcceleratorStorefront.Checked = true; chkExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce ExperienceAnalytics Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceXACore.Checked = true; chkCommerceXACore.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce ExperienceProfile Core OnPrem 15.0.26.scwdp.zip")) { chkExperienceProfile.Checked = true; chkExperienceProfile.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Marketing Automation Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceMarketingAutomationCore.Checked = true; chkCommerceMarketingAutomationCore.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Commerce Marketing Automation for AutomationEngine 15.0.26.zip")) { chkMarketingAutomationAutomationEngine.Checked = true; chkMarketingAutomationAutomationEngine.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip")) { chkSitecoreExperienceAccelerator.Checked = true; chkSitecoreExperienceAccelerator.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.BizFx.OnPrem.5.0.12.scwdp.zip")) { chkBizFxOnPrem.Checked = true; chkBizFxOnPrem.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.BizFX.SDK.5.0.12.zip")) { chkBizFxSdk.Checked = true; chkBizFxSdk.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.Commerce.Engine.OnPrem.Solr.6.0.238.scwdp.zip")) { chkCommerceEngineSolr.Checked = true; chkCommerceEngineSolr.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip")) { chkHabitatImages.Checked = true; chkHabitatImages.BackColor = Color.LightGreen; }
            if (FileExists(destFolder + "\\Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip")) { chkPowershellExtensions.Checked = true; chkPowershellExtensions.BackColor = Color.LightGreen; }
            if (FolderExists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.8") || FolderExists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.7")) { chkCoreRuntime.Checked = true; chkCoreRuntime.BackColor = Color.LightGreen; }
            if (FolderExists("C:\\Program Files\\Redis")) { chkRedis.Checked = true; chkRedis.BackColor = Color.LightGreen; }
        }

        private bool FolderExists(string folderPath)
        {
            if (Directory.Exists(folderPath)) return true;
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "One or more missing Pre-requisites: " + folderPath;
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
            Process.Start("https://download.visualstudio.microsoft.com/download/pr/854cbd11-4b96-4a44-9664-b95991c0c4f7/8ec4944a5bd770faba2f769e647b1e6e/dotnet-hosting-3.1.8-win.exe");
            //IntPtr parent = IntPtr.Zero;
            //MsiInstallUILevel oldLevel =
            //  MsiInterop.MsiSetInternalUI(MsiInstallUILevel.None |
            //  MsiInstallUILevel.SourceResOnly, ref parent);
            //MsiInstallUIHandler oldHandler = null;

            //try
            //{
            //    oldHandler =
            //      MsiInterop.MsiSetExternalUI(new
            //      MsiInstallUIHandler(_OnExternalUI),
            //      MsiInstallLogMode.ExternalUI, IntPtr.Zero);

            //    Application.DoEvents();

            //    MsiError ret =
            //      MsiInterop.MsiOpenPackage("",
            //      out parent);

            //    if (ret != MsiError.Success)
            //        throw new
            //        ApplicationException(string.Format("Failed to install -- {0}", ret));
            //}
            //catch (Exception ex)
            //{
            //    Debug.WriteLine("EXCEPTION -- " + ex.ToString());
            //    //   do something meaningful
            //}
            //finally
            //{
            //    if (oldHandler != null)
            //        MsiInterop.MsiSetExternalUI(oldHandler,
            //          MsiInstallLogMode.None, IntPtr.Zero);

            //    MsiInterop.MsiSetInternalUI(oldLevel, ref parent);
            //}
        }

        private void linkLabelSitecoreCommerce_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

            linkLabelSitecoreCommerce.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://dev.sitecore.net/Downloads/Sitecore_Commerce/100/Sitecore_Experience_Commerce_100.aspx");
        }

        private void linkLabelMsBuild_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabelMsBuild.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://www.nuget.org/packages/MSBuild.Microsoft.VisualStudio.Web.targets/");
        
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel1.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi");
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
            System.Diagnostics.Process.Start("https://dev.sitecore.net/Downloads/Sitecore_Experience_Accelerator/10x/Sitecore_Experience_Accelerator_1000.aspx");
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            linkLabel3.LinkVisited = true;
            //Call the Process.Start method to open the default browser
            //with a URL:
            System.Diagnostics.Process.Start("https://dev.sitecore.net/Downloads/Sitecore_Experience_Accelerator/10x/Sitecore_Experience_Accelerator_1000.aspx");
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
            WritePSFile(".\\PSVersion7.ps1");
            CommonFunctions.LaunchPSScript(".\\PSVersion7.ps1");
        }

        void WritePSFile(string path)
        {
            using var file = new StreamWriter(path);

            file.WriteLine("if (($PSVersionTable.PSVersion.Major -lt 6)) {");
            file.WriteLine("iex \"& { $(irm https://aka.ms/install-powershell.ps1) } -UseMSI\"");
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
            file.WriteLine(".\\DownloadandSetupAllPrereqs.ps1 -InstallSourcePath $InstallSourcePath -SitecoreUsername \"" + Login.username + "\" -SitecorePassword \"" + Login.password + "\"");
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

        private void linkLabel6_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //PrerequisitesInstaller prerequisitesInstaller = new PrerequisitesInstaller();
            //prerequisitesInstaller.ShowDialog();
            if (!Login.Success)
            {
                SetStatusMessage("Login to Sdn from menubar...", Color.Red);
                return;
            }

            if (FolderExists(@".\\Sitecore.Commerce.WDP.2020.08-6.0.238")) return;

            WriteWorkerFile(".\\DownloadandSetupAllPrereqs.ps1");
            WriteMainFile(".\\DownloadandExpandZip.ps1");
            CommonFunctions.LaunchPSScript(".\\DownloadandExpandZip.ps1 -InstallSourcePath \".\" -SitecoreUsername \"" + Login.username + "\" -SitecorePassword \"" + Login.password + "\"");
        }
    }
}
