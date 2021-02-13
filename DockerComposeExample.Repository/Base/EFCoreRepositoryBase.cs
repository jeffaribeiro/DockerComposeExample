using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Base;
using DockerComposeExample.Domain.Repository.EFCore;
using DockerComposeExample.Repository.Context;

namespace DockerComposeExample.Repository.Base
{
    public abstract class EFCoreRepositoryBase<TEntity> : IEFCoreRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        protected readonly DockerComposeExampleDbContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected EFCoreRepositoryBase(DockerComposeExampleDbContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }

        public virtual async Task Adicionar(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await Task.CompletedTask;
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(DbSet.Find(id));
            await Task.CompletedTask;
        }

        public void Dispose()
        {
            Db?.Dispose();
        }
    }
}
