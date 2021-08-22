using System;
using System.Collections.Generic;
using System.Text;

namespace Indexer
{
    /// <summary>
    /// Index data result summary
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class IndexReult<T> where T:class
    {
        /// <summary>
        /// List of pagineted data 
        /// </summary>
        public IEnumerable<T> Data { get; set; }

        /// <summary>
        /// Count of total data for page count
        /// </summary>
        public int Count { get; set; }


        public IndexReult()
        {
                
        }

    }
}
