using Agrisustain_Jamaica.Models;
using static Agrisustain_Jamaica.Models.CropCare;
using System.Text.Json;

namespace Agrisustain_Jamaica.Services
{
    public class JSONFileCropCareService
    {
        // public string subHeading { get; set; }
        public JSONFileCropCareService(IWebHostEnvironment webHostEnvironment)
        {
            WebHostEnvironment = webHostEnvironment;
        }

        public IWebHostEnvironment WebHostEnvironment { get; }

        private string JsonFileName
        {
            get { return Path.Combine(WebHostEnvironment.WebRootPath, "data", "cropCare.json"); }
        }

        public IEnumerable<CropCare>? GetCropCareInfo()
        {
            using (var jsonFileReader = File.OpenText(JsonFileName))
            {
                return JsonSerializer.Deserialize<CropCare[]>(jsonFileReader.ReadToEnd(),
               new JsonSerializerOptions
               {
                   PropertyNameCaseInsensitive = true
               });

            }

        }

        public IEnumerable<CropCareData>? GetData()
        {

            var cropCareData = GetCropCareInfo()?.ElementAt(0).CropCareInfo;
            var jsonSerializer = JsonSerializer.Serialize(cropCareData);
            return JsonSerializer.Deserialize<CropCareData[]>(jsonSerializer);


        }

    }
}
