namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;


public sealed record UserTokenDto(
    string TokenType,
    string AccessToken,
    string RefreshToken,
    int ExpiresIn,
    string Roleid
);
