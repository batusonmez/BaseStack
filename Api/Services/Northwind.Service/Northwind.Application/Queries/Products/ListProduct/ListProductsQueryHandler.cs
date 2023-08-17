using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Products.ListProduct
{
    public class ListProductsQueryHandler : BasePagedQueryHandler<ProductsDTO, Product>
    {
        public ListProductsQueryHandler(IMapper mapper, IRepository<Product> repository,  IIndexService indexService) : base(mapper, repository,indexService, "Supplier,Category")
        {
        }
         
    }
}
