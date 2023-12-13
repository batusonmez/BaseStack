using AutoMapper;
using Exceptions;
using MediatR;
using Northwind.Application.Commands;
using Repository;
using Todo.Application.Models.DTO;
using Todo.Application.Models.DTO.Types;
using Todo.Application.Services.Outbox;

namespace Todo.Application.Commands.GenericCommands.Upsert
{
    public class UpsertCommandHandler<T, E>(IMapper mapper,
        IRepository<E> repository,
        IOutBoxService outBoxService,
        IUOW uow) : IRequestHandler<UpsertCommand<T>, UpsertCommandResponse> where T : IDTO where E : class
    {
        public virtual Task<UpsertCommandResponse> Handle(UpsertCommand<T> request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                using (uow)
                {
                    E entity = GetEntity(request.Data);
                    if (!request.Data.HasID)
                    {
                        repository.Insert(entity);
                    }
                    else
                    {
                        repository.Update(entity);
                    }

                    UpsertCommandResponse result = await PostExecute(entity);
                    return result;
                }
            });
        }

        public virtual async Task<UpsertCommandResponse> PostExecute(E entity)
        {
            T dto = mapper.Map<T>(entity);
            SetOutbox(dto);
            await uow.Save();
            UpsertCommandResponse resp = new UpsertCommandResponse(dto);
            return resp;
        }

        private E GetEntity(T dto)
        {
            if (dto.HasID)
            {
                E? entity = repository.GetByID(dto.ID);
                BaseException.ThrowIf(entity == null, "Argument null exception :" + typeof(E));
                entity = mapper.Map(dto, entity);
                if (entity != null)
                    return entity;
            }

            return mapper.Map<E>(dto);
        }

        public virtual Guid? SetOutbox(T dto)
        {
            if (dto.IndexEnabled)
            {
                OutBoxDTO outbox = mapper.Map<OutBoxDTO>(dto); ;
                return outBoxService.SaveOutBox(outbox);
            }
            return null;
        }
    }
}
