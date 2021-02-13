using Microsoft.Extensions.DependencyInjection;
using DockerComposeExample.IoC.Extensions;

namespace DockerComposeExample.IoC
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddRepositoryServices();
            services.AddDomainServices();
            services.AddApplicationServices();
        }
    }
}
