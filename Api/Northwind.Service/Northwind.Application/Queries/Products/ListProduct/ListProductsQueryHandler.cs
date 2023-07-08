﻿using AutoMapper;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Domain.Entities;
using Repository;

namespace Northwind.Application.Queries.Products.ListProduct
{
    public class ListProductsQueryHandler : QueryHandler<ProductsDTO, Product>
    {
        public ListProductsQueryHandler(IMapper mapper, IRepository<Product> repository) : base(mapper, repository, "Supplier,Category")
        {
        }
         
    }
}