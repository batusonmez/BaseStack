﻿using AutoMapper;
using EFAdapter;
using Northwind.Application.Commands;
using Northwind.Application.Commands.GenericCommands.Delete;
using Northwind.Application.Models.DTO;
using Northwind.Domain.Entities;
using Northwind.Infrastructure.Services.Outbox;
using Northwind.Test;
using System.Net;

namespace Northwind.Test.ProductTests
{
    [TestClass]
    public class ProductCommandTests : BaseTest
    {

        Repository.IUOW? uow;
        IMapper? mapper;
        EFRepository<Outbox>? outboxRepository;
        EFRepository<Product>? repository;

        OutboxService? outboxService;

        [TestInitialize]
        public void Setup()
        {
            ResetTestDB();
            uow = InitUOW();
            mapper = InitNorthwindAPIMapper();
            outboxRepository = new  (uow);
            outboxService = new  (outboxRepository, mapper, uow);
            repository = new  (uow);

            DB.Categories.Add(new ()
            {
                CategoryId = 1,
                CategoryName = "Test Categoty",
                Description = "Test category description ",
                Picture = new byte[] { 1, 2, 3 }

            });
            DB.Suppliers.Add(new ()
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
            DB.Products.Add(new ()
            {
                CategoryId = 1,
                ProductId = 1,
                ProductName = "Test Product",
                QuantityPerUnit = "test quantity",
                UnitPrice = 20,
                SupplierId = 3,
                UnitsInStock = 0,
                UnitsOnOrder = 0
            });



            DB.SaveChanges();
        }

        [TestMethod]
        public async Task New_Product_Command()
        {
            // Arrange  
            ProductsDTO testDTO = new  ()
            {

                CategoryId = 1,
                SupplierId = 3,
                UnitsOnOrder = 2,
                UnitsInStock = 5,
                UnitPrice = 32,
                QuantityPerUnit = "12 pc",
                CategoryName = "Test Category",
                ProductName = "Product Name",
                SupplierName = "Suplier Name",
                ReorderLevel = 1
            };
            UpsertCommand<ProductsDTO> testData = new (testDTO);
            UpsertCommandHandler<ProductsDTO, Product>? handler = new (mapper, repository, outboxService, uow);

            //Act 
            UpsertCommandResponse response = await handler.Handle(testData, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            ProductsDTO? resultDto = response.Data as ProductsDTO;
            Assert.IsNotNull(resultDto);
            Assert.IsTrue(resultDto.HasID);
            Product? product = DB.Products.FirstOrDefault(d => d.ProductId == resultDto.ProductId);
            Outbox? outbox = DB.Outbox.FirstOrDefault(d => d.DataID == resultDto.IndexTrace.ToString());
            Assert.IsNotNull(product);
            Assert.IsNotNull(outbox);

        }

        [TestMethod]
        public async Task Update_Product_Command()
        {
            // Arrange  
            ProductsDTO testDTO = new ()
            {
                ProductId = 1,
                CategoryId = 1,
                SupplierId = 3,
                UnitsOnOrder = 2,
                UnitsInStock = 5,
                UnitPrice = 32,
                QuantityPerUnit = "12 pc",
                CategoryName = "Test Category",
                ProductName = "Product Name",
                SupplierName = "Suplier Name",
                ReorderLevel = 1
            };
            UpsertCommand<ProductsDTO> testData = new UpsertCommand<ProductsDTO>(testDTO);
            UpsertCommandHandler<ProductsDTO, Product>? handler = new  (mapper, repository, outboxService, uow);

            //Act 
            UpsertCommandResponse response = await handler.Handle(testData, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            ProductsDTO? resultDto = response.Data as ProductsDTO;
            Assert.IsNotNull(resultDto);
            Assert.AreEqual(resultDto.ProductId , 1);
            Product? product = DB.Products.FirstOrDefault(d => d.ProductId == resultDto.ProductId);
            Outbox? outbox = DB.Outbox.FirstOrDefault(d => d.DataID == resultDto.IndexTrace.ToString());
            Assert.IsNotNull(product);
            Assert.IsNotNull(outbox);
            Assert.AreEqual(product.UnitPrice , testDTO.UnitPrice);
            Assert.AreEqual(product.QuantityPerUnit ,testDTO.QuantityPerUnit);

        }

        [TestMethod]
        public async Task Delete_Product_Command()
        {
            // Arrange  
            ProductsDTO testDTO = new ()
            {
                ProductId = 1,
                CategoryId = 1,
                SupplierId = 3,
                UnitsOnOrder = 2,
                UnitsInStock = 5,
                UnitPrice = 32,
                QuantityPerUnit = "12 pc",
                CategoryName = "Test Category",
                ProductName = "Product Name",
                SupplierName = "Suplier Name",
                ReorderLevel = 1
            };
            DeleteCommand<ProductsDTO> testData = new (testDTO);
            if (repository == null || outboxService == null || uow==null)
            {
                Assert.Fail("Invalid arrangement");
            }
            DeleteCommandHandler<ProductsDTO, Product>? handler = new (repository, outboxService, uow);

            //Act 
            DeleteCommandResponse response = await handler.Handle(testData, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Product? product = DB.Products.FirstOrDefault(d => d.ProductId == testDTO.ProductId);
            Assert.IsNull(product);
            Outbox? outbox = DB.Outbox.FirstOrDefault(d => d.DataID == testDTO.ProductId.ToString());
            Assert.IsNotNull(outbox);
            Assert.AreEqual(outbox.DataType , outbox.DataType);
            Assert.AreEqual(outbox.DataID,testDTO.ProductId.ToString());
        }
    }
}
