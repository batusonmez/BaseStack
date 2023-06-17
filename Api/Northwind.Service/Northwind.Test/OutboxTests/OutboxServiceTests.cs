using EFAdapter;
using Northwind.Application.Models.DTO;
using Northwind.Domain.Entities;
using Northwind.Infrastructure.Services.Outbox;

namespace Northwind.Test.OutboxTests
{
    [TestClass]
    public class OutboxServiceTests: BaseTest
    {
        [TestMethod]
        public async Task Save_Outbox()
        {
            // Arrange
            var uow =InitUOW();
            var mapper = InitNorthwindAPIMapper();
            var repository = new EFRepository<Outbox>(uow);
            var service = new OutboxService(repository,mapper, uow);
            var testData = new  OutBoxDTO()
            {
                Data = new
                {
                    a = 1,
                    b = 2
                },
                DataType = "unitTest",
                DataID =Guid.NewGuid().ToString()
            };
            //Act
            service.SaveOutBox(testData);
           await uow.Save();
            //Assert
            var entry = DB.Outbox.FirstOrDefault(d => d.DataID == testData.DataID);
            Assert.IsNotNull(entry);
            Assert.IsTrue(entry.DataType == "unitTest"); 
        }

        [TestMethod]
        public async Task Save_Outbox_List()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitNorthwindAPIMapper();
            var repository = new EFRepository<Outbox>(uow);
            var service = new OutboxService(repository, mapper, uow);
            var outboxes = new List<OutBoxDTO>();
            for (int i = 0; i < 100; i++)
            {
                var testData = new OutBoxDTO()
                {
                    Data = new
                    {
                        a = i,
                        b = i+4
                    },
                    DataType = "unitTest",
                    DataID = Guid.NewGuid().ToString()
                };
                outboxes.Add(testData);
            }
            
            //Act
            service.SaveOutBox(outboxes);
            await uow.Save();
            //Assert
            foreach (var item in outboxes)
            {
                var entry = DB.Outbox.FirstOrDefault(d => d.DataID == item.DataID);
                Assert.IsNotNull(entry); 
            }
     
        }
    }
}
