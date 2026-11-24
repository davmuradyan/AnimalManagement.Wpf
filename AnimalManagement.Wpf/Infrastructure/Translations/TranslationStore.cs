using AnimalManagement.Wpf.Models.Responses;
using CommunityToolkit.Mvvm.ComponentModel;

namespace AnimalManagement.Wpf.Infrastructure.Translations;

public partial class TranslationStore : ObservableObject
{
    [ObservableProperty]
    private int selectedLanguageCode;

    [ObservableProperty]
    private Dictionary<string, List<EnumTranslationResponse>> translations = [];

    public List<EnumTranslationResponse> Languages =>
        Translations.TryGetValue("Language", out var languages) ? languages : [];
    
    public List<EnumTranslationResponse> Colors =>
        Translations.TryGetValue("Color", out var colors) ? colors : [];
    
    public List<EnumTranslationResponse> Genders =>
        Translations.TryGetValue("Gender", out var colors) ? colors : [];

    public event EventHandler TranslationsChanged;

    partial void OnTranslationsChanged(Dictionary<string, List<EnumTranslationResponse>>? oldValue, Dictionary<string, List<EnumTranslationResponse>> newValue)
    {
        TranslationsChanged?.Invoke(this, EventArgs.Empty);
    }
}