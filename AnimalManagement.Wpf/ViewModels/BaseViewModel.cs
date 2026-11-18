using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalManagement.Wpf.ViewModels;

public abstract partial class BaseViewModel : ObservableObject
{
    [ObservableProperty]
    private string? errorMessage;
}