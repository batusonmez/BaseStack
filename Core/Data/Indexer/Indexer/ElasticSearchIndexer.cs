using Nest;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Indexer
{
    /// <summary>
    /// Elastic Search Index Implamentation
    /// </summary>
    public class ElasticSearchIndexer : IIndexer
    {
        private readonly ElasticClient client;

        public ElasticSearchIndexer(ElasticClient client)
        {
            this.client = client;
        }

        /// <summary>
        /// Save Index
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        public void Index<T>(T document) where T : class
        {
            client.IndexDocument<T>(document);
        }

        /// <summary>
        /// search index by string
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public IEnumerable<T> Search<T>(string query) where T : class
        {
            return client.Search<T>(s => s
    .Query(q => q
        .MatchAll()
    )
).Documents;


            //    return client.Search<T>(
            //s => s.Query(q => q.QueryString(d => d.Query(query)))).Documents;

             
        }
    }
}
