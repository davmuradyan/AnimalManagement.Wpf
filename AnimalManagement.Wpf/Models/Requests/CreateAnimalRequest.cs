namespace AnimalManagement.Wpf.Models.Requests;

public record CreateAnimalRequest(string Name, int Age, double Weight, int Gender, int Color);
