using AutoMapper; 
using Northwind.Application.Models.DTO;
using Northwind.Application.Services.Outbox;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Commands.UpsertProduct
{
    public class UpsertProductCommandHandler  :  UpsertCommandHandler<ProductsDTO,Product>
    { 
        public UpsertProductCommandHandler(IMapper mapper,
            IRepository<Product> repository,
            IOutBoxService outBoxService, 
            IUOW uow):base(mapper,repository,outBoxService,uow)
        {            
        }

        public override Task<UpsertCommandResponse> Handle(UpsertCommand<ProductsDTO> request, CancellationToken cancellationToken)
        {
            // overide custom logic

            return base.Handle(request, cancellationToken);
        }

    }
}
