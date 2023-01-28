using Newtonsoft.Json;
using Person.Domain.DTO;
using Repository;

namespace Person.Domain.Services.Outbox
{
    public class OutboxService : IOutBoxService
    {
        private readonly IRepository<Entities.Outbox> outboxRepository;
        private readonly IUOW uow;

        public OutboxService(IRepository<Entities.Outbox> outboxRepository,
            IUOW uow)
        {
            this.outboxRepository = outboxRepository;
            this.uow = uow;
        }
        public Guid SaveOutBox(OutBoxDTO outboxDTO)
        {
            var outbox = new Entities.Outbox()
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
