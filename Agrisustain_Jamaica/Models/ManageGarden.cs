using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using System.Text.Json.Serialization;
using static System.Net.Mime.MediaTypeNames;

namespace AgriSustain_Jamaica.Models
{
  public class ManageGarden
    {
        public object[] CropInventory { get; set; }
        


        public IEnumerable<ManageGarden> getData()
        
        {
            string text = File.ReadAllText(@"wwwroot\data\cropData.json");
            var classObj = JsonSerializer.Deserialize<ManageGarden[]>(text);
            return classObj;
        }

        

        //public override string ToString()
        //{
        // return JsonSerializer.Serialize<Garden>(this);
        // }

    }

    public class cropDetails
    {
        public int Id { get; set; }
        public string CropType { get; set; }
        public int Quantity { get; set; }
        public string Community { get; set; }
        public string[] Supplies { get; set; }
        public string[] GardeningTools { get; set; }
        public string Comments { get; set; }


        public IEnumerable<cropDetails> getCategoryData()

        {


            var categories = new ManageGarden();
            var data = categories.getData().ElementAt(0).CropInventory;

            var jsonSerializer = JsonSerializer.Serialize(data);
        

            var categoryObj = JsonSerializer.Deserialize<cropDetails[]>(jsonSerializer);
            return categoryObj;

        }
    }
}