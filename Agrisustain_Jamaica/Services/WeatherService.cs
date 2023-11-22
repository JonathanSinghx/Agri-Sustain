﻿using Agrisustain_Jamaica.Controllers;
using Agrisustain_Jamaica.Models;
using Agrisustain_Jamaica.Models.WeatherForecastDataModels;
using Humanizer;
using NuGet.Packaging;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text.Json;

namespace Agrisustain_Jamaica.Services
{
    public class WeatherService
    {
        List<CurrentWeatherConditionModel> currentWeatherCondition = new List<CurrentWeatherConditionModel>();
        List<CurrentMainForecastModel> currentMainForecast = new List<CurrentMainForecastModel>();
        List<CurrentWindModel> currentWind = new List<CurrentWindModel>();
        List<SunriseAndSunsetModel> sunriseAndSunset = new List<SunriseAndSunsetModel>();
        List<WeatherModel> weatherData = new List<WeatherModel>();

        List<HourlyWeatherForecastModel> hourlyWeatherForecast = new List<HourlyWeatherForecastModel>();
        List<HourlyWeatherForecasts> hourlyWeatherForecasts = new List<HourlyWeatherForecasts>();

        List<AirQualityIndexModel> airQualityIndex = new List<AirQualityIndexModel>();
        List<AirQualityIndexData> airQualityIndexData = new List<AirQualityIndexData>();

