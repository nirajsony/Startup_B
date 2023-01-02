using Newtonsoft.Json;

namespace Startup_B.Models.Stock
{
    public class StockModel
    {
        [JsonProperty("Sr")] 
        public string Sr { get; set; }

        [JsonProperty("Brand")]
        public string Brand { get; set; }

        [JsonProperty("Asset Type")]
        public string AssetType { get; set; }

        [JsonProperty("MAKE/MODEL")]
        public string MAKEMODEL { get; set; }

        [JsonProperty("SN/ID NUMBER")]
        public string SNIDNUMBER { get; set; }

        [JsonProperty("STATUS")]
        public string STATUS19072022 { get; set; }
    }
}
