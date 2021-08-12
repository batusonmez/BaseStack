using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Threading.Tasks;
using UnitOfWork;

namespace UOW
{

    /// <summary>
    /// Unit Of Work Implementation For Entity Framework
    /// </summary>
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void Dispose()
        {
            context.Dispose();
        }

        /// <summary>
        /// Create instance of an EF Repository 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> IUnitOfWork.CreateRepository<T>()
        {
            return new EFRepository<T>(context);
        }


        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        public Task<int> Commit()
        {
           return context.SaveChangesAsync();
        }
    }
}
