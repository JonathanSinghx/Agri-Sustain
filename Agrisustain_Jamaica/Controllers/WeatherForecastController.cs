using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;

namespace Agrisustain_Jamaica.Controllers
{
   
    public class WeatherForecastController : Controller
    {
        public IActionResult WeatherForecast()
        {
            return View();
        }
    }
}
