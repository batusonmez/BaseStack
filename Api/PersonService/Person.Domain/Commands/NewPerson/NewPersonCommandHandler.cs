using AutoMapper;
using MediatR;
using Person.Domain.DTO;
using Person.Domain.Entities;
using Repository;

namespace Person.Domain.Commands.NewPerson
{
    internal class NewPersonCommandHandler : IRequestHandler<NewPersonCommand, NewPersonResponse>
    {
        private readonly IMapper mapper;
        private readonly IRepository<Entities.Person> personRepository;
        private readonly IUOW uow;

        public NewPersonCommandHandler(IMapper mapper,
            IRepository<Entities.Person> personRepository,
            IUOW uow)
        {
            this.mapper = mapper;
            this.personRepository = personRepository;
            this.uow = uow;
        }
        public async Task<NewPersonResponse> Handle(NewPersonCommand request, CancellationToken cancellationToken)
        {
            
            var entity = mapper.Map<Entities.Person>(request.Person);
            personRepository.Insert(entity);
            var personDTO = mapper.Map<PersonDTO>(entity);
            var resp = new NewPersonResponse(personDTO);
            await uow.Save();

            return resp;
        }
    }
}
