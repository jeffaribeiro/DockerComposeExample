using Dapper.FluentMap.Dommel.Mapping;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Repository.Map.Dapper
{
    public class PagamentoMapping : DommelEntityMap<Pagamento>
    {
        public PagamentoMapping()
        {
            ToTable("Pagamento");
            Map(p => p.Id).IsKey().IsIdentity();
        }
    }
}
