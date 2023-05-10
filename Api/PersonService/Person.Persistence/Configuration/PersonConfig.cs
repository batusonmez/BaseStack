using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DomainEnties = Person.Domain.Entities;

namespace Person.Persistence.Configuration
{
    public class PersonConfig : BaseConfig<DomainEnties.Person>
    {
        public override void Configure(EntityTypeBuilder<DomainEnties.Person> builder)
        {
            base.Configure(builder);
            builder.Property(d => d.Name).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Surname).IsRequired().HasMaxLength(200);
            builder.Property(d => d.City).HasMaxLength(200);

        }
    }
}
