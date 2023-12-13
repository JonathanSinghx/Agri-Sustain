using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.CropTracking;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class FertilizationPlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public FertilizationPlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
        {
            //agriSustainDBContext = agrisustainDB;
            _addToAgrisustainDB = agriSustainDBContext;
            _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;
            _updateAgrisustainDB = updateAgrisustainDB;
            _deleteFromAgrisustainDB = deleteFromAgrisustainDB;
        }

        //get list of fertilizeEvents stored in the database
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SavedFertilizationEventsModel savedFertilizationEvents = new SavedFertilizationEventsModel();

             //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
             //var crops = await agriSustainDBContext.Crops.ToListAsync();
             //create a view to display this info

             var fertilizationEvents = _retrieveFromAgrisustain.GetData("FertilizationPlanning");
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
                   
                    savedFertilizationEvents.eventsList.Add(events);
                }
            }

            return View(savedFertilizationEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddFertilizationEventModel fertilizationEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newFertilizationEvent = new FertilizationPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = fertilizationEvent.StartDate,
                EndDate = fertilizationEvent.EndDate,
                Reason = fertilizationEvent.Reason,
                GrowthStage = fertilizationEvent.GrowthStage,
                TargetCrops = fertilizationEvent.TargetCrops,
                TargetPests = fertilizationEvent.TargetPests,
                ApplicationPattern = fertilizationEvent.ApplicationPattern
            };

            object[] data = new object[] {
                newFertilizationEvent.Id,
                newFertilizationEvent.StartDate,
                newFertilizationEvent.EndDate,
                newFertilizationEvent.Reason,
                newFertilizationEvent.GrowthStage,
                newFertilizationEvent.TargetCrops,
                newFertilizationEvent.TargetPests,
                newFertilizationEvent.ApplicationPattern,
            };

            _addToAgrisustainDB.AddData("FertilizationPlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("FertilizationPlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdateFertilizationEventModel();

                if (Guid.TryParse(findEvent["Id"].ToString(), out Guid eventId))
                {
                    viewModel.Id = id;
                }

                if (DateTime.TryParse(findEvent["StartDate"].ToString(), out DateTime startDate))
                {
                    // Conversion successful
                    viewModel.StartDate = startDate;
                }

                if (DateTime.TryParse(findEvent["EndDate"].ToString(), out DateTime endDate))
                {
                    // Conversion successful
                    viewModel.EndDate = endDate;
                }

                viewModel.Reason = findEvent["Reason"].ToString();
                viewModel.GrowthStage = findEvent["GrowthStage"].ToString();
                viewModel.TargetCrops = findEvent["TargetCrops"].ToString();
                viewModel.TargetPests = findEvent["TargetPests"].ToString();
                viewModel.ApplicationPattern = findEvent["ApplicationPattern"].ToString();

                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateFertilizationEventModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "Reason", model.Reason },
                { "GrowthStage", model.GrowthStage },
                {"TargetCrops", model.TargetCrops},
                {"TargetPests", model.TargetPests },
                {"ApplicationPattern", model.ApplicationPattern }
            };

            _updateAgrisustainDB.UpdateData("FertilizationPlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateFertilizationEventModel model)
        {

            _deleteFromAgrisustainDB.DeleteData("FertilizationPlanning", model.Id);
            return RedirectToAction("Index");

        }

        //All fieldActivities

        //public IActionResult FieldActivities()
        //{
           
        //    return View();
        //}


    }
}
