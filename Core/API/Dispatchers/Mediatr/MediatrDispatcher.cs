using Dispatcher;
using MediatR;

namespace MediatRDispatcher
{
    public class MediatrDispatcher : IDispatcher
    {
        private readonly IMediator mediator;

        public MediatrDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
            return mediator.Send(request, cancellationToken);
        }

        public async Task<T?> Send<T>(object request, CancellationToken cancellationToken = default) where T : class
        {
            var response = await  Send(request, cancellationToken);
            var tResponse = response as T;
            return tResponse;
        }
    }
}