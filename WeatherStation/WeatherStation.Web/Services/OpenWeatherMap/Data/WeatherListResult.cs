using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherStation.Web.Services.OpenWeatherMap.Data
{
    public class WeatherListResult
    {
        /// <summary>
        /// All the weather results returned from the query
        /// </summary>
        public IEnumerable<WeatherResult> List { get; set; }
    }
}