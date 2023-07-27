using Repository.Models;
using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        IPagedData<T> GetPaged(IDataQuery<T> queryModel);

        T? GetByID(object id);

        void Insert(T entity);
        void Insert(IEnumerable<T> entities);
        void Delete(object id);
        void Update(T entityToUpdate);
         
    }
}