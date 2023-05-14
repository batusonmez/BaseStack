using EFAdapter;
using Person.Domain.Entities;
using Person.Application.DTO;
using Person.Application.Commands.NewPerson;
using Person.Infrastructure.Services.Outbox;
using Person.Application.Queries.ListPeople;
using Person.Application.Services.Outbox;

namespace Person.Test
{
    [TestClass]
    public class ListPeopleQueryTests : BaseTest
    {
        [TestMethod]
        public async Task List_People_Query()
        {
            // Arrange
            var uow = InitUOW();
            var mapper = InitPersonAPIMapper();            
            var repository = new EFRepository<Domain.Entities.Person>(uow);
            var handler = new ListPeopleQueryHandler(mapper, repository );
            var query1 = new ListPeopleQuery()
            {
                Page = 2,
                PageSize = 10 
            };
            var query2 = new ListPeopleQuery()
            {
                Page = 3,
                PageSize = 25
            };

            for (int i = 0; i < 100; i++)
            {
                var testData = new Domain.Entities.Person()
                {
                    ID=Guid.NewGuid(),
                    City = Guid.NewGuid().ToString(),
                    Name = Guid.NewGuid().ToString(),
                    Surname = Guid.NewGuid().ToString()
                } ;
                DB.Person.Add(testData);
                DB.SaveChanges();
            }


            //Act 
           var response= await handler.Handle( query1,CancellationToken.None);
            var response2 = await handler.Handle(query2, CancellationToken.None);

            //Assert 
            Assert.IsTrue(response.Count()==query1.PageSize );
            Assert.IsTrue(response2.Count() == query2.PageSize);
            Assert.IsTrue(response.Total==100 );
            Assert.IsTrue(response2.Total == 100);
            Assert.IsTrue(response.ElementAt(0).Name != response2.ElementAt(0).Name);
        }
    }
}
