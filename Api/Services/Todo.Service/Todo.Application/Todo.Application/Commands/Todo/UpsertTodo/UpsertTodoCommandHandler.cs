using AutoMapper;
using Repository;
using Todo.Application.Commands.GenericCommands.Upsert;
using Todo.Application.Models.DTO;
using Todo.Application.Services.Outbox;

namespace Todo.Application.Commands.Todo.UpsertTodo
{
    public class UpsertTodoCommandHandler(IMapper mapper,
        IRepository<Domain.Entities.Todo> repository,
        IOutBoxService outBoxService,
        IUOW uow) : UpsertCommandHandler<TodoDTO, Domain.Entities.Todo>(mapper, repository, outBoxService, uow)
    {
    }
}
