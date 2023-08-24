using Northwind.Application.Models.DTO.Types;
using Northwind.Application.Queries.GenericQueries.ListQueryModels;

namespace Northwind.Application.Queries.GenericQueries.Extansions.IndexQuery
{
    internal static class QueryToIndexSearch
    {
        public static IndexQueryParameters? ToIndexQuery<T>(this ListQuery<T> query) where T : class
        {
            if (query == null)
            {
                return null;
            }
            if (string.IsNullOrEmpty(query.QuickSearchKeyword))
            {
                return null;
            }            
  
            IDTO? mock = Activator.CreateInstance(typeof(T)) as IDTO;
            if (mock == null)
            {
                return null;
            }
            if (!mock.IndexEnabled ||  mock.IndexKey==null)
            {
                return null;
            }
            IndexQueryParameters result = new();
            result.Limit = query.PageSize;            
            result.IndexName = mock.IndexKey.ToString();
            result.Keyword = query.QuickSearchKeyword;

            return result;
        }
    }
}
