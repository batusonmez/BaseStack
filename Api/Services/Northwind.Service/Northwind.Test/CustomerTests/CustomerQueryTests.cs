﻿using AutoMapper;
using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.Customers.ListCustomers;
using Northwind.Application.Queries.GenericQueries.ListQueryModels;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;

namespace Northwind.Test.CustomerTests
{
    [TestClass]
    public class CustomerQueryTests : BaseTest
    {

        Repository.IUOW? uow;
        IMapper? mapper; 
        EFRepository<Customer>? repository;
        IIndexService indexService;

       [TestInitialize]
        public void Setup()
        {
            ResetTestDB();
            uow = InitUOW();
            mapper = InitNorthwindAPIMapper(); 
            repository = new (uow);
            indexService = MockIndexService();
            DB.Customers.Add(new ()
            {
                Address="test address",
                City="Test city",
                CompanyName="Test Company",
                ContactName="Test contact name",
                ContactTitle="Test Contact Title",
                Country="Test Country",
                CustomerId="TestID",
                Fax="test fax",
                Phone="test phone",
                PostalCode="test code",
                Region="Test Region"
                

            });

            DB.SaveChanges();
        }

        [TestMethod]
        public async Task  Customer_Query()
        {
            // Arrange   
            ListQuery<CustomersDTO> query = new();
            if(mapper==null || repository == null)
            {
                Assert.Fail("Invalid arrangement");
            }
            ListCustomersQueryHandler handler = new(mapper, repository, indexService);

            //Act 
            ListQueryResponse<CustomersDTO> response = await handler.Handle(query, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.IsTrue(response.Total == 1);
            foreach (var item in DB.Customers)
            {
                var productDto = response.FirstOrDefault(d => d.CustomerId == item.CustomerId);
                
                Assert.IsNotNull(productDto);
                Assert.IsTrue(productDto.Address == item.Address);
                Assert.IsTrue(productDto.City == item.City);
                Assert.IsTrue(productDto.Fax == item.Fax);
                Assert.IsTrue(productDto.Country == item.Country);
                Assert.IsTrue(productDto.CustomerId == item.CustomerId);                
                Assert.IsTrue(productDto.CompanyName == item.CompanyName);
                Assert.IsTrue(productDto.ContactTitle == item.ContactTitle);
                Assert.IsTrue(productDto.ContactName == item.ContactName);
                Assert.IsTrue(productDto.PostalCode == item.PostalCode);
                Assert.IsTrue(productDto.Region == item.Region); 
            }
             

        }
         
    }
}
