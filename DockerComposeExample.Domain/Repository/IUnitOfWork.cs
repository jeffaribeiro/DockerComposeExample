using System;

namespace DockerComposeExample.Domain.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        void Commit();
    }
}
