using Newtonsoft.Json;

namespace RealEstatePriceMonitor.Classes
{
    public class PriceDataRecord
    {
        [JsonProperty(PropertyName = "id")]
        private string id {  get; set; }
        [JsonProperty(PropertyName = "city")]
        private string city { get; set; }
        [JsonProperty(PropertyName = "date")]
        private string date { get; set; }
        [JsonProperty(PropertyName = "rentPrices")]
        private int[] rentPrices { get; set; }
        public PriceDataRecord(string id, string city, string date, int[] rentPrices)
        {
            this.id = id;
            this.city = city;
            this.date = date;
            this.rentPrices = rentPrices;
        }
    }
}
