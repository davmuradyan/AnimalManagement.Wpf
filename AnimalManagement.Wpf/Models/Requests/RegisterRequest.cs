namespace AnimalManagement.Wpf.Models.Requests;

public record RegisterRequest(
    string Name,
    string Surname,
    string Username,
    string Password);