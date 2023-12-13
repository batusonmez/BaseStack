using Todo.Application.Models.DTO;

namespace Todo.Application.Services.Outbox;

public interface IOutBoxService
{
    Guid SaveOutBox(OutBoxDTO outboxDTO);
    IEnumerable<OutBoxDTO> SaveOutBox(IEnumerable<OutBoxDTO> outboxDTOs);
}
