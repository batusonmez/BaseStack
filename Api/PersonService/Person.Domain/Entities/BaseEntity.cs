using System.ComponentModel.DataAnnotations;

namespace Person.Domain.Entities
{
    internal abstract class BaseEntity
    {
        [Key]
        public int ID { get; set; }
    }
}
