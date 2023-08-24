
using MediatRDispatcher.Query;

namespace Northwind.Application.Queries.GenericQueries.ListQueryModels
{
    public class ListQueryResponse<T> : BaseQueryResult<T> where T : class
    {


        public ListQueryResponse(IEnumerable<T> data) : base(data)
        {

        }

    }
}
