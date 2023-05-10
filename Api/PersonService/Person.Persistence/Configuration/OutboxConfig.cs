using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Person.Domain.Entities;

namespace Person.Persistence.Configuration
{
    public class OutboxConfig : BaseConfig<Outbox>
    {
        public override void Configure(EntityTypeBuilder<Outbox> builder)
        {
            base.Configure(builder);
            builder.Property(d=>d.DataType).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Data).IsRequired();
            builder.Property(d => d.DataID).IsRequired();
        }
    }
}
