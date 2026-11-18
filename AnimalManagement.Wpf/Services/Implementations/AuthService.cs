using System.Net.Http;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;

namespace AnimalManagement.Wpf.Services.Implementations;

public sealed class AuthService(HttpClient httpClient) : IAuthService
{
    public async Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/user/login", request, cancellationToken);

        response.EnsureSuccessStatusCode();

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>(
            cancellationToken: cancellationToken);

        return authResponse ?? throw new InvalidOperationException("The server returned an empty authentication response.");
    }

    public async Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync(
            "api/v1/user/register",
            request,
            cancellationToken);

        response.EnsureSuccessStatusCode();
    }

    public async Task<AuthResponse> RotateAsync(RotateRefreshTokenRequest request, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("api/v1/user/rotate", request, cancellationToken);
        
        response.EnsureSuccessStatusCode();

        var authResponse = await response.Content.ReadFromJsonAsync<AuthResponse>(
            cancellationToken: cancellationToken);

        return authResponse ?? throw new InvalidOperationException("The server returned an empty authentication response.");
    }
}