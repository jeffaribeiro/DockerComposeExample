using Microsoft.Extensions.DependencyInjection;
using DockerComposeExample.Domain.Interfaces;
using DockerComposeExample.Domain.Notificacoes;
using DockerComposeExample.Domain.Repository;
using DockerComposeExample.Domain.Services;
using DockerComposeExample.Repository.UoW;

namespace DockerComposeExample.IoC.Extensions
{
    internal static class DomainServiceCollection
    {
        internal static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<IPagamentoService, PagamentoService>();

            services.AddScoped<INotificador, Notificador>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
