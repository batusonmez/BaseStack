using AutoMapper;
using EFAdapter;
using Microsoft.EntityFrameworkCore;
using Moq;
using Northwind.Application.Maps;
using Northwind.Application.Services.Index;
using Northwind.Persistence;
using Repository;

namespace Northwind.Test
{
    public abstract class BaseTest
    {
        public static string? DBSeed { get; set; }

        private static NorthwindContext? db;
        public static NorthwindContext DB
        {
            get
            {
                if (string.IsNullOrEmpty(DBSeed))
                {
                    DBSeed = Guid.NewGuid().ToString().Substring(0, 6);
                }

                if (db == null)
                {
                    var dbContextOptions = new DbContextOptionsBuilder<NorthwindContext>().UseInMemoryDatabase(databaseName: DBSeed).Options;
                    db = new NorthwindContext(dbContextOptions);
                }
                return db;
            }
        }

        public void ClearTestConnection()
        {
            db = null;
        }

        public void ResetTestDB()
        {
            DBSeed = null;
            ClearTestConnection();
        }
        public IUOW InitUOW()
        {
            return new EFUnitOfWork(DB);
        }

        public IMapper InitNorthwindAPIMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<NorthwindMapProfile>());
            return config.CreateMapper();
        }

        public IIndexService MockIndexService(IEnumerable<string>  result =null)
        {
            List<string> response = new();
            if (result != null)
            {
                response.AddRange(result);
            }
            Mock<IIndexService> mockService = new();
            mockService.Setup(d => d.SearchKeyword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<int>())).Returns(Task.FromResult(response.AsEnumerable()));
            return mockService.Object;
        }

    }
}
