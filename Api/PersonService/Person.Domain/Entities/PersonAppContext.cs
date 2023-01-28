using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;
using Person.Domain.Entities.Configuration;

namespace Person.Domain.Entities
{
    public class PersonAppContext : DbContext
    {
        public string? ConnectionString { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
        public DbSet<Person> Person { get; set; }
        public DbSet<Outbox> Outbox { get; set; }

        public PersonAppContext(string connection)
        {

            ConnectionString = connection; 
        }

        public PersonAppContext()
        {

        }

        public PersonAppContext(DbContextOptions<PersonAppContext> options)
        {
            ConnectionString = options.FindExtension<SqlServerOptionsExtension>().ConnectionString;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new OutboxConfig());            
        }

    }
}
