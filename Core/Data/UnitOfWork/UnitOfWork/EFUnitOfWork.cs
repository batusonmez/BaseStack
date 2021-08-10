using Microsoft.EntityFrameworkCore;
using Repository;
using System;


namespace UOW
{

    /// <summary>
    /// Unit Of Work Implementation For Entity Framework
    /// </summary>
    public class EFUnitOfWork
    {
        private readonly DbContext context;

        public EFUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// Create instance of an EF Repository 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateRepository<T>() where T : class
        {
            return new EFRepository<T>(context);
        }


    }
}
