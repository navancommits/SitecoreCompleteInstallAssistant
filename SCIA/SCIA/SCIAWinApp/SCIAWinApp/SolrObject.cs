﻿using System;
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

    public class SiteDetailsRecords
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
        public string CoreDbSuffix { get; set; }
        public string CommerceGlobalDbSuffix { get; set; }
        public string CommSharedDbSuffix { get; set; }
        public string UserSuffix { get; set; }
        public string StorefrontHostSuffix  { get; set; }
        public string HostSuffix { get; set; }
        public string HttpsString { get; set; }
        public string UserPassword { get; set; }
    }

    public static class Login
    {
        public static string username { get; set; }
        public static string password { get; set; }

        public static bool rememberMe { get; set; }
        public static string requestUrl { get; set; }
        public static bool Success { get; set; }
    }


    public class SiteDetailList
    {
        public List<SiteDetails> SiteDetailRecords { get; set; }
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
        public string SqlUser { get; set; }
        public string SqlPass { get; set; }
        public string SitecoreSqlPass { get; set; }
        public string SitecoreDomain { get; set; }
        public string SitecoreUsername { get; set; }
        public string SitecoreUserPassword { get; set; }
        public string SearchIndexPrefix { get; set; }
        public string SolrUrl { get; set; }
        public string SolrRoot { get; set; }
        public string SolrService { get; set; }
        public string RedisHost { get; set; }
        public short RedisPort { get; set; }
        public string CommerceServicesDBServer { get; set; }
        public string CommerceDbName { get; set; }
        public string CommerceGlobalDbName { get; set; }
        public short CommerceOpsSvcPort { get; set; }
        public short CommerceShopsServicesPort { get; set; }
        public short CommerceAuthSvcPort { get; set; }
        public short CommerceMinionsSvcPort { get; set; }
        public string IdServerSiteHostName { get; set; }
        public string SiteHostName { get; set; }
        public string CommerceOpsSiteHostName { get; set; }
        public string CommerceShopsSiteHostName { get; set; }
        public string CommerceAuthSiteHostName { get; set; }
        public string CommerceMinionsSiteHostName { get; set; }
        public string BizFxSiteHostName { get; set; }
        public string HostPostFixforContainer { get; set; }
        public string CommerceServicesHostPostFix { get; set; }
        public string CommerceSvcPostFix { get; set; }
        public string BizFxName { get; set; }
        public short BizFxPort { get; set; }
        public string EnvironmentsPrefix { get; set; }
        public string SitecoreCoreDbName { get; set; }
        public string DeploySampleData { get; set; }
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
