using AutoMapper;

using Repository;
using Todo.Application.Models.DTO;
using Todo.Application.Services.Outbox;

namespace Todo.Infrastructure.Services.Outbox;

public class OutboxService(IRepository<Domain.Entities.Outbox> outboxRepository,
    IMapper mapper ) : IOutBoxService
{
    public Guid SaveOutBox(OutBoxDTO outboxDTO)
    {
        var outbox = mapper.Map<Domain.Entities.Outbox>(outboxDTO);
        outboxRepository.Insert(outbox);
        return outbox.ID;
    }

    public IEnumerable<OutBoxDTO> SaveOutBox(IEnumerable<OutBoxDTO> outboxDTOs)
    {
        var outboxes = outboxDTOs.Select(d => mapper.Map<Domain.Entities.Outbox>(d)).ToList();
        outboxRepository.Insert(outboxes);
        return outboxDTOs;
    }



}
