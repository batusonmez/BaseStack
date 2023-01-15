using MediatRDispatcher;
using Person.Domain.DTO;

namespace Person.Domain.Commands.NewPerson
{
    public class NewPersonCommand : BaseCommand<NewPersonResponse>
    {
       public PersonDTO Person { get; set; }

        public NewPersonCommand(PersonDTO person)
        {
            Person = person;
        }
    }
}
