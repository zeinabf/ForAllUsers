using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.Presentation.Authentication
{
    public class APIKeyAuthenticationEndpointFilter : IEndpointFilter
    {
        private const string APIKeyHeader = "X-Api-Key";
        private readonly IConfiguration _configuration;

        public APIKeyAuthenticationEndpointFilter(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            string? apikey = context.HttpContext.Request.Headers[APIKeyHeader];
             if (!ISAPIKeyIsvalid(apikey))
            {
                return Results.Unauthorized();
            }
            return await next(context);
        }
        private bool ISAPIKeyIsvalid(string? APIKey)
        {
            if (string.IsNullOrWhiteSpace(APIKey))
                return false;

           // string actualAPIKEy = "1814D8A8BA50438290A04F22E8A3CC5D";
           string actualAPIKEy = _configuration.GetValue<string>("APIKEY")!;


            return actualAPIKEy == APIKey;
             
             
        }



    }
}
