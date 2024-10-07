using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeatherForecast.ApplicationLayer.Contracts;

namespace WeatherForecast.ApplicationLayer
{
    public class weatherForecastManager : IweatherForecastmanager
    {
       
        public Result<IEnumerable<WeatherForecast>> getForecasts()
        {
            var summaries = new[]
      {
                    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
                };
            var forecast = Enumerable.Range(1, 5).Select(index =>
                   new WeatherForecast
                   (
                       DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                       Random.Shared.Next(-20, 55),
                       summaries[Random.Shared.Next(summaries.Length)]
                   ))
                   .ToArray();
            return forecast;
            

        }
    }
}
