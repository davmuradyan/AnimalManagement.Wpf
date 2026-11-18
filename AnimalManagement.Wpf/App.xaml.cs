using System.Windows;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Infrastructure.Translations;
using AnimalManagement.Wpf.Services.Implementations;
using AnimalManagement.Wpf.Services.Interfaces;
using AnimalManagement.Wpf.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AnimalManagement.Wpf;

public partial class App : Application
{
    private readonly IHost _host;
    private readonly string _baseUrl =  "https://localhost:7215/";

    public App()
    {
        _host = Host.CreateDefaultBuilder()
            .ConfigureServices((_, services) =>
            {
                // Windows
                services.AddSingleton<MainWindow>();

                // Navigation
                services.AddSingleton<INavigationService, NavigationService>();

                // ViewModels
                services.AddTransient<LoginViewModel>();
                services.AddTransient<RegisterViewModel>();
                services.AddTransient<DashboardViewModel>();
                
                // Base URL
                services.AddHttpClient<IAuthService, AuthService>(client =>
                {
                    client.BaseAddress = new Uri(_baseUrl);
                });

                services.AddHttpClient<ITranslationService, TranslationService>(client =>
                {
                    client.BaseAddress = new Uri(_baseUrl);
                });

                // Token store
                services.AddSingleton<TokenStore>();
                // TranslationStore
                services.AddSingleton<TranslationStore>();
            })
            .Build();
    }

    protected override async void OnStartup(StartupEventArgs e)
    {
        await _host.StartAsync();

        var navigationService =
            _host.Services.GetRequiredService<INavigationService>();

        navigationService.NavigateTo<LoginViewModel>();

        var mainWindow =
            _host.Services.GetRequiredService<MainWindow>();

        mainWindow.Show();

        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _host.StopAsync().GetAwaiter().GetResult();
        _host.Dispose();

        base.OnExit(e);
    }
}