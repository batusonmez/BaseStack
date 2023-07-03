
using MediatRDispatcher.Query;
using Northwind.Application.Models.DTO.Types;

namespace Northwind.Application.Queries.GenericQueries
{
    public class QueryResponse<T> : BaseQueryResult<T> where T:class 
    {


        public QueryResponse(IEnumerable<T> data, int total)  : base(data, total)
        {

        }

    }
}
