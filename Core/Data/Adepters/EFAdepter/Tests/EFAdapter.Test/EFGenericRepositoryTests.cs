using EFAdapter.Models;
using EFAdapter.Test.Models;
using EFAdapter.Tests.Models;
using Microsoft.EntityFrameworkCore;

namespace EFAdapter.Tests
{
    [TestClass]
    public class EFGenericRepositoryTests
    {
        TestDBContext? DB;
        public EFGenericRepositoryTests()
        {

        }

        [TestInitialize]
        public void Setup()
        {
            var dbContextOptions = new DbContextOptionsBuilder<TestDBContext>().UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString().Substring(0,5)).Options;
            DB = new TestDBContext(dbContextOptions);
       
        } 

        private EFUnitOfWork GetUnitOfWork()
        {
            if (DB == null)
            {
                Assert.Fail("Can not initialize DB");
            }
            return new EFUnitOfWork(DB);
        }
         
        [TestMethod]
        public async Task Should_Insert_Data_and_Retrive()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);
            var entity = new TestEnity()
            {
                Age = 1,
                Name = "TestName",
                Surname = "TestSurname",
                ID = Guid.NewGuid(),
                CreatinDate = DateTime.Now
            };

            //Act
            repository.Insert(entity);
            await uow.Save();

            //Assert
            var inserted = repository.GetByID( entity.ID) ;
            Assert.IsNotNull(inserted);
            Assert.AreEqual(entity.ID, inserted.ID);

        }

        [TestMethod]
        public async Task Should_Insert_List_and_Retrive()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);

            var entities=new List<TestEnity>();
            for (int i = 0; i < 100; i++)
            {
                var entity = new TestEnity()
                {
                    Age =i,
                    Name = Guid.NewGuid().ToString(),
                    Surname = Guid.NewGuid().ToString(),
                    ID = Guid.NewGuid(),
                    CreatinDate = DateTime.Now
                };
                entities.Add(entity);
            }

    

            //Act
            repository.Insert(entities);
            await uow.Save();

            //Assert
            foreach (var item in entities)
            {
                var inserted = repository.GetByID(item.ID);
                Assert.IsNotNull(inserted);
                Assert.AreEqual(item.ID, inserted.ID);
            }
       

        }

        [TestMethod]
        public void Should_Search_Work()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);
            var entity1 = new TestEnity()
            {
                Age = 1,
                Name = "VALID Name",
                Surname = "TestSurname",
                ID = Guid.NewGuid(),
                CreatinDate = DateTime.Now
            };
            var entity2 = new TestEnity()
            {
                Age = 1,
                Name = "xxxxx",
                Surname = "yyy yyy",
                ID = Guid.NewGuid(),
                CreatinDate = DateTime.Now
            };
            var db = uow.Context as TestDBContext;
            db.TestEnities.Add(entity1);
            db.TestEnities.Add(entity2);
            db.SaveChanges();

            //Act
            var entity1Result = repository.Get(d => d.Name != null && d.Name.Contains("VALID"));
            var NotFounResult = repository.Get(d => d.Name != null && d.Name.Contains("yyy"));

            //Assert            
            Assert.IsTrue(entity1Result.Any() && entity1Result.Count() == 1);
            Assert.AreEqual(entity1Result.First().ID, entity1.ID);
            Assert.IsTrue(!NotFounResult.Any());

        }
         
        [TestMethod]
        public async Task Should_Update()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);
            var entity = new TestEnity()
            {
                Age = 1,
                Name = "To update",
                Surname = "TestSurname",
                ID = Guid.NewGuid(),
                CreatinDate = DateTime.Now
            };

            var db = uow.Context as TestDBContext;
            db.TestEnities.Add(entity);
            db.SaveChanges();

            //Act
            var entityResult = repository.Get(d => d.ID == entity.ID).First();
            entityResult.Name = "Updated";
            repository.Update(entityResult);
            await uow.Save();

            //Assert            
            var updated = repository.Get(d => d.ID == entity.ID).FirstOrDefault();
            Assert.IsNotNull(updated);
            Assert.AreEqual(updated.ID, entity.ID);
            Assert.AreEqual(updated.Name, "Updated");
        }
         
        [TestMethod]
        public async Task Should_Delete()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);
            var entity = new TestEnity()
            {
                Age = 1,
                Name = "To Delete",
                Surname = "TestSurname",
                ID = Guid.NewGuid(),
                CreatinDate = DateTime.Now
            };

            var db = uow.Context as TestDBContext;
            db.TestEnities.Add(entity);
            db.SaveChanges();

            //Act
            repository.Delete(entity.ID);
            await uow.Save();

            //Assert            
            var deleted = repository.GetByID(  entity.ID) ;
            Assert.IsNull(deleted);
        }
         
        [TestMethod]
        public  void Should_Paged_Valid()
        {
            // Arrange
            var uow = GetUnitOfWork();
            var repository = new EFRepository<TestEnity>(uow);
            

            var db = uow.Context as TestDBContext;

            for (int i = 0; i < 100; i++)
            {
                var entity = new TestEnity()
                {
                    Age = i,
                    Name = "paged",
                    Surname = "test",
                    ID = Guid.NewGuid(),
                    CreatinDate = DateTime.Now
                };
                db.TestEnities.Add(entity);
                db.SaveChanges();
            }


            //Act
            TestDataQuery<TestEnity> query = new()
            {
                Page =3,
                PageSize = 10
            };
           var test1= repository.GetPaged(query);
            query.Filter = d => d.Age > 50;
            var test2 = repository.GetPaged(query);


            //Assert            
            Assert.IsTrue( test1.Total == 100);
            Assert.IsTrue(test2.Total == 49);

            Assert.IsTrue(test1.Count() == 10);
            Assert.IsTrue(test2.Count() == 10);

            Assert.IsTrue(test1.Any(d=>d.Age == 23));
            Assert.IsTrue(test2.Any(d => d.Age == 78));

        }

    }
}