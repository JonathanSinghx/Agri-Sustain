using Agrisustain_Jamaica.Models;
using Agrisustain_Jamaica.Services;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Diagnostics;

namespace Agrisustain_Jamaica.Controllers
{
    /// <summary>
    /// CROP TRACKING VIEW MODEL CLASS FOR RETURNING A SINGLE SERVICES OBJECT - START
    /// </summary>
    public class ViewModels
    {
        public object? ListedIrrigationService { get; set; }

        public object? ListedCropCareService { get; set; }

        public object? ListedPlantingGuidesService { get; set; }
    }
    /// <summary>
    /// CROP TRACKING VIEW MODEL CLASS FOR RETURNING A SINGLE SERVICES OBJECT - END
    /// </summary>


    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        
        /// <summary>
        /// SERVICES DATA - FOR DEPENDENCY INJECTION RE CROP INFO BLOG POSTS - START
        /// </summary>
        /// <param name="logger"></param>
        private readonly ILogger<JSONFileIrrigationService>? _irrigationService;

        private readonly ILogger<JSONFileCropCareService>? _cropcareService;

        private readonly ILogger<JSONFilePlantingGuidesService>? _plantingGuidesService;

        /// <summary>
        /// SERVICES DATA - FOR DEPENDENCY INJECTION RE CROP INFO BLOG POSTS - END
        /// </summary>

        public HomeController(ILogger<HomeController> logger, ILogger<JSONFileIrrigationService> IrrigationService,
            ILogger<JSONFilePlantingGuidesService>? plantingGuidesService,
            ILogger<JSONFileCropCareService>? cropcareService)
        {
            _logger = logger;
            _irrigationService = IrrigationService;
            _plantingGuidesService = plantingGuidesService;
            _cropcareService = cropcareService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult Resources()
        {
            return View();
        }

        public IActionResult Contact()
        {
            return View();
        }

        public IActionResult SignUpForm()
        {
            return View();
        }

        public IActionResult PestDiseaseNotifications()
        {
            return View();
        }

        public IActionResult PestDiseaseMitigation()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult SustainableFarming()
        {
            return View();
        }

        public IActionResult Composting()
        {
            return View();
        }

        public IActionResult SustainableFarmingTechniques()
        {
            return View();
        }

        public IActionResult ReducingFoodWaste()
        {
            return View();
        }

        public IActionResult DripIrrigation()
        {
            return View();
        }

        public IActionResult CropRotation()
        {
            return View();
        }
        public IActionResult LogIn()
        {
            return View();
        }
        public IActionResult farmersHub() 
        {
            return View();
        }

        public IActionResult Recipes()
        {
            return View();
        }

        public IActionResult JerkChicken()
        {
            return View();
        }
        public IActionResult agriInfo() 
        {
            return View();
        }
        public IActionResult weatherData() 
        {
            return View();
        }
        public IActionResult Marketplace() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult Confirmation() 
        {
            string name = Request.Form["cust_name"], addr = Request.Form["cust_addr"], em = Request.Form["cust_em"], items_tot = Request.Form["Hidden1"], card_type = Request.Form["card_type"], card_num = Request.Form["card_num"];
            order ord=new order();
            ord.id = 0;
            ord.o_date = DateTime.Now;
            ord.cust_name = name;
            ord.cust_addr = addr;
            ord.cust_em = em;
            ord.data = items_tot;
            ord.card_type = card_type;
            ord.card_num = card_num;
            write_db(ord);
            return View(ord);
        }
        public IActionResult R_sys() 
        {
            return View();
        }
        [HttpPost]
        public IActionResult R_Confirmation() 
        {
            string id = Request.Form["id"], name = Request.Form["name"], r_amt = Request.Form["r_amt"], rev = Request.Form["rev"];
            Item_r rating = new Item_r();
            rating.id = Convert.ToInt16(id);
            rating.name = name;
            rating.r_amt = Convert.ToInt16(r_amt);
            rating.rev = rev;
            rating.dateTime = DateTime.Now;
            write_db(rating);
            return View(rating);
        }
        public void write_db(object obj)
        {
            string constr = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=master;Integrated Security=True;Connect Timeout=30;Encrypt=False;";
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            SqlCommand comm = new SqlCommand(constr, conn);
            comm.Connection = conn;
            if (obj.GetType() == typeof(order))
            {
                order order = (order)obj;
                comm.CommandText = "use agri_sus;insert into [dbo].[order] (o_date,cust_name,cust_addr,cust_em,data,card_type) values (@o_date,@cust_name,@cust_addr,@cust_em,@data,@card_type)";
                comm.Parameters.AddWithValue("@o_date", order.o_date);
                comm.Parameters.AddWithValue("@cust_name", order.cust_name);
                comm.Parameters.AddWithValue("@cust_addr", order.cust_addr);
                comm.Parameters.AddWithValue("@cust_em", order.cust_em);
                comm.Parameters.AddWithValue("@data", order.data);
                comm.Parameters.AddWithValue("@card_type", order.card_type);
            }
            else if (obj.GetType() == typeof(Item_r))
            {
                Item_r item = (Item_r)obj;
                comm.CommandText = "use agri_sus;insert into [dbo].[R_Table] (Id,prod_name,r_amt,rev,dateTime) values (@Id,@prod_name,@r_amt,@rev,@dateTime)";
                comm.Parameters.AddWithValue("@Id", item.id);
                comm.Parameters.AddWithValue("@prod_name", item.name);
                comm.Parameters.AddWithValue("@r_amt", item.r_amt);
                comm.Parameters.AddWithValue("@rev", item.rev);
                comm.Parameters.AddWithValue("@dateTime", item.dateTime);
            }
            else { }
            comm.ExecuteNonQuery();
        }
        public IActionResult UserSelector()
        {
            return View();
        }
        public IActionResult ManageGarden()
        {
            return View();
        }

        public IActionResult StarterGuide()
        {
            return View();
        }

        public IActionResult ShareProduce()
        {
            return View();
        }



        /// CROP TRACKING DATA - START

        public IActionResult CropInfoBlogs()
        {
            //Create a view model to pass multiple data to view since it only accepts 1 parameter

            ViewModels viewModels = new ViewModels
            {
                ListedIrrigationService = _irrigationService,
                ListedPlantingGuidesService = _plantingGuidesService,
                ListedCropCareService = _cropcareService,
            };

            return View(viewModels);

        }

        public IActionResult Irrigation()
        {
            return View("CropInfo/Irrigation");
        }

        public IActionResult CropCare()
        {
            return View("CropInfo/CropCare");
        }

        public IActionResult PlantingGuides()
        {
            return View("CropInfo/PlantingGuides");
        }

        /// CROP TRACKING DATA - END

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}