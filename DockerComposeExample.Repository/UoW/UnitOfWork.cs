using DockerComposeExample.Domain.Repository;
using DockerComposeExample.Repository.Context;

namespace DockerComposeExample.Repository.UoW
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly DockerComposeExampleDbContext _context;

        public UnitOfWork(DockerComposeExampleDbContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        private void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_context == null) return;

            _context.Dispose();
        }
    }
}
