using MediatRDispatcher.Query;
using Northwind.Application.Models.Filters;

namespace Northwind.Application.Queries.GenericQueries.ListQueryModels
{
    public class ListQuery<T> : BaseQuery<ListQueryResponse<T>> where T : class
    {
        public string? QuickSearchKeyword { get; set; }
        public ListQuery()
        {


        }


    }
}
