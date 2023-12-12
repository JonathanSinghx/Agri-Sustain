namespace Agrisustain_Jamaica.Models.ViewModels
{
    public class UpdateCropViewModel
    {
        public Guid Id { get; set; }
        public string CropName { get; set; }
        public string Description { get; set; }
        public DateTime DatePlanted { get; set; } = DateTime.Now;
        //public string CropStatus { get; set; }
        public DateTime HarvestDate { get; set; }
    }
}
