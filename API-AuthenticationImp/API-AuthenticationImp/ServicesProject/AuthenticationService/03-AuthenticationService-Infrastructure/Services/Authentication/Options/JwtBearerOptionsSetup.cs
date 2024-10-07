using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;

public class JwtBearerOptionsSetup : IConfigureNamedOptions<JwtBearerOptions>
{
    private readonly IdentityServiceOptions _userServiceOptions;

    public JwtBearerOptionsSetup(IOptions<IdentityServiceOptions> options)
    {
        _userServiceOptions = options.Value;
    }

    public void Configure(JwtBearerOptions options)
    {
        Configure(JwtBearerDefaults.AuthenticationScheme, options);
    }

    public void Configure(string? name, JwtBearerOptions options)
    {
        options.TokenValidationParameters = new()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _userServiceOptions.Issuer,
            ValidAudience = _userServiceOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_userServiceOptions.SecretKey)
            ),
            ClockSkew = TimeSpan.Zero,
           
        };
    }
}
