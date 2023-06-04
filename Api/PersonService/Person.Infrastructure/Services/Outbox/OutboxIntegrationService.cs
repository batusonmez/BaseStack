

using MassTransit;
using MassTransit.Clients;
using MassTransit.Transports;
using MessageBusDomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Person.Application.Models.Configuration;
using Person.Infrastructure.BackgroundServices;
using Repository;

namespace Person.Infrastructure.Services.Outbox
{
    public class OutboxIntegrationService : BaseBackgroundService
    {

        private readonly IServiceScopeFactory scopeFactory;
        private readonly IOptions<IndexConfig> indexConfig;

        public OutboxIntegrationService(IServiceScopeFactory scopeFactory,
            IOptions<IndexConfig> indexConfig
            ) : base(indexConfig.Value.Delay)
        {
            this.scopeFactory = scopeFactory;
            this.indexConfig = indexConfig;
        }

        public override async Task Execute()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var uow = scope.ServiceProvider.GetRequiredService<IUOW>();
                var eventBus = scope.ServiceProvider.GetRequiredService<IRequestClient<IndexData>>();

                var repository = scope.ServiceProvider.GetRequiredService<IRepository<DomainEntities.Outbox>>();

                var awaitingJobs = repository.Get(d => !d.ProcessDate.HasValue
                ).OrderBy(d => d.CreationDate).Take(indexConfig.Value.BatchSize);
                if (awaitingJobs.Any())
                {
                    foreach (var item in awaitingJobs)
                    {
                        item.RequestDate = DateTime.Now;
                        repository.Update(item);
                        await uow.Save();


                        var resp = await eventBus.GetResponse<DataIndexed>(new IndexData()
                        {
                            ID = item.ID,
                            Name = item.DataType,
                            Value = item.Data
                        });
                        item.ProcessDate = DateTime.Now;
                        await uow.Save();
                    }
                }
            }
        }
    }
}
