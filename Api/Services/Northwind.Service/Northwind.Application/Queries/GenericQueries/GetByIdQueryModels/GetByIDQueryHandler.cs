using AutoMapper;
using MediatR;
using Northwind.Application.Models.Filters.DataQueryFilter;
using Northwind.Application.Queries.GenericQueries.Extansions.IndexQuery;
using Northwind.Application.Services.Index;
using Repository;
using Repository.Models;
using System.Linq.Expressions;

namespace Northwind.Application.Queries.GenericQueries.GetByIdQueryModels
{
    /// <summary>
    /// Base handler for Query lists
    /// </summary>
    /// <typeparam name="T">DTO object</typeparam>
    /// <typeparam name="E">Entity object</typeparam>
    public class GetByIDQueryHandler<T,E> : IRequestHandler<GetByIDQuery<T>, T> where T : class where E : class
    {
        private readonly IMapper mapper;
        private readonly IRepository<E> repository;
        private readonly IIndexService indexService;
        private string includeProperties;
        public GetByIDQueryHandler(IMapper mapper,
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
         

      
        public Task<T?> Handle(GetByIDQuery<T> request, CancellationToken cancellationToken)
        {
           return Task.Run(() =>
            {
                if (request.ID == null)
                {
                    return null;
                }

               E? data= repository.GetByID(request.ID);
                if(data == null)
                {
                    return null;
                }                    
                
               return mapper.Map<T>(data);
                
            });
            
        }
    }
}
