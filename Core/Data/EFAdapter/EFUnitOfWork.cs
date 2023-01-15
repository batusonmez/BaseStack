using Microsoft.EntityFrameworkCore;
using Repository;
using System.Reflection.Metadata.Ecma335;

namespace EFAdapter
{
    public class EFUnitOfWork : IUOW
    {
        private readonly DbContext context;

        public object Context => context;

        public async Task Save()
        {
           await context.SaveChangesAsync();
        }
        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }
    }
}
