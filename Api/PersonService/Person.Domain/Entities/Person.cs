
namespace Person.Domain.Entities
{
    internal class Person:BaseEntity
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? City { get; set; }
    }
}
