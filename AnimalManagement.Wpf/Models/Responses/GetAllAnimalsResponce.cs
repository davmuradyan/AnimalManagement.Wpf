using AnimalManagement.Wpf.Models.Dtos;

namespace AnimalManagement.Wpf.Models.Responses;

public record GetAllAnimalsResponse(List<AnimalDto> Animals, int MaxPageNumber);