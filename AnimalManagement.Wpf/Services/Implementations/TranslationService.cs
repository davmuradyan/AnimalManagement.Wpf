using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;

namespace AnimalManagement.Wpf.Services.Implementations;

public sealed class TranslationService(
    HttpClient httpClient,
    TokenStore tokenStore,
    IAuthService authService) : ITranslationService
{
    public async Task<GetAllTranslationsResponse> GetAllTranslationsAsync(int languageCode, CancellationToken ct = default)
    {
        if (string.IsNullOrWhiteSpace(tokenStore.AccessToken))
            throw new InvalidOperationException("Authentication token is missing.");

        using var response = await SendRequestAsync(languageCode, ct);

        if (response.StatusCode == HttpStatusCode.Unauthorized)
        {
            var authResponse = await authService.RotateAsync(new RotateRefreshTokenRequest(tokenStore.RefreshToken!), ct);

            tokenStore.SetToken(authResponse.AccessToken, authResponse.RefreshToken);

            using var retryResponse = await SendRequestAsync(languageCode, ct);

            retryResponse.EnsureSuccessStatusCode();

            return await retryResponse.Content
                       .ReadFromJsonAsync<GetAllTranslationsResponse>(cancellationToken: ct)
                        ?? throw new InvalidOperationException("Empty response.");
        }

        response.EnsureSuccessStatusCode();

        return await response.Content
                   .ReadFromJsonAsync<GetAllTranslationsResponse>(cancellationToken: ct)
                    ?? throw new InvalidOperationException("Empty response.");
    }
    private async Task<HttpResponseMessage> SendRequestAsync(int languageCode, CancellationToken ct)
    {
        using var request = new HttpRequestMessage(HttpMethod.Get, $"api/v1/translation?language={languageCode}");

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);

        return await httpClient.SendAsync(request, ct);
    }
}