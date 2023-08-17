using AutoMapper;
using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Domain.Entities;
using Northwind.Infrastructure.CLI.Commands;
using Northwind.Infrastructure.Services.Outbox;
using Repository;

namespace Northwind.Test.CLITests
{
    [TestClass]
    public class ReIndexTests : BaseTest
    {
        [TestMethod]
        public async Task ReIndex_Categories()
        {
            // Arrange
            ResetTestDB();
            IUOW uow =InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Outbox> repository = new(uow);
            EFRepository<Category> categoryRepository = new(uow);
            OutboxService service = new (repository,mapper, uow);
            Category testData = new ()
            {
                CategoryId=1,
                CategoryName="Category name",
                Description= "Category description",
                Picture=new byte[] {1,2,3}                
            };
            DB.Categories.Add(testData);
            DB.SaveChanges();
            ReindexCategoriesCommand command = new(mapper, categoryRepository, service, uow);
            
            //Act
           await command.Handle(10);

           
            //Assert
            Outbox? entry = DB.Outbox.FirstOrDefault(d => d.DataID == testData.CategoryId.ToString());
            Assert.IsNotNull(entry);
            Assert.AreEqual(entry.DataID ,testData.CategoryId.ToString());
            Assert.IsTrue(entry.Data?.Contains(testData.CategoryName));
            Assert.IsTrue(entry.Data?.Contains(testData.Description));
        }

        [TestMethod]
        public async Task ReIndex_Suppliers()
        {
            // Arrange
            ResetTestDB();
            IUOW uow = InitUOW();
            IMapper mapper = InitNorthwindAPIMapper();
            EFRepository<Outbox> repository = new(uow);
            EFRepository<Supplier> supplierRepository = new(uow);
            OutboxService service = new(repository, mapper, uow);
            Supplier testData = new()
            {
                SupplierId = 1,
                CompanyName = "Company 1",
                ContactName = " Contact 1",
                ContactTitle = "Title 1",
                Address = "Adress 1",
                City = "City 1",
                Region = "Region 1",
                PostalCode = "Postal code 1",
                Country = "Country 1",
                Phone = "Phone 1",
                Fax = "Fax 1",
                HomePage = "https://www.company1.com"
            };
            DB.Suppliers.Add(testData);
            DB.SaveChanges();
            ReindexSupplierCommand command = new(mapper, supplierRepository, service, uow);

            //Act
            await command.Handle(10);


            //Assert
            Outbox? entry = DB.Outbox.FirstOrDefault(d => d.DataID == testData.SupplierId.ToString());
            Assert.IsNotNull(entry);
            Assert.AreEqual(entry.DataID, testData.SupplierId.ToString());
            Assert.IsTrue(entry.Data?.Contains(testData.CompanyName));
            Assert.IsTrue(entry.Data?.Contains(testData.ContactName));
        }
    }
}
