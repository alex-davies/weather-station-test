using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherStation.Web.Services.OpenWeatherMap.Data
{


    public class WeatherResult
    {
        public class Coordinate
        {
            /// <summary>
            /// Latitude
            /// </summary>
            public decimal Lat { get; set; }

            /// <summary>
            /// Longitude
            /// </summary>
            public decimal Lon { get; set; }
        }

        public class WeatherStatistics
        {
            /// <summary>
            /// Expected tTemperature
            /// </summary>
            public decimal Temp { get; set; }

            /// <summary>
            /// Expected minimum Temperature
            /// </summary>
            public decimal Temp_Min { get; set; }

            /// <summary>
            /// Expected maximum temperature
            /// </summary>
            public decimal Temp_Max { get; set; }

            /// <summary>
            /// Expected pressure
            /// </summary>
            public decimal Pressure { get; set; }

            /// <summary>
            /// Expected humidity
            /// </summary>
            public decimal Humidity { get; set; }
        }

        public class WeatherForcast
        {
            /// <summary>
            /// ID of the weather
            /// </summary>
            public string Id { get; set; }

            /// <summary>
            /// Short description of weather
            /// </summary>
            public string Main { get; set; }

            /// <summary>
            /// Long description of the weather
            /// </summary>
            public string Description { get; set; }

            /// <summary>
            /// name of icon. Full image can be found at http://openweathermap.org/img/w/[icon].png
            /// </summary>
            public string icon { get; set; }
        }

        /// <summary>
        /// Id of the city
        /// </summary>
        public int Id { get; set;}

        /// <summary>
        /// Name of the city
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Coordinates of the city
        /// </summary>
        public Coordinate Coord { get; set; }

        /// <summary>
        /// Status code of the request, uses HTTP status codes
        /// </summary>
        public string Cod { get; set; }

        /// <summary>
        /// Message with the result
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Forcast of the weather
        /// </summary>
        public IEnumerable<WeatherForcast> Weather { get; set; }

        /// <summary>
        /// Statistics for current weather
        /// </summary>
        public WeatherStatistics Main { get; set; }
    }
}