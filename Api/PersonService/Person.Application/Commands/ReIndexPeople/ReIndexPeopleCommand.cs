using MediatRDispatcher;

namespace Person.Application.Commands.NewPerson
{
    public class ReIndexPeopleCommand : BaseCommand<ReIndexPeopleResponse>
    { 

        public ReIndexPeopleCommand( )
        {
            
        }
    }
}
