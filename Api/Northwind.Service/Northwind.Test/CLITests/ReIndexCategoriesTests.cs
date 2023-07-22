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
    public class ReIndexCategoriesTests : BaseTest
    {
        [TestMethod]
        public async Task Save_Outbox()
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
            Outbox entry = DB.Outbox.FirstOrDefault(d => d.DataID == testData.CategoryId.ToString());
            Assert.IsNotNull(entry);
            Assert.IsTrue(entry.DataID ==testData.CategoryId.ToString());
            Assert.IsTrue(entry.Data.Contains(testData.CategoryName));
            Assert.IsTrue(entry.Data.Contains(testData.Description));
        }

        
    }
}
