using AnimalManagement.Wpf.Models.Responses;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface ITranslationService
{
    Task<GetAllTranslationsResponse> GetAllTranslationsAsync(int languageCode, CancellationToken ct = default);
}