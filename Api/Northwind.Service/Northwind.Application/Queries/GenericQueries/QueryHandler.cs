using AutoMapper;
using MediatR;
using Repository;
using Repository.Models;

namespace Northwind.Application.Queries.GenericQueries
{
    public class QueryHandler<T, E> : IRequestHandler<Query<T>, QueryResponse<T>> where T : class where E : class
    {
        private readonly IMapper mapper;
        private readonly IRepository<E> repository;
        private string includeProperties;
        public QueryHandler(IMapper mapper,
            IRepository<E> repository,
           string includeProperties = ""
            )
        {
            this.includeProperties = includeProperties;
            this.mapper = mapper;
            this.repository = repository;
        }
        public virtual Task<QueryResponse<T>> Handle(Query<T> request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
            {
                IPagedData<E> query = repository.GetPaged(request.Page, request.PageSize, includeProperties: includeProperties);
                IEnumerable<T> data = query.Select(d => mapper.Map<T>(d));
                QueryResponse<T> resp = new QueryResponse<T>(data)
                {
                    Total = query.Total,
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalPages = request.PageSize > 0 ? (query.Total / request.PageSize) + 1 : 0
                };

                return resp;
            });
        }


    }
}
