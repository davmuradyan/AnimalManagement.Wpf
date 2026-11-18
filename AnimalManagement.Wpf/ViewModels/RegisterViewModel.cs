using System.Net.Http;
using System.Text.RegularExpressions;
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
    [NotifyCanExecuteChangedFor(nameof(RegisterCommand))]
    private bool allowRegister;
    
    [ObservableProperty]
    private string name = string.Empty;

    [ObservableProperty]
    private string surname = string.Empty;

    [ObservableProperty]
    private string username = string.Empty;

    [ObservableProperty]
    private string password = string.Empty;
    
    [ObservableProperty]
    private string passwordError = string.Empty;
    
    [ObservableProperty]
    private string repeatPassword = string.Empty;

    private bool CanRegister() => !IsLoading && allowRegister;

    partial void OnPasswordChanged(string value)
    {
        if (Regex.IsMatch(value, @"[\s\p{C}]") || Regex.IsMatch(value, @"[\p{L}-[A-Za-z]]"))
        {
            PasswordError = "Password contains an invalid character.";
            allowRegister = false;
        }
        else
        {
            PasswordError = "";
            allowRegister = true;
        }
    }
    
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