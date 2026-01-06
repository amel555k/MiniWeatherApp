namespace WeatherApp.Helpers
{
    public static class WeatherCodeHelper
    {
        public static string GetWeatherDescription(int code)
        {
            return code switch
            {
                0 => "Vedro",
                1 => "Pretežno vedro",
                2 => "Djelimično oblačno",
                3 => "Oblačno",
                45 => "Magla",
                48 => "Magla sa mrazom",
                51 => "Slaba kiša",
                53 => "Umjerena kiša",
                55 => "Jaka kiša",
                61 => "Slaba kiša",
                63 => "Umjerena kiša",
                65 => "Jaka kiša",
                71 => "Slab snijeg",
                73 => "Umjeren snijeg",
                75 => "Jak snijeg",
                77 => "Snježne pahulje",
                80 => "Pljuskovi",
                81 => "Jaki pljuskovi",
                82 => "Oluja",
                85 => "Snježni pljuskovi",
                86 => "Jaki snježni pljuskovi",
                95 => "Grmljavina",
                96 => "Grmljavina sa gradom",
                99 => "Grmljavina sa jakim gradom",
                _ => "Nepoznato"
            };
        }

        public static string GetWeatherIcon(int code)
        {
            return code switch
            {
                0 => "☀️",
                1 => "🌤️",
                2 => "⛅",
                3 => "☁️",
                45 or 48 => "🌫️",
                51 or 53 or 55 or 61 or 63 or 65 => "🌧️",
                71 or 73 or 75 or 77 or 85 or 86 => "❄️",
                80 or 81 or 82 => "⛈️",
                95 or 96 or 99 => "⚡",
                _ => "🌡️"
            };
        }
    }
}