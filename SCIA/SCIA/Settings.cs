using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using Microsoft.PowerShell.Commands;

namespace SCIA
{
    public partial class Settings : Form
    {
        string DefaultStatusMessage = "Happy Sitecoring!";
        const int const_Port_Tab = 7;
        const int const_Redis_Tab = 6;
        public Settings()
        {
            InitializeComponent();
            tabSiteDetails.Region = new Region(tabSiteDetails.DisplayRectangle);
            tabSiteDetails.SelectedIndex = TabIndexValue;
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SqlConnection connection = null;

            if (!CommonFunctions.CheckDatabaseExists("SettingsDB", txtDbServer.Text, txtSqlUser.Text, txtSqlPass.Text))
            {
                connection = CreateDatabase("SettingsDB");
                if (connection==null)
                {
                    SetStatusMessage("Database Connectivity issues....", Color.Red);
                    return;
                }
            }

            connection = new SqlConnection(CommonFunctions.BuildConnectionString(txtDbServer.Text, "SettingsDB", txtSqlUser.Text, txtSqlPass.Text));
            if (!CommonFunctions.DbTableExists("Settings", connection))
            {
                CommonFunctions.CreateSettingsTable(connection, CommonFunctions.BuildConnectionString(txtDbServer.Text, "SettingsDB", txtSqlUser.Text, txtSqlPass.Text));
            }

            SaveSettingsDatatoDBSuccess(connection);
        }

        public bool SaveSettingsDatatoDBSuccess(SqlConnection sqlConn)
        {
            try
            {
                sqlConn.Open();

                var query = " INSERT INTO Settings([SiteSuffix], [SitePrefixAdditional]) VALUES (@siteSuffix, @siteNamePrefixAdditional)";

                SqlCommand sqlcommand = new SqlCommand(query, sqlConn);
                sqlcommand.Parameters.AddWithValue("@siteSuffix", txtSiteNameSuffix.Text);
                sqlcommand.Parameters.AddWithValue("@siteNamePrefixAdditional", txtSitePrefixAdditional.Text);

                int numberOfInsertedRows = sqlcommand.ExecuteNonQuery();
            }
            catch
            {
                CommonFunctions.WritetoEventLog("SCIA - Error saving settings data to Settings DB", EventLogEntryType.Error);
                return false;
            }
            finally { sqlConn.Close(); }
            
            return true;
        }

        private void SetStatusMessage(string statusmsg, Color color)
        {
            lblStepStatus.ForeColor = color;
            lblStepStatus.Text = statusmsg;
        }

        private void txtCommerceMinionsSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceAuthSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceShopsServicesPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceOpsSvcPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBizFxPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }


        private void txtIDServerSiteName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreIdentityServerUrl_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreIdentityServerUrl_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceEngineConnectClientSecret_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteHostHeaderName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSXAInstallDir_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtxConnectInstallDir_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceInstallRoot_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlDbPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreDbServer_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreCoreDbName_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtSitecoreCoreDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlUser_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSqlPass_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreDomain_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreUsername_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSitecoreUserPassword_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSearchIndexPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrUrl_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrRoot_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSolrService_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtStorefrontIndexPrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtRedisHost_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtRedisPort_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceServicesDBServer_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceGlobalDbName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceSvcPostFix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtCommerceServicesHostPostFix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBizFxName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserDomain_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtUserPassword_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txttxtBraintreeMerchantId_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreePublicKey_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreePrivateKey_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtBraintreeEnvironment_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteName_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteNameSuffix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void txtSiteNamePrefix_Leave(object sender, EventArgs e)
        {
            SetStatusMessage(DefaultStatusMessage, Color.DarkGreen);
        }

        private void AssignStepStatus(int tabIndex)
        {
            chkStepList.SelectedIndex = tabIndex;
            chkStepList.SetItemChecked(tabIndex, true);
            switch (tabIndex)
            {
                case 0:
                    lblStepStatus.Text = "Step 1 of 11: DB Connection";
                    break;
                case 1:
                    lblStepStatus.Text = "Step 2 of 11: Site Info";
                    break;
                case 2:
                    lblStepStatus.Text = "Step 3 of 11: General Info";
                    break;
                case 3:
                    lblStepStatus.Text = "Step 4 of 11: Install Details";
                    break;
                case 4:
                    lblStepStatus.Text = "Step 5 of 11 Sitecore Details";
                    break;
                case 5:
                    lblStepStatus.Text = "Step 6 of 11: Solr Details";
                    break;
                case 6:
                    lblStepStatus.Text = "Step 7 of 11: Redis Details";
                    break;                
                case 7:
                    lblStepStatus.Text = "Step 8 of 11: Port Details";
                    break;
                case 8:
                    lblStepStatus.Text = "Step 9 of 11: Environment Details";
                    break;
                case 9:
                    lblStepStatus.Text = "Step 10 of 11: Win User Details";
                    break;
                case 10:
                    lblStepStatus.Text = "Step 11 of 11: Braintree Details";
                    break;
            }
        }


