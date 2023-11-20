using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using System.Xml.Linq;

namespace Agrisustain_Jamaica.Controllers
{
    [ApiController]
    [Route("weatherforecast/[controller]")]

    public class WeatherForecastController : Controller
    {
        //Location geolocation = new Location();
       
        [HttpPost]
        public IActionResult WeatherForecast([FromBody] GeolocationModel locationData)
        {
            //Request.EnableBuffering();
            //using var reader = new StreamReader(Request.Body);
            //var body = reader.ReadToEndAsync();
            //// Do something with the body
            //Request.Body.Position = 0;
            //return Ok();

            double latitude = locationData.Latitude;
            double longitude = locationData.Longitude;

            //  geolocation.Latitude = latitude;
            // geolocation.Longitude = longitude;

           // return Json(latitude);
           GeolocationModel geolocationModel = new GeolocationModel();
            geolocationModel.Latitude = latitude;
            geolocationModel.Longitude = longitude;
           return Ok();
            //geolocation.Longitude = longitude;
            //geolocation.Latitude = latitude;
           // return View(viewModel);
            //return View(geolocation.GetLocation());
        }

        public IActionResult WeatherForecast()
        {
            return View();
        }
    }

   
   
}
