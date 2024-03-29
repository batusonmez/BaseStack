﻿using AutoMapper;
using MediatR;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Northwind.Application.Queries.GenericQueries.Extansions.IndexQuery;
using Northwind.Application.Services.Index;
using Repository;
using Repository.Models;
using System.Linq.Expressions;

namespace Northwind.Application.Queries.GenericQueries.ListQueryModels
{
    /// <summary>
    /// Base handler for Query lists
    /// </summary>
    /// <typeparam name="T">DTO object</typeparam>
    /// <typeparam name="E">Entity object</typeparam>
    public class ListQueryHandler<T, E> : IRequestHandler<ListQuery<T>, ListQueryResponse<T>> where T : class where E : class
    {
        private readonly IMapper mapper;
        private readonly IRepository<E> repository;
        private readonly IIndexService indexService;
        private string includeProperties;
        public ListQueryHandler(IMapper mapper,
            IRepository<E> repository,
            IIndexService indexService,
           string includeProperties = ""
            )
        {
            this.includeProperties = includeProperties;
            this.mapper = mapper;
            this.repository = repository;
            this.indexService = indexService;
        }


        public virtual Task<ListQueryResponse<T>> Handle(ListQuery<T> request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                IDataQuery<E> queryFilter = await BuildQuery(request);
                IPagedData<E> query = repository.GetPaged(queryFilter);
                IEnumerable<T> data = query.Select(d => mapper.Map<T>(d));
                ListQueryResponse<T> resp = new ListQueryResponse<T>(data)
                {
                    Total = query.Total,
                    Page = request.Page,
                    PageSize = request.PageSize,
                    TotalPages = request.PageSize > 0 ? query.Total / request.PageSize + 1 : 0
                };

                return resp;
            });
        }

        public virtual async Task<IDataQuery<E>> BuildQuery(ListQuery<T> request)
        {
            IEnumerable<string>? indexSearchResult = await BuildKeywordQuery(request);
            Expression<Func<E, bool>>? filter = BuildFilter(request, indexSearchResult);

            return new DataQuery<E>()
            {
                Page = request.Page,
                PageSize = request.PageSize,
                IncludeProperties = includeProperties,
                Filter = filter
            };
        }

        public virtual Expression<Func<E, bool>>? BuildFilter(ListQuery<T> request, IEnumerable<string>? indexSearchResult)
        {

            return null;
        }

        public virtual async Task<IEnumerable<string>?> BuildKeywordQuery(ListQuery<T> request)
        {
            IndexQueryParameters? indexRequest = request.ToIndexQuery();
            if (indexRequest != null)
            {
                return await indexService.SearchKeyword(indexRequest.IndexName, indexRequest.Keyword, indexRequest.Limit);
            }


            return null;
        }
    }
}
