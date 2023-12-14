namespace Agrisustain_Jamaica.Models.CropPlanning
{
    public class PruningPlanning
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public string? GrowthStage { get; set; }
        public string TargetCrops { get; set; }
    }

    public class SavedPruningEventsModel
    {
        public List<PruningPlanning> eventsList = new List<PruningPlanning>();
    }
}
