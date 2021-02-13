using DockerComposeExample.Domain.Models;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Repository.Base;
using DockerComposeExample.Repository.Context;

namespace DockerComposeExample.Repository.EFCore
{
    public class PagamentoEFCoreRepository : EFCoreRepositoryBase<Pagamento>, IPagamentoEFCoreRepository
    {
        public PagamentoEFCoreRepository(DockerComposeExampleDbContext db) : base(db)
        {
        }
    }
}
