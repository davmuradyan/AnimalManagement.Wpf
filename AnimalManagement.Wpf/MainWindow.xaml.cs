using AnimalManagement.Wpf.Services.Interfaces;
using System.Windows;

namespace AnimalManagement.Wpf;

public partial class MainWindow : Window
{
    public MainWindow(INavigationService navigationService)
    {
        InitializeComponent();

        DataContext = navigationService;

        navigationService.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(navigationService.CurrentViewModel))
            {
                Console.WriteLine($"Current DataContext: {navigationService.CurrentViewModel?.GetType().FullName}");
            }
        };
    }
}