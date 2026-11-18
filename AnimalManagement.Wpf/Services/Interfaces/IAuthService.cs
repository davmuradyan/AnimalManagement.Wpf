using AnimalManagement.Wpf.Models.Requests;
using AnimalManagement.Wpf.Models.Responses;

namespace AnimalManagement.Wpf.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default);

    Task RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default);
    
    Task<AuthResponse> RotateAsync(RotateRefreshTokenRequest request, CancellationToken cancellationToken = default);
}