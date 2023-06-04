
using Index.Application.Common;
using Index.Application.Models;
using MassTransit;
using MessageBusDomainEvents;

namespace Index.Application.Consumers
{
    public class IndexDataConsumer : IConsumer<IndexData>
    {
        private readonly IPublishEndpoint eventBus;
        private readonly IIndexer indexer;

        public IndexDataConsumer(IPublishEndpoint eventBus, IIndexer indexer)
        {
            this.eventBus = eventBus;
            this.indexer = indexer;
        }
        public async Task Consume(ConsumeContext<IndexData> context)
        {
            var data = context.Message;

            if (!string.IsNullOrEmpty(data.Name) && data.Value != null)
            {
                var resp = await indexer.Index(data.Name, data.ID.ToString(), data.Value);
                IndexException.ThrowIf(!resp, $"Unable to create Index {data.ID}");

            }

            await context.RespondAsync<DataIndexed>(new {
                ID = context.Message.ID
                
            });
            
        }
    }
}
