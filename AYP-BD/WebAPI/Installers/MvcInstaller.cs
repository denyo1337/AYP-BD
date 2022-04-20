using FluentValidation.AspNetCore;

namespace WebAPI.Installers
{
    public class MvcInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            //here register installers from other proj refs

            services.AddControllers().AddFluentValidation();
        }
    }
}
