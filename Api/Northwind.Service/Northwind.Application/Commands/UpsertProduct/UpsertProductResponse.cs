
using Person.Application.DTO;

namespace Northwind.Application.Commands.UpsertProduct
{
    public class UpsertProductResponse
    {
        public PersonDTO  Person{ get; set; }
        public Guid? ID { get; set; }
        public UpsertProductResponse(PersonDTO person)
        {
            Person = person;
        }
    }
}
