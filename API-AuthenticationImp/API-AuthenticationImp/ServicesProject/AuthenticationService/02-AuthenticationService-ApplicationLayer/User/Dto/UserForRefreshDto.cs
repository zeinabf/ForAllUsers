namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;

public sealed record UserForRefreshDto(string AccessToken, string RefreshToken);
