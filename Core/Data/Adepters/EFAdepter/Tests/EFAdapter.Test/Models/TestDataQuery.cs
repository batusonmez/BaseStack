using Repository.Models;
using System.Linq.Expressions;

namespace EFAdapter.Test.Models
{
    public class TestDataQuery<T> : IDataQuery<T> where T : class
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public Expression<Func<T, bool>>? Filter { get; set; }
        public Func<IQueryable<T>, IOrderedQueryable<T>>? OrderBy { get; set; }
        public string IncludeProperties { get; set; } = "";
        public IEnumerable<object>? KeyFilter { get; set ; }
    }
}
