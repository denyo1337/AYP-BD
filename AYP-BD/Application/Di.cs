using Application.Common;
using Application.Interfaces;
using Application.Services;
using Domain.Models;
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
            services.AddScoped<IUserContextService, UserContextService>();
            services.AddScoped<IHttpRequestHandler, RequestHandler>();
            services.AddScoped<IUsersFriendListService, UsersFriendListService>();
            services.AddScoped<IUsersServiceValidator, UsersServiceValidator>();

            //here register MiddleWare for error handling
            //passwordhashers, contextProvider, mediator, services etc.
            return services;
        }
    }
}
