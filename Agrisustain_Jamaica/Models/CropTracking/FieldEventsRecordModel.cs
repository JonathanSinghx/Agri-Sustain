using Agrisustain_Jamaica.Models.CropPlanning;

namespace Agrisustain_Jamaica.Models.CropTracking
{
    public class FieldEventsRecordModel
    {
        public List<FertilizationPlanning> FertilizationHistory { get; set; }
        public List<IrrigationPlanning> IrrigationHistory { get; set; }
        public List<SprayPlanning> SprayingHistory { get; set; }
        public List<PlantingPlanning> PlantingHistory { get; set; }
        public List<PruningPlanning> PruningHistory { get; set; }
        public List<HarvestPlanning> HarvestHistory { get; set; }

        public FieldEventsRecordModel()
        {
            FertilizationHistory = new List<FertilizationPlanning>();
            IrrigationHistory = new List<IrrigationPlanning>();
            SprayingHistory = new List<SprayPlanning>();
            PlantingHistory = new List<PlantingPlanning>();
            PruningHistory = new List<PruningPlanning>();
            HarvestHistory = new List<HarvestPlanning>();

        }

    }
}
