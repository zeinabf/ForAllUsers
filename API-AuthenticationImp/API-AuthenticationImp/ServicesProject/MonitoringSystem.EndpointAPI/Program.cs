
using Carter;
using Microsoft.AspNetCore.Authorization;


namespace MonitoringSystem.EndpointAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
                 
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddServices(builder.Configuration,builder.Environment);
         

            var app = builder.Build();
       
          //  app.MapGet("/weatherforecast", () =>
          //  {

          //      var forecast = Enumerable.Range(1, 5).Select(index =>
          //          new WeatherForecast
          //          (
          //              DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
          //              Random.Shared.Next(-20, 55),
          //              summaries[Random.Shared.Next(summaries.Length)]
          //          ))
          //          .ToArray();
          //      return forecast;
          //  })
          //.WithName("GetWeatherForecast")
          //.WithOpenApi();


            app.UseSwagger();
            app.UseSwaggerUI();
            
            app.UseCors();
            
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapCarter();
     
             


            app.Run();

     


        }
    }
}
