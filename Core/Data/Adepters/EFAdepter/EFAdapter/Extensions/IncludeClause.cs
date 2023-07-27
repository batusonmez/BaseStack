using Microsoft.EntityFrameworkCore;

namespace EFAdapter.Extensions
{
    internal static class IncludeClause
    {
        public static IQueryable<T> AddInclude<T>(this IQueryable<T> source, string includeProperties) where T : class
        {
            if (string.IsNullOrEmpty(includeProperties))
            {
                return source;
            }

            foreach (var includeProperty in includeProperties.Split
                          (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                source = source.Include(includeProperty);
            }
            return source;
        }
    }
}
