using MediatRDispatcher;
using Northwind.Application.Models.DTO;
using Person.Application.DTO;

namespace Northwind.Application.Commands.UpsertProduct
{
    public class UpsertProductCommand : BaseCommand<UpsertProductResponse>
    {
       public ProductsDTO Product  { get; set; }

        public UpsertProductCommand(ProductsDTO pro)
        {
            Product = person;
        }
    }
}
