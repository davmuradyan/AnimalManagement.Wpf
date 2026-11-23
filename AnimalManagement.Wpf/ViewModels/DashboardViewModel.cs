using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    readonly INavigationService navigationService;
    public DashboardViewModel(INavigationService navigationService)
    {
        this.navigationService = navigationService;
    }

    [RelayCommand]
    private void NavigateToUsers()
    {
        navigationService.NavigateTo<UserViewModel>();
    }
    
    [RelayCommand]
    private void NavigateToAnimalControl()
    {
        navigationService.NavigateTo<AnimalControlViewModel>();
    }
}