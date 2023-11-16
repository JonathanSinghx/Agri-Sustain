using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class SunriseAndSunsetModel
    {
        [JsonPropertyName("sunrise")]
        public long Sunrise { get; set; }

        [JsonPropertyName("sunset")]
        public long Sunset { get; set; }
    }
}
