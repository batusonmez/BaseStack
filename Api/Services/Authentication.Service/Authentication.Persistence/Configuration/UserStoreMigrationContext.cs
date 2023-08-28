using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
namespace Authentication.Persistence.Configuration
{
    public class UserDBContextDesignFactory : IDesignTimeDbContextFactory<UserStoreContext>
    {
        public UserStoreContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();
            
            var connectionString = "Server=.;Initial Catalog=UserStore;user id=sa; password=1234qqqQ;MultipleActiveResultSets=true;Encrypt=False";

            var optionsBuilder = new DbContextOptionsBuilder<UserStoreContext>()
                .UseSqlServer(connectionString);
            optionsBuilder.UseOpenIddict();
            return new UserStoreContext(optionsBuilder.Options);
        }
    }
}
