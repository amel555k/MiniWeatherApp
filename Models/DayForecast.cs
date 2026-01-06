namespace WeatherApp.Models
{
    public class DayForecast
    {
        public DateTime Date { get; set; }
        public string DayName { get; set; }
        public double TempMax { get; set; }
        public double TempMin { get; set; }
        public int WeatherCode { get; set; }
        public string WeatherDescription { get; set; }
        public double Precipitation { get; set; }
        public double WindSpeed { get; set; }
        public string WeatherIcon { get; set; }
    }
}