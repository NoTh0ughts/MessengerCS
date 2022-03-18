using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Messenger.DB.entity;
using Messenger.Host.Extensions;
using Messenger.Host.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;

namespace Messenger
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddNewtonsoftJson(x =>  x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            
            services.AddEntityFrameworkMySql()
                .AddDbContext<MessengerContext>()
                .AddUnitOfWork<MessengerContext>()
                .AddCustomRepository<Bot, GenericRepository<Bot>>()
                .AddCustomRepository<Call, GenericRepository<Call>>()
                .AddCustomRepository<Command, GenericRepository<Command>>()
                .AddCustomRepository<Content, GenericRepository<Content>>()
                .AddCustomRepository<Dialog, GenericRepository<Dialog>>()
                .AddCustomRepository<User, GenericRepository<User>>()
                .AddCustomRepository<Message, GenericRepository<Message>>()
                .AddCustomRepository<MessageSticker, GenericRepository<MessageSticker>>()
                .AddCustomRepository<Sticker, GenericRepository<Sticker>>()
                .AddCustomRepository<UserDialog, GenericRepository<UserDialog>>()
                .AddCustomRepository<Value, GenericRepository<Value>>();

            services.AddMediatR(this.GetType().Assembly);
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Messenger", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Messenger v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
