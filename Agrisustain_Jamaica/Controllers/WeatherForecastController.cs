using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;

namespace Agrisustain_Jamaica.Controllers
{
    public class WeatherForecastViewModels
    {
       // public object? WeatherService { get; set; }

        //public object? ListedCropCareService { get; set; }

        //public object? ListedPlantingGuidesService { get; set; }
    }
    public class WeatherForecastController : Controller
    {
       
        //private readonly ILogger<WeatherService>? _weatherService;

        //public WeatherForecastController(ILogger<WeatherService> weatherService)
        //{
        //    _weatherService = weatherService;

        //}
        public IActionResult WeatherForecast()
        {
           // WeatherForecastViewModels weatherForecast = new WeatherForecastViewModels
           // {
               // WeatherService = _weatherService,

                //ListedPlantingGuidesService = _plantingGuidesService,
                //ListedCropCareService = _cropcareService,
          //  };
          //  return View(weatherForecast);
            return View();
        }
    }
}
