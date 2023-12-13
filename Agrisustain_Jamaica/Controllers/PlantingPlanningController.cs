using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Agrisustain_Jamaica.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class PlantingPlanningController : Controller
    {
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public PlantingPlanningController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
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
            SavedPlantingEventsModel savedPlantingEvents = new SavedPlantingEventsModel();

            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var plantingEvents = _retrieveFromAgrisustain.GetData("PlantingPlanning");
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
                    events.Crop= plantingEvents.Rows[i]["Crop"].ToString();
                    events.QuantityDescription = plantingEvents.Rows[i]["QuantityDescription"].ToString();

                    savedPlantingEvents.eventsList.Add(events);
                }


            }

            return View(savedPlantingEvents);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddPlantingEventModel plantingEvent)
        {
            //handle conversion from AddCropViewModel to Crop model
            var newPlantingEvent = new PlantingPlanning()
            {
                Id = Guid.NewGuid(),
                StartDate = plantingEvent.StartDate,
                EndDate = plantingEvent.EndDate,
                Crop = plantingEvent.Crop,
                QuantityDescription = plantingEvent.QuantityDescription,
            };

            object[] data = new object[] {
                newPlantingEvent.Id,
                newPlantingEvent.StartDate,
                newPlantingEvent.EndDate,
                newPlantingEvent.Crop,
                newPlantingEvent.QuantityDescription,
            };

            _addToAgrisustainDB.AddData("PlantingPlanning", data);

            return RedirectToAction("Index");

        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable eventsTable = _retrieveFromAgrisustain.GetData("PlantingPlanning");
            eventsTable.PrimaryKey = new DataColumn[] { eventsTable.Columns["Id"] };

            //find primarykey that matches id
            DataRow findEvent = eventsTable.Rows.Find(id);

            if (findEvent != null)
            {
                var viewModel = new UpdatePlantingEventModel();

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

                viewModel.Crop = findEvent["Crop"].ToString();
                viewModel.QuantityDescription = findEvent["QuantityDescription"].ToString();

                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePlantingEventModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "StartDate", model.StartDate },
                { "EndDate", model.EndDate },
                { "Crop", model.Crop },
                {"QuantityDescription", model.QuantityDescription},
            };

            _updateAgrisustainDB.UpdateData("PlantingPlanning", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting event 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdatePlantingEventModel model)
        {

            _deleteFromAgrisustainDB.DeleteData("PlantingPlanning", model.Id);
            return RedirectToAction("Index");

        }
    }
}

