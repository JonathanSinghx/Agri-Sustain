using Agrisustain_Jamaica.Models.ViewModels;

namespace Agrisustain_Jamaica.Models.CropPlanning
{
    public class FertilizationPlanning
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string? GrowthStage { get; set; }
        public string TargetCrops { get; set; }
        public string TargetPests { get; set; }
        public string? ApplicationPattern { get; set; }
    }

    public class SavedFertilizationEventsModel
    {
        public List<FertilizationPlanning> eventsList = new List<FertilizationPlanning>();
    }
}
