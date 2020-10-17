using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace SCIA
{
    public partial class Prerequisites : Form
    {
        bool AllChecked = true;
        public Prerequisites()
        {
            InitializeComponent();
            CheckPrerequisites();
            if (AllChecked)
            {
                lblStatus.ForeColor = Color.DarkGreen;
                lblStatus.Text = "All Pre-requisites Available";
            }
        }

        private void CheckPrerequisites()
        {
            if (FolderExists("..\\msbuild.microsoft.visualstudio.web.targets.14.0.0.3"))  chkMsBuild.Checked = true;
            if (FolderExists("..\\SIF.Sitecore.Commerce.5.0.49")) chkSIF.Checked = true;
            if (FolderExists("..\\Sitecore.Commerce.Engine.SDK.6.0.130")) chkCommerceEngSdk.Checked = true;
            if (FileExists("..\\Adventure Works Images.OnPrem.scwdp.zip")) chkAdvworksImages.Checked = true;
            if (FileExists("..\\Sitecore Commerce Connect Core OnPrem 15.0.26.scwdp.zip")) chkCommerceConnectCore.Checked = true;
            if (FileExists("..\\Sitecore Commerce Engine Connect OnPrem 6.0.77.scwdp.zip")) chkCommerceEngConnect.Checked = true;
            if (FileExists("..\\Sitecore Commerce Experience Accelerator 5.0.106.scwdp.zip")) chkCommerceExperienceAccelerator.Checked = true;
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Habitat Catalog 5.0.106.scwdp.zip")) chkExperienceAcceleratorHabitat.Checked = true;
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Storefront 5.0.106.scwdp.zip")) chkCommerceExperienceAcceleratorStorefront.Checked = true;
            if (FileExists("..\\Sitecore Commerce Experience Accelerator Storefront Themes 5.0.106.scwdp.zip")) chkExperienceAcceleratorStorefront.Checked = true;
            if (FileExists("..\\Sitecore Commerce ExperienceAnalytics Core OnPrem 15.0.26.scwdp.zip")) chkCommerceXACore.Checked = true;
            if (FileExists("..\\Sitecore Commerce ExperienceProfile Core OnPrem 15.0.26.scwdp.zip")) chkExperienceProfile.Checked = true;
            if (FileExists("..\\Sitecore Commerce Marketing Automation Core OnPrem 15.0.26.scwdp.zip")) chkCommerceMarketingAutomationCore.Checked = true;
            if (FileExists("..\\Sitecore Commerce Marketing Automation for AutomationEngine 15.0.26.zip")) chkMarketingAutomationAutomationEngine.Checked = true;
            if (FileExists("..\\Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip")) chkSitecoreExperienceAccelerator.Checked = true;
            if (FileExists("..\\Sitecore.BizFx.OnPrem.5.0.12.scwdp.zip")) chkBizFxOnPrem.Checked = true;
            if (FileExists("..\\Sitecore.BizFX.SDK.5.0.12.zip")) chkBizFxSdk.Checked = true;
            if (FileExists("..\\Sitecore.Commerce.Engine.OnPrem.Solr.6.0.238.scwdp.zip")) chkCommerceEngineSolr.Checked = true;
            if (FileExists("..\\Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip")) chkHabitatImages.Checked = true;
            if (FileExists("..\\Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip")) chkPowershellExtensions.Checked = true;
            
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
    }
}
