namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class UpdateHarvestEventModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? CropStatus { get; set; }
        public string TargetCrops { get; set; }
    }
}
