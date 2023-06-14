using Northwind.Application.Models.DTO; 

namespace Northwind.Application.Services.Outbox
{
    public interface IOutBoxService
    {
        Guid SaveOutBox(OutBoxDTO outboxDTO);
        IEnumerable<OutBoxDTO> SaveOutBox(IEnumerable<OutBoxDTO> outboxDTOs);
    }
}
