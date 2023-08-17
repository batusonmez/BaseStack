
using MediatRDispatcher.Query;

namespace Northwind.Application.Queries.GenericQueries
{
    public class QueryResponse<T> : BaseQueryResult<T> where T:class 
    {


        public QueryResponse(IEnumerable<T> data )  : base(data )
        {

        }

    }
}
