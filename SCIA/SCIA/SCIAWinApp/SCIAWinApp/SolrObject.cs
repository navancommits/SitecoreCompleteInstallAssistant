using System.Collections.Generic;
using System.Runtime.Serialization;

namespace SCIA
{

    [DataContract]
    public class Lucene
    {
        [DataMember(Name = "solr-spec-version")]
        public string SolrSpecVersion { get; set; }        
       
    }

    [DataContract]
    public class SolrInfo
    {
        [DataMember]
        public string solr_home { get; set; }
        [DataMember]
        public Lucene lucene { get; set; }
        
    }

    public class SiteDetailsRecord
    {
        public List<SiteDetails> SiteDetailsList { get; set; }
    }

    public class SettingsData
    {
        public string SiteNameSuffix { get; set; }
        public string SitePrefixString { get; set; }
        public string IdentityServerNameString { get; set; }
        public string xConnectServerNameString { get; set; }
        public string CommerceEngineConnectClientId { get; set; }
        public string CommerceEngineConnectClientSecret { get; set; }
        public string SiteRootDir { get; set; }
        public string SitecoreDomain { get; set; }
        public string SitecoreUsername { get; set; }
        public string SearchIndexPrefix { get; set; }
        public string RedisHost { get; set; }
        public string RedisPort { get; set; }
        public string BizFxSitenamePrefix { get; set; }
        public string EnvironmentsPrefix { get; set; }
        public string CommerceDbNameString { get; set; }
        public string UserDomain { get; set; }
        public string BraintreeMerchantId { get; set; }
        public string BraintreePublicKey { get; set; }
        public string BraintreePrivateKey { get; set; }
        public string BraintreeEnvironment { get; set; }

    }

    public class SiteDetails
    {
        public string SiteNamePrefix { get; set; }
        public string SiteNameSuffix { get; set; }
        public string SiteName { get; set; }
        public string IDServerSiteName { get; set; }
        public string SitecoreIdentityServerUrl { get; set; }
        public string CommerceEngineConnectClientId { get; set; }
        public string CommerceEngineConnectClientSecret { get; set; }
        public string SiteHostHeaderName { get; set; }
        public string SXAInstallDir { get; set; }
        public string xConnectInstallDir { get; set; }
        public string CommerceInstallRoot { get; set; }
        public string SitecoreDbServer { get; set; }
        public string SitecoreSqlUser { get; set; }
        public string SitecoreSqlPass { get; set; }
        public string SitecoreDomain { get; set; }
        public string SitecoreUsername { get; set; }
        public string SitecoreUserPassword { get; set; }
        public string SearchIndexPrefix { get; set; }
        public string SolrUrl { get; set; }
        public string SolrRoot { get; set; }
        public string SolrService { get; set; }
        public string RedisHost { get; set; }
        public string RedisPort { get; set; }
        public string CommerceServicesDBServer { get; set; }
        public string CommerceDbName { get; set; }
        public string CommerceGlobalDbName { get; set; }
        public string CommerceOpsSvcPort { get; set; }
        public string CommerceShopsServicesPort { get; set; }
        public string CommerceAuthSvcPort { get; set; }
        public string CommerceMinionsSvcPort { get; set; }
        public string CommerceSvcPostFix { get; set; }
        public string CommerceServicesHostPostFix { get; set; }
        public string BizFxName { get; set; }
        public string BizFxPort { get; set; }
        public string EnvironmentsPrefix { get; set; }
        public string SitecoreCoreDbName { get; set; }
        public bool DeploySampleData { get; set; }
        public string UserName { get; set; }
        public string UserDomain { get; set; }
        public string UserPassword { get; set; }
        public string BraintreeMerchantId { get; set; }
        public string BraintreePublicKey { get; set; }
        public string BraintreePrivateKey { get; set; }
        public string BraintreeEnvironment { get; set; }       
        public bool HabitatExists { get; set; }
    }
}
