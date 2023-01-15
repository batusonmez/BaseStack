
using Person.Domain.DTO;

namespace Person.Domain.Commands.NewPerson
{
    public class NewPersonResponse
    {
        public PersonDTO  Person{ get; set; }

        public NewPersonResponse(PersonDTO person)
        {
            Person = person;
        }
    }
}
