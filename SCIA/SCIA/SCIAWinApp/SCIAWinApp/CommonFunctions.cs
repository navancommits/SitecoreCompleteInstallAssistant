using System;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Runtime.Serialization.Json;



namespace SCIA
{
    public static class CommonFunctions
    {
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
            // Open the connection [SiteNameSuffix], [SitePrefixString],[IdentityServerNameAdditional] 
            var sql = "CREATE TABLE Settings" +
            "(" +
            "SiteNameSuffix CHAR(50), SitePrefixString CHAR(50), IdentityServerNameString CHAR(50), xConnectServerNameString CHAR(50), CommerceEngineConnectClientId CHAR(50), CommerceEngineConnectClientSecret CHAR(50), SiteRootDir CHAR(100), SitecoreDomain CHAR(50), SitecoreUsername CHAR(50),SearchIndexPrefix  CHAR(50),RedisHost  CHAR(50),RedisPort  CHAR(10),BizFxSitenamePrefix  CHAR(50),EnvironmentsPrefix  CHAR(50),CommerceDbNameString  CHAR(50),UserDomain CHAR(50), BraintreeMerchantId CHAR(100),BraintreePublicKey CHAR(100),BraintreePrivateKey CHAR(100),BraintreeEnvironment CHAR(100),created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP)";
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
    }
}
