using MassTransit;
using MessageBusDomainEvents;

namespace Index.Domain.Consumers
{
    public class IndexDataConsumer : IConsumer<IndexData>
    {
        public Task Consume(ConsumeContext<IndexData> context)
        {
            
            return Task.CompletedTask;  
        }
    }
}
