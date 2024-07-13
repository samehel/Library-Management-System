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
    public class WeatherService
    {
        private readonly HttpClient _httpClient;

        public WeatherService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
            this._httpClient.BaseAddress = new Uri("http://localhost:5109");
        }

        public async Task<IEnumerable<WeatherForecast>> GetWeatherForecastsAsync()
        {
            try
            {
                return await this._httpClient.GetFromJsonAsync<IEnumerable<WeatherForecast>>("WeatherForecast");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
