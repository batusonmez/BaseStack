using Microsoft.EntityFrameworkCore;
using Person.Domain.Entities;
using Person.Persistence.Configuration;

namespace Person.Persistence
{
    public class PersonAppContext : DbContext
    { 
        public DbSet<Person.Domain.Entities.Person> Person { get; set; }
        public DbSet<Outbox> Outbox { get; set; }
 
  
        public PersonAppContext(DbContextOptions<PersonAppContext> options) : base(options)
        {
           
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PersonConfig());
            modelBuilder.ApplyConfiguration(new OutboxConfig());
        }

    }
}
