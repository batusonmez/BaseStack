using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Person.Domain.Entities.Configuration
{
    public class PersonConfig : BaseConfig<Person>
    {
        public override void Configure(EntityTypeBuilder<Person> builder)
        {
            base.Configure(builder);
            builder.Property(d=>d.Name).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Surname).IsRequired().HasMaxLength(200);
            builder.Property(d=>d.City).HasMaxLength(200);

        }
    }
}
