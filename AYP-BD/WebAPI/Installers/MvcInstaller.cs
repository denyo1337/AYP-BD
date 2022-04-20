using Application;
using FluentValidation.AspNetCore;
using Infrastructure;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //here register installers from other proj refs
            services.AddInfra();
            services.AddApplicationLayer();
            services.AddControllers().AddFluentValidation();
        }
    }
}
