using Dispatcher;
using MassTransit;
using MediatR;
using Microsoft.AspNetCore.Http;
using Northwind.Application.Models.DTO.Types;
using Northwind.Application.Queries.GenericQueries;
using Repository.Models;

namespace Northwind.Application.Interceptors
{
    /// <summary>
    /// Set paging header parameters
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    /// <typeparam name="TResponse"></typeparam>
    public class PagingInterceptor<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private HttpContext HttpContext { get; set; }
        public PagingInterceptor(IHttpContextAccessor httpContextAccessor)
        {
            
            HttpContext = httpContextAccessor.HttpContext;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            TResponse? originalResponse = await next();

            TResponse modifiedResponse = ModifyResponse(originalResponse);
            
            return modifiedResponse;
        }

        private TResponse ModifyResponse(TResponse originalResponse)
        {
          
            if(originalResponse is IQueryResult)
            {
                IQueryResult queryResult = (IQueryResult)originalResponse;
                setHeader("X-Total-Count", queryResult.Total.ToString());
                setHeader("X-Page-Size", queryResult.PageSize.ToString()); 
                setHeader("X-Current-Page", queryResult.Page.ToString());
                setHeader("X-Total-Pages", queryResult.TotalPages.ToString());

            }
            return originalResponse;
        }

        private void setHeader(string key,string value)
        {
            HttpContext.Response.Headers.Add(key, value);
        }

    }  
}
