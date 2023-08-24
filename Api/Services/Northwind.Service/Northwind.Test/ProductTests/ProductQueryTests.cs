using AutoMapper;
using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Application.Queries.GenericQueries.ListQueryModels;
using Northwind.Application.Queries.Products.GetProduct;
using Northwind.Application.Queries.Products.ListProduct;
using Northwind.Application.Services.Index;
using Northwind.Domain.Entities;

namespace Northwind.Test.ProductTests
{
    [TestClass]
    public class ProductQueryTests : BaseTest
    {

        Repository.IUOW? uow;
        IMapper? mapper;
        EFRepository<Product>? repository;
        IIndexService? indexService;

        [TestInitialize]
        public void Setup()
        {
            ResetTestDB();
            uow = InitUOW();
            mapper = InitNorthwindAPIMapper();
            repository = new EFRepository<Product>(uow);
            indexService = MockIndexService();
            DB.Categories.Add(new Category()
            {
                CategoryId = 1,
                CategoryName = "Test Categoty",
                Description = "Test category description ",
                Picture = new byte[] { 1, 2, 3 }

            });
            DB.Suppliers.Add(new Supplier()
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
            DB.Products.Add(new Product()
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

            DB.Products.Add(new Product()
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

            DB.Products.Add(new Product()
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
        public async Task Product_Query()
        {
            // Arrange   
            ListQuery<ProductsDTO> query = new();
            if (mapper == null || repository == null || indexService == null)
            {
                Assert.Fail("Invalid arrangement");
            }
            ListProductsQueryHandler handler = new(mapper, repository, indexService);

            //Act 
            ListQueryResponse<ProductsDTO> response = await handler.Handle(query, CancellationToken.None);

            //Assert
            ClearTestConnection();
            uow = InitUOW();
            Assert.AreEqual(response.Total, 3);
            foreach (var item in DB.Products)
            {
                var productDto = response.FirstOrDefault(d => d.ProductId == item.ProductId);

                Assert.IsNotNull(productDto);
                Assert.AreEqual(productDto.ProductName, item.ProductName);
                Assert.AreEqual(productDto.QuantityPerUnit, item.QuantityPerUnit);
                Assert.AreEqual(productDto.UnitPrice, item.UnitPrice);
                Assert.AreEqual(productDto.CategoryId, item.CategoryId);
                Assert.AreEqual(productDto.SupplierId, item.SupplierId);
                Assert.AreEqual(productDto.UnitsOnOrder, item.UnitsOnOrder);
                Assert.AreEqual(productDto.UnitsInStock, item.UnitsInStock);
                Supplier? supplier = DB.Suppliers.Find(item.SupplierId);
                Assert.IsNotNull(supplier);
                Assert.AreEqual(productDto.SupplierName, supplier.CompanyName);
            }
        }


        [TestMethod]
        public async Task Product_By_ID_Query()
        {
            // Arrange   
            GetProductQuery query = new()
            {
                ProductID=2
            };
            if (mapper == null || repository == null || indexService == null)
            {
                Assert.Fail("Invalid arrangement");
            }
            GetProductQueryHandler handler = new (mapper, repository);

            //Act 
            ProductsDTO? response = await handler.Handle(query, CancellationToken.None);

            //Assert
            ClearTestConnection(); 
            Assert.IsNotNull(response);
            Product factProduct= DB.Products.First(d => d.ProductId == 2);

             
            Assert.AreEqual(factProduct.ProductName, response.ProductName);
            Assert.AreEqual(factProduct.QuantityPerUnit, response.QuantityPerUnit);
            Assert.AreEqual(factProduct.UnitPrice, response.UnitPrice);
            Assert.AreEqual(factProduct.CategoryId, response.CategoryId);
            Assert.AreEqual(factProduct.SupplierId, response.SupplierId);
            Assert.AreEqual(factProduct.UnitsOnOrder, response.UnitsOnOrder);
            Assert.AreEqual(factProduct.UnitsInStock, response.UnitsInStock);
       
          

        }

    }
}
