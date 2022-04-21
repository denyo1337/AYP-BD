using Application.Common;
using Domain.Models;
using IdGen;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application
{
    public static class Di
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {
            services.AddSingleton<IEntityGenerator, IdGeneratorWrapper>();

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

            //here register MiddleWare for error handling
            //passwordhashers, contextProvider, mediator, services etc.
            return services;
        }
    }
}
