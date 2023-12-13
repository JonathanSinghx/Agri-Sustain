namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class AddHarvestEventModel
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? CropStatus { get; set; }
        public string TargetCrops { get; set; }
    }
}
