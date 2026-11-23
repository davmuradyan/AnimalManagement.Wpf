using AnimalManagement.Wpf.Models.Dtos;
using AnimalManagement.Wpf.Models.Responses;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface IUserService
{
    Task<GetAllUsersResponse> GetUsersAsync();
}