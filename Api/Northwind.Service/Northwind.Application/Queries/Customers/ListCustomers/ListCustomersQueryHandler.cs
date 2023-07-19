using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Customers.ListCustomers
{
    public class ListCustomersQueryHandler : QueryHandler<CustomersDTO, Customer>
    {
        public ListCustomersQueryHandler(IMapper mapper, IRepository<Customer> repository) : base(mapper, repository)
        {
        }
         
    }
}
