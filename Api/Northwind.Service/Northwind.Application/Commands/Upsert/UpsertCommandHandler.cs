using AutoMapper;
using MediatR;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.DTO.Types;
using Northwind.Application.Services.Outbox;
using Repository;

namespace Northwind.Application.Commands
{
    public class UpsertCommandHandler<T,E> : IRequestHandler<UpsertCommand<T>, UpsertCommandResponse> where T:IDTO where  E:class
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
        

        public  virtual async Task<UpsertCommandResponse> Handle(UpsertCommand<T> request, CancellationToken cancellationToken)
        {
            using (uow)
            {
                var entity = mapper.Map<E>(request.Data);
                repository.Insert(entity);
                T dto = mapper.Map<T>(entity);
                var resp = new UpsertCommandResponse(dto);
                outBoxService.SaveOutBox(mapper.Map<OutBoxDTO>(dto));
                await uow.Save();
                return resp;
            }            
        }
    }
}
