using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;

namespace WeatherStation.Web.Models
{
    public class WeatherViewModel
    {
        /// <summary>
        /// Represents a single cities weather
        /// </summary>
        public class CityWeatherModel
        {
            /// <summary>
            /// The cities unique identifier
            /// </summary>
            public int CityId { get; set; }

            /// <summary>
            /// The name of hte city this weather is for
            /// </summary>
            public string CityName { get; set; }

            /// <summary>
            /// The latitude of the city this weather is for
            /// </summary>
            public decimal CityLatitude { get; set; }

            /// <summary>
            /// The longitude of the city this weather is for
            /// </summary>
            public decimal CityLongitude { get; set; }

            /// <summary>
            /// Url of the icon for the current weather
            /// </summary>
            public string WeatherIconUrl { get; set; }

            /// <summary>
            /// Text description of the current weather
            /// </summary>
            public string WeatherDescription { get; set; }

            /// <summary>
            /// The temperature of the current weather
            /// </summary>
            public decimal Temperature { get; set; }

            /// <summary>
            /// The days minimum expected temperature
            /// </summary>
            public decimal TemperatureMin { get; set; }

            /// <summary>
            /// The days maximum expected temperature
            /// </summary>
            public decimal TemperatureMax { get; set; }

            /// <summary>
            /// The humididty of the current weather
            /// </summary>
            public decimal Humidity { get; set; }

            /// <summary>
            /// The atmospheric pressure of the current weather
            /// </summary>
            public decimal AtmosphericPressure { get; set; }
        }

        /// <summary>
        /// All the weather for the available cities
        /// </summary>
        public List<CityWeatherModel> AvailableCityWeathers { get; set; }

      
        /// <summary>
        /// The weather for the city we are acurrently viewing
        /// </summary>
        public CityWeatherModel SelectedCityWeather { get; set; }

        /// <summary>
        /// The field available cities are sorted on
        /// </summary>
        public SortField? SortBy { get; set;}

        /// <summary>
        /// THe direction availble cities are sorted
        /// </summary>
        public SortDirection? SortDirection {get; set;}

        
    }
}