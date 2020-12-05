using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.Serialization.Json;
using System.Data.Linq;
using System.Linq;
using System.Net.Http;
using System.Drawing.Imaging;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Transactions;
using System.Net.NetworkInformation;

namespace SCIA
{
    public static class CommonFunctions
    {
        private static readonly HttpClient client = new HttpClient();
        /// <summary>
        /// Test that the server is connected
        /// </summary>
        /// <param name="connectionString">The connection string</param>
        /// <returns>true if the connection is opened</returns>
        public static bool IsServerConnected(string connectionString)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return true;
            }
            catch (SqlException ex)
            {
                WritetoEventLog("SCIA - Error while checking if Server is Connected - IsServerConnected - " + ex.Message, EventLogEntryType.Error);
                return false;
            }

        }

        public static void InvokeWebRequest()
        {
            var values = new Dictionary<string, string>
            {
                { "username", Login.username },
                { "password", Login.password },
                { "rememberMe", Login.rememberMe.ToString() }
            };

            var content = new FormUrlEncodedContent(values);

            var loginResponse = client.PostAsync(Login.requestUrl, content);

            if(loginResponse ==null || loginResponse.Result.StatusCode.ToString()!="OK")
			{
                WritetoEventLog("Unable to login to " + Login.requestUrl + "  with the supplied credentials.",  EventLogEntryType.Error);
                Login.Success=false;
                return;
			}

            Login.Success = true;
        }

        public static SqlConnection GetConnection(string connectionString)
        {
            try
            {
                using SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();
                return connection;
            }
            catch (SqlException ex)
            {
                WritetoEventLog("SCIA - Error while checking if Server is Connected - IsServerConnected - " + ex.Message, EventLogEntryType.Error);
                return null;
            }

        }

        public static void ResetVersionRelatedInfo()
        {
            ZipList.CommerceZip = CommonFunctions.GetZipNamefromWdpVersion("commerce", Version.SitecoreVersion);
            ZipList.CommerceContainerZip = CommonFunctions.GetZipNamefromWdpVersion("commercecon", Version.SitecoreVersion);
            ZipList.SitecoreContainerZip = CommonFunctions.GetZipNamefromWdpVersion("sitecorecon", Version.SitecoreVersion);
            ZipList.SitecoreDevSetupZip = CommonFunctions.GetZipNamefromWdpVersion("sitecoredevsetup", Version.SitecoreVersion);

        }

        public static bool SqlInjectionCheck(string inputString)
        {
            bool isSQLInjection = false;

            string[] sqlCheckList = { "--",
                                       ";--",
                                       ";",
                                       "/*",
                                       "*/",
                                        "@@",
                                        "@",
                                        "char",
                                       "nchar",
                                       "varchar",
                                       "nvarchar",
                                       "alter",
                                       "begin",
                                       "cast",
                                       "create",
                                       "cursor",
                                       "declare",
                                       "delete",
                                       "drop",
                                       "end",
                                       "exec",
                                       "execute",
                                       "fetch",
                                            "insert",
                                          "kill",
                                             "select",
                                           "sys",
                                            "sysobjects",
                                            "syscolumns",
                                           "table",
                                           "update"

                                       };

            string CheckString = inputString.Replace("'", "''");

            for (int i = 0; i <= sqlCheckList.Length - 1; i++)

            {

                if ((CheckString.IndexOf(sqlCheckList[i], StringComparison.OrdinalIgnoreCase) >= 0))
                {
                    isSQLInjection = true;
                    break;
                }
            }

            return isSQLInjection;
        }


        public static bool GrantAccess(string fullPath)
        {
            DirectoryInfo info = new DirectoryInfo(fullPath);
            WindowsIdentity self = WindowsIdentity.GetCurrent();
            DirectorySecurity ds = info.GetAccessControl();
            ds.AddAccessRule(new FileSystemAccessRule(self.Name,
            FileSystemRights.FullControl,
            InheritanceFlags.ObjectInherit |
            InheritanceFlags.ContainerInherit,
            PropagationFlags.None,
            AccessControlType.Allow));
            info.SetAccessControl(ds);
            return true;
        }

        public static bool CheckandSetupZipVersionsTable()
        {
            if (string.IsNullOrWhiteSpace(CommonFunctions.ConnectionString)) return false;
            using (var connection = new SqlConnection(CommonFunctions.ConnectionString))
            {
                connection.Open();
                if (CommonFunctions.DbTableExists("ZipVersions", connection)) { return true; }
                using TransactionScope scope = new TransactionScope();
                if (CommonFunctions.CreateZipVersionsTable(connection) == 0) { return false; };

                if (CommonFunctions.InsertDataintoZipVersionsTable(connection) == 0) { return false; };
                scope.Complete();
            }

            return true;
        }

        public static bool CheckDatabaseExists(string databaseName, string dbServer, string sqlUser, string sqlPass)
        {
            string sqlCreateDBQuery;
            bool result = false;
            try
            {
                using SqlConnection sqlConn = new SqlConnection(CommonFunctions.BuildConnectionString(dbServer, databaseName, sqlUser, sqlPass, true));
                {
                    sqlConn.Open();
                    sqlCreateDBQuery = string.Format("SELECT database_id FROM sys.databases WHERE Name = '{0}'", databaseName);

                    using (SqlCommand sqlCmd = new SqlCommand(sqlCreateDBQuery, sqlConn))
                    {
                        object resultObj = sqlCmd.ExecuteScalar();
                        int databaseID = 0;
                        if (resultObj != null)
                        {
                            int.TryParse(resultObj.ToString(), out databaseID);
                        }
                        result = (databaseID > 0);
                    }
                }
            }
            catch (Exception ex)
            {
                result = false;
                WritetoEventLog("SCIA - Error while checking if SCIA_DB Exists" + ex.Message, EventLogEntryType.Error);
            }

            return result;
        }

        public static int CreateSettingsTable(SqlConnection conn)
        {
            if (DbTableExists("dbo.Settings", conn)) return 1;

            var sql = "CREATE TABLE Settings" +
            "(" +
            "SiteNameSuffix VARCHAR(50), SitePrefixString VARCHAR(50), IdentityServerNameString VARCHAR(50), xConnectServerNameString VARCHAR(50), CommerceEngineConnectClientId VARCHAR(50), CommerceEngineConnectClientSecret VARCHAR(50), SiteRootDir VARCHAR(100), SitecoreDomain VARCHAR(50), SitecoreUsername VARCHAR(50),SearchIndexPrefix  VARCHAR(50),RedisHost  VARCHAR(50),RedisPort  VARCHAR(10),BizFxSitenamePrefix  VARCHAR(50),EnvironmentsPrefix  VARCHAR(200),CommerceDbNameString  VARCHAR(50),UserDomain VARCHAR(50), BraintreeMerchantId VARCHAR(100),BraintreePublicKey VARCHAR(100),BraintreePrivateKey VARCHAR(100),BraintreeEnvironment VARCHAR(100),HttpsString varchar(20),CoreDbSuffix varchar(15),CommerceGlobalDbSuffix  varchar(20),CommSharedDbSuffix  varchar(20) ,UserSuffix  varchar(50),StorefrontHostSuffix varchar(50), HostSuffix  varchar(10),UserPassword  varchar(50) ,created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to create Settings Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static int CreateZipVersionsTable(SqlConnection conn)
        {
            var sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[ZipVersions]') AND type in (N'U')) DROP TABLE [dbo].[ZipVersions];CREATE TABLE ZipVersions" +
            "(" +
            "Version VARCHAR(20), ZipName  VARCHAR(200), ZipType VARCHAR(20),Url varchar(max), [IdColumn] [smallint] IDENTITY(1,1) NOT NULL,created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to create ZipVersions Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static int InsertDataintoZipVersionsTable(SqlConnection conn)
        {
            var sql = "INSERT [dbo].[ZipVersions] ([Version], [ZipName], [ZipType], [url]) VALUES (N'10.0', N'SitecoreContainerDeployment.10.0.0.004346.150', N'sitecorecon',N'https://dev.sitecore.net/~/media/C9EA651A4B204A4ABD588CD1BD0A67D4.ashx'); INSERT [dbo].[ZipVersions] ([Version], [ZipName], [ZipType], [url]) VALUES (N'10.0', N'Sitecore.Commerce.WDP.2020.08-6.0.238', N'commerce',N'https://dev.sitecore.net/~/media/7ED76B7A45D04746A3862726ADB59583.ashx'); INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'10.0', N'Sitecore.Commerce.Container.SDK.1.0.214', N'commercecon',N'https://dev.sitecore.net/~/media/FB50C51D304C47E89EB1C21C087B9B73.ashx'); INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'10.0', N'Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)', N'sitecoredevsetup',N'https://dev.sitecore.net/~/media/A74E47524738460B83332BAE82F123D1.ashx'); INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.3', N'Sitecore.Commerce.WDP.2020.01-5.0.145', N'commerce',N'https://dev.sitecore.net/~/media/B915EEE9B5C0429AB557C357E2B23EEA.ashx'); INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.2', N'Sitecore.Commerce.WDP.2019.07-4.0.165', N'commerce',N'https://dev.sitecore.net/~/media/07F9ABE455944146B37E9D71CA781A27.ashx');             INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.3', N'Sitecore 9.3.0 rev. 003498 (Setup XP0 Developer Workstation rev. 1.1.1-r4)', N'sitecoredevsetup','https://dev.sitecore.net/~/media/A1BC9FD8B20841959EF5275A3C97A8F9.ashx'); INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.1.1', N'Sitecore 9.1.1 rev. 002459 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/B262F95B2AFA4C39BDE821590CE6F1A6.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'10.0', N'Sitecore 10.0.0 rev. 004346 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/1461853C8977488D9AABA716242137FC.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.3', N'Sitecore 9.3.0 rev. 003498 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/88666D3532F24973939C1CC140E12A27.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.1', N'Sitecore 9.1.0 rev. 001564 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/442BA3B6E6334CEA9546C647D434BC13.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.0', N'Sitecore 9.0.0 rev. 171002 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/CB45E46E57C34573B07F106D6991720A.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.0.1', N'Sitecore 9.0.1 rev. 171219 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/8551EF0996794A7FA9FF64943B391855.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.0.2', N'Sitecore 9.0.2 rev. 180604 (WDP XP0 packages)', N'sitecoresif','https://dev.sitecore.net/~/media/F53E9734518E47EF892AD40A333B9426.ashx');INSERT[dbo].[ZipVersions]([Version], [ZipName], [ZipType], [url]) VALUES(N'9.2', N'Sitecore 9.2.0 rev. 002893 (Setup XP0 Developer Workstation rev. r150)', N'sitecoredevsetup','https://dev.sitecore.net/~/media/1C1D7C4CBC934A6AA36825974A18A72E.ashx');";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to insert data into ZipVersions Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }


        public static int CreateSCIATable(SqlConnection conn)
        {
            if (DbTableExists("dbo.SCIA", conn)) return 1;
            // Open the connection [SiteNameSuffix], [SitePrefixString],[IdentityServerNameAdditional] 
            var sql = "CREATE TABLE SCIA" +
            "(" +
            "SiteNameSuffix VARCHAR(50), SiteNamePrefix  VARCHAR(50), SiteName VARCHAR(100), IDServerSiteName  VARCHAR(100), SitecoreIdentityServerUrl  VARCHAR(200), SXAInstallDir  VARCHAR(200), xConnectInstallDir  VARCHAR(200),CommerceInstallRoot   VARCHAR(200), CommerceEngineConnectClientId VARCHAR(50), CommerceEngineConnectClientSecret VARCHAR(100), SiteHostHeaderName VARCHAR(100), SitecoreDomain VARCHAR(50), SitecoreUsername VARCHAR(50),SitecoreUserPassword VARCHAR(20),SearchIndexPrefix  VARCHAR(50),SolrUrl Varchar(200), SolrRoot VARCHAR(200), SolrService Varchar(50),StorefrontIndexPrefix VARCHAR(100),RedisHost  VARCHAR(50),RedisPort  smallint,SqlDbPrefix  VARCHAR(50),SitecoreDbServer Varchar(100),SitecoreCoreDbName  Varchar(50),SqlUser Varchar(50),SqlPass Varchar(50),CommerceServicesDBServer  VARCHAR(100), CommerceDbName Varchar(200),CommerceGlobalDbName Varchar(200),CommerceSvcPostFix  Varchar(50),CommerceServicesHostPostFix  Varchar(100),CommerceOpsSvcPort smallint, CommerceShopsServicesPort  smallint,CommerceAuthSvcPort smallint, CommerceMinionsSvcPort Smallint,BizFxPort SmallInt, BizFxName  Varchar(100),EnvironmentsPrefix  VARCHAR(200),DeploySampleData varchar(1),UserDomain VARCHAR(50),UserName VARCHAR(50), UserPassword varchar(20), BraintreeMerchantId VARCHAR(100),BraintreePublicKey VARCHAR(100),BraintreePrivateKey VARCHAR(100),BraintreeEnvironment VARCHAR(100),created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to create SCIA Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static int CreateSIFTable(SqlConnection conn)
        {
            if (DbTableExists("dbo.SIF", conn)) return 1;
            var sql = "CREATE TABLE SIF" +
            "(" +
            "SiteNameSuffix VARCHAR(50), SiteNamePrefix  VARCHAR(50), SiteName VARCHAR(100), IDServerSiteName  VARCHAR(100), SitecoreIdentityServerUrl  VARCHAR(200), SXAInstallDir  VARCHAR(200), xConnectInstallDir  VARCHAR(200),PasswordRecoveryUrl   VARCHAR(1000), ClientSecret VARCHAR(100), xConnectCollectionSvc  VARCHAR(1000), SitecoreIdentityAuthority  VARCHAR(1000), SitecoreDomain VARCHAR(50), SitecoreUsername VARCHAR(50),SitecoreUserPassword VARCHAR(20),SearchIndexPrefix  VARCHAR(50),SolrUrl Varchar(200), SolrRoot VARCHAR(200), SolrService Varchar(50),SolrVersion VARCHAR(10),SqlDbPrefix  VARCHAR(50),SitecoreDbServer Varchar(100),SitecoreCoreDbName  Varchar(50),SqlUser Varchar(50),SqlPass Varchar(50),created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to create SIF Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static int InsertDataintoVersionPrerequisitesTable(SqlConnection conn)
        {
            var sql = "INSERT [dbo].[VersionPrerequisites] ([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES (N'10.0', N'commerce', N'sxa', N'Sitecore Experience Accelerator 10.0.0.3138.scwdp.zip',  N'https://dev.sitecore.net/~/media/42992D85CC134384A0660F6C41479C16.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'psextension', N'Sitecore.PowerShell.Extensions-6.1.1.scwdp.zip',  N'https://dev.sitecore.net/~/media/E820B0DA62464072891DA92470F93954.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'msbuild', N'msbuild.microsoft.visualstudio.web.targets.14.0.0.3', N'https://www.nuget.org/api/v2/package/MSBuild.Microsoft.VisualStudio.Web.targets/14.0.0.3'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'dotnethost', N'dotnet-hosting-3.1.8-win.exe',  N'https://download.visualstudio.microsoft.com/download/pr/854cbd11-4b96-4a44-9664-b95991c0c4f7/8ec4944a5bd770faba2f769e647b1e6e/dotnet-hosting-3.1.8-win.exe');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'commercezip', 'Sitecore.Commerce.WDP.2020.08-6.0.238',  N'https://dev.sitecore.net/~/media/7ED76B7A45D04746A3862726ADB59583.ashx'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'redis', N'Redis-x64-3.0.504.msi',  N'https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'powershell', N'Latest Powershell',  N'https://aka.ms/install-powershell.ps1'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'commerce', N'sif', N'SIF.Sitecore.Commerce.5.0.49',  NULL); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'sxa', N'Sitecore Experience Accelerator 9.3.0.2589.zip', N'https://dev.sitecore.net/~/media/6F15F11E72D942218F2820108533F691.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'psextension', N'Sitecore.PowerShell.Extensions-6.0.zip',  N'https://dev.sitecore.net/~/media/30973C391E9148F8B866FE4522E7F260.ashx'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'msbuild', N'msbuild.microsoft.visualstudio.web.targets.14.0.0.3',  N'https://www.nuget.org/api/v2/package/MSBuild.Microsoft.VisualStudio.Web.targets/14.0.0.3');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'dotnethost', N'dotnet-hosting-3.1.8-win.exe',  N'https://download.visualstudio.microsoft.com/download/pr/854cbd11-4b96-4a44-9664-b95991c0c4f7/8ec4944a5bd770faba2f769e647b1e6e/dotnet-hosting-3.1.8-win.exe');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'commercezip', 'Sitecore.Commerce.WDP.2020.01-5.0.145', N'https://dev.sitecore.net/~/media/B915EEE9B5C0429AB557C357E2B23EEA.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'redis', N'Redis-x64-3.0.504.msi', N'https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'powershell', N'Latest Powershell',  N'https://aka.ms/install-powershell.ps1');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'9.3', N'commerce', N'sif', N'SIF.Sitecore.Commerce.4.0.31',  NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'10.0', N'sitecorecon', N'sitecoreconzip', N'SitecoreContainerDeployment.10.0.0.004346.150',  N'https://dev.sitecore.net/~/media/C9EA651A4B204A4ABD588CD1BD0A67D4.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.3', N'sitecoresif', N'XP0', N'XP0 Configuration files 9.3.0 rev. 003498',  NULL);            INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'sitecoresif', N'XP0', N'XP0 Configuration files 10.0.0 rev. 004346', NULL); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'10.0', N'sitecoresif', N'XP0', N'XP0 Configuration files 10.0.0 rev. 004346', NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.1.1', N'sitecoresif', N'XP0', N'XP0 Configuration files 9.1.1 rev. 002459',  NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.1', N'sitecoresif', N'XP0', N'XP0 Configuration files 9.1.0 rev. 001564',  NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.0.2', N'sitecoresif', N'XP0', N'XP0 Configuration files 9.0.2 rev. 180604',  NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.0.1', N'sitecoresif', N'XP0', N'XP0 Configuration files 9.0.1 rev. 171219',  NULL);INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.0', N'sitecoresif', N'XP0', N'XP0 Configuration files rev.171002',  NULL);INSERT [dbo].[VersionPrerequisites] ([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES (N'9.2', N'commerce', N'sxa', N'Sitecore Experience Accelerator 1.9.0 rev. 190528 for 9.2.zip',  N'https://dev.sitecore.net/~/media/FB62D25D54C945999FE05C0E22B29622.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'psextension', N'Sitecore PowerShell Extensions-5.0 for 9.2.zip',  N'https://dev.sitecore.net/~/media/F02391CD82A144D18021665D3D3F4675.ashx');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'msbuild', N'msbuild.microsoft.visualstudio.web.targets.14.0.0.3', N'https://www.nuget.org/api/v2/package/MSBuild.Microsoft.VisualStudio.Web.targets/14.0.0.3'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'dotnethost', N'dotnet-hosting-3.1.8-win.exe',  N'https://download.visualstudio.microsoft.com/download/pr/854cbd11-4b96-4a44-9664-b95991c0c4f7/8ec4944a5bd770faba2f769e647b1e6e/dotnet-hosting-3.1.8-win.exe');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName], [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'commercezip', 'Sitecore.Commerce.WDP.2019.07-4.0.165',  N'https://dev.sitecore.net/~/media/07F9ABE455944146B37E9D71CA781A27.ashx'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'redis', N'Redis-x64-3.0.504.msi',  N'https://github.com/MicrosoftArchive/redis/releases/download/win-3.0.504/Redis-x64-3.0.504.msi');INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'powershell', N'Latest Powershell',  N'https://aka.ms/install-powershell.ps1'); INSERT[dbo].[VersionPrerequisites]([Version], [ZipType], [PrerequisiteKey], [PrerequisiteName],  [PrerequisiteUrl]) VALUES(N'9.2', N'commerce', N'sif', N'SIF.Sitecore.Commerce.3.0.28',  NULL); ";

            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to insert data into VersionPrerequisites Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static int CreateVersionPrerequisitesTable(SqlConnection conn)
        {
            var sql = "IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[VersionPrerequisites]') AND type in (N'U')) DROP TABLE [dbo].[VersionPrerequisites]; CREATE TABLE [dbo].[VersionPrerequisites]([Version] [varchar](20) NULL,[ZipType] [varchar](20) NULL,[PrerequisiteKey] [varchar](20) NULL,[PrerequisiteName] [varchar](500) NULL,[IdColumn] [bigint] IDENTITY(1, 1) NOT NULL,[PrerequisiteUrl] [varchar](max)NULL,created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
            SqlCommand cmd = new SqlCommand(sql, conn);
            int success;
            try
            {

                success = cmd.ExecuteNonQuery();

            }
            catch (SqlException ex)
            {
                success = 0;
                WritetoEventLog("SCIA - Unable to create VersionPrerequisites Table" + ex.Message, EventLogEntryType.Error);
            }

            return success;
        }

        public static SqlDataReader GetSettingsData(SqlConnection conn)
        {
            SqlDataReader reader = null;

            var sql = "select * from Settings";

            SqlCommand cmd = new SqlCommand(sql, conn);
            try
            {

                reader = cmd.ExecuteReader();

            }
            catch (SqlException ex)
            {
                WritetoEventLog("SCIA - Unable to create Settings Table" + ex.Message, EventLogEntryType.Error);
            }

            return reader;
        }

        

        public static SettingsData GetSettingsData(string connectionString)
        {
            try
            {
                using var dc = new DataContext(connectionString);
                var settings = dc.ExecuteQuery<SettingsData>(@"select top(1)* FROM [settings]");

                return settings.FirstOrDefault();
            }

            catch (SqlException ex)
            {
                CommonFunctions.WritetoEventLog("Get Settings Data - SQL Connectivity issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
                return null;
            }

        }

        public static SqlConnection CreateDatabase(string database,string dbServer,string sqlUser, string sqlPass)
        {
            string appPath = System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            CommonFunctions.GrantAccess(appPath); //Need to assign the permission for current application to allow create database on server (if you are in domain).

            using SqlConnection sqlConnection = new SqlConnection(CommonFunctions.BuildConnectionString(dbServer, database, sqlUser, sqlPass, true));
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
                    CommonFunctions.WritetoEventLog("Unable to create SCIA_DB" + ex.Message, EventLogEntryType.Error);
                    return null;

                }

                return sqlConnection;
            }

        }


        public static bool CheckPrerequisiteList(string destFolder,string sxaFileName,string pseFileName)
        {
                if (!CommonFunctions.FileSystemEntryExists(destFolder, null, "folder",true)) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "msbuild", "folder")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "SIF.Sitecore.Commerce.*", "folder",true)) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Adventure Works Images.OnPrem.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Connect Core OnPrem *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Engine Connect OnPrem *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Habitat Catalog *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Storefront *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Experience Accelerator Storefront Themes *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce ExperienceAnalytics Core OnPrem *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce ExperienceProfile Core OnPrem *.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Marketing Automation Core OnPrem *.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore Commerce Marketing Automation for AutomationEngine *.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, sxaFileName, "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.BizFx.OnPrem*", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.BizFX.SDK*.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.Commerce.Engine.OnPrem.Solr*.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, "Sitecore.Commerce.Habitat.Images.OnPrem.scwdp.zip", "file")) { return false; }
                if (!CommonFunctions.FileSystemEntryExists(destFolder, pseFileName, "file")) { return false; }
                if ((!Directory.Exists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.8")) && (!Directory.Exists("C:\\Program Files\\dotnet\\shared\\Microsoft.AspNetCore.App\\3.1.7"))) { return false; }
                if (!Directory.Exists("C:\\Program Files\\Redis")) { return false; }

            return true;
        }

        public static string ConnectionString { get; set; }

       public static ZipVersions GetZipVersionData(string version, string ziptype)
        {
            try
            {
                using var dc = new DataContext(ConnectionString);
                var zipversions = dc.ExecuteQuery<ZipVersions>(@"select * FROM [ZipVersions] where Version='" + version +"' and ZipType='" + ziptype + "'");

                List<ZipVersions> zipvers = zipversions.ToList();
                if (zipvers.Count == 0) return null;

                return zipvers.FirstOrDefault();
            }

            catch (SqlException ex)
            {
                WritetoEventLog("Get Zip Versions Data - SQL Connectivity issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
                return null;
            }

        }

        public static List<VersionPrerequisites> GetVersionPrerequisites(string version, string ziptype)
        {
            try
            {
                using var dc = new DataContext(ConnectionString);
                var zipversionprereqs = dc.ExecuteQuery<VersionPrerequisites>(@"select * FROM [VersionPrerequisites] where Version='" + version + "' and ZipType='" + ziptype + "'");

                return zipversionprereqs.ToList();
            }

            catch (SqlException ex)
            {
                WritetoEventLog("GetVersionPrerequisites - SQL Connectivity issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
                return null;
            }

        }

        public static string GetZipNamefromWdpVersion(string type, string versionkey)
        {
            return GetZipVersionData(versionkey, type)?.ZipName;
        }

        public static string GetUrlfromWdpVersion(string type, string versionkey)
        {
            return GetZipVersionData(versionkey, type)?.Url;
        }

        public static void SetVersionsDictionary()
        {
            VersionList.CommerceVersions = new Dictionary<string, string>
            {
                { "9.2", "One" },
                { "9.3", "One" },
                { "10.0", "Sitecore.Commerce.WDP.2020.08-6.0.238" }
            };

            VersionList.CommerceContainerVersions = new Dictionary<string, string>
            {
                { "9.2", "One" },
                { "9.3", "One" },
                { "10.0", "Sitecore.Commerce.Container.SDK.1.0.214" }
            };

            VersionList.SitecoreDevSetupVersions = new Dictionary<string, string>
            {
                { "9.2", "One" },
                { "9.3", "One" },
                { "10.0", "Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)" }
            };

            VersionList.SitecoreContainerVersions = new Dictionary<string, string>
            {
                { "9.2", "One" },
                { "9.3", "One" },
                { "10.0", "Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)" }
            };

            VersionList.CommerceWdpList=new Dictionary<string, string>
            {
                { "SIF", "One" },
                { "9.3", "One" },
                { "10.0", "Sitecore 10.0.0 rev. 004346 (Setup XP0 Developer Workstation rev. 1.2.0-r64)" }
            };

        }

        public static bool CheckSubDirectories(string folderPath)
        {
            if (Directory.GetDirectories(folderPath).Length > 0)
            {
                return true;
            }

            return false;
        }

        public static bool VersionCheck(string version, string sitecoreversion)
        {
            string[] parts = version.Split('.');
            var majorversion = parts[0];
            var minorversion = parts[1];
            var subversion = parts[2];

            switch (sitecoreversion)
            {
                case "10.0":
                    if (Convert.ToInt32(majorversion) >= 8) { return true; }
                    if (Convert.ToInt32(minorversion) >= 4) { return true; }
                    break;
                case "9.3":
                    if (Convert.ToInt32(majorversion) >= 8) { return true; }
                    if (Convert.ToInt32(minorversion) >= 1) { return true; }
                    if (Convert.ToInt32(subversion) >= 1) { return true; }
                    break;
                case "9.2":
                    if (Convert.ToInt32(majorversion) >= 7) { return true; }
                    if (Convert.ToInt32(minorversion) >= 5) { return true; }
                    break;
                case "9.1":
                    if (Convert.ToInt32(majorversion) >= 7) { return true; }
                    if (Convert.ToInt32(minorversion) >= 2) { return true; }
                    if (Convert.ToInt32(subversion) >= 1) { return true; }
                    break;
                case "9.0":
                    if (Convert.ToInt32(majorversion) >= 6) { return true; }
                    if (Convert.ToInt32(minorversion) >= 6) { return true; }
                    if (Convert.ToInt32(subversion) >= 5) { return true; }
                    break;
                default:
                    break;
            }
            

            return false;
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


        public static bool FileSystemEntryExists(string folderPath, string searchPattern = null,string type="file",bool checksubdirs=false)
        {
            if (!string.IsNullOrWhiteSpace(searchPattern))
            {
                if (Directory.EnumerateFileSystemEntries(folderPath, searchPattern).Any()) return true;            }
            else
            {
                if (type=="folder")
                {
                    if (!checksubdirs) { if (Directory.Exists(folderPath)) return true; }
                    if (Directory.Exists(folderPath) && CheckSubDirectories(folderPath)) return true;
                }
                else
                { if (File.Exists(folderPath)) return true; }
                
            }

            return false;
        }

        public static void LaunchPSScript(string scriptname,string workingDirectory=".")
        {
            var script = scriptname;
            var startInfo = new ProcessStartInfo()
            {
                FileName = @"powershell.exe",
                WorkingDirectory = workingDirectory,
                Arguments = $"-NoProfile -noexit -ExecutionPolicy Bypass \"{script}\"",
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }

        public static void LaunchCmdScript(string dockerCmd, string workingDirectory = ".")
        {
            var script = dockerCmd;
            var startInfo = new ProcessStartInfo()
            {
                FileName = @"cmd.exe",
                Arguments = "/user:Administrator \"cmd /K " + script + "\"",
                WorkingDirectory = workingDirectory,
                UseShellExecute = false
            };
            Process.Start(startInfo);
        }


        public static bool SCIARecordExists(string connectionString, string siteName)
        {

            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                SqlCommand comm = new SqlCommand("SELECT COUNT(1) FROM [SCIA] where SiteName='" + siteName + "'", conn);
                int recCount = (int)comm.ExecuteScalar();
                if (recCount > 0) { return true; } else { return false; }
            }
            catch(Exception ex)
            {
                CommonFunctions.WritetoEventLog("Get SCIA Record Count - SQL issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
                return false;
            }
        }


        public static SiteDetails GetSCIAData(string connectionString, string siteName)
        {
            SiteDetails siteData = null;
            try
            {
                using var dc = new DataContext(connectionString);
                var siteDetails = dc.ExecuteQuery<SiteDetails>(@"select top(1)* FROM [SCIA] where SiteName='" + siteName + "'").ToList();

                if (!siteDetails.Any()) return null;
                siteData= siteDetails.FirstOrDefault();
            }

            catch (SqlException ex)
            {
                CommonFunctions.WritetoEventLog("Get SCIA Data - SQL Connectivity issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
            }
            return siteData;
        }

        public static SIFDetails GetSIFData(string connectionString, string siteName)
        {
            SIFDetails siteData = null;
            try
            {
                using var dc = new DataContext(connectionString);
                var siteDetails = dc.ExecuteQuery<SIFDetails>(@"select top(1)* FROM [SIF] where SiteName='" + siteName + "'").ToList();

                if (!siteDetails.Any()) return null;
                siteData = siteDetails.FirstOrDefault();
            }

            catch (SqlException ex)
            {
                CommonFunctions.WritetoEventLog("Get SIF Data - SQL Connectivity issues... check server and credential details...." + ex.Message, EventLogEntryType.Error);
            }
            return siteData;
        }


        public static bool DbTableExists(string strTableNameAndSchema, SqlConnection connection)
        {
            try
            {
                string strCheckTable =
                    String.Format(
                        "IF OBJECT_ID('{0}', 'U') IS NOT NULL SELECT 'true' ELSE SELECT 'false'",
                        strTableNameAndSchema);
                SqlCommand command = new SqlCommand(strCheckTable, connection)
                {
                    CommandType = CommandType.Text
                };

                return Convert.ToBoolean(command.ExecuteScalar());
            }
            catch (SqlException ex)
            {

                WritetoEventLog("SCIA - Error while checking if Settings Table Exists" + ex.Message, EventLogEntryType.Error);
                return false;
            }

        }


        public static void WritetoEventLog(string message, EventLogEntryType type, string application = "Application")
        {
            using (EventLog eventLog = new EventLog(application))
            {
                eventLog.Source = application;
                eventLog.WriteEntry(message, type, 101, 1);
            }
        }

        public static SolrInfo DeserializeJson(string url)
        {
            try
            {
                using (WebClient client = new WebClient())
                {

                    //add HTTP headers for content type, and others
                    client.Headers["Content-type"] = "application/json";

                    //add HTTP header for remote authentication credentials for web service 
                    //( in this case the Default Network Credentials, because the service is ours)
                    client.UseDefaultCredentials = true;
                    client.Credentials = CredentialCache.DefaultNetworkCredentials;

                    //add HTTP headers to authenticate to our web proxy 
                    //( in this case the Default Network Credentials)
                    client.Proxy.Credentials = CredentialCache.DefaultNetworkCredentials;

                    // call the service & return the results to a byte array.
                    // (Some error handling might be good here)
                    byte[] bytedata = client.DownloadData(url);

                    // load this to a memory stream
                    MemoryStream ms = new MemoryStream();
                    ms = new MemoryStream(bytedata);

                    //Now Deserialize to a c# objects....
                    var serializer = new DataContractJsonSerializer(typeof(SolrInfo));
                    SolrInfo solrInfo = (SolrInfo)serializer.ReadObject(ms);

                    return solrInfo;
                }
            }
            catch(Exception ex)
            {
                WritetoEventLog("Could not establish connection with Solr - " + url + ex.Message, EventLogEntryType.Error);
                return null;
            }
        }


        public static string GetSolrUrl(string webSitePath)
        {
            var configFilePath = webSitePath + "\\App_Config\\ConnectionStrings.Config";
            var solrUrl = string.Empty;
            if (File.Exists(configFilePath))
            {
                string[] lines = File.ReadAllLines(configFilePath);

                IEnumerable<string> selectLines = lines.Where(line => line.Contains("solr.search"));

                foreach (var item in selectLines)
                {
                    int indexofSolr = item.LastIndexOf("solr");
                    int indexofhttp = item.LastIndexOf("http");
                    int indexofSolrFinish = indexofSolr + 4;
                    int lengthOfSolrUrl = indexofSolrFinish - indexofhttp;

                    solrUrl = item.Substring(indexofhttp, lengthOfSolrUrl);
                }
            }

            return solrUrl;
        }


        public static SolrInfo GetSolrInformation(string url)
        {
            string solrInfoUrl = $"{url}/admin/info/system";
            SolrInfo solrObj = DeserializeJson(solrInfoUrl);

            return solrObj;
        }

        public static string BuildConnectionString(string datasource, string dbname, string uid, string pwd, bool firstTime = false)
        {
            if (!firstTime)
                return "Data Source=" + datasource + "; Initial Catalog=" + dbname + "; User ID=" + uid + "; Password=" + pwd;

            return "Data Source=" + datasource + "; Initial Catalog=" + dbname + "; User ID=" + uid + "; Password=" + pwd + "; database=master";
        }

    }
    public class WebClientResponse : WebClient
    {
        public bool HeadOnly { get; set; }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest req = base.GetWebRequest(address);
            if (HeadOnly && req.Method == "GET")
            {
                req.Method = "HEAD";
            }
            return req;
        }
    }

    public class DBServerDetails
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Server { get; set; }

        public bool IsSettingsPresent { get; set; }
    }

    

}
