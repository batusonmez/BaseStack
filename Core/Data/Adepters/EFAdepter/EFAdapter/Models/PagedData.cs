using Repository.Models;
using System.Collections;

namespace EFAdapter.Models
{
    public class PagedData<T> : IPagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get;set; }

        public PagedData()
        {
            Data = new List<T>();   
        }

        public IEnumerator<T> GetEnumerator()
        {
           return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return Data.GetEnumerator();
        }
    }
}
