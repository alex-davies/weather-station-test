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
    public class OpenWeatherMapService : IOpenWeatherMapService, IDisposable
    {
        /// <summary>
        /// Format string for querying by multiple ids
        /// </summary>
        protected readonly string QueryMultipleByIdFormat;

        /// <summary>
        /// HttpClient our proxy will use, we will keep a single instance to allow reuse
        /// </summary>
        protected readonly HttpClient HttpClient;

        public OpenWeatherMapService(
            string queryMultipleByIdFormat = "http://api.openweathermap.org/data/2.5/group?id={0}&units=metric")
        {
            QueryMultipleByIdFormat = queryMultipleByIdFormat;
            HttpClient = new HttpClient();
        }



        /// <summary>
        /// Gets the weither information for the given set of city ids
        /// </summary>
        /// <param name="cityIds"></param>
        /// <returns></returns>
        public async Task<IEnumerable<WeatherResult>> GetWeather(IEnumerable<int> cityIds)
        {
            var joinedCityIds = string.Join(",", cityIds);
            
            WeatherListResult weatherResultList;
            try
            {
                var result = await HttpClient.GetAsync(string.Format(QueryMultipleByIdFormat, joinedCityIds));
                weatherResultList = await result.Content.ReadAsAsync<WeatherListResult>();
            }
            catch (Exception ex)
            {
                //we will hide other errors that come from our service, we dont want callers
                //to have to worry about catching invalid domains, and bad media type errors
                throw new GeneralServiceException(joinedCityIds);
            }

            if(weatherResultList == null || weatherResultList.List == null)
                throw new GeneralServiceException(joinedCityIds);

            return weatherResultList.List;
        }

        public void Dispose()
        {
            if(HttpClient != null)
                HttpClient.Dispose();
        }
    }
}