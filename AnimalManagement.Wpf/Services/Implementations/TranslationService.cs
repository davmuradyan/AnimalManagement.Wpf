using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Infrastructure.Translations;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;

namespace AnimalManagement.Wpf.Services.Implementations;

public sealed class TranslationService(
    HttpClient client, 
    TokenStore tokenStore, 
    TranslationStore translationStore, 
    IAuthService authService) : ITranslationService
{
    public async Task GetAllTranslations(int languageCode)
    {
        if (tokenStore.AccessToken is null)
            throw new InvalidOperationException("No access token provided");
        
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);

        try
        {
            var response = await client.GetAsync("translation?language=" + languageCode);
            if (!response.IsSuccessStatusCode)
            {
                if (response.StatusCode == HttpStatusCode.Unauthorized && tokenStore.RefreshToken is not null)
                {
                    await authService.RotateAsync(new RotateRefreshTokenRequest(tokenStore.RefreshToken));
                }
            }
            // TODO: handle later
        
            var translationsResponse = await response.Content.ReadFromJsonAsync<GetAllTranslationsResponse>();

            translationStore.SelectedLanguageCode = languageCode;
            translationStore.Translations = translationsResponse?.Translations ?? new();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Task<GetEnumTypeTranslationsResponse> GetEnumTypeTranslations(string type, int languageCode)
    {
        throw new NotImplementedException();
    }
}