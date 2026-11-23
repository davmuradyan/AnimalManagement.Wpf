using System.Net.Http;
using System.Net.Http.Headers;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Models.Dtos;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;

namespace AnimalManagement.Wpf.Services.Implementations;

public class UserService(HttpClient client, TokenStore tokenStore) : IUserService
{
    public async Task<GetAllUsersResponse> GetUsersAsync()
    {
        if (tokenStore.AccessToken is null)
            throw new InvalidOperationException("No access token provided");
        
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);

        var response = await client.GetAsync("user");

        if (response.IsSuccessStatusCode) {}
            // TODO: handle later
        
        var users = await response.Content.ReadFromJsonAsync<GetAllUsersResponse>();

        return users ?? new GetAllUsersResponse([]);
    }
}