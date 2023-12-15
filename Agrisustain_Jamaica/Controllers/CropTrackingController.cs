using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models;
using Agrisustain_Jamaica.Models.CropTracking;
using Agrisustain_Jamaica.Models.ViewModels;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class CropTrackingController : Controller
    {
        //private readonly AgriSustainDBContext agriSustainDBContext;
        //AddToAgrisustainDB agrisustainDB = new AddToAgrisustainDB();
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly UpdateAgrisustainDB _updateAgrisustainDB;
        private readonly DeleteFromAgrisustainDB _deleteFromAgrisustainDB;

        //get access to use database injected in services (program.cs file) by defining a constructor
        public CropTrackingController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext, UpdateAgrisustainDB updateAgrisustainDB, DeleteFromAgrisustainDB deleteFromAgrisustainDB)
        {
            //agriSustainDBContext = agrisustainDB;
            _addToAgrisustainDB = agriSustainDBContext;
            _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;
            _updateAgrisustainDB = updateAgrisustainDB;
            _deleteFromAgrisustainDB = deleteFromAgrisustainDB;
        }

        public IActionResult Homepage()
        {
            return View();
        }

        public IActionResult JobPlanning()
        {
            return View();
        }

        //get list of crops stored in the database
        [HttpGet]
        public async Task<IActionResult> Index()
        {

            SavedCropsModel savedCrops = new SavedCropsModel();
          
            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var crops = _retrieveFromAgrisustain.GetData("Crops");
            if (crops.Rows.Count > 0)
            {
                for (int i = 0; i < crops.Rows.Count; i++)
                {
                    CropViewModel newCrop = new CropViewModel();
                    if (Guid.TryParse(crops.Rows[i]["Id"].ToString(), out Guid id))
                    {
                        newCrop.Id = id;
                    }
                    //newCrop.Id = crops.Rows[i]["Id"];
                    newCrop.CropName = crops.Rows[i]["CropName"].ToString();
                    newCrop.Description = crops.Rows[i]["DescriptionName"].ToString();
                    newCrop.DatePlanted = Convert.ToDateTime(crops.Rows[i]["DatePlanted"]);
                    newCrop.HarvestDate = Convert.ToDateTime(crops.Rows[i]["HarvestDate"]);
                    savedCrops.cropsList.Add(newCrop);
                }
            }

            return View(savedCrops);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        //Adding a new Crop - this data will be received on submission of the form
        [HttpPost]
        public async Task<IActionResult> Add(AddCropViewModel addCrop)
        {
            //handle conversion from AddCropViewModel to Crop model
            var crop = new Crop()
            {
                Id = Guid.NewGuid(),
                CropName = addCrop.CropName,
                Description = addCrop.Description,
                DatePlanted = addCrop.DatePlanted,
                HarvestDate = addCrop.HarvestDate
            };

            object[] data = new object[] {
                crop.Id,
                crop.CropName,
                crop.Description,
                crop.DatePlanted,
                crop.HarvestDate
            };

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            DataTable cropTable = _retrieveFromAgrisustain.GetData("Crops");
            cropTable.PrimaryKey = new DataColumn[] { cropTable.Columns["Id"] };
            
            //find primarykey that matches id
            DataRow findCrop = cropTable.Rows.Find(id);

            if(findCrop != null)
            {
                var viewModel = new UpdateCropViewModel();
                
                if (Guid.TryParse(findCrop["Id"].ToString(), out Guid cropId))
                {
                    viewModel.Id = id;
                }

                viewModel.CropName = findCrop["CropName"].ToString();
                viewModel.Description = findCrop["DescriptionName"].ToString();
                viewModel.DatePlanted = Convert.ToDateTime(findCrop["DatePlanted"]);
                viewModel.HarvestDate = Convert.ToDateTime(findCrop["HarvestDate"]);

                return View("Edit", viewModel);
            }

            //redirect to index page if id is not found
            return RedirectToAction("Index");
        }

        //create a post method for the above View action
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateCropViewModel model)
        {
            Dictionary<string, object> fieldData = new Dictionary<string, object>
            {
                { "CropName", model.CropName },
                { "DescriptionName", model.Description },
                { "DatePlanted", model.DatePlanted },
                { "HarvestDate", model.HarvestDate }
            };

            _updateAgrisustainDB.UpdateData("Crops", fieldData, model.Id);
            //change below to return to an error page if not found
            return RedirectToAction("Index");
        }

        //Deleting crop 
        [HttpPost]
        public async Task<IActionResult> Delete(UpdateCropViewModel model)
        {
            _deleteFromAgrisustainDB.DeleteData("Crops", model.Id);
            return RedirectToAction("Index");
        }


        public IActionResult FieldActivity()
        {

            return View();
        }
    }
    }

