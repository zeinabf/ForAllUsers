using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Validators;
using CSharpFunctionalExtensions;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.Role
{

    public class RoleManager : IRoleManager
    {
        private const int TOKEN_EXPIRES_IN = 1800;
        private readonly IIdentityService _identityService;


        public RoleManager(IIdentityService identityService)
        {
            _identityService = identityService;
        }


        public async Task<Result> RegisterAsync(RoleForRegistrationDto roleRegistration)
        {

            _01_AuthenticationService_DomainLayer.Role.Role newRole =
             new() { Name = roleRegistration.Name, NormalizedName = roleRegistration.Normalizedname };

            var registerResult =
                await _identityService.RegisterRoleAsync(newRole);            

            foreach (KeyValuePair<string, string> entry in roleRegistration.Claims)
            {
                Claim claims = new Claim(ClaimTypes.Webpage, entry.Value);
                var addClaimResult = await _identityService.AddClaimsAsyncRole(newRole, claims);
                if (addClaimResult.IsFailure)
                {
                    return addClaimResult;
                }

            }

            return Result.Success();
        }

        public async Task<Result> RegisterRoleClaimsAsync(RoleClaimsDto roleRegistration)
        {
            _01_AuthenticationService_DomainLayer.Role.Role role = new()
            {
                Id = roleRegistration.roleid,
            };

            Claim claims = new Claim(ClaimTypes.Webpage, roleRegistration.ClaimValue);
            //var registerResult =
            //    await _identityService.AddClaimsAsyncRole(role.Id!, claims);

 
            //if (registerResult.IsFailure)
            //{
            //    return registerResult;
            //}
            return Result.Success();
        }
        public async Task<Result<IList<Claim>>> GetRoleClaims(int roleid)
        {
            _01_AuthenticationService_DomainLayer.Role.Role role = new()
            { Id = roleid };
            var claimsResult = await _identityService.GetRoleClaimsAsync(role);
            
            if (claimsResult.IsFailure)
            {
                return Result.Failure<IList<Claim>>(claimsResult.Error);
            }
            return claimsResult;

        }

    }
}
