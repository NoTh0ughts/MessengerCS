using System;
using System.IO;
using System.Reflection;
using BusinessLogic.Extensions;
using Data.Entity;
using Data.HostExtensions;
using Data.Repos;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Messages.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql()
                .AddDbContext<MessengerContext>()
                .AddUnitOfWork<MessengerContext>()
                .AddCustomRepository<Message, GenericRepository<Message>>()
                .AddCustomRepository<UserAccess, GenericRepository<UserAccess>>()
                .AddCustomRepository<Dialog, GenericRepository<Dialog>>();

            services.AddControllers()
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = Assembly.GetCallingAssembly().GetName().Name, Version = "v1"});
                var securitySchema = new OpenApiSecurityScheme
                {
                    Description =
                        "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\"",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                };

                c.AddSecurityDefinition("Bearer", securitySchema);

                var securityRequirement = new OpenApiSecurityRequirement
                {
                    {securitySchema, new[] {"Bearer"}}
                };

                c.AddSecurityRequirement(securityRequirement);

                var filePath = Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetCallingAssembly().GetName().Name}.xml");
                c.IncludeXmlComments(filePath);
            });
            services.AddResponceFactory();
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureStuff(env);
        }
    }
}
