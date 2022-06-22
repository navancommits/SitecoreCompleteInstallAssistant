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
    public partial class DeleteAllLauncher : Form
    {
        public DeleteAllLauncher()
        {
            InitializeComponent();
        }

        private void DeleteScript(string path)
        {
            var appcmdExe = "C:\\windows\\system32\\inetsrv\\appcmd.exe";
            var stoppedStatus = "Stopped";
            var sitePath = "IIS:\\Sites\\Default Web Site\\$SiteName";
            var webRootPath = txtSiteRootFolder.Text;
            var siteAppPool = "IIS:\\AppPools\\$SiteName";

            using var file = new StreamWriter(path);
            file.WriteLine("Param(");
            file.WriteLine("\t[string]$SiteName = \"" + txtSiteName.Text + "\"");
            file.WriteLine(")");
            file.WriteLine();

            file.WriteLine("Function Remove-Website{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$siteName");
            file.WriteLine("\t)");
            file.WriteLine("\t$appCmd=\"" + appcmdExe + "\"");
            file.WriteLine("\t& $appCmd delete site $siteName");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Stop-WebAppPool{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$appPoolName");
            file.WriteLine("\t)");
            file.WriteLine("\t\t$ApplicationPoolStatus = Get-WebAppPoolState $appPoolName");
            file.WriteLine("\t\t$ApplicationPoolStatusValue = $ApplicationPoolStatus.Value");
            file.WriteLine("\t\t#Write-Host \"$appPoolName-> $ApplicationPoolStatusValue\"");

            file.WriteLine("\t\tif ($ApplicationPoolStatus.Value -ne \"" + stoppedStatus + "\")");
            file.WriteLine("\t\t{");
            file.WriteLine("\t\t\tStop-WebAppPool -Name $appPoolName");
            file.WriteLine("\t\t}");
            file.WriteLine("}");
            file.WriteLine();

            file.WriteLine("Function Remove-AppPool{");
            file.WriteLine("\t[CmdletBinding()]");
            file.WriteLine("\tparam(");
            file.WriteLine("\t\t[string]$appPoolName");
            file.WriteLine("\t)");
            file.WriteLine("\t$appCmd=\"" + appcmdExe + "\"");
            file.WriteLine("\t& $appCmd delete apppool $appPoolName");
            file.WriteLine("}");

            file.WriteLine("if (Test-Path \"" + sitePath + "\") { Stop-Website -Name $SiteName }");
            file.WriteLine();

            file.WriteLine("if (Test-Path \"" + siteAppPool + "\") { Stop-WebAppPool -appPoolName $SiteName }");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting App Pools\"");
            file.WriteLine("Remove-AppPool -appPoolName $SiteName");
            file.WriteLine("Write-Host \"App Pool deleted successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting Websites from IIS\"");
            file.WriteLine("Remove-Website -siteName $SiteName");
            file.WriteLine("Write-Host \"IIS Websites deleted successfully\"");
            file.WriteLine();

            file.WriteLine("Write-Host \"Deleting Websites from wwwroot\"");
            file.WriteLine("rm " + webRootPath + "$SiteName -force -recurse -ea ig");
            file.WriteLine("Write-Host \"Websites removed from wwwroot\"");

            file.WriteLine("pop-location");
        }
        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtSiteName.Text)) { SetStatusMessage("Site Name required....", Color.Red); return; }
            if (string.IsNullOrEmpty(txtSiteRootFolder.Text)) { SetStatusMessage("Site Root Folder required....", Color.Red); return; }

            DeleteScript(SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Delete_Web_Script.ps1");
            DeleteAll deleteAll = new DeleteAll(SCIASettings.FilePrefixAppString + txtSiteName.Text + "_Delete_Web_Script.ps1");
            deleteAll.ShowDialog();
        }

        private void DeleteAllLauncher_Load(object sender, EventArgs e)
        {

        }
    }
}
