namespace Agrisustain_Jamaica.Models.CropPlanning
{
    public class HarvestPlanning
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? CropStatus { get; set; }
        public string TargetCrops { get; set; }
    }

    public class SavedHarvestEventsModel
    {
        public List<HarvestPlanning> eventsList = new List<HarvestPlanning>();
    }
}   
