using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Customers.ListCustomers
{
    public class ListCustomersQueryHandler : QueryHandler<ProductsDTO, Product>
    {
        public ListCustomersQueryHandler(IMapper mapper, IRepository<Product> repository) : base(mapper, repository, "Supplier,Category")
        {
        }
         
    }
}
