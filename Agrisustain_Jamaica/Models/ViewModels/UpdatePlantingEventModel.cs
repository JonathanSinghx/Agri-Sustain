namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class UpdatePlantingEventModel
    {
        public Guid Id { get; set; }
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; }
        public string Crop { get; set; }
        public string? QuantityDescription { get; set; }
    }
}
