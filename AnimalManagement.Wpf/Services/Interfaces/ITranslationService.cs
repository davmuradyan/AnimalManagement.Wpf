using AnimalManagement.Wpf.Models.Responses;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface ITranslationService
{
    Task GetAllTranslations(int languageCode);
    Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string type, int languageCode);
}