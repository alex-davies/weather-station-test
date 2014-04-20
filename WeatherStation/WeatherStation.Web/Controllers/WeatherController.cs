using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;
using WeatherStation.Web.Infrastructure;
using WeatherStation.Web.Infrastructure.Extensions;
using WeatherStation.Web.Models;
using WeatherStation.Web.Services.OpenWeatherMap;
using WeatherStation.Web.Services.OpenWeatherMap.Data;

namespace WeatherStation.Web.Controllers
{
    public class WeatherController : Controller
    {
        /// <summary>
        /// Service to retrieve weather data
        /// </summary>
        public readonly IOpenWeatherMapService WeatherService;

        /// <summary>
        /// General site settings
        /// </summary>
        public readonly ISettings Settings;

        public WeatherController(ISettings settings, IOpenWeatherMapService weatherService)
        {
            WeatherService = weatherService;
            Settings = settings;
        }

        /// <summary>
        /// Lists weather for the available city, optionally showing deteails for a specific city
        /// </summary>
        /// <param name="cityId"></param>
        /// <param name="sortBy"></param>
        /// <param name="SortDirection"></param>
        /// <returns></returns>
        [OutputCache(CacheProfile = "WeatherCacheProfile")]
        public async Task<ActionResult> Index(int? cityId = null, SortField? sortBy = null, SortDirection? sortDirection = null)
        {
            //make sure we are showing at least one city, makes the UI look tidier
            if (cityId == null && Settings.AvailableCityIds.Any())
                return RedirectToAction("Index", new { cityId = Settings.AvailableCityIds.First() });

            //Grab the weather results from the service for all the available city ids. This is reasonable
            //as our available city Ids should remain small
            var weatherResults = await WeatherService.GetWeather(Settings.AvailableCityIds);

            //Create the model and set the initial parameters
            var model = new WeatherViewModel();
            model.SortBy = sortBy;
            model.SortDirection = sortDirection;

            //map our items to fit our view model, we will also sort our items based on the sort selection.
            //we are just sorting in memory the service doesnt allow us to sort
            var mappedItems = weatherResults.Select(MapWeatherViewModel);
            switch (sortBy)
            {
                case SortField.City:
                    model.AvailableCityWeathers = mappedItems
                        .OrderByDirection(x => x.CityName, sortDirection ?? SortDirection.Ascending).ToList();
                    break;
                case SortField.Temperature:
                    model.AvailableCityWeathers = mappedItems
                        .OrderByDirection(x => x.Temperature, sortDirection ?? SortDirection.Ascending).ToList();
                    break;
                case SortField.Weather:
                    model.AvailableCityWeathers = mappedItems
                        .OrderByDirection(x => x.WeatherDescription, sortDirection ?? SortDirection.Ascending).ToList();
                    break;
                default:
                    model.AvailableCityWeathers = mappedItems.ToList();
                    break;
            }

            //find our selected city amongst the available ones
            model.SelectedCityWeather = model.AvailableCityWeathers.FirstOrDefault(c => c.CityId == cityId);

            //seems the selected city is not one of the cities we know about throw a 404
            if (cityId != null && model.SelectedCityWeather == null)
                return new HttpStatusCodeResult(404);


            return View(model);
        }


        /// <summary>
        /// Maps a result from the weather service to a view model object
        /// </summary>
        /// <param name="serviceResult"></param>
        /// <returns></returns>
        [NonAction]
        private WeatherViewModel.CityWeatherModel MapWeatherViewModel(WeatherResult serviceResult)
        {
            var output = new WeatherViewModel.CityWeatherModel()
            {
                CityId = serviceResult.Id,
                CityName = serviceResult.Name,
            };

           
            //if our result has coordinates we will map them
            if (serviceResult.Coord != null)
            {
                output.CityLatitude = serviceResult.Coord.Lat;
                output.CityLongitude = serviceResult.Coord.Lon;
            }

            //get some statistics about the temperature
            if (serviceResult.Main != null)
            {
                output.Temperature = serviceResult.Main.Temp;
                output.TemperatureMax = serviceResult.Main.Temp_Max;
                output.TemperatureMin = serviceResult.Main.Temp_Min;
                output.Humidity = serviceResult.Main.Humidity;
                output.AtmosphericPressure = serviceResult.Main.Pressure;
            }

            //the first weather should be the current weather, which we will use to populate todays weather
            var currentWeather = serviceResult.Weather.FirstOrDefault();
            if(currentWeather != null){
                output.WeatherIconUrl = string.Format("http://openweathermap.org/img/w/{0}.png", currentWeather.icon);
                output.WeatherDescription = currentWeather.Description;
            }

            return output;
        }

        
    }
}