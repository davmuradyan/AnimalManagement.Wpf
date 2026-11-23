
using System.Collections.ObjectModel;
using AnimalManagement.Wpf.Infrastructure.Translations;
using AnimalManagement.Wpf.Models.Dtos;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace AnimalManagement.Wpf.ViewModels;

public partial class AnimalControlViewModel : BaseViewModel
{
    readonly INavigationService _navigationService;
    readonly ITranslationService _translationService;
    readonly TranslationStore _translationStore;
    readonly IAnimalService _animalService;

    [ObservableProperty]
    int previousPage;

    [ObservableProperty]
    int currentPage = 1;

    [ObservableProperty]
    int nextPage = 1;

    [ObservableProperty]
    bool canGoPrevious;

    [ObservableProperty]
    bool canGoNext;

    private readonly int pageSize = 10;
    private int MaxPage { get; set; } = 1;

    [ObservableProperty]
    private ObservableCollection<EnumTranslationResponse> _languages = [];

    [ObservableProperty]
    private EnumTranslationResponse? _selectedLanguage;

    [ObservableProperty]
    private ObservableCollection<AnimalDto> _animals = [];

    public AnimalControlViewModel(INavigationService navigationService,
        ITranslationService translationService,
        TranslationStore translationStore,
        IAnimalService animalService)
    {
        _navigationService = navigationService;
        _translationService = translationService;
        _translationStore = translationStore;
        _animalService = animalService;
        _ = InitializeLanguageAsync(1);

        _ = GetAnimalsAsync(new GetAllAnimalsRequest
        {
            Page = 1,
            PageSize = pageSize,
            Count = pageSize
        });
    }

    async Task GetAnimalsAsync(GetAllAnimalsRequest request)
    {
        var result = await _animalService.GetAllAsync(request);

        Animals.Clear();
        result.Animals.ForEach(Animals.Add);

        MaxPage = result.MaxPageNumber;

        CurrentPage = request.Page;

        if (CurrentPage < 1)
            CurrentPage = 1;

        if (CurrentPage > MaxPage)
            CurrentPage = MaxPage;

        PreviousPage = Math.Max(1, CurrentPage - 1);
        NextPage = Math.Min(MaxPage, CurrentPage + 1);

        CanGoPrevious = CurrentPage > 1;
        CanGoNext = CurrentPage < MaxPage;
    }

    [RelayCommand]
    public async Task NextPageAsync()
    {
        if (!CanGoNext)
            return;

        await GetAnimalsAsync(new GetAllAnimalsRequest
        {
            Page = CurrentPage + 1,
            PageSize = pageSize,
            Count = pageSize
        });
    }

    [RelayCommand]
    public async Task PreviousPageAsync()
    {
        if (!CanGoPrevious)
            return;

        await GetAnimalsAsync(new GetAllAnimalsRequest
        {
            Page = CurrentPage - 1,
            PageSize = pageSize,
            Count = pageSize
        });
    }

    bool _isUpdatingSelection;

    async Task InitializeLanguageAsync(int code)
    {
        _isUpdatingSelection = true;

        try
        {
            await _translationService.GetAllTranslations(code);

            Languages.Clear();

            foreach (var language in _translationStore.Languages)
                Languages.Add(language);

            SelectedLanguage = Languages.FirstOrDefault(x => x.Code == code);
        }
        finally
        {
            _isUpdatingSelection = false;
        }
    }

    partial void OnSelectedLanguageChanged(EnumTranslationResponse? value)
    {
        if (value is null || _isUpdatingSelection)
            return;

        _ = InitializeLanguageAsync(value.Code);
    }

    [RelayCommand]
    void NavigateToDashboard()
    {
        _navigationService.NavigateTo<DashboardViewModel>();
    }
}
