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


}
