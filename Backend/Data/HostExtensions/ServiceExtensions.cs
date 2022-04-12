using Data.Repos;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Data.HostExtensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddUnitOfWork<TContext>(this IServiceCollection services)
            where TContext : DbContext
        {
            services.AddScoped<IRepositoryFactory, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork, UnitOfWork<TContext>>();
            services.AddScoped<IUnitOfWork<TContext>, UnitOfWork<TContext>>();

            return services;
        }

        public static IServiceCollection AddCustomRepository<TEntity, TRepo>(this IServiceCollection services)
            where TEntity : class
            where TRepo : class, IGenericRepository<TEntity>
        {
            services.AddScoped<IGenericRepository<TEntity>, TRepo>();

            return services;
        }
    }
}