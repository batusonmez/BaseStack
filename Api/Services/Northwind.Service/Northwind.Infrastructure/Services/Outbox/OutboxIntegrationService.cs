using MassTransit;
using MessageBusDomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Northwind.Application.Models.Configuration;
using Northwind.Infrastructure.BackgroundServices;
using Repository;

namespace Northwind.Infrastructure.Services.Outbox
{
    public class OutboxIntegrationService : BaseBackgroundService
    {

        private readonly IServiceScopeFactory scopeFactory;
        private readonly IOptions<IndexConfig> indexConfig;
 

        public OutboxIntegrationService(IServiceScopeFactory scopeFactory,
            IOptions<IndexConfig> indexConfig,
            ILogger<OutboxIntegrationService> logger 
            ) : base(indexConfig.Value.Delay, logger)
        {
            this.scopeFactory = scopeFactory;
            this.indexConfig = indexConfig; 
        }

        public override async Task Execute()
        {

            List<DomainEntities.Outbox> awaitingJobs = new();
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                IUOW uow = scope.ServiceProvider.GetRequiredService<IUOW>();
                IRepository<DomainEntities.Outbox> repository = scope.ServiceProvider.GetRequiredService<IRepository<DomainEntities.Outbox>>();
                awaitingJobs = repository.Get(d => !d.ProcessDate.HasValue
               ).OrderBy(d => d.CreationDate).Take(indexConfig.Value.BatchSize).ToList();
            }
            await Task.WhenAll(awaitingJobs.Select(d => proccessIndexRequest(d)));
        }


        private Task proccessIndexRequest(DomainEntities.Outbox outbox)
        {
            return Task.Run(async () =>
            {
                using (IServiceScope scope = scopeFactory.CreateScope())
                {
                    IUOW uow = scope.ServiceProvider.GetRequiredService<IUOW>();
                    IRepository<DomainEntities.Outbox> repository = scope.ServiceProvider.GetRequiredService<IRepository<DomainEntities.Outbox>>();
                    IRequestClient<IndexData> eventBus = scope.ServiceProvider.GetRequiredService<IRequestClient<IndexData>>();
                    IPublishEndpoint bus = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                    outbox.RequestDate = DateTime.Now;                                        
                    repository.Update(outbox);
                    await bus.Publish<IndexData>(new()
                    {
                        OutboxID=outbox.ID,
                        ID = outbox.DataID,
                        Name = outbox.DataType,
                        Value = outbox.Data
                    });
                    await uow.Save();
                    
                   

                }
            });

        }

    }
}
