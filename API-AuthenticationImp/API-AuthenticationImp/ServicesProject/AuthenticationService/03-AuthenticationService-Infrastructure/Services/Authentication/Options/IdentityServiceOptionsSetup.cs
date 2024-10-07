using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;

public class IdentityServiceOptionsSetup : IConfigureOptions<IdentityServiceOptions>
{
    private const string SectionName = "IdentityService";

    private readonly IConfiguration _configuration;

    public IdentityServiceOptionsSetup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(IdentityServiceOptions options)
    {
        _configuration.GetSection(SectionName).Bind(options);
    }
}
