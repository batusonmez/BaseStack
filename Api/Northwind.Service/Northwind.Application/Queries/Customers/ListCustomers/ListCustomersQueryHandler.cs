﻿using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Customers.ListCustomers
{
    public class ListCustomersQueryHandler : BasePagedQueryHandler<CustomersDTO, Customer>
    {
        public ListCustomersQueryHandler(IMapper mapper, IRepository<Customer> repository, IIndexService indexService) : base(mapper, repository, indexService)
        {
        }
        
         
    }
}
