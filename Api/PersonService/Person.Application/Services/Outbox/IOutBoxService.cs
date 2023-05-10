using Person.Application.DTO;

namespace Person.Application.Services.Outbox
{
    public interface IOutBoxService
    {
        Guid SaveOutBox(OutBoxDTO outboxDTO);
    }
}
