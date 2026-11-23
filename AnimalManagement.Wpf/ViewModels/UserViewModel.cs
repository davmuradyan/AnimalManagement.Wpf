using System.Collections.ObjectModel;
using AnimalManagement.Wpf.Models.Dtos;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class UserViewModel : BaseViewModel
{
    readonly IUserService userService;
    readonly INavigationService navigationService;

    [ObservableProperty]
    bool _isLoading;

    [ObservableProperty]
    string _currentUserFullName;
    
    [ObservableProperty]
    string _currentUsername;

    [ObservableProperty] private ObservableCollection<UserDto> _users = [];

    public UserViewModel(IUserService _userService, INavigationService _navigationService)
    {
        userService = _userService;
        navigationService = _navigationService;
        
        _ = InitializeAsync();
    }

    async Task InitializeAsync()
    {
        IsLoading = true;
        try
        {
            await LoadUsersAsync();
            await Task.Delay(500);
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    public async Task LoadUsersAsync()
    {
        var users = await userService.GetUsersAsync();
        Users.Clear();
        users.Users.ForEach(u => Users.Add(u));
    }
    
    [RelayCommand]
    private void NavigateToDashboard()
    {
        navigationService.NavigateTo<DashboardViewModel>();
    }
}