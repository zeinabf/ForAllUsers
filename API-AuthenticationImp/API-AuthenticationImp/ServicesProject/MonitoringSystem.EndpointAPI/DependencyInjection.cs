using Carter;
using AuthenticationService._01_AuthenticationService_DomainLayer.Role;
using AuthenticationService._01_AuthenticationService_DomainLayer.User;
using AuthenticationService._03_AuthenticationService_Infrastructure.Persistence;
 
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
 
using MonitoringSystem.EndpointAPI.Security.Option;
 
using AuthenticationService;
using Microsoft.OpenApi.Models;
using AuthenticationService._03_AuthenticationService_Infrastructure.Services.Authentication.Options;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using WeatherForecast.ApplicationLayer.Contracts;
using WeatherForecast.ApplicationLayer;


namespace MonitoringSystem.EndpointAPI
{
    public static class DependencyInjection
    {
        public static void AddServices(this IServiceCollection Services,
                                            IConfiguration configuration,
                                            IWebHostEnvironment env)
        {
            // Add services to the container.
            Services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "Minimal API", Version = "v1" });
                opt.AddSecurityDefinition(
                    "Bearer",
                    new OpenApiSecurityScheme
                    {
                        In = ParameterLocation.Header,
                        Description = "Please enter token",
                        Name = "Authorization",
                        Type = SecuritySchemeType.Http,
                        BearerFormat = "JWT",
                        Scheme = "bearer"
                    }
                );

                opt.AddSecurityRequirement(
                    new OpenApiSecurityRequirement
                    {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] { }
                    }
                    }
                );
            });
            Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
            //Services.AddAuthentication("Bearer")
            //   .AddJwtBearer(options =>
            //   {
            //       options.TokenValidationParameters = new TokenValidationParameters
            //       {
            //           ValidateIssuer = true,
            //           ValidateAudience = true,
            //           ValidateLifetime = true,
            //           ValidateIssuerSigningKey = true,
            //           ValidIssuer = configuration["Jwt:Issuer"],
            //           ValidAudience = configuration["Jwt:Audience"],
            //           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
            //       };
            //   });
            Services.ConfigureOptions<JwtBearerOptionsSetup>();

            Services.AddCors();
            Services.ConfigureOptions<MonitoringSystem.EndpointAPI.Security.Option.CorsOptionsetup>();
          

           Services.AddSecurityServices(configuration, env);



            Services.AddAuthorization();

            Services.AddEndpointsApiExplorer();

            Services.AddCarter();
            Services.AddScoped<IweatherForecastmanager, weatherForecastManager>();


        }
    }
    }
  
