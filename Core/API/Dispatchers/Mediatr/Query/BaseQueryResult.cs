using Dispatcher;
using System.Collections;

namespace MediatRDispatcher.Query
{
    public abstract class BaseQueryResult<T> : IQueryResult<T> where T : class
    {
        List<T> Data { get; set; }

        public int Total { get; set; }

        public IEnumerator<T> GetEnumerator()
        {

            return Data.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public BaseQueryResult()
        {
            Data = new List<T>();
        }

        public BaseQueryResult(IEnumerable<T> data)
        {
            Data = data.ToList();
        }
    }
}
