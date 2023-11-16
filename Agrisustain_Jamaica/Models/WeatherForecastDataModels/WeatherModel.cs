using System.Text.Json;
using System.Text.Json.Serialization;

namespace Agrisustain_Jamaica.Models.WeatherForecastDataModels
{
    public class WeatherModel
    {
      
      
        [JsonPropertyName("visibility")]
        public int Visibility { get; set; }

        [JsonPropertyName("dt")]
        public long CurrentWeatherDateTime { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<WeatherModel>(this);
        }
    }
}
