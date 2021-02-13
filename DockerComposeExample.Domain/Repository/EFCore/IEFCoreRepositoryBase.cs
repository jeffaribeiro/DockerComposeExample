using System;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Base;

namespace DockerComposeExample.Domain.Repository.EFCore
{
    public interface IEFCoreRepositoryBase<TEntity> : IDisposable where TEntity : EntidadeBase
    {
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(Guid id);
    }
}
