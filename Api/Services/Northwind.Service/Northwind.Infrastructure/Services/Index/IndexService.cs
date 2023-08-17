using Grpc.Net.Client;
using Northwind.Application.Services.Index;
using Northwind.Infrastructure.Protos;
using Microsoft.Extensions.Options;
using Northwind.Application.Models.Configuration;

namespace Northwind.Infrastructure.Services.Index
{
    public class IndexService : IIndexService
    {

        private Indexer.IndexerClient client;
        public IndexService(IOptions<IndexConfig> indexConfig)
        {
            GrpcChannel channel = GrpcChannel.ForAddress(indexConfig.Value.IndexAPI);
            client = new Indexer.IndexerClient(channel);
        }

        public async Task<IEnumerable<string>> SearchKeyword(string index, string keyword, int limit)
        {
            QuickSearchRequest data = new() { IndexName = index, Query = keyword, Limit = limit };

            QuickSearchResponse response = await client.QuickSearchAsync(data);
            return response.Result.AsEnumerable();

        }
    }
}
