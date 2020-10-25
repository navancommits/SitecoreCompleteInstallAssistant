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
        public Prerequisites()
        {
            InitializeComponent();
            this.Width = 890;
            this.Height = 570;
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Pre-requisites Available";
            }
        }

        private void CheckPrerequisites()
        {
            if (FolderExists("..\\msbuild.microsoft.visualstudio.web.targets.14.0.0.3")) { chkMsBuild.Checked = true; chkMsBuild.BackColor = Color.LightGreen; }
            if (FolderExists("..\\SIF.Sitecore.Commerce.5.0.49")) { chkSIF.Checked = true; chkSIF.BackColor = Color.LightGreen; }
            if (FileExists("..\\Adventure Works Images.OnPrem.scwdp.zip")) { chkAdvworksImages.Checked = true; chkAdvworksImages.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Connect Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceConnectCore.Checked = true; chkCommerceConnectCore.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Engine Connect OnPrem 6.0.77.scwdp.zip")) { chkCommerceEngConnect.Checked = true; chkCommerceEngConnect.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Experience Accelerator 5.0.106.scwdp.zip")) { chkCommerceExperienceAccelerator.Checked = true; chkCommerceExperienceAccelerator.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Habitat Catalog 5.0.106.scwdp.zip")) { chkExperienceAcceleratorHabitat.Checked = true; chkExperienceAcceleratorHabitat.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Storefront 5.0.106.scwdp.zip")) { chkCommerceExperienceAcceleratorStorefront.Checked = true; chkCommerceExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Storefront Themes 5.0.106.scwdp.zip")) { chkExperienceAcceleratorStorefront.Checked = true; chkExperienceAcceleratorStorefront.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce ExperienceAnalytics Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceXACore.Checked = true; chkCommerceXACore.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce ExperienceProfile Core OnPrem 15.0.26.scwdp.zip")) { chkExperienceProfile.Checked = true; chkExperienceProfile.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Marketing Automation Core OnPrem 15.0.26.scwdp.zip")) { chkCommerceMarketingAutomationCore.Checked = true; chkCommerceMarketingAutomationCore.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Commerce Marketing Automation for AutomationEngine 15.0.26.zip")) { chkMarketingAutomationAutomationEngine.Checked = true; chkMarketingAutomationAutomationEngine.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip")) { chkSitecoreExperienceAccelerator.Checked = true; chkSitecoreExperienceAccelerator.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore.BizFx.OnPrem.5.0.12.scwdp.zip")) { chkBizFxOnPrem.Checked = true; chkBizFxOnPrem.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore.BizFX.SDK.5.0.12.zip")) { chkBizFxSdk.Checked = true; chkBizFxSdk.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore.Commerce.Engine.OnPrem.Solr.6.0.238.scwdp.zip")) { chkCommerceEngineSolr.Checked = true; chkCommerceEngineSolr.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip")) { chkHabitatImages.Checked = true; chkHabitatImages.BackColor = Color.LightGreen; }
            if (FileExists("..\\Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip")) { chkPowershellExtensions.Checked = true; chkPowershellExtensions.BackColor = Color.LightGreen; }
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

        private string[] _ParseCommonData(string s)
        {
            string[] res = new string[3];
            Regex regex = new Regex(@"\d:\w+\s");
            int i = 0;

            foreach (Match m in regex.Matches(s))
            {
                if (i > 3) return null;

                res[i++] = m.Value.Substring(m.Value.IndexOf(":") + 1).Trim();
            }

            return res;
        }

        private string[] _ParseProgressString(string s)
        {
            string[] res = new string[4];
            Regex regex = new Regex(@"\d:\s\d+\s");
            int i = 0;

            foreach (Match m in regex.Matches(s))
            {
                if (i > 4) return null;

                res[i++] = m.Value.Substring(m.Value.IndexOf(":") + 2).Trim();
            }

            return res;
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
    }
}
