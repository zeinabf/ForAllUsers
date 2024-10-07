namespace AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;

public class IdentityServiceOptions
{
    public string SecretKey { get; init; } = string.Empty;
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public bool ValidateIssuer { get; init; } = true;
    public bool ValidateAudience { get; init; } = true;
}
