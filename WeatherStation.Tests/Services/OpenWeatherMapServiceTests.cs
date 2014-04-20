using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherStation.Web.Services.OpenWeatherMap;
using WeatherStation.Web.Services.OpenWeatherMap.Data;

namespace WeatherStation.Tests.Services
{
    /// <summary>
    /// Tests the OpenWeatherMapService proxy class. These area more integration type tests but still
    /// useful in testing that the proxy is still able to connect and give a result
    /// </summary>
    [TestClass]
    public class OpenWeatherMapServiceTests
    {
        public const int LondonCityId = 2643743;
        public const int NonExistantCity = 9999999;

        protected OpenWeatherMapService Service;

        [TestInitialize]
        public void Setup()
        {
            Service = new OpenWeatherMapService();
        }

        [TestMethod]
        public async Task ShouldReturnResultWhenValidQuery()
        {
            var results = await Service.GetWeather(new [] {LondonCityId});
            var result = results.FirstOrDefault();

            //Do not want to assert much more than the fact we got a result
            //becasue the data changes the test is more to ensure we can get
            //a result rather than validating the reuslts are correct
            Assert.IsNotNull(result);
            Assert.AreEqual("London", result.Name);
            Assert.IsNotNull(result.Weather);
        }

        [TestMethod]
        public async Task ShouldReturnEmptyWhenQueryDoesNotMatch()
        {
            var result = await Service.GetWeather(new [] {NonExistantCity});
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralServiceException))]
        public async Task ShouldThrowGeneralErrorWhenIncorrectEndpoint()
        {
            var badService = new OpenWeatherMapService(queryMultipleByIdFormat: "http://www.google.com?test={0}");
            var result = await badService.GetWeather(new[] { LondonCityId });
        }

        [TestMethod]
        [ExpectedException(typeof(GeneralServiceException))]
        public async Task ShouldThrowGeneralErrorWhenInvalidEndpoint()
        {
            var badService = new OpenWeatherMapService(queryMultipleByIdFormat: "http://not.a.real.domain/i/do/no/exist/{0}");
            var result = await badService.GetWeather(new[] { LondonCityId });
        }
    }
}
