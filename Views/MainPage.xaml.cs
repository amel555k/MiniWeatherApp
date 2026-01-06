using MiniWeatherApp.ViewModels;

namespace MiniWeatherApp.Views
{
    public partial class MainPage : ContentPage
    {
        private MainViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = App.MainViewModel;
            BindingContext = _viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            // Osvježi favorite status kada se vraćaš na stranicu 
            _viewModel.SelectedCity = App.CityService.GetCityByName(_viewModel.SelectedCity?.Name ?? "Sarajevo");
        }

        private async void OnMenuClicked(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Meni", "Odustani", null, "Pretraži gradove", "Favoriti");
            
            if (action == "Pretraži gradove")
            {
                var cityListPage = new CityListPage();
                await Navigation.PushAsync(cityListPage);
            }
            else if (action == "Favoriti")
            {
                var favoritesPage = new FavoritesPage();
                await Navigation.PushAsync(favoritesPage);
            }
        }
    }
}