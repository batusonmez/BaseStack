using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    /// <summary>
    /// Generic Repository Template
    /// </summary>
    /// <typeparam name="T"></typeparam>
   public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Get by unique identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        T GetById(Object[] id);

        /// <summary>
        /// Get whole data set
        /// </summary>
        /// <returns></returns>
        IEnumerable<T> List();

        /// <summary>
        /// Get a filtered data set
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<T> List(System.Linq.Expressions.Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Create new entity
        /// </summary>
        /// <param name="entity"></param>
        void Insert(T entity);
            
        /// <summary>
        /// Update an existing entity
        /// </summary>
        /// <param name="entity"></param>
        void Update(T entity);

        /// <summary>
        /// Remove an entity
        /// </summary>
        /// <param name="entity"></param>
        void Delete(T entity);

    }
}
