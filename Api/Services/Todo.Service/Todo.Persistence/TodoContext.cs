using Microsoft.EntityFrameworkCore;
using Northwind.Persistence.Configuration;

namespace Todo.Persistence
{
    public class TodoContext : DbContext
    {
        public DbSet<Todo.Domain.Entities.Todo> Todos { get; set; }
        public TodoContext(DbContextOptions<TodoContext> options) : base(options)
        {
            ChangeTracker.AutoDetectChangesEnabled = false;
            ChangeTracker.LazyLoadingEnabled = false;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new TodoConfiguration()); 
        }
    }
}
