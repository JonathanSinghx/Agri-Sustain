using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;

using System.Xml.Linq;
using System.Collections;

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

    [ApiController]
    [Route("weatherforecast/[controller]")]

    public class WeatherForecastController : Controller
    {
      List<Double> coords = new List<Double>();

        public double Latitude {get; set;}
        public double Longitude { get; set;}

        private readonly IWeatherService _weatherService;
        
        public WeatherForecastController(IWeatherService weatherForecast)
        {
            _weatherService = weatherForecast; 
        }
        //Location geolocation = new Location();

        [HttpPost]
        public async Task<IActionResult> WeatherForecast([FromBody] GeolocationModel locationData)
        {
            double latitude = locationData.Latitude;
            double longitude = locationData.Longitude;

            Latitude = latitude;
            Longitude = longitude;
            
            coords.Add(latitude);
            coords.Add(longitude);

           // await _weatherService.GetWeather(Latitude, Longitude);
            return Ok(coords);
          
        }

        public async Task<IActionResult> WeatherForecast()
        {
            await _weatherService.GetWeather(Latitude, Longitude);

            var currentWeatherData = _weatherService.GetCurrentWeatherCondition();
            var currentForecastData = _weatherService.GetCurrentMainForecast();
            var currentWindForecastData = _weatherService.GetCurrentWind();
            var currentSunriseAndSunsetData = _weatherService.GetCurrentSunriseAndSunset();
            var visibilityAndTimeData = _weatherService.GetCurrentWeatherVisibility();
            var currentHourlyForecastData = _weatherService.GetHourlyWeatherForecast();
            var fiveDaysHourlyForecastsData = _weatherService.GetHourlyWeatherForecasts();

            Task.WhenAll(currentWeatherData, 
                        currentForecastData, 
                        currentWindForecastData, 
                        currentSunriseAndSunsetData, 
                        visibilityAndTimeData, 
                        currentHourlyForecastData, 
                        fiveDaysHourlyForecastsData);

            var currentWeatherListItems = await currentWeatherData;
            var currentForecastListItems = await currentForecastData;
            var currentWindListItems = await currentWindForecastData;
            var currentSunriseAndSunsetListItems = await currentSunriseAndSunsetData;
            var currentVisibilityAndTimeListItems = await visibilityAndTimeData;
            var currentHourlyForecastListItems = await currentHourlyForecastData;
            var fiveDaysHourlyForecastListItems = await fiveDaysHourlyForecastsData;
            // var current =  _currentWeatherCondition.GetCurrentWeatherCondition().Result;
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

            return await Task.Run(() => View(weatherViewModel));

            //return View(weatherViewModel);
        }
    }

   
   
}
