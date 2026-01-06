namespace MiniWeatherApp.Models{

    public class City
    {
        public string Name {get;set;}
        public double Latitude{get;set;}
        public double Longitude {get;set;}
        public bool IsFavorite{get;set;}

        public City(string name, double latitude, double longitude)
        {
            Name=name;
            Latitude=latitude;
            Longitude=longitude;
            IsFavorite=false;
        }

    }
 }
