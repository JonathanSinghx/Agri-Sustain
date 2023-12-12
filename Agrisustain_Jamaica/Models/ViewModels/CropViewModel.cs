using Agrisustain_Jamaica.Models.WeatherForecastDataModels;

namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class CropViewModel
    {
        public Guid Id { get; set; }
        public string CropName { get; set; }
        public string Description { get; set; }
        public DateTime DatePlanted { get; set; } = DateTime.Now;
        //public string CropStatus { get; set; }
        public DateTime HarvestDate { get; set; }
    }

    public class SavedCropsModel
    {
        public List<CropViewModel> cropsList = new List<CropViewModel>();
    }
}
