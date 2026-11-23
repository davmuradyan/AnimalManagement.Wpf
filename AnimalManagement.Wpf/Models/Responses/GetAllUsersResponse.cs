using AnimalManagement.Wpf.Models.Dtos;

namespace AnimalManagement.Wpf.Models.Responses;

public record GetAllUsersResponse(List<UserDto> Users);