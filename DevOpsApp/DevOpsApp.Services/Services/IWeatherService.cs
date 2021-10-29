using System.Collections.Generic;
using System.Threading.Tasks;
using DevOpsApp.Services.Models;

namespace DevOpsApp.Services.Services
{
    public interface IWeatherService
    {
        public Task<IEnumerable<WeatherForecast>> GetWeather();
    }
}