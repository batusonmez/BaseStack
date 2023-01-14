using Microsoft.EntityFrameworkCore;

namespace Person.Domain.Entities
{
    internal class PersonAppContext : DbContext
    {
        protected override void OnConfiguring
     (DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase(databaseName: "PersonDB");
        }
        public DbSet<Person> Person { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>().HasData(
                new Person(){ID=1, Name = "Engin", Surname = "Özdemir", City = "Ankara" },
                new Person(){ ID = 2, Name = "Emine", Surname = "Yetkin",City = "Ankara" },
                new Person() { ID = 3, Name = "Mike", Surname = "Brown", City = "London" }
                );
        }

    }
}
