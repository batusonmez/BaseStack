 
namespace Northwind.Domain.Entities
{
    public abstract class BaseEntity
    { 
        public Guid ID { get; set; }= Guid.NewGuid();

        public DateTime CreationDate { get; set; } = DateTime.Now;
    }
}
