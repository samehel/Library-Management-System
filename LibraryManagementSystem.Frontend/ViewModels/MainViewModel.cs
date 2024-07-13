using LibraryManagementSystem.Frontend.Models;
using LibraryManagementSystem.Frontend.Services;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Reactive;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Frontend.ViewModels;

public class MainViewModel : ViewModelBase
{
    private readonly WeatherService _weatherService;
    public ObservableCollection<WeatherForecast> _weatherForecasts { get; } 
    public ReactiveCommand<Unit, Unit> LoadWeatherCommand { get; }

    public MainViewModel()
    {
        this._weatherService = new WeatherService(new HttpClient());
        this._weatherForecasts = new ObservableCollection<WeatherForecast>();

        LoadWeatherCommand = ReactiveCommand.CreateFromTask(LoadWeatherAsync);
    }

    private async Task LoadWeatherAsync()
    {
        var forecasts = await this._weatherService.GetWeatherForecastsAsync();
        this._weatherForecasts.Clear();
        foreach (var forecast in forecasts)
        {
            this._weatherForecasts.Add(forecast);
        }
    }
}