        public int TabIndexValue { get; set; }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 0 && TabIndexValue <= tabSiteDetails.TabCount - 2) TabIndexValue += 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            TabIndexValue = tabSiteDetails.TabCount - 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private bool ValidateData(TextBox control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (string.IsNullOrWhiteSpace(control.Text))
            {
                lblStatusInfo.Text = controlString + " needed... ";
                lblStatusInfo.ForeColor = Color.Red;
                tabSiteDetails.SelectedIndex = tabIndex;
                Valid = false;
            }
            return Valid;
        }

        private bool ValidatePortNumber(NumericUpDown control, string controlString, int tabIndex)
        {
            bool Valid = true;
            if (control.Value < 1024)
            {
                lblStatusInfo.Text = controlString + " must be between 1024 to 49151... ";
                lblStatusInfo.ForeColor = Color.Red;
                control.Focus();
                tabSiteDetails.SelectedIndex = tabIndex;
                AssignStepStatus(tabIndex);
                Valid = false;
            }
            return Valid;
        }

        private bool IsPortNotinUse(NumericUpDown control, int tabIndex)
        {
            if (PortInUse(Convert.ToInt32(control.Value)))
            {
                lblStatusInfo.Text = control.Value + " port in use... provide a different number...";
                lblStatusInfo.ForeColor = Color.Red;
                tabSiteDetails.SelectedIndex = tabIndex;
                AssignStepStatus(tabIndex);
                return false;
            }
            return true;
        }

        private bool PerformPortValidations()
        {
            string portString = string.Empty;

            if (!ValidatePortNumber(txtRedisPort, "Redis Port", const_Redis_Tab)) return false;

            if (!ValidatePortNumber(txtCommerceOpsSvcPort, "Commerce Ops Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceShopsServicesPort, "Commerce Shops Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceAuthSvcPort, "Commerce Auth Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtCommerceMinionsSvcPort, "Commerce Minions Svc Port", const_Port_Tab)) return false;
            if (!ValidatePortNumber(txtBizFxPort, "BizFx Port Number", const_Port_Tab)) return false;
            if (!IsPortNotinUse(txtCommerceOpsSvcPort, const_Port_Tab)) return false;
            if (!IsPortNotinUse(txtCommerceShopsServicesPort, const_Port_Tab)) return false;
            if (!IsPortNotinUse(txtCommerceAuthSvcPort, const_Port_Tab)) return false;
            if (!IsPortNotinUse(txtCommerceMinionsSvcPort, const_Port_Tab)) return false;
            if (!IsPortNotinUse(txtBizFxPort, const_Port_Tab)) return false;

            if (IsPortDuplicated(AddPortstoArray())) { lblStatusInfo.Text = "Duplicate port numbers detected! provide unique port numbers...."; return false; }

            portString = StatusMessageBuilder(portString);
            if (!string.IsNullOrWhiteSpace(portString))
            { lblStatusInfo.Text = "Port(s) in use... provide different numbers for - " + portString; lblStatusInfo.ForeColor = Color.Red; }

            return true;
        }


        private bool ValidateAll()
        {
            if (!ValidateData(txtCommerceEngineConnectClientId, "Sitecore Commerce Connect Client Id", 0)) return false;
            if (!ValidateData(txtCommerceEngineConnectClientSecret, "Sitecore Commerce Connect Client Secret", 0)) return false;

            if (!ValidateData(txtSiteRootDir, "Sitecore SXA Install Directory", 1)) return false;

            if (!ValidateData(txtDbServer, "Db Server", 2)) return false;
            if (!ValidateData(txtSqlUser, "Sql User", 2)) return false;
            if (!ValidateData(txtSqlPass, "Sql Password", 2)) return false;

            if (!ValidateData(txtSitecoreDomain, "Sitecore Domain", 3)) return false;
            if (!ValidateData(txtSitecoreUserName, "Sitecore Username", 3)) return false;
            if (!ValidateData(txtSitecoreUserPassword, "Sitecore User Password", 3)) return false;

            if (!ValidateData(txtSearchIndexPrefix, "Search Index Prefix", 4)) return false;
            if (!ValidateData(txtSolrUrl, "Solr Url", 4)) return false;
            if (!ValidateData(txtSolrRoot, "Solr Root Path", 4)) return false;
            if (!ValidateData(txtSolrService, "Solr Service Name", 4)) return false;

            if (!ValidateData(txtRedisHost, "Redis Host", 5)) return false;

            if (!PerformPortValidations()) return false;
            if (!ValidateData(txtBizFxSitePrefix, "BizFx Prefix", 7)) return false;

            if (!ValidateData(txtUserDomain, "Win User Domain", 9)) return false;

            if (!ValidateData(txtBraintreeMerchantId, "Braintree Merchant Id", 10)) return false;
            if (!ValidateData(txtBraintreePublicKey, "Braintree Public Key", 10)) return false;
            if (!ValidateData(txtBraintreePrivateKey, "Braintree Private Key", 10)) return false;
            if (!ValidateData(txtBraintreeEnvironment, "Braintree Environment", 10)) return false;

            return true;
        }

        private bool IsPortDuplicated(List<int> ports)
        {
            bool portDuplicated = false;
            for (int index = 0; index < ports.Count; index++)
            {
                for (int loopIndex = 0; loopIndex < ports.Count; loopIndex++)
                {
                    if (loopIndex != index)
                    {
                        if (ports[loopIndex] == ports[index])
                        {
                            portDuplicated = true;
                            break;
                        }
                    }

                }
            }
            return portDuplicated;
        }

        public SqlConnection CreateDatabase(string database)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            CommonFunctions.GrantAccess(appPath); //Need to assign the permission for current application to allow create database on server (if you are in domain).
            
            using SqlConnection sqlConnection = new SqlConnection(CommonFunctions.BuildConnectionString(txtDbServer.Text, "SettingsDB", txtSqlUser.Text, txtSqlPass.Text,true));
            {
                sqlConnection.Open();
                string createDBString = "CREATE DATABASE " + database + "; ";
                SqlCommand command = new SqlCommand(createDBString, sqlConnection);
                try
                {
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    CommonFunctions.WritetoEventLog("Unable to create Settings DB" + ex.Message, EventLogEntryType.Error);
                    return null;

                }

                return sqlConnection;
            }
            
        }

        private List<int> AddPortstoArray()
        {
            List<int> ports = new List<int>
            {
                Convert.ToInt32(txtBizFxPort.Value),
                Convert.ToInt32(txtCommerceAuthSvcPort.Value),
                Convert.ToInt32(txtCommerceMinionsSvcPort.Value),
                Convert.ToInt32(txtCommerceOpsSvcPort.Value),
                Convert.ToInt32(txtCommerceShopsServicesPort.Value),
                Convert.ToInt32(txtRedisPort.Value)
            };
            return ports;
        }

        public static bool PortInUse(int port)
        {
            bool inUse = false;

            IPGlobalProperties ipProperties = IPGlobalProperties.GetIPGlobalProperties();
            IPEndPoint[] ipEndPoints = ipProperties.GetActiveTcpListeners();


            foreach (IPEndPoint endPoint in ipEndPoints)
            {
                if (endPoint.Port == port)
                {
                    inUse = true;
                    break;
                }
            }

            return inUse;
        }

        private string BuildPortString(string port, string buildMsgString)
        {
            string portMessageString = string.Empty;
            if (string.IsNullOrWhiteSpace(buildMsgString))
            {
                portMessageString = port;
            }
            else
            {
                portMessageString = buildMsgString + ", " + port;
            }

            return portMessageString;
        }

        private string StatusMessageBuilder(string msg)
        {
            string portString = string.Empty;
            if (PortInUse(Convert.ToInt32(txtCommerceShopsServicesPort.Value))) { portString = BuildPortString(txtCommerceShopsServicesPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceOpsSvcPort.Value))) { portString = BuildPortString(txtCommerceOpsSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtBizFxPort.Value))) { portString = BuildPortString(txtBizFxPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceAuthSvcPort.Value))) { portString = BuildPortString(txtCommerceAuthSvcPort.Value.ToString(), portString); }
            if (PortInUse(Convert.ToInt32(txtCommerceMinionsSvcPort.Value))) { portString = BuildPortString(txtCommerceMinionsSvcPort.Value.ToString(), portString); }
            return portString;
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            TabIndexValue = 0;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            if (TabIndexValue >= 1 && TabIndexValue <= tabSiteDetails.TabCount - 1) TabIndexValue -= 1;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_Click(object sender, EventArgs e)
        {
            TabIndexValue = chkStepList.SelectedIndex;
            tabSiteDetails.SelectedIndex = TabIndexValue;
            AssignStepStatus(TabIndexValue);
        }

        private void chkStepsList_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            for (int ix = 0; ix < chkStepList.Items.Count; ++ix)
                if (ix != e.Index) chkStepList.SetItemChecked(ix, false);
        }

        private void chkStepList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
