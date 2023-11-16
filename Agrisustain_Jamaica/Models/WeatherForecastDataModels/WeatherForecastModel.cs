using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class WeatherForecastModel
    {
        [JsonPropertyName("lat")]
        public double Latitude { get; set; }

        [JsonPropertyName("lon")]
        public double Longitude { get; set; }

        [JsonPropertyName("timezone")]
        public string TimeZone { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<WeatherForecastModel>(this);
        }
    }
}
