
using Index.Application.Common;
using Index.Application.Models;
using MassTransit;
using MessageBusDomainEvents;
using Microsoft.Extensions.Logging;

namespace Index.Application.Consumers
{
    public class IndexDataConsumer : IConsumer<IndexData>
    {
        private readonly IPublishEndpoint eventBus;
        private readonly IIndexer indexer;
        private readonly ILogger<IndexDataConsumer> logger;

        public IndexDataConsumer(IPublishEndpoint eventBus, IIndexer indexer, ILogger<IndexDataConsumer> logger)
        {
            this.eventBus = eventBus;
            this.indexer = indexer;
            this.logger = logger;
        }

        public async Task Consume(ConsumeContext<IndexData> context)
        {
            try
            {
                var data = context.Message;

                if (!string.IsNullOrEmpty(data.Name) && data.Value != null)
                {
                    var resp = await indexer.Index(data.Name, data.ID, data.Value);
                    IndexException.ThrowIf(!resp, $"Unable to create Index {data.ID}");
                }

                await eventBus.Publish<DataIndexed>(new
                {
                    OutboxID=context.Message.OutboxID,
                    ID = context.Message.ID

                });                
            }
            catch(Exception ex)
            {
                HandleException(ex);
                throw;                
            }
                 
        }


        private void HandleException(Exception ex)
        {
            logger.LogError(ex.Message);
            if (ex.InnerException != null)
            {
                logger.LogError(ex.InnerException.Message);
            }
        }
    }
}
