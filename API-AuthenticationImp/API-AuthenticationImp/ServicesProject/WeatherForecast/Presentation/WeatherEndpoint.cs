using Carter;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using WeatherForecast.ApplicationLayer.Contracts;
using WeatherForecast.Presentation.Authentication;

namespace WeatherForecast.Presentation
{
    public class WeatherEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            var connection = app.MapGroup("Weathers").WithTags("Weathers");
            connection.MapGet("/All", GetAnalyzes)
                .AddEndpointFilter<APIKeyAuthenticationEndpointFilter>();

        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]

        private IResult GetAnalyzes([FromServices] IweatherForecastmanager wmanager)

        {
            var queries = wmanager.getForecasts();
            if (queries.IsSuccess)
            {
                return Results.Ok(queries.Value);
            }
            else
                return Results.BadRequest(queries.Value);
 

        }

    }
}
