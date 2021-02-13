using Microsoft.Extensions.Configuration;
using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Repository.Base;

namespace DockerComposeExample.Repository.Dapper
{
    public class TrocoItemDapperRepository : DapperRepositoryBase<TrocoItem>, ITrocoItemDapperRepository
    {
        public TrocoItemDapperRepository(IConfiguration configuration) : base(configuration)
        {
        }
    }
}
