using System.Linq.Expressions;

namespace EFAdapter.Extensions
{
    internal static class WhereClause
    {
        public static IQueryable<T> AddWhere<T>(this IQueryable<T> source, Expression<Func<T, bool>>? filter) where T : class
        {
            if (filter != null)
            {
              return source.Where(filter);
            }
            return source;
        }
    }
}
