using Dapper.FluentMap.Dommel.Mapping;
using DockerComposeExample.Domain.Models;

namespace DockerComposeExample.Repository.Map.Dapper
{
    public class TrocoItemMapping : DommelEntityMap<TrocoItem>
    {
        public TrocoItemMapping()
        {
            ToTable("TrocoItem");
            Map(p => p.Id).IsKey().IsIdentity();
        }
    }
}
