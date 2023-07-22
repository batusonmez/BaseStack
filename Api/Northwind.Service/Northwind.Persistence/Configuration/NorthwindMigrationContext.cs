using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Northwind.Persistence.Configuration
{
    public class TuyepContextDesignFactory : IDesignTimeDbContextFactory<NorthwindContext>
    {
        public NorthwindContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();

            //  var connectionString = configuration.GetConnectionString("ConnectionString");
            var connectionString = "Server=.;Initial Catalog=Northwind;user id=sa; password=1234qqqQ;MultipleActiveResultSets=true;Encrypt=False";

            var optionsBuilder = new DbContextOptionsBuilder<NorthwindContext>()
                .UseSqlServer(connectionString);

            return new NorthwindContext(optionsBuilder.Options);
        }
    }
}
