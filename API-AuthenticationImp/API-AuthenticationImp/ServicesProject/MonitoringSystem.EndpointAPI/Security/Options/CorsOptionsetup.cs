using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitoringSystem.EndpointAPI.Security.Option
{
    public class CorsOptionsetup : IConfigureOptions<CorsOptions>
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CorsOptionsetup(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void Configure(CorsOptions options)
        {
            options.AddDefaultPolicy(policy =>
            {
                if (_webHostEnvironment.IsDevelopment())
                {
                    policy
               .AllowAnyHeader()
               .AllowAnyMethod()
               .AllowAnyOrigin()
               
                    .WithExposedHeaders("X-TotalRecordCount");
                }
                else
                {
                    policy
                    .WithOrigins("http://localhost:4200/")
                    .WithMethods("GET", "POST", "PUT", "Delete")
                    .WithHeaders("Header Name1");
                }

            });
        }
    }
}
