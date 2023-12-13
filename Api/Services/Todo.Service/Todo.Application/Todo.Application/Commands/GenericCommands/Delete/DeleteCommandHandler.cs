
using MediatR;
using Northwind.Application.Commands.GenericCommands.Delete;
using Repository;
using Todo.Application.Models.DTO;
using Todo.Application.Models.DTO.Types;
using Todo.Application.Services.Outbox;

namespace Todo.Application.Commands.GenericCommands.Delete;

public class DeleteCommandHandler<T, E>(
    IRepository<E> repository,
    IOutBoxService outBoxService,
    IUOW uow) : IRequestHandler<DeleteCommand<T>, DeleteCommandResponse> where T : IDTO where E : class
{
    public virtual Task<DeleteCommandResponse> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
    {
        return Task.Run(async () =>
        {
            using (uow)
            {

                DeleteCommandResponse resp = new DeleteCommandResponse(request.Data);
                repository.Delete(request.Data.ID);
                SetOutbox(request.Data);
                await uow.Save();
                return resp;
            }
        });
    }


    public virtual Guid? SetOutbox(T dto)
    {
        if (dto.IndexEnabled)
        {
            OutBoxDTO outbox = new OutBoxDTO();
            outbox.Data = dto;
            outbox.DataID = dto.ID.ToString();
            outbox.DataType = OutBoxDTO.DELETE;
            return outBoxService.SaveOutBox(outbox);
        }
        return null;
    }
}
