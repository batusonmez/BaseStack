using AutoMapper;
using Northwind.Application.Commands.GenericCommands.Delete;
using Northwind.Application.Models.DTO;
using Northwind.Application.Services.Outbox;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler  : DeleteCommandHandler<ProductsDTO,Product>
    { 
        public DeleteProductCommandHandler( 
            IRepository<Product> repository,
            IOutBoxService outBoxService, 
            IUOW uow):base( repository,outBoxService,uow)
        {            
        }
         

    }
}
