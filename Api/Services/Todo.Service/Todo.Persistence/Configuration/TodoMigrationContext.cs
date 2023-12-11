using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Todo.Persistence;

namespace Northwind.Persistence.Configuration
{
    public class TodoContextDesignFactory : IDesignTimeDbContextFactory<TodoContext>
    {
        public TodoContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder().Build();

            //  var connectionString = configuration.GetConnectionString("ConnectionString");
            var connectionString = "Server=.;Initial Catalog=TodoApp;user id=sa; password=1234qqqQ;MultipleActiveResultSets=true;Encrypt=False";

            var optionsBuilder = new DbContextOptionsBuilder<TodoContext>()
                .UseSqlServer(connectionString);

            return new TodoContext(optionsBuilder.Options);
        }
    }
}
