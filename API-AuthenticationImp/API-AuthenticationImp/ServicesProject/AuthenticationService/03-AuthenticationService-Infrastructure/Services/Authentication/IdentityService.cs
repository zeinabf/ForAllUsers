using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using AuthenticationService._01_AuthenticationService_DomainLayer.User;
using AuthenticationService._01_AuthenticationService_DomainLayer.UserRole;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role;
using AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace eShop.Security.Infrastructure.Services.Authentication;


public class IdentityService : IIdentityService
{
    private const string TOKEN_PROVIDER = "Default";
    private const string PURPOSE = "refresh_token";
    private readonly UserManager<User> _userManager;
    private readonly RoleManager<Role> _RoleManager;

    private readonly SignInManager<User> _signInManager;
    private readonly IUserClaimsPrincipalFactory<User> _userClaimsPrincipalFactory;
    private readonly IUserClaimsPrincipalFactory<Role> _roleClaimsPrincipalFactory;

    private readonly IdentityServiceOptions _options;

 
    public IdentityService(
       UserManager<User> userManager,
       RoleManager<Role> roleManager,
      
       SignInManager<User> signInManager,
       IUserClaimsPrincipalFactory<User> userClaimsPrincipalFactory,
       IOptions<IdentityServiceOptions> options

   )
    {
        _RoleManager = roleManager;
        _userManager = userManager;
        _signInManager = signInManager;
        _userClaimsPrincipalFactory = userClaimsPrincipalFactory;

        _options = options.Value;
    }

    public async Task<Result> AddClaimsAsync(User user, IEnumerable<Claim> claims)
    {
        var addClaimResult = await _userManager.AddClaimsAsync(user, claims);  

        if (addClaimResult.Succeeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(
                string.Join(",", addClaimResult.Errors.Select(e => e.Description))
            );
        }
    }
    public async Task<Result> AddClaimsAsyncRole(Role role, Claim claim)
    {
         
        var addClaimResult = await _RoleManager.AddClaimAsync(role, claim);
        if (addClaimResult.Succeeded)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure(
                string.Join(",", addClaimResult.Errors.Select(e => e.Description))
            );
        }
    }
    public async Task<Result<User>> FindByNameAsync(string userName)
    {
        var user = await _userManager.FindByNameAsync(userName);
        if (user is not null)
        { 
            return user;
        }
        else
        {
            return Result.Failure<User>($"User with name ({userName}) not found");
        }
    }

    public async Task<Result<IList<Claim>>> GetUserClaimsAsync(User user)
    {
       // var principal = await _userClaimsPrincipalFactory.CreateAsync(user);
       var principal = await _userManager.GetClaimsAsync(user);
        return Result.Success(principal);
    }

    public async Task<Result<IList<Claim>>> GetRoleClaimsAsync(Role role)
    {
        var principal = await  _RoleManager.GetClaimsAsync(role);
        return Result.Success(principal);
    }
   
    public async Task<Result<User>> GetUserByIDAsync(int userid)
    {

        var user = await _userManager.FindByIdAsync(userid.ToString());
        if (user is not null)
        {
            return Result.Success(user);
        }
        else
        {
            return Result.Failure<User>($"User with name ({user}) not found");
        }
    }

    public async Task<Result<Role>> FindByIDAsync(int  roleid)
    {
        var role = await _RoleManager.FindByIdAsync(roleid.ToString());
        if (role is not null)
        {
            return Result.Success(role);
        }
        else
        {
            return Result.Failure<Role>($"User with name ({roleid}) not found");
        }
    }

    public async Task<Result> CheckPasswordAsync(User user, string password)
    {
        // var signInResult = await _signInManager.PasswordSignInAsync(user, password, false, false);
        var signInResult = await _userManager.CheckPasswordAsync(user, password);
        if (signInResult)
        {
            return Result.Success();
        }
        else
        {
            return Result.Failure("UserName or Password is incorrect");
        }
    }

    public async Task<Result> RegisterAsync(User user, string password)
    {
        var registerResult = await _userManager.CreateAsync(user, password);
        if (registerResult.Succeeded)
        {
            //await _userManager.AddToRoleAsync(user, "Manager");
            return Result.Success();
        }
        else
        {
            return Result.Failure(
                string.Join(",", registerResult.Errors.Select(e => e.Description))
            );
        }
    }

    public async Task<Result> RegisterRoleAsync(Role role)
    {
       
        var registerResult = await _RoleManager.CreateAsync(role);

        if (registerResult.Succeeded)
        {
            //await _userManager.AddToRoleAsync(user, "Manager");
            return Result.Success(registerResult);
        }
        else
        {
            return Result.Failure(
                string.Join(",", registerResult.Errors.Select(e => e.Description))
            );
        }
    }

    public async Task<Result> RegisterUserRoleAsync(User user , Role role)
    {

        var registerResult = await _userManager.AddToRoleAsync(user, role.Name);
        if (registerResult.Succeeded)
        {
            await _userManager.AddClaimAsync(user, new Claim(ClaimTypes.Role, role.Id.ToString()));
            return Result.Success();
        }
        else
        {
            return Result.Failure(
                string.Join(",", registerResult.Errors.Select(e => e.Description))
            );
        }
    }

    public Result<string> GenerateAccessToken(IEnumerable<Claim> claims, DateTime expire)
    {
        var credentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SecretKey)),
            SecurityAlgorithms.HmacSha256
        ); 
        var securityToken = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            expires: expire,
            signingCredentials: credentials,
            claims: claims
        );

        var token = new JwtSecurityTokenHandler().WriteToken(securityToken);

        return token;
    }

     
}
