using Person.Domain.DTO;

namespace Person.Domain.Services.Outbox
{
    public interface IOutBoxService
    {
        Guid SaveOutBox(OutBoxDTO outboxDTO);
    }
}
