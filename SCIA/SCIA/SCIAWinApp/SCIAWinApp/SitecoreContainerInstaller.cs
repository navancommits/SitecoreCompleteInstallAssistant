using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Win32;

namespace SCIA
{
    public partial class SitecoreContainerInstaller : Form
    {
        const int const_DBConn_Tab = 0;
        const int const_SiteInfo_Tab = 1;
        const int const_Sitecore_Tab = 2;
        string xp0Path = "\\compose\\ltsc2019\\xp0\\";
        string siteNamePrefixString = "sc";//sc
        string identityServerNameString = "identityserver";//identityserver
        public SitecoreContainerInstaller()
        {
            InitializeComponent();
            this.Text = this.Text + " for Sitecore v" + Version.SitecoreVersion;
            tabDetails.Region = new Region(tabDetails.DisplayRectangle);
            ToggleEnableControls(false);
            AssignStepStatus(const_DBConn_Tab);
            txtSqlDbServer.Text = DBDetails.DbServer;
            txtSqlPass.Text = DBDetails.SqlPass;
            txtSqlUser.Text = DBDetails.SqlUser;
        }

        private void txtSqlPass_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlPass_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSqlUser_Leave(object sender, EventArgs e)
        {

        }

        private void txtSqlDbServer_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreDbServer_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNameSuffix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void txtSiteNameSuffix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteNamePrefix_TextChanged(object sender, EventArgs e)
        {
            SetFieldValues();
        }

        private void txtSiteNamePrefix_Leave(object sender, EventArgs e)
        {

        }

        private void txtSiteName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUserPassword_Leave(object sender, EventArgs e)
        {

        }

        private void txtSitecoreUsername_Leave(object sender, EventArgs e)
        {

        }

