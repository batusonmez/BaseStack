

using EFAdapter;
using Microsoft.Extensions.DependencyInjection;
using Person.Infrastructure.BackgroundServices;
using Repository;

namespace Person.Domain.Services.Outbox
{
    public class OutboxIntegrationService : BaseBackgroundService
    {
        private readonly IServiceScopeFactory scopeFactory;

        public OutboxIntegrationService(IServiceScopeFactory scopeFactory) :base( 20000 )
        {
                                
            this.scopeFactory = scopeFactory;
        }

        public override  async Task Execute()
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var uow = scope.ServiceProvider.GetRequiredService<IUOW>();
                var repository = scope.ServiceProvider.GetRequiredService<IRepository<Entities.Outbox>>();

                var awaitingJobs= repository.Get(d => !d.ProcessDate.HasValue);
                if (awaitingJobs.Any())
                {
                    foreach (var item in awaitingJobs)
                    {
                        item.ProcessDate = DateTime.Now;
                        repository.Update(item);
                    }
                    await uow.Save();
                }
            }
        }
    }
}
