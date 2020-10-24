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
using WindowsInstaller;

namespace SCIA
{
    public partial class Prerequisites : Form
    {
        bool AllChecked = true;
        public Prerequisites()
        {
            InitializeComponent();
            this.Width = 900;
            this.Height = 470;
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

        private int _OnExternalUI(IntPtr context, uint messageType, string message)
        {
            MsiInstallMessage msg =
              (MsiInstallMessage)(MsiInterop.MessageTypeMask & messageType);

            Debug.WriteLine(string.Format("MSI:  {0} {1}", msg, message));

            try
            {
                switch (msg)
                {
                    case MsiInstallMessage.ActionData:
                        //   set a label's text to the message

                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.ActionStart:
                        //   set a label's text to the message, with the
                        //   message.Substring(message.LastIndexOf(".") + 1);
                        //   being the action start description

                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.CommonData:
                        string[] data = _ParseCommonData(message);

                        if (data != null && data[0] != null)
                        {
                            switch (data[0][0])
                            {
                                case '0':   //   language
                                    break;

                                case '1':   //   caption
                                            //   store data[1] for dialog captions

                                    break;

                                case '2':   //   CancelShow
                                    //if ("0" == data[1])
                        //   hide / disable the "cancel" button
                     //else
                                                //   show / enable the cancel button

                                                break;

                                default: break;
                            }
                        }

                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.Error:
                        return (int)MessageBox.Show(message,
                           "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    case MsiInstallMessage.FatalExit:
                        return (int)MessageBox.Show(message,
                           "Fatal Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    case MsiInstallMessage.FilesInUse:
                        //   display in use files in a dialog, informing the user
                        //   that they should close whatever applications are using
                        //   them.  You must return the DialogResult to the service
                        //   if displayed.

                        Application.DoEvents();

                        return 0;   //   we didn't handle it in this case!

                    case MsiInstallMessage.Info:
                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.Initialize:
                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.OutOfDiskSpace:
                        Application.DoEvents();

                        break;

                    case MsiInstallMessage.Progress:
                        string[] fields = _ParseProgressString(message);

                        if (null == fields || null == fields[0])
                        {
                            Application.DoEvents();

                            return (int)DialogResult.OK;
                        }

                        switch (fields[0][0])
                        {
                            case '0':   //   reset progress bar
                                        //   1 = total, 2 = direction , 3 = in progress, 4 = state

                                break;

                            case '1':   //   action info
                                        //   1 = # ticks for the step size, 2 = actuall step it?

                                break;

                            case '2':   //   progress
                                        //   1 = how far the progress bar moved,
                                        //   forward / backward, based on case '0'

                                break;

                            default: break;
                        }

                        Application.DoEvents();

                        //if (/*  the user cancelled */)
                            return (int)DialogResult.Cancel;
                        //else
                        //    return (int)DialogResult.OK;

                    case MsiInstallMessage.ResolveSource:
                        Application.DoEvents();

                        return 0;

                    case MsiInstallMessage.ShowDialog:
                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.Terminate:
                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.User:
                        //   get message, parse

                        Application.DoEvents();

                        return (int)DialogResult.OK;

                    case MsiInstallMessage.Warning:
                        return (int)MessageBox.Show(message,
                           "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    default: break;
                }
            }
            catch (Exception e)
            {
                //   do something meaningful, but don't rethrow here.
                Debug.WriteLine("EXCEPTION -- " + e.ToString());
            }

            Application.DoEvents();

            return 0;
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
