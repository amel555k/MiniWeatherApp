using MiniWeatherApp.Models;

namespace MiniWeatherApp.Services
{
    public class CityService
    {
        private List<City> _cities;
        private const string FAVORITES_KEY="favorite_cities";

        public CityService()
        {
            InitializeCities();
            LoadFavorites();
        }

        private void InitializeCities()
        {
            _cities=new List<City>
            {
               
                new City("Sarajevo", 43.8563, 18.4131),
                new City("Banja Luka", 44.7722, 17.1910),
                new City("Tuzla", 44.5384, 18.6644),
                new City("Zenica", 44.2014, 17.9078),
                new City("Mostar", 43.3438, 17.8078),
                new City("Bijeljina", 44.7597, 19.2144),
                new City("Brčko", 44.8694, 18.8111),
                new City("Prijedor", 44.9800, 16.7142),
                new City("Trebinje", 42.7125, 18.3439),
                new City("Travnik", 44.2253, 17.6656),
                new City("Cazin", 44.9667, 15.9431),
                new City("Goražde", 43.6678, 18.9756),
                new City("Visoko", 43.9889, 18.1778),
                new City("Gračanica", 44.7028, 18.3111),
                new City("Livno", 43.8269, 17.0078),
                new City("Bugojno", 44.0569, 17.4508),
                new City("Konjic", 43.6514, 17.9614),
                new City("Gradačac", 44.8786, 18.4278),
                new City("Široki Brijeg", 43.3833, 17.6000),
                new City("Bihać", 44.8167, 15.8708) 
            };
        }

        public List<City> GetAllCities()
        {
            return _cities.OrderBy(c=>c.Name).ToList();
        }

        public List<City> GetFavoriteCities()
        {
            return _cities.Where(c=>c.IsFavorite).OrderBy(c=>c.Name).ToList();
        }

        public City GetCityByName(string name)
        {
            return _cities.FirstOrDefault(c=>c.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        public void ToggleFavorite(string cityName)
        {
            var city=GetCityByName(cityName);
            if (city != null)
            {
                city.IsFavorite=!city.IsFavorite;
                SaveFavorites();
            }
        }

        private void SaveFavorites()
        {
            var favoriteNames=_cities.Where(c=>c.IsFavorite).Select(c=>c.Name).ToList();
            var favoritesString=string.Join(",", favoriteNames);
            Preferences.Set(FAVORITES_KEY, favoritesString);
        }

        private void LoadFavorites()
        {
            var favoritesString=Preferences.Get(FAVORITES_KEY, string.Empty);
            if (!string.IsNullOrEmpty(favoritesString))
            {
                var favoritesNames=favoritesString.Split(',');
                foreach(var name in favoritesNames)
                {
                    var city = GetCityByName(name);
                        if (city != null)
                        {
                            city.IsFavorite=true;
                        }
                    }
                }
            }

            public List<City> SearchCities(string query)
            {
            if (string.IsNullOrWhiteSpace(query))
                return GetAllCities();
            
                return _cities
                .Where(c=>c.Name.Contains(query,StringComparison.OrdinalIgnoreCase))
                .OrderBy(c=>c.Name)
                .ToList();
            }
    }
}




