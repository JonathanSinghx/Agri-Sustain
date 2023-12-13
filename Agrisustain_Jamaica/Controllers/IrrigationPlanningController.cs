using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class IrrigationPlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public IrrigationPlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
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
            SavedIrrigationEventsModel savedIrrigationEvents = new SavedIrrigationEventsModel();

            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var irrigationEvents = _retrieveFromAgrisustain.GetData("IrrigationPlanning");
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
                    
                    savedIrrigationEvents.eventsList.Add(events);
                }
            }

            return View(savedIrrigationEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddIrrigationEventModel irrigationEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newIrrigationEvent = new IrrigationPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = irrigationEvent.StartDate,
                EndDate = irrigationEvent.EndDate,
                Reason = irrigationEvent.Reason,
                GrowthStage = irrigationEvent.GrowthStage,
                TargetCrops = irrigationEvent.TargetCrops,
                ApplicationPattern = irrigationEvent.ApplicationPattern
            };

            object[] data = new object[] {
                newIrrigationEvent.Id,
                newIrrigationEvent.StartDate,
                newIrrigationEvent.EndDate,
                newIrrigationEvent.Reason,
                newIrrigationEvent.GrowthStage,
                newIrrigationEvent.TargetCrops,
                newIrrigationEvent.ApplicationPattern
            };

            _addToAgrisustainDB.AddData("IrrigationPlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("IrrigationPlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdateIrrigationEventModel();

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
                viewModel.ApplicationPattern = findEvent["ApplicationPattern"].ToString();

                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateIrrigationEventModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "Reason", model.Reason },
                { "GrowthStage", model.GrowthStage },
                {"TargetCrops", model.TargetCrops},
                { "ApplicationPattern", model.ApplicationPattern },
            };

            _updateAgrisustainDB.UpdateData("IrrigationPlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateIrrigationEventModel model)
        {
            _deleteFromAgrisustainDB.DeleteData("IrrigationPlanning", model.Id);
            return RedirectToAction("Index");
        }
    }
}
