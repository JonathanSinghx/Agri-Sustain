namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class AddSprayEventModel
    {
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string? Reason { get; set; }
        public string? GrowthStage { get; set; }
        public string TargetCrops { get; set; }
        public string TargetPests { get; set; }
        public string? ApplicationPattern { get; set; }
    }
}
