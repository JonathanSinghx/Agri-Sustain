using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class SprayPlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public SprayPlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
        {
            //agriSustainDBContext = agrisustainDB;
            _addToAgrisustainDB = agriSustainDBContext;
            _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;
            _updateAgrisustainDB = updateAgrisustainDB;
            _deleteFromAgrisustainDB = deleteFromAgrisustainDB;
        }

        //get list of spray Events stored in the database
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            SavedSprayEventsModel savedSprayEvents = new SavedSprayEventsModel();

            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var sprayEvents = _retrieveFromAgrisustain.GetData("SprayPlanning");
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

                    savedSprayEvents.eventsList.Add(events);
                }
            }

            return View(savedSprayEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddSprayEventModel sprayEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newSprayEvent = new SprayPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = sprayEvent.StartDate,
                EndDate = sprayEvent.EndDate,
                Reason = sprayEvent.Reason,
                GrowthStage = sprayEvent.GrowthStage,
                TargetCrops = sprayEvent.TargetCrops,
                TargetPests = sprayEvent.TargetPests,
                ApplicationPattern = sprayEvent.ApplicationPattern
            };

            object[] data = new object[] {
                newSprayEvent.Id,
                newSprayEvent.StartDate,
                newSprayEvent.EndDate,
                newSprayEvent.Reason,
                newSprayEvent.GrowthStage,
                newSprayEvent.TargetCrops,
                newSprayEvent.TargetPests,
                newSprayEvent.ApplicationPattern,
            };

            _addToAgrisustainDB.AddData("SprayPlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("SprayPlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdateSprayEventModel();

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
        public async Task<IActionResult> Edit(UpdateSprayEventModel model)
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

            _updateAgrisustainDB.UpdateData("SprayPlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateSprayEventModel model)
        {

            _deleteFromAgrisustainDB.DeleteData("SprayPlanning", model.Id);
            return RedirectToAction("Index");

        }
    }
}
