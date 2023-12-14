using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class PrunePlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public PrunePlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
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
            SavedPruningEventsModel savedPruningEvents = new SavedPruningEventsModel();

            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var pruningEvents = _retrieveFromAgrisustain.GetData("PrunePlanning");
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

                    savedPruningEvents.eventsList.Add(events);
                }

            }

            return View(savedPruningEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPrunningEventModel pruneEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newPruningEvent = new PruningPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = pruneEvent.StartDate,
                EndDate = pruneEvent.EndDate,
                Reason = pruneEvent.Reason,
                GrowthStage = pruneEvent.GrowthStage,
                TargetCrops = pruneEvent.TargetCrops,
            };

            object[] data = new object[] {
                newPruningEvent.Id,
                newPruningEvent.StartDate,
                newPruningEvent.EndDate,
                newPruningEvent.Reason,
                newPruningEvent.GrowthStage,
                newPruningEvent.TargetCrops,
            };

            _addToAgrisustainDB.AddData("PrunePlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("PrunePlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdatePrunningEventModel();

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

                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePrunningEventModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "Reason", model.Reason },
                { "GrowthStage", model.GrowthStage },
                { "TargetCrops", model.TargetCrops },
            };

            _updateAgrisustainDB.UpdateData("PrunePlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePrunningEventModel model)
        {

            _deleteFromAgrisustainDB.DeleteData("PrunePlanning", model.Id);
            return RedirectToAction("Index");

        }
    }
}
