using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Repository

{
    /// <summary>
    /// Entity framework repository
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EFRepository<T> : IRepository<T> where T : class
    {
        /// <summary>
        /// Primary data source
        /// </summary>
        private readonly DbContext context;
        public EFRepository(DbContext dbContext)
        {
            context = dbContext;
        }

        /// <summary>
        /// Data set for this repository
        /// </summary>
        DbSet<T> Set
        {
            get
            {
                return context.Set<T>();
            }
        }

        /// <summary>
        /// Get by unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(object[] id)
        {
            return Set.Find(id);
        }

        /// <summary>
        /// Get whole data set
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> List()
        {
            return Set.AsNoTracking().AsEnumerable();
        }


        /// <summary>
        /// Get a filtered data set
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        public virtual IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return Set.AsNoTracking()
                   .Where(predicate)
                   .AsEnumerable();
        }

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"></param>
        public void Insert(T entity)
        {
            Set.Add(entity);
        }

        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <param name="entity"></param>
        public void Update(T entity)
        {
            context.Entry(entity).State = EntityState.Modified;

        }


        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        public void Delete(T entity)
        {
            context.Set<T>().Remove(entity);
        }
    }
}
