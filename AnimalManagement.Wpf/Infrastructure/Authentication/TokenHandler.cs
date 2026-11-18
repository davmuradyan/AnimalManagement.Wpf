namespace AnimalManagement.Wpf.Infrastructure.Authentication;

public class TokenStore
{
    public string? AccessToken { get; private set; }

    public string? RefreshToken { get; private set; }

    public bool IsAuthenticated =>
        !string.IsNullOrWhiteSpace(AccessToken);

    public void SetToken(string token, string refreshToken)
    {
        AccessToken = token;
        RefreshToken = refreshToken;
    }

    public void Clear()
    {
        AccessToken = null;
        RefreshToken = null;
    }
}