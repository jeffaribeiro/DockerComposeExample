using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Base;

namespace DockerComposeExample.Domain.Repository.Dapper
{
    public interface IDapperRepositoryBase<TEntity> : IDisposable where TEntity : EntidadeBase
    {
        Task<TEntity> BuscarPorId(Guid id);
        Task<IEnumerable<TEntity>> BuscarTodos();
    }
}
