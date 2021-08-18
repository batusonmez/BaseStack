using Nest;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Indexer
{
    public class ElasticSearchIndexer : IIndexer
    {
        private readonly ElasticClient client;

        public ElasticSearchIndexer(ElasticClient client)
        {
            this.client = client;
        }

        public void Index<T>(T document) where T:class
        {
            client.IndexDocument<T>(document);
        }

        public IEnumerable<T> Search<T>(string query, int page = 1, int pageSize = 5) where T:class
        {
            IElasticClient t;

            t.Search<mdw>(
       s => s.Query(q => q.QueryString(d => d.Query(query)))
           .From((page - 1) * pageSize)
           .Size(pageSize).d

            return null;
        }
    }
}
