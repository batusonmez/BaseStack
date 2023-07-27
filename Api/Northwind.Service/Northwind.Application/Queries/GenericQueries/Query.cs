using MediatRDispatcher.Query;
using Northwind.Application.Models.Filters;

namespace Northwind.Application.Queries.GenericQueries
{
    public class Query<T> : BaseQuery<QueryResponse<T>> where T : class
    {
        public IEnumerable<Filter>? Filters { get; set; }
    }
}
