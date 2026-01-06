using System.Collections.ObjectModel;
using System.Windows.Input;
using Android.Database;
using MiniWeatherApp.Models;
using MiniWeatherApp.Services;

namespace MiniWeatherApp.ViewModels
{
    public class CityListViewModel : BaseViewModel
    {
        private readonly CityService _cityService;
        private readonly MainViewModel _mainViewModel;

        private ObservableCollection <City> _cities;
        public ObservableCollection<City> Cities
        {
            get=> _cities;
            set=> SetProperty(ref _cities,value);
        }

        private string _searchText;
        public string SearchText
        {
            get=>_searchText;
            set
            {
                if(SetProperty(ref _searchText, value))
                {
                    PerformSearch();
                }
            }
        }

        public ICommand SelectCityCommand{get;}

        public CityListViewModel(CityService cityService, MainViewModel mainViewModel)
        {
            _cityService=cityService;
            _mainViewModel=mainViewModel;
            Cities=new ObservableCollection<City>();
            SelectCityCommand=new Command<City>(async(city)=>await SelectCity(city));
            LoadCities();
        }

        private void LoadCities()
        {
            var allCities=_cityService.GetAllCities();
            Cities.Clear();
            foreach(var city in allCities)
            {
                Cities.Add(city);
            }
        }

        private void PerformSearch()
        {
            var searchResults=_cityService.SearchCities(SearchText);
            Cities.Clear();
            foreach(var city in searchResults)
            {
                Cities.Add(city);
            }
        }

        private async Task SelectCity(City city)
        {
            if (city != null)
            {
                await _mainViewModel.SelectCity(city);
                await Application.Current.MainPage.Navigation.PopAsync();
            }
        }
    }
}