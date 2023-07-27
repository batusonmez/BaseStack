namespace EFAdapter.Extensions
{
    internal static class OrderClause
    {
        public static IQueryable<T> AddOrderBy<T>(this IQueryable<T> source, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null) where T : class
        {
      
            if (orderBy != null)
            {
                return orderBy(source);
            }
            return source;
        }
    }
}
