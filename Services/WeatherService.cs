using System.Text.Json;
using System.Text.Json.Serialization;
using System.Globalization;
using MiniWeatherApp.Models;
using MiniWeatherApp.Helpers;

namespace MiniWeatherApp.Services
{
    public class WeatherService
    {
        private readonly HttpClient _httpClient;
        private const string BASE_URL = "https://api.open-meteo.com/v1/forecast";

        public WeatherService()
        {
            _httpClient = new HttpClient();
        }

        public async Task<WeatherForecast> GetForecastAsync(City city)
        {
            if (city == null) return null;

            try
            {
                string lat = city.Latitude.ToString(CultureInfo.InvariantCulture);
                string lon = city.Longitude.ToString(CultureInfo.InvariantCulture);

                var url = $"{BASE_URL}?latitude={lat}&longitude={lon}&daily=temperature_2m_max,temperature_2m_min,weathercode,precipitation_sum,windspeed_10m_max&current_weather=true&timezone=Europe/Sarajevo&forecast_days=5";

                var response = await _httpClient.GetStringAsync(url);

                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true,
                    NumberHandling = JsonNumberHandling.AllowReadingFromString
                };

                var forecast = JsonSerializer.Deserialize<WeatherForecast>(response, options);

                if (forecast != null)
                {
                    forecast.CityName = city.Name;
                }

                return forecast;
            }
            catch
            {
                return null;
            }
        }

        public List<DayForecast> ParseDailyForecast(WeatherForecast forecast)
        {
            if (forecast?.Daily?.Time == null) return new List<DayForecast>();

            var dailyForecasts = new List<DayForecast>();

            for (int i = 0; i < forecast.Daily.Time.Count; i++)
            {
                var date = DateTime.Parse(forecast.Daily.Time[i]);
                var dayForecast = new DayForecast
                {
                    Date = date,
                    DayName = date.ToString("dddd", new CultureInfo("bs-BA")),
                    TempMax = Math.Round(forecast.Daily.TemperatureMax[i], 1),
                    TempMin = Math.Round(forecast.Daily.TemperatureMin[i], 1),
                    WeatherCode = forecast.Daily.WeatherCode[i],
                    WeatherDescription = WeatherCodeHelper.GetWeatherDescription(forecast.Daily.WeatherCode[i]),
                    Precipitation = Math.Round(forecast.Daily.PrecipitationSum[i], 1),
                    WindSpeed = Math.Round(forecast.Daily.WindSpeedMax[i], 1),
                    WeatherIcon = WeatherCodeHelper.GetWeatherIcon(forecast.Daily.WeatherCode[i])
                };

                dailyForecasts.Add(dayForecast);
            }

            return dailyForecasts;
        }
    }
}