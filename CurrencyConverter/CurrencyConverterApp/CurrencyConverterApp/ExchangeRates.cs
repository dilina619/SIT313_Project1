using Newtonsoft.Json;

namespace CurrencyConverterApp
{
    public class ExchangeRates
    {
        [JsonProperty("disclaimer")]
        public string Disclaimer { get; set; }
        [JsonProperty("license")]
        public string License { get; set; }
        [JsonProperty("timestamp")]
        public long TimeStamp { get; set; }
        [JsonProperty("base")]
        public string CurrencyBase { get; set; }
        [JsonProperty("rates")]
        public Rates Rates { get; set; }
    }

    public class Rates
    {
        public double SEK { get; set; } //Swedish Crown
        public double EUR { get; set; } //Euro
        public double USD { get; set; } //Dollar
        public double RON { get; set; } //Romanian Leu
    }
}
