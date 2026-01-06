using Microsoft.Extensions.Logging;
using MiniWeatherApp.Services;
using MiniWeatherApp.ViewModels;
using MiniWeatherApp.Views;
using MiniWeatherApp.Services;

namespace MiniWeatherApp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

			builder.Services.AddSingleton<WeatherService>();
			builder.Services.AddSingleton<CityService>();

			builder.Services.AddSingleton<MainViewModel>();
			builder.Services.AddSingleton<CityListViewModel>();
			builder.Services.AddSingleton<FavoritesViewModel>();


			builder.Services.AddSingleton<MainPage>();
			builder.Services.AddSingleton<CityListPage>();
			builder.Services.AddSingleton<FavoritesPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
