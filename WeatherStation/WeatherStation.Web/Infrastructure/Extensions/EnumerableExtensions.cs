using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using WeatherStation.Web.Models;

namespace WeatherStation.Web.Infrastructure.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Helper method to sort based on a direction
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="enumerable"></param>
        /// <param name="keySelector"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderByDirection<T, TKey>(this IEnumerable<T> enumerable, Func<T, TKey> keySelector, SortDirection direction)
        {
            return direction == SortDirection.Ascending ? enumerable.OrderBy(keySelector) : enumerable.OrderByDescending(keySelector);
        }
    }
}