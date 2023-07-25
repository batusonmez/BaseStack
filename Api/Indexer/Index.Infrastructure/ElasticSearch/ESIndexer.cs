using Elasticsearch.Net;
using Index.Application.Common;
using Index.Application.Models;
using Index.Application.Queries.ListForKeys;
using Nest;
 
namespace Index.Infrastructure.ElasticSearch
{
    public class ESIndexer : IIndexer
    {
        private readonly ElasticClient client;

        public ESIndexer(ElasticClient client)
        {
            this.client = client;


        }

        public async Task<bool> Index(string indexName, string id, object data)
        {
            await InitIndex(indexName);
            var resp = client.LowLevel.Index<StringResponse>(indexName, id, data.ToString());

            return resp.Success;
        }


        public async Task InitIndex(string indexName)
        {
            var resp = await client.Indices.ExistsAsync(indexName);
            if (!resp.Exists)
            {
                var indexResp = await client.Indices.CreateAsync(indexName);
                IndexException.ThrowIf(indexResp != null && !indexResp.IsValid, $"Unable to create index '${indexName}'  EXP: {indexResp?.ServerError?.Error?.Reason}");
            }
        }

        public IEnumerable<string> QuickKeywordSearch(QuickKeywordSearchRequest query)
        {
            List<string> result = new();
            var response = client.Search<object>(s => s
                 .Index(query.IndexName)
                 .Size(query.Limit)
                .Source(sf => sf
                .Includes(f => f.Field("Id")))
                .Query(q => q.MultiMatch(d => {
                    d.Query(query.Query);
                    //d.Type(TextQueryType.PhrasePrefix);                                        
                    return d;                     
                    
                    })) 
                 );
            if (response.IsValid)
            {
                result = response.Hits.Select(d => d.Id.ToString()).ToList();

            }
            return result;

        }

    }
}
