 
using MassTransit;
using MessageBusDomainEvents;
using Microsoft.Extensions.Logging;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Consumers { 
    public class DataIndexedConsumer : IConsumer<DataIndexed>
    {
  
        
        private readonly ILogger<DataIndexedConsumer> logger;
        private readonly IRepository<Outbox> repository;
        private readonly IUOW uow;

        public DataIndexedConsumer( 
            ILogger<DataIndexedConsumer> logger,
            IRepository<Outbox> repository,
            IUOW uow
            )
        { 
            this.logger = logger;
            this.repository = repository;
            this.uow = uow;
        }
         

        public async Task Consume(ConsumeContext<DataIndexed> context)
        {
            try
            {
                DataIndexed data = context.Message;
                if (!data.OutboxID.HasValue)
                {
                    return;
                }
                Outbox? outbox =  repository.GetByID(data.OutboxID);
                if (outbox != null)
                {
                    outbox.ProcessDate = DateTime.Now;
                    repository.Update(outbox);
                    await uow.Save();
                }
            }
            catch (Exception ex)
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
