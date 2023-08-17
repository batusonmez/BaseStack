using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Northwind.Domain.Entities;

namespace Northwind.Persistence.Configuration
{
    public class TerritoryConfiguration : IEntityTypeConfiguration<Territory>
    {
        public void Configure(EntityTypeBuilder<Territory> builder)
        {
            builder.HasKey(d => d.TerritoryId);
        }
    }
}
