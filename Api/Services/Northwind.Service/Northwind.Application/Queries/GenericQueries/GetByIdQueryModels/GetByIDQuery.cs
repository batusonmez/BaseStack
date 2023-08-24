using MediatR;
using MediatRDispatcher.Query;

namespace Northwind.Application.Queries.GenericQueries.GetByIdQueryModels
{
    public class GetByIDQuery<T> : IRequest<T?> where T : class
    {
        public object? ID { get; set; }
        public GetByIDQuery()
        {


        }


    }
}
