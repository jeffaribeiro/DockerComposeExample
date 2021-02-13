using Dapper.FluentMap;
using Dapper.FluentMap.Dommel;
using Dommel;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;
using DockerComposeExample.Domain.Base;
using DockerComposeExample.Domain.Repository.Dapper;
using DockerComposeExample.Repository.Map.Dapper;

namespace DockerComposeExample.Repository.Base
{
    public abstract class DapperRepositoryBase<TEntity> : IDapperRepositoryBase<TEntity> where TEntity : EntidadeBase
    {
        private readonly IConfiguration _configuration;
        protected readonly SqlConnection conn;

        public DapperRepositoryBase(IConfiguration configuration)
        {
            _configuration = configuration;
            if (FluentMapper.EntityMaps.IsEmpty)
            {
                FluentMapper.Initialize(c =>
                {
                    c.AddMap(new PagamentoMapping());
                    c.AddMap(new TrocoItemMapping());
                    c.ForDommel();
                });
            }
            conn = new SqlConnection(_configuration.GetConnectionString("DefaultConnection"));
        }

        public async virtual Task<IEnumerable<TEntity>> BuscarTodos() =>
            await conn.GetAllAsync<TEntity>();

        public async virtual Task<TEntity> BuscarPorId(Guid id) =>
            await conn.GetAsync<TEntity>(id);

        private bool _disposed = false;

        ~DapperRepositoryBase() =>
            Dispose();

        public void Dispose()
        {
            if (!_disposed)
            {
                conn.Close();
                conn.Dispose();
                _disposed = true;
            }
            GC.SuppressFinalize(this);
        }
    }
}
