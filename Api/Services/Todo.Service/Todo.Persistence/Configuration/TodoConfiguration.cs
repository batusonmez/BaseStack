using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders; 

namespace Northwind.Persistence.Configuration
{
    public class TodoConfiguration : IEntityTypeConfiguration<Todo.Domain.Entities.Todo>
    {
        public void Configure(EntityTypeBuilder<Todo.Domain.Entities.Todo> builder)
        {
            builder.HasKey(d => d.ID);
            builder.Property(d=>d.Title).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Description).IsRequired().HasMaxLength(3000);
            builder.Property(d => d.CreateDate).IsRequired();
            


        }
    }
}
