using Elasticsearch.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
namespace Indexer
{
    /// <summary>
    /// Elastic Search Index Implamentation
    /// </summary>
    public class ElasticSearchIndexer : IIndexer
    {
        private readonly ElasticLowLevelClient client;

        public ElasticSearchIndexer(ElasticLowLevelClient client)
        {
            this.client = client;
        }


        /// <summary>
        /// Save index document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        public string Index<T>(string indexName, string id, T document) where T : class
        {
            var response= client.Index<StringResponse>(indexName, id, PostData.Serializable<T>(document));
            if (!response.Success)
            {
                throw new Exception(response.DebugInformation);
            }
            return response.Body;

        }

        /// <summary>
        /// seach indexed documents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        public IndexReult<T> Search<T>(string index, string query) where T : class
        {
            var result = new IndexReult<T>();

            var searchResponse = client.Search<StringResponse>(index, query);
            var token = JToken.Parse(searchResponse.Body);

             result.Data= token
                    .SelectTokens("hits.hits[*]._source")
                    .Select(t => t.ToObject<T>())
                    .ToList();

            result.Count = token.SelectToken("hits.total.value").ToObject<int>();

            return result;
        }

  
    }
}
