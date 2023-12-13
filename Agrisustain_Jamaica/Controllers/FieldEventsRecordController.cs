using Agrisustain_Jamaica.Data;
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
            var harvestingEvents = _retrieveFromAgrisustain.GetData("HarvestPlanning");
            var pruningEvents = _retrieveFromAgrisustain.GetData("PrunePlanning");
            var sprayingEvents = _retrieveFromAgrisustain.GetData("SprayPlanning");

            var viewModel = new List<FieldEventsRecordModel>();

            //    var viewModel = new List<FieldEventsRecordModel>
            //    {
            //        new FieldEventsRecordModel
            //        {
            //              FertilizationHistory = fertilizationEvents,
            //              IrrigationHistory = irrigationEvents,
            //              SprayingHistory = sprayingEvents,
            //              PlantingHistory = plantingEvents,
            //              PruningHistory = pruningEvents,
            //              HarvestHistory = harvestingEvents
            //        }

            //};

            

            //create a view to display this info
            return View(viewModel);
        }

        //public IActionResult Index()
        //{
        //    return View();
        //}
    }
}
