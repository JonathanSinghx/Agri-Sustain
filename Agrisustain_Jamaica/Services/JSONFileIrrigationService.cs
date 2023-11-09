using Agrisustain_Jamaica.Models;
using System.Text.Json;

namespace Agrisustain_Jamaica.Services
{
    public class JSONFileIrrigationService
    {
        // public string subHeading { get; set; }
        public JSONFileIrrigationService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "irrigation.json"); }
        }

       
        public IEnumerable<Irrigation>? GetIrrigationInfo()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<Irrigation[]>(jsonFileReader.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });

            }

        }

        public IEnumerable<SubHeadings>? GetData()
        {

            var irrigationData = GetIrrigationInfo()?.ElementAt(0).IrrigationMethods;
            var jsonSerializer = JsonSerializer.Serialize(irrigationData);
            return JsonSerializer.Deserialize<SubHeadings[]>(jsonSerializer);

        }

    }
}
