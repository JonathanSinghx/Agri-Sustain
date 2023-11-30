using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;

using System.Xml.Linq;
using System.Collections;
using System.Net;
using Microsoft.CodeAnalysis;
using System.Text.Json;

namespace Agrisustain_Jamaica.Controllers
{
    public class WeatherViewModel
    {
        public IEnumerable<CurrentWeatherConditionModel> CurrentWeatherConditionModel { get; set; }
        public IEnumerable<CurrentMainForecastModel> CurrentMainForecastModel { get; set; }
        public IEnumerable<CurrentWindModel> CurrentWindModel { get; set; }
        public IEnumerable<SunriseAndSunsetModel> SunriseAndSunsetModel { get; set; }
        public IEnumerable<WeatherModel> WeatherDataModel { get; set; }
        public IEnumerable<HourlyWeatherForecastModel> HourlyWeatherForecastModel { get; set; }
        public IEnumerable<HourlyWeatherForecasts> HourlyWeatherForecastsModel { get; set; }
        //public IEnumerable<AirQualityIndexModel> airQualityIndexModel { get; set; }
        //public IEnumerable<AirQualityIndexData> airQualityIndexDataModel { get; set; }
        // Add properties for other weather-related data as needed
        // ...
    }

  
   // [ApiController]
    [Route("weatherforecast/[controller]")]
   // [Route("weatherforecast/[controller]")]

    public class WeatherForecastController : Controller
    {
       
        // List<object> coords = new List<object>();
        //GeolocationModel location = new GeolocationModel();
        //public double Lat { get; set; } = 18.0367;
        //public double Lng { get; set; } = -77.4859;

        public double Lat { get; set; }
        public double Lng { get; set; }
        public IEnumerable<CurrentWeatherConditionModel> CurrentWeatherListItems { get; set; }
        public IEnumerable<CurrentMainForecastModel> CurrentForecastListItems { get; set; }
        public IEnumerable<CurrentWindModel> CurrentWindListItems { get; set; }
        public IEnumerable<SunriseAndSunsetModel> CurrentSunriseAndSunsetListItems { get; set; }
        public IEnumerable<WeatherModel> CurrentVisibilityAndTimeListItems { get; set; }
        public IEnumerable<HourlyWeatherForecastModel> CurrentHourlyForecastListItems { get; set; }
        public IEnumerable<HourlyWeatherForecasts> FiveDaysHourlyForecastListItems { get; set; }

        // public object getData = new object();

        private readonly IWeatherService _weatherService;
        

        public WeatherForecastController(IWeatherService weatherForecast)
        {
            _weatherService = weatherForecast;       
          
        }

        //[HttpGet]
        //public IActionResult WeatherForecast()
        //{
        //    return View();
        //}

        //[HttpPost("PostCoordinates")]
        
        [HttpPost]
        public JsonResult PostCoordinates(GeolocationModel data)
        {
            double latitude = data.Latitude;
            double longitude = data.Longitude;

            GeolocationModel geolocation = new GeolocationModel()
            {
                Latitude = latitude,
                Longitude = longitude
            };

            // _weatherService.GetCoordinates(latitude, longitude);
            // await _weatherService.GetWeather(latitude, longitude);
             _weatherService.GetWeather(
             latitude, longitude);

            var currentWeatherData =  _weatherService.GetCurrentWeatherCondition();
            //var currentForecastData = await _weatherService.GetCurrentMainForecast();
            //var currentWindForecastData = await _weatherService.GetCurrentWind();
            //var currentSunriseAndSunsetData = await _weatherService.GetCurrentSunriseAndSunset();
            //var visibilityAndTimeData = await _weatherService.GetCurrentWeatherVisibility();
            //var currentHourlyForecastData = await _weatherService.GetHourlyWeatherForecast();
            //var fiveDaysHourlyForecastsData = await _weatherService.GetHourlyWeatherForecasts();

            // CurrentWeatherListItems = currentWeatherData;
            // CurrentForecastListItems = currentForecastData;
            // CurrentWindListItems = currentWindForecastData;
            // CurrentSunriseAndSunsetListItems = currentSunriseAndSunsetData;
            // CurrentVisibilityAndTimeListItems = visibilityAndTimeData;
            // CurrentHourlyForecastListItems = currentHourlyForecastData;
            // FiveDaysHourlyForecastListItems = fiveDaysHourlyForecastsData;

            //var weatherViewModel = new WeatherViewModel
            //{
            //    CurrentWeatherConditionModel = CurrentWeatherListItems,
            //    CurrentMainForecastModel = CurrentForecastListItems,
            //    CurrentWindModel = CurrentWindListItems,
            //    SunriseAndSunsetModel = CurrentSunriseAndSunsetListItems,
            //    WeatherDataModel = CurrentVisibilityAndTimeListItems,
            //    HourlyWeatherForecastModel = CurrentHourlyForecastListItems,
            //    HourlyWeatherForecastsModel = FiveDaysHourlyForecastListItems

            //};

            //return View("weatherforecast/weatherforecast", weatherViewModel);
            //HttpContext.Items["GeolocationData"] = geolocation;

            //HttpContext.Items["GeolocationData"] = geolocation;

            // return View(weatherViewModel);
            return Json(currentWeatherData);

        }


        public async Task<IActionResult> WeatherForecast()
        {

            //    // GeolocationModel data = Lat;
            //    //CoordinatesModel coordinatesModel = new CoordinatesModel();
            //    //double res1 = coordinatesModel.AccessGeolocationData()
            await _weatherService.GetWeather(
                Lat, Lng);

            var currentWeatherData = await _weatherService.GetCurrentWeatherCondition();
            var currentForecastData = await _weatherService.GetCurrentMainForecast();
            var currentWindForecastData = await _weatherService.GetCurrentWind();
            var currentSunriseAndSunsetData = await _weatherService.GetCurrentSunriseAndSunset();
            var visibilityAndTimeData = await _weatherService.GetCurrentWeatherVisibility();
            var currentHourlyForecastData = await _weatherService.GetHourlyWeatherForecast();
            var fiveDaysHourlyForecastsData = await _weatherService.GetHourlyWeatherForecasts();

            //    //Task.WhenAll(currentWeatherData, 
            //    //            currentForecastData, 
            //    //            currentWindForecastData, 
            //    //            currentSunriseAndSunsetData, 
            //    //            visibilityAndTimeData, 
            //    //            currentHourlyForecastData, 
            //    //            fiveDaysHourlyForecastsData);

            var currentWeatherListItems = currentWeatherData;
            var currentForecastListItems = currentForecastData;
            var currentWindListItems = currentWindForecastData;
            var currentSunriseAndSunsetListItems = currentSunriseAndSunsetData;
            var currentVisibilityAndTimeListItems = visibilityAndTimeData;
            var currentHourlyForecastListItems = currentHourlyForecastData;
            var fiveDaysHourlyForecastListItems = fiveDaysHourlyForecastsData;

            var weatherViewModel = new WeatherViewModel
            {
                CurrentWeatherConditionModel = currentWeatherListItems,
                CurrentMainForecastModel = currentForecastListItems,
                CurrentWindModel = currentWindListItems,
                SunriseAndSunsetModel = currentSunriseAndSunsetListItems,
                WeatherDataModel = currentVisibilityAndTimeListItems,
                HourlyWeatherForecastModel = currentHourlyForecastListItems,
                HourlyWeatherForecastsModel = fiveDaysHourlyForecastListItems

            };

            //    //return await Task.Run(() => View(weatherViewModel));

            return View(weatherViewModel);
        }
    }



}
