using Microsoft.EntityFrameworkCore;
using Repository;

namespace EFAdapter
{
    public class EFUnitOfWork : IUOW
    {
        private readonly DbContext context;

        public object Context => context;
        private bool disposed;
        public async Task Save()
        {
           await context.SaveChangesAsync();
        }

        public void Dispose()
        {
            if (disposed)
            {
                return;
            }
            
            context.Dispose();
            disposed = true;
        }

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }
    }
}
