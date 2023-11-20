
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class AirQualityIndexModel
    {
        [JsonPropertyName("main")]
        public object AirQualityIndex { get; set; }

        [JsonPropertyName("components")]
        public object Components { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<AirQualityIndexModel>(this);
        }
    }

    public class AirQualityIndexData
    {
        [JsonPropertyName("aqi")]
        public object Index { get; set; }
        [JsonPropertyName("co")]
        public decimal CarbonMonoxide { get; set; }
        [JsonPropertyName("no")]
        public decimal NitrogenMonoxide { get; set; }
        [JsonPropertyName("no2")]
        public decimal NitrogenDioxide { get; set; }
        [JsonPropertyName("o3")]
        public decimal Ozone {  get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<AirQualityIndexData>(this);
        }
    }
    
}
