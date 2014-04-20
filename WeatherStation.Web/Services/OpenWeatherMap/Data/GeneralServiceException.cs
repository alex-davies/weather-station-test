using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WeatherStation.Web.Services.OpenWeatherMap.Data
{
    /// <summary>
    /// Exception if there are any issues with contacting the OpenWeatherMap service
    /// </summary>
    public class GeneralServiceException : Exception
    {
        public GeneralServiceException(string cityName, Exception innerException=null)
            : base(string.Format("Error retreiving weather data for city '{0}'", cityName), innerException)
        {

        }
    }
}