using Dispatcher;
using Grpc.Core;
using Index.API.Protos;
using Index.Application.Queries.ListForKeys;

namespace Index.API.Services
{
    public class IndexerService : Indexer.IndexerBase
    {
        private readonly ILogger<IndexerService> _logger;
        private readonly IDispatcher dispatcher;

        public IndexerService(ILogger<IndexerService> logger,
            IDispatcher dispatcher)
        {
            _logger = logger;
            this.dispatcher = dispatcher;
        }


        public override async Task<QuickSearchResponse> QuickSearch(QuickSearchRequest request, ServerCallContext context)
        {
            QuickKeywordSearchResponse? result =await   dispatcher.Send<QuickKeywordSearchResponse>(new QuickKeywordSearchRequest()
            {
                IndexName = request.IndexName,
                Limit = request.Limit,
                Query=request.Query
            });
            QuickSearchResponse rpcResponse = new();
            rpcResponse.Result.AddRange(result);
            return rpcResponse;
            
        }
       
    }
}