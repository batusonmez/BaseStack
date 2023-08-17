using AutoMapper;
using Exceptions;
using MediatR;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.DTO.Types;
using Northwind.Application.Services.Outbox;
using Repository;

namespace Northwind.Application.Commands
{
    public class UpsertCommandHandler<T, E> : IRequestHandler<UpsertCommand<T>, UpsertCommandResponse> where T : IDTO where E : class
    {
        private readonly IMapper mapper;
        private readonly IRepository<E> repository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;

        public UpsertCommandHandler(IMapper mapper,
            IRepository<E> repository,
            IOutBoxService outBoxService,
            IUOW uow)
        {
            this.mapper = mapper;
            this.repository = repository;
            this.outBoxService = outBoxService;
            this.uow = uow;
        }


        public virtual Task<UpsertCommandResponse> Handle(UpsertCommand<T> request, CancellationToken cancellationToken)
        {
         return Task.Run(()=>{ 
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

                    return PostExecute(entity);
                }
            } ) ;
        }

        public virtual  async Task<UpsertCommandResponse> PostExecute(E entity)
        {
            T dto = mapper.Map<T>(entity);
            UpsertCommandResponse resp = new UpsertCommandResponse(dto);
            SetOutbox(dto);
            await uow.Save();
            return resp;
        }

        private E GetEntity(T dto)
        {
            if (dto.HasID)
            {
                E? entity = repository.GetByID(dto.IndexKey);
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
                OutBoxDTO outbox = new OutBoxDTO();
                outbox.Data = dto;
                outbox.DataID = dto.IndexKey.ToString();
                return outBoxService.SaveOutBox(outbox);
            }
            return null;
        }
    }
}
