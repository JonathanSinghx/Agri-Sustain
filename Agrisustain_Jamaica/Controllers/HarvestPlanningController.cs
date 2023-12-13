using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class HarvestPlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public HarvestPlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
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
            SavedHarvestEventsModel savedHarvestEvents = new SavedHarvestEventsModel();

            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var harvestEvents = _retrieveFromAgrisustain.GetData("HarvestPlanning");
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
                   
                    savedHarvestEvents.eventsList.Add(events);
                }
            }
            
            return View(savedHarvestEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddHarvestEventModel harvestEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newHarvestEvent = new HarvestPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = harvestEvent.StartDate,
                EndDate = harvestEvent.EndDate,
                CropStatus = harvestEvent.CropStatus,
                TargetCrops = harvestEvent.TargetCrops,
            };

            object[] data = new object[] {
                newHarvestEvent.Id,
                newHarvestEvent.StartDate,
                newHarvestEvent.EndDate,
                newHarvestEvent.CropStatus,
                newHarvestEvent.TargetCrops,
            };

            _addToAgrisustainDB.AddData("HarvestPlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("HarvestPlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdateHarvestEventModel();

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

                viewModel.CropStatus = findEvent["CropStatus"].ToString();
                viewModel.TargetCrops = findEvent["TargetCrops"].ToString();
               
                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateHarvestEventModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "CropStatus", model.CropStatus },
                {"TargetCrops", model.TargetCrops},
            };

            _updateAgrisustainDB.UpdateData("HarvestPlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateHarvestEventModel model)
        {

            _deleteFromAgrisustainDB.DeleteData("HarvestPlanning", model.Id);
            return RedirectToAction("Index");

        }

    }
}
