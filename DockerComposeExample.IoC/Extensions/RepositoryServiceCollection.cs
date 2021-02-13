using Microsoft.Extensions.DependencyInjection;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Repository.Dapper;
using DockerComposeExample.Repository.EFCore;

namespace DockerComposeExample.IoC.Extensions
{
    internal static class RepositoryServiceCollection
    {
        internal static IServiceCollection AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IPagamentoDapperRepository, PagamentoDapperRepository>();
            services.AddScoped<ITrocoItemDapperRepository, TrocoItemDapperRepository>();
            services.AddScoped<IPagamentoEFCoreRepository, PagamentoEFCoreRepository>();
            services.AddScoped<ITrocoItemEFCoreRepository, TrocoItemEFCoreRepository>();

            return services;
        }
    }
}
