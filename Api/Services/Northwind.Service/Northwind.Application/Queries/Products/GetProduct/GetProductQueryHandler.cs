using AutoMapper;
using MediatR;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.Products.GetProduct;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Products.ListProduct
{
    public class GetProductQueryHandler : IRequestHandler<GetProductQuery, ProductsDTO>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Product> repository;

        public GetProductQueryHandler(IMapper mapper, IRepository<Product> repository)
        {
            this.mapper = mapper;
            this.repository = repository;
        }

        public Task<ProductsDTO?> Handle(GetProductQuery request, CancellationToken cancellationToken)
        {
            return Task.Run(() =>
               {
                   Product? product = repository.Get(d => d.ProductId == request.ProductID, includeProperties: "Supplier,Category").FirstOrDefault();
                   if (product == null)
                   {
                       return null;
                   }

                   return mapper.Map<ProductsDTO>(product);
               });
        }
    }
}
