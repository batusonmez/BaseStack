using Repository.Models;

namespace EFAdapter.Test.Models
{
    public class PagedData<T> : IPagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }
        public int Total { get;set; }

        public PagedData()
        {
            Data = new List<T>();   
        }

    }
}
