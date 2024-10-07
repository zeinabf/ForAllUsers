using AuthenticationService._01_AuthenticationService_DomainLayer.UserRole;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.SqlServer.Management.Smo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.User
{
    
    public class UserManager : IUserManager
    {
        private const int TOKEN_EXPIRES_IN = 1800;
        private readonly IIdentityService _identityService;
        private readonly IValidator<UserForRegistrationDto> _userForRegistrationValidator;
        private readonly IValidator<UserForLoginDto> _userForLoginValidator;
        private readonly IValidator<UserForRefreshDto> _userForRefreshValidator;
 
        public UserManager(
            IIdentityService identityService
            ,
            IValidator<UserForRegistrationDto> userForRegistrationValidator,
            IValidator<UserForLoginDto> userForLoginValidator,
            IValidator<UserForRefreshDto> userForRefreshValidator
        )
        {
            _identityService = identityService;
            _userForLoginValidator = userForLoginValidator;
            _userForRegistrationValidator = userForRegistrationValidator;
            _userForRefreshValidator = userForRefreshValidator;
        }
        public async Task<Result<UserTokenDto>> LoginAsync(UserForLoginDto loginDto)
        {
             
            var vaidationResult = _userForLoginValidator.Validate(loginDto);
            if (!vaidationResult.IsValid)
            {

                return Result.Failure<UserTokenDto>(JsonSerializer.Serialize(vaidationResult));
            }

            var findResult = await _identityService.FindByNameAsync(loginDto.UserName);
            
            if (findResult.IsFailure)
            {
             
                return Result.Failure<UserTokenDto>(findResult.Error);
            }

            var singInResult = await _identityService.CheckPasswordAsync(
                findResult.Value,
                loginDto.Password
            );
            if (singInResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(singInResult.Error);
            }

            return await GenerateTokensAsync(findResult.Value);
        }
  

        public async Task<Result> RegisterAsync(UserForRegistrationDto userRegistration)
        {
            var vaidationResult = _userForRegistrationValidator.Validate(userRegistration);
            if (!vaidationResult.IsValid)
                return Result.Failure(JsonSerializer.Serialize(vaidationResult));

            _01_AuthenticationService_DomainLayer.User.User newUser =
                new() { UserName = userRegistration.UserName, Email = userRegistration.Email };
            var registerResult =
                await _identityService.RegisterAsync(
                newUser,
                userRegistration.Password
            );

            if (registerResult.IsFailure)
            {
                return registerResult;
            }

            List<Claim> claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.NameIdentifier, newUser.Id.ToString()),
                    new Claim(ClaimTypes.Name, newUser?.UserName ?? string.Empty),
                    new Claim(ClaimTypes.Email, newUser?.Email ?? string.Empty),
                    new Claim(ClaimTypes.GivenName, userRegistration.FirstName),
                    new Claim(ClaimTypes.Surname, userRegistration.LastName),
                    new Claim(ClaimTypes.Gender, userRegistration.Gender),
                    
                   // new Claim(ClaimTypes.Role,userRegistration.role)
            
            
           
                };
            var addClaimResult = await _identityService.AddClaimsAsync(newUser!, claims);
            if (addClaimResult.IsFailure)
            {
                return addClaimResult;
            }

            return Result.Success();
        } 

         

        public async Task<Result> RegisterRoleUserAsync(UserRoleRegistrationDto userRegistration)
        {
            var role = await _identityService.FindByIDAsync(userRegistration.roleid);
            var user = await _identityService.GetUserByIDAsync(userRegistration.userid);


            var registerResult =
                 await _identityService.RegisterUserRoleAsync(user.Value, role.Value);

            //Claim claims = new Claim(ClaimTypes.NameIdentifier, newRole.Name.ToString());

            //var addClaimResult = await _identityService.AddClaimsAsyncRole(newRole!, claims);
            //if (addClaimResult.IsFailure)
            //{
            //    return addClaimResult;
            //}


            return Result.Success();
        }

        private async Task<Result<UserTokenDto>> GenerateTokensAsync(_01_AuthenticationService_DomainLayer.User.User user)
        {
            string  roleid = "2";
            var claimsResult = await _identityService.GetUserClaimsAsync(user);
            foreach (var claim in claimsResult.Value) {
                if (claim.Type.Contains("role"))
                    roleid = claim.Value;
 
            }
            if (claimsResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(claimsResult.Error);
            }

            var expire = DateTime.Now.AddSeconds(TOKEN_EXPIRES_IN);
            var jwtAccessTokenResult = _identityService.GenerateAccessToken(claimsResult.Value, expire);
            if (jwtAccessTokenResult.IsFailure)
            {
                return Result.Failure<UserTokenDto>(jwtAccessTokenResult.Error);
            }

            var result = new UserTokenDto(
                "Bearer",
                jwtAccessTokenResult.Value,
                string.Empty,
                TOKEN_EXPIRES_IN,
                roleid
            );

            return Result.Success(result);
        }
    }
}
