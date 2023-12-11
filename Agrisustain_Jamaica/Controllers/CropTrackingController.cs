using Agrisustain_Jamaica.Data;
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
        AddToAgrisustainDB agrisustainDB = new AddToAgrisustainDB();
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        //RetrieveFromAgrisustainDB retrieveFromAgrisustain = new RetrieveFromAgrisustainDB();

            //get access to use dbcontext injected in services (program.cs file)
            public CropTrackingController(AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext)
            {
                //agriSustainDBContext = agrisustainDB;
                _addToAgrisustainDB = agriSustainDBContext;
                _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;
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
          
            //RetrieveFromAgrisustainDB retrieveFromAgrisustainDB = new RetrieveFromAgrisustainDB();
            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

            var crops = _retrieveFromAgrisustain.GetData("Crops");
            if (crops.Rows.Count > 0)
            {
                for (int i = 0; i < crops.Rows.Count; i++)
                {
                    CropViewModel newCrop = new CropViewModel();
                    newCrop.CropName = crops.Rows[i]["CropName"].ToString();
                    newCrop.Description = crops.Rows[i]["Description"].ToString();
                    newCrop.DatePlanted = Convert.ToDateTime(crops.Rows[i]["DatePlanted"]);
                    newCrop.CropStatus = crops.Rows[i]["CropStatus"].ToString();
                    newCrop.HarvestDate = Convert.ToDateTime(crops.Rows[i]["HarvestDate"]);

                    savedCrops.cropsList.Add(newCrop);
                    // triggers.AddTriggerEvent(newTrigger);
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
                    //CropStatus = addCrop.CropStatus,
                    HarvestDate = addCrop.HarvestDate
                };

            object[] data = new object[] { crop };

            // use addCrop to call entity framework dbcontext to save data to the database
            // await agriSustainDBContext.Crops.AddAsync(crop);
            // await agriSustainDBContext.SaveChangesAsync();
            AddToAgrisustainDB add = new AddToAgrisustainDB();
            add.AddData("Crops", data);
            return RedirectToAction("Index");
            }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            
            return View("Index");
        }
            //create a post method for the above View action
            //to be uncommented
           // [HttpPost]
            //public async Task<IActionResult> Edit(UpdateCropViewModel model)
            //{
            //    var crop = await agriSustainDBContext.Crops.FindAsync(model.Id);

            //    if (crop != null)
            //    {
            //        crop.CropName = model.CropName;
            //        crop.Description = model.Description;
            //        crop.DatePlanted = model.DatePlanted;
            //        //CropStatus = addCrop.CropStatus,
            //        crop.HarvestDate = model.HarvestDate;

            //        await agriSustainDBContext.SaveChangesAsync();

            //        return RedirectToAction("Index");

            //    }

            //    //change below to return to an error page if not found
            //    return RedirectToAction("Index");
            //}

            //Deleting crop 
            //to be uncommented
            //[HttpPost]
            //public async Task<IActionResult> Delete(UpdateCropViewModel model)
            //{
            //    var crop = await agriSustainDBContext.Crops.FindAsync(model.Id);

            //    if (crop != null)
            //    {
            //        agriSustainDBContext.Crops.Remove(crop);
            //        await agriSustainDBContext.SaveChangesAsync();

            //        return RedirectToAction("Index");
            //    }

            //    return RedirectToAction("Index");
            //}



            //public IActionResult FieldActivity()
            //{

            //    return View();
            //}
        }
    }

