using MassTransit;
using MessageBusDomainEvents;
using Person.Domain.Entities;
using Person.Domain.Services.Outbox;
using Repository;

namespace Person.Domain.Consumers
{
    public class DataIndexedConsumer : IConsumer<DataIndexed>
    {
        private readonly IRepository<Outbox> repository;
        private readonly IUOW uow;

        public DataIndexedConsumer(IRepository<Entities.Outbox> repository,
            IUOW uow)
        {
            this.repository = repository;
            this.uow = uow;
        }
        public async Task Consume(ConsumeContext<DataIndexed> context)
        {
            var outbox = repository.Get(d => d.ID == context.Message.ID && !d.ProcessDate.HasValue).FirstOrDefault();
            if (outbox == null)
            {
                return;
            }
            outbox.ProcessDate = DateTime.Now;
            await uow.Save();
        }
    }
}
