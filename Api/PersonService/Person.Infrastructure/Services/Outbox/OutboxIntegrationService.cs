
 
using MassTransit;
using MessageBusDomainEvents;
using Microsoft.Extensions.DependencyInjection;
using Person.Infrastructure.BackgroundServices;
using Repository;

namespace Person.Infrastructure.Services.Outbox
{
    public class OutboxIntegrationService : BaseBackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory; 

        public OutboxIntegrationService(IServiceScopeFactory scopeFactory ) : base(20000)
        {
            this.scopeFactory = scopeFactory; 
        }

        public override async Task Execute()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var uow = scope.ServiceProvider.GetRequiredService<IUOW>();
               var eventBus= scope.ServiceProvider.GetRequiredService<IPublishEndpoint>();
                
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<DomainEntities.Outbox>>();

                var awaitingJobs = repository.Get(d => !d.RequestDate.HasValue).OrderBy(d=>d.CreationDate);
                if (awaitingJobs.Any())
                {
                    foreach (var item in awaitingJobs)
                    {
                        item.RequestDate = DateTime.Now;
                        repository.Update(item);
                        await uow.Save();
                        try
                        {
                            await eventBus.Publish(new IndexData()
                            {
                                ID = item.ID,
                                Name = item.DataType,
                                Value = item.Data
                            });
                        }
                        catch (Exception)
                        {
                            item.RequestDate =null;
                            repository.Update(item);
                            await uow.Save();
                            throw;
                        }
                        
                        
                    }                    
                }
            }
        }
    }
}
