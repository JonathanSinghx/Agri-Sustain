using Microsoft.AspNetCore.Mvc;
using Agrisustain_Jamaica.Services;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;

using System.Xml.Linq;
using System.Collections;
using System.Net;
using Microsoft.CodeAnalysis;
using System.Text.Json;
using System.Data;
using System.Data.SqlClient;
using Agrisustain_Jamaica.Data;
using Agrisustain_Jamaica.Models.CropPlanning;
using Microsoft.AspNetCore.Http;

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
   // [Route("weatherforecast/[controller]")]
   // [Route("weatherforecast/[controller]")]

    public class WeatherForecastController : Controller
    {
        public double Temperature { get; set; }

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
        private readonly IConfiguration _configuration;
        private readonly AddToAgrisustainDB _addToAgrisustainDB;
        private readonly RetrieveFromAgrisustainDB _retrieveFromAgrisustain;

        public WeatherForecastController(IWeatherService weatherForecast, IConfiguration configuration, AddToAgrisustainDB agriSustainDBContext, RetrieveFromAgrisustainDB retrieveFromAgrisustainDBContext)
        {
            _weatherService = weatherForecast;    
            _configuration = configuration;
            _addToAgrisustainDB = agriSustainDBContext;
            _retrieveFromAgrisustain = retrieveFromAgrisustainDBContext;

        }
        
        //[HttpPost("PostCoordinates")]

        [HttpPost]
        public JsonResult PostCoordinates([FromBody] GeolocationModel data)
        {
            double latitude = data.Latitude;
            double longitude = data.Longitude;

            var geolocation  = new GeolocationModel()
            {
                Latitude = latitude,
                Longitude = longitude
            };


            object[] coords = new object[] {
             latitude,
             longitude
     
            };

            _addToAgrisustainDB.AddData("Coordinates", coords);

          
            return Json(new { message = "Coordinates received successfully" });
        }

    

        
        public async Task<IActionResult> WeatherForecast()
        {
            //var coords = _retrieveFromAgrisustain.GetData("Coordinates");
            //double lat = 0;
            //double lng = 0;

            //GeolocationModel location = new GeolocationModel();

            //if (coords.Rows.Count > 0)
            //{
            //    for (int i = 0; i < coords.Rows.Count; i++)
            //    {
            //        lat = Convert.ToDouble(coords.Rows[i]["Latitude"]);
            //        lng = Convert.ToDouble(coords.Rows[i]["Longitude"]);

            //        location.Latitude = Convert.ToDouble(coords.Rows[i]["Latitude"]);
            //        location.Longitude = Convert.ToDouble(coords.Rows[i]["Longitude"]);
            //    }
            


            await _weatherService.GetWeather(
              17.99702, -76.79358);

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

           



            Temperature = currentForecastData.ElementAt(0).Temperature;
            //ViewData["Temperature"] = Temperature;
            //TempData["Temperature"] = Temperature;
            HttpContext.Session.SetString("Temperature", Temperature.ToString());
            //    //return await Task.Run(() => View(weatherViewModel));

            return View(weatherViewModel);
        }


        //Weather Trigger Events

       // [HttpGet]
        public IActionResult CreateWeatherTrigger()
        {

           return View();
           
        }

        [HttpPost]
        public async Task<IActionResult>CreateWeatherTrigger(WeatherTriggerModel trigger)
        {
            // List<SavedTriggerEventsModel> savedTriggerEventsModel = new List<SavedTriggerEventsModel>();

            WeatherTriggerViewModel weatherTrigger = new WeatherTriggerViewModel
            {
                TriggerName = trigger.TriggerName,
                WeatherCondition = trigger.WeatherCondition,
                ConditionLevel = trigger.ConditionLevel,
                Condition = trigger.Condition,
                Units = trigger.Units,
                Duration = trigger.Duration,
            };

            SqlConnection sqlConnection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus"));
            SqlCommand createTrigger = new SqlCommand("insert into WEATHERTRIGGER values ('" +Guid.NewGuid()+"','"+trigger.TriggerName+"', '"+trigger.WeatherCondition+"','"+trigger.ConditionLevel+"', '"+trigger.Condition+"', '"+trigger.Units+"', '"+trigger.Duration+"', '"+trigger.CreatedAt+"')", sqlConnection);
            
            sqlConnection.Open();
            createTrigger.ExecuteNonQuery();
            sqlConnection.Close();
         
            return PartialView("_FormSubmitModal", weatherTrigger);
        }

        [HttpGet]
        public async Task<IActionResult>SavedWeatherTriggers()
        {
            SavedTriggerEventsModel triggers = new SavedTriggerEventsModel();
          //  List<WeatherTriggerViewModel> weatherTriggers = new List<WeatherTriggerViewModel>();

            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agri_Sus"));
           connection.Open();
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM WEATHERTRIGGER", connection);
            //SqlDataReader reader = sqlCommand.ExecuteReader();
            //while(reader.Read())
            //{
            //    WeatherTriggerViewModel newTrigger = new WeatherTriggerViewModel();
            //    newTrigger.TriggerName = (string)reader["TriggerName"];
            //}
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            dataAdapter.Fill(dataTable);
            if (dataTable.Rows.Count > 0)
            {
                for (int i = 0; i < dataTable.Rows.Count; i++)
                {
                    WeatherTriggerViewModel newTrigger = new WeatherTriggerViewModel();
                    newTrigger.TriggerName = dataTable.Rows[i]["TriggerName"].ToString();
                    newTrigger.WeatherCondition = dataTable.Rows[i]["WeatherCondition"].ToString();
                    newTrigger.ConditionLevel = (int)dataTable.Rows[i]["ConditionLevel"];
                    newTrigger.Condition = dataTable.Rows[i]["Condition"].ToString();
                    newTrigger.Units = dataTable.Rows[i]["Units"].ToString();
                    newTrigger.Duration = (int)dataTable.Rows[i]["Duration"];
                    newTrigger.CreatedAt = Convert.ToDateTime(dataTable.Rows[i]["CreatedAt"]);

                    triggers.triggerEvents.Add(newTrigger);
                   // triggers.AddTriggerEvent(newTrigger);
                }
                connection.Close();
               // triggers
            }

            return View(triggers);
        }

        public IActionResult ClimateSmart()
        {
            return View();
        }

        //[HttpGet("DisplayPopup")]
        //public async Task<IActionResult> DisplayPopup()
        //{
        //    List<WeatherTriggersModel> weatherTriggers = new List<WeatherTriggersModel>();
        //    DataTable dataTable = new DataTable();
        //    SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("Agrisustain_WeatherDatabase"));
        //    SqlCommand sqlCommand = new SqlCommand("SELECT * FROM WEATHERTRIGGER", connection);
        //    SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
        //    dataAdapter.Fill(dataTable);
        //    if(dataTable.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dataTable.Rows.Count; i++)
        //        {
        //            WeatherTriggersModel trigger = new WeatherTriggersModel();
        //            trigger.TriggerName = dataTable.Rows[i]["TriggerName"].ToString();
        //            trigger.WeatherCondition = dataTable.Rows[i]["WeatherCondition"].ToString();
        //            trigger.ConditionLevel = (int)dataTable.Rows[i]["ConditionLevel"];
        //            trigger.Condition = dataTable.Rows[i]["Condition"].ToString();
        //            trigger.Units = dataTable.Rows[i]["Units"].ToString();
        //            trigger.Duration = (int)dataTable.Rows[i]["Duration"];
        //            weatherTriggers.Add(trigger);
        //        }
        //    }
          
        //    return View("CreateWeatherTrigger", weatherTriggers);
        //}
    }

}
