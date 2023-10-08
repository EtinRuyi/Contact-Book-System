using ContactBookAPI.Core.Interfaces;
using ContactBookAPI.Core.Repositories;
using ContactBookAPI.Data;
using Microsoft.EntityFrameworkCore;

namespace ContactBookAPI.Extension
{
    public static class DbContextRegistryExtension
    {
        public static void AddDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ContactBookAPIDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("Connection"));
            });

            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<ICrudRepository, CrudRepository>();
        }
    }
}
