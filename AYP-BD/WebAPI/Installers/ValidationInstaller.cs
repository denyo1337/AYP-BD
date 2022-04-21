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
        }
    }
}
