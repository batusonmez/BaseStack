using Repository.Models;
using System.Linq.Expressions;

namespace Northwind.Application.Models.Filters.DataQueryFilter
{
    public class DataQuery<T> : IDataQuery<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        public string IncludeProperties { get; set; } = "";
    }
}
