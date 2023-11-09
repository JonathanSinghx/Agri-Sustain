using System.Text.Json.Serialization;
using System.Text.Json;

namespace Agrisustain_Jamaica.Models
{
    public class Irrigation
    {
        [JsonPropertyName("id")]
        public string? PostID { get; set; }

        [JsonPropertyName("main_heading")]
        public string? MainHeading { get; set; }

        [JsonPropertyName("intro")]
        public string[]? BlogIntro { get; set; }

        [JsonPropertyName("sub-headings")]
        public object[]? IrrigationMethods { get; set; }

        public override string ToString()
        {
            //taking all the object info and converting it back into the text that will be part of the JSON file. To serialize means to take one after the other
            return JsonSerializer.Serialize<Irrigation>(this);
        }
    }

    public class SubHeadings
    {
        [JsonPropertyName("_id")]
        public string? ID { get; set; }

        [JsonPropertyName("sub-heading")]
        public string? Subheading { get; set; }

        [JsonPropertyName("info")]
        public string[]? Info { get; set; }

        public override string ToString()
        {
            return JsonSerializer.Serialize<SubHeadings>(this);
        }


    }
}
