using AutoMapper;
using EFAdapter;
using Microsoft.EntityFrameworkCore;
using Northwind.Application.Maps;
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

        public void ClearTestContext()
        {
            db = null;
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

    }
}
