namespace AnimalManagement.Wpf.Models.Responses;

public record AnimalDto(
    int Id,
    string Name,
    int Age,
    double Weight,
    string Gender,
    string Color);