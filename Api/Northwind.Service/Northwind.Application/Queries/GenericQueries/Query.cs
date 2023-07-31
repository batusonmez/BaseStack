using MediatRDispatcher.Query;
using Northwind.Application.Models.Filters;

namespace Northwind.Application.Queries.GenericQueries
{
    public class Query<T> : BaseQuery<QueryResponse<T>> where T : class
    {
        public string? QuickSearchKeyword { get; set; }
        public Query()
        {

        
        }

        
    }
}
