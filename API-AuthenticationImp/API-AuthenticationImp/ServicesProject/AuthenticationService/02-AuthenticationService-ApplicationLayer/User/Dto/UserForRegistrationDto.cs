namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;

public sealed record UserForRegistrationDto(
    string UserName,
    string Email,
    string FirstName,
    string LastName,
    string Password,
    string Gender
     
);
