namespace AnimalManagement.Wpf.Models.Responses;

public record EnumTranslationResponse(int Code, string Translation);

public record GetAllTranslationsResponse(
    Dictionary<string, List<EnumTranslationResponse>> Translations);