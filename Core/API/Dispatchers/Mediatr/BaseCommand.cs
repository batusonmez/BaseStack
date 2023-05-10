using Dispatcher;
using MediatR;

namespace MediatRDispatcher
{
    public class BaseCommand<T> : IRequest<T> where T : class
    {
    }
}
