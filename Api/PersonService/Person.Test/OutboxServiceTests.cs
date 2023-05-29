using EFAdapter;
using Person.Domain.Entities;
using Person.Application.DTO;
using Person.Infrastructure.Services.Outbox;

namespace Person.Test
{
    [TestClass]
    public class OutboxServiceTests: BaseTest
    {
        [TestMethod]
        public async Task Save_Outbox()
        {
            // Arrange
            var uow =InitUOW();
            var mapper = InitPersonAPIMapper();
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
                ID =Guid.NewGuid()
            };
            //Act
            service.SaveOutBox(testData);
           await uow.Save();
            //Assert
            var entry = DB.Outbox.FirstOrDefault(d => d.DataID == testData.ID);
            Assert.IsNotNull(entry);
            Assert.IsTrue(entry.DataType == "unitTest");
            Assert.IsTrue(entry.DataID ==testData.ID);
        }

        [TestMethod]
        public async Task Save_Outbox_List()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitPersonAPIMapper();
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
                    ID = Guid.NewGuid()
                };
                outboxes.Add(testData);
            }
            
            //Act
            service.SaveOutBox(outboxes);
            await uow.Save();
            //Assert
            foreach (var item in outboxes)
            {
                var entry = DB.Outbox.FirstOrDefault(d => d.DataID == item.ID);
                Assert.IsNotNull(entry);
                Assert.IsTrue(entry.DataID == item.ID);
            }
     
        }
    }
}
