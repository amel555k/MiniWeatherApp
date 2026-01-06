using System.Collections.ObjectModel;
using System.Windows.Input;
using MiniWeatherApp.Models;
using MiniWeatherApp.Services;
using MiniWeatherApp.Models;
using MiniWeatherApp.Services;

namespace MiniWeatherApp.ViewModels
{
    public class FavoritesViewModel : BaseViewModel
    {
        private readonly CityService _cityService;
        private readonly MainViewModel _mainViewModel;

        private ObservableCollection<City> _favoriteCities;
        public ObservableCollection<City> FavoriteCities
        {
            get => _favoriteCities;
            set => SetProperty(ref _favoriteCities, value);
        }

        private bool _hasFavorites;
        public bool HasFavorites
        {
            get => _hasFavorites;
            set => SetProperty(ref _hasFavorites, value);
        }

        public ICommand SelectCityCommand { get; }
        public ICommand RemoveFavoriteCommand { get; }

        public FavoritesViewModel(CityService cityService, MainViewModel mainViewModel)
        {
            _cityService = cityService;
            _mainViewModel = mainViewModel;

            FavoriteCities = new ObservableCollection<City>();

            SelectCityCommand = new Command<City>(async (city) => await SelectCity(city));
            RemoveFavoriteCommand = new Command<City>(RemoveFavorite);
        }

        public void LoadFavorites()
        {
            var favorites = _cityService.GetFavoriteCities();
            FavoriteCities.Clear();
            foreach (var city in favorites)
            {
                FavoriteCities.Add(city);
            }

            HasFavorites = FavoriteCities.Count > 0;
        }

        private async Task SelectCity(City city)
        {
            if (city != null)
            {
                await _mainViewModel.SelectCity(city);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }

        private void RemoveFavorite(City city)
        {
            if (city != null)
            {
                _cityService.ToggleFavorite(city.Name);
                LoadFavorites();
            }
        }
    }
}