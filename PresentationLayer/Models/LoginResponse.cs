namespace PresentationLayer.Models
{
    public sealed record LoginResponse(string AccessToken, string TokenType, DateTimeOffset ExpiresAt);
}
