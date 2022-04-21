using Application.Common;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
namespace Application
{
    public static class Di
    {
        public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
        {

            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddSingleton<IEntityGenerator, IdGeneratorWrapper>();
            //here register MiddleWare for error handling
            //passwordhashers, contextProvider, mediator, services etc.
            return services;
        }
    }
}
