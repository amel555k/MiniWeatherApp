using AndroidX.AppCompat.View.Menu;
using MiniWeatherApp.Models;
using MiniWeatherApp.Services;
using MiniWeatherApp.ViewModels;
using MiniWeatherApp.Views;
using MiniWeatherApp.Services;

namespace MiniWeatherApp
{
	public partial class App : Application
	{

		public static WeatherService WeatherService {get; private set;}
		public static CityService CityService {get; private set;}
		public static MainViewModel MainViewModel {get;private set;}
		public App()
		{
			InitializeComponent();

			WeatherService=new WeatherService();
			CityService=new CityService();
			MainViewModel=new MainViewModel(WeatherService, CityService);

			MainPage=new NavigationPage(new MainPage());
		}
	}


}

