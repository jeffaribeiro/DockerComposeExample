using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Repository.Base;
using DockerComposeExample.Repository.Context;

namespace DockerComposeExample.Repository.EFCore
{
    public class TrocoItemEFCoreRepository : EFCoreRepositoryBase<TrocoItem>, ITrocoItemEFCoreRepository
    {
        public TrocoItemEFCoreRepository(DockerComposeExampleDbContext db) : base(db)
        {
        }
    }
}
