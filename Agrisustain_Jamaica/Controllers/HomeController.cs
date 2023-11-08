using Agrisustain_Jamaica.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Agrisustain_Jamaica.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserProfile()
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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}