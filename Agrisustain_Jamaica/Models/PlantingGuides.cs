using System.Text.Json.Serialization;
using System.Text.Json;

namespace Agrisustain_Jamaica.Models
{
    public class PlantingGuides
    {
        [JsonPropertyName("_id")]
        public string? PostID { get; set; }

        [JsonPropertyName("main_heading")]
        public string? MainHeading { get; set; }

        [JsonPropertyName("intro")]
        public string[]? BlogIntro { get; set; }

        [JsonPropertyName("crops")]
        public object[]? Crops { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<PlantingGuides>(this);
        }
    }

    public class CropData
    {
        [JsonPropertyName("id")]
        public string? CropID { get; set; }

        [JsonPropertyName("crop")]
        public string? CropName { get; set; }

        [JsonPropertyName("seasons")]
        public string[]? Seasons { get; set; }

        [JsonPropertyName("info")]
        public string[]? CropInfo { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<CropData>(this);
        }

    }
}
