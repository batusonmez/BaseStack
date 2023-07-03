 
using MediatR;
using Northwind.Application.Models.DTO;
using Northwind.Application.Models.DTO.Types;
using Northwind.Application.Services.Outbox;
using Repository;

namespace Northwind.Application.Commands.GenericCommands.Delete
{
    public class DeleteCommandHandler<T, E> : IRequestHandler<DeleteCommand<T>, DeleteCommandResponse> where T : IDTO where E : class
    { 
        private readonly IRepository<E> repository;
        private readonly IOutBoxService outBoxService;
        private readonly IUOW uow;

        public DeleteCommandHandler( 
            IRepository<E> repository,
            IOutBoxService outBoxService,
            IUOW uow)
        { 
            this.repository = repository;
            this.outBoxService = outBoxService;
            this.uow = uow;
        }


        public virtual Task<DeleteCommandResponse> Handle(DeleteCommand<T> request, CancellationToken cancellationToken)
        {
            return Task.Run(async () =>
            {
                using (uow)
                {

                    DeleteCommandResponse resp = new DeleteCommandResponse(request.Data);
                    repository.Delete(request.Data.IndexKey);
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
                outbox.DataID = dto.IndexKey.ToString();
                outbox.DataType = OutBoxDTO.DELETE;                
                return outBoxService.SaveOutBox(outbox);
            }
            return null;
        }
    }
}
