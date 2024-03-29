﻿using Application.Common;
using Application.Functions.Commands.UserCommands;
using FluentValidation;
using Infrastructure.Validators;

namespace WebAPI.Installers
{
    public class ValidationInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //services.AddScoped<IValidator<Comand>, Validator>(); scheme
            services.AddScoped<IValidator<RegisterUserCommand>, RegisterUserCommandValidator>();
            services.AddScoped<IValidator<AssignSteamIdToUserCommand>, AssignSteamIdToUserCommandValidator>();


            services.AddScoped<IValidator<FriendsListQueryParams>, FriendsListQueryParamsValidator>();


            services.AddScoped<IValidator<UpdateAccountDetailsCommand>, UpdateAccountDetailsCommandValidator>();
        }
    }
}
