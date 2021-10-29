using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using DevOpsApp.Controllers;
using DevOpsApp.Services.Models;
using DevOpsApp.Services.Services;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace DevOpsApp.ServiceTests
{
    public class ServiceTests
    {
        private Mock<IWeatherService> _wearherServiceMock;
        private Mock<ILogger<WeatherForecastController>> _loggerMock;
        private WeatherForecastController _weatherController;
        private Fixture _fixture;
        
        [SetUp]
        public void Setup()
        {
            _wearherServiceMock = new Mock<IWeatherService>();
            _loggerMock = new Mock<ILogger<WeatherForecastController>>();
            _weatherController = new WeatherForecastController(_loggerMock.Object, _wearherServiceMock.Object);
            _fixture = new Fixture();
        }

        [Test]
        public async Task WeatherController_Get_ReturnWeatherForecast()
        {
            // Arrange
            var weatherForecasts = _fixture.CreateMany<WeatherForecast>(3).ToArray();
            _wearherServiceMock.Setup(c => c.GetWeather()).ReturnsAsync(weatherForecasts);
            
            // Act
            var result = await _weatherController.Get();
            
            // Assert
            Assert.NotNull(result);
        }
    }
}