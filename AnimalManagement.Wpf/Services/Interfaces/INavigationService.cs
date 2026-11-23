using System.ComponentModel;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface INavigationService : INotifyPropertyChanged
{
    object? CurrentViewModel { get; }

    void NavigateTo<TViewModel>() where TViewModel : class;
}