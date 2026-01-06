using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace MiniWeatherApp.Models
{
    public class DailyWeather
    {
        [JsonPropertyName("time")]
        public List<string> Time { get; set; }

        [JsonPropertyName("temperature_2m_max")]
        public List<double> TemperatureMax { get; set; }

        [JsonPropertyName("temperature_2m_min")]
        public List<double> TemperatureMin { get; set; }

        [JsonPropertyName("weathercode")]
        public List<int> WeatherCode { get; set; }

        [JsonPropertyName("precipitation_sum")]
        public List<double> PrecipitationSum { get; set; }

        [JsonPropertyName("windspeed_10m_max")]
        public List<double> WindSpeedMax { get; set; }
    }
}