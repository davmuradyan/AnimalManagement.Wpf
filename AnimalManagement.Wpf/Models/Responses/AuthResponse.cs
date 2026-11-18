namespace AnimalManagement.Wpf.Models.Responses;

public record AuthResponse(
    string AccessToken,
    string RefreshToken);