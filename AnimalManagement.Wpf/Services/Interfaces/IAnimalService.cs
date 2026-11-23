using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface IAnimalService
{
    Task<GetAllAnimalsResponse> GetAllAsync(GetAllAnimalsRequest request);
}