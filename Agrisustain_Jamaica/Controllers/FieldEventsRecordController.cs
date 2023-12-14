using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.CropTracking;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class FieldEventsRecordController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public FieldEventsRecordController(RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext)
        {
           _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;
        }

        [HttpGet]
        public async Task<IActionResult> FieldHistory()
        {

            var fertilizationEvents = _retrieveFromAgrisustain.GetData("FertilizationPlanning");
            var irrigationEvents = _retrieveFromAgrisustain.GetData("IrrigationPlanning");
            var plantingEvents = _retrieveFromAgrisustain.GetData("PlantingPlanning");
            var harvestEvents = _retrieveFromAgrisustain.GetData("HarvestPlanning");
            var pruningEvents = _retrieveFromAgrisustain.GetData("PrunePlanning");
            var sprayEvents = _retrieveFromAgrisustain.GetData("SprayPlanning");

			FieldEventsRecordModel model = new FieldEventsRecordModel();
			

			if (fertilizationEvents.Rows.Count > 0)
			{
				for (int i = 0; i < fertilizationEvents.Rows.Count; i++)
				{
					FertilizationPlanning events = new FertilizationPlanning();
					if (Guid.TryParse(fertilizationEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(fertilizationEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(fertilizationEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.Reason = fertilizationEvents.Rows[i]["Reason"].ToString();
					events.GrowthStage = fertilizationEvents.Rows[i]["GrowthStage"].ToString();
					events.TargetCrops = fertilizationEvents.Rows[i]["TargetCrops"].ToString();
					events.TargetPests = fertilizationEvents.Rows[i]["TargetPests"].ToString();
					events.ApplicationPattern = fertilizationEvents.Rows[i]["ApplicationPattern"].ToString();

					model.FertilizationHistory.Add(events);
				}
			}

			if (irrigationEvents.Rows.Count > 0)
			{
				for (int i = 0; i < irrigationEvents.Rows.Count; i++)
				{
					IrrigationPlanning events = new IrrigationPlanning();
					if (Guid.TryParse(irrigationEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(irrigationEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(irrigationEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.Reason = irrigationEvents.Rows[i]["Reason"].ToString();
					events.GrowthStage = irrigationEvents.Rows[i]["GrowthStage"].ToString();
					events.TargetCrops = irrigationEvents.Rows[i]["TargetCrops"].ToString();
					events.ApplicationPattern = irrigationEvents.Rows[i]["ApplicationPattern"].ToString();

					model.IrrigationHistory.Add(events);
				}
			}

			if (plantingEvents.Rows.Count > 0)
			{
				for (int i = 0; i < plantingEvents.Rows.Count; i++)
				{
					PlantingPlanning events = new PlantingPlanning();
					if (Guid.TryParse(plantingEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(plantingEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(plantingEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.Crop = plantingEvents.Rows[i]["Crop"].ToString();
					events.QuantityDescription = plantingEvents.Rows[i]["QuantityDescription"].ToString();

					model.PlantingHistory.Add(events);
				}


			}

			if (harvestEvents.Rows.Count > 0)
			{
				for (int i = 0; i < harvestEvents.Rows.Count; i++)
				{
					HarvestPlanning events = new HarvestPlanning();
					if (Guid.TryParse(harvestEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(harvestEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(harvestEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.CropStatus = harvestEvents.Rows[i]["CropStatus"].ToString();
					events.TargetCrops = harvestEvents.Rows[i]["TargetCrops"].ToString();

					model.HarvestHistory.Add(events);
				}
			}

			if (pruningEvents.Rows.Count > 0)
			{
				for (int i = 0; i < pruningEvents.Rows.Count; i++)
				{
					PruningPlanning events = new PruningPlanning();
					if (Guid.TryParse(pruningEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(pruningEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(pruningEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.Reason = pruningEvents.Rows[i]["Reason"].ToString();
					events.GrowthStage = pruningEvents.Rows[i]["GrowthStage"].ToString();
					events.TargetCrops = pruningEvents.Rows[i]["TargetCrops"].ToString();

					model.PruningHistory.Add(events);
				}

			}

			if (sprayEvents.Rows.Count > 0)
			{
				for (int i = 0; i < sprayEvents.Rows.Count; i++)
				{
					SprayPlanning events = new SprayPlanning();
					if (Guid.TryParse(sprayEvents.Rows[i]["Id"].ToString(), out Guid id))
					{
						events.Id = id;
					}

					if (DateTime.TryParse(sprayEvents.Rows[i]["StartDate"].ToString(), out DateTime startDate))
					{
						// Conversion successful
						events.StartDate = startDate;
					}

					if (DateTime.TryParse(sprayEvents.Rows[i]["EndDate"].ToString(), out DateTime endDate))
					{
						// Conversion successful
						events.EndDate = endDate;
					}
					events.Reason = sprayEvents.Rows[i]["Reason"].ToString();
					events.GrowthStage = sprayEvents.Rows[i]["GrowthStage"].ToString();
					events.TargetCrops = sprayEvents.Rows[i]["TargetCrops"].ToString();
					events.TargetPests = sprayEvents.Rows[i]["TargetPests"].ToString();
					events.ApplicationPattern = sprayEvents.Rows[i]["ApplicationPattern"].ToString();

					model.SprayingHistory.Add(events);
				}
			}


			return View(model);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
