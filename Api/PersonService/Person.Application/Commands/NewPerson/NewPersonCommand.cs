using MediatRDispatcher;
using Person.Application.DTO;

namespace Person.Application.Commands.NewPerson
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