        void WriteAutoFillFile(string path)
        {
            using var file = new StreamWriter(path);
            file.WriteLine("param(");
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$LicenseXmlPath = (Join-Path $PSScriptRoot \".\"),");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SitecoreAdminPassword = \"Password12345\",");
            file.WriteLine();
            file.WriteLine("\t[Parameter(Mandatory = $true)]");
            file.WriteLine("\t[ValidateNotNullOrEmpty()]");
            file.WriteLine("\t[string]$SqlSaPassword = \"Password12345\",");
            file.WriteLine();
            file.WriteLine(")");
            file.WriteLine();
            file.WriteLine("$ErrorActionPreference = \"Stop\";");
            file.WriteLine();
            file.WriteLine("if (-not (Test-Path $LicenseXmlPath)) {");
            file.WriteLine("\tthrow \"Did not find $LicenseXmlPath\"");
            file.WriteLine("}");
            file.WriteLine("if (-not (Test-Path $LicenseXmlPath -PathType Leaf)) {");
            file.WriteLine("\tthrow \"$LicenseXmlPath is not a file\"");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# Check for Sitecore Gallery");
            file.WriteLine("Import-Module PowerShellGet");
            file.WriteLine("$SitecoreGallery = Get-PSRepository | Where-Object { $_.SourceLocation -eq \"https://sitecore.myget.org/F/sc-powershell/api/v2\" }");
            file.WriteLine("if (-not $SitecoreGallery) {");
            file.WriteLine("\tWrite-Host \"Adding Sitecore PowerShell Gallery...\" -ForegroundColor Green ");
            file.WriteLine("\tRegister-PSRepository -Name SitecoreGallery -SourceLocation https://sitecore.myget.org/F/sc-powershell/api/v2 -InstallationPolicy Trusted");
            file.WriteLine("\t$SitecoreGallery = Get-PSRepository -Name SitecoreGallery");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("# Install and Import SitecoreDockerTools ");
            file.WriteLine("$dockerToolsVersion = \"10.0.5\"");
            file.WriteLine("Remove-Module SitecoreDockerTools -ErrorAction SilentlyContinue");
            file.WriteLine("if (-not (Get-InstalledModule -Name SitecoreDockerTools -RequiredVersion $dockerToolsVersion -AllowPrerelease -ErrorAction SilentlyContinue)) {");
            file.WriteLine("\tWrite-Host \"Installing SitecoreDockerTools...\" -ForegroundColor Green");
            file.WriteLine("\tInstall-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion -AllowPrerelease -Scope CurrentUser -Repository $SitecoreGallery.Name");
            file.WriteLine("}");
            file.WriteLine("Write-Host \"Importing SitecoreDockerTools...\" -ForegroundColor Green");
            file.WriteLine("Import-Module SitecoreDockerTools -RequiredVersion $dockerToolsVersion");
            file.WriteLine();
            file.WriteLine("###############################");
            file.WriteLine("# Populate the environment file");
            file.WriteLine("###############################");
            file.WriteLine();
            file.WriteLine("Write-Host \"Populating required.env file variables...\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("# SITECORE_ADMIN_PASSWORD");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ADMIN_PASSWORD\" -Value $SitecoreAdminPassword");
            file.WriteLine();
            file.WriteLine("# SQL_SA_PASSWORD");
            file.WriteLine();
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SQL_SA_PASSWORD\" -Value $SqlSaPassword");
            file.WriteLine();
            file.WriteLine("# TELERIK_ENCRYPTION_KEY = random 64-128 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"TELERIK_ENCRYPTION_KEY\" -Value (Get-SitecoreRandomString 128)");
            file.WriteLine();
            file.WriteLine("# SITECORE_IDSECRET = random 64 chars");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_IDSECRET\" -Value (Get-SitecoreRandomString 64 -DisallowSpecial)");
            file.WriteLine();
            file.WriteLine("# SITECORE_ID_CERTIFICATE");
            file.WriteLine("$idCertPassword = Get-SitecoreRandomString 12 -DisallowSpecial");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ID_CERTIFICATE\" -Value (Get-SitecoreCertificateAsBase64String -DnsName \"" + txtSiteNameSuffix.Text + "\" -Password (ConvertTo-SecureString -String $idCertPassword -Force -AsPlainText))");
            file.WriteLine();
            file.WriteLine("# SITECORE_ID_CERTIFICATE_PASSWORD");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_ID_CERTIFICATE_PASSWORD\" -Value $idCertPassword");
            file.WriteLine();
            file.WriteLine("# SITECORE_LICENSE");
            file.WriteLine("Set-DockerComposeEnvFileVariable \"SITECORE_LICENSE\" -Value (ConvertTo-CompressedBase64String -Path $LicenseXmlPath)");
            file.WriteLine();
            file.WriteLine();
            file.WriteLine("##################################");
            file.WriteLine("# Configure TLS/HTTPS certificates");
            file.WriteLine("##################################");
            file.WriteLine();
            file.WriteLine("Push-Location traefik\\certs");
            file.WriteLine("try {");
            file.WriteLine("\t$mkcert = \".\\mkcert.exe\"");
            file.WriteLine("\tif ($null -ne (Get-Command mkcert.exe -ErrorAction SilentlyContinue)) {");
            file.WriteLine("\t\t# mkcert installed in PATH");
            file.WriteLine("\t\t$mkcert = \"mkcert\"");
            file.WriteLine("\t} elseif (-not (Test-Path $mkcert)) {");
            file.WriteLine("\t\tWrite-Host \"Downloading and installing mkcert certificate tool...\" -ForegroundColor Green ");
            file.WriteLine("\t\tInvoke-WebRequest \"https://github.com/FiloSottile/mkcert/releases/download/v1.4.1/mkcert-v1.4.1-windows-amd64.exe\" -UseBasicParsing -OutFile mkcert.exe ");
            file.WriteLine("\t\tif ((Get-FileHash mkcert.exe).Hash -ne \"1BE92F598145F61CA67DD9F5C687DFEC17953548D013715FF54067B34D7C3246\") {");
            file.WriteLine("\t\t\tRemove-Item mkcert.exe -Force");
            file.WriteLine("\t\t\tthrow \"Invalid mkcert.exe file\"");
            file.WriteLine("\t\t}");
            file.WriteLine("\t}");
            file.WriteLine("\tWrite-Host \"Generating Traefik TLS certificates...\" -ForegroundColor Green");
            file.WriteLine("\t& $mkcert -install");
            file.WriteLine("\t& $mkcert -cert-file " + txtSiteName.Text + ".crt -key-file " + txtSiteName.Text + ".key \"" + txtSiteName.Text + "\"");
            file.WriteLine("\t& $mkcert -cert-file " + txtIDServerSiteName.Text + ".crt -key-file " + txtIDServerSiteName.Text + ".key \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("}");
            file.WriteLine("catch {");
            file.WriteLine("\tWrite-Host \"An error occurred while attempting to generate TLS certificates: $_\" -ForegroundColor Red");
            file.WriteLine("}");
            file.WriteLine("finally {");
            file.WriteLine("\tPop-Location");
            file.WriteLine("}");
            file.WriteLine();
            file.WriteLine("################################");
            file.WriteLine("# Add Windows hosts file entries");
            file.WriteLine("################################");
            file.WriteLine();
            file.WriteLine("Write-Host \"Adding Windows hosts file entries...\" -ForegroundColor Green");
            file.WriteLine();
            file.WriteLine("Add-HostsEntry \"" + txtIDServerSiteName.Text + "\"");
            file.WriteLine("Add-HostsEntry \"" + txtSiteName.Text + "\"");
            file.WriteLine();
            file.WriteLine("Write-Host \"Done!\" -ForegroundColor Green");
            file.WriteLine();
            file.Dispose();
        }

        private void ToggleEnableControls(bool enabled)
        {
            ToggleButtonControls(enabled);
            MenubarControls(enabled);
        }

        private void MenubarControls(bool enabled)
        {
            btnLast.Enabled = enabled;
            btnAppSettings.Enabled = enabled;
            btnFirst.Enabled = enabled;
            btnPrevious.Enabled = enabled;
            chkStepsList.Enabled = enabled;
            btnNext.Enabled = enabled;
            btnValidateAll.Enabled = enabled;
        }

        private void ToggleButtonControls(bool enabled)
        {

            btnInstall.Enabled = enabled;
            btnInstall.Enabled = enabled;
            btnUninstall.Enabled = enabled;
            btnGenerate.Enabled = enabled;
        }

        private bool CheckPrerequisites()
        {
            if (!Directory.Exists(".\\" + ZipList.CommerceContainerZip)) { return false; }
            if (!File.Exists(".\\" + ZipList.CommerceContainerZip + "\\xc0\\license.xml")) { return false; }
            if (!WindowsVersionOk()) { return false; };
            if (!Directory.Exists("c:\\program files\\docker")) { return false; }

            return true;
        }

        private bool DbConnTabValidations()
        {
            if (!ValidateData(txtSqlDbServer, "Db Server", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlUser, "Sql User", const_DBConn_Tab)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", const_DBConn_Tab)) return false;

            return true;
        }

        private bool SiteInfoTabValidations()
        {
            if (!ValidateData(txtSiteNamePrefix, "Site Prefix", const_SiteInfo_Tab)) return false;
            if (!ValidateData(txtSiteNameSuffix, "Site Suffix", const_SiteInfo_Tab)) return false;
            return true;
        }

        private bool ValidateData(TextBox control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                lblStatus.Text = controlString + " needed... ";
                lblStatus.ForeColor = Color.Red;
                AssignStepStatus(tabIndex);
                Valid = false;
            }
            return Valid;
        }

        
        private void AssignStepStatus(int tabIndex)
        {
            TabIndexValue = tabIndex;
            chkStepsList.SelectedIndex = tabIndex;
            tabDetails.SelectedIndex = tabIndex;
            chkStepsList.SetItemChecked(tabIndex, true);
            int tabCount = tabDetails.TabCount;
            switch (tabIndex)
            {
                case const_DBConn_Tab:
                    lblStepInfo.Text = "Step 1 of " + tabCount + ": DB Connection";
                    break;
                case const_SiteInfo_Tab:
                    ToggleEnableControls(false);
                    btnAppSettings.Enabled = true;
                    btnNext.Enabled = true;
                    lblStepInfo.Text = "Step 2 of " + tabCount + ": Site Info";
                    break;
                case const_Sitecore_Tab:
                    ToggleEnableControls(true); 
                    lblStepInfo.Text = "Step 3 of " + tabCount + ": Sitecore Details";
                    break;
            }

        }

        public int TabIndexValue { get; set; }

        private bool WindowsVersionOk()
        {
            string version = (string)Registry.GetValue(@"HKEY_LOCAL_MACHINE\SOFTWARE\WOW6432Node\Microsoft\Windows NT\CurrentVersion", "ProductName", null);
            if (version == "Windows 10 Pro" || version == "Windows 10 Enterprise") { return true; }
            lblStatus.ForeColor = Color.Red;
            lblStatus.Text = "Windows Edition must be Pro or Enterprise Build for Docker Windows";
            return false;
        }
        private void SetStatusMessage(string statusmsg, Color color)
        {

            lblStatus.ForeColor = color;
            lblStatus.Text = statusmsg;
        }

        private bool ValidateAll(bool unInstall = false)
        {
            if (!DbConnTabValidations()) return false;
            if (!SiteInfoTabValidations()) return false;

            if (!ValidateData(txtIDServerSiteName, "ID Server Site Name", const_SiteInfo_Tab)) return false;
            if (!ValidateData(txtSitecoreUsername, "Sitecore Username", const_Sitecore_Tab)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", const_Sitecore_Tab)) return false;


            if (unInstall) return true;

            return true;
        }


        private bool CheckAllValidations(bool uninstall = false, bool generatescript = false)
        {
            ToggleButtonControls(false);

            if (!CheckPrerequisites())
            {
                SetStatusMessage("One or more pre-requisites missing.... Click Pre-requisites button to check...", Color.Red);
                return false;
            }

            if (!ValidateAll(uninstall)) return false;
            if (!SiteInfoTabValidations()) return false;


            ToggleEnableControls(true);
            //if (uninstall) { btnInstall.Enabled = false; } else { btnUninstall.Enabled = false; }

            return true;
        }

        private void btnGenerate_Click(object sender, EventArgs e)
        {
            string initPS1 = "SCIA-" + txtSiteName.Text + "-init.ps1";
            if (!CheckAllValidations()) return;
            WriteAutoFillFile(".\\" + ZipList.SitecoreContainerZip + xp0Path + initPS1);

            CommonFunctions.LaunchPSScript(".\\" + initPS1 + "  -SitecoreAdminPassword \"" + txtSitecoreUserPassword.Text + "\" -SqlSaPassword \"" + txtSqlPass.Text + "\" -LicenseXmlPath \"license.xml\"", ".\\" + ZipList.CommerceContainerZip + "\\compose\\ltsc2019\\xp0");

            lblStatus.Text = ".env file generated successfully....";
            lblStatus.ForeColor = Color.DarkGreen;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //var destFolder = ZipList.SitecoreContainerZip + xp0Path;
            //CommonFunctions.LaunchPSScript(@".\CleanContainerCache.ps1", destFolder);
        }

        private void btnInstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            CommonFunctions.LaunchCmdScript("docker-compose up -d", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker-Compose Up successfully launched....";
            ToggleEnableControls(false);
        }

        private void btnUninstall_Click(object sender, EventArgs e)
        {
            if (!CheckAllValidations()) return;

            CommonFunctions.LaunchCmdScript("docker-compose down", ".\\" + ZipList.SitecoreContainerZip + xp0Path);
            lblStatus.ForeColor = Color.DarkGreen;
            lblStatus.Text = "Docker-Compose Down successfully launched....";
            ToggleEnableControls(false);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            AssignStepStatus(tabDetails.TabCount-1);

        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            AssignStepStatus(0);

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabDetails.TabCount - 1) TabIndexValue -= 1;
            AssignStepStatus(TabIndexValue);
        }

        private void btnGenerate_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void btnInstall_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void btnUninstall_EnabledChanged(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            btn.ForeColor = btn.Enabled == false ? Color.DarkGray : Color.White;
            btn.BackColor = btn.Enabled == false ? Color.Gray : Color.Black; ;
        }

        private void chkStepsList_Click(object sender, EventArgs e)
        {
            TabIndexValue = chkStepsList.SelectedIndex;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < chkStepsList.Items.Count; ++ix)
                if (ix != e.Index) chkStepsList.SetItemChecked(ix, false);
        }


        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TabIndexValue == const_DBConn_Tab) if (!DbConnTabValidations() || settingsData == null) return;

            //Get data from SCIA table
            if (TabIndexValue == const_SiteInfo_Tab)
            {
                if (!SiteInfoTabValidations()) return;
                //PopulateSCIAData();                
            }
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 2) TabIndexValue += 1;
            if (TabIndexValue >= 0 && TabIndexValue <= tabDetails.TabCount - 1) AssignStepStatus(TabIndexValue);
        }
        private void DisplaySettingsDialog(bool settingsDataPresent)
        {
            DBServerDetails dBServerDetails = new DBServerDetails
            {
                Username = txtSqlUser.Text,
                Password = txtSqlPass.Text,
                Server = txtSqlDbServer.Text,
                IsSettingsPresent = settingsDataPresent
            };
            Settings settings = new Settings(dBServerDetails);
            settings.ShowDialog();
        }

        private bool LoadSettingsFormRoutine(bool settingsDataPresent)
        {
            SetStatusMessage("Successfully established DB Connection... Settings unavailable... Save settings and restart application...", Color.Red);
            ToggleEnableDbControls(false);
            ToggleEnableControls(false);
            DisplaySettingsDialog(settingsDataPresent);
            return true;
        }
        private void ToggleEnableDbControls(bool enabled)
        {
            txtSqlPass.Enabled = enabled;
            txtSqlUser.Enabled = enabled;
            txtSqlDbServer.Enabled = enabled;
            btnDbConn.Enabled = enabled;
        }

        private void btnPrerequisites_Click(object sender, EventArgs e)
        {
            SitecoreContainerPrerequisites prerequisites = new SitecoreContainerPrerequisites();
            prerequisites.ShowDialog();
        }
        private bool PopulateSettingsData()
        {
            SqlConnection connection;
            if (CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                using (connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text)))
                {
                    connection.Open();
                    if (!CommonFunctions.DbTableExists("Settings", connection)) { LoadSettingsFormRoutine(false); return false; }

                    settingsData = CommonFunctions.GetSettingsData(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "SCIA_DB", txtSqlUser.Text, txtSqlPass.Text));

                    if (settingsData == null) { LoadSettingsFormRoutine(false); return false; }

                    txtSiteNameSuffix.Text = settingsData.SiteNameSuffix.Trim();
                    siteNamePrefixString = settingsData.SitePrefixString.Trim();
                    identityServerNameString = settingsData.IdentityServerNameString.Trim();
                    txtSitecoreUsername.Text = settingsData.SitecoreUsername.Trim();
                    txtSiteNameSuffix.Text = "dev.local";

                    SetFieldValues();

                }
            }
            return true;
        }

        private void SetFieldValues()
        {
            txtSiteName.Text = txtSiteNamePrefix.Text + siteNamePrefixString + "." + txtSiteNameSuffix.Text;
            txtIDServerSiteName.Text = txtSiteNamePrefix.Text + identityServerNameString + "." + txtSiteNameSuffix.Text;
        }
        private SettingsData settingsData { get; set; }


        private void btnDbConn_Click(object sender, EventArgs e)
        {
            if (!CommonFunctions.IsServerConnected(CommonFunctions.BuildConnectionString(txtSqlDbServer.Text, "master", txtSqlUser.Text, txtSqlPass.Text)))
            {
                SetStatusMessage("Check Connection Details, Unable to establish DB Connection", Color.Red);
                TabIndexValue = const_DBConn_Tab;
                AssignStepStatus(TabIndexValue);
                ToggleEnableControls(false);
                return;
            }
            if (!CommonFunctions.CheckDatabaseExists("SCIA_DB", txtSqlDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                LoadSettingsFormRoutine(false);
                return;
                /* if no records in settings table, just enable settings button
                * else load next tab
                * */

            }

            if (!PopulateSettingsData()) return;
            SetStatusMessage("Successfully established DB Connection", Color.DarkGreen);

            ToggleEnableDbControls(false);
            btnDbConn.Enabled = false;
            AssignStepStatus(const_SiteInfo_Tab);

        }

        private void btnValidateAll_Click(object sender, EventArgs e)
        {
            if (CheckAllValidations()) SetStatusMessage("Congrats! Passed all Validations!", Color.DarkGreen);
        }
    }
}
