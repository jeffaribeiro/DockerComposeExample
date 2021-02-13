using Microsoft.Extensions.DependencyInjection;
using DockerComposeExample.Application.Interfaces;
using DockerComposeExample.Application.Services;

namespace DockerComposeExample.IoC.Extensions
{
    internal static class ApplicationServiceCollection
    {
        internal static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IPagamentoAppService, PagamentoAppService>();

            return services;
        }
    }
}
