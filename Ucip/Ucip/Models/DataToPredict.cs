using Newtonsoft.Json;

namespace Ucip.Models
{
    public class DataToPredict
    {
        [JsonProperty(PropertyName = "fc")]
        public float Fc { get; set; }

        [JsonProperty(PropertyName = "spo2")]
        public float SpO2 { get; set; }

        [JsonProperty(PropertyName = "fr")]
        public float Fr { get; set; }

        [JsonProperty(PropertyName = "tam")]
        public float Tam { get; set; }

        [JsonProperty(PropertyName = "condition")]
        public int Condition { get; set; }
    }

    public class PreparedData
    {
        [JsonProperty(PropertyName = "data")]
        public List<DataToPredict> Data { get; set; }
    }
}
