using AutoMapper;
using MediatR;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Repository;
using Repository.Models;
using System.Linq.Expressions;

namespace Northwind.Application.Queries.GenericQueries
{
    /// <summary>
    /// Base handler for Query lists
    /// </summary>
    /// <typeparam name="T">DTO object</typeparam>
    /// <typeparam name="E">Entity object</typeparam>
    public  class QueryHandler<T, E> : IRequestHandler<Query<T>, QueryResponse<T>> where T : class where E : class
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
                IDataQuery<E> queryFilter = BuildQuery(request);
                IPagedData<E> query = repository.GetPaged(queryFilter);
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

        public virtual IDataQuery<E> BuildQuery(Query<T> request)
        {
            return new DataQuery<E>()
            {
                Page = request.Page,
                PageSize = request.PageSize,
                IncludeProperties=includeProperties,
                Filter= BuildFilter(request)
            };
        }

        public virtual  Expression<Func<E, bool>>? BuildFilter(Query<T> request)
        {
            return null;
        }

    }
}
