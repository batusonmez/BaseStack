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
            var repository = new EFRepository<Outbox>(uow);
            var service = new OutboxService(repository, uow);
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
        }
    }
}
