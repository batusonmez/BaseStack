using AutoMapper;
using Newtonsoft.Json;
using Northwind.Application.Models.DTO;
using Northwind.Application.Services.Outbox;
using Repository;

namespace Northwind.Infrastructure.Services.Outbox
{
    public class OutboxService : IOutBoxService
    {
        private readonly IRepository<DomainEntities.Outbox> outboxRepository;
        private readonly IMapper mapper;
        private readonly IUOW uow;

        public OutboxService(IRepository<DomainEntities.Outbox> outboxRepository,
            IMapper mapper,
            IUOW uow)
        {
            this.outboxRepository = outboxRepository;
            this.mapper = mapper;
            this.uow = uow;
        }

        public Guid SaveOutBox(OutBoxDTO outboxDTO)
        {
            var outbox = mapper.Map<DomainEntities.Outbox>(outboxDTO);
            outboxRepository.Insert(outbox); 
            return outbox.ID;
        }

        public IEnumerable<OutBoxDTO> SaveOutBox(IEnumerable<OutBoxDTO> outboxDTOs)
        {
            var outboxes=outboxDTOs.Select(d=>mapper.Map<DomainEntities.Outbox>(d)).ToList();
            outboxRepository.Insert(outboxes); 
            return outboxDTOs;
        }



    }
}
