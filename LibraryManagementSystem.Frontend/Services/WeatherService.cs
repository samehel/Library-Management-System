using LibraryManagementSystem.Frontend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.Services
{
    public class WeatherService : ServiceBase
    {
        public WeatherService(HttpClient httpClient) : base(httpClient)
        {
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
        {
            try
            {
                var res = await this._httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("WeatherForecast");
                return res;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
