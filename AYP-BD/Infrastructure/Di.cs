using Domain.Data.Interfaces;
using Infrastructure.Data.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure
{
    public static class Di
    {
        public static IServiceCollection AddInfra(this IServiceCollection services)
        {
            //services.AddScoped<IRepository, Repository>() keep that scheme
            services.AddScoped<IUsersRepostiory, UsersRepository>();
            return services;
        }
    }
}
