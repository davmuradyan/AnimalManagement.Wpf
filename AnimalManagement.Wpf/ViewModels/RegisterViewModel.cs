using System.Net.Http;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class RegisterViewModel(
    IAuthService authService,
    INavigationService navigationService) : BaseViewModel
{
    [ObservableProperty]
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private bool isLoading;
    
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string surname = string.Empty;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;

    [ObservableProperty]
    private string repeatPassword = string.Empty;

    private bool CanRegister() => !IsLoading;

    [RelayCommand(CanExecute = nameof(CanRegister))]
    private async Task RegisterAsync()
    {
        ErrorMessage = null;

        if (string.IsNullOrWhiteSpace(Name) ||
            string.IsNullOrWhiteSpace(Surname) ||
            string.IsNullOrWhiteSpace(Username) ||
            string.IsNullOrWhiteSpace(Password) ||
            string.IsNullOrWhiteSpace(RepeatPassword))
        {
            ErrorMessage = "All fields are required.";
            return;
        }

        if (Password != RepeatPassword)
        {
            ErrorMessage = "Passwords do not match.";
            return;
        }

        try
        {
            IsLoading = true;

            await authService.RegisterAsync(
                new RegisterRequest(
                    Name.Trim(),
                    Surname.Trim(),
                    Username.Trim(),
                    Password));

            navigationService.NavigateTo<LoginViewModel>();
        }
        catch (HttpRequestException)
        {
            ErrorMessage = "Registration failed. Please check your information.";
        }
        finally
        {
            IsLoading = false;
        }
    }

    [RelayCommand]
    private void NavigateToLogin()
    {
        navigationService.NavigateTo<LoginViewModel>();
    }
}