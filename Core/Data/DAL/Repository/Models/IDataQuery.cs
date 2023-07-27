using System.Linq.Expressions;

namespace Repository.Models
{
    public interface IDataQuery<T> where T : class
    {
        int Page { get; set; }
        int PageSize { get; set; }
        Expression<Func<T, bool>>? Filter { get; set; } 
        Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        string IncludeProperties { get; set; }
    }
}
