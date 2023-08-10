
using EFAdapter.Models;
using Microsoft.EntityFrameworkCore;
using Repository;
using Repository.Models;
using System.Linq;
using System.Linq.Expressions;
using EFAdapter.Extensions;
namespace EFAdapter
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly IUOW uow;

        private readonly DbSet<T> dbSet;

        private DbContext context
        {
            get
            {
                return (DbContext)uow.Context;
            }
        }

        public void Delete(object id)
        {
            T? entityToDelete = GetByID(id);
            if (entityToDelete != null)
            {
                dbSet.Remove(entityToDelete);
            }
        }


        private IQueryable<T> GetInternal(Expression<Func<T, bool>>? filter = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        { 
            return dbSet.AddWhere(filter).AddInclude(includeProperties).AddOrderBy(orderBy);             
        }

        public IEnumerable<T> Get(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, string includeProperties = "")
        {
            return GetInternal(filter, orderBy, includeProperties).ToList();
        }

        public T? GetByID(object id)
        {
            return dbSet.Find(id);
        }

        public void Insert(T entity)
        {
           dbSet.Add(entity);
        }
        public void Insert(IEnumerable<T> entities)
        {
            dbSet.AddRange(entities);
        }

        public void Update(T entityToUpdate)
        {
            dbSet.Attach(entityToUpdate);
            context.Entry(entityToUpdate).State = EntityState.Modified;
        }

        public IPagedData<T> GetPaged(IDataQuery<T> queryModel)
        {
            var query = GetInternal(queryModel.Filter,queryModel.OrderBy,queryModel.IncludeProperties);

            var result = new PagedData<T>()
            {
                Total=query.Count(),
                Data=query.Skip((Math.Max(queryModel.Page -1,0))*queryModel.PageSize).Take(queryModel.PageSize).ToList()
            };
            return result;
        }
         
        public EFRepository(IUOW uow)
        {
            this.uow = uow;
            dbSet = context.Set<T>(); 
        }

    }
}