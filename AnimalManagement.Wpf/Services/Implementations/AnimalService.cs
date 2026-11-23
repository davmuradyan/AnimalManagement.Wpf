using System.Net.Http;
using System.Net.Http.Headers;
using AnimalManagement.Wpf.Infrastructure.Authentication;
using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;
using AnimalManagement.Wpf.Services.Interfaces;

namespace AnimalManagement.Wpf.Services.Implementations;

public class AnimalService(TokenStore tokenStore, HttpClient client) : IAnimalService
{
    public async Task<GetAllAnimalsResponse> GetAllAsync(GetAllAnimalsRequest request)
    {
        if (tokenStore.AccessToken is null)
            throw new InvalidOperationException("No access token provided");
        
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", tokenStore.AccessToken);
        
        try
        {
            var response = await client.PostAsJsonAsync("animal/all?api-version=2", request);
            if (response.IsSuccessStatusCode) {}
            // TODO: handle later
        
            var animalResponse = await response.Content.ReadFromJsonAsync<GetAllAnimalsResponse>();

            return animalResponse ?? new GetAllAnimalsResponse([], -1);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}