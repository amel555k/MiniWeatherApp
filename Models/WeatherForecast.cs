using System.Text.Json.Serialization;
using MiniWeatherApp.Models;

namespace MiniWeatherApp.Models
{
    
    public class WeatherForecast
    {
        [JsonPropertyName("latitude")]
        public double Latitude {get;set;}

        [JsonPropertyName("longitude")]
        public double Longitude {get;set;}

        [JsonPropertyName("timezone")]
        public string Timezone {get;set;}

        [JsonPropertyName("current_weather")]
        public CurrentWeather CurrentWeather {get;set;}

        [JsonPropertyName("daily")]
        public DailyWeather Daily{get;set;}

        public string CityName {get;set;}
    }

}

