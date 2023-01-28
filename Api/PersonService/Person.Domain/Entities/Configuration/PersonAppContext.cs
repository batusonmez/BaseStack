using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Person.Domain.Entities.Configuration
{
    public class TuyepContextDesignFactory : IDesignTimeDbContextFactory<PersonAppContext>
    {
        public PersonAppContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();

            //  var connectionString = configuration.GetConnectionString("ConnectionString");
            var connectionString = "Server=.;Initial Catalog=PersonDB;user id=sa; password=1234qqqQ;MultipleActiveResultSets=true;Encrypt=False";

            var optionsBuilder = new DbContextOptionsBuilder<PersonAppContext>()
                .UseSqlServer(connectionString);

            return new PersonAppContext(optionsBuilder.Options);
        }
    }
}
