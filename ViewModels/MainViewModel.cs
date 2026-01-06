using MiniWeatherApp.Models;
using MiniWeatherApp.Services;
using System.Windows.Input;
using System.Collections.ObjectModel;

namespace MiniWeatherApp.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private readonly WeatherService _weatherService;
        private readonly CityService _cityService;

        private City _selectedCity;
        public City SelectedCity
        {
            get=>_selectedCity;
            set
            {
                if(SetProperty(ref _selectedCity, value))
                {
                    OnPropertyChanged(nameof(IsFavorite));
                }
            }
        }

        private WeatherForecast _currentForecast;
        public WeatherForecast CurrentForecast
        {
          get=> _currentForecast;
          set=> SetProperty(ref _currentForecast, value);

        }

        private ObservableCollection<DayForecast> _dailyForecasts;
        public ObservableCollection<DayForecast> DailyForecasts
        {
            get=> _dailyForecasts;
            set=> SetProperty(ref _dailyForecasts, value);
        }

        private string _errorMessage;

        public string ErrorMessage
        {
            get=>_errorMessage;
            set=>SetProperty(ref _errorMessage,value);
        }

        public bool IsFavorite=> SelectedCity?.IsFavorite ?? false;

        public ICommand OpenCityListCommand {get;}
        public ICommand OpenFavoritesCommand {get;}
        public ICommand ToggleFavoriteCommand {get;}
        public ICommand RefreshCommand {get;}


        public MainViewModel(WeatherService weatherService, CityService cityService)
        {
            _weatherService=weatherService;
            _cityService=cityService;

            OpenCityListCommand=new Command(async() => await OpenCityList());
            OpenFavoritesCommand=new Command(async()=> await OpenFavorites());
            ToggleFavoriteCommand=new Command(ToggleFavorite);
            RefreshCommand=new Command(async()=> await LoadWeatherData());


            DailyForecasts=new ObservableCollection<DayForecast>();

            var lastCity=Preferences.Get("last_selected_city", "Sarajevo");
            SelectedCity=_cityService.GetCityByName(lastCity);

            _=LoadWeatherData();
        }

        public async Task LoadWeatherData()
        {
            if(SelectedCity==null)
            return;

            IsBusy=true;
            ErrorMessage=string.Empty;

            try
            {
                CurrentForecast=await _weatherService.GetForecastAsync(SelectedCity);
                if (CurrentForecast != null)
                {
                    var forecasts=_weatherService.ParseDailyForecast(CurrentForecast);
                    DailyForecasts.Clear();
                    foreach(var forecast in forecasts)
                    {
                        DailyForecasts.Add(forecast);
                    }

                    Preferences.Set("last_selected_city", SelectedCity.Name);
                }
                else
                {
                    ErrorMessage="Nije moguce ucitati vremensku prognozu!";
                }
            }

            catch (Exception ex)
            {
                ErrorMessage=$"Greska : {ex.Message}";
            }
            finally
            {
                IsBusy=false;
            }
        }

        public async Task SelectCity(City city)
        {
            if (city != null)
            {
                SelectedCity=_cityService.GetCityByName(city.Name);
                await LoadWeatherData();
            }
        }

        private void ToggleFavorite()
        {
            if (SelectedCity != null)
            {
                _cityService.ToggleFavorite(SelectedCity.Name);
                SelectedCity=_cityService.GetCityByName(SelectedCity.Name);
                OnPropertyChanged(nameof(IsFavorite));
            }
        }

        private async Task OpenCityList()
        {
            var cityListPage=new Views.CityListPage();
             await Application.Current.MainPage.Navigation.PushAsync(cityListPage);
        }

        private async Task OpenFavorites()
        {
            var favoritesPage=new Views.FavoritesPage();
            await Application.Current.MainPage.Navigation.PushAsync(favoritesPage);
        }
    }

}
