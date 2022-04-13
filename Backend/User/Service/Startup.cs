using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace User.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddApiStuff(typeof(Startup));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureStuff(env);
        }
    }
}
