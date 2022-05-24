using System;
using System.IO;
using System.Reflection;
using BusinessLogic.Response;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using MediatR;

namespace BusinessLogic.Extensions
{
    public static class HostExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            return services.AddSwaggerGen(c =>
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
        }

        public static IServiceCollection AddResponceFactory(this IServiceCollection services)
        {
            return services.AddTransient(typeof(ResponseFactory<>));
        }

        public static IServiceCollection AddApiStuff(this IServiceCollection services, Type startupType)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x =>
                    x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
        
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSwagger();
            services.AddResponceFactory();
        
            return services;
        }

        public static IApplicationBuilder ConfigureStuff(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", $"{Assembly.GetCallingAssembly().GetName().Name} v1"));
            }
            
            app.UseRouting();
            app.UseCors(builder => builder
                .AllowAnyMethod()
                .AllowAnyHeader()
                .SetIsOriginAllowed(hostName => true)
                .AllowCredentials()
            );
            
            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

            return app;
        }
    }
}