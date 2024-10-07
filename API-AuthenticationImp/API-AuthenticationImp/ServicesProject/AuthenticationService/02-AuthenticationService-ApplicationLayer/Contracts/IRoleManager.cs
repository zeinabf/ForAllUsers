using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto;
using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts
{
    public interface IRoleManager
    {
        Task<Result> RegisterAsync(RoleForRegistrationDto roleRegistration);
        Task<Result<IList<Claim>>> GetRoleClaims(int roleid);
      Task<Result> RegisterRoleClaimsAsync(RoleClaimsDto roleRegistration);

    }
}
