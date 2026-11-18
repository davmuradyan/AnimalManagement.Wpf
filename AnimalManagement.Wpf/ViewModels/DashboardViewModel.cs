using System.Collections.ObjectModel;
using AnimalManagement.Wpf.Infrastructure.Translations;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalManagement.Wpf.ViewModels;

public partial class DashboardViewModel : BaseViewModel
{
    private readonly ITranslationService _translationService;
    private readonly TranslationStore _translationStore;

    public IReadOnlyList<EnumTranslationResponse> Languages =>
        _translationStore.Languages;

    public ObservableCollection<AnimalDto> Animals { get; } = [];

    [ObservableProperty]
    private EnumTranslationResponse? selectedLanguage;

    public DashboardViewModel(
        ITranslationService translationService,
        TranslationStore translationStore)
    {
        _translationService = translationService;
        _translationStore = translationStore;

        _ = InitializeAsync();
    }

    partial void OnSelectedLanguageChanged(EnumTranslationResponse? value)
    {
        if (value is null ||
            value.Code == _translationStore.SelectedLanguageCode)
            return;

        _ = LoadDataAsync(value.Code);
    }

    private async Task InitializeAsync()
    {
        await LoadDataAsync(0);
    }

    private async Task LoadDataAsync(int languageCode)
    {
        try
        {
            ErrorMessage = null;

            // Load translations.
            var translations = await _translationService
                .GetAllTranslationsAsync(languageCode);

            _translationStore.Translations = translations.Translations;
            _translationStore.SelectedLanguageCode = languageCode;

            OnPropertyChanged(nameof(Languages));

            SelectedLanguage = Languages.FirstOrDefault(
                x => x.Code == languageCode);
        }
        catch (Exception ex)
        {
            ErrorMessage = $"Failed to load dashboard data: {ex.Message}";
        }
    }
}