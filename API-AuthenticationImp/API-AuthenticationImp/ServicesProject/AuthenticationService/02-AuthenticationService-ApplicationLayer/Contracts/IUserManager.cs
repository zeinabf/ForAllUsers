using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto;


namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts
{
    public interface IUserManager
    {
        Task<Result> RegisterAsync(UserForRegistrationDto userRegistration);
        Task<Result> RegisterRoleUserAsync(UserRoleRegistrationDto userRegistration);

        Task<Result<UserTokenDto>> LoginAsync(UserForLoginDto loginDto);
    }
}
