using System.Net.Http;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class LoginViewModel(
    IAuthService authService,
    INavigationService navigationService,
    TokenStore tokenStore) : BaseViewModel
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(LoginCommand))]
    private bool isLoading;
    
    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;
    
    private bool CanLogin() => !IsLoading;

    [RelayCommand(CanExecute = nameof(CanLogin))]
    private async Task LoginAsync()
    {
        if (string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Password))
        {
            ErrorMessage = "Username and password are required.";
            return;
        }

        try
        {
            IsLoading = true;
            ErrorMessage = null;

            var request = new LoginRequest(Username, Password);

            var response = await authService.LoginAsync(request);

            await Task.Delay(500);
            tokenStore.SetToken(response.AccessToken, response.RefreshToken);

            navigationService.NavigateTo<DashboardViewModel>();
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "Invalid username or password, or server unavailable.";
        }
        catch (Exception)
        {
            ErrorMessage = "An unexpected error occurred.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void NavigateToRegister()
    {
        navigationService.NavigateTo<RegisterViewModel>();
    }
}