using Newtonsoft.Json;
using Person.Application.DTO;
using Person.Application.Services.Outbox;
using Repository;

namespace Person.Infrastructure.Services.Outbox
{
    public class OutboxService : IOutBoxService
    {
        private readonly IRepository<DomainEntities.Outbox> outboxRepository;
        private readonly IUOW uow;

        public OutboxService(IRepository<DomainEntities.Outbox> outboxRepository,
            IUOW uow)
        {
            this.outboxRepository = outboxRepository;
            this.uow = uow;
        }
        public Guid SaveOutBox(OutBoxDTO outboxDTO)
        {
            var outbox = new DomainEntities.Outbox()
            {
                Data = JsonConvert.SerializeObject(outboxDTO.Data),
                DataID= outboxDTO.ID,
                DataType=outboxDTO.DataType,
                CreationDate=DateTime.Now
            };
            outboxRepository.Insert(outbox);

            return outbox.ID;
        }
         
    }
}
