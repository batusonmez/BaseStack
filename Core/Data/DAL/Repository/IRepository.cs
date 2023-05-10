using System.Linq.Expressions;

namespace Repository
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> Get(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            string includeProperties = "");

        T? GetByID(object id);

        void Insert(T entity);
        void Delete(object id);
        void Update(T entityToUpdate);
         
    }
}