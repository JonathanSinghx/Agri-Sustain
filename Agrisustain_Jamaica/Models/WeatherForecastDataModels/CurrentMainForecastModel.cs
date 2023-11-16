using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class CurrentMainForecastModel
    {
        //[JsonPropertyName("sunrise")]
        //public long Sunrise { get; set;}

        //[JsonPropertyName("sunset")]
        //public long Sunset { get; set; }

        [JsonPropertyName("temp")]
        public double Temperature { get; set; }

        [JsonPropertyName("feels_like")]
        public double FeelsLike { get; set; }

        [JsonPropertyName("pressure")]
        public double Pressure { get; set; }

        [JsonPropertyName("humidity")]
        public double Humidity { get; set; }

        //[JsonPropertyName("dew_point")]
        //public double DewPoint { get; set; }

        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        //[JsonPropertyName("wind_speed")]
        //public double WindSpeed { get; set; }

        //[JsonPropertyName("wind_deg")]
        //public double WindDegree { get; set; }

        //[JsonPropertyName("wind_gust")]
        //public double WindGust { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<CurrentMainForecastModel>(this);
        }
    }
}
