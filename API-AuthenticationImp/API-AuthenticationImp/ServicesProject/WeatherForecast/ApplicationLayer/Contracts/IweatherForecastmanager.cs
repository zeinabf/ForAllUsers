using CSharpFunctionalExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherForecast.ApplicationLayer.Contracts
{
    public interface IweatherForecastmanager
    {
        Result<IEnumerable<WeatherForecast>> getForecasts();
    }
}
