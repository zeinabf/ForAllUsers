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

namespace AuthenticationService._04_AuthenticationService_Presentation
{

    public class UserEndPoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var users = app.MapGroup("users").WithTags("Users");

            users.MapPost("/register", RegisterAsync);
            users.MapPost("/login", LoginAsync);
            users.MapPost("/UserRoleAssigned", UserRoleAssignedAsync);

        }

        async Task<IResult> RegisterAsync(
            [FromBody] UserForRegistrationDto registerDto,
            [FromServices] IUserManager _manager
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

        async Task<IResult> RegisterRoleAsync(
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

        async Task<IResult> UserRoleAssignedAsync(
                [FromBody] UserRoleRegistrationDto userRoleregisterDto,
                [FromServices] IUserManager _manager  )
        {
            var result = await _manager.RegisterRoleUserAsync(userRoleregisterDto);

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

        async Task<IResult> LoginAsync(
            [FromBody] UserForLoginDto loginDto,
            [FromServices] IUserManager _manager
        )
        {
            var result = await _manager.LoginAsync(loginDto);

            if (result.IsSuccess)
                return Results.Ok(result.Value);

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
