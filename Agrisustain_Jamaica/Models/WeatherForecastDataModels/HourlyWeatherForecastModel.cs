using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class HourlyWeatherForecastModel
    {
        [JsonPropertyName("dt")]
        public long Time { get; set; }

        //[JsonPropertyName("temp")]
        //public double Temperature { get; set; }
        [JsonPropertyName("weather")]
        public object[] Weather { get; set; }

        //[JsonPropertyName("icon")]
        //public string Icon { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<HourlyWeatherForecastModel>(this);
        }
    }

    public class HourlyWeatherForecasts
    {
        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("icon")]
        public string Icon { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<HourlyWeatherForecasts>(this);
        }
    }
}
