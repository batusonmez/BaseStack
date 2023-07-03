using MediatRDispatcher.Query;

namespace Northwind.Application.Queries.GenericQueries
{
    public class Query<T> : BaseQuery<QueryResponse<T>> where T : class
    {
    }
}
