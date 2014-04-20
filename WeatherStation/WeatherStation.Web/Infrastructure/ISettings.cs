using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherStation.Web.Infrastructure
{
    public interface ISettings
    {
          /// <summary>
        /// Returns the city ids that are available
        /// </summary>
        IEnumerable<int> AvailableCityIds { get;}           
    }
}
