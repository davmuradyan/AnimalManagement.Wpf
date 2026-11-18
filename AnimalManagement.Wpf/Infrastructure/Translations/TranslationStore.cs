using AnimalManagement.Wpf.Models.Responses;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalManagement.Wpf.Infrastructure.Translations;

public partial class TranslationStore : ObservableObject
{
    [ObservableProperty]
    private int selectedLanguageCode;

    [ObservableProperty]
    private Dictionary<string, List<EnumTranslationResponse>> translations = [];

    public IReadOnlyList<EnumTranslationResponse> Languages =>
        Translations.TryGetValue("Language", out var languages) ? languages : [];
}