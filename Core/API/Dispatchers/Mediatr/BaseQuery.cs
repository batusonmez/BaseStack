using Dispatcher;
using MediatR;

namespace MediatRDispatcher
{
    public class BaseQuery<T> : IRequest<T> where T : class
    {
        public virtual int MaxPageSize { get; set; } = 100;
        private int pageSize;

        public int PageSize
        {
            get => pageSize;
            set
            {
                pageSize = Math.Min(Math.Max(value, 0), MaxPageSize);
            }
        }
    }
}
