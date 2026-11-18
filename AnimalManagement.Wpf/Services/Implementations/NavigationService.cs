using System.ComponentModel;
using System.Runtime.CompilerServices;
using AnimalManagement.Wpf.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace AnimalManagement.Wpf.Services.Implementations;

public class NavigationService(IServiceProvider serviceProvider) : INavigationService
{
    private object? _currentViewModel;

    public object? CurrentViewModel
    {
        get => _currentViewModel;
        private set
        {
            if (_currentViewModel == value)
                return;

            _currentViewModel = value;
            OnPropertyChanged();
        }
    }

    public void NavigateTo<TViewModel>() where TViewModel : class
    {
        var viewModel = serviceProvider.GetRequiredService<TViewModel>();

        Console.WriteLine($"Navigating to: {viewModel.GetType().FullName}");

        CurrentViewModel = viewModel;
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    private void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}