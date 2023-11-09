using System.Text.Json.Serialization;
using System.Text.Json;

namespace Agrisustain_Jamaica.Models
{
    public class CropCare
    {
            [JsonPropertyName("_id")]
            public string? PostID { get; set; }

            [JsonPropertyName("main_heading")]
            public string? MainHeading { get; set; }

            [JsonPropertyName("intro")]
            public string[]? BlogIntro { get; set; }

            [JsonPropertyName("guides")]
            public object[]? CropCareInfo { get; set; }

            public override string ToString()
            {
                //taking all the object info and converting it back into the text that will be part of the JSON file. To serialize means to take one after the other
                return JsonSerializer.Serialize<CropCare>(this);
            }

        public class CropCareData
        {
            [JsonPropertyName("id")]
            public string? ID { get; set; }

            [JsonPropertyName("name")]
            public string? Name { get; set; }

            [JsonPropertyName("info")]
            public object[]? CropCareInstructions { get; set; }

            public override string ToString()
            {
                return JsonSerializer.Serialize<CropCareData>(this);
            }

        }
    }
}
