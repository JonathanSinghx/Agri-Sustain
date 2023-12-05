using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropTracking;
using Agrisustain_Jamaica.Models.ViewModels;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Agrisustain_Jamaica.Controllers
{
    public class CropsController : Controller
    {
        //private readonly AgriSustainDBContext agriSustainDBContext;
        AddToAgrisustainDB agrisustainDB = new AddToAgrisustainDB();


            //get access to use dbcontext injected in services (program.cs file)
            public CropsController(AddToAgrisustainDB agriSustainDBContext)
            {
                agriSustainDBContext = agrisustainDB;
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
            RetrieveFromAgrisustainDB retrieveFromAgrisustainDB = new RetrieveFromAgrisustainDB();
            //use agrisustainDbcontext to talk to Crops folder, which is a property of this context
            //var crops = await agriSustainDBContext.Crops.ToListAsync();
            //create a view to display this info

           var crops = retrieveFromAgrisustainDB.GetData("Crops");
                return View(crops);
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
            // var crop = await agriSustainDBContext.Crops.FirstOrDefaultAsync(x => x.Id == id);
            RetrieveFromAgrisustainDB retrieveFromAgrisustainDB = new RetrieveFromAgrisustainDB();

            var crops = retrieveFromAgrisustainDB.GetData("Crops");
            var crop = "";

            if (crops.Rows.Count > 0)
            {
                for (int i = 0; i < crops.Rows.Count; i++)
                {

                    //if (crops.Rows[i]["Id"])
                    //{

                    //}
                    //WeatherTriggerViewModel newTrigger = new WeatherTriggerViewModel();
                    //newTrigger.TriggerName = crops.Rows[i]["TriggerName"].ToString();
                    //newTrigger.WeatherCondition = dataTable.Rows[i]["WeatherCondition"].ToString();
                    //newTrigger.ConditionLevel = (int)dataTable.Rows[i]["ConditionLevel"];
                    //newTrigger.Condition = dataTable.Rows[i]["Condition"].ToString();
                    //newTrigger.Units = dataTable.Rows[i]["Units"].ToString();
                    //newTrigger.Duration = (int)dataTable.Rows[i]["Duration"];
                    //newTrigger.CreatedAt = Convert.ToDateTime(dataTable.Rows[i]["CreatedAt"]);

                    //triggers.triggerEvents.Add(newTrigger);
                    // triggers.AddTriggerEvent(newTrigger);
                }



                //if (crop != null)
                //{
                //    var viewModel = new UpdateCropViewModel()
                //    {
                //        Id = crop.Id,
                //        CropName = crop.CropName,
                //        Description = crop.Description,
                //        DatePlanted = crop.DatePlanted,
                //        //CropStatus = addCrop.CropStatus,
                //        HarvestDate = crop.HarvestDate
                //    };

                //    return await Task.Run(() => View("Edit", viewModel));
                //}

                return RedirectToAction("Index");
            }
            //to be deleted
            return View(crop);
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

