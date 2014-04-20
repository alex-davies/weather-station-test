using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using WeatherStation.Web.Services.OpenWeatherMap.Data;

namespace WeatherStation.Web.Services.OpenWeatherMap
{
    public interface IOpenWeatherMapService
    {
        /// <summary>
        /// Gets the weither information for the given set of city ids
        /// </summary>
        /// <param name="cityIds"></param>
        /// <returns></returns>
        Task<IEnumerable<WeatherResult>> GetWeather(IEnumerable<int> cityIds);
       
    }
}