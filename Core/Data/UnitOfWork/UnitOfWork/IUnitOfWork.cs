using Repository;
using System;
using System.Threading.Tasks;

namespace UnitOfWork
{
    /// <summary>
    /// Generic Unit of Work 
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        /// <summary>
        /// Get a generic repository
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        IRepository<T> CreateRepository<T>() where T : class;

        /// <summary>
        /// Save changes
        /// </summary>
        /// <returns></returns>
        Task<int> Commit();
    }
}
