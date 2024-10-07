 using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using AuthenticationService._01_AuthenticationService_DomainLayer.User;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.User;
using AuthenticationService._03_AuthenticationService_Infrastructure.Persistence;
using AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;
using eShop.Security.Infrastructure.Services.Authentication;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;





namespace AuthenticationService
{
    public static class DependencyInjection
    {
        public static void AddSecurityServices(this IServiceCollection Services,
                                            IConfiguration configuration,
                                            IWebHostEnvironment env)
        {
            // Add services to the container.
            Services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            Services.AddScoped<IRoleManager, RoleManager>();
            Services.AddScoped<IUserManager, UserManager>();
         


            Services.AddScoped<IIdentityService, IdentityService>();
            Services.ConfigureOptions<IdentityServiceOptionsSetup>();
       
            Services
                .AddDbContext<SecurityContext>(options =>
                {
                    var cnnStr = configuration.GetConnectionString("Security");
                    options.UseSqlServer(cnnStr);
                })
                .AddIdentity<User, Role>()
                .AddEntityFrameworkStores<SecurityContext>()
                .AddDefaultTokenProviders();
          


        }
    }
}
