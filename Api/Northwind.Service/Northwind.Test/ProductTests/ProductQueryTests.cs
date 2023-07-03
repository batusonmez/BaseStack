﻿using AutoMapper;
using EFAdapter;
using Northwind.Application.Commands;
using Northwind.Application.Commands.GenericCommands.Delete;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries;
using Northwind.Application.Queries.Products.ListProduct;
using Northwind.Domain.Entities;
using Northwind.Infrastructure.Services.Outbox;
using Northwind.Test;
using System.Net;

namespace Northwind.Test.ProductTests
{
    [TestClass]
    public class ProductQueryTests : BaseTest
    {

        Repository.IUOW? uow;
        IMapper? mapper; 
        EFRepository<Product>? repository;
         

        [TestInitialize]
        public void Setup()
        {
            ResetTestDB();
            uow = InitUOW();
            mapper = InitNorthwindAPIMapper(); 
            repository = new EFRepository<Product>(uow);

            DB.Category.Add(new Category()
            {
                CategoryId = 1,
                CategoryName = "Test Categoty",
                Description = "Test category description ",
                Picture = new byte[] { 1, 2, 3 }

            });
            DB.Supplier.Add(new Supplier()
            {
                Address = "Test Aderess",
                City = "test City",
                CompanyName = "Test Company",
                ContactName = "Test Contact",
                ContactTitle = "Test Contact Title",
                Country = "Test Country",
                Fax = "Test Fax",
                HomePage = "wwww.test.com",
                Phone = "0 000 000",
                PostalCode = "06200",
                Region = "Test Region",
                SupplierId = 1
            });
            DB.Product.Add(new Product()
            {
                CategoryId = 1,
                ProductId = 1,
                ProductName = "Test Product",
                QuantityPerUnit = "test quantity",
                UnitPrice = 20,
                SupplierId = 1,
                UnitsInStock = 0,
                UnitsOnOrder = 0
            });

            DB.Product.Add(new Product()
            {
                CategoryId = 1,
                ProductId = 2,
                ProductName = "Test Product 2",
                QuantityPerUnit = "test quantity 2",
                UnitPrice = 30,
                SupplierId = 1,
                UnitsInStock = 0,
                UnitsOnOrder = 0
            });

            DB.Product.Add(new Product()
            {
                CategoryId = 1,
                ProductId = 3,
                ProductName = "Test Product 3",
                QuantityPerUnit = "test quantity 3",
                UnitPrice = 40,
                SupplierId = 1,
                UnitsInStock = 0,
                UnitsOnOrder = 0
            });

            DB.SaveChanges();
        }

        [TestMethod]
        public async Task  Product_Query()
        {
            // Arrange   
            Query<ProductsDTO> query = new Query<ProductsDTO>();
            if(mapper==null || repository == null)
            {
                Assert.Fail("Invalid arrangement");
            }
            ListProductsQueryHandler handler = new ListProductsQueryHandler(mapper, repository );

            //Act 
            QueryResponse<ProductsDTO> response = await handler.Handle(query, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 3);
            foreach (var item in DB.Product)
            {
                var productDto = response.FirstOrDefault(d => d.ProductId == item.ProductId);
                Assert.IsNotNull(productDto);
                Assert.IsTrue(productDto.ProductName==item.ProductName);
                Assert.IsTrue(productDto.QuantityPerUnit == item.QuantityPerUnit);
                Assert.IsTrue(productDto.UnitPrice == item.UnitPrice);
                Assert.IsTrue(productDto.CategoryId == item.CategoryId);
                Assert.IsTrue(productDto.SupplierId == item.SupplierId);
                Assert.IsTrue(productDto.UnitsOnOrder == item.UnitsOnOrder);
                Assert.IsTrue(productDto.UnitsInStock == item.UnitsInStock);
            }
             

        }
         
    }
}
