using EFAdapter;
using Person.Domain.Entities;
using Person.Application.DTO;
using Person.Application.Commands.NewPerson;
using Person.Infrastructure.Services.Outbox;

namespace Person.Test
{
    [TestClass]
    public class NewPersonCommandTests : BaseTest
    {
        [TestMethod]
        public async Task New_Person_Command()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitPersonAPIMapper();            
            var outboxRepository = new EFRepository<Outbox>(uow);
            var outboxService = new OutboxService(outboxRepository,mapper, uow);
            var repository = new EFRepository<Domain.Entities.Person>(uow);
            var handler = new NewPersonCommandHandler(mapper,repository, outboxService, uow);

            var testData = new NewPersonCommand(new PersonDTO()
            {
                City = "CityTest",
                Name = "NameTest",
                Surname = "SurnameTest"
            });


            //Act
           var response= await handler.Handle(testData,CancellationToken.None);

            //Assert
            var person = DB.Person.FirstOrDefault(d => d.ID == response.ID);
            var outbox = DB.Outbox.FirstOrDefault(d => d.DataID == response.ID);
            Assert.IsNotNull(person);
            Assert.IsNotNull(outbox);
            Assert.IsTrue(person.Name == "NameTest");
            Assert.IsTrue(person.Surname == "SurnameTest");
            Assert.IsTrue(!string.IsNullOrEmpty(outbox.Data) && outbox.Data.Contains("NameTest") && outbox.Data.Contains("SurnameTest"));
        }
    }
}
