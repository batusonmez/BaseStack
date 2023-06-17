using EFAdapter;
using Northwind.Application.Commands;
using Northwind.Application.Models.DTO;
using Northwind.Domain.Entities;
using Northwind.Infrastructure.Services.Outbox;
using Northwind.Test;

namespace Northwind.Test.ProductTests
{
    [TestClass]
    public class NewProductCommandTests : BaseTest
    {
        [TestMethod]
        public async Task New_Product_Command()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitNorthwindAPIMapper();
            var outboxRepository = new EFRepository<Outbox>(uow);
            var outboxService = new OutboxService(outboxRepository, mapper, uow);
            var repository = new EFRepository<Product>(uow);
            var handler = new UpsertCommandHandler<ProductsDTO, Product>(mapper, repository, outboxService, uow);

            DB.Category.Add(new Category()
            {
                CategoryId = 1,
                CategoryName = "Test Categoty",
                Description = "Test category description ",
                Picture=new byte[] { 1,2,3}

            });
            DB.Supplier.Add(new Supplier()
            {
                Address = "Test Aderess",
                City = "test City",
                CompanyName = "Test Company",
                ContactName = "Test Contact",
                ContactTitle = "Test Contact Title",
                Country="Test Country",
                Fax="Test Fax",
                HomePage="wwww.test.com",
                Phone="0 000 000",
                PostalCode="06200",
                Region="Test Region",
                SupplierId=1           
            });

            DB.SaveChanges();

            var testDTO = new ProductsDTO()
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
            var testData = new UpsertCommand<ProductsDTO>(testDTO);


            //Act
            var response = await handler.Handle(testData, CancellationToken.None);

            //Assert
            ClearTestContext();
            uow = InitUOW();
            var resultDto = response.Data as ProductsDTO;
            Assert.IsNotNull(resultDto);
            Assert.IsTrue(resultDto.ProductId > 0);
            var product = DB.Product.FirstOrDefault(d => d.ProductId == resultDto.ProductId);
            var outbox = DB.Outbox.FirstOrDefault(d => d.DataID == resultDto.ProductId.ToString());
            Assert.IsNotNull(product);
            Assert.IsNotNull(outbox); 
             
        }
    }
}
