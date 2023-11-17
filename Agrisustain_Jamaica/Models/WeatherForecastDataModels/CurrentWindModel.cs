using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class CurrentWindModel
    {
        [JsonPropertyName("speed")]
        public double Speed { get; set; }

        [JsonPropertyName("deg")]
        public double Degree { get; set; }

        [JsonPropertyName("gust")]
        public double Gust { get; set; }
    }
}
