
using Person.Application.DTO;

namespace Person.Application.Commands.NewPerson
{
    public class NewPersonResponse
    {
        public PersonDTO  Person{ get; set; }
        public Guid? ID { get; set; }
        public NewPersonResponse(PersonDTO person)
        {
            Person = person;
        }
    }
}
