using Agrisustain_Jamaica.Models;
using System.Text.Json;

namespace Agrisustain_Jamaica.Services
{
    public class JSONFilePlantingGuidesService
    {
        public JSONFilePlantingGuidesService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "plantingGuides.json"); }
        }

        public IEnumerable<PlantingGuides>? GetPlantingGuidesInfo()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<PlantingGuides[]>(jsonFileReader.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });

            }

        }

        public IEnumerable<CropData>? GetData()
        {

            var cropData = GetPlantingGuidesInfo()?.ElementAt(0).Crops;
            var jsonSerializer = JsonSerializer.Serialize(cropData);
            return JsonSerializer.Deserialize<CropData[]>(jsonSerializer);

        }


    }
}
