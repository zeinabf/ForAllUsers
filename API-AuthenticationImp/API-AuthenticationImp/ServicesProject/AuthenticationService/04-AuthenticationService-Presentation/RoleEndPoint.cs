using AuthenticationService._02_AuthenticationService_ApplicationLayer.Contracts;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.User.Dto;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using FluentValidation.Results;
 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

using Carter;
using Microsoft.AspNetCore.Builder;
 using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Http;
using AuthenticationService._02_AuthenticationService_ApplicationLayer.Role.Dto;
using System.Security.Claims;

namespace AuthenticationService._04_AuthenticationService_Presentation
{

    public class RoleEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var roles = app.MapGroup("Roles").WithTags("Roles");

            roles.MapPost("/register", RegisterAsync);
            roles.MapPost("/registerClaim", RegisterClaimAsync);

            roles.MapGet("/claims/{roleid}", GetClaimsAsync);                     
           
        }

        async Task<IResult> RegisterAsync(
            [FromBody] RoleForRegistrationDto registerDto,
            [FromServices] IRoleManager _manager
        )
        {
            var result = await _manager.RegisterAsync(registerDto);

            if (result.IsSuccess)
                return Results.Ok();

            if (!IsValidJson(result.Error))
                return Results.BadRequest(result.Error);

            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);

            if (validationResult is not null and ValidationResult)
                return Results.ValidationProblem(validationResult.ToDictionary());
            else
                return Results.BadRequest("Unknown error is occurred");
        }
        async Task<IResult> RegisterClaimAsync(
           [FromBody] RoleClaimsDto registerDto,
           [FromServices] IRoleManager _manager
       )
        {
            var result = await _manager.RegisterRoleClaimsAsync(registerDto);

            if (result.IsSuccess)
                return Results.Ok();

            if (!IsValidJson(result.Error))
                return Results.BadRequest(result.Error);

            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);

            if (validationResult is not null and ValidationResult)
                return Results.ValidationProblem(validationResult.ToDictionary());
            else
                return Results.BadRequest("Unknown error is occurred");
        }

        async Task<IResult> GetClaimsAsync(
           [FromRoute] string roleid,
           [FromServices] IRoleManager _manager
         )

        {
            var result = await _manager.GetRoleClaims(Int32.Parse(roleid));
            IList<string> claims = new List<string>();
            if (result.IsSuccess)
            {
                foreach (var claim in result.Value)
                {
                    if (claim.Type.Contains("webpage"))
                        claims.Add(claim.Value);
                    
                }
                return Results.Ok(claims);
            }

            if (!IsValidJson(result.Error))
                return Results.BadRequest(result.Error);

            var validationResult = JsonSerializer.Deserialize<ValidationResult>(result.Error);

            if (validationResult is not null and ValidationResult)
                return Results.ValidationProblem(validationResult.ToDictionary());
            else
                return Results.BadRequest("Unknown error is occurred");
        }
        bool IsValidJson(string json)
        {
            try
            {
                JsonDocument.Parse(json);
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
