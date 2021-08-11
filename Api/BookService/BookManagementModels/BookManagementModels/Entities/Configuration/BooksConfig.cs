using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagementModels.Entities.Configuration
{ 
    /// <summary>
    /// Entity Configuration
    /// </summary>
    public class BooksConfiguration : IEntityTypeConfiguration<Books>
    {
        public void Configure(EntityTypeBuilder<Books> builder)
        {
            builder.HasKey(b => b.ID);
            builder.Property(b => b.Title).IsRequired().HasMaxLength(500);
            builder.Property(b => b.Description).IsRequired();
            builder.Property(b => b.CreationDate).IsRequired();            
        }
    }
}
