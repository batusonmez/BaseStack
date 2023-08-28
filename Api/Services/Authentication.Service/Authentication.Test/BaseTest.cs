using Authentication.Application.Maps;
using Authentication.Persistence;
using AutoMapper;
using EFAdapter;
using Microsoft.EntityFrameworkCore;
using Repository;

namespace Authentication.Test
{
    public abstract class BaseTest
    {
        public static string? DBSeed { get; set; }

        private static UserStoreContext? db;
        public static UserStoreContext DB
        {
            get
            {
                if (string.IsNullOrEmpty(DBSeed))
                {
                    DBSeed = Guid.NewGuid().ToString().Substring(0, 6);
                }

                if (db == null)
                {
                    var dbContextOptions = new DbContextOptionsBuilder<UserStoreContext>().UseInMemoryDatabase(databaseName: DBSeed);
                    dbContextOptions.UseOpenIddict();
                    db = new UserStoreContext(dbContextOptions.Options);
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

        public IMapper InitMapper()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticationAppMapProfile>());
            return config.CreateMapper();
        }

        

    }
}
