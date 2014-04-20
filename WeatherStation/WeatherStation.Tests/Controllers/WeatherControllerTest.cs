using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using WeatherStation.Web;
using WeatherStation.Web.Controllers;
using System.Threading.Tasks;
using WeatherStation.Web.Infrastructure;
using WeatherStation.Web.Services.OpenWeatherMap;
using WeatherStation.Web.Services.OpenWeatherMap.Data;
using WeatherStation.Web.Models;
using System.Web.Helpers;

namespace WeatherStation.Tests.Controllers
{
    

    [TestClass]
    public class WeatherControllerTest
    {
        /// Manually created mock for the WeatherController tests. Due to the small size of the project and interfaces it is easier
        /// to create mocks manually than use a mocking framework
        #region Mock Objects

        public class MockSettings : ISettings
        {
            public IEnumerable<int> AvailableCityIds
            {
                get { return new[] { 2, 3, 5 }; }
            }
        }

        public class MockOpenWeatherMapService : IOpenWeatherMapService
        {
            public async Task<IEnumerable<Web.Services.OpenWeatherMap.Data.WeatherResult>> GetWeather(IEnumerable<int> cityIds)
            {
                return new[]{
                new WeatherResult(){
                    Id = 2,
                    Name = "Too Hot",
                    Main = new WeatherResult.WeatherStatistics() {
                        Temp = 40,
                        Temp_Min = 39,
                        Temp_Max = 41
                    },
                    Weather = new[]{new WeatherResult.WeatherForcast(){
                        Description = "Sunny"
                    }},
                },
                new WeatherResult(){
                    Id = 3,
                    Name = "Too Cold",
                    Main = new WeatherResult.WeatherStatistics() {
                        Temp = -10,
                        Temp_Min = -11,
                        Temp_Max = 11
                    },
                    Weather = new[]{new WeatherResult.WeatherForcast(){
                        Description = "Snowing"
                    }},
                },
                new WeatherResult(){
                    Id = 5,
                    Name = "Just Right",
                    Coord = new WeatherResult.Coordinate() { Lat = 42.42M, Lon = - 24.24M},
                    Main = new WeatherResult.WeatherStatistics() {
                        Temp = 15,
                        Temp_Min = 14,
                        Temp_Max = 16,
                        Humidity = 80,
                        Pressure= 1000
                    },
                    Weather = new[]{new WeatherResult.WeatherForcast(){
                        Description = "Overcast"
                    }},
                }
            };
            }
        }

        #endregion

        public WeatherController Controller;

        [TestInitialize]
        public void Setup()
        {
            Controller = new WeatherController(new MockSettings(), new MockOpenWeatherMapService());
        }


        [TestMethod]
        public async Task WhenNoCitySelectedShouldRedirectToFirstCity()
        {
            var result = await Controller.Index();
            var redirectResult = result as RedirectToRouteResult;

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(2, redirectResult.RouteValues["cityId"]);
        }

        [TestMethod]
        public async Task WhenCityNotValidShouldReturn404()
        {
            var result = await Controller.Index(cityId:999);
            var statusResult = result as HttpStatusCodeResult;

            Assert.IsNotNull(statusResult);
            Assert.AreEqual(404, statusResult.StatusCode);
        }

        [TestMethod]
        public async Task WhenSortedByTemperatureAscShouldReturnSortedResult()
        {
            var result = await Controller.Index(cityId:2, sortBy:SortField.Temperature, sortDirection:SortDirection.Ascending) as ViewResult;
            var model = result.Model as WeatherViewModel;

            Assert.IsNotNull(model);
            Assert.AreEqual("Too Cold", model.AvailableCityWeathers[0].CityName);
            Assert.AreEqual("Just Right", model.AvailableCityWeathers[1].CityName);
            Assert.AreEqual("Too Hot", model.AvailableCityWeathers[2].CityName);
        }

        [TestMethod]
        public async Task WhenSortedByTemperatureDescShouldReturnSortedResult()
        {
            var result = await Controller.Index(cityId: 2, sortBy: SortField.Temperature, sortDirection: SortDirection.Descending) as ViewResult;
            var model = result.Model as WeatherViewModel;

            Assert.IsNotNull(model);
            
            Assert.AreEqual("Too Hot", model.AvailableCityWeathers[0].CityName);
            Assert.AreEqual("Just Right", model.AvailableCityWeathers[1].CityName);
            Assert.AreEqual("Too Cold", model.AvailableCityWeathers[2].CityName);
        }


        [TestMethod]
        public async Task WhenSortedByCityAscShouldReturnSortedResult()
        {
            var result = await Controller.Index(cityId: 2, sortBy: SortField.City, sortDirection: SortDirection.Ascending) as ViewResult;
            var model = result.Model as WeatherViewModel;

            Assert.IsNotNull(model);

            Assert.AreEqual("Just Right", model.AvailableCityWeathers[0].CityName);
            Assert.AreEqual("Too Cold", model.AvailableCityWeathers[1].CityName);
            Assert.AreEqual("Too Hot", model.AvailableCityWeathers[2].CityName);
        }

        [TestMethod]
        public async Task WhenSortedByCityDescShouldReturnSortedResult()
        {
            var result = await Controller.Index(cityId: 2, sortBy: SortField.City, sortDirection: SortDirection.Descending) as ViewResult;
            var model = result.Model as WeatherViewModel;

            Assert.IsNotNull(model);

            Assert.AreEqual("Too Hot", model.AvailableCityWeathers[0].CityName);
            Assert.AreEqual("Too Cold", model.AvailableCityWeathers[1].CityName);
            Assert.AreEqual("Just Right", model.AvailableCityWeathers[2].CityName);
           
        }

        [TestMethod]
        public async Task WhenCitySelectedShouldReturnDataInViewModel()
        {
            var result = await Controller.Index(cityId: 5) as ViewResult;
            var model = result.Model as WeatherViewModel;

            Assert.IsNotNull(model);

            Assert.AreEqual("Just Right", model.SelectedCityWeather.CityName);
            Assert.AreEqual(42.42M, model.SelectedCityWeather.CityLatitude);
            Assert.AreEqual(-24.24M, model.SelectedCityWeather.CityLongitude);
            Assert.AreEqual(15, model.SelectedCityWeather.Temperature);
            Assert.AreEqual(16, model.SelectedCityWeather.TemperatureMax);
            Assert.AreEqual(14, model.SelectedCityWeather.TemperatureMin);
            Assert.AreEqual(1000, model.SelectedCityWeather.AtmosphericPressure);
            Assert.AreEqual(80, model.SelectedCityWeather.Humidity);
            Assert.AreEqual("Overcast", model.SelectedCityWeather.WeatherDescription);
        }

       
    }
}
