using System.ComponentModel.DataAnnotations;

namespace Person.Domain.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
