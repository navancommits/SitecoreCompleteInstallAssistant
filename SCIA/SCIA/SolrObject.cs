using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace SCIA
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class ResponseHeader
    {
        public int status { get; set; }
        public int QTime { get; set; }
    }

    public class Lucene
    {
        [JsonProperty("solr-spec-version")]
        public string SolrSpecVersion { get; set; }
        [JsonProperty("solr-impl-version")]
        public string SolrImplVersion { get; set; }
        [JsonProperty("lucene-spec-version")]
        public string LuceneSpecVersion { get; set; }
        [JsonProperty("lucene-impl-version")]
        public string LuceneImplVersion { get; set; }
    }

    public class Spec
    {
        public string vendor { get; set; }
        public string name { get; set; }
        public string version { get; set; }
    }

    public class Jre
    {
        public string vendor { get; set; }
        public string version { get; set; }
    }

    public class Vm
    {
        public string vendor { get; set; }
        public string name { get; set; }
        public string version { get; set; }
    }

    public class Raw
    {
        public int free { get; set; }
        public int total { get; set; }
        public int max { get; set; }
        public int used { get; set; }
        [JsonProperty("used%")]
        public double Used { get; set; }
    }

    public class Memory
    {
        public string free { get; set; }
        public string total { get; set; }
        public string max { get; set; }
        public string used { get; set; }
        public Raw raw { get; set; }
    }

    public class Jmx
    {
        public string bootclasspath { get; set; }
        public string classpath { get; set; }
        public List<string> commandLineArgs { get; set; }
        public DateTime startTime { get; set; }
        public int upTimeMS { get; set; }
    }

    public class Jvm
    {
        public string version { get; set; }
        public string name { get; set; }
        public Spec spec { get; set; }
        public Jre jre { get; set; }
        public Vm vm { get; set; }
        public int processors { get; set; }
        public Memory memory { get; set; }
        public Jmx jmx { get; set; }
    }

    public class SolrSystem
    {
        public string name { get; set; }
        public string arch { get; set; }
        public int availableProcessors { get; set; }
        public double systemLoadAverage { get; set; }
        public string version { get; set; }
        public int committedVirtualMemorySize { get; set; }
        public long freePhysicalMemorySize { get; set; }
        public long freeSwapSpaceSize { get; set; }
        public double processCpuLoad { get; set; }
        public long processCpuTime { get; set; }
        public double systemCpuLoad { get; set; }
        public long totalPhysicalMemorySize { get; set; }
        public long totalSwapSpaceSize { get; set; }
    }

    public class SolrInfo
    {
        public ResponseHeader responseHeader { get; set; }
        public string mode { get; set; }
        public string solr_home { get; set; }
        public Lucene lucene { get; set; }
        public Jvm jvm { get; set; }
        public SolrSystem system { get; set; }
    }


}
