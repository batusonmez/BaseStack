using Dispatcher;
using MediatR;

namespace MediatRDispatcher
{
    public class BaseQuery<T> : IRequest<T> where T : class
    {
    }
}
