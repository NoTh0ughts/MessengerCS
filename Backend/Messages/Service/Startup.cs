using Data.Entity;
using Data.HostExtensions;
using Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

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

            services.AddApiStuff(typeof(Startup));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureStuff(env);
        }
    }
}
