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
        /// Index Document
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="document"></param>
        void Index<T>(T document) where T : class;
        /// <summary>
        /// Seach Index 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        IEnumerable<T> Search<T>(string query) where T : class;

    }
}
