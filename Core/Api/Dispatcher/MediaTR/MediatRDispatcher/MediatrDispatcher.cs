using Dispatcher;
using MediatR;

namespace MediatRDispatcher
{
    public class MediatrDispatcher : IDispatcher
    {
        private readonly IMediator mediator;
        public Task<object?> Send(object request, CancellationToken cancellationToken = default)
        {
          
            return mediator.Send(request, cancellationToken);            
        } 

        public MediatrDispatcher(IMediator mediator)
        {
            this.mediator = mediator;
        }
    }
}