        public async Task GetCurrentWeatherData(double latitude, double longitude)
        {
            string baseUrl = "https://api.openweathermap.org/";
            string API_Key = "9aa7f0ef9d7b97bd856b7bf109607d29";

            using (var client = new HttpClient())
            {
              //  HttpResponseMessage getRequest = await client.GetAsync("https://localhost:7269/weatherforecast/weatherforecast");
              
                client.BaseAddress = new Uri(baseUrl);
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Clear();

                HttpResponseMessage currentWeatherResponseMessage = await client.GetAsync($"{baseUrl}data/2.5/weather?lat=44.34&lon=10.99&appid={API_Key}&units=metric");
                HttpResponseMessage hourlyWeatherResponseMessage = await client.GetAsync($"{baseUrl}data/2.5/forecast?lat=44.34&lon=10.99&appid={API_Key}");
                HttpResponseMessage AirQualityResponseMessage = await client.GetAsync($"{baseUrl}data/2.5/air_pollution?lat=44.34&lon=10.99&appid={API_Key}");

                if (currentWeatherResponseMessage.IsSuccessStatusCode)
                {
                    using (JsonDocument document = JsonDocument.Parse(await currentWeatherResponseMessage.Content.ReadAsStringAsync()))
                    {
                        JsonElement currentWeatherJsonData = document.RootElement;
                        var currentWeatherJsonDataProps = JsonSerializer.Deserialize<WeatherModel>(currentWeatherJsonData);
                        weatherData.Add(currentWeatherJsonDataProps);

                        JsonElement weatherProp = currentWeatherJsonData.GetProperty("weather")[0];

                        var currentWeatherJson = JsonSerializer.Deserialize<CurrentWeatherConditionModel>(weatherProp);
                        currentWeatherCondition.Add(currentWeatherJson);

                        JsonElement mainProp = currentWeatherJsonData.GetProperty("main");
                      //  JsonElement visibilityProp = currentWeatherJsonData.GetProperty("visibility");
                      //  var visibilityPropJson = JsonSerializer.Deserialize<object>(visibilityProp);
                        var currentMainJson = JsonSerializer.Deserialize<CurrentMainForecastModel>(mainProp);
                        
                        currentMainForecast.Add(currentMainJson);
                      //  currentMainForecast.Add(visibilityPropJson);


                        //wind

                        JsonElement windProp = currentWeatherJsonData.GetProperty("wind");
                        var currentWindJson = JsonSerializer.Deserialize<CurrentWindModel>(windProp);
                        currentWind.Add(currentWindJson);

                        JsonElement sunriseAndSunsetProp = currentWeatherJsonData.GetProperty("sys");
                        var sunriseAndSunsetJson = JsonSerializer.Deserialize<SunriseAndSunsetModel>(sunriseAndSunsetProp);
                        sunriseAndSunset.Add(sunriseAndSunsetJson);

                    }

                }

                if (hourlyWeatherResponseMessage.IsSuccessStatusCode)
                {
                    using (JsonDocument document = JsonDocument.Parse(await hourlyWeatherResponseMessage.Content.ReadAsStringAsync()))
                    {
                        JsonElement hourlyWeatherJsonData = document.RootElement;

                        JsonElement hourlyWeatherProp = hourlyWeatherJsonData.GetProperty("list");

                        foreach( var weatherData in hourlyWeatherProp.EnumerateArray())
                        {
                            JsonElement hourlyWeatherProps = weatherData.GetProperty("weather")[0];
                            var hourlyWeatherForecastsJson = JsonSerializer.Deserialize<HourlyWeatherForecasts>(hourlyWeatherProps);

                            var hourlyWeatherJson = JsonSerializer.Deserialize<HourlyWeatherForecastModel>(weatherData);
                            hourlyWeatherForecast.Add(hourlyWeatherJson);
                            hourlyWeatherForecasts.Add(hourlyWeatherForecastsJson);
                        }

                        //JsonElement hourlyWeatherProps = hourlyWeatherProp.GetProperty("weather");
                       // foreach (var forecasts in hourlyWeatherProps.EnumerateArray())
                       // {
                           // var hourlyWeatherForecastsJson = JsonSerializer.Deserialize<HourlyWeatherForecasts>(forecasts);
                            //hourlyWeatherForecasts.Add(hourlyWeatherForecastsJson);
                       // }

                    }

                }

                if(AirQualityResponseMessage.IsSuccessStatusCode)
                {
                    using (JsonDocument document = JsonDocument.Parse(await AirQualityResponseMessage.Content.ReadAsStringAsync()))
                    {
                        JsonElement airQualityJsonData = document.RootElement;

                        JsonElement airQualityIndexList = airQualityJsonData.GetProperty("list")[0];

                        JsonElement mainProp = airQualityIndexList.GetProperty("main");

                        JsonElement aqi = mainProp.GetProperty("aqi");

                        JsonElement components = airQualityIndexList.GetProperty("components");

                     //   var index = JsonSerializer.Deserialize<AirQualityIndexData>(aqi);
                        var JsonComponents = JsonSerializer.Deserialize<AirQualityIndexModel>(airQualityIndexList);
                     
                       // airQualityIndexData.Add();
                        airQualityIndex.Add(JsonComponents);
                    }

                }

            }
        }

        //public async Task WeatherForecastResult()
        //{
        //  // await GetCurrentWeatherData();
        //}

        public async Task<IEnumerable<CurrentWeatherConditionModel>>GetCurrentWeatherCondition()
        {
           // await GetCurrentWeatherData();

            return currentWeatherCondition;

        }

        public async Task<IEnumerable<CurrentMainForecastModel>>GetCurrentMainForecast()
        {
            return currentMainForecast;
        }

        public async Task<IEnumerable<CurrentWindModel>> GetCurrentWind()
        {
            return currentWind;

        }

        public async Task<IEnumerable<SunriseAndSunsetModel>> GetCurrentSunriseAndSunset()
        {
            return sunriseAndSunset;
        }

        public async Task<IEnumerable<WeatherModel>> GetCurrentWeatherVisibility()
        {
            return weatherData;
        }

        public async Task<IEnumerable<HourlyWeatherForecastModel>> GetHourlyWeatherForecast()
        {
            return hourlyWeatherForecast;
        }

        public async Task<IEnumerable<HourlyWeatherForecasts>> GetHourlyWeatherForecasts()
        {
            return hourlyWeatherForecasts;
        }

        public async Task<IEnumerable<AirQualityIndexData>> GetAirQualityIndex()
        {
            return airQualityIndexData;
        }


    }
}
