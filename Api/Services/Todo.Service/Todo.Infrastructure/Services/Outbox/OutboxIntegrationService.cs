using MassTransit;
using MessageBusDomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Repository;
using Todo.Application.Models.Configuration;
using Todo.Infrastructure.BackgroundServices;

namespace Todo.Infrastructure.Services.Outbox;

public class OutboxIntegrationService(IServiceScopeFactory scopeFactory,
    IOptions<IndexConfig> indexConfig,
    ILogger<OutboxIntegrationService> logger
        ) : BaseBackgroundService(indexConfig.Value.Delay, logger)
{
    public override async Task Execute()
    {

        List<Domain.Entities.Outbox> awaitingJobs = new();
        using (IServiceScope scope = scopeFactory.CreateScope())
        {
            IUOW uow = scope.ServiceProvider.GetRequiredService<IUOW>();
            IRepository<Domain.Entities.Outbox> repository = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Entities.Outbox>>();
            awaitingJobs = repository.Get(d => !d.ProcessDate.HasValue
           ).OrderBy(d => d.CreationDate).Take(indexConfig.Value.BatchSize).ToList();
        }
        await Task.WhenAll(awaitingJobs.Select(d => proccessIndexRequest(d)));
    }


    private Task proccessIndexRequest(Domain.Entities.Outbox outbox)
    {
        return Task.Run(async () =>
        {
            using (IServiceScope scope = scopeFactory.CreateScope())
            {
                IUOW uow = scope.ServiceProvider.GetRequiredService<IUOW>();
                IRepository<Domain.Entities.Outbox> repository = scope.ServiceProvider.GetRequiredService<IRepository<Domain.Entities.Outbox>>();
                IRequestClient<IndexData> eventBus = scope.ServiceProvider.GetRequiredService<IRequestClient<IndexData>>();
                IPublishEndpoint bus = scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();

                outbox.RequestDate = DateTime.Now;
                repository.Update(outbox);
                await bus.Publish<IndexData>(new()
                {
                    OutboxID = outbox.ID,
                    ID = outbox.DataID,
                    Name = outbox.DataType,
                    Value = outbox.Data
                });
                await uow.Save();

            }
        });

    }

}
