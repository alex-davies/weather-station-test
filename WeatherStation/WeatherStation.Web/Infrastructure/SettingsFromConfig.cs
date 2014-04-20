using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace WeatherStation.Web.Infrastructure
{
    public class SettingsFromConfig : ISettings
    {
        /// <summary>
        /// Returns the city ids that are available
        /// </summary>
        public IEnumerable<int> AvailableCityIds
        {
            get
            {
                var cityIdsString = ConfigurationManager.AppSettings["AvailableCityIds"] ?? "";

                //convert our strings into ints so we they can be processed more easily
                foreach(var cityIdString in cityIdsString.Split(',', ';'))
                {
                    int cityId;
                    if (int.TryParse(cityIdString, out cityId))
                        yield return cityId;
                }
            }
        }
        
    
    }
}