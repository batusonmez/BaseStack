using System;
using System.Collections.Generic;
using System.Text;

namespace Indexer
{
    /// <summary>
    /// Document indexing interface
    /// </summary>
  public  interface IIndexer
    {
        /// <summary>
        /// Save index document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="indexName"></param>
        /// <param name="id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        string Index<T>(string indexName, string id, T document) where T : class;

        /// <summary>
        /// seach indexed documents
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="index"></param>
        /// <param name="query"></param>
        /// <returns></returns>
        IndexResult<T> Search<T>(string index, string query) where T : class;

    }
}
