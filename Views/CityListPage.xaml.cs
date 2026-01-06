using MiniWeatherApp.ViewModels;

namespace MiniWeatherApp.Views
{
    public partial class CityListPage : ContentPage
    {
        public CityListPage()
        {
            InitializeComponent();
            
            var weatherService = App.WeatherService;
            var cityService = App.CityService;
            var mainViewModel = App.MainViewModel;

            BindingContext = new CityListViewModel(cityService, mainViewModel);
        }
    }
}