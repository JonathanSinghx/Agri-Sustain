using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class CurrentWeatherConditionModel
    {
        [JsonPropertyName("main")]
        public string Forecast { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<CurrentWeatherConditionModel>(this);
        }
    }
}
