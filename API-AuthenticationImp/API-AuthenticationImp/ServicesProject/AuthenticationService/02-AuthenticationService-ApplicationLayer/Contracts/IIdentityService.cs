using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationService._01_AuthenticationService_DomainLayer.User;
using AuthenticationService._01_AuthenticationService_DomainLayer.UserRole;
using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
namespace AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts
{
    public interface IIdentityService
    {
        Task<Result> RegisterAsync(_01_AuthenticationService_DomainLayer.User.User user, string password);
        Task<Result> RegisterRoleAsync(_01_AuthenticationService_DomainLayer.Role.Role role);
        Task<Result<_01_AuthenticationService_DomainLayer.User.User>> FindByNameAsync(string userName);
        Task<Result<IList<Claim>>> GetUserClaimsAsync(_01_AuthenticationService_DomainLayer.User.User user);
        Task<Result> AddClaimsAsync(_01_AuthenticationService_DomainLayer.User.User user, IEnumerable<Claim> claims);
        Task<Result> AddClaimsAsyncRole(_01_AuthenticationService_DomainLayer.Role.Role role, Claim claim);
        Task<Result> CheckPasswordAsync(_01_AuthenticationService_DomainLayer.User.User user, string password);
        Result<string> GenerateAccessToken(IEnumerable<Claim> claims, DateTime expire);
        Task<Result<_01_AuthenticationService_DomainLayer.User.User>> GetUserByIDAsync(int userid);
        Task<Result<_01_AuthenticationService_DomainLayer.Role.Role>> FindByIDAsync(int roleid);
        Task<Result<IList<Claim>>> GetRoleClaimsAsync(_01_AuthenticationService_DomainLayer.Role.Role role);
        Task<Result> RegisterUserRoleAsync(_01_AuthenticationService_DomainLayer.User.User user, _01_AuthenticationService_DomainLayer.Role.Role role);

    }
}
