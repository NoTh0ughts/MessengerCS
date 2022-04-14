using Data.Entity;
using Data.HostExtensions;
using Data.Repos;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace Auth.Service
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddEntityFrameworkMySql()
                .AddDbContext<MessengerContext>()
                .AddUnitOfWork<MessengerContext>()
                .AddCustomRepository<User, GenericRepository<User>>();

            services.AddApiStuff(typeof(Startup));
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureStuff(env);
        }
    }
}
