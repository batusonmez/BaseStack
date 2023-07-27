using Dispatcher;
using MediatR;

namespace MediatRDispatcher.Query
{
    public abstract class BaseQuery<T> : IRequest<T> where T : class
    {
          int MaxPageSize { get; set; } = 100;
        private int pageSize=10;

        public int PageSize
        {
            get {
                return regulatedPageSize(pageSize);
            }
            set
            {
                if (value<=0) return;
                pageSize = regulatedPageSize(value);
            }
        }
        int regulatedPageSize(int refVal)
        {
           return Math.Min(Math.Max(refVal, 0), MaxPageSize);
        } 

        public int Page { get; set; }
    }
}
