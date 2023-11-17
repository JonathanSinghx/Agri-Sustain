using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;
using static System.Net.Mime.MediaTypeNames;

namespace AgriSustain_Jamaica.Models
{
    public class RecipeInfo
    {
        public void OnGet()
        {

        }

    }
}

//        public object[] RecipeList { get; set; }



//        public IEnumerable<ShareProduce> getData()

//        {
//            string text = File.ReadAllText(@"C:\Users\js614673\source\repos\AgriSustain_Jamaica\Agrisustain_Jamaica\wwwroot\data\cropData.json");
//            var classObj = JsonSerializer.Deserialize<ShareProduce[]>(text);
//            return classObj;
//        }

//    }

//    public class cropInfo
//    {
//        public int Id { get; set; }
//        public string CropType { get; set; }
//        public int Quantity { get; set; }
//        public string Community { get; set; }
//        public string[] Supplies { get; set; }
//        public string[] GardeningTools { get; set; }
//        public string Comments { get; set; }

//        public string Image { get; set; }


//        public IEnumerable<cropInfo> getCategoryInfo()

//        {


//            var categories = new ShareProduce();
//            var data = categories.getData().ElementAt(0).CropInventory;

//            var jsonSerializer = JsonSerializer.Serialize(data);


//            var categoryObject = JsonSerializer.Deserialize<cropInfo[]>(jsonSerializer);
//            return categoryObject;

//        }
//    }
//}

